<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_Membership_Controls_MyProfile" CodeFile="MyProfile.ascx.cs" %>

<asp:Label ID="lblInfo" runat="server" Visible="false" EnableViewState="false" />
<asp:Label ID="lblError" runat="server" ForeColor="red" Visible="false" EnableViewState="false" />

<asp:Panel ID="RegForm" runat="server" CssClass="MyProfilePanel">    
    <cms:DataForm ID="editProfileForm" runat="server" ClassName="cms.user" />    
</asp:Panel>
