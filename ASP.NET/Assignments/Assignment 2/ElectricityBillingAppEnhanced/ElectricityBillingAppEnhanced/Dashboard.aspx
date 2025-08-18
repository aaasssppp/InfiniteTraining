<%@ Page Title="Dashboard" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="ElectricityBillingAppEnhanced.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="text-center">
        <h3>Welcome, Admin</h3>
        <p>Select an option below:</p>

        <div class="d-grid gap-3 col-4 mx-auto">
            <asp:Button ID="btnAdd" runat="server" Text="Add Bills" CssClass="btn btn-success btn-lg" OnClick="btnAdd_Click" />
            <asp:Button ID="btnView" runat="server" Text="View Last N Bills" CssClass="btn btn-info btn-lg" OnClick="btnView_Click" />
        </div>
    </div>
</asp:Content>
