<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddBill.aspx.cs" Inherits="ElectricityBillingApp.AddBill" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add Electricity Bill</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Add Electricity Bill</h2>
            <asp:Label runat="server" Text="Consumer Number:" />
            <asp:TextBox ID="txtConsumerNo" runat="server" />
            <br />
            <asp:Label runat="server" Text="Consumer Name:" />
            <asp:TextBox ID="txtConsumerName" runat="server" />
            <br />
            <asp:Label runat="server" Text="Units Consumed:" />
            <asp:TextBox ID="txtUnits" runat="server" />
            <br />
            <asp:Button ID="btnAdd" runat="server" Text="Add Bill" OnClick="btnAdd_Click" />
            <br />
            <asp:Label ID="lblResult" runat="server" ForeColor="Green" />
        </div>
    </form>
</body>
</html>