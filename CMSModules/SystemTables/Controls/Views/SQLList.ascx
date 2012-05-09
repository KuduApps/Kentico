<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SQLList.ascx.cs" Inherits="CMSModules_SystemTables_Controls_Views_SQLList" %>
<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/UI/UniGrid/Filters/TextSimpleFilter.ascx" TagName="SimpleFilter"
    TagPrefix="cms" %>
<asp:Label ID="lblError" runat="server" Visible="false" CssClass="ErrorLabel" />
<asp:Label ID="lblInfo" runat="server" Visible="false" CssClass="InfoLabel" />
<table>
    <tr>
        <td>
            <cms:LocalizedLabel ID="lblDisplayName" runat="server" Visible="true" CssClass="FieldLabel"
                ResourceString="general.name" DisplayColon="true" />
        </td>
        <td>
            <cms:SimpleFilter ID="fltViews" runat="server" />
        </td>
    </tr>
    <tr>
        <td>
            <cms:LocalizedLabel ID="lblIsCustom" runat="server" Visible="true" CssClass="FieldLabel"
                ResourceString="systbl.view.iscustom" DisplayColon="true" />
        </td>
        <td>
            <asp:DropDownList ID="drpCustom" runat="server" CssClass="DropDownFieldFilter" />
        </td>
    </tr>    
    <tr>
        <td>
            &nbsp;
        </td>
        <td>
            <cms:LocalizedButton ID="btnShow" runat="server" CssClass="ContentButton" OnClick="btnShow_Click"
                ResourceString="general.show" />
        </td>
    </tr>
</table>
<br />
<br />
<cms:UniGrid ID="gridViews" runat="server" IsLiveSite="false" />
