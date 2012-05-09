<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SettingsGroup.ascx.cs"
    Inherits="CMSModules_Settings_Controls_SettingsGroup" %>
<%@ Register TagPrefix="cms" Namespace="CMS.UIControls" Assembly="CMS.UIControls" %>
<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UGrid" TagPrefix="cms" %>
<cms:CategoryPanel ID="cpCategory" runat="server" Text="Some Category Panel" AllowCollapsing="false"
    DisplayActions="true">
    <cms:CategoryPanelRow ID="cprRow01" runat="server" IsRequired="true" LabelTitle="" ShowFormLabelCell="false">
        <div class="NewLink">
            <asp:Image ID="imgNewKey" runat="server" EnableViewState="false" />
            <asp:LinkButton ID="lnkNewKey" runat="server" class="NewLinkText" EnableViewState="false" />
        </div>
    </cms:CategoryPanelRow>
    <cms:CategoryPanelRow ID="cprRow02" runat="server" IsRequired="true" LabelTitle="" ShowFormLabelCell="false">
        <asp:Label ID="lblError" runat="server" CssClass="ErrorLabel" EnableViewState="false"
            Visible="false" />
        <cms:UGrid runat="server" ID="gridElem" GridName="~/CMSModules/Settings/Controls/SettingsGroup_List.xml"
            OrderBy="KeyOrder" IsLiveSite="false" />
    </cms:CategoryPanelRow>
</cms:CategoryPanel>
