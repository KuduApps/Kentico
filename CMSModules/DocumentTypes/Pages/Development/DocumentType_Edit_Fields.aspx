<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_DocumentTypes_Pages_Development_DocumentType_Edit_Fields"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Document Type Edit - Fields"
    CodeFile="DocumentType_Edit_Fields.aspx.cs" %>

<%@ Register Src="~/CMSModules/AdminControls/Controls/Class/FieldEditor/FieldEditor.ascx"
    TagName="FieldEditor" TagPrefix="cms" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:Panel ID="pnlError" runat="server" CssClass="FieldPanelError" Visible="false"
        EnableViewState="false">
        <cms:LocalizedLabel ID="lblError" runat="server" EnableViewState="false" Visible="true"
            ResourceString="EditTemplateFields.ErrorIsNotCoupled" />
    </asp:Panel>
    <cms:FieldEditor ID="FieldEditor" runat="server" EnableSystemFields="true" />
</asp:Content>
