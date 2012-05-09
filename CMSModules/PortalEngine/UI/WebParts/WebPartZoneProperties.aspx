<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_PortalEngine_UI_WebParts_WebPartZoneProperties"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/Dialogs/ModalDialogPage.master"
    Title="Web part zone - Properties" CodeFile="WebPartZoneProperties.aspx.cs" %>

<%@ Register TagPrefix="cms" Namespace="CMS.UIControls" Assembly="CMS.UIControls" %>
<%@ Register Src="~/CMSModules/PortalEngine/Controls/WebParts/WebPartZoneProperties.ascx"
    TagName="WebPartZoneProperties" TagPrefix="cms" %>
<asp:Content ContentPlaceHolderID="plcHeaderTabs" runat="server" ID="plcHeaderTabs">
    <asp:Panel ID="pnlTabsContainer" runat="server" Visible="false">
        <asp:Panel runat="server" ID="pnlTabs" CssClass="TabsPageTabs LightTabs">
            <asp:Panel ID="pnlContainer" runat="server">
                <asp:Panel runat="server" ID="pnlLeft" CssClass="FullTabsLeft">
                    &nbsp;
                </asp:Panel>
                <asp:Panel runat="server" ID="pnlPropTabs" CssClass="TabsTabs">
                    <asp:Panel runat="server" ID="pnlWhite" CssClass="TabsWhite">
                        <cms:UITabs ID="tabsElem" runat="server" UseClientScript="true" ModuleName="CMS.Content"
                            ElementName="Design.WebPartZoneProperties" />
                    </asp:Panel>
                </asp:Panel>
                <asp:Panel runat="server" ID="Panel1" CssClass="FullTabsRight">
                    &nbsp;
                </asp:Panel>
            </asp:Panel>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlZonePropertiesSeparator" CssClass="ZonePropertiesSeparator">
            <asp:Panel runat="server" ID="pnlSeparator" CssClass="HeaderSeparator">
                &nbsp;
            </asp:Panel>
        </asp:Panel>
    </asp:Panel>
</asp:Content>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <script type="text/javascript">
        function UpdateVariantPosition(itemCode, variantId) {
            wopener = parent.wopener;
            if (wopener.UpdateVariantPosition) {
                wopener.UpdateVariantPosition(itemCode, variantId);
            }
        }
    </script>
    <div class="WebPartZoneProperties">
        <div class="PageContent">
            <cms:CMSUpdatePanel runat="server" ID="pnlUpdate" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:HiddenField runat="server" ID="hdnSelectedTab" />
                    <asp:PlaceHolder runat="server" ID="plcDynamicContent"></asp:PlaceHolder>
                </ContentTemplate>
            </cms:CMSUpdatePanel>
        </div>
    </div>
    <br />
    <asp:Literal ID="ltlScript" runat="server" EnableViewState="false" />
</asp:Content>
<asp:Content ID="cntFooter" ContentPlaceHolderID="plcFooter" runat="server">
    <div class="FloatLeft">
        <asp:CheckBox runat="server" ID="chkRefresh" Checked="true" />
    </div>
    <div class="FloatRight">
        <cms:CMSButton ID="btnOk" runat="server" CssClass="SubmitButton" Visible="true" OnClick="btnOK_Click" /><cms:CMSButton
            ID="btnCancel" runat="server" CssClass="SubmitButton" OnClientClick="window.close(); return false;" /><cms:CMSButton
                ID="btnApply" runat="server" CssClass="SubmitButton" Visible="true" OnClick="btnApply_Click" />
    </div>
</asp:Content>
