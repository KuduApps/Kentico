<%@ Page Language="C#" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" AutoEventWireup="true" Inherits="CMSModules_Groups_Tools_Forums_Forums_Forums_List"
    Title="Group forums" Theme="Default" CodeFile="Forums_List.aspx.cs" %>

<%@ Register Src="~/CMSModules/Forums/Controls/Forums/ForumList.ascx" TagName="ForumList"
    TagPrefix="cms" %>
<asp:Content ID="Content3" ContentPlaceHolderID="plcContent" runat="Server">
    <cms:ForumList ID="forumList" runat="server" Visible="true" />
</asp:Content>
