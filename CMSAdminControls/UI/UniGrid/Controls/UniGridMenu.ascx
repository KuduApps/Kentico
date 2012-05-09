<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UniGridMenu.ascx.cs" Inherits="CMSAdminControls_UI_UniGrid_Controls_UniGridMenu" %>
<%@ Register Assembly="CMS.UIControls" Namespace="CMS.UIControls" TagPrefix="cms" %>
<asp:Panel runat="server" ID="pnlUniGridMenu" CssClass="PortalContextMenu WebPartContextMenu"
    EnableViewState="false">
    <asp:Panel runat="server" ID="pnlExcel" CssClass="Item">
        <asp:Panel runat="server" ID="pnlExcelPadding" CssClass="ItemPadding">
            <asp:Image runat="server" ID="imgExcel" CssClass="Icon" EnableViewState="false" />&nbsp;
            <cms:LocalizedLabel runat="server" ID="lblExcel" CssClass="Name" EnableViewState="false"
                ResourceString="export.exporttoexcel" />
        </asp:Panel>
    </asp:Panel>
    <asp:Panel runat="server" ID="pnlCSV" CssClass="Item">
        <asp:Panel runat="server" ID="pnlCSVPadding" CssClass="ItemPadding">
            <asp:Image runat="server" ID="imgCSV" CssClass="Icon" EnableViewState="false" />&nbsp;
            <cms:LocalizedLabel runat="server" ID="lblCSV" CssClass="Name" EnableViewState="false"
                ResourceString="export.exporttocsv" />
        </asp:Panel>
    </asp:Panel>
    <asp:Panel runat="server" ID="pnlXML" CssClass="Item">
        <asp:Panel runat="server" ID="pnlXMLPadding" CssClass="ItemPadding">
            <asp:Image runat="server" ID="imgXML" CssClass="Icon" EnableViewState="false" />&nbsp;
            <cms:LocalizedLabel runat="server" ID="lblXML" CssClass="Name" EnableViewState="false"
                ResourceString="export.exporttoxml" />
        </asp:Panel>
    </asp:Panel>
    <asp:Panel runat="server" ID="pnlAdvancedExport" CssClass="ItemLast">
        <asp:Panel runat="server" ID="pnlAdvancedExportPadding" CssClass="ItemPadding">
            <asp:Image runat="server" ID="imgAdvancedExport" CssClass="Icon" EnableViewState="false" />&nbsp;
            <cms:LocalizedLabel runat="server" ID="lblAdvancedExport" CssClass="Name" EnableViewState="false"
                ResourceString="export.advancedexport" />
        </asp:Panel>
    </asp:Panel>
</asp:Panel>
