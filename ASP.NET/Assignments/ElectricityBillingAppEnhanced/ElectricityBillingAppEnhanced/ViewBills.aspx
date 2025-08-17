<%@ Page Title="View Bills" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewBills.aspx.cs" Inherits="ElectricityBillingAppEnhanced.ViewBills" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="col-md-6 mx-auto">
        <h3 class="text-center">Retrieve Last N Bills</h3>
        <div class="mb-3">
            <asp:Label Text="Enter N:" runat="server" />
            <asp:TextBox ID="txtN" runat="server" CssClass="form-control" />
        </div>
        <asp:Button ID="btnGet" runat="server" Text="Get Bills" CssClass="btn btn-info" OnClick="btnGet_Click" />
        <br /><br />
        <asp:GridView ID="gvBills" runat="server" CssClass="table table-bordered table-striped"
            AutoGenerateColumns="false">
            <Columns>
                <asp:BoundField DataField="ConsumerNumber" HeaderText="Consumer Number" />
                <asp:BoundField DataField="ConsumerName" HeaderText="Consumer Name" />
                <asp:BoundField DataField="UnitsConsumed" HeaderText="Units Consumed" />
                <asp:BoundField DataField="BillAmount" HeaderText="Bill Amount" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
