<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSAdminControls_UI_Macros_MacroDesignerGroup"
    CodeFile="MacroDesignerGroup.ascx.cs" %>
<%@ Register Assembly="CMS.UIControls" Namespace="CMS.UIControls" TagPrefix="cms" %>
<asp:Literal runat="server" ID="ltlScript" EnableViewState="false" />
<asp:Panel runat="server" ID="pnlOperator" CssClass="MacroDesignerOperator">
    <asp:RadioButton runat="server" ID="radAnd" Checked="true" GroupName="Operator" Text="AND" /><asp:RadioButton
        runat="server" ID="radOr" Text="OR" GroupName="Operator" />
</asp:Panel>
<asp:Panel runat="server" ID="pnlMacroGroup" CssClass="MacroDesignerGroup">
    <asp:Panel runat="server" ID="pnlHeader" CssClass="MacroDesignerHeader">
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <asp:PlaceHolder runat="server" ID="pnlButtons">
                    <td class="MacroDesignerButtons">
                        <asp:ImageButton runat="server" ID="btnAddGroup" />&nbsp;
                        <asp:ImageButton runat="server" ID="btnAddExp" />
                    </td>
                </asp:PlaceHolder>
                <td style="width: 100%">
                    <cms:ContextMenuContainer runat="server" ID="cmcGroupHeader" MenuID="handleMenu"
                        HorizontalPosition="Cursor" RenderAsTag="Span" MouseButton="Right">
                        <asp:Panel runat="server" ID="pnlGroupHandle" CssClass="MacroElementHandle">
                            &nbsp;
                        </asp:Panel>
                    </cms:ContextMenuContainer>
                </td>
                <asp:PlaceHolder runat="server" ID="pnlRemoveGroup">
                    <td class="MacroDesignerButtons">
                        <cms:ContextMenuContainer runat="server" ID="cmcGroup" MenuID="newMenu" HorizontalPosition="Left"
                            RenderAsTag="Span" MouseButton="Both">
                            <asp:ImageButton runat="server" ID="btnGroupContextMenu" /></cms:ContextMenuContainer>
                        &nbsp;
                        <asp:ImageButton runat="server" ID="btnRemove" />
                    </td>
                </asp:PlaceHolder>
            </tr>
        </table>
    </asp:Panel>
    <table cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <asp:Panel runat="server" ID="pnlHandle" CssClass="MacroDesignerMove">
                    <asp:Panel runat="server" ID="pnlItemHandle" CssClass="MacroElementHandle">
                        <cms:ContextMenuContainer runat="server" ID="cmcExprHandle" MenuID="handleMenu" HorizontalPosition="Cursor"
                            RenderAsTag="Span" MouseButton="Right">
                            <asp:Image runat="server" ID="imgMove" EnableViewState="false" />
                        </cms:ContextMenuContainer>
                    </asp:Panel>
                </asp:Panel>
            </td>
            <td>
                <asp:PlaceHolder runat="server" ID="plcNoItems" Visible="false">
                    <br />
                    <cms:LocalizedLabel runat="server" ID="lblNoItems" ResourceString="macrodesigner.noitems"
                        CssClass="MacroDesignerChildGroups" />
                </asp:PlaceHolder>
                <cms:CMSPanel runat="server" ID="pnlGroups" ShortID="g" CssClass="MacroDesignerChildGroups">
                </cms:CMSPanel>
            </td>
            <td>
                <asp:Panel runat="server" ID="pnlRemoveExpression" CssClass="MacroDesignerRemoveButton">
                    <cms:ContextMenuContainer runat="server" ID="cmcExpr" MenuID="newMenu" HorizontalPosition="Left"
                        RenderAsTag="Span" MouseButton="Both">
                        <asp:ImageButton runat="server" ID="btnExprContextMenu" />
                    </cms:ContextMenuContainer>
                    &nbsp;
                    <asp:ImageButton runat="server" ID="btnRemoveExpression" />
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Panel>
