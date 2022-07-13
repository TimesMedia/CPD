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
    public partial class Login2 : System.Web.UI.Page
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {
            // Cleanup previous responses. 
            LabelResponse.Text = "";

            LabelVersion.Text = "";
        }

        protected void ButtonLogin_Click(object sender, EventArgs e)
        {
            try
            {
                int lCustomerId = 0;
                if (!Int32.TryParse(TextCustomerId.Text, out lCustomerId))
                {
                    LabelResponse.Text = "Sorry, the CustomerId has to be a numeric value. Please contact MIMS at 011 280 5856";
                    return;
                }

                var lContext = new MimsDataContext(Settings.MIMSConnectionString);  // This is the live CPD database.

                var lCustomerInfoQuery = from lValues in lContext.MIMS_DataContext_CustomerInfo(lCustomerId)
                                         select lValues;

                List<MIMS_DataContext_CustomerInfoResult> lList = lCustomerInfoQuery.ToList<MIMS_DataContext_CustomerInfoResult>();

                MIMS_DataContext_CustomerInfoResult lCustomerInfo;
                
                if (lList.Count != 1)
                {
                    ExceptionData.WriteException(5, "There is no CustomerId that corresponds to that number", this.ToString(), "ButtonLogin_Click", TextCustomerId.Text);
                    LabelResponse.Text = "Sorry, there is no CustomerId that corresponds to that number. Please contact MIMS at 011 280 5856";
                    return;
                }
                else
                {
                    lCustomerInfo = lList[0];
                }

                if (lCustomerInfo.EmailAddress == "NoEmailAddress" )
                {
                    LabelResponse.Text = "Sorry, MIMS does not have an EmailAddress for you. Please contact MIMS at 011 280 5856";
                    return;
                }

                if (lCustomerInfo.EmailAddress.ToLower() != this.TextEMail.Text.ToLower())
                {
                    ExceptionData.WriteException(5, "There is no Email Address that corresponds to your entry", this.ToString(), "ButtonLogin_Click", this.TextEMail.Text);
                    LabelResponse.Text = "Sorry, MIMS has a different EmailAddress for you. Please contact MIMS at 011 280 5856";
                    return;
                }


                if (lCustomerInfo.CouncilNumber == "Missing")
                {
                    ExceptionData.WriteException(5, "There is no CouncilNumber to put on your certificate.", this.ToString(), "ButtonLogin_Click", this.TextEMail.Text);
                    LabelResponse.Text = "We do not have your HPCSA number, please contact Riëtte van der Merwe at 011 280 5856 or vandermerwer@mims.co.za with your number to grant you access.";
                    return;
                }

                //  If OK, save the CustomerId in a Session variable

                Session.Add("CustomerId", TextCustomerId.Text.ToString());
                Session.Add("Customer", lCustomerInfo.FullName);


                Response.Redirect("~/Default.aspx", false);


            }
            catch (Exception ex)
            {
                //Display all the exceptions

                Exception CurrentException = ex;
                int ExceptionLevel = 0;
                do
                {
                    ExceptionLevel++;
                    ExceptionData.WriteException(1, ExceptionLevel.ToString() + " " + CurrentException.Message, this.ToString(), "ButtonLogin_Click", "");
                    CurrentException = CurrentException.InnerException;
                } while (CurrentException != null);

                throw ex; // So, the original level exception is propagated. 
            }
        }


        protected void ButtonHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }
    }
}