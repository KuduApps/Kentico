<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSAdminControls_UI_Macros_MacroDesigner"
    CodeFile="MacroDesigner.ascx.cs" %>
<%@ Register Src="~/CMSAdminControls/UI/Macros/MacroDesignerGroup.ascx" TagName="MacroDesignerGroup"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/UI/Macros/MacroEditor.ascx" TagName="MacroEditor"
    TagPrefix="cms" %>
<asp:Literal runat="server" ID="ltlScript" EnableViewState="false" />
<asp:Panel runat="server" ID="pnlBody" CssClass="TabsHeaderInline">
    <asp:Panel runat="server" ID="pnlTabsContainer" CssClass="TabsPageTabs LightTabs"
        EnableViewState="false">
        <asp:Panel runat="server" ID="pnlLeft" CssClass="FullTabsLeft">
            &nbsp;
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlTabs" CssClass="TabsTabs">
            <asp:Panel runat="server" ID="pnlWhite" CssClass="Tabs">
                <cms:BasicTabControl runat="server" ID="tabsElem" />
            </asp:Panel>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlRight" CssClass="FullTabsRight">
            &nbsp;
        </asp:Panel>
    </asp:Panel>
</asp:Panel>
<asp:Panel runat="server" ID="pnlDesigner" CssClass="MacroDesigner">
    <cms:MacroDesignerGroup runat="server" ID="designerElem" ShortID="d" />
</asp:Panel>
<asp:Panel runat="server" ID="pnlEditor" CssClass="MacroDesigner" Visible="false">
    <cms:MacroEditor runat="server" ID="editorElem" UseAutoComplete="true" MixedMode="false"
        Width="100%" Height="520px" />
</asp:Panel>
<asp:Button runat="server" ID="btnShowDesigner" CssClass="HiddenButton" EnableViewState="false" />
<asp:Button runat="server" ID="btnShowCode" CssClass="HiddenButton" EnableViewState="false" />
<asp:Button runat="server" ID="btnMoveGroup" CssClass="HiddenButton" EnableViewState="false" />
<asp:HiddenField runat="server" ID="hdnMove" EnableViewState="false" />
<asp:HiddenField runat="server" ID="hdnSelTab" EnableViewState="false" />
