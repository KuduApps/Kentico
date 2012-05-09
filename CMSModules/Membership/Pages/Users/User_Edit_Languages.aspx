<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_Membership_Pages_Users_User_Edit_Languages" ValidateRequest="false"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="User Edit - Languages" CodeFile="User_Edit_Languages.aspx.cs" %>
<%@ Register src="~/CMSAdminControls/UI/UniSelector/UniSelector.ascx" tagname="UniSelector" tagprefix="cms" %>


<%@ Register Src="~/CMSFormControls/Sites/SiteSelector.ascx" TagName="SiteSelector" TagPrefix="cms" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
        Visible="false" />
    <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
        Visible="false" />
    <asp:Panel ID="pnlLanguages" runat="server">
        <table>
            <tr>
                <td>
                    <cms:LocalizedRadioButton ID="radAllLanguages" runat="server" ResourceString="transman.editallcultures"
                        GroupName="grpLanguages" CssClass="RadioButtonMovedLeft" AutoPostBack="true" />
                </td>
            </tr>
            <tr>
                <td>
                    <cms:LocalizedRadioButton ID="radSelectedLanguages" runat="server" ResourceString="transman.editselectedcultures"
                        GroupName="grpLanguages" CssClass="RadioButtonMovedLeft" AutoPostBack="true" />
                </td>
            </tr>
        </table>
        <br />
        <asp:PlaceHolder ID="plcSite" runat="server">
            <cms:LocalizedLabel ID="lblSite" runat="server" ResourceString="transman.selectsite"
                DisplayColon="true" />&nbsp;
            <cms:SiteSelector ID="siteSelector" runat="server" IsLiveSite="false" />
            <br />
            <br />
            <br />
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="plcCultures" runat="server">
            <cms:CMSUpdatePanel ID="pnlUpdate" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <cms:UniSelector ID="uniSelector" runat="server" IsLiveSite="false" ObjectType="cms.culture"
                        SelectionMode="Multiple" ResourcePrefix="languageselect" OrderBy="CultureName" />
                </ContentTemplate>
            </cms:CMSUpdatePanel>
            <br />
        </asp:PlaceHolder>
    </asp:Panel>
</asp:Content>
