<%@ Page ValidateRequest="false" Language="C#" AutoEventWireup="true" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master"
    Inherits="CMSModules_PortalEngine_UI_WebParts_Development_WebPart_Edit_Properties"
    Theme="Default" EnableEventValidation="false" CodeFile="WebPart_Edit_Properties.aspx.cs" %>

<%@ Register Src="DefaultValueEditor.ascx" TagName="DefaultValueEditor" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/AdminControls/Controls/Class/FieldEditor/FieldEditor.ascx"
    TagName="FieldEditor" TagPrefix="cms" %>
<asp:Content ContentPlaceHolderID="plcContent" ID="content" runat="server">
    <cms:FieldEditor ID="FieldEditor" ShortID="f" runat="server" DisplaySourceFieldSelection="false" />
    <asp:Panel ID="pnlDefaultEditor" runat="server" CssClass="FieldPanelError" Visible="false">
        <asp:Label runat="server" ID="lblInfo" Visible="false" EnableViewState="false" />
        <cms:DefaultValueEditor ID="DefaultValueEditor1" ShortID="d" runat="server" Visible="false" />
    </asp:Panel>
</asp:Content>
