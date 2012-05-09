<%@ Page Language="C#" AutoEventWireup="true" CodeFile="~/CMSModules/PortalEngine/UI/WebParts/Development/WebPart_Edit_CSS.aspx.cs"
    MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Inherits="CMSModules_PortalEngine_UI_WebParts_Development_WebPart_Edit_CSS"
    Theme="Default" %>

<asp:Content ContentPlaceHolderID="plcContent" ID="content" runat="server">
    <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
        Visible="false" />
    <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
        Visible="false" />
    <table style="width: 100%;" cellpadding="0">
        <tr>
            <td>
                <cms:LocalizedLabel runat="server" ID="lblCss" ResourceString="Container_Edit.ContainerCSS"
                    DisplayColon="true" EnableViewState="false" />
                <br />
                <cms:ExtendedTextArea ID="etaCSS" runat="server" EnableViewState="true"
                    EditorMode="Advanced" Language="CSS" Width="98%" Height="500px" />
            </td>
        </tr>
        <tr>
            <td>
                <cms:CMSButton ID="btnOk" runat="server" OnClick="btnOK_Click" CssClass="SubmitButton" />
            </td>
        </tr>
    </table>
</asp:Content>
