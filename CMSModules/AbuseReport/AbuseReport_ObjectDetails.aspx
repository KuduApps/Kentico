<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_AbuseReport_AbuseReport_ObjectDetails" Title="Untitled Page"
    ValidateRequest="false" Theme="Default" MasterPageFile="~/CMSMasterPages/UI/Dialogs/ModalDialogPage.master" CodeFile="AbuseReport_ObjectDetails.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/ObjectDataViewer.ascx" TagName="ObjectDataViewer"
    TagPrefix="cms" %>
<asp:Content ID="cntBody" ContentPlaceHolderID="plcContent" runat="server">
    <div class="PageContent">
        <cms:LocalizedLabel ID="lblNotSupported" runat="server" ResourceString="abuse.NotSupported"
            CssClass="ErrorLabel" EnableViewState="false" />
        <cms:ObjectDataViewer ID="ObjectDataViewer" runat="server" />
    </div>
</asp:Content>
<asp:Content ID="cntFooter" ContentPlaceHolderID="plcFooter" runat="server">
    <div class="FloatRight">
        <cms:LocalizedButton ID="btnClose" runat="server" CssClass="SubmitButton" ResourceString="general.close"
            OnClientClick="window.close(); return false;" />
    </div>
</asp:Content>
