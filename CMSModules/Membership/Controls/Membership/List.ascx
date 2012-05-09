<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_Membership_Controls_Membership_List"
    CodeFile="List.ascx.cs" %>
<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<asp:Label ID="lblError" runat="server" CssClass="ErrorLabel" Visible="false" EnableViewState="false" />
<cms:UniGrid ID="gridElem" runat="server" GridName="~/CMSModules/Membership/Controls/Membership/List.xml"
    OrderBy="MembershipDisplayName" Columns="MembershipID,MembershipDisplayName" />
