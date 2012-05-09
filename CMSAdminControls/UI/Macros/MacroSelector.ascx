<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSAdminControls_UI_Macros_MacroSelector"
    CodeFile="MacroSelector.ascx.cs" %>
<%@ Register Src="~/CMSAdminControls/UI/Macros/MacroTreeEditor.ascx" TagName="MacroTreeEditor"
    TagPrefix="cms" %>
<table cellspacing="0" cellspacing="0">
    <tr>
        <td>
            <cms:MacroTreeEditor runat="server" ID="macroElem" />
        </td>
        <td style="vertical-align: middle;">
            <cms:LocalizedButton ID="btnInsert" runat="server" CssClass="ContentButton" ResourceString="macroselector.insert" />
        </td>
    </tr>
</table>
<asp:Label ID="lblError" runat="server" EnableViewState="false" />
