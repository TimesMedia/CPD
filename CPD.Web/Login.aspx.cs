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
    public partial class Login : System.Web.UI.Page
    {
        // This version is designed to work in the context of Hein's MIMS shop, with an integrated login procedure.
        // Rather than call Login with a parameter, you should try to run CPD as an applicatin under the new Shop. 
  
        protected void Page_Load(object sender, EventArgs e)
        {
            // Cleanup previous responses. 
            LabelResponse.Text = "";

            LabelVersion.Text = "";

            int lCustomerId = 0;

            if (Request.QueryString.Count == 1)
            {
                lCustomerId = 117224; // Int32.Parse(Server.HtmlEncode(Request.QueryString["CustomerId"]));
            }

            // Get CustomerInfo directly from the MIMS database on the same server.


            var lContext = new MimsDataContext(Settings.MIMSConnectionString);  // This is the live CPD database.

            var lCustomerInfoQuery = from lValues in lContext.MIMS_DataContext_CustomerInfo(lCustomerId)
                                     select lValues;
            MIMS_DataContext_CustomerInfoResult lCustomerInfo = lCustomerInfoQuery.Single();

            if (lCustomerInfo.CouncilNumber.Length < 3)
            {
                ExceptionData.WriteException(5, "There is no CouncilNumber to put on your certificate.", this.ToString(), "ButtonLogin_Click", "");
                LabelResponse.Text = "Sorry, MIMS has no CouncilNumber to put on your certificate. No point in doing any tests. Please contact MIMS at 011 280 5533";
                return;
            }

            //  If OK, save the CustomerId in a Session variable

            Session.Add("CustomerId", lCustomerId.ToString());
            Session.Add("Customer", lCustomerInfo.FullName);

            Response.Redirect("~/Default.aspx", false);
        }

       

        protected void ButtonHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }
    }
}