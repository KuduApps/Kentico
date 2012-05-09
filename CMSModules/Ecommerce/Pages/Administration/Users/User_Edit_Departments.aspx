<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Ecommerce_Pages_Administration_Users_User_Edit_Departments"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="User Edit - Departments"
    CodeFile="User_Edit_Departments.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/UniSelector/UniSelector.ascx" TagName="UniSelector"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSFormControls/Sites/SiteSelector.ascx" TagName="SiteSelector"
    TagPrefix="cms" %>
<asp:Content ID="cntSiteSelect" runat="server" ContentPlaceHolderID="plcSiteSelector">
    <asp:PlaceHolder ID="plcSites" runat="server">
        <div style="padding-bottom: 0px;">
            <cms:LocalizedLabel ID="lblSelectSite" runat="server" ResourceString="general.site"
                DisplayColon="true" />&nbsp;
            <cms:SiteSelector ID="siteSelector" runat="server" IsLiveSite="false" />
        </div>
    </asp:PlaceHolder>
</asp:Content>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <cms:CMSUpdatePanel ID="pnlUpdate" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
                Visible="false" />
            <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
                Visible="false" />
            <asp:PlaceHolder ID="plcTable" runat="server"><strong>
                <cms:LocalizedLabel runat="server" ID="lblDepartmentInfo" DisplayColon="true" ResourceString="com.departments.userdepartments"
                    EnableViewState="false" CssClass="InfoLabel" /></strong>
                <cms:UniSelector ID="uniSelector" runat="server" IsLiveSite="false" OrderBy="DepartmentName"
                    ObjectType="ecommerce.department" SelectionMode="Multiple" ResourcePrefix="departmentsselector" />
            </asp:PlaceHolder>
        </ContentTemplate>
    </cms:CMSUpdatePanel>
</asp:Content>
