using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CPD.Data;
using CPD.Business;

namespace CPD.Web
{
    public partial class _Default : System.Web.UI.Page
    {
        private QuestionareDoc.SurveyDisplayDataTable gAvailibleSurveys = new QuestionareDoc.SurveyDisplayDataTable();
        private ResultDoc.HistoryDataTable gCurrent = new ResultDoc.HistoryDataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            string Stage = "Login";
            string LoggedInAs = "";
            try
            {
                if (Session["CustomerId"] != null)
                {
                    LoggedInAs = Session["CustomerId"].ToString();
                    ButtonHistory.Visible = true;
                    //ButtonMIMS.Visible = true;
                    LabelCatalogue.Visible = true;

                    LabelGreeting.Text = "You are now logged in as " + Session["Customer"].ToString();
                    gAvailibleSurveys = ModuleData.GetAvailibleSurveys(Int32.Parse(Session["CustomerId"].ToString()));
                    GridViewCatalogue.DataSource = gAvailibleSurveys;
                    GridViewCatalogue.DataBind();
                }
                else
                {
                    LabelGreeting.Text = "You are not currently logged into the system. Your session might have expired. Please log in again.";
                    ButtonHistory.Visible = false;
                    //ButtonMIMS.Visible = false;
                    return;
                }

                Stage = "BuzyWithTest";

                if (ResultData.BuzyWithTest(Int32.Parse(Session["CustomerId"].ToString())))
                {
                    ResultData.GetCurrentTest(Int32.Parse(Session["CustomerId"].ToString()), ref gCurrent);
                    GridViewCurrent.DataSource = gCurrent;
                    GridViewCurrent.DataBind();
                    LabelCurrent.Visible = true;
                    GridViewCurrent.Visible = true;
                }
                else
                {
                    LabelCurrent.Visible = false;
                    GridViewCurrent.Visible = false;
                }

                Stage = "Message";


                if (Request.QueryString["Message"] == "")
                {
                    LabelResponse.Text = ""; // Clear from previous responses.
                }
                else
                {
                    LabelResponse.Text = Request.QueryString["Message"];
                }

                Stage="Switch";

                switch (Session["NextPage"].ToString())
                {
                    case "Enrol":
                        Enrol();
                        break;
                    default:
                        break;
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
                    ExceptionData.WriteException(1, ExceptionLevel.ToString() + " " + CurrentException.Message, this.ToString(), "Page_Load", "Stage = " + Stage + " LoggedInAs = " + LoggedInAs);
                    CurrentException = CurrentException.InnerException;
                } while (CurrentException != null);
                LabelResponse.Text = "Error in Page load " + ex.Message;
            }
        }

        protected void ButtonLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("./Login2.aspx", false);
        }
       

        protected void ButtonHistory_Click(object sender, EventArgs e)
        {
            History();
        }
        //protected void ButtonMimsOnline_Click(object sender, EventArgs e)
        //{
        //    try
        //    {

        //        // See if the customer has received the material


        //        var lContext = new MimsDataContext(Settings.MIMSConnectionString);  // This is the live CPD database.

        //        var lQuery = from lValues in lContext.MIMS_DataContext_AuthoriseCPDIssue(Int32.Parse(Session["CustomerId"].ToString()), 788)
        //                     select lValues;

        //        MIMS_DataContext_AuthoriseCPDIssueResult lResult = lQuery.Single();


        //        if (lResult.Column1 == 0)
        //        {
        //            LabelResponse.Text = "Sorry, you have not subscribed to this issue of MIMS.  Please contact MIMS at 011 280 5856";
        //            return;
        //        }
        //        else
        //        {
        //            // Redirect to electronic version of MIMS

        //            Response.Redirect("~/EBooks/MIMSApril2020/Index.html", true);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //Display all the exceptions

        //        Exception CurrentException = ex;
        //        int ExceptionLevel = 0;
        //        do
        //        {
        //            ExceptionLevel++;
        //            ExceptionData.WriteException(1, ExceptionLevel.ToString() + " " + CurrentException.Message, this.ToString(), "ButtonMimsOnline_Click", "");
        //            CurrentException = CurrentException.InnerException;
        //        } while (CurrentException != null);
        //        throw ex;
        //    }
        //}

        protected void ButtonMimsPortal_Click(object sender, EventArgs e)
        {
            Response.Redirect("http://www.mims.co.za", true);
        }

        private void Read()
        {
            try
            {
                // Ensure that a publication has been selected

                int lSelectedIndex = this.GridViewCatalogue.SelectedIndex;

                if (lSelectedIndex < 0)
                {
                    LabelResponse.Text = "Please select a publication first";
                    return;
                }

                // See if the customer has received the material

                var lContext = new MimsDataContext(Settings.MIMSConnectionString);  // This is the live CPD database.

                var lQuery = from lValues in lContext.MIMS_DataContext_AuthoriseCPDIssue(Int32.Parse(Session["CustomerId"].ToString()),
                    Int32.Parse(this.GridViewCatalogue.SelectedDataKey["IssueId"].ToString()))
                             select lValues;

                MIMS_DataContext_AuthoriseCPDIssueResult lResult = lQuery.Single();


                if (lResult.Column1 == 0)
                {
                    LabelResponse.Text = "Sorry, the required publication has not been delivered to you.  Please contact MIMS at 011 280 5856";
                    return;
                }

                Session["SurveyId"] = (int)this.GridViewCatalogue.SelectedDataKey["SurveyId"];
 
                Response.Redirect("~/EBooks/" + this.GridViewCatalogue.SelectedDataKey["EBookURL"].ToString(), true);
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    ExceptionData.WriteException(1, ex.Message, this.ToString(), "Read", "");
                    throw new Exception(this.ToString() + " : " + "Read" + " : ", ex);
                }
                else
                {
                    throw ex; // Just bubble it up
                }
            }
        }

        private void Enrol()
        {
            try
            {
                // Check to see if the person is not busy with a test

                if (ResultData.BuzyWithTest(Int32.Parse(Session["CustomerId"].ToString())))
                {
                    LabelResponse.Text = "Please complete and submit your current test,  or cancel it, before commencing to a new test.";
                    return;
                }

                // Ensure that a publication has been selected

                int lSelectedIndex = this.GridViewCatalogue.SelectedIndex;

                if ( lSelectedIndex < 0)
                {
                    LabelResponse.Text = "Please select a publication first";
                    return;
                }

                // See if the customer has received the material


                var lContext = new MimsDataContext(Settings.MIMSConnectionString);  // This is the live CPD database.

                var lQuery = from lValues in lContext.MIMS_DataContext_AuthoriseCPDIssue(Int32.Parse(Session["CustomerId"].ToString()),
                    Int32.Parse(this.GridViewCatalogue.SelectedDataKey["IssueId"].ToString()))
                             select lValues;

                MIMS_DataContext_AuthoriseCPDIssueResult lResult = lQuery.Single();


                if (lResult.Column1 == 0)
                {
                    LabelResponse.Text = "Sorry, the required publication has not been delivered to you.  Please contact MIMS at 011 280 5856";
                    return;
                }

                Session["SurveyId"] = (int)this.GridViewCatalogue.SelectedDataKey["SurveyId"];
                Response.Redirect("./Enrol2.aspx", false); 
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    ExceptionData.WriteException( 1, ex.Message, this.ToString(), "Enrol", "");
                    throw new Exception(this.ToString() + " : " + "Enrol" + " : ", ex);
                }
                else
                {
                    throw ex; // Just bubble it up
                }
            }
        }

        protected void GridViewCurrent_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Ensure that the user is logged in.

            if (Session["CustomerId"] == null)
            {
                Response.Redirect("./Login2.aspx", false);
            }
            else
            {
                Response.Redirect("./TestForm2.aspx", true);
            }
        }


        protected void GridViewCatalogue_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Ensure that the user is logged in.

            if (Session["CustomerId"] == null)
            {
                //Session["NextPage"] = "Enrol";
                Response.Redirect("./Login2.aspx", false);
            }
            else
            {
                // Ensure that a publication has been selected

                int lSelectedIndex = this.GridViewCatalogue.SelectedIndex;

                if (lSelectedIndex < 0)
                {
                    LabelResponse.Text = "Please select a publication first";
                    return;
                }

                if (this.GridViewCatalogue.SelectedDataKey["Facility"].ToString() == "Read only")
                {
                    Read();
                }
                else
                {
                    Enrol();
                }
            }
        }

        private void History()
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

    }
}
