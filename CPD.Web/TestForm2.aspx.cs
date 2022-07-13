using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CPD.Data;
using CPD.Business;
using System.Xml;

namespace CPD.Web
{
    public partial class TestForm2 : System.Web.UI.Page
    {
        private QuestionareDoc.ModuleDataTable gModuleTable = new QuestionareDoc.ModuleDataTable();
        private Data.QuestionareDocTableAdapters.ModuleTableAdapter gModuleTableAdapter = new Data.QuestionareDocTableAdapters.ModuleTableAdapter();
        private Data.ResultDoc.AnswerDataTable gAnswerTable = new ResultDoc.AnswerDataTable();
        private XmlDataSource gXmlDataSource;

        public string Modules { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
              PageLoadProcessing();
        }

        private void PageLoadProcessing()
        {
            string lStage = "Start";

            try
            {
                if (Session["CustomerId"] == null)
                {
                    Response.Redirect("~/Login2.aspx");
                }

                gModuleTableAdapter.AttachConnection();

                // Clear previous responses

                LabelResponse.Text = "";
 
                if (!this.IsPostBack)
               
                {   // This is a first invocation. Start at the beginning. 
                    
                    lStage = "Is not PostBack";
                    // If the user is busy with a test, display it.
                    
                    ResultDoc.HistoryDataTable lHistory = new ResultDoc.HistoryDataTable();

                    ResultData.GetCurrentTest(Int32.Parse(Session["CustomerId"].ToString()), ref lHistory);
                  
                    if (lHistory.Count != 1)
                    {
                        throw new Exception("There is no current test");
                    }

                    gModuleTableAdapter.FillBy(gModuleTable, lHistory[0].ModuleId);

                    if (!gModuleTable[0].IsEBookURLNull())
                    {
                        HyperLink2.HRef = "~/EBooks/" + gModuleTable[0].EBookURL;

                        // Replace suffix for the thumbnail
                        string[] lSuffixArray = gModuleTable[0].EBookURL.Split('.');
                        string lSuffix = lSuffixArray[lSuffixArray.Length - 1];
                        string lThumbNail = gModuleTable[0].EBookURL.Replace(lSuffix, "jpg");

                        HyperLink1Image.Src = "~/EBooks/" + lThumbNail;
                        HyperLink2.Target = "_blank";
                    }
                    //else
                    //{
                    //    ebookDiv.Style.Add("display", "none");
                    //    //HyperLink1.Visible = false;
                    //}

                    if (!gModuleTable[0].IsAdvertURLNull())
                    {
                        VerticalAdvert.ImageUrl = "~/images/" + gModuleTable[0].AdvertURL;
                    }
                    else
                    {
                        VerticalAdvert.Visible = false;
                    }

                    //// Horisontal image
                    //if (!gModuleTable[0].IsHorisontalAdvertURLNull())
                    //{
                    //    HorisontalImage.ImageUrl = "~/images/" + gModuleTable[0].HorisontalAdvertURL;
                    //}
                    //else
                    //{
                    //    HorisontalImage.Visible = false;
                    //}

 
                    gXmlDataSource = new XmlDataSource();
                    gXmlDataSource.EnableCaching = false;
                    gXmlDataSource.TransformFile = "~/App_Data/Transform2.xslt";
                    gXmlDataSource.Data = ModuleData.GetQuestions(lHistory[0].ModuleId);
                    gXmlDataSource.DataBind();

                    TreeView1.DataSource = gXmlDataSource;  
                    TreeView1.DataBind();
                    
                    TreeView1.Nodes[0].ChildNodes[0].Expand();
                    TreeView1.Nodes[0].ChildNodes[0].ChildNodes[0].Select(); // This raises an event in the ObjectDataSourceAnswer
                    SingleArticle.Text = TreeView1.SelectedNode.Parent.Text;
                    SingleQuestion.Text = TreeView1.SelectedNode.Text;

                    string lLastQuestionId = TreeView1.Nodes[0].ChildNodes[0].ChildNodes[0].Value;

                    gAnswerTable = ResultData.GetAnswer(Int32.Parse(Session["CustomerId"].ToString()), Int32.Parse(lLastQuestionId));
                    CheckBoxListAnswer.DataSource = gAnswerTable;
                    CheckBoxListAnswer.DataBind();
                    Session.Add("LastQuestionId", lLastQuestionId);
                  
                }
                else
                {
                    lStage = "IsPostBack";

                    //Save the answer of the previous question, before repopulating the selected answer. I.e. the treeview might have changed the question in the mean time.
                    //If it did not, e.g. if the next button was hit, no harm is done.
                    // Do the update to the database from here
                    ResultData.SetAnswer(Int32.Parse(Session["CustomerId"].ToString()), Int32.Parse(Session["LastQuestionId"].ToString()), CodeAnswer());
                   

                    // Remember what your questionid was.
                    //Session["LastQuestionId"] = TreeView1.SelectedValue;

                    // Set the new question, if it was changed in the TreeView

                    SingleArticle.Text = TreeView1.SelectedNode.Parent.Text;
                    SingleQuestion.Text = TreeView1.SelectedNode.Text;

                    string lLastQuestionId = TreeView1.SelectedNode.Value;

                    gAnswerTable = ResultData.GetAnswer(Int32.Parse(Session["CustomerId"].ToString()), Int32.Parse(lLastQuestionId));
                    CheckBoxListAnswer.DataSource = gAnswerTable;
                    CheckBoxListAnswer.DataBind();
                    Session.Add("LastQuestionId", lLastQuestionId);

                }

                lStage = "DisplayExistingAnswerFromTreeView()"; 

                DisplayExistingAnswerFromTreeView(); 
            }
            catch (Exception ex)
            {
                //Display all the exceptions

                Exception CurrentException = ex;
                int ExceptionLevel = 0;
                do
                {
                    ExceptionLevel++;
                    ExceptionData.WriteException(1, ExceptionLevel.ToString() + " " + CurrentException.Message, this.ToString(), "PageLoadProcessing", "CustomerId = " + Session["CustomerId"].ToString() + "Stage = " + lStage);
                    CurrentException = CurrentException.InnerException;
                } while (CurrentException != null);

                return;
            }
        }
          
        private void DisplayExistingAnswerFromTreeView()
        {
            try
            {
                // Load the answer to the new question, by selecting the necessary checkboxes.

                CheckBoxListAnswer.ClearSelection();

                CheckBoxListAnswer.DataBind();

                foreach (ListItem lItem in this.CheckBoxListAnswer.Items)
                {
                    if (lItem.Value == "True")
                    {
                        lItem.Selected = true;
                    }
                } 
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    ExceptionData.WriteException( 1, ex.Message, this.ToString(), "DisplayExistingAnswerFromTreeView", "");
                    throw new Exception(this.ToString() + " : " + "DisplayExistingAnswerFromTreeView" + " : ", ex);
                }
                else
                {
                    throw ex; // Just bubble it up
                }
            }
        }

        private int CodeAnswer()
        {
            try
            {
                int Answer = 0;
                int i = 0;
                foreach (ListItem lItem in this.CheckBoxListAnswer.Items)
                {
                    if (lItem.Selected == true)
                    {
                        Answer = Answer + (int)Math.Pow(2, i);
                    }
                    i++;
                }
                return Answer; 
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    ExceptionData.WriteException(1, ex.Message, this.ToString(), "CodeAnswer(", "");
                    throw new Exception(this.ToString() + " : " + "CodeAnswer(" + " : ", ex);
                }
                else
                {
                    throw ex; // Just bubble it up
                }
            }
        }

        private void ProcessCertificate(object pState)

        {
            try
            {
                CPD.Business.CPDCertificate lCertificate = new Business.CPDCertificate();

                {
                    string lResult;
                    if ((lResult = lCertificate.Render((int)pState)) != "OK")
                    {
                        LabelResponse.Text = "Error rendering certificate";
                        return;
                    }
                }

                {
                    string lResult;
                    if ((lResult = lCertificate.EmailCertificate((int)pState)) != "OK")
                    {
                        LabelResponse.Text = "Error emailing certificate";
                        return;
                    }
                }

               LabelResponse.Text = "Congratulations";

            }
            catch (Exception ex)
            {
                //Display all the exceptions

                Exception CurrentException = ex;
                int ExceptionLevel = 0;
                do
                {
                    ExceptionLevel++;
                    ExceptionData.WriteException(1, ExceptionLevel.ToString() + " " + CurrentException.Message, this.ToString(), "ProcessCertificate", "");
                    CurrentException = CurrentException.InnerException;
                } while (CurrentException != null);

            }
        }

        protected void ButtonSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string lMessage = "";
                int Attempt = 0;
                ResultDoc.HistoryDataTable lHistory = new ResultDoc.HistoryDataTable();

                lHistory.Clear();
                if (!ResultData.GetCurrentTest(Int32.Parse(Session["CustomerId"].ToString()), ref lHistory))
                {
                    LabelResponse.Text = "You do not have a test that can be marked.";
                }
                else
                {
                    Attempt = lHistory[0].Attempt;

                    if (ResultData.Mark(lHistory[0].ResultId) == 1)
                    {
                        Thread lWorkerThread = new Thread(ProcessCertificate);
                        lWorkerThread.SetApartmentState(ApartmentState.STA);
                        object pState = lHistory[0].ResultId;  // Box it
                        lWorkerThread.Start(pState);
                        lWorkerThread.Join();

                        if (LabelResponse.Text.StartsWith("Congratulations"))
                        {
                            lMessage = "Congratulations. You have passed. Your certificate has been emailed to you.";
                        }
                    }
                    else
                    {
                        // Determine if this person can take a supplementary test. 

                        if (Attempt == 1)
                        {
                            lMessage = "Unfortunately, you have been unsuccessful on this test. You are, however, entitled to enrol for ONE additional opportunity to retake the test.";
                        }
                        else
                        {
                            lMessage = "Unfortunately, you have been unsuccessful on both opportunities to pass the test.";
                        }
                    }

                    Response.Redirect("~/Default.aspx?Message=" + lMessage, false);
                } 
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    ExceptionData.WriteException(1, ex.Message, this.ToString(), "ButtonSubmit_Click", "");
                    throw new Exception(this.ToString() + " : " + "ButtonSubmit_Click" + " : ", ex);
                }
                else
                {
                    throw ex; // Just bubble it up
                }
            }
        }

        protected void ButtonHome_Click(object sender, EventArgs e)
        {
            SaveCurrentAnswer(); // Just in case.

            Response.Redirect("~/Default.aspx", false);
        }

        protected void ButtonNext_Click(object sender, EventArgs e)
        {
            string lStage = "Start";
            try
            {
                if (TreeView1.SelectedNode == null)
                {
                    throw new Exception("No node has been selected yet");
                }

                // All I want to do here is changing the selection from the server side, and then I want to simulate another submit from the side of the browser.
                //Calculate the next question

                //Get the current position
                int ArticleIndex = 0;
                int QuestionIndex = 0;

                bool Found = false;
                for (int k = 0; k < (TreeView1.Nodes[0].ChildNodes.Count); k++)
                {
                    if (Found) break;

                    lStage = "k = " + k.ToString();

                    for (int l = 0; l < TreeView1.Nodes[0].ChildNodes[k].ChildNodes.Count; l++)
                    {
                        if (Found) break;

                        lStage = "k = " + k.ToString() + "l = " + l.ToString();

                        if (TreeView1.Nodes[0].ChildNodes[k].ChildNodes[l].Equals(TreeView1.SelectedNode))
                        {
                            ArticleIndex = k;
                            QuestionIndex = l;
                            Found = true;
                            break;
                        }
                    }
                }

                //See if you can increment
                if (QuestionIndex >= TreeView1.Nodes[0].ChildNodes[ArticleIndex].ChildNodes.Count - 1)
                {
                    // You are at the last in the range of questions in this article

                    lStage = "ArticleIndex = " + ArticleIndex.ToString();

                    if (ArticleIndex >= TreeView1.Nodes[0].ChildNodes.Count - 1)
                    {
                        TreeView1.Nodes[0].ChildNodes[ArticleIndex].Collapse();
                        TreeView1.Nodes[0].ChildNodes[0].Expand(); ;
                        // You are at the top of the articles as well. So you have to wrap back to the beginning.
                        ArticleIndex = 0;
                        QuestionIndex = 0;
                    }
                    else
                    {
                        TreeView1.Nodes[0].ChildNodes[ArticleIndex].Collapse();
                        ArticleIndex++;
                        TreeView1.Nodes[0].ChildNodes[ArticleIndex].Expand();
                        QuestionIndex = 0;
                    }
                }
                else
                {
                    // You can up the questionindex
                    QuestionIndex++;
                }

                lStage = "QuestionIndex = " + QuestionIndex.ToString() + "ArticleIndex = " + ArticleIndex.ToString();


                TreeView1.Nodes[0].ChildNodes[ArticleIndex].ChildNodes[QuestionIndex].Select();

                // Now, send this back to the browser, and get it to immediately post again, hopefully with the new selection on the treeview.

                TreeView1.Nodes[0].CollapseAll();

                PageLoadProcessing();
                
            }

            catch (Exception ex)
            {
                //Display all the exceptions

                Exception CurrentException = ex;
                int ExceptionLevel = 0;
                do
                {
                    ExceptionLevel++;
                    ExceptionData.WriteException(1, ExceptionLevel.ToString() + " " + CurrentException.Message, this.ToString(), "ButtonNext_Click", "CustomerId = " + Session["CustomerId"].ToString() + lStage);
                    CurrentException = CurrentException.InnerException;
                } while (CurrentException != null);

                throw ex;
            }        
        }

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            SaveCurrentAnswer();
        }

        private void SaveCurrentAnswer()
        {
            try
            {
                //Save the answer of the current question

                int Answer = CodeAnswer();

                // Do the update to the database from here
                ResultData.SetAnswer(Int32.Parse(Session["CustomerId"].ToString()), Int32.Parse(TreeView1.SelectedValue), Answer);

                // Remember what your questionid was, for future reference.
                Session["LastQuestionId"] = TreeView1.SelectedValue; 
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    ExceptionData.WriteException(1, ex.Message, this.ToString(), "SaveCurrentAnswer", "");
                    throw new Exception(this.ToString() + " : " + "SaveCurrentAnswer" + " : ", ex);
                }
                else
                {
                    throw ex; // Just bubble it up
                }
            }
        }
          
        protected void ButtonQuit_Click(object sender, EventArgs e)
        {
            // Delete the Result record

            ResultDoc.HistoryDataTable lHistory = new ResultDoc.HistoryDataTable(); ;
               if (!ResultData.GetCurrentTest(Int32.Parse(Session["CustomerId"].ToString()), ref lHistory))
                {
                    return;
                }
            
            ResultData.Quit(lHistory[0].ResultId);
            Response.Redirect("~/Default.aspx", true);
        }
   
     }
}