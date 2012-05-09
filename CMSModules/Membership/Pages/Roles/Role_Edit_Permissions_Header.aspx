<%@ Page Title="" Language="C#" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master"
    AutoEventWireup="true" CodeFile="Role_Edit_Permissions_Header.aspx.cs" Inherits="CMSModules_Membership_Pages_Roles_Role_Edit_Permissions_Header"
    Theme="Default" %>

<%@ Register Src="~/CMSFormControls/SelectModule.ascx" TagName="ModuleSelector" TagPrefix="cms" %>
<%@ Register Src="~/CMSFormControls/Classes/SelectClass.ascx" TagName="ClassSelector"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/Permissions/Controls/PermissionsFilter.ascx"
    TagName="PermissionsHeader" TagPrefix="cms" %>
<asp:Content ID="contentElem" ContentPlaceHolderID="plcContent" runat="server">
    <cms:PermissionsHeader ID="prmhdrHeader" runat="server" DisplaySiteSelector="false" />
</asp:Content>
