using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Auth=LH.Reminder2.Server.Auth;

namespace LH.Reminder2.Server
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void createUserButton_Click(object sender, EventArgs e)
        {
            Auth.Rem2MembershipProvider provider = Membership.Provider as Auth.Rem2MembershipProvider;
            MembershipCreateStatus status;

            provider.CreateUser(userNameTextBox.Text, passwordTextBox.Text, emailTextBox.Text, 
                firstNameTextBox.Text, lastNameTextBox.Text, out status);
            statusLabel.Text = status.ToString();
        }
    }
}
