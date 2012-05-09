<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_ProjectManagement_Controls_UI_ProjectTask_List"
    CodeFile="List.ascx.cs" %>
<%@ Register TagPrefix="cms" Namespace="CMS.ExtendedControls" Assembly="CMS.ExtendedControls" %>
<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/UI/PageElements/PageTitle.ascx" TagName="PageTitle"
    TagPrefix="cms" %>
<cms:LocalizedLabel runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
    Visible="false" />
<cms:LocalizedLabel runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
    Visible="false" />
<cms:UniGrid ID="gridElem" runat="server" GridName="~/CMSModules/ProjectManagement/Controls/UI/ProjectTask/List.xml" />
<cms:CMSUpdatePanel runat="server" ID="pnlUpdate" UpdateMode="Conditional">
    <ContentTemplate>
        <cms:ModalPopupDialog runat="server" ID="reminderPopupDialog" CancelControlID="btnReminderCancel"
            BackgroundCssClass="ModalBackground" CssClass="ModalPopupDialog">
            <asp:Panel ID="pnlDialogBody" runat="server" CssClass="DialogPageBody">
                <asp:Panel ID="pnlHeader" runat="server" CssClass="PageHeader" EnableViewState="false">
                    <cms:PageTitle SetWindowTitle="false" GenerateFullWidth="false" ID="titleElem" runat="server" />
                </asp:Panel>
                <asp:Panel ID="pnlContent" runat="server" CssClass="DialogPageContent DialogScrollableContent">
                    <cms:LocalizedLabel runat="server" ID="lblReminderError" CssClass="ErrorLabel" EnableViewState="false"
                        Visible="false" />
                    <cms:LocalizedLabel ID="lblProjectDescription" runat="server" EnableViewState="false"
                        DisplayColon="true" ResourceString="pm.projecttask.remindertext" AssociatedControlID="txtReminderText" />
                    <br />
                    <cms:CMSTextBox ID="txtReminderText" runat="server" TextMode="MultiLine" CssClass="TextAreaLarge"
                        EnableViewState="false" />
                    <br />
                </asp:Panel>
                <asp:Panel runat="server" ID="pnlFooterContainer">
                    <div class="PageFooterLine">
                        <asp:Panel runat="server" ID="pnlFooter" CssClass="Buttons">
                            <cms:LocalizedButton runat="server" ID="btnReminderOK" ResourceString="general.send"
                                OnClick="btnReminderOK_onClick" CssClass="SubmitButton" />
                            <cms:LocalizedButton runat="server" OnClientClick="return false;" ID="btnReminderCancel"
                                ResourceString="general.cancel" CssClass="SubmitButton" />
                        </asp:Panel>
                    </div>
                </asp:Panel>
            </asp:Panel>
        </cms:ModalPopupDialog>
    </ContentTemplate>
</cms:CMSUpdatePanel>
