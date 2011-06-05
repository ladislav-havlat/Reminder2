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
                        where t.idTask == TaskID &&
                              t.User.UserName == context.User.Identity.Name
                        select t;

            if (query.Count() > 0)
                try
                {
                    query.First().Deleted = true;
                    dataCtx.SubmitChanges();
                    WriteOutputStatus(CommonStatusCode.OK, "OK.");
                }
                catch (Exception ex)
                {
                    WriteOutputStatus(CommonStatusCode.ServerError, string.Format("Couldn't delete task. {0}", ex.Message));
                }
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
