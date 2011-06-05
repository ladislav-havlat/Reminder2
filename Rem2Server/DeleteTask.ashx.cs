using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using LH.Reminder2.Server.Data;

namespace LH.Reminder2.Server
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class DeleteTask : GenericHandler
    {
        protected override void InternalProcessRequest(HttpContext context)
        {
            Reminder2DataContext dataCtx = new Reminder2DataContext();
            var query = from Task t in dataCtx.Tasks
                        where t.idTask == TaskID
                        select new
                        {
                            Task = t,
                            UserName = t.User.UserName
                        };

            var result = query.FirstOrDefault();
            if (result != null)
                try
                {
                    if (result.UserName.Equals(context.User.Identity.Name, StringComparison.OrdinalIgnoreCase))
                    {
                        result.Task.Deleted = true;
                        dataCtx.SubmitChanges();
                        WriteOutputStatus(CommonStatusCode.OK, "OK.");
                    }
                    else
                        WriteOutputStatus(CommonStatusCode.Forbidden, "You are not allowed to delete this task.");
                }
                catch (Exception ex)
                {
                    WriteOutputStatus(CommonStatusCode.ServerError, string.Format("Couldn't delete task. {0}", ex.Message));
                }
            else
                WriteOutputStatus(CommonStatusCode.NotFound, "Task not found.");
        }

        protected override bool CheckParams(HttpContext context)
        {
            string stringId = context.Request.QueryString["id"];
            int dummy;
            return (stringId != null) && int.TryParse(stringId, out dummy);
        }

        protected int TaskID
        {
            get { return int.Parse(HttpContext.Current.Request.QueryString["id"]); }
        }
    }
}
