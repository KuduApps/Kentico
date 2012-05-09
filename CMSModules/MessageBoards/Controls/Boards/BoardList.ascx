<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_MessageBoards_Controls_Boards_BoardList" CodeFile="BoardList.ascx.cs" %>
<%@ Register src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" tagname="UniGrid" tagprefix="cms" %>
<table>
    <tr>
        <td colspan="2">
            <asp:Label ID="lblInfo" runat="server" EnableViewState="false" CssClass="InfoLabel" />
        </td>
    </tr>
    <tr>
        <td style="white-space: nowrap;">
            <asp:Label ID="lblBoardName" AssociatedControlID="txtBoardName" runat="server" EnableViewState="false" />
        </td>
        <td style="width: 100%;">
            <cms:CMSTextBox ID="txtBoardName" runat="server" CssClass="TextBoxField" EnableViewState="false" />
        </td>
    </tr>
    <tr>
        <td>
        </td>
        <td>
            <cms:CMSButton ID="btnFilter" runat="server" CssClass="ContentButton" OnClick="btnFilter_Click"
                EnableViewState="false" />
        </td>
    </tr>
    <tr>
        <td colspan="2">
            &nbsp;
        </td>
    </tr>
</table>
<cms:UniGrid ID="gridBoards" runat="server" ExportFileName="board_board" />
