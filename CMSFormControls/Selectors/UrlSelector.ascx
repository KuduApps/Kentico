<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UrlSelector.ascx.cs" Inherits="CMSFormControls_Selectors_UrlSelector" %>
<cms:CMSUpdatePanel ID="pnlUpdate" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <cms:CMSTextBox ID="txtPath" runat="server" MaxLength="200" CssClass="SelectorTextBox" /><cms:CMSButton
            ID="btnSelectPath" runat="server" CssClass="ContentButton" EnableViewState="false" />
    </ContentTemplate>    
</cms:CMSUpdatePanel>
