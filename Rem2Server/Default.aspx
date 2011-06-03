<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="LH.Reminder2.Server._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Reminder2 server</title>
</head>
<body>
    <form id="form1" runat="server">
<h1>Reminder 2 server</h1>
<p>This is a Reminder2 server.</p>

<h2>Quick links:</h2>
<p>
    <a href="GetTasks.ashx">GetTasks.ashx</a>
</p>

<h2>Create new user</h2>
<table>
    <tr>
        <td>User name</td>
        <td><asp:TextBox ID="userNameTextBox" runat="server"></asp:TextBox></td>
    </tr>
    <tr>
        <td>E-mail</td>
        <td><asp:TextBox ID="emailTextBox" runat="server"></asp:TextBox></td>
    </tr>
    <tr>
        <td>First name</td>
        <td><asp:TextBox ID="firstNameTextBox" runat="server"></asp:TextBox></td>
    </tr>
    <tr>
        <td>Last name</td>
        <td><asp:TextBox ID="lastNameTextBox" runat="server"></asp:TextBox></td>
    </tr>
    <tr>
        <td>Password</td>
        <td><asp:TextBox ID="passwordTextBox" runat="server"></asp:TextBox></td>
    </tr>
    <tr><td colspan="2">
        <asp:Button ID="createUserButton" Text="Create user" Width="100%" 
            runat="server" onclick="createUserButton_Click" />
    </td></tr>
    <tr><td colspan="2">
        <asp:Label ID="statusLabel" Text=" " runat="server" />
    </td></tr>
</table>

</form>

</body>
</html>
