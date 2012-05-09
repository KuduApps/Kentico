<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_PortalEngine_UI_PageTemplates_PageTemplate_HeaderTab" Theme="Default"
    ValidateRequest="false" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master"
    Title="Page Template Edit - Header" CodeFile="PageTemplate_HeaderTab.aspx.cs" %>

<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
        Visible="false" />
    <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
        Visible="false" />
    <asp:Literal ID="ltlScript" runat="server" />
    <table style="width: 100%">
        <tr>
            <td style="vertical-align: top;">
                <asp:Label ID="lblTemplateHeader" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <cms:ExtendedTextArea ID="txtTemplateHeader" runat="server" EnableViewState="false"
                    EditorMode="Advanced" Width="98%" Height="270" />
            </td>
        </tr>
        <tr>
            <td>
                <cms:CMSButton ID="btnOk" runat="server" OnClick="btnOK_Click" CssClass="SubmitButton" />
            </td>
        </tr>
    </table>
</asp:Content>
