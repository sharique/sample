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
        <br />
        <br />
        <asp:Button ID="SaveButton" runat="Server" Text="Save and Install" OnClick="SaveButtonClick" CssClass="ui-button"></asp:Button>
    </div>
</asp:Content>
