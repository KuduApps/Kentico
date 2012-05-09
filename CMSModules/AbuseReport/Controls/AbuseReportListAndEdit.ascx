<%@ Control Language="C#" AutoEventWireup="true" CodeFile="~/CMSModules/AbuseReport/Controls/AbuseReportListAndEdit.ascx.cs"
    Inherits="CMSModules_AbuseReport_Controls_AbuseReportListAndEdit" %>
<%@ Register Src="~/CMSModules/AbuseReport/Controls/AbuseReportList.ascx" TagName="AbuseReportList"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/AbuseReport/Controls/AbuseReportStatusEdit.ascx" TagName="AbuseReportEdit"
    TagPrefix="cms" %>
<asp:Panel ID="pnlHeader" runat="server" CssClass="PageTitleBreadCrumbsPadding" Visible="false">
    <div class="HeaderTitleBreadcrumbs">
        <asp:Panel ID="pnlNewHeader" runat="server" >
            <asp:LinkButton ID="lnkEditBack" runat="server" CausesValidation="false" />
            <asp:Label ID="lblEditBack" runat="server" />
            <asp:Label ID="lblEditNew" runat="server" /><br />
        </asp:Panel>
    </div>
</asp:Panel>
<cms:AbuseReportEdit ID="ucAbuseEdit" runat="server" Visible="false" />
<cms:AbuseReportList ID="ucAbuseReportList" runat="server" />
<asp:HiddenField runat="server" ID="hfEditReport" />
