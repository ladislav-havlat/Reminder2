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
using System.Xml;
using System.Collections.Generic;
using Data = LH.Reminder2.Server.Data;

namespace LH.Reminder2.Server
{
    public enum CommonStatusCode
    {
        OK = 200,
        BadRequest = 400,
        Forbidden = 401,
        NotFound = 404,
        ServerError = 500
    }

    public abstract class GenericHandler : IHttpHandler
    {
        private XmlTextWriter xmlOutput;

        #region Properties and events
        /// <summary>
        /// XML writer connected to the output stream. Use for any XML output.
        /// </summary>
        protected XmlTextWriter XmlOutput
        {
            get { return xmlOutput; }
        }

        /// <summary>
        /// Determines whether this instance may be reused for another request.
        /// </summary>
        public bool IsReusable
        {
            get { return false; }
        }
        #endregion

        #region IHttpHandler implementation
        /// <summary>
        /// Processes a HTTP request.
        /// </summary>
        /// <param name="context">HTTP request context.</param>
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/xml";
            xmlOutput = new XmlTextWriter(context.Response.Output);
            xmlOutput.WriteStartDocument(true);

            #if DEBUG
            xmlOutput.Formatting = Formatting.Indented;
            #endif

            try
            {
                xmlOutput.WriteStartElement("reminder");
                xmlOutput.WriteAttributeString("version", "1.0");

                if (CheckParams(context))
                    try
                    {
                        InternalProcessRequest(context);
                    }
                    catch (Exception ex)
                    {
                        WriteOutputStatus(CommonStatusCode.ServerError, string.Format("Unknown server error. {0}", ex.Message));
                    }
                else
                    WriteOutputStatus(CommonStatusCode.BadRequest, "One or more of the required parameters is missing or invalid.");
            }
            finally
            {
                xmlOutput.WriteEndDocument();
            }
        }
        #endregion

        #region Processing stubs
        /// <summary>
        /// Placeholder for main processing method.
        /// </summary>
        /// <param name="context">HTTP request context.</param>
        protected abstract void InternalProcessRequest(HttpContext context);

        /// <summary>
        /// Placeholder for input parameters check method.
        /// </summary>
        /// <param name="context">HTTP request context.</param>
        /// <returns>True if all required parameters are present, false otherwise.</returns>
        protected virtual bool CheckParams(HttpContext context)
        {
            return true; //no parameters are required by default
        }
        #endregion

        #region Common XML output utilities
        /// <summary>
        /// Writes a status message to the output stream.
        /// </summary>
        /// <param name="code">Status code.</param>
        /// <param name="status">Status message.</param>
        protected void WriteOutputStatus(int code, string status)
        {
            xmlOutput.WriteStartElement("status");
            try
            {
                xmlOutput.WriteAttributeString("code", code.ToString());
                xmlOutput.WriteValue(status);
            }
            finally
            {
                xmlOutput.WriteEndElement();
            }
        }

        /// <summary>
        /// Writes a status message to the output stream.
        /// </summary>
        /// <param name="code">Status code.</param>
        /// <param name="status">Status message.</param>
        protected void WriteOutputStatus(CommonStatusCode code, string status)
        {
            WriteOutputStatus((int)code, status);
        }

        /// <summary>
        /// Writes a single task element to the output stream.
        /// </summary>
        /// <param name="task">The Task object to be written.</param>
        protected void WriteTask(Data.Task task)
        {
            try
            {
                xmlOutput.WriteStartElement("task");
                xmlOutput.WriteAttributeString("id", task.idTask.ToString());
                xmlOutput.WriteElementString("message", task.Message);
                xmlOutput.WriteElementString("datetime", task.DateTime.ToString("yyyy-MM-dd HH:mm:ss"));
                xmlOutput.WriteElementString("checked", task.Checked.ToString());
            }
            finally
            {
                xmlOutput.WriteEndElement();
            }
        }

        /// <summary>
        /// Writes a collection of task elements to the output stream.
        /// </summary>
        /// <param name="tasks">The collection of Task objects to be written.</param>
        protected void WriteTasks(IEnumerable<Data.Task> tasks)
        {
            try
            {
                xmlOutput.WriteStartElement("tasks");
                foreach (Data.Task task in tasks)
                    WriteTask(task);
            }
            finally
            {
                xmlOutput.WriteEndElement();
            }
        }
        #endregion
    }
}
