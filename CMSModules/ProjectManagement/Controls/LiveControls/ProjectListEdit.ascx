<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_ProjectManagement_Controls_LiveControls_ProjectListEdit" CodeFile="ProjectListEdit.ascx.cs" %>
<%@ Register Src="~/CMSModules/ProjectManagement/Controls/UI/ProjectTask/List.ascx"
    TagName="TaskList" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/ProjectManagement/Controls/UI/ProjectTask/Edit.ascx"
    TagName="TaskEdit" TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/UI/PageElements/HeaderActions.ascx" TagName="HeaderActions"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/ProjectManagement/Controls/UI/Project/Edit.ascx" TagName="ProjectEdit"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/UI/PageElements/PageTitle.ascx" TagName="PageTitle"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/ProjectManagement/Controls/UI/Project/Security.ascx"
    TagName="Security" TagPrefix="cms" %>
<div class="ProjectDetail">
    <div class="ProjectDetailInfo">
        <cms:CMSUpdatePanel runat="server" ID="pnlUpdateProjectInfo" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Repeater ID="repElem" runat="server">
                    <ItemTemplate>
                        <h2>
                            <%# HTMLHelper.HTMLEncode(Eval("ProjectDisplayName").ToString())%></h2>
                        <table>
                            <tr>
                                <td>
                                    <%= GetString("pm.project.goal") %>:
                                </td>
                                <td>
                                    <%# HTMLHelper.HTMLEncode(Convert.ToString(Eval("ProjectDescription")))%>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <%= GetString("pm.project.deadline")%>:
                                </td>
                                <td>
                                    <%#GetConvertedTime(Eval("ProjectDeadline")) %>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <%= GetString("pm.project.progress")%>:
                                </td>
                                <td>
                                    <%# CMS.ProjectManagement.ProjectTaskInfoProvider.GenerateProgressHtml(ValidationHelper.GetInteger(Eval("ProjectProgress"),0), true) %>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <%= GetString("pm.projectstatus")%>:
                                </td>
                                <td>
                                    <img class="FloatLeft" src='<%# CMS.GlobalHelper.URLHelper.ResolveUrl(HTMLHelper.HTMLEncode(Convert.ToString(Eval("ProjectStatusIcon")))) %>'
                                        alt="Project status" title="Project status" />
                                    &nbsp;&nbsp;<%#  HTMLHelper.HTMLEncode (Convert.ToString (Eval("ProjectStatus"))) %>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <%= GetString("pm.project.owner")%>:
                                </td>
                                <td>
                                    <%# GetFormattedName(Convert.ToString(Eval("ProjectOwnerFullName")), Convert.ToString(Eval("ProjectOwnerUserName")))%>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <%= GetString("pm.project.createdby")%>:
                                </td>
                                <td>
                                    <%# GetFormattedName(Convert.ToString(Eval("ProjectCreatedFullName")), Convert.ToString(Eval("ProjectCreatedUserName")))%>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:Repeater>
            </ContentTemplate>
        </cms:CMSUpdatePanel>
    </div>
    <cms:CMSUpdatePanel runat="server" ID="pnlUpdateProjectEdit" UpdateMode="Conditional">
        <ContentTemplate>
            <cms:LocalizedButton runat="server" ID="btnEdit" ResourceString="pm.project.edit"
                CssClass="ContentButton" OnClick="btnEdit_Click" />
        </ContentTemplate>
    </cms:CMSUpdatePanel>
</div>
<cms:CMSUpdatePanel runat="server" ID="pnlUpdate" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:Panel ID="pnlBody" runat="server" CssClass="PageBody">
            <asp:PlaceHolder ID="plcList" runat="server">
                <asp:Panel ID="pnlListActions" runat="server" CssClass="Actions">
                    <cms:HeaderActions ID="actionsElem" runat="server" />
                </asp:Panel>
                <asp:Panel ID="pnlListContent" runat="server">
                    <cms:LocalizedLabel runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
                        Visible="false"></cms:LocalizedLabel>
                    <cms:TaskList ID="ucTaskList" runat="server" />
                </asp:Panel>
            </asp:PlaceHolder>
        </asp:Panel>
    </ContentTemplate>
</cms:CMSUpdatePanel>
<cms:CMSUpdatePanel runat="server" ID="pnlUpdateModalProject" UpdateMode="Conditional">
    <ContentTemplate>
        <cms:ModalPopupDialog runat="server" ID="ucPopupDialogProject" BackgroundCssClass="ModalBackground"
            CssClass="ModalPopupDialog" CancelControlID="btnCancel">
            <asp:Panel ID="pnlDialogBody" runat="server" CssClass="DialogPageBody">
                <asp:Panel ID="pnlHeader" runat="server" CssClass="PageHeader" EnableViewState="false">
                    <cms:PageTitle SetWindowTitle="false" GenerateFullWidth="false" ID="titleElem" runat="server" />
                </asp:Panel>
                <asp:Panel ID="pnlContent" runat="server" CssClass="DialogPageContent">
                    <asp:Panel runat="server" ID="pnlTabsMain" CssClass="TabsPageHeader">
                        <asp:Panel runat="server" ID="pnlTabsContainer" CssClass="TabsPageTabs">
                            <asp:Panel runat="server" ID="pnlTabs" CssClass="TabsTabs">
                                <asp:Panel runat="server" ID="pnlWhite" CssClass="TabsWhite">
                                    <cms:BasicTabControl ID="tabControlElem" runat="server" TabControlLayout="Horizontal"
                                        UseClientScript="true" UsePostback="true" />
                                </asp:Panel>
                            </asp:Panel>
                        </asp:Panel>
                    </asp:Panel>
                    <div class="TabBody DialogScrollableContent">
                        <cms:ProjectEdit ID="ucProjectEdit" DisableOnSiteValidators="true" ShowOKButton="false"
                            ShowPageSelector="false" runat="server" />
                        <asp:Panel runat="server" ID="pnlSecurity" CssClass="SecurityArea">
                            <cms:CMSUpdatePanel runat="server" ID="pnlUpdateProjectSecurity" UpdateMode="Conditional">
                                <ContentTemplate>
                                <cms:Security runat="server" ID="ucSecurity" />
                                </ContentTemplate>
                            </cms:CMSUpdatePanel>
                        </asp:Panel>
                    </div>
                </asp:Panel>
                <asp:Panel runat="server" ID="pnlFooterContainer">
                    <div class="PageFooterLine">
                        <asp:Panel runat="server" ID="pnlFooter" CssClass="Buttons">
                            <cms:LocalizedButton runat="server" ID="btnOK" ResourceString="general.ok" CssClass="SubmitButton"
                                OnClick="btnOK_Click" />
                            <cms:LocalizedButton runat="server" ID="btnCancel" OnClientClick="return false;"
                                ResourceString="general.cancel" CssClass="SubmitButton" />
                                <cms:LocalizedButton runat="server" ID="btnApply" OnClick="btnApply_Click"
                            ResourceString="general.apply" CssClass="SubmitButton" />
                        </asp:Panel>
                    </div>
                </asp:Panel>
            </asp:Panel>
        </cms:ModalPopupDialog>
    </ContentTemplate>
</cms:CMSUpdatePanel>
<cms:CMSUpdatePanel runat="server" ID="pnlUpdateModalTask" UpdateMode="Conditional">
    <ContentTemplate>
        <cms:ModalPopupDialog runat="server" Visible="false" ID="ucPopupDialogTask" BackgroundCssClass="ModalBackground"
            CssClass="ModalPopupDialog" CancelControlID="btnCancelTask">
            <asp:Panel ID="pnlTaskBody" runat="server" CssClass="DialogPageBody">
                <asp:Panel ID="pnlTaskTilte" runat="server" CssClass="PageHeader" EnableViewState="false">
                    <cms:PageTitle SetWindowTitle="false" GenerateFullWidth="false" ID="titleTaskElem"
                        runat="server" />
                </asp:Panel>
                <asp:Panel ID="pnlTaskEdit" runat="server" CssClass="DialogPageContent DialogScrollableContent">
                    <cms:TaskEdit ID="ucTaskEdit" DisplayTaskURL="true" ShowOKButton="false" runat="server" />
                </asp:Panel>
                <asp:Panel runat="server" ID="pnlTaskFooter">
                    <div class="PageFooterLine">
                        <asp:Panel runat="server" ID="Panel5" CssClass="Buttons">
                            <cms:LocalizedButton runat="server" ID="btnOkTask" ResourceString="general.ok" CssClass="SubmitButton"
                                OnClick="btnOkTask_Click" />
                            <cms:LocalizedButton runat="server" ID="btnCancelTask" OnClientClick="return false;"
                                ResourceString="general.cancel" CssClass="SubmitButton" />
                        </asp:Panel>
                    </div>
                </asp:Panel>
            </asp:Panel>
        </cms:ModalPopupDialog>
    </ContentTemplate>
</cms:CMSUpdatePanel>
