<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_FormControls_Pages_Development_List"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Form User Controls - Form User Control List"
    CodeFile="List.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<%@ Register Src="~/CMSFormControls/System/UserControlTypeSelector.ascx" TagPrefix="cms"
    TagName="TypeSelector" %>
<%@ Register Src="~/CMSAdminControls/UI/UniGrid/Filters/TextSimpleFilter.ascx" TagName="TextSimpleFilter"
    TagPrefix="cms" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <table border="0">
        <tr>
            <td>
                <cms:LocalizedLabel ID="lblSource" runat="server" ResourceString="general.displayname"
                    DisplayColon="true" />
            </td>
            <td>
                <cms:TextSimpleFilter ID="fltName" runat="server" Column="UserControlDisplayName" />
            </td>
        </tr>
        <tr>
            <td>
                <cms:LocalizedLabel ID="lblType" runat="server" ResourceString="formcontrols.type"
                    DisplayColon="true" />
            </td>
            <td>
                <cms:TypeSelector ID="drpType" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <cms:LocalizedButton ID="btnShow" runat="server" CssClass="ContentButton" ResourceString="general.show" />
            </td>
        </tr>
    </table>
    <br />
    <cms:UniGrid ID="grdList" runat="server" GridName="List.xml" OrderBy="UserControlDisplayName"
        IsLiveSite="false" />
</asp:Content>
