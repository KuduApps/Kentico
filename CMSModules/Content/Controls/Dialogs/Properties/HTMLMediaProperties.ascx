<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_Content_Controls_Dialogs_Properties_HTMLMediaProperties" CodeFile="HTMLMediaProperties.ascx.cs" %>
<%@ Register Src="~/CMSModules/Content/Controls/Dialogs/General/WidthHeightSelector.ascx"
    TagPrefix="cms" TagName="WidthHeightSelector" %>
<%@ Register Src="~/CMSInlineControls/MediaControl.ascx" TagPrefix="cms" TagName="MediaPreview" %>
<%@ Register Src="~/CMSInlineControls/ImageControl.ascx" TagPrefix="cms" TagName="ImagePreview" %>
<asp:Panel runat="server" ID="pnlEmpty" Visible="true" CssClass="DialogInfoArea">
    <asp:Label runat="server" ID="lblEmpty" EnableViewState="false" />
</asp:Panel>
<cms:JQueryTabContainer ID="pnlTabs" runat="server" CssClass="DialogElementHidden">
    <cms:JQueryTab ID="tabImageGeneral" runat="server">
        <ContentTemplate>
            <div class="ImageGeneralTab">
                <table style="vertical-align: top;" width="100%">
                    <asp:PlaceHolder ID="plcUrlTxt" runat="server">
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
                                            <asp:ImageButton ID="imgRefresh" runat="server" CssClass="DialogItemUrlRefresh" EnableViewState="false" />
                                        </ContentTemplate>
                                    </cms:CMSUpdatePanel>
                                </div>
                            </td>
                        </tr>
                    </asp:PlaceHolder>
                    <tr>
                        <td style="white-space: nowrap;">
                            <cms:LocalizedLabel ID="lblAlt" runat="server" EnableViewState="false" DisplayColon="true"
                                ResourceString="dialogs.image.altlabel" />&nbsp;
                        </td>
                        <td>
                            <cms:CMSTextBox ID="txtAlt" runat="server" CssClass="LongTextBox" />
                        </td>
                        <td style="width: 100%; vertical-align: top" rowspan="8">
                            <div class="DialogPropertiesPreview">
                                <cms:CMSUpdatePanel ID="pnlImgPreview" runat="server">
                                    <ContentTemplate>
                                        <cms:ImagePreview ID="imagePreview" runat="server" />
                                        Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque turpis lacus,
                                        convallis dignissim, consectetur vel, rutrum non, risus. Integer non risus et diam
                                        ultrices sollicitudin. Aliquam faucibus imperdiet massa. Vivamus eros. Cras eu dolor.
                                        Duis lacinia purus at massa. Praesent ornare nisl ac odio. Integer eget metus. Sed
                                        porttitor. Aliquam erat volutpat.
                                        <cms:CMSButton ID="btnImagePreview" CssClass="HiddenButton" runat="server" EnableViewState="false" />
                                        <cms:CMSButton ID="btnImageTxtPreview" CssClass="HiddenButton" runat="server" EnableViewState="false" />
                                    </ContentTemplate>
                                </cms:CMSUpdatePanel>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="white-space: nowrap;">
                            <cms:LocalizedLabel ID="lblWidthHeight" runat="server" EnableViewState="false" DisplayColon="true"
                                ResourceString="dialogs.image.width" />
                        </td>
                        <td rowspan="2">
                            <cms:CMSUpdatePanel ID="pnlUpdateWidthHeight" runat="server" UpdateMode="Always">
                                <ContentTemplate>
                                    <cms:WidthHeightSelector ID="widthHeightElem" runat="server" ShowLabels="false" />
                                </ContentTemplate>
                            </cms:CMSUpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td style="white-space: nowrap;">
                            <cms:LocalizedLabel ID="LocalizedLabel1" runat="server" EnableViewState="false" DisplayColon="true"
                                ResourceString="dialogs.image.height" />
                        </td>
                    </tr>
                    <tr>
                        <td style="white-space: nowrap;">
                            <cms:LocalizedLabel ID="lblBorderWidth" runat="server" EnableViewState="false" DisplayColon="true"
                                ResourceString="dialogs.image.borderwidthlabel" />
                        </td>
                        <td>
                            <cms:CMSTextBox ID="txtBorderWidth" runat="server" CssClass="ShortTextBox" />
                        </td>
                    </tr>
                    <tr>
                        <td style="white-space: nowrap;">
                            <cms:LocalizedLabel ID="lblColor" runat="server" EnableViewState="false" DisplayColon="true"
                                ResourceString="dialogs.image.bordercolorlabel" />
                        </td>
                        <td>
                            <cms:CMSUpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                                <ContentTemplate>
                                    <cms:ColorPicker ID="colorElem" runat="server" AllowOnInitInitialization="true" />
                                </ContentTemplate>
                            </cms:CMSUpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td style="white-space: nowrap;">
                            <cms:LocalizedLabel ID="lblHSpace" runat="server" EnableViewState="false" DisplayColon="true"
                                ResourceString="dialogs.image.hspacelabel" />
                        </td>
                        <td>
                            <cms:CMSTextBox ID="txtHSpace" runat="server" CssClass="ShortTextBox" />
                        </td>
                    </tr>
                    <tr>
                        <td style="white-space: nowrap;">
                            <cms:LocalizedLabel ID="lblVSpace" runat="server" EnableViewState="false" DisplayColon="true"
                                ResourceString="dialogs.image.vspacelabel" />
                        </td>
                        <td>
                            <cms:CMSTextBox ID="txtVSpace" runat="server" CssClass="ShortTextBox" />
                        </td>
                    </tr>
                    <tr>
                        <td style="white-space: nowrap;">
                            <cms:LocalizedLabel ID="lblAlign" runat="server" EnableViewState="false" DisplayColon="true"
                                ResourceString="dialogs.image.alignlabel" />
                        </td>
                        <td>
                            <asp:DropDownList ID="drpAlign" runat="server" Width="105" />
                        </td>
                    </tr>
                </table>
            </div>
            <asp:HiddenField ID="hdnUpdateItemUrl" runat="server" />
        </ContentTemplate>
    </cms:JQueryTab>
    <cms:JQueryTab ID="tabImageLink" runat="server">
        <ContentTemplate>
            <div class="ImageLinkTab">
                <table style="vertical-align: top;">
                    <tr id="rowLinkUrlInfo">
                        <td colspan="2">
                            <cms:LocalizedLabel ID="lblLinkInfo" runat="server" EnableViewState="false" ResourceString="dialogs.link.url.info" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <cms:LocalizedLabel ID="lblLinkUrl" runat="server" EnableViewState="false" ResourceString="dialogs.link.url"
                                DisplayColon="true" />
                        </td>
                        <td>
                            <cms:CMSTextBox runat="server" ID="txtLinkUrl" CssClass="VeryLongTextBox" />
                            <cms:LocalizedButton runat="server" ID="btnLinkBrowseServer" EnableViewState="false"
                                ResourceString="dialogs.link.browseserver" CssClass="LongButton" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <cms:LocalizedLabel ID="lblLinkTarget" runat="server" EnableViewState="false" ResourceString="dialogs.link.targetname"
                                DisplayColon="true" />
                        </td>
                        <td>
                            <asp:DropDownList ID="drpLinkTarget" runat="server" Width="200" />
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </cms:JQueryTab>
    <cms:JQueryTab ID="tabImageAdvanced" runat="server">
        <ContentTemplate>
            <div class="ImageAdvancedTab">
                <table style="vertical-align: top;">
                    <tr>
                        <td>
                            <cms:LocalizedLabel ID="lblImageAdvID" runat="server" EnableViewState="false" ResourceString="dialogs.advanced.id"
                                DisplayColon="true" />
                        </td>
                        <td>
                            <cms:CMSTextBox runat="server" ID="txtImageAdvId" CssClass="LongTextBox" EnableViewState="false" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <cms:LocalizedLabel ID="lblImageAdvTooltip" runat="server" EnableViewState="false"
                                ResourceString="dialogs.advanced.tooltip" DisplayColon="true" />
                        </td>
                        <td>
                            <cms:CMSTextBox runat="server" ID="txtImageAdvTooltip" CssClass="LongTextBox" EnableViewState="false" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <cms:LocalizedLabel ID="lblImageAdvStyleClass" runat="server" EnableViewState="false"
                                ResourceString="dialogs.advanced.class" DisplayColon="true" />
                        </td>
                        <td>
                            <cms:CMSTextBox runat="server" ID="txtImageAdvClass" CssClass="LongTextBox" EnableViewState="false" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <cms:LocalizedLabel ID="lblImageAdvStyle" runat="server" EnableViewState="false"
                                ResourceString="dialogs.advanced.style" DisplayColon="true" />
                        </td>
                        <td>
                            <cms:CMSTextBox runat="server" ID="txtImageAdvStyle" CssClass="TextAreaField" TextMode="MultiLine"
                                EnableViewState="false" />
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </cms:JQueryTab>
    <cms:JQueryTab ID="tabImageBehavior" runat="server">
        <ContentTemplate>
            <div class="ImageBehaviorTab">
                <table>
                    <tr>
                        <td colspan="2">
                            <cms:LocalizedRadioButton ID="radImageNone" runat="server" ResourceString="dialogs.image.behaviornone"
                                GroupName="imgBehavior" Height="20" Checked="true" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <cms:LocalizedRadioButton ID="radImageSame" runat="server" ResourceString="dialogs.image.behaviorsame"
                                GroupName="imgBehavior" Height="20" />
                        </td>
                        <td style="vertical-align: top;">
                            <asp:Panel runat="server" ID="pnlRemoveLink">
                                &nbsp;(<cms:LocalizedLinkButton ID="btnRemoveLink" runat="server" ResourceString="dialogs.behavior.removelink" />&nbsp;<cms:LocalizedLabel
                                    ID="lblRemoveLinkText" runat="server" EnableViewState="false" ResourceString="dialogs.behavior.removelinktext" />)</asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <cms:LocalizedRadioButton ID="radImageNew" runat="server" ResourceString="dialogs.image.behaviornew"
                                GroupName="imgBehavior" Height="20" />
                        </td>
                        <td style="vertical-align: top;">
                            <asp:Panel runat="server" ID="pnlRemoveLink2">
                                &nbsp;(<cms:LocalizedLinkButton ID="btnRemoveLink2" runat="server" ResourceString="dialogs.behavior.removelink" />&nbsp;<cms:LocalizedLabel
                                    ID="lblRemoveLinkText2" runat="server" EnableViewState="false" ResourceString="dialogs.behavior.removelinktext" />)</asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <cms:LocalizedRadioButton ID="radImageLarger" runat="server" ResourceString="dialogs.image.behaviorlarger"
                                GroupName="imgBehavior" Height="20" />
                        </td>
                    </tr>
                </table>
                <div style="padding-left: 27px; padding-top: 5px;">
                    <cms:WidthHeightSelector ID="imgWidthHeightElem" runat="server" VerticalLayout="false" />
                </div>
            </div>
        </ContentTemplate>
    </cms:JQueryTab>
    <cms:JQueryTab ID="tabFlashGeneral" runat="server">
        <ContentTemplate>
            <div class="FlashGeneralTab">
                <table style="vertical-align: top;" width="100%">
                    <asp:PlaceHolder ID="plcFlashUrl" runat="server">
                        <tr>
                            <td style="white-space: nowrap;">
                                <cms:LocalizedLabel ID="lblFlashUrl" runat="server" EnableViewState="false" DisplayColon="true"
                                    ResourceString="general.url" />&nbsp;
                            </td>
                            <td style="width: 100%;" colspan="2">
                                <cms:CMSUpdatePanel ID="pnlFlashUrl" runat="server" UpdateMode="Always">
                                    <ContentTemplate>
                                        <cms:CMSTextBox ID="txtFlashUrl" runat="server" CssClass="DialogItemUrlBox" />
                                        <asp:ImageButton ID="imgFlashRefresh" runat="server" CssClass="DialogItemUrlRefresh"
                                            EnableViewState="false" />
                                    </ContentTemplate>
                                </cms:CMSUpdatePanel>
                            </td>
                        </tr>
                    </asp:PlaceHolder>
                    <tr>
                        <td style="white-space: nowrap;">
                            <cms:LocalizedLabel ID="lblFlashWidth" runat="server" ResourceString="general.width"
                                EnableViewState="false" DisplayColon="true" />
                        </td>
                        <td rowspan="2" style="white-space: nowrap;">
                            <cms:CMSUpdatePanel ID="pnlFlashWidthHeight" runat="server">
                                <ContentTemplate>
                                    <cms:WidthHeightSelector ID="flashWidthHeightElem" runat="server" ShowLabels="false"
                                        Locked="false" />
                                </ContentTemplate>
                            </cms:CMSUpdatePanel>
                        </td>
                        <td style="width: 100%; vertical-align: top;" rowspan="4">
                            <div class="DialogPropertiesPreview DialogMediaPreview">
                                <cms:CMSUpdatePanel ID="pnlFlashPreview" runat="server">
                                    <ContentTemplate>
                                        <cms:CMSButton ID="btnFlashPreview" CssClass="HiddenButton" runat="server" EnableViewState="false" />
                                        <cms:MediaPreview ID="flashPreview" runat="server" />
                                    </ContentTemplate>
                                </cms:CMSUpdatePanel>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="white-space: nowrap;">
                            <cms:LocalizedLabel ID="lblFlashHeight" runat="server" ResourceString="general.height"
                                EnableViewState="false" DisplayColon="true" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="white-space: nowrap;">
                            <cms:LocalizedLabel ID="lblFlashAutoplay" runat="server" ResourceString="dialogs.vid.autoplay"
                                EnableViewState="false" DisplayColon="true" />
                        </td>
                        <td>
                            <cms:LocalizedCheckBox ID="chkFlashAutoplay" runat="server" Checked="true" EnableViewState="false" />
                        </td>
                    </tr>
                    <tr>
                        <td style="white-space: nowrap;">
                            <cms:LocalizedLabel ID="lblFlashLoop" runat="server" ResourceString="dialogs.vid.loop"
                                EnableViewState="false" DisplayColon="true" />
                        </td>
                        <td>
                            <cms:LocalizedCheckBox ID="chkFlashLoop" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td style="white-space: nowrap;">
                            <cms:LocalizedLabel ID="lblFlashEnableMenu" runat="server" ResourceString="dialogs.flash.enablemenu"
                                EnableViewState="false" DisplayColon="true" />
                        </td>
                        <td>
                            <cms:LocalizedCheckBox ID="chkFlashEnableMenu" runat="server" />
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </cms:JQueryTab>
    <cms:JQueryTab ID="tabFlashAdvanced" runat="server">
        <ContentTemplate>
            <div class="FlashAdvancedTab">
                <table style="vertical-align: top;">
                    <tr>
                        <td style="white-space: nowrap;">
                            <cms:LocalizedLabel ID="lblFlashScale" runat="server" EnableViewState="false" ResourceString="dialogs.flash.scale"
                                DisplayColon="true" />
                        </td>
                        <td>
                            <asp:DropDownList ID="drpFlashScale" runat="server" Width="205" />
                        </td>
                    </tr>
                    <tr>
                        <td style="white-space: nowrap;">
                            <cms:LocalizedLabel ID="lblFlashId" runat="server" EnableViewState="false" ResourceString="dialogs.advanced.id"
                                DisplayColon="true" />
                        </td>
                        <td>
                            <cms:CMSTextBox runat="server" ID="txtFlashId" CssClass="LongTextBox" EnableViewState="false" />
                        </td>
                    </tr>
                    <tr>
                        <td style="white-space: nowrap;">
                            <cms:LocalizedLabel ID="lblFlashTitle" runat="server" EnableViewState="false" ResourceString="dialogs.flash.advisorytitle"
                                DisplayColon="true" />
                        </td>
                        <td>
                            <cms:CMSTextBox runat="server" ID="txtFlashTitle" CssClass="LongTextBox" EnableViewState="false" />
                        </td>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="white-space: nowrap;">
                            <cms:LocalizedLabel ID="lblFlashClass" runat="server" EnableViewState="false" ResourceString="dialogs.advanced.class"
                                DisplayColon="true" />
                        </td>
                        <td>
                            <cms:CMSTextBox runat="server" ID="txtFlashClass" CssClass="LongTextBox" EnableViewState="false" />
                        </td>
                    </tr>
                    <tr>
                        <td style="white-space: nowrap;">
                            <cms:LocalizedLabel ID="lblFlasVars" runat="server" EnableViewState="false" ResourceString="dialogs.advanced.flashvars"
                                DisplayColon="true" />
                        </td>
                        <td>
                            <cms:CMSTextBox runat="server" ID="txtFlashVars" CssClass="LongTextBox" EnableViewState="false" />
                        </td>
                    </tr>
                    <tr>
                        <td style="white-space: nowrap;">
                            <cms:LocalizedLabel ID="lblFlashStyle" runat="server" EnableViewState="false" ResourceString="dialogs.advanced.style"
                                DisplayColon="true" />
                        </td>
                        <td>
                            <cms:CMSTextBox runat="server" ID="txtFlashStyle" CssClass="TextAreaField" TextMode="MultiLine"
                                EnableViewState="false" />
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </cms:JQueryTab>
    <cms:JQueryTab ID="tabVideoGeneral" runat="server">
        <ContentTemplate>
            <div class="VideoGeneralTab">
                <table style="vertical-align: top;">
                    <asp:PlaceHolder ID="plcVidUrl" runat="server">
                        <tr>
                            <td style="white-space: nowrap;">
                                <cms:LocalizedLabel ID="lblVidUrl" runat="server" EnableViewState="false" DisplayColon="true"
                                    ResourceString="general.url" />&nbsp;
                            </td>
                            <td style="width: 100%;" colspan="2">
                                <cms:CMSUpdatePanel ID="pnlVidUrl" runat="server" UpdateMode="Always">
                                    <ContentTemplate>
                                        <cms:CMSTextBox ID="txtVidUrl" runat="server" CssClass="DialogItemUrlBox" />
                                        <asp:ImageButton ID="imgVidRefresh" runat="server" CssClass="DialogItemUrlRefresh"
                                            EnableViewState="false" />
                                    </ContentTemplate>
                                </cms:CMSUpdatePanel>
                            </td>
                        </tr>
                    </asp:PlaceHolder>
                    <tr>
                        <td style="white-space: nowrap;">
                            <cms:LocalizedLabel ID="lblAVWidth" runat="server" ResourceString="general.width"
                                EnableViewState="false" DisplayColon="true" />
                        </td>
                        <td style="white-space: nowrap;" rowspan="2">
                            <cms:CMSUpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
                                <ContentTemplate>
                                    <cms:WidthHeightSelector ID="vidWidthHeightElem" runat="server" ShowLabels="false"
                                        Locked="false" />
                                </ContentTemplate>
                            </cms:CMSUpdatePanel>
                        </td>
                        <td style="width: 100%; vertical-align: top;" rowspan="4">
                            <div class="DialogPropertiesPreview DialogMediaPreview">
                                <cms:CMSUpdatePanel ID="pnlVidPreview" runat="server">
                                    <ContentTemplate>
                                        <cms:CMSButton ID="btnVideoPreview" CssClass="HiddenButton" runat="server" EnableViewState="false" />
                                        <cms:MediaPreview ID="videoPreview" runat="server" />
                                    </ContentTemplate>
                                </cms:CMSUpdatePanel>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="white-space: nowrap;">
                            <cms:LocalizedLabel ID="lblAVHeight" runat="server" ResourceString="general.height"
                                EnableViewState="false" DisplayColon="true" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="white-space: nowrap;">
                            <cms:LocalizedLabel ID="lblVidAutoPlay" runat="server" ResourceString="dialogs.vid.autoplay"
                                EnableViewState="false" DisplayColon="true" />
                        </td>
                        <td>
                            <cms:LocalizedCheckBox ID="chkVidAutoPlay" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td style="white-space: nowrap;">
                            <cms:LocalizedLabel ID="lblVidLoop" runat="server" ResourceString="dialogs.vid.loop"
                                EnableViewState="false" DisplayColon="true" />
                        </td>
                        <td>
                            <cms:LocalizedCheckBox ID="chkVidLoop" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td style="white-space: nowrap;">
                            <cms:LocalizedLabel ID="lblVidShowControls" runat="server" ResourceString="dialogs.vid.showcontrols"
                                EnableViewState="false" DisplayColon="true" />
                        </td>
                        <td>
                            <cms:LocalizedCheckBox ID="chkVidShowControls" runat="server" Checked="true" EnableViewState="false" />
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </cms:JQueryTab>
</cms:JQueryTabContainer>
<cms:CMSButton ID="btnSizeRefreshHidden" runat="server" CssClass="HiddenButton" EnableViewState="false" />
<cms:CMSButton ID="btnBehaviorSizeRefreshHidden" runat="server" CssClass="HiddenButton"
    EnableViewState="false" />
