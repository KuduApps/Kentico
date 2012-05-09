<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Reporting_Tools_Report_Parameters"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/EmptyPage.master" CodeFile="Report_Parameters.aspx.cs" %>

<%@ Register Src="~/CMSModules/AdminControls/Controls/Class/FieldEditor/FieldEditor.ascx"
    TagName="FieldEditor" TagPrefix="cms" %>
<asp:Content ContentPlaceHolderID="plcContent" ID="content" runat="server">
    <asp:Panel ID="pnlClassField" runat="server" CssClass="FieldClassEditor">
        <cms:FieldEditor ID="editor" runat="server" />
    </asp:Panel>
</asp:Content>
