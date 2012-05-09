<%@ Control Language="C#" AutoEventWireup="true" CodeFile="List.ascx.cs" Inherits="CMSModules_Scoring_Controls_UI_Contact_List" %>
<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<%@ Register Namespace="CMS.UIControls.UniGridConfig" TagPrefix="ug" Assembly="CMS.UIControls" %>
<%@ Register Src="~/CMSFormControls/Filters/NumberFilter.ascx" TagName="NumberFilter"
    TagPrefix="cms" %>
<cms:CMSUpdatePanel ID="pnlUpdate" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <table>
            <tr>
                <td>
                    <cms:LocalizedLabel runat="server" ResourceString="om.score" DisplayColon="true"
                        EnableViewState="false" />
                </td>
                <td>
                    <cms:NumberFilter runat="server" ID="ucScoreFilter" />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <cms:LocalizedButton ID="btnSearch" runat="server" ResourceString="general.search"
                        CssClass="ContentButton" EnableViewState="false" />
                </td>
            </tr>
        </table>
        <br />
        <cms:UniGrid runat="server" ID="gridElem" ObjectType="om.scorecontactrulelist" Columns="OM_SelectScoreContact.ContactID, ContactFullNameJoined, ContactStatusID, SUM(Value) AS Score"
            OrderBy="SUM(Value) DESC, ContactFullNameJoined ASC" ShowActionsMenu="true" ShowObjectMenu="false">
            <GridActions>
                <ug:Action ExternalSourceName="edit" Name="edit" Caption="$om.contact.viewdetail$"
                    Icon="contactdetail.png" ModuleName="CMS.OnlineMarketing" CommandArgument="ContactID" />
            </GridActions>
            <GridColumns>
                <ug:Column Source="ContactFullNameJoined" Caption="$general.fullname$" Wrap="false">
                </ug:Column>
                <ug:Column Source="ContactStatusID" ExternalSourceName="#statusdisplayname" Caption="$om.contactstatus$"
                    Wrap="false">
                </ug:Column>
                <ug:Column Source="Score" Caption="$om.score$" Wrap="false" Sort="false" AllowSorting="false" />
                <ug:Column Width="100%" />
            </GridColumns>
            <GridOptions ShowSelection="true" SelectionColumn="ContactID" DisplayFilter="false" />
        </cms:UniGrid>
        <asp:Panel ID="pnlFooter" runat="server" CssClass="MassAction">
            <asp:DropDownList ID="drpWhat" runat="server" CssClass="DropDownFieldSmall" />
            <asp:DropDownList ID="drpAction" runat="server" CssClass="DropDownFieldSmall" />
            <cms:LocalizedButton ID="btnOk" runat="server" ResourceString="general.ok" CssClass="SubmitButton SelectorButton"
                EnableViewState="false" OnClick="btnOk_Click" />
            <br />
            <br />
        </asp:Panel>
        <asp:Label ID="lblInfo" runat="server" CssClass="InfoLabel" EnableViewState="false" />
        <asp:HiddenField ID="hdnIdentificator" runat="server" EnableViewState="false" />
    </ContentTemplate>
</cms:CMSUpdatePanel>
