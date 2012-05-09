<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSSiteManager_Sites_Site_Edit_OfflineMode"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Site Edit - Offline mode"
    CodeFile="Site_Edit_OfflineMode.aspx.cs" %>

<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:Label ID="lblError" runat="server" CssClass="ErrorLabel" EnableViewState="false"
        Visible="false" />
    <asp:Label ID="lblInfo" runat="server" CssClass="InfoLabel" EnableViewState="false"
        Visible="false" />
    <table>
        <tbody>
            <tr>
                <td>
                    <asp:Label ID="lblSystemInfo" runat="server" CssClass="InfoLabel" EnableViewState="false" />
                    <cms:CMSButton ID="btnSubmit" runat="server" CssClass="XXLongSubmitButton" OnClick="btnSubmit_Click" />
                    <br /><br /><br />
                    <strong><cms:LocalizedLabel ID="lblOfflineTitle" runat="server" CssClass="InfoLabel" EnableViewState="false" ResourceString="sm.offline.offlinetitle" /></strong>
                </td>
            </tr>
            <tr>
                <td>
                    <cms:LocalizedRadioButton ID="radMessage" runat="server" GroupName="Offline" EnableViewState="false"
                        ResourceString="sm.offline.displaymessage" />
                </td>
            </tr>
            <tr>
                <td class="UnderRadioContent">
                    <cms:CMSHtmlEditor runat="server" id="txtMessage" ToolbarSet="SimpleEdit" Width="700px" Height="340px" />
                </td>
            </tr>
            <tr>
                <td>
                    <cms:LocalizedRadioButton ID="radURL" runat="server" GroupName="Offline" EnableViewState="false"
                        ResourceString="sm.offline.redirecttourl" />
                </td>
            </tr>
            <tr>
                <td class="UnderRadioContent">
                    <cms:CMSTextBox ID="txtURL" runat="server" CssClass="TextBoxField" MaxLength="400" />
                </td>
            </tr>
            <tr>
                <td class="UnderRadioContent">
                    <cms:CMSButton ID="btnOK" runat="server" CssClass="SubmitButton" OnClick="btnOK_Click" />
                    
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>
