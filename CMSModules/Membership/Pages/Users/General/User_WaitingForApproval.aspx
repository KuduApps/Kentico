<%@ Page Language="C#" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" AutoEventWireup="true" Inherits="CMSModules_Membership_Pages_Users_General_User_WaitingForApproval"
    Title="Untitled Page" Theme="Default" CodeFile="User_WaitingForApproval.aspx.cs" %>
<%@ Register src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" tagname="UniGrid" tagprefix="cms" %>

<%@ Register Src="~/CMSModules/Membership/Controls/Users/UserFilter.ascx" TagName="UserFilter"
    TagPrefix="cms" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:HiddenField ID="hdnReason" runat="server" />
    <asp:HiddenField ID="hdnSendEmail" runat="server" />
    <asp:HiddenField ID="hdnConfirmDelete" runat="server" />
    <asp:HiddenField ID="hdnUser" runat="server" />
    <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
        Visible="false" />
    <cms:UserFilter ID="userFilterElem" runat="server" />
    <br />
    <div>
        <cms:LocalizedButton ID="btnApproveAllSelected" runat="server" OnClick="btnApproveAllClick"
            ResourceString="administration.users.approveallselected" CssClass="XLongButton" />
        <cms:LocalizedButton ID="btnRejectAllSelected" runat="server" OnClick="btnRejectAllClick"
            ResourceString="administration.users.rejectallselected" CssClass="XLongButton" />
    </div>
    <br />
    <cms:UniGrid ID="gridElem" runat="server" GridName="../User_List_Approval.xml" OrderBy="UserName"
        Columns="UserID, UserName, FullName, Email, UserNickName, UserCreated, UserEnabled"
        IsLiveSite="false" ShowObjectMenu="false" />
    <asp:Literal ID="ltlScript" runat="server" />
    <cms:CMSButton ID="btnUpdate" runat="server" Text="Button" CssClass="HiddenButton" OnClick="btnUpdate_Click" />
</asp:Content>
