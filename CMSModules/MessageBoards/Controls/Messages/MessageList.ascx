<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_MessageBoards_Controls_Messages_MessageList"
    CodeFile="MessageList.ascx.cs" %>
<%@ Register Src="~/CMSAdminControls/UI/PageElements/HeaderActions.ascx" TagName="HeaderActions"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<%@ Register Namespace="CMS.UIControls.UniGridConfig" TagPrefix="ug" Assembly="CMS.UIControls" %>
<%@ Register Src="~/CMSFormControls/Sites/SiteSelector.ascx" TagName="SiteSelector"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/MessageBoards/FormControls/SelectBoard.ascx" TagName="BoardSelector"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/UI/System/PermissionMessage.ascx" TagName="PermissionMessage"
    TagPrefix="cms" %>

<script type="text/javascript">
    //<![CDATA[

    // Confirm mass delete
    function MassConfirm(dropdown, msg) {
        var drop = document.getElementById(dropdown);
        if (drop != null) {
            if (drop.value == "DELETE") {
                return confirm(msg);
            }
            return true;
        }
        return true;
    }

    //]]>
</script>

<cms:PermissionMessage ID="messageElem" runat="server" Visible="false" EnableViewState="false" />
<asp:PlaceHolder ID="plcNewMessageGroups" runat="server" Visible="false">
    <div class="PageHeaderLine">
        <cms:HeaderActions ID="headerActions" runat="server" />
    </div>
</asp:PlaceHolder>
<asp:Label ID="lblError" runat="server" Visible="false" EnableViewState="false" CssClass="ErrorLabel" />
<table width="100%" class="BoardTable">
    <%-- Filter --%>
    <tr runat="server" id="RowFilter">
        <td>
            <table>
                <%-- Site --%>
                <asp:PlaceHolder ID="plcSite" runat="server" Visible="false">
                    <tr>
                        <td class="FieldLabel">
                            <cms:LocalizedLabel ID="lblSiteName" runat="server" ResourceString="board.messagelist.sitename"
                                EnableViewState="false" />
                        </td>
                        <td>
                            <cms:SiteSelector ID="siteSelector" runat="server" />
                        </td>
                    </tr>
                </asp:PlaceHolder>
                <%-- Board --%>
                <asp:PlaceHolder ID="plcBoard" runat="server">
                    <tr>
                        <td class="FieldLabel">
                            <cms:LocalizedLabel ID="lblBoardName" runat="server" ResourceString="board.messagelist.boardname"
                                EnableViewState="false" />
                        </td>
                        <td>
                            <cms:CMSUpdatePanel ID="pnlUpdateBoardSelector" runat="server" UpdateMode="Conditional">
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="siteSelector" />
                                </Triggers>
                                <ContentTemplate>
                                    <cms:BoardSelector ID="boardSelector" runat="server" AddAllItemsRecord="true" />
                                </ContentTemplate>
                            </cms:CMSUpdatePanel>
                        </td>
                    </tr>
                </asp:PlaceHolder>
                <%-- User name --%>
                <tr>
                    <td class="FieldLabel">
                        <cms:LocalizedLabel ID="lblUserName" runat="server" AssociatedControlID="txtUserName"
                            ResourceString="general.username" DisplayColon="true" EnableViewState="false" />
                    </td>
                    <td>
                        <cms:CMSTextBox ID="txtUserName" runat="server" CssClass="TextBoxField" EnableViewState="false" />
                    </td>
                </tr>
                <%-- Text --%>
                <tr>
                    <td class="FieldLabel">
                        <cms:LocalizedLabel ID="lblMessage" AssociatedControlID="txtMessage" runat="server"
                            ResourceString="board.messagelist.message" EnableViewState="false" />
                    </td>
                    <td>
                        <cms:CMSTextBox ID="txtMessage" runat="server" CssClass="TextBoxField" EnableViewState="false" />
                    </td>
                </tr>
                <%-- Is approved --%>
                <tr>
                    <td class="FieldLabel">
                        <cms:LocalizedLabel ID="lblApproved" AssociatedControlID="drpApproved" runat="server"
                            ResourceString="board.messagelist.approved" EnableViewState="false" />
                    </td>
                    <td>
                        <asp:DropDownList ID="drpApproved" runat="server" CssClass="DropDownFieldSmall" />
                    </td>
                </tr>
                <%-- Is spam --%>
                <tr>
                    <td class="FieldLabel">
                        <cms:LocalizedLabel ID="lblSpam" AssociatedControlID="drpSpam" runat="server" ResourceString="board.messagelist.spam"
                            EnableViewState="false" />
                    </td>
                    <td>
                        <asp:DropDownList ID="drpSpam" runat="server" CssClass="DropDownFieldSmall" />
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <cms:CMSButton ID="btnFilter" runat="server" CssClass="ContentButton" EnableViewState="false" />
                    </td>
                </tr>
            </table>
            <br />
        </td>
    </tr>
    <%-- Messages grid --%>
    <tr>
        <td>
            <cms:UniGrid runat="server" ID="gridElem" OrderBy="MessageInserted DESC" Columns="MessageID, MessageUserName, BoardID, MessageText, MessageApproved, MessageIsSpam, BoardDisplayName, MessageInserted"
                ObjectType="board.boardmessagelist">
                <GridActions>
                    <ug:Action Name="edit" ExternalSourceName="edit" Caption="$General.Edit$" Icon="Edit.png" />
                    <ug:Action Name="delete" Caption="$General.Delete$" Icon="Delete.png" Confirmation="$general.confirmdelete$" />
                    <ug:Action Name="approve" ExternalSourceName="approve" Caption="$General.Approve$"
                        Icon="Approve.png" />
                </GridActions>
                <GridColumns>
                    <ug:Column Source="MessageUserName" Caption="$General.UserName$" Wrap="false" />
                    <ug:Column Source="MessageText" ExternalSourceName="MessageText" IsText="true" Caption="$Unigrid.Board.Message.Columns.MessageText$"
                        Wrap="false">
                        <Tooltip Source="MessageText" ExternalSourceName="MessageTooltip" />
                    </ug:Column>
                    <ug:Column Source="MessageApproved" ExternalSourceName="MessageApproved" Caption="$Unigrid.Board.Message.Columns.MessageApproved$"
                        Wrap="false" />
                    <ug:Column Source="MessageIsSpam" ExternalSourceName="MessageIsSpam" Caption="$Unigrid.Board.Message.Columns.MessageIsSpam$"
                        Wrap="false" />
                    <ug:Column Source="BoardDisplayName" Caption="$Unigrid.Board.Message.Columns.BoardName$"
                        Wrap="false" />
                    <ug:Column Source="MessageInserted" ExternalSourceName="MessageInserted" Caption="$Unigrid.Board.Message.Columns.MessageInserted$"
                        Wrap="false" />
                    <ug:Column Width="100%" />
                </GridColumns>
                <GridOptions DisplayFilter="false" ShowSelection="true" />
            </cms:UniGrid>
        </td>
    </tr>
    <%-- Mass actions --%>
    <tr runat="server" id="rowActions">
        <td class="ItemsActions">
            <cms:LocalizedLabel ID="lblActions" AssociatedControlID="drpActions" ResourceString="board.messagelist.actions"
                runat="server" EnableViewState="false" />
            <asp:DropDownList ID="drpActions" runat="server" CssClass="DropDownFieldSmall" />
            <cms:CMSButton ID="btnOk" runat="server" CssClass="SubmitButton" OnClick="btnOk_Clicked"
                EnableViewState="false" />
        </td>
    </tr>
</table>
<cms:CMSButton ID="btnRefreshHdn" runat="server" Visible="false" OnCommand="btnRefreshHdn_Command" />
<asp:Literal ID="ltlScript" runat="server" EnableViewState="false" />
