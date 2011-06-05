using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Xml.Linq;

namespace LH.Reminder2.Client.Data
{
    public class Task
    {
        private int id;
        private string message;
        private DateTime dateTime;
        private bool isChecked;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Message
        {
            get { return message; }
            set { message = value; }
        }

        public DateTime DateTime
        {
            get { return dateTime; }
            set { dateTime = value; }
        }

        public bool IsChecked
        {
            get { return isChecked; }
            set { isChecked = value; }
        }
    }

    public class TaskList : List<Task>
    {
        public void ReadXmlStream(Stream stream)
        {
            XElement root = XElement.Load(new XmlTextReader(stream));
            var query = from XElement el in root.Element("tasks").Elements("task")

                        where el.Attribute("id") != null &&
                              el.Element("message") != null &&
                              el.Element("datetime") != null &&
                              el.Element("checked") != null

                        let id = Utils.ParseIntNullable(el.Attribute("id").Value)
                        let dateTime = Utils.ParseDateTimeNullable(el.Element("datetime").Value)
                        let isChecked = Utils.ParseBoolNullable(el.Element("checked").Value)
                        where id.HasValue && dateTime.HasValue

                        select new Task()
                        {
                            Id = id.Value,
                            Message = el.Element("message").Value,
                            DateTime = dateTime.Value,
                            IsChecked = isChecked.Value
                        };
            AddRange(query);
        }
    }

    public static class Utils
    {
        public static int? ParseIntNullable(string str)
        {
            int result;
            if (int.TryParse(str, out result))
                return result;
            else
                return null;
        }

        public static DateTime? ParseDateTimeNullable(string str)
        {
            DateTime result;
            if (DateTime.TryParse(str, out result))
                return result;
            else
                return null;
        }

        public static bool? ParseBoolNullable(string str)
        {
            bool result;
            if (bool.TryParse(str, out result))
                return result;
            else
                return null;
        }
    }
}
