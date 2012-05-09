<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_AbuseReport_Controls_AbuseReportEdit" CodeFile="AbuseReportEdit.ascx.cs" %>
<asp:Panel ID="pnlBody" runat="server" CssClass="AbuseBody" EnableViewState="false">
    <div>
        <cms:LocalizedLabel ID="lblText" EnableViewState="false" ResourceString="abuse.abusetext"
            runat="server" AssociatedControlID="txtText" />
        <cms:CMSTextBox ID="txtText" runat="server" TextMode="MultiLine" CssClass="ReportComment"
            MaxLength="1000" EnableViewState="false" />
    </div>
    <div class="Messages">
        <cms:LocalizedLabel ID="lblSaved" runat="server" CssClass="ContentLabel" EnableViewState="false"
            Visible="false" />
        <cms:CMSRequiredFieldValidator ID="rfvText" runat="server" ControlToValidate="txtText"
            CssClass="ErrorLabel" Display="Static" />
    </div>
    <asp:PlaceHolder ID="plcButtons" runat="server">
        <div class="FloatRight">
            <cms:LocalizedButton ID="btnReport" runat="server" ResourceString="abuse.reportabuse"
                CssClass="SubmitButton" EnableViewState="false" OnClick="btnReport_Click" />
            <cms:LocalizedButton ID="btnCancel" runat="server" ResourceString="general.cancel"
                Visible="false" CssClass="SubmitButton" EnableViewState="false" OnClick="btnCancel_Click" />
        </div>
    </asp:PlaceHolder>
</asp:Panel>
