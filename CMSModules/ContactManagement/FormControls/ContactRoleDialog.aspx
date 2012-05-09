<%@ Page Language="C#" AutoEventWireup="True" Inherits="CMSModules_ContactManagement_FormControls_ContactRoleDialog"
    Title="Contact role" EnableEventValidation="false" MasterPageFile="~/CMSMasterPages/UI/Dialogs/ModalDialogPage.master"
    Theme="Default" CodeFile="ContactRoleDialog.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<%@ Register Namespace="CMS.UIControls.UniGridConfig" TagPrefix="ug" Assembly="CMS.UIControls" %>
<asp:Content ID="content" ContentPlaceHolderID="plcContent" runat="Server">
    <asp:Panel ID="pnlBody" runat="server" CssClass="UniSelectorDialogBody">
        <cms:CMSUpdatePanel ID="pnlUpdate" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="UniSelectorDialogGridArea">
                    <div class="UniSelectorDialogGridPadding">
                        <cms:UniGrid runat="server" ID="gridElem" OrderBy="ContactRoleDisplayName" ObjectType="om.contactrole"
                            Columns="ContactRoleID,ContactRoleDisplayName,ContactRoleDescription,ContactRoleSiteID"
                            IsLiveSite="false" ShowActionsMenu="false">
                            <GridColumns>
                                <ug:Column ExternalSourceName="ContactRoleDisplayName" Source="##ALL##" Caption="$om.contactrole.displayname$"
                                    Wrap="false" Width="100%">
                                    <Filter Type="text" Size="100" Source="ContactRoleDisplayName" />
                                </ug:Column>
                            </GridColumns>
                            <GridOptions DisplayFilter="true" ShowSelection="false" />
                        </cms:UniGrid>
                        <div class="ClearBoth">
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </cms:CMSUpdatePanel>
    </asp:Panel>
</asp:Content>
<asp:Content ID="footer" ContentPlaceHolderID="plcFooter" runat="server">
    <div class="FloatRight">
        <cms:LocalizedButton ID="btnReset" runat="server" CssClass="SubmitButton" EnableViewState="False"
            ResourceString="general.reset" Visible="false" />
        <cms:LocalizedButton ID="btnCancel" runat="server" CssClass="SubmitButton" EnableViewState="False"
            ResourceString="general.cancel" OnClientClick="window.close();return false;" />
    </div>
</asp:Content>
