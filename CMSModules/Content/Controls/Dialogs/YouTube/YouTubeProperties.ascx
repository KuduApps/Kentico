<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_Content_Controls_Dialogs_YouTube_YouTubeProperties" CodeFile="YouTubeProperties.ascx.cs" %>    
    
<%@ Register Src="~/CMSModules/Content/Controls/Dialogs/General/WidthHeightSelector.ascx" TagPrefix="cms"
    TagName="WidthHeightSelector" %>
<%@ Register Src="~/CMSModules/Content/Controls/Dialogs/YouTube/YouTubeColors.ascx" TagPrefix="cms"
    TagName="YouTubeColors" %>
<%@ Register Src="~/CMSModules/Content/Controls/Dialogs/YouTube/YouTubeSizes.ascx" TagPrefix="cms"
    TagName="YouTubeSizes" %>
<%@ Register Src="~/CMSInlineControls/YouTubeControl.ascx" TagPrefix="cms" TagName="YouTubeControl" %>

<cms:CMSUpdatePanel runat="server" ID="pnlUpdate" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:Panel ID="pnlYouTube" runat="server" DefaultButton="imgRefresh">
            <div class="YouTubeProperties">
                <table width="100%" style="vertical-align: top;">
                    <tr>
                        <td class="DialogLabel">
                            <cms:LocalizedLabel ID="lblUrl" runat="server" EnableViewState="false" ResourceString="dialogs.link.url"
                                DisplayColon="true" />
                        </td>
                        <td colspan="2" style="width: 100%;">
                            <cms:CMSUpdatePanel ID="pnlUpdateYouTubeUrl" runat="server" UpdateMode="Always">
                                <ContentTemplate>
                                    <cms:CMSTextBox runat="server" ID="txtLinkText" CssClass="YouTubeUrl" />
                                    <span style="margin-left: 5px; vertical-align: middle;">
                                        <asp:ImageButton ID="imgRefresh" runat="server" OnClick="imgRefresh_Click" EnableViewState="false" /></span>
                                </ContentTemplate>
                            </cms:CMSUpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="DialogLabel">
                            <cms:LocalizedLabel ID="lblFullScreen" runat="server" EnableViewState="false" ResourceString="dialogs.youtube.fullscreen"
                                DisplayColon="true" />
                        </td>
                        <td>
                            <asp:CheckBox ID="chkFullScreen" runat="server" Checked="true" />
                        </td>
                        <td class="YouTubePreview" rowspan="15">
                            <div class="YouTubeBox">
                                <div class="YouTubePreviewBox">
                                    <cms:CMSUpdatePanel ID="pnlYouTubePreview" runat="server">
                                        <ContentTemplate>
                                            <cms:YouTubeControl runat="server" ID="previewElem" />
                                            <cms:CMSButton ID="btnDefaultSizesHidden" runat="server" CssClass="HiddenButton" />
                                            <cms:CMSButton ID="btnDefaultColorsHidden" runat="server" CssClass="HiddenButton" />
                                            <cms:CMSButton ID="btnHiddenPreview" runat="server" CssClass="HiddenButton" />
                                            <cms:CMSButton ID="btnHiddenInsert" runat="server" CssClass="HiddenButton" />
                                            <cms:CMSButton ID="btnHiddenSizeRefresh" runat="server" CssClass="HiddenButton" />
                                        </ContentTemplate>
                                    </cms:CMSUpdatePanel>
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="DialogLabel">
                            <cms:LocalizedLabel ID="lblAutoplay" runat="server" EnableViewState="false" ResourceString="dialogs.vid.autoplay"
                                DisplayColon="true" />
                        </td>
                        <td>
                            <asp:CheckBox ID="chkAutoplay" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="DialogLabel">
                            <cms:LocalizedLabel ID="lblLoop" runat="server" EnableViewState="false" ResourceString="dialogs.vid.loop"
                                DisplayColon="true" />
                        </td>
                        <td>
                            <asp:CheckBox ID="chkLoop" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="DialogLabel">
                            <cms:LocalizedLabel ID="lblIncludeRelated" runat="server" EnableViewState="false"
                                ResourceString="dialogs.youtube.related" DisplayColon="true" />
                        </td>
                        <td>
                            <asp:CheckBox ID="chkIncludeRelated" runat="server" Checked="true" />
                        </td>
                    </tr>
                    <tr>
                        <td class="DialogLabel">
                            <cms:LocalizedLabel ID="lblEnableDelayed" runat="server" EnableViewState="false"
                                ResourceString="dialogs.youtube.delayed" DisplayColon="true" />
                        </td>
                        <td>
                            <asp:CheckBox ID="chkEnableDelayed" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="DialogLabel">
                            <cms:LocalizedLabel ID="lblPlayInHD" runat="server" EnableViewState="false"
                                ResourceString="dialogs.youtube.playinhd" DisplayColon="true" />
                        </td>
                        <td>
                            <asp:CheckBox ID="chkPlayInHD" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="DialogLabel">
                            <cms:LocalizedLabel ID="lblDefaultColor" runat="server" EnableViewState="false" ResourceString="dialogs.youtube.defaultcolor"
                                DisplayColon="true" />
                        </td>
                        <td>
                            <cms:YouTubeColors ID="youTubeColors" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="DialogLabel">
                            <cms:LocalizedLabel ID="lblColor1" runat="server" EnableViewState="false" ResourceString="dialogs.youtube.color1"
                                DisplayColon="true" />
                        </td>
                        <td>
                            <cms:CMSUpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                                <ContentTemplate>
                                    <cms:ColorPicker ID="colorElem1" runat="server" AutoPostback="true" />
                                </ContentTemplate>
                            </cms:CMSUpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td class="DialogLabel">
                            <cms:LocalizedLabel ID="lblColor2" runat="server" EnableViewState="false" ResourceString="dialogs.youtube.color2"
                                DisplayColon="true" />
                        </td>
                        <td>
                            <cms:CMSUpdatePanel ID="pnlUpdateColor2" runat="server" UpdateMode="Always">
                                <ContentTemplate>
                                    <cms:ColorPicker ID="colorElem2" runat="server" AutoPostback="true" />
                                </ContentTemplate>
                            </cms:CMSUpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="DialogLabel">
                            <cms:LocalizedLabel ID="lblDefaultSize" runat="server" EnableViewState="false" ResourceString="dialogs.youtube.defaultsize"
                                DisplayColon="true" />
                        </td>
                        <td>
                            <cms:YouTubeSizes ID="youTubeSizes" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="DialogLabel">
                            <cms:LocalizedLabel ID="lblWidthHeight" runat="server" EnableViewState="false" ResourceString="dialogs.youtube.width"
                                DisplayColon="true" />
                        </td>
                        <td rowspan="2">
                            <cms:CMSUpdatePanel ID="pnlYouTubeWidthHeight" runat="server" UpdateMode="Always">
                                <ContentTemplate>
                                    <cms:WidthHeightSelector ID="sizeElem" runat="server" ShowLabels="false" />
                                </ContentTemplate>
                            </cms:CMSUpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td class="DialogLabel">
                            <cms:LocalizedLabel ID="LocalizedLabel1" runat="server" EnableViewState="false" ResourceString="dialogs.youtube.height"
                                DisplayColon="true" />
                        </td>
                    </tr>
                    <tr>
                        <td class="DialogLabel">
                            <cms:LocalizedLabel ID="lblShowBorder" runat="server" EnableViewState="false" ResourceString="dialogs.youtube.showborder"
                                DisplayColon="true" />
                        </td>
                        <td>
                            <asp:CheckBox ID="chkShowBorder" runat="server" AutoPostBack="true" />
                        </td>
                    </tr>
                </table>
            </div>
            <asp:Literal ID="ltlScript" runat="server" EnableViewState="false" />
        </asp:Panel>
    </ContentTemplate>
</cms:CMSUpdatePanel>
