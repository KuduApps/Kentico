<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_RecycleBin_Pages_ViewVersion"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/Dialogs/ModalDialogPage.master"
    Title="Document details" CodeFile="ViewVersion.aspx.cs" %>

<%@ Register Src="~/CMSModules/Content/Controls/ViewVersion.ascx" TagName="ViewVersion"
    TagPrefix="cms" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <cms:ViewVersion ID="viewVersion" runat="server" />
</asp:Content>
<asp:Content ID="cntFooter" ContentPlaceHolderID="plcFooter" runat="server">
    <div class="FloatRight">
        <cms:LocalizedButton ID="btnClose" runat="server" CssClass="SubmitButton" OnClientClick="window.close(); return false;"
            ResourceString="General.Close" />
    </div>
</asp:Content>
