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
    public class AddTask : GenericHandler
    {
        /// <summary>
        /// Processes the HTTP request.
        /// </summary>
        /// <param name="context">Context of the HTTP operation.</param>
        protected override void InternalProcessRequest(HttpContext context)
        {
            Reminder2DataContext dataCtx = new Reminder2DataContext();
            var query = from User u in dataCtx.Users
                        where u.UserName == context.User.Identity.Name
                        select u.idUser;
            if (query.Count() > 0)
                try
                {
                    Task task = new Task()
                    {
                        Message = this.Message,
                        DateTime = this.DateTime,
                        idUser = query.First()
                    };
                    dataCtx.Tasks.InsertOnSubmit(task);
                    dataCtx.SubmitChanges();
                    WriteOutputStatus(200, "OK.");

                    XmlOutput.WriteStartElement("task");
                    try
                    {
                        XmlOutput.WriteAttributeString("id", task.idTask.ToString());
                    }
                    finally
                    {
                        XmlOutput.WriteEndElement();
                    }
                }
                catch (Exception ex)
                {
                    WriteOutputStatus(CommonStatusCode.ServerError, string.Format("Couldn't add task. {0}", ex.Message));
                }
            else
                WriteOutputStatus(CommonStatusCode.ServerError, "Invalid user Id.");
        }

        /// <summary>
        /// Checks for input POST parameters.
        /// </summary>
        /// <returns>True if all required parameters are present, false otherwise.</returns>
        protected override bool CheckParams(HttpContext ctx)
        {
            return ctx.Request.Form["message"] != null &&
                   ctx.Request.Form["dateTime"] != null;
        }

        #region Input parameters parsing
        /// <summary>
        /// Extracts message ot the task from the input parameters.
        /// </summary>
        protected string Message
        {
            get { return HttpContext.Current.Request.Form["message"]; }
        }

        /// <summary>
        /// Extracts DateTime of the task from the input parameters.
        /// </summary>
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
        #endregion
    }
}
