<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_SmartSearch_Controls_UI_SearchIndex_CustomTable_Edit" CodeFile="SearchIndex_CustomTable_Edit.ascx.cs" %>
<%@ Register Src="~/CMSFormControls/Inputs/LargeTextArea.ascx" TagName="LargeTextArea" TagPrefix="cms" %>
<%@ Register src="~/CMSFormControls/Classes/CustomTableSelector.ascx" tagname="CustomTableSelector" tagprefix="uc1" %>
<asp:Panel ID="pnlConetnEdit" runat="server">
    <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
        Visible="false" />
    <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
        Visible="false" />
    <asp:Panel runat="server" ID="pnlDisabled" CssClass="DisabledInfoPanel" Visible="false">
        <cms:LocalizedLabel runat="server" ID="lblDisabled" EnableViewState="false" ResourceString="srch.searchdisabledinfo"
            CssClass="InfoLabel"></cms:LocalizedLabel>
    </asp:Panel>
    <table>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel runat="server" ID="lblCustomTable" EnableViewState="false" DisplayColon="true" ResourceString="srch.index.customtable"></cms:LocalizedLabel>
            </td>
            <td>
                <uc1:CustomTableSelector ID="customTableSelector" IsLiveSite="false" AllSites="true" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel runat="server" ID="lblWhere" EnableViewState="false" DisplayColon="true" ResourceString="srch.index.where"></cms:LocalizedLabel>
            </td>
            <td>
                <cms:LargeTextArea ID="txtWhere" AllowMacros="false" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
            </td>
            <td>
                <cms:LocalizedButton runat="server" ID="btnOk" CssClass="SubmitButton"  OnClick="btnOK_Click"  ResourceString="general.ok" EnableViewState="false" />
            </td>
        </tr>
    </table>
</asp:Panel>
