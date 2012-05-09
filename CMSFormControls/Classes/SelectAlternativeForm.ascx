<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSFormControls_Classes_SelectAlternativeForm" CodeFile="SelectAlternativeForm.ascx.cs" %>
<cms:CMSTextBox ID="txtName" runat="server" MaxLength="200" CssClass="SelectorTextBox" /><cms:CMSButton 
    ID="btnSelect" runat="server" CssClass="ContentButton" />
<asp:Label ID="lblStatus" runat="server" CssClass="SelectorError" EnableViewState="False" />
