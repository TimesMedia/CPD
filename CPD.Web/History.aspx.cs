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
    public partial class History : System.Web.UI.Page
    {
        ResultDoc.HistoryDataTable gCurrent = new ResultDoc.HistoryDataTable();
        ResultDoc.HistoryDataTable gHistory = new ResultDoc.HistoryDataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            string lStage = "Current"; 
            try
            {

                ResultData.GetCurrentTest(Int32.Parse(Session["CustomerId"].ToString()), ref gCurrent);

                GridViewCurrent.DataSource = gCurrent;
                GridViewCurrent.DataBind();

                lStage = "History";

                gHistory = ResultData.GetHistory(Int32.Parse(Session["CustomerId"].ToString()));
                GridViewHistory.DataSource = gHistory;
                GridViewHistory.DataBind();

                if (GridViewCurrent.Rows.Count == 0)
                {
                    LabelCurrent.Visible = false;
                }
                else
                {
                    LabelCurrent.Visible = true;
                }

                if (GridViewHistory.Rows.Count == 0)
                {
                    LabelHistory.Visible = false;
                }
                else
                {
                    LabelHistory.Visible = true;
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
                        "Customer: " + Session["CustomerId"].ToString() + "Stage = " + lStage);
                    CurrentException = CurrentException.InnerException;
                } while (CurrentException != null);
                throw ex;
            }

        }

        protected void ButtonHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }

        protected void ButtonReissue_Click(object sender, EventArgs e)
        {
            try
            {
                // Take precautions in case the session has expired. 
                if (Session["CustomerId"] == null)
                {
                    Session["NextPage"] = "Enrol";
                    Response.Redirect("./Login2.aspx", false);
                }

                // Ensure that a module has been selected

                if (this.GridViewHistory.SelectedIndex < 0)
                {
                    LabelResponse.Text = "Please select a test that you have passed first";
                    return;
                }

                int lResultId = (int)this.GridViewHistory.SelectedValue;
                ResultDoc.HistoryDataTable lResult = ResultData.GetByResultId(lResultId);

                if (lResult[0].Verdict == "Failed")
                {
                    LabelResponse.Text = "According to my records you did not pass this test. If you think you have a case, please contact MIMS at 011 280 5533";
                    return;
                }
                else
                {
                    Thread lWorkerThread = new Thread(ProcessCertificate);
                    lWorkerThread.SetApartmentState(ApartmentState.STA);
                    object pState = lResultId;  // Box it
                    lWorkerThread.Start(pState);
                    lWorkerThread.Join();
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
                    ExceptionData.WriteException(1, ExceptionLevel.ToString() + " " + CurrentException.Message, this.ToString(), "ButtonReissue_Click", "");
                    CurrentException = CurrentException.InnerException;
                } while (CurrentException != null);
                throw ex;
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
                        LabelResponse.Text = "Error rendering certificate " + lResult;
                    }
                }

                {
                    string lResult;
                    if ((lResult = lCertificate.EmailCertificate((int)pState)) != "OK")
                    {
                        LabelResponse.Text = "Error emailing certificate " + lResult;
                        return;
                    }
                }
                LabelResponse.Text = "Certificate sucessfully reissued.";
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
    }
}