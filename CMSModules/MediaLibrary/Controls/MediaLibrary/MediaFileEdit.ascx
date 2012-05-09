<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_MediaLibrary_Controls_MediaLibrary_MediaFileEdit"
    CodeFile="MediaFileEdit.ascx.cs" %>
<%@ Register Src="~/CMSInlineControls/MediaControl.ascx" TagPrefix="cms" TagName="MediaPreview" %>
<%@ Register Src="~/CMSInlineControls/ImageControl.ascx" TagPrefix="cms" TagName="ImagePreview" %>
<%@ Register Src="~/CMSModules/MediaLibrary/Controls/MediaLibrary/MediaFileUpload.ascx"
    TagName="FileUpload" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/Objects/Controls/ObjectVersionList.ascx" TagPrefix="cms"
    TagName="VersionList" %>
<%@ Register Src="~/CMSFormControls/System/LocalizableTextBox.ascx" TagName="LocalizableTextBox"
    TagPrefix="cms" %>
<cms:JQueryTabContainer ID="pnlTabs" runat="server" CssClass="Dialog_Tabs">
    <cms:JQueryTab ID="tabGeneral" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlTabFile" runat="server">
                <asp:Label ID="lblErrorFile" runat="server" Visible="false" CssClass="ErrorLabel"
                    EnableViewState="false" />
                <asp:Label ID="lblInfoFile" runat="server" Visible="false" EnableViewState="false"
                    CssClass="InfoLabel"></asp:Label>
                <cms:CMSUpdatePanel ID="pnlUpdateGeneral" runat="server" UpdateMode="Conditional"
                    ChildrenAsTriggers="true">
                    <ContentTemplate>
                        <table>
                            <asp:PlaceHolder ID="plcDirPath" runat="server">
                                <tr>
                                    <td style="white-space: nowrap; vertical-align: top;">
                                        <cms:LocalizedLabel ID="lblDirPath" runat="server" EnableViewState="false" DisplayColon="true" />
                                    </td>
                                    <td>
                                        <asp:Literal ID="ltrDirPathValue" runat="server" EnableViewState="false" />
                                    </td>
                                </tr>
                            </asp:PlaceHolder>
                            <tr>
                                <td style="white-space: nowrap; vertical-align: top;">
                                    <asp:Label ID="lblPermaLink" runat="server" EnableViewState="false" />
                                </td>
                                <td>
                                    <asp:Literal ID="ltrPermaLinkValue" runat="server" EnableViewState="false" />
                                </td>
                            </tr>
                            <asp:Panel ID="pnlPrew" runat="server">
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:PlaceHolder ID="plcImagePreview" runat="server" Visible="false">
                                            <cms:ImagePreview ID="imagePreview" runat="server" />
                                        </asp:PlaceHolder>
                                        <asp:PlaceHolder ID="plcMediaPreview" runat="server" Visible="false">
                                            <cms:MediaPreview ID="mediaPreview" runat="server" />
                                        </asp:PlaceHolder>
                                    </td>
                                </tr>
                            </asp:Panel>
                        </table>
                    </ContentTemplate>
                </cms:CMSUpdatePanel>
            </asp:Panel>
        </ContentTemplate>
    </cms:JQueryTab>
    <cms:JQueryTab ID="tabPreview" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlTabPreview" runat="server">
                <asp:Label ID="lblErrorPreview" runat="server" Visible="false" CssClass="ErrorLabel"
                    EnableViewState="false" />
                <asp:Label ID="lblInfoPreview" runat="server" Visible="false" EnableViewState="false"
                    CssClass="InfoLabel"></asp:Label>
                <cms:CMSUpdatePanel ID="pnlUpdatePreviewDetails" runat="server" UpdateMode="Conditional"
                    ChildrenAsTriggers="true">
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td style="white-space: nowrap;">
                                    <cms:LocalizedLabel ID="lblEditPreview" runat="server" ResourceString="general.thumbnail"
                                        DisplayColon="true" EnableViewState="false" />
                                </td>
                                <td>
                                    <cms:FileUpload ID="fileUplPreview" runat="server" IsMediaThumbnail="true" StopProcessing="true" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    &nbsp;
                                </td>
                            </tr>
                            <asp:PlaceHolder ID="plcPreview" runat="server" Visible="false">
                                <asp:PlaceHolder ID="plcPrevDirPath" runat="server">
                                    <tr>
                                        <td style="white-space: nowrap; vertical-align: top;">
                                            <cms:LocalizedLabel ID="lblPrevDirectLink" runat="server" ResourceString="media.file.dirpath"
                                                EnableViewState="false" DisplayColon="true"></cms:LocalizedLabel>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblPrevDirectLinkVal" runat="server" EnableViewState="false" />
                                        </td>
                                    </tr>
                                </asp:PlaceHolder>
                                <tr>
                                    <td style="white-space: nowrap; vertical-align: top;">
                                        <cms:LocalizedLabel ID="lblPrevPermaLink" runat="server" ResourceString="media.file.permalink"
                                            EnableViewState="false" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblPreviewPermaLink" runat="server" EnableViewState="false" />
                                    </td>
                                </tr>
                            </asp:PlaceHolder>
                            <asp:PlaceHolder ID="plcNoPreview" runat="server" Visible="false">
                                <tr>
                                    <td colspan="2">
                                        <asp:Label ID="lblNoPreview" runat="server" EnableViewState="false" />
                                    </td>
                                </tr>
                            </asp:PlaceHolder>
                        </table>
                    </ContentTemplate>
                </cms:CMSUpdatePanel>
            </asp:Panel>
        </ContentTemplate>
    </cms:JQueryTab>
    <cms:JQueryTab ID="tabEdit" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlTabEdit" runat="server">
                <cms:CMSUpdatePanel ID="pnlUpdateEditInfo" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Label ID="lblErrorEdit" runat="server" Visible="false" CssClass="ErrorLabel"
                            EnableViewState="false" /><asp:Label ID="lblInfoEdit" runat="server" Visible="false"
                                EnableViewState="false" CssClass="InfoLabel" /></ContentTemplate>
                </cms:CMSUpdatePanel>
                <cms:CMSUpdatePanel ID="pnlUpdateFileInfo" runat="server" ChildrenAsTriggers="false"
                    UpdateMode="Conditional">
                    <ContentTemplate>
                        <table width="100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="width: 50%; vertical-align: top;">
                                    <table>
                                        <tr>
                                            <td>
                                                <cms:LocalizedLabel ID="lblEditName" runat="server" ResourceString="general.filename"
                                                    DisplayColon="true" EnableViewState="false" />
                                            </td>
                                            <td>
                                                <cms:CMSTextBox ID="txtEditName" runat="server" CssClass="TextBoxField" MaxLength="250"
                                                    EnableViewState="false" />
                                                <cms:CMSRequiredFieldValidator ID="rfvEditName" runat="server" ValidationGroup="MediaFileEdit"
                                                    ControlToValidate="txtEditName" Display="Dynamic" EnableViewState="false" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblEditTitle" runat="server" EnableViewState="false" />
                                            </td>
                                            <td>
                                                <cms:LocalizableTextBox ID="txtEditTitle" runat="server" CssClass="TextBoxField"
                                                    MaxLength="250" EnableViewState="false" AutoSave="False" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top;">
                                                <asp:Label ID="lblEditDescription" runat="server" EnableViewState="false" />
                                            </td>
                                            <td>
                                                <cms:LocalizableTextBox ID="txtEditDescription" runat="server" CssClass="TextAreaField"
                                                    TextMode="MultiLine" EnableViewState="false" AutoSave="False" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                <cms:CMSButton ID="btnEdit" runat="server" OnClick="btnEdit_Click" CssClass="SubmitButton"
                                                    ValidationGroup="MediaFileEdit" EnableViewState="false" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="width: 50%; vertical-align: top;">
                                    <table>
                                        <tr>
                                            <td>
                                                <cms:LocalizedLabel ID="lblCreatedBy" runat="server" EnableViewState="false" DisplayColon="true" />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCreatedByVal" runat="server" EnableViewState="false" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <cms:LocalizedLabel ID="lblCreatedWhen" runat="server" EnableViewState="false" DisplayColon="true" />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCreatedWhenVal" runat="server" EnableViewState="false" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="line-height: 5px;">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <cms:LocalizedLabel ID="lblModified" runat="server" EnableViewState="false" DisplayColon="true" />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblModifiedVal" runat="server" EnableViewState="false" />
                                            </td>
                                        </tr>
                                        <asp:PlaceHolder ID="plcFileModified" runat="server" EnableViewState="false" Visible="false">
                                            <tr>
                                                <td>
                                                    <cms:LocalizedLabel ID="lblFileModified" runat="server" EnableViewState="false" DisplayColon="true" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblFileModifiedVal" runat="server" EnableViewState="false" />
                                                </td>
                                            </tr>
                                        </asp:PlaceHolder>
                                        <tr>
                                            <td colspan="2" style="line-height: 5px;">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <cms:LocalizedLabel ID="lblExtension" runat="server" EnableViewState="false" DisplayColon="true" />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblExtensionVal" runat="server" EnableViewState="false" />
                                            </td>
                                        </tr>
                                        <asp:PlaceHolder ID="plcDimensions" runat="server" EnableViewState="false" Visible="false">
                                            <tr>
                                                <td>
                                                    <cms:LocalizedLabel ID="lblDimensions" runat="server" EnableViewState="false" DisplayColon="true" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblDimensionsVal" runat="server" EnableViewState="false" />
                                                </td>
                                            </tr>
                                        </asp:PlaceHolder>
                                        <tr>
                                            <td colspan="2" style="line-height: 5px;">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <cms:LocalizedLabel ID="lblSize" runat="server" EnableViewState="false" DisplayColon="true" />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblSizeVal" runat="server" EnableViewState="false" />
                                            </td>
                                        </tr>
                                        <asp:PlaceHolder ID="plcFileSize" runat="server" EnableViewState="false" Visible="false">
                                            <tr>
                                                <td>
                                                    <cms:LocalizedLabel ID="lblFileSize" runat="server" EnableViewState="false" DisplayColon="true" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblFileSizeVal" runat="server" EnableViewState="false" />
                                                </td>
                                            </tr>
                                        </asp:PlaceHolder>
                                        <asp:PlaceHolder ID="plcRefresh" runat="server" EnableViewState="false" Visible="false">
                                            <tr>
                                                <td colspan="2" style="line-height: 5px;">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <cms:LocalizedButton ID="btnRefresh" runat="server" ResourceString="general.refresh"
                                                        CssClass="ContentButton" OnClick="btnRefresh_Click" />
                                                </td>
                                            </tr>
                                        </asp:PlaceHolder>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </cms:CMSUpdatePanel>
            </asp:Panel>
        </ContentTemplate>
    </cms:JQueryTab>
    <cms:JQueryTab ID="tabCustomFields" CssClass="MediaLibraryCustomTab" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlTabCustomFields" runat="server" CssClass="MediaLibraryCustomTab MediaFileCustomFields">
                <asp:PlaceHolder runat="server" ID="plcMediaFileCustomFields">
                    <cms:CMSUpdatePanel ID="pnlUpdateCustomFields" runat="server" UpdateMode="Conditional"
                        ChildrenAsTriggers="false">
                        <ContentTemplate>
                            <cms:LocalizedLabel ID="lblInfoCustom" runat="server" Visible="false" CssClass="ErrorLabel"
                                EnableViewState="false" ResourceString="general.changessaved"></cms:LocalizedLabel>
                            <cms:LocalizedLabel ID="lblErrorCustom" runat="server" Visible="false" EnableViewState="false"
                                CssClass="ErrorLabel"></cms:LocalizedLabel>
                            <asp:Label ID="lblInfo" runat="server" EnableViewState="false" Visible="false" CssClass="InfoLabel"></asp:Label>
                            <cms:DataForm ID="formMediaFileCustomFields" runat="server" IsLiveSite="false" />
                        </ContentTemplate>
                    </cms:CMSUpdatePanel>
                </asp:PlaceHolder>
            </asp:Panel>
        </ContentTemplate>
    </cms:JQueryTab>
    <cms:JQueryTab ID="tabVersions" runat="server" Visible="True">
        <ContentTemplate>
            <cms:CMSUpdatePanel ID="pnlUpdateVersions" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <cms:VersionList ID="objectVersionList" runat="server" />
                    <asp:Button ID="btnHidden" runat="server" CssClass="HiddenButton" EnableViewState="false"
                        OnClick="btnHidden_Click" />
                </ContentTemplate>
            </cms:CMSUpdatePanel>
        </ContentTemplate>
    </cms:JQueryTab>
</cms:JQueryTabContainer>
