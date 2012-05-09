<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master"
    Title="ALternative forms - fields" Inherits="CMSModules_BizForms_Tools_AlternativeForms_AlternativeForms_Fields"
    Theme="Default" CodeFile="AlternativeForms_Fields.aspx.cs" %>

<%@ Register Src="~/CMSModules/AdminControls/Controls/Class/AlternativeFormFieldEditor.ascx"
    TagName="AlternativeFormFieldEditor" TagPrefix="cms" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:Panel ID="pnlError" runat="server" CssClass="FieldPanelError" Visible="false" EnableViewState="false">
        <cms:LocalizedLabel ID="lblError" runat="server" CssClass="ErrorLabel" EnableViewState="false"
            ResourceString="general.invalidid" />
    </asp:Panel>
    <cms:AlternativeFormFieldEditor ID="altFormFieldEditor" runat="server" />
</asp:Content>
