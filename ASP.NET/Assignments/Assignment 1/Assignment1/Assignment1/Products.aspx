<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Products.aspx.cs" Inherits="Assignment1.Products" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <title>Products</title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="width:400px; margin:auto;">
            <h2>Product Selection</h2>

            <asp:DropDownList ID="ddlProducts" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlProducts_SelectedIndexChanged"></asp:DropDownList><br /><br />

            <asp:Image ID="imgProduct" runat="server" Width="200" Height="200" /><br /><br />

            <asp:Button ID="btnPrice" runat="server" Text="Get Price" OnClick="btnPrice_Click" /><br /><br />

            <asp:Label ID="lblPrice" runat="server" Font-Bold="true"></asp:Label>
        </div>
    </form>
</body>
</html>