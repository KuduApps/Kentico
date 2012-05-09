<%@ Page Language="C#" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" AutoEventWireup="true" Inherits="CMSModules_Groups_Tools_Forums_Groups_ForumGroups_List"
    Title="Forum Groups List" Theme="Default" CodeFile="ForumGroups_List.aspx.cs" %>
<%@ Register Src="~/CMSModules/Forums/Controls/Forums/ForumGroupList.ascx" TagName="ForumGroupsList" TagPrefix="cms" %>

<asp:Content ID="Content3" ContentPlaceHolderID="plcContent" runat="Server">
    <cms:ForumGroupsList ID="forumGroupsList" runat="server" IsLiveSite="false" />
</asp:Content>
