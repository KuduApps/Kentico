<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_Content_Controls_Dialogs_Web_WebContentSelector" CodeFile="WebContentSelector.ascx.cs" %>
<%@ Register src="~/CMSAdminControls/UI/PageElements/Help.ascx" tagname="Help" tagprefix="cms" %>
<%@ Register Src="~/CMSModules/Content/Controls/Dialogs/Properties/HTMLMediaProperties.ascx"
    TagName="MediaProperties" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/Content/Controls/Dialogs/Properties/BBMediaProperties.ascx" TagName="BBMediaProperties"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/Content/Controls/Dialogs/Properties/URLProperties.ascx" TagName="URLProperties"
    TagPrefix="cms" %>

<script type="text/javascript" language="javascript">
    function insertItem() {
        RaiseHiddenPostBack();
    }
</script>

<cms:CMSUpdatePanel runat="server" ID="pnlUpdate" UpdateMode="Conditional">
    <ContentTemplate>
        <div class="DialogWebContent">
            <table width="100%">
                <tr>
                    <td style="white-space: nowrap;">
                        <cms:LocalizedLabel ID="lblUrl" runat="server" ResourceString="dialogs.link.url"
                            EnableViewState="false" DisplayColon="true" />
                    </td>
                    <td style="white-space: nowrap; width: 100%;">
                        <div class="LeftAlign">
                            <cms:CMSTextBox runat="server" ID="txtUrl" CssClass="DialogWebUrlMaxBox LeftAlign" />
                            <asp:PlaceHolder ID="plcRefresh" runat="server">
                                <asp:ImageButton ID="imgRefresh" runat="server" CssClass="DialogItemUrlRefresh" EnableViewState="false" />
                            </asp:PlaceHolder>
                        </div>
                    </td>
                    <td class="TextRight" style="width: 100%; vertical-align: top; white-space: nowrap;"
                        rowspan="2">
                        <cms:Help ID="helpElem" runat="server" TopicName="dialogs_web" />
                    </td>
                </tr>
                <asp:PlaceHolder ID="plcMediaType" runat="server">
                    <tr>
                        <td style="white-space: nowrap;">
                            <cms:LocalizedLabel ID="lblMediaType" runat="server" ResourceString="dialogs.web.mediatype"
                                EnableViewState="false" DisplayColon="true" />
                        </td>
                        <td>
                            <asp:DropDownList ID="drpMediaType" runat="server" AutoPostBack="true" CssClass="DropDownField" />
                        </td>
                    </tr>
                </asp:PlaceHolder>
            </table>
        </div>
        <asp:Panel ID="pnlProperties" runat="server" CssClass="DialogWebProperties">
            <asp:PlaceHolder runat="server" ID="plcInfo" Visible="true">
                <div class="DialogInfoArea LeftAlign">
                    <cms:LocalizedLabel runat="server" ID="lblInfo" EnableViewState="false" />
                </div>
            </asp:PlaceHolder>
            <asp:PlaceHolder runat="server" ID="plcHTMLMediaProp" Visible="true">
                <cms:MediaProperties runat="server" ID="propMedia" DisplayUrlTextbox="false" />
            </asp:PlaceHolder>
            <asp:PlaceHolder runat="server" ID="plcBBMediaProp" Visible="true">
                <cms:BBMediaProperties runat="server" ID="propBBMedia" HideUrlBox="true" IsWeb="true" />
            </asp:PlaceHolder>
            <asp:PlaceHolder runat="server" ID="plcURLProp" Visible="true">
                <div class="DialogWebUrlProp">
                    <cms:URLProperties runat="server" ID="propURL" IsWeb="true" />
                </div>
            </asp:PlaceHolder>
        </asp:Panel>
        <cms:CMSButton ID="hdnButton" runat="server" OnClick="hdnButton_Click" CssClass="HiddenButton" />
        <cms:CMSButton ID="hdnButtonUrl" runat="server" OnClick="hdnButtonUrl_Click" CssClass="HiddenButton" />
        <asp:Literal ID="ltlScript" runat="server" EnableViewState="false" />
    </ContentTemplate>
</cms:CMSUpdatePanel>
