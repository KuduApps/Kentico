<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_Content_FormControls_Documents_SelectPath"
    CodeFile="SelectPath.ascx.cs" %>
<cms:CMSUpdatePanel ID="pnlUpdate" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <cms:CMSTextBox ID="txtPath" runat="server" MaxLength="200" CssClass="SelectorTextBox" /><cms:CMSButton
            ID="btnSelectPath" runat="server" CssClass="ContentButton" EnableViewState="false" />
    </ContentTemplate>    
</cms:CMSUpdatePanel>
