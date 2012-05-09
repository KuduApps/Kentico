<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_Content_FormControls_Documents_SelectDocument"
    CodeFile="SelectDocument.ascx.cs" %>
<%@ Register TagPrefix="cms" Namespace="CMS.UIControls" Assembly="CMS.UIControls" %>
<cms:CMSUpdatePanel ID="pnlUpdateHidden" runat="server">
    <ContentTemplate>
        <cms:CMSTextBox ID="txtName" runat="server" MaxLength="800" CssClass="SelectorTextBox" /><cms:CMSButton
            ID="btnSelect" runat="server" CssClass="ContentButton" />
        <cms:CMSButton ID="btnClear" runat="server" CssClass="ContentButton" />
        <cms:CMSTextBox ID="txtGuid" runat="server" CssClass="Hidden" AutoPostBack="true" />
        <cms:LocalizedLabel ID="lblGuid" runat="server" EnableViewState="false" Display="false" AssociatedControlID="txtGuid" ResourceString="development_formusercontrol_edit.lblforguid" />
    </ContentTemplate>
</cms:CMSUpdatePanel>
