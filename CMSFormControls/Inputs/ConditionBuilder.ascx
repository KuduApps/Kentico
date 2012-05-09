<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSFormControls_Inputs_ConditionBuilder"
    CodeFile="ConditionBuilder.ascx.cs" %>
<%@ Register Src="~/CMSAdminControls/UI/Macros/MacroEditor.ascx" TagName="MacroEditor"
    TagPrefix="cms" %>
<table cellpadding="0" cellspacing="0">
    <tr>
        <td>
            <div style="padding-top: 1px; width: 297px;">
                <cms:MacroEditor runat="server" ID="txtMacro" CssClass="TextBoxField" />
            </div>
        </td>
        <td valign="top">
            <asp:Panel ID="pnlButtons" runat="server" CssClass="LocalizablePanel">
                <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" CssClass="LocalizableIcon"
                    EnableViewState="false" />
            </asp:Panel>
        </td>
    </tr>
</table>
