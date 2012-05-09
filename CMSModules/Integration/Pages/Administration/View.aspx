<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Integration_Pages_Administration_View"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/Dialogs/ModalDialogPage.master"
    Title="Staging - Task detail" CodeFile="View.aspx.cs" %>

<%@ Register Src="~/CMSModules/Objects/Controls/ViewObjectDataSet.ascx" TagName="ViewDataSet"
    TagPrefix="cms" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <div class="PageContent">
        <cms:ViewDataSet ID="viewDataSet" runat="server" />
    </div>
</asp:Content>
<asp:Content ID="cntFooter" ContentPlaceHolderID="plcFooter" runat="server" EnableViewState="false">
    <div class="FloatRight">
        <cms:LocalizedButton ID="btnClose" runat="server" CssClass="SubmitButton" ResourceString="general.close"
            OnClientClick="window.close(); return false;" EnableViewState="false" />
    </div>
</asp:Content>
