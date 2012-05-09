<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ResetPassword.aspx.cs" Inherits="CMSModules_Membership_CMSPages_ResetPassword" 
MasterPageFile="~/CMSMasterPages/LiveSite/SimplePage.master" Theme="Default" Title="ResetPassword"%>

<%@ Register src="~/CMSModules/Membership/Controls/ResetPassword.ascx" tagname="ResetPassword" tagprefix="cms" %>

<asp:Content ContentPlaceHolderID="plcContent" runat="server" >
    <cms:ResetPassword ID="resetPasswordItem" runat="server" />
</asp:Content>
