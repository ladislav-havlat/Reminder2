using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Xml;
using System.Xml.Linq;
using LH.Reminder2.Client.Data;

namespace LH.Reminder2.Client
{
    public partial class MainForm : Form
    {
        TaskList localTasks;

        public MainForm()
        {
            InitializeComponent();
            localTasks = new TaskList();
        }

        private void SendRequest(string handler, AsyncCallback callback)
        {
            Uri uri = new Uri(new Uri(serverBaseTextBox.Text), handler);

            CredentialCache credentials = new CredentialCache();
            credentials.Add(uri, "Basic", new NetworkCredential(userNameTextBox.Text, passwordTextBox.Text));

            WebRequest request = HttpWebRequest.Create(uri);
            request.Credentials = credentials;
            request.BeginGetResponse(callback, request);
        }

        private void getTasksButton_Click(object sender, EventArgs e)
        {
            tasksListBox.BackColor = SystemColors.ButtonShadow;
            SendRequest("/GetTasks.ashx", GetTasksCallback);
        }

        private void GetTasksCallback(IAsyncResult ar)
        {
            WebRequest request = ar.AsyncState as WebRequest;
            if (request != null)
            {
                WebResponse response = request.EndGetResponse(ar);
                if (response != null)
                    Invoke(new MethodInvoker(delegate() {
                        localTasks.Clear();
                        localTasks.ReadXmlStream(response.GetResponseStream());
                        tasksListBox.VirtualListSize = localTasks.Count;
                        tasksListBox.BackColor = SystemColors.Window;
                    }));
            }
        }

        private void tasksListBox_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            if (e.ItemIndex >= 0 && e.ItemIndex < localTasks.Count)
                e.Item = localTasks[e.ItemIndex].ToListViewItem();
        }

        private void checkTaskMenuItem_Click(object sender, EventArgs e)
        {
            Task task = null;
            if (tasksListBox.SelectedIndices.Count > 0)
                task = localTasks[tasksListBox.SelectedIndices[0]];

            if (task != null)
            {
                string queryString = string.Format("id={0}", task.Id);
                SendRequest(string.Format("/CheckTask.ashx?{0}", queryString), GenericStatusCallback);
            }
        }

        private void deleteTaskMenuItem_Click(object sender, EventArgs e)
        {
            Task task = null;
            if (tasksListBox.SelectedIndices.Count > 0)
                task = localTasks[tasksListBox.SelectedIndices[0]];

            if (task != null)
            {
                string queryString = string.Format("id={0}", task.Id);
                SendRequest(string.Format("/DeleteTask.ashx?{0}", queryString), GenericStatusCallback);
            }
        }

        private void uncheckTaskMenuItem_Click(object sender, EventArgs e)
        {
            Task task = null;
            if (tasksListBox.SelectedIndices.Count > 0)
                task = localTasks[tasksListBox.SelectedIndices[0]];

            if (task != null)
            {
                string queryString = string.Format("id={0}", task.Id);
                SendRequest(string.Format("/UncheckTask.ashx?{0}", queryString), GenericStatusCallback);
            }
        }

        private void GenericStatusCallback(IAsyncResult ar)
        {
            WebRequest request = ar.AsyncState as WebRequest;
            if (request != null)
            {
                WebResponse response = request.EndGetResponse(ar);
                if (response != null)
                {
                    XmlDocument responseXml = new XmlDocument();
                    responseXml.Load(new XmlTextReader(response.GetResponseStream()));

                    XmlNode statusCodeAttr = responseXml.SelectSingleNode("/reminder/status/@code");
                    int statusCode = -1;
                    if (statusCodeAttr != null)
                        int.TryParse(statusCodeAttr.Value, out statusCode);

                    XmlNode statusMessageAttr = responseXml.SelectSingleNode("/reminder/status");
                    string statusMessage;
                    if (statusMessageAttr != null)
                        statusMessage = statusMessageAttr.InnerText;
                    else
                        statusMessage = string.Empty;

                    Invoke(new MethodInvoker(delegate(){
                        MessageBox.Show(string.Format("{0} {1}", statusCode, statusMessage));
                        getTasksButton_Click(this, new EventArgs());
                    }));
                }
            }
        }
    }
}
