<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_Forums_Controls_Forums_ForumModerators" CodeFile="ForumModerators.ascx.cs" %>
<%@ Register Src="~/CMSModules/Membership/FormControls/Users/securityAddUsers.ascx" TagName="SelectUser" TagPrefix="cms" %>

<asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
    Visible="false" />
<asp:CheckBox ID="chkForumModerated" runat="server" CssClass="CheckBoxMovedLeft"
    EnableViewState="true" AutoPostBack="true" OnCheckedChanged="chkForumModerated_CheckedChanged" />
<br />
<br />
<asp:Label ID="lblModerators" runat="server" CssClass="BoldInfoLabel" />
<cms:SelectUser ID="userSelector" runat="server" />
