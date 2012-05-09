<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Membership_Pages_Users_General_User_Online"
    MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Theme="Default" Title="Users - online users" CodeFile="User_Online.aspx.cs" %>
<%@ Register src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" tagname="UniGrid" tagprefix="cms" %>

<%@ Register Src="~/CMSModules/Membership/Controls/Users/UserFilter.ascx" TagName="UserFilter"
    TagPrefix="cms" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <cms:LocalizedLabel ID="lblDisabled" runat="server" Visible="False" EnableViewState="false"
        ResourceString="administration.users.oline.disabled" />
    <cms:UserFilter ID="userFilterElem" runat="server" />
    <br />
    <cms:UniGrid ID="gridElem" runat="server" GridName="User_Online.xml" OrderBy="UserName"
        Columns="UserID, UserName, FullName, Email, UserNickName, UserCreated, UserEnabled, UserIsGlobalAdministrator"
        IsLiveSite="false"  ShowObjectMenu="false" />
    <br />
    <asp:Label runat="server" ID="lblGeneralInfo" EnableViewState="false" CssClass="UsersOnline"></asp:Label>
</asp:Content>
