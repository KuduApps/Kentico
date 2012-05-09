<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_PortalEngine_UI_PageTemplates_PageTemplate_WebParts" Theme="Default"
    ValidateRequest="false" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Page Template Edit - Web Parts" CodeFile="PageTemplate_WebParts.aspx.cs" %>

<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
        Visible="false" />
    <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
        Visible="false" />
    <asp:Label runat="server" ID="lblWarning" Style="font-weight: bold; display: block"
        EnableViewState="false" />
    <br />
    <table style="width: 100%;" cellpadding="0">
        <tr>
            <td>
                <asp:Label runat="server" ID="lblWPConfig" EnableViewState="false" /><br />
                <cms:ExtendedTextArea runat="server" ID="txtWebParts" EnableViewState="false"
                    EditorMode="Advanced" Language="XML" Width="98%" Height="480px" />
            </td>
        </tr>
        <tr>
            <td>
                <cms:CMSButton ID="btnOk" runat="server" OnClick="btnOK_Click" CssClass="SubmitButton" /></td>
        </tr>
    </table>
</asp:Content>
