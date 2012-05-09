<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_UIPersonalization_Pages_Administration_UI_Editor"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/EmptyPage.master" Title="UI Personalization - Dialogs" CodeFile="UI_Editor.aspx.cs" %>

<%@ Register Src="~/CMSModules/UIPersonalization/Controls/UIPersonalization.ascx"  TagName="UIPersonalization" TagPrefix="cms" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <div style="position: absolute; width: 100%; height: 100%; top: 0px; bottom: 0px;">
        <cms:UIPersonalization runat="server" ID="editElem" />
    </div>
</asp:Content>
