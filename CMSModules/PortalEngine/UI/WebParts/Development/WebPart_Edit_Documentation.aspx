<%@ Page Language="C#" AutoEventWireup="true"
    MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Inherits="CMSModules_PortalEngine_UI_WebParts_Development_WebPart_Edit_Documentation"
    Theme="Default" EnableEventValidation="false" CodeFile="WebPart_Edit_Documentation.aspx.cs" %>

<asp:Content ContentPlaceHolderID="plcContent" ID="content" runat="server">
    <asp:Label ID="lblInfo" runat="server" Visible="false" EnableViewState="false" />
    <table style="width: 100%">
        <tbody>
            <tr>
                <td>
                    <cms:CMSHtmlEditor ID="htmlText" runat="server" Height="500px" Width="100%" />
                </td>
            </tr>
            <tr>
                <td>
                    <cms:CMSButton ID="btnOk" runat="server" CssClass="SubmitButton" OnClick="btnOk_Click" />
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>
