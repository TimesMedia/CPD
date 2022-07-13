using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CPD.Data;
using CPD.Business;

namespace CPD.Web
{
    public partial class Enrol2 : System.Web.UI.Page
    {
        QuestionareDoc.AvailibleDataTable gAvailibleDataTable = new QuestionareDoc.AvailibleDataTable();
       
        protected void Page_Load(object sender, EventArgs e)
        {
            string lStage = "Start";

            try
            {
                gAvailibleDataTable = ModuleData.GetAvailibleModules(Int32.Parse(Session["CustomerId"].ToString()), Int32.Parse(Session["SurveyId"].ToString()));
                GridViewCatalogue.DataSource = gAvailibleDataTable;
                GridViewCatalogue.DataBind();

                lStage = "Survey";

                Data.QuestionareDoc.Survey2DataTable lSurveyTable = new QuestionareDoc.Survey2DataTable();
                Data.QuestionareDocTableAdapters.Survey2TableAdapter lSurveyTableAdapter = new Data.QuestionareDocTableAdapters.Survey2TableAdapter();
                lSurveyTableAdapter.AttachConnection();
                lSurveyTableAdapter.FillBy(lSurveyTable, (int)Session["SurveyId"]);
                if (lSurveyTable.Count != 1)
                {
                    throw new Exception("There is a problem with surveyId " + Session["SurveyId"].ToString());
                }

                lStage = "SurveyHeading";
                Label3.Text = lSurveyTable[0].Naam; 

                lStage = "Ebook";

                if (!lSurveyTable[0].IsEBookURLNull())
                {
                    MainContent_HyperLink1.HRef = "~/EBooks/" + lSurveyTable[0].EBookURL;

                    // Replace suffix for the thumbnail
                    string[] lSuffixArray = lSurveyTable[0].EBookURL.Split('.');
                    string lSuffix = lSuffixArray[lSuffixArray.Length - 1];
                    string lThumbNail = lSurveyTable[0].EBookURL.Replace(lSuffix, "jpg");
                    HyperLink1Image.Src = "~/EBooks/" + lThumbNail;
                    MainContent_HyperLink1.Target = "_blank";
                }
                else
                {
                    MainContent_HyperLink1.Visible = false;
                }

                lStage = "Horisontal";
            
                                // Horisontal image
                if (!lSurveyTable[0].IsHorisontalAdvertURLNull())
                {
                    HorisontalImage.ImageUrl = "~/images/" + lSurveyTable[0].HorisontalAdvertURL;
                }
                else
                {
                    HorisontalImage.Visible = false;
                }

                lStage = "Vertical";

                // Verrtical image
                if (!lSurveyTable[0].IsVerticalAdvertURLNull())
                {
                    VerticalImage.ImageUrl = "~/images/" + lSurveyTable[0].VerticalAdvertURL;
                }
                else
                {
                    VerticalImage.Visible = false;
                }
            }

            catch (Exception ex)
            {
                //Display all the exceptions

                Exception CurrentException = ex;
                int ExceptionLevel = 0;
                do
                {
                    ExceptionLevel++;
                    ExceptionData.WriteException(1, ExceptionLevel.ToString() + " " + CurrentException.Message, this.ToString(), "Page_Load", 
                        "Stage = " + lStage + " Survey = " + Session["SurveyId"].ToString());
                    CurrentException = CurrentException.InnerException;
                } while (CurrentException != null);
                throw ex;
            }

        }

        protected void ButtonHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }
            

        protected void ButtonHelp_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/HelpForm.aspx", false);
        }

        protected void ButtonHistory_Click(object sender, EventArgs e)
        {
            History();
        }

        private void History()
        {
            try
            {
                //Ensure that the user is logged in.

                if (Session["CustomerId"] == null)
                {
                    Session["NextPage"] = "History";
                    Response.Redirect("./Login2.aspx", false);
                }
                else
                {

                    Response.Redirect("./History.aspx", false);
                }
            }
            catch (Exception ex)
            {
                //Display all the exceptions

                Exception CurrentException = ex;
                int ExceptionLevel = 0;
                do
                {
                    ExceptionLevel++;
                    ExceptionData.WriteException(1, ExceptionLevel.ToString() + " " + CurrentException.Message, this.ToString(), "History", "");
                    CurrentException = CurrentException.InnerException;
                } while (CurrentException != null);
                throw ex;
            }
        }
    

        protected void GridViewCatalogue_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // Take precautions in case the session has expired. 
                if (Session["CustomerId"] == null)
                {
                    Session["NextPage"] = "Enrol";
                    Response.Redirect("./Login2.aspx", false);
                }

                // Check to see if this person is busy with any test

                if (ResultData.BuzyWithTest(Int32.Parse(Session["CustomerId"].ToString())))
                {
                    LabelResponse.Text = "Please complete and submit your current test,  or cancel it, before commencing to a new test.";
                    return;
                }

                // Ensure that a module has been selected

                if (this.GridViewCatalogue.SelectedIndex < 0)
                {
                    LabelResponse.Text = "Please select a test first";
                    return;
                }

                // Initialise the test
                string ErrorMessage;
                int ResultId = ResultBiz.InitialiseTest(Int32.Parse(Session["CustomerId"].ToString()), (int)this.GridViewCatalogue.SelectedValue, out ErrorMessage);
                if (ErrorMessage == "OK")
                {
                    Response.Redirect("./TestForm2.aspx", false);
                }
                else
                {
                    LabelResponse.Text = ErrorMessage;
                }
            }
            catch (Exception ex)
            {
                //Display all the exceptions

                Exception CurrentException = ex;
                int ExceptionLevel = 0;
                do
                {
                    ExceptionLevel++;
                    ExceptionData.WriteException(1, ExceptionLevel.ToString() + " " + CurrentException.Message, this.ToString(), "GridViewCatalogue_SelectedIndexChanged", "");
                    CurrentException = CurrentException.InnerException;
                } while (CurrentException != null);
                throw ex;
            }
        }



    }
}