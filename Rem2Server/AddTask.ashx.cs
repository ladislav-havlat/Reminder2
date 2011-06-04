using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Xml;
using System.Text;
using LH.Reminder2.Server.Data;

namespace LH.Reminder2.Server
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class AddTask : IHttpHandler
    {
        private XmlTextWriter outputWriter;

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/xml";
            outputWriter = new XmlTextWriter(context.Response.Output);
            Reminder2DataContext dataCtx = new Reminder2DataContext();

            outputWriter.Formatting = Formatting.Indented;

            try
            {
                outputWriter.WriteStartDocument(true);
                outputWriter.WriteStartElement("reminder");
                if (CheckParams())
                {
                    var query = from User u in dataCtx.Users
                                where u.UserName == context.User.Identity.Name
                                select u.idUser;
                    if (query.Count() > 0)
                    {
                        try
                        {
                            Task t = new Task()
                            {
                                Message = this.Message,
                                DateTime = this.DateTime,
                                idUser = query.First()
                            };
                            dataCtx.Tasks.InsertOnSubmit(t);
                            dataCtx.SubmitChanges();
                            WriteOutputStatus(200, "OK.");

                            outputWriter.WriteStartElement("task");
                            try
                            {
                                outputWriter.WriteElementString("id", t.idTask.ToString());
                            }
                            finally
                            {
                                outputWriter.WriteEndElement();
                            }
                        }
                        catch (Exception ex)
                        {
                            WriteOutputStatus(500, string.Format("Couldn't add task. {0}", ex.Message));
                        }
                    }
                    else
                        WriteOutputStatus(500, "Invalid user Id.");
                }
                else
                    WriteOutputStatus(400, "One or more of the required parameters is missing.");
            }
            finally
            {
                outputWriter.WriteEndDocument();
                outputWriter.Flush();
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        protected bool CheckParams()
        {
            HttpContext ctx = HttpContext.Current;
            return ctx.Request.Form["message"] != null &&
                   ctx.Request.Form["dateTime"] != null;
        }

        protected string Message
        {
            get { return HttpContext.Current.Request.Form["message"]; }
        }

        protected DateTime DateTime
        {
            get
            {
                DateTime value;
                if (DateTime.TryParse(HttpContext.Current.Request.Form["dateTime"], out value))
                    return value;
                else
                    return default(DateTime);
            }
        }

        private void WriteOutputStatus(int code, string status)
        {
            outputWriter.WriteStartElement("status");
            try
            {
                outputWriter.WriteAttributeString("code", code.ToString());
                outputWriter.WriteValue(status);
            }
            finally
            {
                outputWriter.WriteEndElement();
            }
        }
    }
}
