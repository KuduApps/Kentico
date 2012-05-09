<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_Content_Controls_Dialogs_Properties_BBMediaProperties" CodeFile="BBMediaProperties.ascx.cs" %>
<%@ Register Src="~/CMSModules/Content/Controls/Dialogs/General/WidthHeightSelector.ascx" TagPrefix="cms"
    TagName="WidthHeightSelector" %>
<%@ Register Src="~/CMSInlineControls/ImageControl.ascx" TagPrefix="cms" TagName="ImagePreview" %>
<asp:Panel runat="server" ID="pnlEmpty" Visible="true" CssClass="DialogInfoArea">
    <asp:Label runat="server" ID="lblEmpty" EnableViewState="false" />
</asp:Panel>
<cms:JQueryTabContainer ID="pnlTabs" runat="server" CssClass="DialogElementHidden">
    <cms:JQueryTab ID="tabImageGeneral" runat="server">
        <ContentTemplate>
            <asp:Panel runat="server" ID="pnlGeneralTab" CssClass="ImageGeneralTab" DefaultButton="imgRefresh">
                <table style="vertical-align: top; width: 100%">
                    <asp:PlaceHolder ID="plcUrlBox" runat="server">
                        <tr>
                            <td style="white-space: nowrap;">
                                <cms:LocalizedLabel ID="lblUrl" runat="server" EnableViewState="false" DisplayColon="true"
                                    ResourceString="general.url" />&nbsp;
                            </td>
                            <td style="width: 100%;" colspan="2">
                                <div style="width: 100%;">
                                    <cms:CMSUpdatePanel ID="pnlUpdateImgUrl" runat="server" UpdateMode="Always">
                                        <ContentTemplate>
                                            <cms:CMSTextBox ID="txtUrl" runat="server" CssClass="DialogItemUrlBox" />
                                            <asp:ImageButton ID="imgRefresh" runat="server" OnClick="imgRefresh_Click" CssClass="DialogItemUrlRefresh" EnableViewState="false" />
                                        </ContentTemplate>
                                    </cms:CMSUpdatePanel>
                                </div>
                            </td>
                        </tr>
                    </asp:PlaceHolder>
                    <tr>
                        <td>
                            <cms:LocalizedLabel ID="lblWidth" runat="server" DisplayColon="true" ResourceString="general.width"
                                EnableViewState="false" />
                        </td>
                        <td rowspan="2" style="white-space:nowrap;">
                            <cms:CMSUpdatePanel ID="pnlUpdateWidthHeight" runat="server" UpdateMode="Always">
                                <ContentTemplate>
                                    <cms:WidthHeightSelector ID="widthHeightElem" runat="server" ShowActions="false"
                                        ShowLabels="false" Locked="false" />
                                </ContentTemplate>
                            </cms:CMSUpdatePanel>
                        </td>
                        <td rowspan="2" style="width:100%; vertical-align:top;">
                            <cms:CMSUpdatePanel ID="pnlImgPreview" runat="server">
                                <ContentTemplate>
                                    <div class="DialogPropertiesPreview DialogLongPreview">
                                        <cms:ImagePreview ID="imagePreview" runat="server" />
                                        Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque turpis lacus,
                                        convallis dignissim, consectetur vel, rutrum non, risus. Integer non risus et diam
                                        ultrices sollicitudin. Aliquam faucibus imperdiet massa. Vivamus eros. Cras eu dolor.
                                        Duis lacinia purus at massa. Praesent ornare nisl ac odio. Integer eget metus. Sed
                                        porttitor. Aliquam erat volutpat.
                                        <cms:CMSButton ID="btnHidden" runat="server" CssClass="HiddenButton" EnableViewState="false" />
                                        <cms:CMSButton ID="btnTxtHidden" runat="server" CssClass="HiddenButton" EnableViewState="false" />
                                        <cms:CMSButton ID="btnHiddenSize" runat="server" CssClass="HiddenButton" EnableViewState="false" />
                                    </div>
                                </ContentTemplate>
                            </cms:CMSUpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <cms:LocalizedLabel ID="lblHeight" runat="server" DisplayColon="true" ResourceString="general.height"
                                EnableViewState="false" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
    </cms:JQueryTab>
</cms:JQueryTabContainer>
