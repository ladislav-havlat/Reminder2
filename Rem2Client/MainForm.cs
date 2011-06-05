using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
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

        private void getTasksButton_Click(object sender, EventArgs e)
        {
            Uri uri = new Uri(new Uri(serverBaseTextBox.Text), "/GetTasks.ashx");

            CredentialCache credentials = new CredentialCache();
            credentials.Add(uri, "Basic", new NetworkCredential(userNameTextBox.Text, passwordTextBox.Text));

            WebRequest request = HttpWebRequest.Create(uri);
            request.Credentials = credentials;
            request.BeginGetResponse(GetTasksCallback, request);
        }

        private void GetTasksCallback(IAsyncResult ar)
        {
            WebRequest request = ar.AsyncState as WebRequest;
            if (request != null)
            {
                WebResponse response = request.EndGetResponse(ar);
                if (response != null)
                    Invoke(new MethodInvoker(delegate() {
                        localTasks.ReadXmlStream(response.GetResponseStream());
                        tasksListBox.VirtualListSize = localTasks.Count;
                    }));
            }
        }

        private void tasksListBox_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            if (e.ItemIndex >= 0 && e.ItemIndex < localTasks.Count)
                e.Item = localTasks[e.ItemIndex].ToListViewItem();
        }
    }
}
