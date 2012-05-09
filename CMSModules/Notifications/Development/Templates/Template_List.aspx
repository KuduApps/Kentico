<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Notifications_Development_Templates_Template_List"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Template list" CodeFile="Template_List.aspx.cs" %>
<%@ Register src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" tagname="UniGrid" tagprefix="cms" %>

<%@ Register Src="~/CMSFormControls/Sites/SiteSelector.ascx" TagName="SiteSelector" TagPrefix="cms" %>
<asp:Content ContentPlaceHolderID="plcSiteSelector" runat="server">
    <table>
        <tr>
            <td>
                <cms:LocalizedLabel ID="lblSites" runat="server" DisplayColon="true" ResourceString="general.site"
                    CssClass="FieldLabel" EnableViewState="false" />
            </td>
            <td>
                <cms:SiteSelector runat="server" ID="siteSelector" AllowAll="false" OnlyRunningSites="false" />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="cntBody" ContentPlaceHolderID="plcContent" runat="Server">
    <cms:CMSUpdatePanel runat="server" ID="pnlUpdate" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Label ID="lblInfo" runat="server" CssClass="InfoLabel" Visible="false" EnableViewState="false" />
            <asp:Label ID="lblError" runat="server" Visible="false" CssClass="ErrorLabel" EnableViewState="false" />
            <cms:UniGrid ID="gridTemplates" runat="server" GridName="Template_List.xml" IsLiveSite="false" Columns="TemplateID, TemplateDisplayName" />
        </ContentTemplate>
    </cms:CMSUpdatePanel>
</asp:Content>
