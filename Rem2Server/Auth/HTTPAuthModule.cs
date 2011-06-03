using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Text;
using System.Security.Principal;

namespace LH.Reminder2.Server.Auth
{
    public class HTTPAuthModule : IHttpModule
    {
        public void Dispose()
        {
            //nothing to be disposed
        }

        public void Init(HttpApplication context)
        {
            context.AuthenticateRequest += new EventHandler(context_AuthenticateRequest);
            context.EndRequest += new EventHandler(context_EndRequest);
        }

        private void context_AuthenticateRequest(object sender, EventArgs e)
        {
            HttpContext context = HttpContext.Current;
            if (!AuthenticateUser())
            {
                context.Response.StatusCode = 401;
                context.Response.End();
            }
        }

        private void context_EndRequest(object sender, EventArgs e)
        {
            HttpContext context = HttpContext.Current;
            if (context.Response.StatusCode == 401)
            {
                context.Response.AddHeader(
                    "WWW-Authenticate",
                    "Basic realm=\"Reminder\"");
            }
        }

        /// <summary>
        /// Extracts user credentials from Authentication header and verifies them against the current membership
        /// provider.
        /// </summary>
        /// <returns>True if the user was successfully authenticated, false otherwise.</returns>
        private bool AuthenticateUser()
        {
            HttpContext context = HttpContext.Current;

            try
            {
                string authHeader = context.Request.Headers["Authorization"];
                if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Basic"))
                {
                    string encodedCredentials = authHeader.Substring(6).Trim();
                    string decodedCredentials = context.Request.ContentEncoding.GetString(
                        Convert.FromBase64String(encodedCredentials));
                    string[] credentials = decodedCredentials.Split(':');
                    if (credentials.Length == 2)
                    {
                        string userName = credentials[0];
                        string password = credentials[1];
                        if (Membership.Provider.ValidateUser(userName, password))
                        {
                            context.User = new GenericPrincipal(
                                new GenericIdentity(userName, "HttpBasic"), 
                                new string[] { }
                                );
                            return true;
                        }
                        else
                            return false;
                    }
                    else
                        return false;
                }
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
