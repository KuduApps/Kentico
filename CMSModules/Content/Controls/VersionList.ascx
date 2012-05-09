<%@ Control Language="C#" AutoEventWireup="true" CodeFile="VersionList.ascx.cs" Inherits="CMSModules_Content_Controls_VersionList" %>
<%@ Register TagPrefix="cms" TagName="UniGrid" Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" %>
<table width="100%">
    <asp:PlaceHolder ID="plcLabels" runat="server">
        <tr>
            <td colspan="2">
                <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false" />
                <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false" />
            </td>
        </tr>
    </asp:PlaceHolder>
    <tr>
        <td>
            <cms:LocalizedLabel ID="lblHistory" runat="server" Font-Bold="true" ResourceString="VersionsProperties.History"
                EnableViewState="false" />
        </td>
        <td class="TextRight">
            <cms:LocalizedButton ID="btnDestroy" runat="server" CssClass="LongButton" OnClick="btnDestroy_Click"
                OnClientClick="return confirm(varConfirmDestroy);" ResourceString="VersionsProperties.Destroy" />
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <cms:UniGrid ID="gridHistory" runat="server" OrderBy="VersionHistoryID DESC" />
        </td>
    </tr>
</table>
