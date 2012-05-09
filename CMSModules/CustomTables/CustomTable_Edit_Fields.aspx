<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_CustomTables_CustomTable_Edit_Fields"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Custom talble edit - Fields"
    CodeFile="CustomTable_Edit_Fields.aspx.cs" %>

<%@ Register Src="~/CMSModules/AdminControls/Controls/Class/FieldEditor/FieldEditor.ascx"
    TagName="FieldEditor" TagPrefix="cms" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:Panel ID="pnlClassField" runat="server">
        <asp:Panel ID="pnlTopMenu" runat="server" CssClass="FieldClassMenu" Visible="false">
            <asp:Panel ID="pnlTopMenuButtons" runat="server" CssClass="FieldClassMenuWrapper">
                <asp:PlaceHolder ID="plcGUID" runat="server" Visible="false" EnableViewState="false">
                    <asp:Label ID="lblGUID" runat="server" EnableViewState="false" /><br />
                    <asp:LinkButton ID="btnGUID" runat="server" EnableViewState="false" />
                </asp:PlaceHolder>
                <asp:Label ID="lblError" runat="server" EnableViewState="false" CssClass="ErrorLabel" />
            </asp:Panel>
        </asp:Panel>
        <cms:FieldEditor ID="FieldEditor" runat="server" Visible="false" />
    </asp:Panel>
</asp:Content>
