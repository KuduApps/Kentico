<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSAdminControls_UI_UniSelector_UniSelector"
    CodeFile="UniSelector.ascx.cs" %>
<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<asp:Label ID="lblStatus" runat="server" EnableViewState="False" CssClass="InfoLabel" />
<asp:PlaceHolder runat="server" ID="plcButtonSelect" Visible="false" EnableViewState="false">
    <cms:LocalizedButton runat="server" ID="btnDialog" CssClass="ContentButton" /><cms:LocalizedLabel
        ID="lblDialog" runat="server" EnableViewState="false" AssociatedControlID="btnDialog"
        Display="false" />
</asp:PlaceHolder>
<asp:PlaceHolder runat="server" ID="plcImageSelect" Visible="false" EnableViewState="false">
    <asp:Image runat="server" ID="imgDialog" CssClass="ActionImage" />
    <cms:LocalizedHyperlink runat="server" ID="lnkDialog" CssClass="ActionLink" />
</asp:PlaceHolder>
<asp:PlaceHolder runat="server" ID="plcTextBoxSelect" Visible="false">
    <cms:CMSTextBox ID="txtSingleSelect" runat="server" CssClass="SelectorTextBox" ReadOnly="true" /><cms:LocalizedLabel
        ID="lblSingleSelectTxt" runat="server" EnableViewState="false" AssociatedControlID="txtSingleSelect"
        Display="false" /><cms:LocalizedButton ID="btnSelect" runat="server" CssClass="ContentButton"
            EnableViewState="False" /><cms:LocalizedLabel ID="lblSelect" runat="server" EnableViewState="false"
                AssociatedControlID="btnSelect" Display="false" /><cms:LocalizedButton ID="btnEdit"
                    runat="server" CssClass="ContentButton" Visible="false" EnableViewState="False" /><cms:LocalizedLabel
                        ID="lblEdit" runat="server" EnableViewState="false" AssociatedControlID="btnEdit"
                        Display="false" Visible="false" /><cms:LocalizedButton ID="btnNew" runat="server"
                            CssClass="ShortButton" Visible="false" EnableViewState="False" /><cms:LocalizedLabel
                                ID="lblNew" runat="server" EnableViewState="false" AssociatedControlID="btnNew"
                                Display="false" Visible="false" /><cms:LocalizedButton ID="btnClear" runat="server"
                                    CssClass="ContentButton" EnableViewState="False" /><cms:LocalizedLabel ID="lblClear"
                                        runat="server" EnableViewState="false" AssociatedControlID="btnClear" Display="false" />
</asp:PlaceHolder>
<asp:PlaceHolder runat="server" ID="plcDropdownSelect" Visible="false">
    <asp:DropDownList ID="drpSingleSelect" runat="server" CssClass="DropDownField" />
    <cms:LocalizedLabel ID="lblSingleSelectDrp" runat="server" EnableViewState="false"
        AssociatedControlID="drpSingleSelect" Display="false" /><cms:LocalizedButton runat="server"
            ID="btnDropEdit" EnableViewState="false" Visible="false" CssClass="ContentButton"
            RenderScript="true" /><cms:LocalizedLabel ID="lblDropEdit" runat="server" EnableViewState="false"
                AssociatedControlID="btnDropEdit" Display="false" Visible="false" /><cms:LocalizedButton
                    ID="btnDropNew" runat="server" CssClass="ShortButton" Visible="false" EnableViewState="False" /><cms:LocalizedLabel
                        ID="lblDropNew" runat="server" EnableViewState="false" AssociatedControlID="btnDropNew"
                        Display="false" Visible="false" /></asp:PlaceHolder>
<asp:Panel runat="server" ID="pnlGrid" CssClass="UniSelector" Visible="false">
    <asp:PlaceHolder ID="plcContextMenu" runat="server" EnableViewState="false" />
    <cms:UniGrid ID="uniGrid" ShortID="g" runat="server" ZeroRowsText="" ShowObjectMenu="false" ShowActionsMenu="false" />
    <div id="UniSelectorSpacer" runat="server" class="UniSelectorSpacer">
    </div>
    <cms:LocalizedButton ID="btnRemoveSelected" runat="server" CssClass="LongButton"
        EnableViewState="False" OnClick="btnRemoveSelected_Click" /><cms:LocalizedLabel ID="lblRemoveSelected"
            runat="server" EnableViewState="false" AssociatedControlID="btnRemoveSelected"
            Display="false" /><cms:LocalizedButton ID="btnAddItems" runat="server" CssClass="LongButton"
                EnableViewState="False" /><cms:LocalizedLabel ID="lblAddItems" runat="server" EnableViewState="false"
                    AssociatedControlID="btnAddItems" Display="false" />
    <cms:ContextMenuButton runat="server" ID="btnMenu" />
</asp:Panel>
<asp:HiddenField ID="hdnDialogSelect" runat="server" EnableViewState="false" />
<asp:HiddenField ID="hdnIdentificator" runat="server" EnableViewState="false" />
<asp:HiddenField ID="hiddenField" runat="server" EnableViewState="false" />
<asp:HiddenField ID="hiddenSelected" runat="server" EnableViewState="false" />
