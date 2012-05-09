<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LiveCategorySelection.aspx.cs"
    Inherits="CMSModules_Categories_CMSPages_LiveCategorySelection" Title="Live Selection Dialog"
    ValidateRequest="false" Theme="default" MasterPageFile="~/CMSMasterPages/LiveSite/Dialogs/ModalDialogPage.master" %>

<%@ Register Src="~/CMSModules/Categories/Controls/CategorySelectionDialog.ascx"
    TagName="SelectionDialog" TagPrefix="cms" %>
<asp:Content ID="cntHeaderActions" runat="server" ContentPlaceHolderID="plcTitleActions">
    <div class="Categories">
        <cms:CMSUpdatePanel ID="pnlUpdateActions" runat="server">
            <ContentTemplate>
                <asp:Image ID="imgNewCategory" runat="server" CssClass="NewItemImage FloatLeft" /><asp:LinkButton
                    runat="server" ID="lnkNew" CssClass="FloatLeft NewItemLink" /><span class="LeftAlign"
                        style="width: 14px;">&nbsp;</span>
                <asp:Image ID="imgDeleteCategory" runat="server" CssClass="NewItemImage FloatLeft" /><asp:LinkButton
                    runat="server" ID="lnkDelete" OnClientClick="return deleteConfirm();" CssClass="FloatLeft NewItemLink" /><span
                        class="LeftAlign" style="width: 14px;">&nbsp;</span>
                <asp:Image ID="imgEdit" runat="server" CssClass="NewItemImage FloatLeft" /><asp:LinkButton
                    runat="server" ID="lnkEdit" CssClass="FloatLeft NewItemLink" /><span class="LeftAlign"
                        style="width: 14px;">&nbsp;</span>
                <asp:Image ID="imgMoveUp" runat="server" CssClass="NewItemImage FloatLeft" /><asp:LinkButton
                    runat="server" ID="lnkUp" CssClass="FloatLeft NewItemLink" /><span class="LeftAlign"
                        style="width: 14px;">&nbsp;</span>
                <asp:Image ID="imgMoveDown" runat="server" CssClass="NewItemImage FloatLeft" /><asp:LinkButton
                    runat="server" ID="lnkDown" CssClass="FloatLeft NewItemLink" /><span class="LeftAlign"
                        style="width: 14px;">&nbsp;</span>
                <asp:Image ID="imgExpand" runat="server" CssClass="NewItemImage FloatLeft" /><asp:LinkButton
                    runat="server" ID="lnkExpandAll" CssClass="FloatLeft NewItemLink" /><span class="LeftAlign"
                        style="width: 14px;">&nbsp;</span>
                <asp:Image ID="imgCollapse" runat="server" CssClass="NewItemImage FloatLeft" /><asp:LinkButton
                    runat="server" ID="lnkCollapseAll" CssClass="FloatLeft NewItemLink" />
            </ContentTemplate>
        </cms:CMSUpdatePanel>
    </div>
    <div style="clear: both;">
    </div>
</asp:Content>
<asp:Content ID="cntContent" ContentPlaceHolderID="plcContent" runat="Server">
    <cms:SelectionDialog ID="SelectionElem" runat="server" IsLiveSite="true" />
</asp:Content>
<asp:Content ID="cntFooter" ContentPlaceHolderID="plcFooter" runat="server">
    <div class="FloatRight">
        <cms:LocalizedButton ID="btnOk" runat="server" CssClass="SubmitButton" EnableViewState="False"
            ResourceString="general.ok" /><cms:LocalizedButton ID="btnCancel" runat="server"
                CssClass="SubmitButton" EnableViewState="False" ResourceString="general.cancel" />
    </div>
</asp:Content>
