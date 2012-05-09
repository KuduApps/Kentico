<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_ProjectManagement_Controls_LiveControls_Tasks" CodeFile="Tasks.ascx.cs" %>
<%@ Register Src="~/CMSModules/ProjectManagement/Controls/UI/ProjectTask/List.ascx"
    TagPrefix="cms" TagName="TaskList" %>
<%@ Register Src="~/CMSModules/ProjectManagement/Controls/UI/ProjectTask/Edit.ascx"
    TagPrefix="cms" TagName="TaskEdit" %>
<%@ Register Src="~/CMSAdminControls/UI/PageElements/HeaderActions.ascx" TagName="HeaderActions"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/UI/PageElements/PageTitle.ascx" TagName="PageTitle"
    TagPrefix="cms" %>
<cms:CMSUpdatePanel runat="server" ID="pnlUpdateList" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:Panel ID="plcList" runat="server">
            <asp:Panel ID="pnlListActions" runat="server" CssClass="Actions">
                <cms:HeaderActions ID="actionsElem" runat="server" />
            </asp:Panel>
            <cms:LocalizedLabel runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
                Visible="false"></cms:LocalizedLabel>
            <cms:TaskList ID="ucTaskList" IgnoreCommunityGroup="true" runat="server" DelayedReload="true" />
        </asp:Panel>
    </ContentTemplate>
</cms:CMSUpdatePanel>
<cms:CMSUpdatePanel runat="server" ID="pnlUpdate" UpdateMode="Conditional">
    <ContentTemplate>
        <cms:ModalPopupDialog Visible="false" runat="server" ID="ucPopupDialog" BackgroundCssClass="ModalBackground"
            CancelControlID="btnCancel" CssClass="ModalPopupDialog">
            <asp:Panel ID="pnlBody" runat="server" CssClass="DialogPageBody">
                <asp:Panel ID="pnlHeader" runat="server" CssClass="PageHeader" EnableViewState="false">
                    <cms:PageTitle SetWindowTitle="false" GenerateFullWidth="false" ID="titleElem" runat="server" />
                </asp:Panel>
                <asp:Panel ID="pnlContent" runat="server" CssClass="DialogPageContent DialogScrollableContent">
                    <asp:Panel ID="pnlContentBody" runat="server" CssClass="PageBody">
                        <cms:TaskEdit DisplayTaskURL="true" ID="ucTaskEdit" runat="server" />
                        <br />
                    </asp:Panel>
                </asp:Panel>
                <asp:Panel runat="server" ID="pnlFooterContainer">
                    <div class="PageFooterLine">
                        <asp:Panel runat="server" ID="pnlFooter" CssClass="Buttons">
                            <cms:LocalizedButton runat="server" ID="btnOK" ResourceString="general.ok" OnClick="btnOK_onClick"
                                CssClass="SubmitButton" />
                            <cms:LocalizedButton runat="server" ID="btnCancel" ResourceString="general.cancel"
                                OnClientClick="return false;" CssClass="SubmitButton" />
                        </asp:Panel>
                    </div>
                </asp:Panel>
            </asp:Panel>
        </cms:ModalPopupDialog>
    </ContentTemplate>
</cms:CMSUpdatePanel>
