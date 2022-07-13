using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CPD.Web
{
    public partial class References : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Request.QueryString["page"] != null)
            //{
            //    switch (Convert.ToInt32(Request.QueryString["page"]))
            //    {
            //        case 1:
            //            optimag.Style.Add("display", "block");
            //            break;
            //        case 2:
            //            womanshealth.Style.Add("display", "block");
            //            break;
            //        case 3:
            //            diseases.Style.Add("display", "block");
            //            break;
            //        default:
            //            optimag.Style.Add("display", "block");
            //            break;
            //    }
            //}
            //else
            //{
            //   optimag.Attributes.Add("display", "block");
               
            //}
        }

        protected void btnHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }
    }
}