using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using CPD.Data;
using CPD.Business;

namespace CPD.Web
{
    public class Global : System.Web.HttpApplication
    {

        void Application_Start(object sender, EventArgs e)
        {
              Settings.CPDConnectionString = global::CPD.Web.Properties.Settings.Default.CPDConnectionString;
              Settings.MIMSConnectionString = global::CPD.Web.Properties.Settings.Default.MIMSConnectionString; // Used by MIMS applications.
        }

        void Application_End(object sender, EventArgs e)
        {
        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs

        }

        void Session_Start(object sender, EventArgs e)
        {
            Session.Add("NextPage", "Login");
            Session.Add("ArticleIndex", "0");
            Session.Add("QuestionIndex", "0");
            Session.Timeout = 10; // Ten minutes
         }

        void Session_End(object sender, EventArgs e)
        {
            // Ensure that all your answers are saved.

            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.

        }

    }
}
