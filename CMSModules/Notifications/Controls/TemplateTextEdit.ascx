<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_Notifications_Controls_TemplateTextEdit" CodeFile="TemplateTextEdit.ascx.cs" %>
<cms:LocalizedLabel runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
    Visible="false" />
<cms:LocalizedLabel runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
    Visible="false" />
<table width="100%">
    <asp:PlaceHolder runat="server" ID="plcSubject" EnableViewState="false">
        <tr>
            <td>
                <cms:LocalizedLabel runat="server" ID="lblSubject" CssClass="ContentLabel" DisplayColon="true"
                    EnableViewState="false" />
            </td>
            <td>
                <cms:CMSTextBox runat="server" ID="txtSubject" CssClass="TextBoxField" MaxLength="250" EnableViewState="false" />
            </td>
        </tr>
    </asp:PlaceHolder>
    <asp:PlaceHolder runat="server" ID="plcHTMLText" EnableViewState="false">
        <tr>
            <td style="vertical-align: top;">
                <cms:LocalizedLabel runat="server" ID="lblHTMLText" CssClass="ContentLabel" DisplayColon="true"
                    EnableViewState="false" />
            </td>
            <td>
                <div style="width: 625px;">
                    <cms:CMSHtmlEditor ID="htmlText" runat="server" Height="315px" />
                </div>
            </td>
        </tr>
    </asp:PlaceHolder>
    <asp:PlaceHolder runat="server" ID="plcPlainText" EnableViewState="false">
        <tr>
            <td style="vertical-align: top;">
                <cms:LocalizedLabel runat="server" ID="lblPlainText" CssClass="ContentLabel" DisplayColon="true"
                    EnableViewState="false" />
            </td>
            <td>
                <cms:CMSTextBox runat="server" ID="txtPlainText" TextMode="MultiLine" CssClass="TextAreaLarge" EnableViewState="false" />
            </td>
        </tr>
    </asp:PlaceHolder>
    <asp:PlaceHolder runat="server" ID="plcNoTextbox" EnableViewState="false">
        <tr>
            <td colspan="2">
                <cms:LocalizedLabel runat="server" ID="lblNoTextbox" EnableViewState="false" />
            </td>
        </tr>
    </asp:PlaceHolder>
</table>
<div style="padding: 10px;">
</div>
