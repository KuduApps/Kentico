<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_Integration_Controls_UI_IntegrationTask_List"
    CodeFile="List.ascx.cs" %>
<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<%@ Register Namespace="CMS.UIControls.UniGridConfig" TagPrefix="ug" Assembly="CMS.UIControls" %>
<%@ Register Src="~/CMSAdminControls/UI/PageElements/PageTitle.ascx" TagName="PageTitle"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/AsyncControl.ascx" TagName="AsyncControl" TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/AsyncBackground.ascx" TagName="AsyncBackground"
    TagPrefix="cms" %>
<cms:CMSUpdatePanel ID="pnlUpdate" runat="server">
    <ContentTemplate>
        <asp:Panel runat="server" ID="pnlLog" Visible="false">
            <cms:AsyncBackground ID="backgroundElem" runat="server" />
            <div class="AsyncLogArea">
                <asp:Panel ID="pnlAsyncBody" runat="server" CssClass="PageBody">
                    <asp:Panel ID="pnlTitle" runat="server" CssClass="PageHeader">
                        <cms:PageTitle ID="titleElem" runat="server" />
                    </asp:Panel>
                    <asp:Panel ID="pnlCancel" runat="server" CssClass="PageHeaderLine">
                        <cms:CMSButton runat="server" ID="btnCancel" CssClass="SubmitButton" />
                    </asp:Panel>
                    <asp:Panel ID="pnlAsyncContent" runat="server" CssClass="PageContent">
                        <cms:AsyncControl ID="ctlAsync" runat="server" />
                    </asp:Panel>
                </asp:Panel>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlContent" runat="server">
            <asp:Label ID="lblInfo" runat="server" CssClass="InfoLabel" EnableViewState="false" />
            <asp:Label ID="lblError" runat="server" CssClass="ErrorLabel" EnableViewState="false" />
            <cms:UniGrid runat="server" ID="gridElem" ObjectType="integration.tasklist" OrderBy="TaskID ASC"
                Columns="TaskID,TaskTitle,TaskTime,TaskType,SynchronizationErrorMessage,SynchronizationID,ConnectorDisplayName, ConnectorName, ConnectorEnabled"
                IsLiveSite="false">
                <GridActions Parameters="SynchronizationID">
                    <ug:Action Name="view" Caption="$General.View$" Icon="View.png" ExternalSourceName="view" />
                    <ug:Action Name="run" Caption="$General.Synchronize$" Icon="Synchronize.png" CommandArgument="SynchronizationID"
                        ExternalSourceName="run" />
                    <ug:Action Name="delete" Caption="$General.Delete$" Icon="Delete.png" Confirmation="$General.ConfirmDelete$"
                        ModuleName="CMS.Integration" Permissions="modify" CommandArgument="SynchronizationID" />
                </GridActions>
                <GridColumns>
                    <ug:Column Source="TaskTitle" Caption="$integration.tasktitle$" Wrap="false" Width="100%">
                        <Filter Type="text" />
                    </ug:Column>
                    <ug:Column Source="TaskType" Caption="$integration.tasktype$" Wrap="false">
                        <Filter Type="text" />
                    </ug:Column>
                    <ug:Column Source="TaskTime" Caption="$integration.tasktime$" Wrap="false" />
                    <ug:Column Source="ConnectorDisplayName" Caption="$integration.connectorname$" Wrap="false" />
                    <ug:Column Source="##ALL##" Caption="$general.result$" ExternalSourceName="result"
                        Wrap="false">
                        <Tooltip Source="SynchronizationErrorMessage" Encode="true" />
                    </ug:Column>
                </GridColumns>
                <GridOptions DisplayFilter="true" ShowSelection="true" SelectionColumn="SynchronizationID" />
            </cms:UniGrid>
        </asp:Panel>
        <asp:Panel ID="pnlFooter" runat="server" CssClass="MassAction">
            <asp:DropDownList ID="drpWhat" runat="server" CssClass="DropDownFieldSmall" />
            <asp:DropDownList ID="drpAction" runat="server" CssClass="DropDownFieldSmall" />
            <cms:LocalizedButton ID="btnOk" runat="server" ResourceString="general.ok" CssClass="SubmitButton SelectorButton"
                EnableViewState="false" />
            <br />
            <br />
        </asp:Panel>
        <asp:Label ID="lblInfoBottom" runat="server" CssClass="InfoLabel" EnableViewState="false" />
    </ContentTemplate>
</cms:CMSUpdatePanel>
