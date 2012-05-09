<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_Content_Controls_Dialogs_Properties_HTMLLinkProperties" CodeFile="HTMLLinkProperties.ascx.cs" %>
<%@ Register Src="~/CMSModules/Content/Controls/Dialogs/General/WidthHeightSelector.ascx" TagPrefix="cms"
    TagName="WidthHeightSelector" %>
<%@ Register Src="~/CMSModules/Content/Controls/Dialogs/General/URLSelector.ascx" TagPrefix="cms"
    TagName="URLSelector" %>
<div class="HTMLLinkProperties">
    <asp:Panel runat="server" ID="pnlEmpty" Visible="true" CssClass="DialogInfoArea">
        <asp:Label runat="server" ID="lblEmpty" EnableViewState="false" />
    </asp:Panel>
    <cms:JQueryTabContainer ID="pnlTabs" runat="server" CssClass="DialogElementHidden">
        <cms:JQueryTab ID="tabGeneral" runat="server">
            <ContentTemplate>
                <cms:URLSelector runat="server" ID="urlSelectElem" />
            </ContentTemplate>
        </cms:JQueryTab>
        <cms:JQueryTab ID="tabTarget" runat="server">
            <ContentTemplate>
                <div class="TargetTab">
                    <table style="vertical-align: top;">
                        <tr>
                            <td>
                                <cms:LocalizedLabel ID="lblTargetName" runat="server" EnableViewState="false" ResourceString="dialogs.link.targetname"
                                    DisplayColon="true" />
                            </td>
                            <td>
                                <cms:LocalizedLabel ID="lblTargetFrame" runat="server" EnableViewState="false" ResourceString="dialogs.link.targetframe"
                                    DisplayColon="true" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:DropDownList ID="drpTarget" runat="server" Width="200" />
                            </td>
                            <td>
                                <cms:CMSTextBox runat="server" ID="txtTargetFrame" CssClass="LongTextBox" EnableViewState="false" />
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </cms:JQueryTab>
        <cms:JQueryTab ID="tabAdvanced" runat="server">
            <ContentTemplate>
                <div class="AdvancedTab">
                    <table style="vertical-align: top;">
                        <tr>
                            <td>
                                <cms:LocalizedLabel ID="lblAdvID" runat="server" EnableViewState="false" ResourceString="dialogs.advanced.id"
                                    DisplayColon="true" />
                            </td>
                            <td>
                                <cms:CMSTextBox runat="server" ID="txtAdvId" CssClass="LongTextBox" EnableViewState="false" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <cms:LocalizedLabel ID="lblAdvName" runat="server" EnableViewState="false" ResourceString="dialogs.advanced.name"
                                    DisplayColon="true" />
                            </td>
                            <td>
                                <cms:CMSTextBox runat="server" ID="txtAdvName" CssClass="LongTextBox" EnableViewState="false" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <cms:LocalizedLabel ID="lblAdvTooltip" runat="server" EnableViewState="false" ResourceString="dialogs.advanced.tooltip"
                                    DisplayColon="true" />
                            </td>
                            <td>
                                <cms:CMSTextBox runat="server" ID="txtAdvTooltip" CssClass="LongTextBox" EnableViewState="false" />
                            </td>
                            <td colspan="2">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <cms:LocalizedLabel ID="lblAdvStyleSheet" runat="server" EnableViewState="false"
                                    ResourceString="dialogs.advanced.stylesheet" DisplayColon="true" />
                            </td>
                            <td>
                                <cms:CMSTextBox runat="server" ID="txtAdvStyleSheet" CssClass="LongTextBox" EnableViewState="false" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <cms:LocalizedLabel ID="lblAdvStyle" runat="server" EnableViewState="false" ResourceString="dialogs.advanced.style"
                                    DisplayColon="true" />
                            </td>
                            <td>
                                <cms:CMSTextBox runat="server" ID="txtAdvStyle" CssClass="TextAreaField" TextMode="MultiLine"
                                    EnableViewState="false" />
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </cms:JQueryTab>
    </cms:JQueryTabContainer>
</div>
<cms:CMSUpdatePanel ID="pnlAdvancedTab" runat="server">
    <ContentTemplate>
        <cms:CMSButton ID="btnHidden" runat="server" CssClass="HiddenButton" EnableViewState="false" />
    </ContentTemplate>
</cms:CMSUpdatePanel>
