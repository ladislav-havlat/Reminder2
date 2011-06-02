using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Linq;
using LH.Reminder2.Server.Data;

namespace LH.Reminder2.Server
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class GetTasks : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/xml";
            XmlTextWriter respWriter = new XmlTextWriter(context.Response.Output);
            respWriter.Formatting = Formatting.Indented;

            respWriter.WriteStartDocument(true);
            respWriter.WriteStartElement("reminder");

            respWriter.WriteStartElement("tasks");
            Reminder2DataContext ctx = new Reminder2DataContext();
            var tasks = from Task t in ctx.Tasks
                        where t.User.UserName == context.User.Identity.Name
                        select t;
            foreach (Task t in tasks)
            {
                respWriter.WriteStartElement("task");
                respWriter.WriteAttributeString("id", t.idTask.ToString());
                respWriter.WriteElementString("message", t.Message);
                respWriter.WriteElementString("dateTime", t.DateTime.ToString("yyyy-MM-dd HH:mm:ss"));
                respWriter.WriteEndElement();
            }
            respWriter.WriteEndElement();

            respWriter.WriteEndDocument();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
