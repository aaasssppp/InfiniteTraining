<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ElectricityBillingApp.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Admin Login</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Admin Login</h2>
            <asp:Label ID="Label1" runat="server" Text="Username:" />
            <asp:TextBox ID="txtUser" runat="server" />
            <br />
            <asp:Label ID="Label2" runat="server" Text="Password:" />
            <asp:TextBox ID="txtPass" runat="server" TextMode="Password" />
            <br />
            <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" />
            <br />
            <asp:Label ID="lblMsg" runat="server" ForeColor="Red" />
        </div>
    </form>
</body>
</html>