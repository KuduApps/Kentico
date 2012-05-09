<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_SmartSearch_Controls_UI_SearchIndex_User" CodeFile="SearchIndex_User.ascx.cs" %>
<%@ Register Src="~/CMSFormControls/Inputs/LargeTextArea.ascx" TagName="LargeTextArea" TagPrefix="cms" %>
<%@ Register src="~/CMSModules/Membership/FormControls/Roles/selectrole.ascx" tagname="SelectRole" tagprefix="cms" %>
<cms:LocalizedLabel runat="server" CssClass="InfoLabel" ID="lblInfo" EnableViewState="false" Visible="false"></cms:LocalizedLabel>
<table>
    <tr>
        <td class="FieldLabel">
            <cms:LocalizedLabel ID="lblVisible" runat="server" ResourceString="srch.index.userhidden"
                EnableViewState="false" DisplayColon="true" />
        </td>
        <td>
            <cms:LocalizedCheckBox runat="server" runat="server" ID="chkHidden" />
        </td>
    </tr>
    <tr>
        <td class="FieldLabel">
            <cms:LocalizedLabel ID="lblEnabled" runat="server" ResourceString="srch.index.userenabled"
                EnableViewState="false" DisplayColon="true" />
        </td>
        <td>
            <cms:LocalizedCheckBox runat="server" runat="server" ID="chkOnlyEnabled" Checked="true" />
        </td>
    </tr>
    <tr>
        <td class="FieldLabel">
            <cms:LocalizedLabel ID="lblSite" runat="server" ResourceString="srch.index.usersite"
                EnableViewState="false" DisplayColon="true" />
        </td>
        <td>
            <cms:LocalizedCheckBox runat="server" runat="server" ID="chkSite" />
        </td>
    </tr>
    <tr>
        <td class="FieldLabel">
            <cms:LocalizedLabel ID="lblInRole" runat="server" ResourceString="srch.index.userinrole"
                EnableViewState="false" DisplayColon="true" />
        </td>
        <td>
            <cms:SelectRole ID="selectInRole" runat="server" IsLiveSite="false" />
        </td>
    </tr>
    <tr>
        <td class="FieldLabel">
            <cms:LocalizedLabel ID="lblNotInRole" runat="server" ResourceString="srch.index.usernotinrole"
                EnableViewState="false" DisplayColon="true" />
        </td>
        <td>
            <cms:SelectRole ID="selectNotInRole" runat="server" IsLiveSite="false" />
        </td>
    </tr>
    <tr>
        <td class="FieldLabel">
            <cms:LocalizedLabel ID="lblWhere" runat="server" ResourceString="srch.index.where"
                EnableViewState="false" DisplayColon="true" />
        </td>
        <td>
            <cms:LargeTextArea ID="txtWhere" AllowMacros="false" runat="server" />
        </td>
    </tr>
    <tr>
        <td class="FieldLabel">
        </td>
        <td>
            <cms:LocalizedButton runat="server" ID="btnOk" CssClass="SubmitButton" ResourceString="general.ok" OnClick="btnOk_Click" EnableViewState="false" />
        </td>
    </tr>
</table>
