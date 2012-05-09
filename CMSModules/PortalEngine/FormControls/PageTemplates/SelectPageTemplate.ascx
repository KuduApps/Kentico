<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_PortalEngine_FormControls_PageTemplates_SelectPageTemplate" CodeFile="SelectPageTemplate.ascx.cs" %>

<cms:CMSUpdatePanel ID="pnlUpdate" runat="server">
    <ContentTemplate>    
        <cms:CMSTextBox ID="txtTemplate" runat="server" MaxLength="200" ReadOnly="true" CssClass="SelectorTextBox" /><cms:CMSButton ID="btnSelect" runat="server" CssClass="ContentButton" EnableViewState="false" /><cms:CMSButton ID="btnClear" runat="server" CssClass="ContentButton" EnableViewState="false" RenderScript="true" />
        <asp:HiddenField ID="hdnTemplateCode" runat="server" />
        <asp:Literal ID="ltlScript" runat="server" EnableViewState="false" />
    </ContentTemplate>
</cms:CMSUpdatePanel>