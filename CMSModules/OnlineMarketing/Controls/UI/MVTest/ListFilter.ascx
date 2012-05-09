<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ListFilter.ascx.cs" Inherits="CMSModules_OnlineMarketing_Controls_UI_MVTest_ListFilter" %>
<%@ Register Src="~/CMSAdminControls/UI/UniGrid/Filters/TextSimpleFilter.ascx" TagName="TextSimpleFilter"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/UI/UniGrid/Filters/TimeSimpleFilter.ascx" TagName="TimeSimpleFilter"
    TagPrefix="cms" %>
<asp:Label Visible="false" CssClass="ErrorLabel" runat="server" ID="lblError" EnableViewState="false" />
<table>
    <tr>
        <td>
            <cms:LocalizedLabel runat="server" ID="lblName" ResourceString="abtesting.testname"
                DisplayColon="true" />
        </td>
        <td>
            <cms:TextSimpleFilter runat="server" Column="MVTestName" ID="tsfFrom" Size="100" />
        </td>
    </tr>
    <tr>
        <td>
            <cms:LocalizedLabel runat="server" ID="lblSource" ResourceString="abtesting.sourcepage"
                DisplayColon="true" />
        </td>
        <td>
            <cms:TextSimpleFilter ID="tsfSource" runat="server" Column="MVTestPage" Size="450" />
        </td>
    </tr>
    <tr>
        <td>
            <cms:LocalizedLabel runat="server" ID="lblFrom" ResourceString="general.from" DisplayColon="true" />
        </td>
        <td>
            <cms:DateTimePicker runat="server" ID="dtpFrom" />
        </td>
    </tr>
    <tr>
        <td>
            <cms:LocalizedLabel runat="server" ID="lblTo" ResourceString="general.to" DisplayColon="true" />
        </td>
        <td>
            <cms:DateTimePicker runat="server" ID="dtpTo" />
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
        <td>
            <cms:LocalizedButton runat="server" ID="btnShow" ResourceString="general.ok" CssClass="ContentButton" />
        </td>
    </tr>
</table>
