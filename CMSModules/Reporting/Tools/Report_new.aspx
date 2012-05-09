<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Reporting_Tools_Report_new"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" CodeFile="Report_new.aspx.cs" %>

<%@ Register Src="~/CMSFormControls/System/LocalizableTextBox.ascx" TagName="LocalizableTextBox" TagPrefix="cms" %>

<asp:Content ContentPlaceHolderID="plcContent" ID="content" runat="server">
    <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
        Visible="false" />
    <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
        Visible="false" />
    <asp:Literal runat="server" ID="ltlScript" />
    <table style="vertical-align: top">
        <tr>
            <td class="FieldLabel">
                <asp:Label runat="server" ID="lblReportDisplayName" EnableViewState="false" />
            </td>
            <td>
                <cms:LocalizableTextBox ID="txtReportDisplayName" runat="server" CssClass="TextBoxField" MaxLength="440"
                    EnableViewState="false" />
                <cms:CMSRequiredFieldValidator ID="rfvReportDisplayName" runat="server" ErrorMessage=""
                    ControlToValidate="txtReportDisplayName:textbox" EnableViewState="false"></cms:CMSRequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <asp:Label runat="server" ID="lblReportName" EnableViewState="false" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtReportName" runat="server" CssClass="TextBoxField" MaxLength="100"
                    EnableViewState="false" />
                <cms:CMSRequiredFieldValidator ID="rfvReportName" runat="server" ErrorMessage="" ControlToValidate="txtReportName"
                    EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <asp:Label runat="server" ID="lblReportAccess" EnableViewState="false" />
            </td>
            <td>
                <asp:CheckBox ID="chkReportAccess" runat="server" Checked="True" />
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <cms:CMSButton runat="server" ID="btnOk" OnClick="btnOK_Click" EnableViewState="false"
                    CssClass="SubmitButton" />
            </td>
        </tr>
    </table>
</asp:Content>
