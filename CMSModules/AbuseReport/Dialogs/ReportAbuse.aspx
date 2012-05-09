<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_AbuseReport_Dialogs_ReportAbuse"
    Title="Report abuse" Theme="default" MasterPageFile="~/CMSMasterPages/UI/Dialogs/ModalDialogPage.master" CodeFile="ReportAbuse.aspx.cs" %>

<%@ Register Src="~/CMSModules/AbuseReport/Controls/AbuseReportEdit.ascx" TagName="AbuseReportEdit"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/UI/PageElements/PageTitle.ascx" TagName="PageTitle"
    TagPrefix="cms" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <div class="PageContent">
        <cms:AbuseReportEdit ID="reportElem" runat="server" />
    </div>
</asp:Content>
<asp:Content ID="cntFooter" ContentPlaceHolderID="plcFooter" runat="server">
    <div class="FloatRight">
        <cms:LocalizedButton ID="btnReport" runat="server" ResourceString="abuse.reportabuse"
            CssClass="LongSubmitButton" EnableViewState="false" />
        <cms:LocalizedButton ID="btnCancel" runat="server" ResourceString="general.cancel"
            CssClass="SubmitButton" EnableViewState="false" />
    </div>
</asp:Content>
