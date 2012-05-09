<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WebPartDocumentation.ascx.cs"
    Inherits="CMSModules_PortalEngine_Controls_WebParts_WebPartDocumentation" %>
<%@ Register TagPrefix="cms" Namespace="CMS.UIControls" Assembly="CMS.UIControls" %>
<div class="WebpartTabsPageHeader LightTabs">
    <asp:Panel runat="server" ID="pnlFullTabsLeft" CssClass="FullTabsLeft" />
    <asp:Panel runat="server" ID="pnlTabsContainer" CssClass="FullTabsRight">
        <asp:Panel runat="server" ID="pnlTabs" CssClass="TabsTabs">
            <asp:Panel runat="server" ID="pnlWhite" CssClass="Tabs">
                <cms:UITabs ID="tabControlElem" runat="server" UseClientScript="true" OnOnTabClicked="tabControlElem_clicked" />
            </asp:Panel>
        </asp:Panel>
    </asp:Panel>
    <div class="HeaderSeparatorEnvelope">
        <div class="HeaderSeparator">
            &nbsp;</div>
    </div>
</div>
<div class="DocumentationScrollableDiv" id="divScrolable" runat="server">
    <asp:Panel runat="server" ID="pnlDoc" Visible="true">
        <div class="PageContent">
            <!-- Teaser + description -->
            <table class="DocumentationWebPartsDescription" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="DocumentationWebPartColumn">
                        <asp:Image runat="server" ID="imgTeaser" />
                    </td>
                    <td class="DocumentationWebPartColumnNoLine">
                        <asp:Literal runat="server" ID="ltlDescription" />
                    </td>
                </tr>
                <!-- Documentation -->
                <tr>
                    <td colspan="2">
                        <div class="DocumentationAdditionalText">
                            <asp:Literal runat="server" ID="ltlContent" />
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
    <asp:Panel runat="server" ID="pnlProperties" Visible="false">
        <!-- web part properties -->
        <div class="DocumentationWebPartsProperties">
            <asp:Literal runat="server" ID="ltlProperties" />
        </div>
    </asp:Panel>
</div>
