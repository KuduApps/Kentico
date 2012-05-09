<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_OnlineMarketing_Controls_Content_CombinationPanel"
    CodeFile="CombinationPanel.ascx.cs" %>
<%@ Register Src="~/CMSModules/OnlineMarketing/FormControls/SelectMVTCombination.ascx"
    TagName="CombinationSelector" TagPrefix="cms" %>
<cms:CMSUpdateProgress ID="loading" runat="server" HandlePostback="true" EnableViewState="false" />
<asp:Panel ID="pnlMvtCombination" runat="server" CssClass="MVTCombinationPanel">
    <div class="FloatLeft">
        <cms:LocalizedLabel ID="lblCombination" runat="server" AssociatedControlID="" 
            DisplayColon="true" CssClass="mvtCombinationName" />
    </div>
    <div class="FloatLeft">
        <cms:CombinationSelector ID="combinationSelector" runat="server" HighlightEnabled="true" />
    </div>
    <asp:PlaceHolder ID="plcEditCombination" runat="server">
        <div class="FloatLeft">
            <cms:LocalizedCheckBox ID="chkEnabled" runat="server" CssClass="MVTCombinationEnabled" />
        </div>
        <div class="FloatLeft">
            <cms:LocalizedLabel ID="lblCustomName" runat="server" DisplayColon="true" CssClass="MVTCombinationCustomName" />
        </div>
        <div class="FloatLeft">
            <cms:CMSTextBox ID="txtCustomName" runat="server" MaxLength="200" CssClass="MVTCombinationCustomNameText" />
        </div>
        <div class="FloatLeft">
            <cms:LocalizedButton ID="btnChange" runat="server" CssClass="MVTCombinationButton" />
        </div>
        <div class="FloatLeft">
            <cms:LocalizedLabel ID="lblSaved" runat="server" ></cms:LocalizedLabel>
        </div>
        <asp:PlaceHolder ID="plcUseCombination" runat="server" Visible="false">
            <div class="FloatRight">
                <cms:LocalizedButton ID="btnUseCombination" runat="server" CssClass="MVTCombinationButtonUse" />
            </div>
        </asp:PlaceHolder>
    </asp:PlaceHolder>
    <asp:HiddenField ID="hdnCurrentCombination" runat="server" />
</asp:Panel>
<asp:PlaceHolder ID="plcRunningTestWarning" runat="server" Visible="false">
    <div class="MVTCombinationPanelWarning">
        <cms:LocalizedLabel ID="lblWarning" runat="server"></cms:LocalizedLabel>
    </div>
</asp:PlaceHolder>
