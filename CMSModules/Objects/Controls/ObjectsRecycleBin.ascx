<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_Objects_Controls_ObjectsRecycleBin"
    CodeFile="ObjectsRecycleBin.ascx.cs" %>
<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/UI/PageElements/PageTitle.ascx" TagName="PageTitle"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/AsyncControl.ascx" TagName="AsyncControl" TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/AsyncBackground.ascx" TagName="AsyncBackground"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/Content/Controls/Filters/ObjectsRecycleBinFilter.ascx"
    TagName="RecycleBinFilter" TagPrefix="cms" %>
<%@ Register Namespace="CMS.UIControls.UniGridConfig" TagPrefix="ug" Assembly="CMS.UIControls" %>
<asp:Panel runat="server" ID="pnlLog" Visible="false">
    <cms:AsyncBackground ID="backgroundElem" runat="server" />
    <div class="AsyncLogArea">
        <div>
            <asp:Panel ID="pnlAsyncBody" runat="server" CssClass="PageBody">
                <asp:Panel ID="pnlTitleAsync" runat="server" CssClass="PageHeader" EnableViewState="false">
                    <cms:PageTitle ID="titleElemAsync" runat="server" EnableViewState="false" />
                </asp:Panel>
                <asp:Panel ID="pnlCancel" runat="server" CssClass="PageHeaderLine" EnableViewState="false">
                    <cms:LocalizedButton runat="server" ID="btnCancel" ResourceString="general.cancel"
                        CssClass="SubmitButton" EnableViewState="false" />
                </asp:Panel>
                <asp:Panel ID="pnlAsyncContent" runat="server" CssClass="PageContent">
                    <cms:AsyncControl ID="ctlAsync" runat="server" />
                </asp:Panel>
            </asp:Panel>
        </div>
    </div>
</asp:Panel>
<cms:CMSUpdatePanel ID="pnlUpdateInfo" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:Panel ID="pnlInfo" runat="server" EnableViewState="false">
            <asp:Label ID="lblError" runat="server" CssClass="ErrorLabel" EnableViewState="false"
                Visible="false" />
            <asp:Label ID="lblInfo" runat="server" CssClass="InfoLabel" EnableViewState="false"
                Visible="false" />
        </asp:Panel>
    </ContentTemplate>
</cms:CMSUpdatePanel>
<div>
    <asp:PlaceHolder ID="plcFilter" runat="server">
        <cms:CMSUpdatePanel ID="pnlFilter" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <cms:RecycleBinFilter ID="filterBin" runat="server" />
            </ContentTemplate>
        </cms:CMSUpdatePanel>
        <br />
    </asp:PlaceHolder>
    <cms:CMSUpdatePanel ID="pnlGrid" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <cms:UniGrid ID="ugRecycleBin" runat="server" HideControlForZeroRows="true" ShowObjectMenu="false"
                ObjectType="cms.objectversionhistory" Columns="VersionID, VersionObjectType, VersionObjectID, VersionDeletedWhen, VersionObjectDisplayName, VersionObjectSiteID">
                <GridActions Parameters="VersionID">
                    <ug:Action Name="view" Caption="$general.view$" ExternalSourceName="view" Icon="View.png" />
                    <ug:Action Name="restorechilds" CommandArgument="VersionID" Caption="$General.Restore$"
                        ExternalSourceName="restorechilds" Confirmation="$objectversioning.recyclebin.confirmrestore$" />
                    <ug:Action Name="destroy" ExternalSourceName="destroy" CommandArgument="VersionID"
                        Caption="$General.Delete$" Icon="delete.png" Confirmation="$General.ConfirmDelete$" />
                    <ug:Action Name="contextmenu" ExternalSourceName="contextmenu" Caption="$General.OtherActions$"
                        Icon="menu.png" ContextMenu="~/CMSModules/Objects/Controls/RecycleBinMenu.ascx"
                        MenuParameter="'{0}'" />
                </GridActions>
                <GridColumns>
                    <ug:Column Source="VersionObjectDisplayName" ExternalSourceName="objectname" Caption="$general.objectname$"
                        MaxLength="100" Wrap="false" Width="100%" Sort="VersionObjectDisplayName">
                        <Tooltip Source="VersionObjectDisplayName" ExternalSourceName="objectname" />
                    </ug:Column>
                    <ug:Column Source="VersionObjectType" ExternalSourceName="versionobjecttype" Caption="$general.objecttype$"
                        Wrap="false" MaxLength="50" Sort="VersionObjectType">
                    </ug:Column>
                    <ug:Column Source="VersionDeletedWhen" ExternalSourceName="deletedwhen" Caption="$Unigrid.RecycleBin.Columns.LastModified$"
                        Wrap="false" Sort="VersionDeletedWhen">
                        <Tooltip Source="VersionDeletedWhen" ExternalSourceName="deletedwhentooltip" />
                    </ug:Column>
                </GridColumns>
                <GridOptions ShowSelection="true" SelectionColumn="VersionID" DisplayFilter="false" />
            </cms:UniGrid>
            <asp:Panel ID="pnlFooter" runat="server" CssClass="MassAction">
                <asp:DropDownList ID="drpWhat" runat="server" CssClass="DropDownFieldSmall" />
                <asp:DropDownList ID="drpAction" runat="server" />
                <cms:LocalizedButton ID="btnOk" runat="server" ResourceString="general.ok" CssClass="SubmitButton SelectorButton"
                    EnableViewState="false" OnClick="btnOk_OnClick" />
                <br />
                <br />
                <asp:Label ID="lblValidation" runat="server" CssClass="InfoLabel" EnableViewState="false" />
            </asp:Panel>
            <asp:HiddenField ID="hdnValue" runat="server" EnableViewState="false" />
            <asp:Button ID="btnHidden" runat="server" CssClass="HiddenButton" EnableViewState="false"
                OnClick="btnHidden_Click" />
        </ContentTemplate>
    </cms:CMSUpdatePanel>
</div>
