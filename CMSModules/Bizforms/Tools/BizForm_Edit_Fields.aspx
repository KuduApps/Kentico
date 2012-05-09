<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master"
    Title="BizForm Fields" Inherits="CMSModules_BizForms_Tools_BizForm_Edit_Fields"
    EnableEventValidation="false" Theme="Default" CodeFile="BizForm_Edit_Fields.aspx.cs" %>

<%@ Register Src="~/CMSModules/AdminControls/Controls/Class/FieldEditor/FieldEditor.ascx"
    TagName="FieldEditor" TagPrefix="cms" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:Panel ID="pnlError" runat="server" CssClass="FieldPanelError" Visible="false" EnableViewState="false">
        <cms:LocalizedLabel ID="lblError" runat="server" EnableViewState="false" Visible="false"
            ResourceString="EditTemplateFields.ErrorIsNotCoupled" CssClass="ErrorLabel" />
    </asp:Panel>
    <cms:FieldEditor ID="FieldEditor" runat="server" />
</asp:Content>
