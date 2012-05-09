<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Ecommerce_Pages_Tools_Configuration_InvoiceTemplate_InvoiceTemplate_Edit"
    Theme="Default" ValidateRequest="false" EnableEventValidation="false" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master"
    Title="E-commerce Configuration - Invoice template" CodeFile="InvoiceTemplate_Edit.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/PageElements/PageTitle.ascx" TagName="PageTitle"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/AdminControls/Controls/MetaFiles/FileList.ascx" TagName="FileList"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/UI/Macros/MacroSelector.ascx" TagName="MacroSelector"
    TagPrefix="cms" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <ajaxToolkit:ToolkitScriptManager ID="manScript" runat="server" />
    <asp:Label ID="lblInfo" runat="server" CssClass="InfoLabel" EnableViewState="false"
        Visible="false"></asp:Label>
    <asp:Label ID="lblGlobalInfo" runat="server" Visible="false" EnableViewState="false"
        Font-Bold="true" CssClass="InfoLabel" />
    <cms:CMSHtmlEditor ID="htmlInvoiceTemplate" runat="server" Width="770px" Height="400px"
        Toolbar="Invoice" />
    <br />
    <table>
        <tr>
            <td class="PageHeaderLine">
                <asp:Label ID="lblInsertMacro" runat="server" EnableViewState="false" /><br />
            </td>
            <td class="PageHeaderLine">
                <cms:MacroSelector ID="macroSelectorElm" runat="server" IsLiveSite="false" />
            </td>
        </tr>
    </table>
    <br />
    <a href="#" onclick="document.getElementById('moreMacros').style.display = (document.getElementById('moreMacros').style.display == 'none')? 'block' : 'none';">
        <asp:Label ID="lnkMoreMacros" runat="server" /><br />
        <br />
    </a>
    <div id="moreMacros" style="display: none;">
        <strong>
            <asp:Label ID="lblMoreInfo" runat="server" CssClass="InfoLabel" EnableViewState="false" /></strong>
        <asp:Table ID="tblMore" runat="server" EnableViewState="false" GridLines="Horizontal"
            CellPadding="3" CellSpacing="0" Width="100%" CssClass="UniGridGrid">
        </asp:Table>
    </div>
    <asp:PlaceHolder ID="plcAttachments" runat="server">
        <br />
        <br />
        <cms:PageTitle ID="AttachmentTitle" runat="server" TitleCssClass="SubTitleHeader"
            EnableViewState="false" />
        <br />
        <cms:FileList ID="AttachmentList" runat="server" />
    </asp:PlaceHolder>
</asp:Content>
