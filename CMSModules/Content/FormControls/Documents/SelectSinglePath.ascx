<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_Content_FormControls_Documents_SelectSinglePath"
    CodeFile="SelectSinglePath.ascx.cs" %>
<cms:CMSUpdatePanel ID="pnlUpdate" runat="server">
    <ContentTemplate>
        <cms:CMSTextBox ID="txtPath" runat="server" MaxLength="200" CssClass="SelectorTextBox" /><cms:LocalizedButton
            ID="btnSelectPath" runat="server" CssClass="ContentButton" EnableViewState="false"
            ResourceString="general.select" /><cms:LocalizedButton ID="btnSetPermissions" runat="server"
                CssClass="LongButton" EnableViewState="false" ResourceString="selectsinglepath.setpermissions"
                RenderScript="true" />
        <cms:CMSTextBox ID="txtNodeId" runat="server" CssClass="Hidden" AutoPostBack="true" /><cms:LocalizedLabel
            ID="lblNodeId" runat="server" EnableViewState="false" Display="false" ResourceString="generalproperties.nodeid"
            AssociatedControlID="txtNodeId" />
    </ContentTemplate>
</cms:CMSUpdatePanel>
