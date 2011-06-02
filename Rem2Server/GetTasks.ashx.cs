using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Linq;

namespace Rem2Server
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

            respWriter.WriteStartDocument(true);
            respWriter.WriteStartElement("reminder");
            respWriter.WriteEndElement();
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
