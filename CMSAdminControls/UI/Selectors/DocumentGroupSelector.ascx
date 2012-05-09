<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSAdminControls_UI_Selectors_DocumentGroupSelector" CodeFile="DocumentGroupSelector.ascx.cs" %>
<cms:CMSTextBox ID="txtGroups" runat="server" EnableViewState="false" ReadOnly="true"
    CssClass="SelectorTextBox" /><cms:LocalizedButton ID="btnChange" runat="server" ResourceString="general.change"
    CssClass="ContentButton" />
<asp:Literal runat="server" ID="ltlScript" EnableViewState="false" />
<cms:CMSButton runat="server" ID="btnHidden" CssClass="HiddenButton" OnClick="btnHidden_Click" />
