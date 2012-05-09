<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_Content_Controls_Dialogs_Properties_BBLinkProperties" CodeFile="BBLinkProperties.ascx.cs" %>
<%@ Register Src="~/CMSModules/Content/Controls/Dialogs/General/WidthHeightSelector.ascx" TagPrefix="cms"
    TagName="WidthHeightSelector" %>
<%@ Register Src="~/CMSModules/Content/Controls/Dialogs/General/URLSelector.ascx" TagPrefix="cms" TagName="URLSelector" %>
<div class="BBLinkProperties" enableviewstate="false">
    <asp:Panel runat="server" ID="pnlEmpty" Visible="true" CssClass="DialogInfoArea">
        <asp:Label runat="server" ID="lblEmpty" EnableViewState="false"/>
    </asp:Panel>
    <cms:JQueryTabContainer ID="pnlTabs" runat="server" CssClass="DialogElementHidden">
        <cms:JQueryTab ID="tabGeneral" runat="server">
            <ContentTemplate>
                <cms:URLSelector runat="server" ID="urlSelectElem" />
                <cms:CMSButton ID="btnHidden" runat="server" CssClass="HiddenButton" EnableViewState="false" />
            </ContentTemplate>
        </cms:JQueryTab>
    </cms:JQueryTabContainer>
</div>
