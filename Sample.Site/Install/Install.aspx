<%@ Page Title="" Language="C#" MasterPageFile="~/Install/Install.Master" AutoEventWireup="true" CodeBehind="Install.aspx.cs" Inherits="Sample.Site.Install.Install1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <h1>Site Installtion </h1>
        <asp:Label ID="lblMsg" runat="server"></asp:Label>
        <br />
        <label>Sitename</label>
        <asp:TextBox ID="tbSitename" runat="Server" CssClass="ui-state-disabled"></asp:TextBox>
        <label>Site Slogan</label>
        <asp:TextBox ID="tbSiteSlogan" runat="Server" CssClass="ui-state-disabled"></asp:TextBox>
        <br />
        <label>Admin User name</label>
        <asp:TextBox ID="tbUname" runat="Server" CssClass="ui-state-disabled"></asp:TextBox>
        <label>Admin password</label>
        <asp:TextBox ID="tbPass" runat="Server" CssClass="ui-state-disabled" TextMode="Password"></asp:TextBox>
        <label>Admin Email</label>
        <asp:TextBox ID="tbEmail" runat="Server" CssClass="ui-state-disabled" TextMode="Email"></asp:TextBox>
        <br />
        <asp:Button ID="SaveButton" runat="Server" Text="Save and Install" OnClick="SaveButtonClick" CssClass="ui-button"></asp:Button>
    </div>
</asp:Content>
