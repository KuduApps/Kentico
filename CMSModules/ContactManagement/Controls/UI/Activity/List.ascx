<%@ Control Language="C#" AutoEventWireup="true" CodeFile="List.ascx.cs" Inherits="CMSModules_ContactManagement_Controls_UI_Activity_List" %>
<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<%@ Register Namespace="CMS.UIControls.UniGridConfig" TagPrefix="ug" Assembly="CMS.UIControls" %>
<asp:Panel ID="pnlUpdate" runat="server">
    <cms:UniGrid runat="server" ID="gridElem" ObjectType="om.activitylist" Columns="ActivityID,ContactFullNameJoined,ActivityTitle,ActivityType,ActivityCreated,ActivityIPAddress,ActivitySiteID"
        IsLiveSite="false">
        <GridActions Parameters="ActivityID">
            <ug:Action Name="view" ExternalSourceName="view" Caption="$General.View$" Icon="View.png" />
            <ug:Action Name="#delete" ExternalSourceName="delete" CommandArgument="ActivityID"
                Caption="$General.Delete$" Icon="Delete.png" Confirmation="$General.ConfirmDelete$"
                ModuleName="CMS.ContactManagement" Permissions="ManageActivities" />
        </GridActions>
        <GridColumns>
            <ug:Column Source="ActivityTitle" Caption="$om.activity.gridtitle$" Wrap="false" />
            <ug:Column Source="ActivityType" ExternalSourceName="acttype" Caption="$general.type$"
                Wrap="false">
                <Tooltip Source="ActivityType" ExternalSourceName="acttypedesc" />
            </ug:Column>
            <ug:Column Source="ContactFullNameJoined" Caption="$om.activity.contactname$" Wrap="false"
                Name="contactname" />
            <ug:Column Source="ActivityIPAddress" Caption="$om.activity.ipaddress$" Wrap="false"
                Name="ipaddress" />
            <ug:Column Source="ActivityCreated" Caption="$om.activity.activitytime$" Wrap="false" />
            <ug:Column Source="ActivitySiteID" AllowSorting="false" ExternalSourceName="#sitenameorglobal"
                Caption="$general.sitename$" Wrap="false" Name="sitename" Localize="true" />
            <ug:Column Width="100%" />
        </GridColumns>
        <GridOptions ShowSelection="true" />
    </cms:UniGrid>
</asp:Panel>
<asp:Panel ID="pnlFooter" runat="server" CssClass="MassAction" Visible="false">
    <asp:DropDownList ID="drpWhat" runat="server" CssClass="DropDownFieldSmall" />
    <asp:DropDownList ID="drpAction" runat="server" CssClass="DropDownFieldSmall" />
    <cms:LocalizedButton ID="btnOk" runat="server" ResourceString="general.ok" CssClass="SubmitButton"
        EnableViewState="false" />
    <br />
    <br />
</asp:Panel>
<asp:Label ID="lblInfo" runat="server" CssClass="InfoLabel" EnableViewState="false" />
<asp:HiddenField ID="hdnIdentificator" runat="server" EnableViewState="false" />
