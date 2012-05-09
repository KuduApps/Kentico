<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_Content_Controls_Dialogs_General_URLSelector" CodeFile="URLSelector.ascx.cs" %>
<div class="GeneralTab">
    <table style="vertical-align: top;">
        <tr>
            <td>
                <cms:LocalizedLabel ID="lblProtocol" runat="server" EnableViewState="false" ResourceString="dialogs.link.protocol"
                    DisplayColon="true" />
            </td>
            <td>
                <cms:LocalizedLabel ID="lblUrl" runat="server" ResourceString="dialogs.link.url"
                    DisplayColon="true" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:DropDownList runat="server" ID="drpProtocol" />
            </td>
            <td>
                <cms:CMSTextBox runat="server" ID="txtUrl" CssClass="VeryLongTextBox" />
            </td>
        </tr>
        <asp:PlaceHolder runat="server" ID="plcLinkText">
            <tr>
                <td>
                    <cms:LocalizedLabel ID="lblLinkText" runat="server" EnableViewState="false" ResourceString="dialogs.link.text"
                        DisplayColon="true" />
                </td>
                <td>
                    <cms:CMSTextBox runat="server" ID="txtLinkText" CssClass="VeryLongTextBox" />
                </td>
            </tr>
        </asp:PlaceHolder>
    </table>
</div>
