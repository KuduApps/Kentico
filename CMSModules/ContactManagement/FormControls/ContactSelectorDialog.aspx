<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ContactSelectorDialog.aspx.cs"
    Inherits="CMSModules_ContactManagement_FormControls_ContactSelectorDialog" Title="Select contact"
    EnableEventValidation="false" MasterPageFile="~/CMSMasterPages/UI/Dialogs/ModalDialogPage.master"
    Theme="Default" %>

<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<%@ Register Namespace="CMS.UIControls.UniGridConfig" TagPrefix="ug" Assembly="CMS.UIControls" %>
<asp:Content ID="content" ContentPlaceHolderID="plcContent" runat="Server">
    <asp:Panel ID="pnlBody" runat="server" CssClass="UniSelectorDialogBody">
        <cms:CMSUpdatePanel ID="pnlUpdate" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="PageHeaderLine">
                    <div class="Actions">
                        <asp:Image ID="imgNew" runat="server" EnableViewState="false" CssClass="NewItemImage" />
                        <cms:LocalizedLinkButton ID="btnNew" runat="server" ResourceString="om.contact.new" CssClass="NewItemLink" />
                    </div>
                </div>
                <div class="UniSelectorDialogGridArea">
                    <div class="UniSelectorDialogGridPadding">
                        <cms:LocalizedLabel runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false" ResourceString="om.contact.selectparent" />
                        <cms:UniGrid runat="server" ID="gridElem" ObjectType="om.contactlist" OrderBy="ContactLastName"
                            Columns="ContactID,ContactFullNameJoined" IsLiveSite="false">
                            <GridColumns>
                                <ug:Column ExternalSourceName="ContactFullNameJoined" Source="##ALL##" Caption="$om.contact.name$"
                                    Wrap="false">
                                    <Filter Type="text" Size="100" Source="ContactFullNameJoined" />
                                </ug:Column>
                                <ug:Column Width="100%" />
                            </GridColumns>
                            <GridOptions DisplayFilter="true" ShowSelection="false" FilterLimit="10" />
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
        <cms:LocalizedButton ID="btnCancel" runat="server" CssClass="SubmitButton" EnableViewState="False"
            ResourceString="general.cancel" OnClientClick="window.close();return false;" />
    </div>
</asp:Content>
