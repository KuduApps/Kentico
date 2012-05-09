<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_Content_Controls_Dialogs_Properties_URLProperties" CodeFile="URLProperties.ascx.cs" %>
<%@ Register Src="~/CMSModules/Content/Controls/Dialogs/General/WidthHeightSelector.ascx" TagPrefix="cms"
    TagName="WidthHeightSelector" %>
<%@ Register Src="~/CMSInlineControls/ImageControl.ascx" TagPrefix="cms" TagName="ImagePreview" %>
<%@ Register Src="~/CMSInlineControls/MediaControl.ascx" TagPrefix="cms" TagName="MediaPreview" %>
<asp:PlaceHolder ID="plcPropContent" runat="server">
    <asp:Panel runat="server" ID="pnlEmpty" Visible="true" CssClass="DialogInfoArea">
        <asp:Label runat="server" ID="lblEmpty" EnableViewState="false" />
    </asp:Panel>
    <cms:JQueryTabContainer ID="pnlTabs" runat="server" CssClass="DialogElementHidden">
        <cms:JQueryTab ID="tabImageGeneral" runat="server">
            <ContentTemplate>
                <asp:Panel runat="server" ID="pnlGeneralTab" CssClass="ImageGeneralTab" DefaultButton="imgRefresh">
                    <table style="width: 100%;">
                        <asp:PlaceHolder runat="server" ID="plcUrl" Visible="true">
                            <tr>
                                <td style="white-space: nowrap;">
                                    <cms:LocalizedLabel ID="lblUrl" runat="server" EnableViewState="false" DisplayColon="true"
                                        ResourceString="general.url" />&nbsp;
                                </td>
                                <td style="width: 100%;" colspan="2">
                                    <div style="width: 100%;">
                                        <cms:CMSTextBox ID="txtUrl" runat="server" CssClass="DialogItemUrlBox" />
                                        <asp:ImageButton ID="imgRefresh" runat="server" OnClick="imgRefresh_Click" CssClass="DialogItemUrlRefresh" EnableViewState="false" />
                                    </div>
                                </td>
                            </tr>
                        </asp:PlaceHolder>
                        <asp:PlaceHolder runat="server" ID="plcSelectPath" Visible="false">
                            <tr>
                                <td style="white-space: nowrap;">
                                    <cms:LocalizedLabel ID="lblSelectPah" runat="server" EnableViewState="false" ResourceString="generalproperties.aliaspath" />
                                </td>
                                <td style="width: 100%;">
                                    <cms:CMSTextBox ID="txtSelectPath" runat="server" CssClass="DialogItemUrlBox" ReadOnly="true" />
                                </td>
                            </tr>
                            <asp:PlaceHolder runat="server" ID="plcIncludeSubitems" Visible="false">
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <cms:LocalizedCheckBox ID="chkItems" runat="server" CssClass="CheckBoxMovedLeft"
                                            TextAlign="Right" ResourceString="pathselection.inlcudesubitems" EnableViewState="false" />
                                    </td>
                                </tr>
                            </asp:PlaceHolder>
                        </asp:PlaceHolder>
                        <asp:PlaceHolder ID="plcPreviewArea" runat="server">
                        <tr>
                            <asp:PlaceHolder ID="plcSizeSelectorWidth" runat="server">
                                <td style="white-space: nowrap;">
                                    <cms:LocalizedLabel ID="lblWidth" runat="server" DisplayColon="true" ResourceString="general.width"
                                        EnableViewState="false" />
                                </td>
                                <td rowspan="2">
                                    <cms:WidthHeightSelector ID="widthHeightElem" runat="server" ShowActions="false"
                                        ShowLabels="false" Locked="false" />
                                </td>
                            </asp:PlaceHolder>
                            <td runat="server" id="previewTd" style="width: 100%; vertical-align: top;" rowspan="2" enableviewstate="false">
                                <asp:Panel runat="server" ID="pnlImagePreview" CssClass="DialogPropertiesPreview DialogLongPreview">
                                    <cms:ImagePreview ID="imagePreview" runat="server" />
                                </asp:Panel>
                                <asp:Panel runat="server" ID="pnlMediaPreview" CssClass="DialogPropertiesPreview DialogLongPreview">
                                    <cms:MediaPreview runat="server" Id="mediaPreview" />
                                </asp:Panel>
                            </td>
                        </tr>
                        </asp:PlaceHolder>
                        <asp:PlaceHolder ID="plcSizeSelectorHeight" runat="server">
                            <tr>
                                <td>
                                    <cms:LocalizedLabel ID="lblHeight" runat="server" DisplayColon="true" ResourceString="general.height"
                                        EnableViewState="false" />
                                </td>
                            </tr>
                        </asp:PlaceHolder>
                    </table>
                    <cms:CMSButton ID="btnHidden" runat="server" CssClass="HiddenButton" EnableViewState="false" />
                    <cms:CMSButton ID="btnTxtHidden" runat="server" CssClass="HiddenButton" EnableViewState="false" />
                    <cms:CMSButton ID="btnHiddenSize" runat="server" CssClass="HiddenButton" EnableViewState="false" />
                </asp:Panel>
            </ContentTemplate>
        </cms:JQueryTab>
    </cms:JQueryTabContainer>

    <script type="text/javascript">
        //<![CDATA[

        // Called on checkbox checked changed
        function chkItemsChecked_Changed(checked) {
            var aliasPathElem = document.getElementById('<%= this.txtSelectPath.ClientID %>');
            if (aliasPathElem != null) {
                var length = aliasPathElem.value.length;

                if (checked) {
                    // Check last two letters
                    if (aliasPathElem.value == "/") {
                        aliasPathElem.value = "/%";
                    }
                    else if (aliasPathElem.value.substring(length - 2, length) != "/%") {
                        aliasPathElem.value += "/%";
                    }
                }
                else {
                    // Check last two letters                                                          
                    if (aliasPathElem.value == "/%") {
                        aliasPathElem.value = "/";
                    }
                    else if (aliasPathElem.value.substring(length - 2, length) == "/%") {
                        aliasPathElem.value = aliasPathElem.value.substring(0, length - 2);
                    }
                }

                // save alias path
                aliasPath = aliasPathElem.value;
            }
        }

        //]]>
    </script>

</asp:PlaceHolder>
