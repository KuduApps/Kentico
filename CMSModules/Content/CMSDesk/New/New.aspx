<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Content_CMSDesk_New_New"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Content - New"
    CodeFile="New.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<%@ Register TagPrefix="cms" Namespace="CMS.UIControls" Assembly="CMS.UIControls" %>
<asp:Content ContentPlaceHolderID="plcContent" runat="server">
    <asp:Label ID="lblInfo" runat="server" CssClass="ContentLabel" EnableViewState="false"
        Font-Bold="true" /><br />
    <asp:Label ID="lblError" runat="server" CssClass="ContentLabel" ForeColor="Red" EnableViewState="false" />
    <br />
    <div class="ContentNewClasses UniGridClearPager">
        <cms:UniGrid runat="server" ID="gridClasses" GridName="new.xml" IsLiveSite="false"
            ZeroRowsText="" />
    </div>
    <br />
    <cms:UIPlaceHolder runat="server" ID="plcNewLinkNew" ElementName="New" ModuleName="CMS.Content">
        <cms:UIPlaceHolder runat="server" ID="plcNewLink" ElementName="New.LinkExistingDocument"
            ModuleName="CMS.Content">
            <asp:Panel runat="server" ID="pnlFooter" CssClass="PageSeparator">
                <asp:HyperLink runat="server" ID="lnkNewLink" CssClass="ContentNewLink" EnableViewState="false">
                    <asp:Image ID="imgNewLink" runat="server" Width="16" EnableViewState="false" />
                    <asp:Label ID="lblNewLink" runat="server" EnableViewState="false" />
                </asp:HyperLink>
            </asp:Panel>
        </cms:UIPlaceHolder>
    </cms:UIPlaceHolder>
    <cms:UIPlaceHolder runat="server" ID="plcNewABTestVariant" ElementName="New" ModuleName="CMS.Content">
        <cms:UIPlaceHolder runat="server" ID="plcNewVariantLink" ElementName="New.ABTestVariant" ModuleName="CMS.Content">
            <asp:Panel runat="server" ID="pnlABVariant" EnableViewState="false">
                <asp:HyperLink runat="server" ID="lnkNewVariant" CssClass="ContentNewLink" EnableViewState="false">
                    <asp:Image ID="imgNewVariant" runat="server" Width="16" EnableViewState="false" />
                    <asp:Label ID="lblNewVariant" runat="server" EnableViewState="false" />
                </asp:HyperLink>
            </asp:Panel>
        </cms:UIPlaceHolder>
    </cms:UIPlaceHolder>
</asp:Content>
