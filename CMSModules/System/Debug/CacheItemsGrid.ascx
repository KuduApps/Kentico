<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_System_Debug_CacheItemsGrid"
    CodeFile="CacheItemsGrid.ascx.cs" %>
<%@ Register Src="~/CMSAdminControls/UI/UniGrid/Controls/UniGridPager.ascx" TagName="UniGridPager"
    TagPrefix="cms" %>
<asp:HiddenField runat="server" ID="hdnKey" EnableViewState="false" />
<asp:Panel runat="server" ID="pnlSearch" DefaultButton="btnSearch">
    <table cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <cms:LocalizedLabel runat="server" ID="lblFilter" ResourceString="General.Search"
                    DisplayColon="true" EnableViewState="false" />&nbsp;
            </td>
            <td>
                <cms:CMSTextBox runat="server" ID="txtFilter" CssClass="TextBoxField" />
            </td>
            <td>
                <cms:LocalizedButton runat="server" ID="btnSearch" ResourceString="General.Search"
                    CssClass="ContentButton" EnableViewState="false" OnClick="btnSearch_Click" />
            </td>
        </tr>
    </table>
    <br />
</asp:Panel>
<asp:Panel runat="server" ID="plcItems">
    <table border="1" cellspacing="0" cellpadding="3" class="UniGridGrid" rules="rows"
        style="border-collapse: collapse;">
        <tr class="UniGridHead">
            <th style="white-space: nowrap;">
                <asp:Label runat="server" ID="lblAction" EnableViewState="false" />
            </th>
            <th style="white-space: nowrap; width: 100%;">
                <asp:Label runat="server" ID="lblKey" EnableViewState="false" />
            </th>
            <asp:PlaceHolder runat="server" ID="plcData">
                <th>
                    <asp:Label runat="server" ID="lblData" EnableViewState="false" />
                </th>
            </asp:PlaceHolder>
            <asp:PlaceHolder runat="server" ID="plcContainer" Visible="false">
                <th>
                    <asp:Label runat="server" ID="lblExpiration" EnableViewState="false" />
                </th>
                <th>
                    <asp:Label runat="server" ID="lblPriority" EnableViewState="false" />
                </th>
            </asp:PlaceHolder>
        </tr>
        <asp:Literal ID="ltlCacheInfo" runat="server" EnableViewState="false" />
    </table>
    <cms:UniGridPager ID="pagerItems" ShortID="p" ShowDirectPageControl="true" runat="server" />
</asp:Panel>
<cms:LocalizedLabel CssClass="InfoLabel" runat="server" ID="lblInfo" ResourceString="General.NoDataFound"
    Visible="false" EnableViewState="false" />
