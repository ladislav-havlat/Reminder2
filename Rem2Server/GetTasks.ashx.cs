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
    public class GetTasks : GenericHandler
    {
        public enum TasksRequestType
        {
            Recent,
            Unchecked,
        };

        protected override void InternalProcessRequest(HttpContext context)
        {
            Reminder2DataContext ctx = new Reminder2DataContext();
            try
            {
                IQueryable<Task> tasks = null;
                DateTime utcNow = DateTime.UtcNow;
                switch (RequestType)
                {
                    case TasksRequestType.Recent:
                        //select tasks that are either less than 2 days old or not checked
                        tasks = from Task t in ctx.Tasks
                                where t.User.UserName == context.User.Identity.Name &&
                                      !t.Deleted &&
                                      (utcNow - t.DateTime < new TimeSpan(2, 0, 0, 0) || !t.Checked)
                                select t;
                        break;

                    case TasksRequestType.Unchecked:
                        //select all unchecked tasks
                        tasks = from Task t in ctx.Tasks
                                where t.User.UserName == context.User.Identity.Name &&
                                      !t.Deleted && 
                                      !t.Checked
                                select t;
                        break;
                }

                if (tasks != null)
                    WriteTasks(tasks);
            }
            catch (Exception ex)
            {
                WriteOutputStatus(CommonStatusCode.ServerError, string.Format("Could not retrieve tasks. {0}", ex.Message));
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
    }
}
