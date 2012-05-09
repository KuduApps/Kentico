<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_PortalEngine_UI_WebParts_WebPartDocumentationPage"
    MasterPageFile="~/CMSMasterPages/UI/Dialogs/ModalDialogPage.master" Title="Web part - Documentation"
    Theme="Default" CodeFile="WebPartDocumentationPage.aspx.cs" %>
<%@ Register Src="~/CMSModules/PortalEngine/Controls/WebParts/WebPartDocumentation.ascx" TagName="WebPartDocumentation" TagPrefix="cms" %>    
<asp:Content ID="cntContent" ContentPlaceHolderID="plcContent" runat="Server">
   <cms:WebPartDocumentation runat="server" ID="ucWebPartDocumentation" />
</asp:Content>
<asp:Content ID="cntFooter" ContentPlaceHolderID="plcFooter" runat="server">
    <div class="FloatRight" id="divFooter" runat="server">
        <cms:LocalizedButton ID="btnClose" runat="server" CssClass="SubmitButton" EnableViewState="False"
            OnClientClick="parent.window.close(); return false;" ResourceString="WebPartDocumentDialog.Close" />
    </div>
</asp:Content>
