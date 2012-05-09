<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_MediaLibrary_Controls_MediaLibrary_ImageEditor"
    MasterPageFile="~/CMSMasterPages/UI/Dialogs/ModalDialogPage.master" Title="Edit image"
    Theme="Default" CodeFile="ImageEditor.aspx.cs" %>

<%@ Register Src="~/CMSModules/MediaLibrary/Controls/MediaLibrary/ImageEditor.ascx"
    TagName="ImageEditor" TagPrefix="cms" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <div class="PageContent">
        <cms:ImageEditor ID="imageEditor" runat="server" IsLiveSite="false" EnableViewState="true" />
    </div>
</asp:Content>
<asp:Content ID="cntFooter" runat="server" ContentPlaceHolderID="plcFooter">
    <cms:CMSUpdatePanel ID="updPanelFooter" runat="server">
        <ContentTemplate>
            <div class="FloatLeft">
                <cms:LocalizedButton ID="btnUndo" runat="server" CssClass="SubmitButton" ResourceString="general.undo"
                    EnableViewState="false" />&nbsp;&nbsp;<cms:LocalizedButton ID="btnRedo" runat="server"
                        CssClass="SubmitButton" ResourceString="general.redo" EnableViewState="false" />
            </div>
            <div class="FloatRight">
                <cms:LocalizedButton ID="btnSave" runat="server" CssClass="LongSubmitButton" ResourceString="img.savechanges"
                    EnableViewState="false" />
                <cms:LocalizedButton ID="btnClose" runat="server" CssClass="SubmitButton" ResourceString="general.close"
                    EnableViewState="false" />
            </div>
        </ContentTemplate>
    </cms:CMSUpdatePanel>
    <asp:Literal runat="server" ID="ltlScript" EnableViewState="false" />
</asp:Content>
