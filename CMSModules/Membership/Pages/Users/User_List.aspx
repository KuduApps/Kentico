<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Membership_Pages_Users_User_List"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Users - User List" CodeFile="User_list.aspx.cs" %>
<%@ Register src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" tagname="UniGrid" tagprefix="cms" %>

<%@ Register Src="~/CMSModules/Membership/Controls/Users/UserFilter.ascx" TagName="UserFilter"
    TagPrefix="cms" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <cms:UserFilter ID="userFilterElem" runat="server" />
    <br />
    <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
        Visible="false" />
    <cms:UniGrid ID="gridElem" runat="server" GridName="User_List.xml" OrderBy="UserName"
        Columns="UserID, UserName, FullName, Email, UserNickName, UserCreated, UserEnabled, (CASE WHEN UserPassword IS NULL OR UserPassword = '' THEN 0 ELSE 1 END) AS UserHasPassword, UserIsGlobalAdministrator, UserIsExternal"
        IsLiveSite="false" />
</asp:Content>
