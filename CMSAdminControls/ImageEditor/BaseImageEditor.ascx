<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSAdminControls_ImageEditor_BaseImageEditor"
    CodeFile="BaseImageEditor.ascx.cs" %>
<%@ Register Src="~/CMSAdminControls/ImageEditor/MetaDataEditor.ascx" TagName="MetaDataEditor"
    TagPrefix="cms" %>
<ajaxToolkit:ToolkitScriptManager ID="manScript" runat="server" />
<table id="tblMain" class="ImageEditorTable" cellpadding="0" cellspacing="0">
    <tr>
        <td class="EditorMenuMain">
            <ajaxToolkit:Accordion ID="ajaxAccordion" runat="Server" CssClass="ImageEditorMain"
                ContentCssClass="ImageEditorSub" HeaderCssClass="MenuHeaderItem" HeaderSelectedCssClass="MenuHeaderItemSelected">
                <Panes>
                    <ajaxToolkit:AccordionPane ID="pnlAccordion1" runat="server">
                        <Header>
                            <div class="HeaderInner">
                                <cms:LocalizedLabel ID="lblResize" runat="server" EnableViewState="false" ResourceString="img.resize" />
                            </div>
                        </Header>
                        <Content>
                            <cms:LocalizedLabel ID="lblValidationFailedResize" runat="server" EnableViewState="false"
                                CssClass="ErrorLabel" ResourceString="img.errors.resize" />
                            <cms:CMSUpdatePanel ID="pnlAjax" runat="server" EnableViewState="false" UpdateMode="Always">
                                <ContentTemplate>
                                    <table class="ImageEditorTable">
                                        <tr>
                                            <td class="LabelResize">
                                                <cms:LocalizedRadioButton ID="radByPercentage" runat="server" GroupName="Resize"
                                                    Checked="true" AutoPostBack="true" />
                                            </td>
                                            <td>
                                                <cms:CMSTextBox ID="txtResizePercent" runat="server" AutoPostBack="true" CssClass="ImageEditorTextBox"
                                                    MaxLength="3" />
                                                %
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <cms:LocalizedRadioButton ID="radByAbsolute" runat="server" GroupName="Resize" AutoPostBack="true" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <cms:LocalizedLabel ID="lblResizeWidth" runat="server" EnableViewState="false" ResourceString="img.width"
                                                    DisplayColon="true" />
                                            </td>
                                            <td>
                                                <cms:CMSTextBox ID="txtResizeWidth" runat="server" Enabled="false" CssClass="ImageEditorTextBox"
                                                    MaxLength="4" />
                                                px
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <cms:LocalizedLabel ID="lblResizeHeight" runat="server" EnableViewState="false" ResourceString="img.height"
                                                    DisplayColon="true" />
                                            </td>
                                            <td>
                                                <cms:CMSTextBox ID="txtResizeHeight" runat="server" Enabled="false" CssClass="ImageEditorTextBox"
                                                    MaxLength="4" />
                                                px
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <cms:LocalizedCheckBox ID="chkMaintainRatio" runat="server" AutoPostBack="true" OnCheckedChanged="chkMaintainRatioChanged"
                                                    Enabled="false" Checked="true" />
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </cms:CMSUpdatePanel>
                            <table class="ImageEditorTable">
                                <tr>
                                    <tr>
                                        <td class="LabelResize">
                                        </td>
                                        <td>
                                            <cms:LocalizedButton ID="btnResize" runat="server" OnClick="btnResizeClick" CssClass="SubmitButton"
                                                EnableViewState="false" ResourceString="general.ok" />
                                        </td>
                                    </tr>
                            </table>
                        </Content>
                    </ajaxToolkit:AccordionPane>
                    <ajaxToolkit:AccordionPane ID="pnlAccordion2" runat="server">
                        <Header>
                            <div class="HeaderInner">
                                <cms:LocalizedLabel ID="lblRotation" runat="server" EnableViewState="false" ResourceString="img.rotation" />
                            </div>
                        </Header>
                        <Content>
                            <table class="ImageEditorTable">
                                <tr>
                                    <td class="Image">
                                        <asp:ImageButton ID="btnRotate90Left" runat="server" OnClick="btnRotate90LeftClick"
                                            EnableViewState="false" />
                                    </td>
                                    <td>
                                        <cms:LocalizedLinkButton ID="lblRotate90Left" runat="server" EnableViewState="false"
                                            OnClick="btnRotate90LeftClick" ResourceString="img.rotate90left" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" class="Divider">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Image">
                                        <asp:ImageButton ID="btnRotate90Right" runat="server" OnClick="btnRotate90RightClick"
                                            EnableViewState="false" />
                                    </td>
                                    <td>
                                        <cms:LocalizedLinkButton ID="lblRotate90Right" runat="server" EnableViewState="false"
                                            OnClick="btnRotate90RightClick" ResourceString="img.rotate90right" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" class="Divider">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Image">
                                        <asp:ImageButton ID="btnFlipHorizontal" runat="server" OnClick="btnFlipHorizontalClick"
                                            EnableViewState="false" />
                                    </td>
                                    <td>
                                        <cms:LocalizedLinkButton ID="lblFlipHorizontal" runat="server" EnableViewState="false"
                                            OnClick="btnFlipHorizontalClick" ResourceString="img.fliphorizontal" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" class="Divider">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Image">
                                        <asp:ImageButton ID="btnFlipVertical" runat="server" OnClick="btnFlipVerticalClick"
                                            EnableViewState="false" />
                                    </td>
                                    <td>
                                        <cms:LocalizedLinkButton ID="lblFlipVertical" runat="server" EnableViewState="false"
                                            OnClick="btnFlipVerticalClick" ResourceString="img.flipvertical" />
                                    </td>
                                </tr>
                            </table>
                        </Content>
                    </ajaxToolkit:AccordionPane>
                    <ajaxToolkit:AccordionPane ID="pnlAccordion3" runat="server">
                        <Header>
                            <div class="HeaderInner">
                                <cms:LocalizedLabel ID="lblConvert" runat="server" EnableViewState="false" ResourceString="img.convert" />
                            </div>
                        </Header>
                        <Content>
                            <cms:LocalizedLabel ID="lblQualityFailed" runat="server" EnableViewState="false"
                                CssClass="ErrorLabel" ResourceString="img.errors.quality" />
                            <cms:CMSUpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                                <ContentTemplate>
                                    <table class="ImageEditorTable">
                                        <tr>
                                            <td class="LabelConvert">
                                                <cms:LocalizedLabel ID="lblFrom" runat="server" ResourceString="img.from" DisplayColon="true"
                                                    EnableViewState="false" />
                                            </td>
                                            <td>
                                                <cms:LocalizedLabel ID="lblActualFormat" runat="server" EnableViewState="false" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <cms:LocalizedLabel ID="lblTo" runat="server" EnableViewState="false" ResourceString="img.to"
                                                    DisplayColon="true" />
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="drpConvert" runat="server" AutoPostBack="false" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <cms:LocalizedLabel ID="lblQuality" runat="server" EnableViewState="false" ResourceString="img.quality"
                                                    DisplayColon="true" />
                                            </td>
                                            <td>
                                                <cms:CMSTextBox ID="txtQuality" runat="server" Text="100" Enabled="false" CssClass="ImageEditorTextBox"
                                                    MaxLength="3" />
                                                %
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </cms:CMSUpdatePanel>
                            <table class="ImageEditorTable">
                                <tr>
                                    <tr>
                                        <td class="LabelConvert">
                                        </td>
                                        <td>
                                            <cms:CMSButton ID="btnConvert" runat="server" OnClick="btnConvertClick" CssClass="SubmitButton"
                                                EnableViewState="false" />
                                        </td>
                                    </tr>
                            </table>
                        </Content>
                    </ajaxToolkit:AccordionPane>
                    <ajaxToolkit:AccordionPane ID="pnlAccordion4" runat="server">
                        <Header>
                            <div class="HeaderInner Trim">
                                <cms:LocalizedLabel ID="lblCrop" runat="server" EnableViewState="false" ResourceString="img.crop" />
                            </div>
                        </Header>
                        <Content>
                            <asp:Panel ID="pnlCrop" runat="server" DefaultButton="btnCrop">
                                <cms:LocalizedLabel ID="lblCropError" runat="server" EnableViewState="false" CssClass="ErrorLabel"
                                    Visible="false" />
                                <table class="ImageEditorTable" width="100%">
                                    <tr>
                                        <td class="LabelTrim">
                                            <cms:LocalizedLabel ID="lblCropX" runat="server" EnableViewState="false" ResourceString="img.cropX"
                                                DisplayColon="true" />
                                        </td>
                                        <td>
                                            <cms:CMSTextBox ID="txtCropX" runat="server" CausesValidation="true" CssClass="ImageEditorTextBox"
                                                MaxLength="5" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <cms:LocalizedLabel ID="lblCropY" runat="server" EnableViewState="false" ResourceString="img.cropY"
                                                DisplayColon="true" />
                                        </td>
                                        <td>
                                            <cms:CMSTextBox ID="txtCropY" runat="server" CausesValidation="true" CssClass="ImageEditorTextBox"
                                                MaxLength="5" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="LabelTrim">
                                            <cms:LocalizedLabel ID="lblCropWidth" runat="server" EnableViewState="false" ResourceString="img.cropWidth"
                                                DisplayColon="true" />
                                        </td>
                                        <td>
                                            <cms:CMSTextBox ID="txtCropWidth" runat="server" CausesValidation="true" CssClass="ImageEditorTextBox"
                                                MaxLength="5" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <cms:LocalizedLabel ID="lblCropHeight" runat="server" EnableViewState="false" ResourceString="img.cropHeight"
                                                DisplayColon="true" />
                                        </td>
                                        <td>
                                            <cms:CMSTextBox ID="txtCropHeight" runat="server" CausesValidation="true" CssClass="ImageEditorTextBox"
                                                MaxLength="5" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="white-space: nowrap;">
                                            <cms:LocalizedLabel ID="lblCropLock" runat="server" EnableViewState="false" ResourceString="img.cropLock"
                                                DisplayColon="true" />
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="chkCropLock" runat="server" EnableViewState="false" />
                                            <asp:Label ID="lblCropLockLbl" runat="server" EnableViewState="false" AssociatedControlID="chkCropLock"
                                                CssClass="Hidden" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <cms:LocalizedButton ID="btnCropReset" runat="server" CssClass="SubmitButton" ResourceString="img.reset"
                                                RenderScript="true" EnableViewState="false" />
                                        </td>
                                        <td>
                                            <cms:LocalizedButton ID="btnCrop" runat="server" CssClass="SubmitButton" ResourceString="general.ok"
                                                RenderScript="true" OnClick="btnCropClick" EnableViewState="false" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <asp:HiddenField ID="HiddenField1" runat="server" />
                        </Content>
                    </ajaxToolkit:AccordionPane>
                    <ajaxToolkit:AccordionPane ID="pnlAccordion5" runat="server">
                        <Header>
                            <div class="HeaderInner">
                                <cms:LocalizedLabel ID="lblColor" runat="server" EnableViewState="false" ResourceString="img.color" />
                            </div>
                        </Header>
                        <Content>
                            <table class="ImageEditorTable">
                                <tr>
                                    <td class="Image">
                                        <asp:ImageButton ID="btnGrayscale" runat="server" OnClick="btnGrayscaleClick" EnableViewState="false" />
                                    </td>
                                    <td>
                                        <cms:LocalizedLinkButton ID="lblBtnGrayscale" runat="server" EnableViewState="false"
                                            OnClick="btnGrayscaleClick" ResourceString="img.grayscale" />
                                    </td>
                                </tr>
                            </table>
                        </Content>
                    </ajaxToolkit:AccordionPane>
                    <ajaxToolkit:AccordionPane ID="pnlAccordion6" runat="server" ContentCssClass="ImageEditorSubEmpty">
                        <Header>
                            <div class="HeaderInner">
                                <cms:LocalizedLabel ID="lblProperties" runat="server" EnableViewState="false" ResourceString="img.properties" />
                            </div>
                        </Header>
                        <Content>
                        </Content>
                    </ajaxToolkit:AccordionPane>
                </Panes>
            </ajaxToolkit:Accordion>
            <div class="buttonClose">
                <cms:LocalizedLabel ID="lblLoadFailed" runat="server" CssClass="ErrorLabel" EnableViewState="false"
                    Visible="false" />
            </div>
        </td>
        <td valign="top">
            <div id="divProperties" class="ImageEditorProperties">
                <cms:CMSUpdatePanel ID="updPanelProperties" runat="server">
                    <ContentTemplate>
                        <asp:HiddenField ID="hdnShowProperties" runat="server" Value="false" />
                        <cms:LocalizedLabel ID="lblFileNameValidationFailed" runat="server" EnableViewState="false"
                            CssClass="ErrorLabel" />
                        <asp:Label ID="lblPropertiesInfo" runat="server" EnableViewState="false" Visible="false" />
                        <table id="tblProperties" class="MetaDataTable">
                            <tr>
                                <td>
                                    <cms:LocalizedLabel ID="lblFileName" runat="server" EnableViewState="false" ResourceString="general.filename"
                                        DisplayColon="true" />
                                </td>
                                <td>
                                    <cms:CMSTextBox ID="txtFileName" runat="server" CssClass="TextBoxField" MaxLength="250" />
                                </td>
                            </tr>
                            <cms:MetaDataEditor ID="metaDataEditor" runat="server" RenderTableTag="false" ShowOnlyTitleAndDescription="true" />
                            <tr>
                                <td>
                                    <cms:LocalizedLabel ID="lblExtensionText" runat="server" EnableViewState="false"
                                        ResourceString="img.extension" DisplayColon="true" />
                                </td>
                                <td>
                                    <cms:LocalizedLabel ID="lblExtensionValue" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <cms:LocalizedLabel ID="lblImageSizeText" runat="server" ResourceString="img.imagesize"
                                        DisplayColon="true" />
                                </td>
                                <td>
                                    <cms:LocalizedLabel ID="lblImageSizeValue" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <cms:LocalizedLabel ID="lblWidthText" runat="server" EnableViewState="false" ResourceString="img.width"
                                        DisplayColon="true" />
                                </td>
                                <td>
                                    <cms:LocalizedLabel ID="lblWidthValue" runat="server" />
                                    px
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <cms:LocalizedLabel ID="lblHeightText" runat="server" EnableViewState="false" ResourceString="img.height"
                                        DisplayColon="true" />
                                </td>
                                <td>
                                    <cms:LocalizedLabel ID="lblHeightValue" runat="server" />
                                    px
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <cms:CMSButton ID="btnChangeMetaData" runat="server" OnClick="btnChangeMetaDataClick"
                                        EnableViewState="false" CssClass="SubmitButton" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </cms:CMSUpdatePanel>
            </div>
            <iframe id="frameImg" name="imageFrame" scrolling="auto" runat="server" frameborder="0"
                width="99%" height="90%" class="ImageEditorFrame" />
            <asp:Image ID="imgMain" runat="server" Visible="false" />

            <script type="text/javascript" language="javascript">
                //<![CDATA[
                function resizeIframe() {
                    var fameElem = document.getElementById('<%= frameImg.ClientID %>');
                    var propElem = document.getElementById('divProperties');
                    var height = document.documentElement.clientHeight - 120; // footer height

                    fameElem.style.height = Math.max(height - 16, 0) + "px";
                    propElem.style.height = Math.max(height - 20, 0) + "px";
                    fameElem.style.width = "96%";
                };

                function afterResize() {
                    resizeIframe();
                }
                //]]>
            </script>

        </td>
    </tr>
</table>
<asp:Literal ID="ltlScript" runat="server" />