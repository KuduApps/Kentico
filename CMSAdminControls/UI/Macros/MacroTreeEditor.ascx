<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSAdminControls_UI_Macros_MacroTreeEditor"
    CodeFile="MacroTreeEditor.ascx.cs" %>
<%@ Register Src="~/CMSAdminControls/UI/Macros/MacroEditor.ascx" TagName="MacroEditor"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/UI/Trees/MacroTree.ascx" TagName="MacroTree"
    TagPrefix="cms" %>
<asp:PlaceHolder runat="server" ID="plcControl">
    <div class="TreeEditor" onmouseout="macroTreeHasFocus = false;" onmouseover="macroTreeHasFocus = true;">
        <table cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <cms:MacroEditor ID="editorElem" runat="server" MixedMode="false" />
                </td>
                <td>
                    <asp:ImageButton runat="server" ID="btnShowTree" />
                </td>
            </tr>
        </table>
    </div>
    <asp:Panel runat="server" ID="pnlMacroTree" CssClass="MacroTreeEditor">
        <cms:MacroTree ID="treeElem" ShortID="t" runat="server" MixedMode="false" MacroExpression="CMSContext.Current" />
    </asp:Panel>
</asp:PlaceHolder>
