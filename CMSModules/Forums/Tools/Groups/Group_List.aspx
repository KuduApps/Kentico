<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Forums_Tools_Groups_Group_List"
    MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Theme="Default" CodeFile="Group_List.aspx.cs" %>

<%@ Register Src="~/CMSModules/Forums/Controls/Forums/ForumGroupList.ascx" TagName="ForumGroupList"
    TagPrefix="cms" %>
<asp:Content ContentPlaceHolderID="plcContent" ID="content" runat="server">
    <cms:ForumGroupList ID="forumGroupList" runat="server" IsLiveSite="false" />
</asp:Content>
