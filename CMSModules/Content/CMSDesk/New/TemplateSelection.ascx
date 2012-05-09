<%@ Control Language="C#" AutoEventWireup="True" CodeFile="TemplateSelection.ascx.cs"
    Inherits="CMSModules_Content_CMSDesk_New_TemplateSelection" %>
<%@ Register Src="~/CMSModules/PortalEngine/Controls/Layout/PageTemplateSelector.ascx"
    TagName="PageTemplateSelector" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/PortalEngine/Controls/Layout/LayoutFlatSelector.ascx"
    TagName="LayoutFlatSelector" TagPrefix="cms" %>
<%@ Register TagPrefix="cms" Namespace="CMS.UIControls" Assembly="CMS.UIControls" %>
<cms:CMSUpdatePanel ID="pnlUpdate" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <cms:UIPlaceHolder runat="server" ID="plcRadioButtonsNew" ElementName="New" ModuleName="CMS.Content">
            <cms:UIPlaceHolder runat="server" ID="plcRadioButtons" ElementName="New.SelectTemplate"
                ModuleName="CMS.Content">
                <div class="RadioPanel">
                    <cms:UIPlaceHolder runat="server" ID="plcUseTemplate" ElementName="SelectTemplate.UseTemplate"
                        ModuleName="CMS.Content">
                        <cms:LocalizedRadioButton ID="radUseTemplate" runat="server" GroupName="NewPage"
                            CssClass="LeftAlign" ResourceString="NewPage.UseTemplate" AutoPostBack="true" />
                    </cms:UIPlaceHolder>
                    <cms:UIPlaceHolder runat="server" ID="plcInherit" ElementName="SelectTemplate.InheritFromParent"
                        ModuleName="CMS.Content">
                        <cms:LocalizedRadioButton ID="radInherit" CssClass="LeftAlign" runat="server" GroupName="NewPage"
                            ResourceString="NewPage.Inherit" AutoPostBack="true" />
                    </cms:UIPlaceHolder>
                    <cms:UIPlaceHolder runat="server" ID="plcCreateBlank" ElementName="SelectTemplate.CreateBlank"
                        ModuleName="CMS.Content">
                        <cms:LocalizedRadioButton ID="radCreateBlank" CssClass="LeftAlign" runat="server"
                            GroupName="NewPage" ResourceString="NewPage.CreateBlank" AutoPostBack="true"
                            Checked="false" />
                    </cms:UIPlaceHolder>
                    <cms:UIPlaceHolder runat="server" ID="plcCreateEmpty" ElementName="SelectTemplate.CreateEmpty"
                        ModuleName="CMS.Content">
                        <cms:LocalizedRadioButton ID="radCreateEmpty" CssClass="LeftAlign" runat="server"
                            GroupName="NewPage" ResourceString="NewPage.CreateEmpty" AutoPostBack="true"
                            Checked="false" />
                    </cms:UIPlaceHolder>
                </div>
            </cms:UIPlaceHolder>
        </cms:UIPlaceHolder>
        <div class="TemplateSelectorWrap">
            <asp:PlaceHolder ID="plcTemplateSelector" runat="server">
                <cms:PageTemplateSelector ID="templateSelector" runat="server" Mode="newpage" ShowEmptyCategories="false"
                    IsLiveSite="false" IsNewPage="true" />
            </asp:PlaceHolder>
            <asp:PlaceHolder ID="plcInherited" runat="server">
                <div class="ItemSelector">
                    <div class="InheritedTemplate">
                        <cms:LocalizedLabel ID="lblIngerited" runat="server" EnableViewState="false" />
                    </div>
            </asp:PlaceHolder>
            <asp:PlaceHolder ID="plcLayout" runat="server">
                <asp:PlaceHolder ID="plcLayoutSelector" runat="server">
                    <cms:LayoutFlatSelector ID="layoutSelector" runat="server" IsLiveSite="false" />
                </asp:PlaceHolder>
                <div class="CopyLayoutPanel">
                    <cms:LocalizedCheckBox runat="server" ID="chkLayoutPageTemplate" Checked="true" ResourceString="NewPage.LayoutPageTemplate" />
                </div>
            </asp:PlaceHolder>
        </div>
    </ContentTemplate>
</cms:CMSUpdatePanel>
