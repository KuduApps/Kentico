<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_SystemTables_Pages_Development_SystemTable_Edit_Fields"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="System table edit"
    CodeFile="Edit_Fields.aspx.cs" %>

<%@ Register Src="~/CMSModules/AdminControls/Controls/Class/FieldEditor/FieldEditor.ascx"
    TagName="FieldEditor" TagPrefix="cms" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:Panel ID="pnlError" runat="server" CssClass="FieldPanelError" Visible="false"
        EnableViewState="false">
        <cms:localizedlabel id="lblError" runat="server" enableviewstate="false" cssclass="ErrorLabel"
            resourcestring="EditTemplateFields.ErrorIsNotCoupled" />
    </asp:Panel>
    <cms:fieldeditor id="FieldEditor" runat="server" />
</asp:Content>
