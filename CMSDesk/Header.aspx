<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSDesk_Header" Theme="Default"
    MasterPageFile="~/CMSMasterPages/UI/EmptyPage.master" Title="CMSDesk - Header"
    EnableEventValidation="false" CodeFile="Header.aspx.cs" %>

<%@ Register TagPrefix="cms" Namespace="CMS.UIControls" Assembly="CMS.UIControls" %>
<%@ Register Src="~/CMSFormControls/Sites/SiteSelector.ascx" TagName="SiteSelector"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/UI/UniSelector/UniSelector.ascx" TagName="UniSelector"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/UI/HeaderLinks.ascx" TagName="HeaderLinks"
    TagPrefix="cms" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:Literal ID="ltlFBConnectScript" runat="server" EnableViewState="false" />
    <asp:Panel runat="server" ID="pnlBody" CssClass="CMSDeskHeader">
        <asp:HyperLink ID="lnkSiteManagerLogo" runat="server" CssClass="HeaderLeft" EnableViewState="false"
            Font-Underline="false">
            &nbsp;            
        </asp:HyperLink>
        <asp:Panel runat="server" ID="pnlTabs" CssClass="HeaderTabs" EnableViewState="false">
            <cms:UITabs ID="BasicTabControlHeader" ShortID="t" runat="server" RenderLinks="true"
                ModuleName="CMS.Desk" ModuleAvailabilityForSiteRequired="true" permissiontodisplaymodulerequired="true" />
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlRight" CssClass="HeaderRight">
            <table cellpadding="0" cellspacing="0" class="RightAlign">
                <tr>
                    <td style="padding: 0px 10px;">
                        <asp:HyperLink ID="lnkTestingMode" CssClass="HeaderLink" runat="server" Visible="false" />
                    </td>
                    <td style="padding: 0px 10px;">
                        <asp:HyperLink ID="lnkLiveSite" CssClass="HeaderLink" runat="server" EnableViewState="false" />
                    </td>
                    <td>
                        <asp:HyperLink ID="lnkSiteManager" CssClass="HeaderLink" runat="server" Visible="false"
                            EnableViewState="false" />
                    </td>
                    <td style="padding: 0px 10px; vertical-align: middle;">
                        <cms:LocalizedLabel ID="lblSite" runat="server" CssClass="HeaderSite" ResourceString="general.site"
                            DisplayColon="true" Visible="false" />
                        <cms:SiteSelector ID="siteSelector" ShortID="ss" runat="server" IsLiveSite="false" />
                    </td>
                    <td style="padding: 0px 5px;">
                        <asp:Label ID="lblUser" runat="server" CssClass="HeaderUser" EnableViewState="false" Visible="false" />
                    </td>
                    <td>
                        <asp:Label ID="lblUserInfo" runat="server" CssClass="HeaderUserInfo" EnableViewState="false" />
                    </td>
                    <asp:PlaceHolder runat="server" ID="pnlUsers" Visible="false">
                        <td>
                            <div class="Hidden">
                                <cms:UniSelector ID="ucUsers" ShortID="us" ObjectType="CMS.User" ResourcePrefix="selectuser"
                                    runat="server" ReturnColumnName="UserName" SelectionMode="SingleButton" IsLiveSite="false"
                                    DisplayNameFormat="##USERDISPLAYFORMAT##" />
                            </div>
                            &nbsp;
                            <cms:ContextMenuContainer runat="server" ID="menuCont">
                                <asp:ImageButton runat="server" ID="imgImpersonate" />
                            </cms:ContextMenuContainer>
                        </td>
                    </asp:PlaceHolder>
                    <td style="padding: 0px 10px;">
                        <asp:Label runat="server" ID="lblVersion" EnableViewState="false" CssClass="HeaderVersion" />
                    </td>
                    <cms:HeaderLinks ID="elemLinks" runat="server" />
                    <td>
                        <asp:Panel runat="server" ID="pnlSignOut" CssClass="HeaderSignOutPnl">
                            <asp:LinkButton runat="server" ID="lnkSignOut" OnClick="btnSignOut_Click" Font-Underline="false"
                                EnableViewState="false">
                                <asp:Label runat="server" ID="lblSignOut" EnableViewState="false" CssClass="HeaderSignOut"
                                    Font-Underline="false" />
                            </asp:LinkButton>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlExtraIcons" Visible="false" CssClass="ExtraIcons">
            <asp:Image runat="server" ID="imgEnterpriseSolution" Visible="false" /> <asp:Image
                runat="server" ID="imgWindowsAzure" Visible="false" />
        </asp:Panel>
    </asp:Panel>
    <asp:Literal ID="ltlScript" runat="server" EnableViewState="false" />

    <script type="text/javascript">
        //<![CDATA[
        function SetActivePage() {
        }

        function SiteRedirect(url) {
            parent.location = url;
        }

        function CheckChanges() {
            try { if (!parent.frames['cmsdesktop'].CheckChanges()) return false; } catch (ex) { }
            return true;
        }
        //]]>
    </script>

</asp:Content>
