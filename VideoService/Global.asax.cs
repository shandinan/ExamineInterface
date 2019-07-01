using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace VideoService
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            if (Request.Url.AbsolutePath.ToLower().Contains("videoservice.asmx"))
            {
                //byte[] byts = new byte[Request.InputStream.Length];
                //Request.InputStream.Read(byts, 0, byts.Length);
                //string req = System.Text.Encoding.Default.GetString(byts);
                //Response.Redirect("VideoXml.aspx?xmldata=" + req);
                Server.Transfer("VideoXml.aspx");
            }
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}