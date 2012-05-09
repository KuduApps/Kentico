<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_Content_Controls_Dialogs_Properties_NodeGuidProperties" CodeFile="NodeGuidProperties.ascx.cs" %>
<%@ Register Src="~/CMSModules/Content/Controls/Dialogs/General/WidthHeightSelector.ascx" TagPrefix="cms"
    TagName="WidthHeightSelector" %>
<%@ Register Src="~/CMSInlineControls/ImageControl.ascx" TagPrefix="cms" TagName="ImagePreview" %>
<asp:Panel runat="server" ID="pnlEmpty" Visible="true" CssClass="DialogInfoArea">
    <asp:Label runat="server" ID="lblEmpty" EnableViewState="false" />
</asp:Panel>
<cms:JQueryTabContainer ID="pnlTabs" runat="server" CssClass="DialogElementHidden">
    <cms:JQueryTab ID="tabImageGeneral" runat="server">
        <ContentTemplate>
            <asp:Panel runat="server" ID="pnlGeneralTab" CssClass="ImageGeneralTab">
                <table style="width: 100%;">
                    <tr>
                        <td style="white-space: nowrap;">
                            <cms:LocalizedLabel ID="lblUrl" runat="server" EnableViewState="false" DisplayColon="true"
                                ResourceString="dialogs.permanenturl" />&nbsp;
                        </td>
                        <td style="width: 100%;">
                            <cms:LocalizedLabel ID="lblUrlText" runat="server" DisplayColon="false" />
                        </td>
                    </tr>
                    <tr>
                        <td style="white-space: nowrap;">
                            <cms:LocalizedLabel ID="lblName" runat="server" EnableViewState="false" DisplayColon="true"
                                ResourceString="general.name" />&nbsp;
                        </td>
                        <td style="width: 100%;">
                            <cms:LocalizedLabel ID="lblNameText" runat="server" DisplayColon="false" />
                        </td>
                    </tr>
                    <asp:PlaceHolder ID="plcSizeArea" runat="server">
                        <tr>
                            <td style="white-space: nowrap;">
                                <cms:LocalizedLabel ID="lblSize" runat="server" EnableViewState="false" DisplayColon="true"
                                    ResourceString="general.size" />&nbsp;
                            </td>
                            <td style="width: 100%;">
                                <cms:LocalizedLabel ID="lblSizeText" runat="server" DisplayColon="false" />
                            </td>
                        </tr>
                    </asp:PlaceHolder>
                    <asp:PlaceHolder ID="plcImagePreviewArea" runat="server">
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td class="DialogGuidParams">
                                <a id="aFullSize" runat="server" enableviewstate="false">
                                    <cms:ImagePreview ID="imagePreview" runat="server" />
                                </a>
                            </td>
                        </tr>
                    </asp:PlaceHolder>
                </table>
            </asp:Panel>
        </ContentTemplate>
    </cms:JQueryTab>
</cms:JQueryTabContainer>
