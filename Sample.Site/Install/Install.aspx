<%@ Page Title="Site installation" Language="C#" MasterPageFile="~/Install/Install.Master" AutoEventWireup="true" CodeBehind="Install.aspx.cs" Inherits="Sample.Site.Install.Install1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div>
	<h1>Site Installtion </h1>
	<asp:Label id="lblMsg" runat="server"></asp:Label>
	<br/>
	<label> Sitename</label>
	<asp:TextBox id="tbSitename" runat="Server"></asp:TextBox>
	<br/>
	<asp:Button ID="SaveButton" runat="Server" Text="Save and Install" OnClick="SaveButtonClick"> </asp:Button>
</div>
</asp:Content>
