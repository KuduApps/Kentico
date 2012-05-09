<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSAdminControls_UI_Macros_MacroBoolExpression"
    CodeFile="MacroBoolExpression.ascx.cs" %>
<%@ Register Src="~/CMSAdminControls/UI/Macros/MacroTreeEditor.ascx" TagName="MacroTreeEditor"
    TagPrefix="cms" %>
<table>
    <tr>
        <td style="vertical-align: middle">
            <cms:MacroTreeEditor ID="leftOperand" ShortID="lo" runat="server" MixedMode="false" />
        </td>
        <td>
            <asp:DropDownList runat="server" ID="drpOperator" EnableViewState="false" />
        </td>
        <td>
            <asp:Panel runat="server" ID="pnlRightOperand">
                <cms:MacroTreeEditor ID="rightOperand" ShortID="ro" runat="server" MixedMode="false" />
            </asp:Panel>
        </td>
    </tr>
</table>
