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
    public partial class Begin : System.Web.UI.Page
    {
    protected void Page_Load(object sender, EventArgs e)
    {
           
        string Stage = "Login";
        string LoggedInAs = "";
        int lToken = 0;

        try 
        {

        if (Request.Params["Id"] == null)
        {
            LabelResponse.Text = "This is not a valid token. Please contact MIMS at 011 280 5856";
            return;
        }
        else
        {
            LabelResponse.Text = lToken.ToString();
            lToken = Int32.Parse(Request.Params["Id"]);
            if (lToken % (16 * (DateTime.Now.Hour + 1)) != 0)
            {
                LabelResponse.Text = "This is not a valid token. Please contact MIMS at 011 280 5856";
                return;
            }
        }
        Stage = "Processing Token";


        int lCustomerId = lToken / (16 * (DateTime.Now.Hour + 1));

                var lContext = new MimsDataContext(Settings.MIMSConnectionString);  // This is the live CPD database.

        var lCustomerInfoQuery = from lValues in lContext.MIMS_DataContext_CustomerInfo(lCustomerId)
                                    select lValues;

        List<MIMS_DataContext_CustomerInfoResult> lList = lCustomerInfoQuery.ToList<MIMS_DataContext_CustomerInfoResult>();

        MIMS_DataContext_CustomerInfoResult lCustomerInfo;

        if (lList.Count != 1)
        {
            ExceptionData.WriteException(5, "There is no CustomerId that corresponds to that number", this.ToString(), "ButtonLogin_Click",
                lToken.ToString() + " "
                + lCustomerId.ToString() + " "
                + (DateTime.Now.Hour + 1).ToString());
            LabelResponse.Text = "Sorry, I do not know you. Please contact MIMS at 011 280 5856";
            return;
        }
        else
        {
            lCustomerInfo = lList[0];
        }

        if (lCustomerInfo.EmailAddress == "NoEmailAddress")
        {
            LabelResponse.Text = "Sorry, MIMS does not have an EmailAddress for you. Please contact MIMS at 011 280 5856";
            return;
        }

        if (lCustomerInfo.CouncilNumber == "Missing")
        {
            ExceptionData.WriteException(5, "There is no CouncilNumber to put on your certificate.", this.ToString(), "ButtonLogin_Click", lCustomerId.ToString());
            LabelResponse.Text = "We do not have your HPCSA number, please contact Riëtte van der Merwe at 011 280 5856 or vandermerwer@mims.co.za with your number to grant you access.";
            return;
        }

        Session.Add("CustomerId", lCustomerId);
        Session.Add("Customer", lCustomerInfo.FullName);
        Session.Add("EmailAddress", lCustomerInfo.EmailAddress);
        Session.Add("CouncilNumber", lCustomerInfo.CouncilNumber);

        Response.Redirect("~/Default.aspx", true);

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
    }
}