using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LH.Reminder2.Client.Data;

namespace LH.Reminder2.Client.Controls
{
    public partial class TasksView : ListView
    {
        private TaskList data;

        public TaskList Data
        {
            get { return data; }
            set
            {
                data = value;
                DataChanged();
            }
        }

        public TasksView() : base()
        {
            View = View.Details;
            Columns.Add("Message", 400);
            Columns.Add("DateTime", 150);
            Columns.Add("Checked", 80);
            FullRowSelect = true;
            VirtualMode = true;
        }

        protected override void OnRetrieveVirtualItem(RetrieveVirtualItemEventArgs e)
        {
            if (data != null)
                if (e.ItemIndex >= 0 && e.ItemIndex < data.Count)
                    e.Item = data[e.ItemIndex].ToListViewItem();
        }

        public virtual void DataChanged()
        {
            VirtualListSize = data != null ? data.Count : 0;
        }
    }
}
