<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_Membership_FormControls_Users_SelectUsers" CodeFile="SelectUsers.ascx.cs" %>

<%@ Register Src="~/CMSModules/Membership/FormControls/Users/selectuser.ascx" TagName="SelectUser" TagPrefix="cms" %>

<cms:SelectUser ID="selectUser" runat="server" SelectionMode="MultipleTextBox" />
