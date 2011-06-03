using System;
using System.Collections;
using System.Collections.Generic;
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
        public enum TasksRequestType
        {
            Recent,
            Unchecked,
        };

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/xml";
            XmlTextWriter respWriter = new XmlTextWriter(context.Response.Output);
            Reminder2DataContext ctx = new Reminder2DataContext();
            respWriter.Formatting = Formatting.Indented;

            respWriter.WriteStartDocument(true);
            respWriter.WriteStartElement("reminder");

            IQueryable<Task> tasks = null;
            DateTime utcNow = DateTime.UtcNow;
            switch (RequestType)
            {
                case TasksRequestType.Recent:
                    //select tasks that are either less than 2 days old or not checked
                    tasks = from Task t in ctx.Tasks
                            where t.User.UserName == context.User.Identity.Name &&
                                  (utcNow - t.DateTime < new TimeSpan(2, 0, 0, 0) || !t.Checked)
                            select t;
                    break;

                case TasksRequestType.Unchecked:
                    //select all unchecked tasks
                    tasks = from Task t in ctx.Tasks
                            where t.User.UserName == context.User.Identity.Name &&
                                  !t.Checked
                            select t;
                    break;
            }

            if (tasks != null)
            {
                respWriter.WriteStartElement("tasks");
                OutputTasks(respWriter, tasks);
                respWriter.WriteEndElement();
            }

            respWriter.WriteEndDocument();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        protected TasksRequestType RequestType
        {
            get
            { 
                HttpContext context = HttpContext.Current;
                string reqTypeString = context.Request.QueryString["type"];

                if (reqTypeString == null)
                    return TasksRequestType.Recent; //default
                else if (reqTypeString.Equals("recent", StringComparison.OrdinalIgnoreCase))
                    return TasksRequestType.Recent;
                else if (reqTypeString.Equals("unchecked", StringComparison.OrdinalIgnoreCase))
                    return TasksRequestType.Unchecked;
                else
                    return TasksRequestType.Recent; //default
            }
        }

        private void OutputTasks(XmlWriter writer, IEnumerable<Task> tasks)
        {
            foreach (Task t in tasks)
            {
                writer.WriteStartElement("task");
                writer.WriteAttributeString("id", t.idTask.ToString());
                writer.WriteElementString("message", t.Message);
                writer.WriteElementString("dateTime", t.DateTime.ToString("yyyy-MM-dd HH:mm:ss"));
                writer.WriteElementString("checked", t.Checked.ToString());
                writer.WriteEndElement();
            }
        }
    }
}
