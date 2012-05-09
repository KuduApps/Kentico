<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HierarchicalTransformations_List.ascx.cs"
    Inherits="CMSModules_DocumentTypes_Controls_HierarchicalTransformations_List" %>
<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<asp:Panel runat="server" ID="pnlFilter">
    <table cellspacing="3">
        <tr>
            <td class="HierarchicalTransformationFilterLabelWidth">
                <cms:LocalizedLabel runat="server" ID="lblDocTypes" ResourceString="development.documenttypes" />
            </td>
            <td>
                <cms:CMSTextBox runat="server" ID="txtDocTypes" CssClass="TextBoxField " />
            </td>
        </tr>
        <tr>
            <td>
                <cms:LocalizedLabel runat="server" ID="lblLevel" ResourceString="development.level" />
            </td>
            <td>
                <cms:CMSTextBox runat="server" ID="txtLevel" CssClass="TextBoxField" />
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                <cms:LocalizedButton runat="server" ID="btnShow" CssClass="ContentButton" ResourceString="general.show" />
            </td>
        </tr>
    </table>
</asp:Panel>
<br />
<div >
    <cms:UniGrid runat="server" ID="ugTransformations" IsLiveSite="false" ExportFileName="cms_transformation"/>
</div>
