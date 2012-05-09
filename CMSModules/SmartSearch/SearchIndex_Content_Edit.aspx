<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_SmartSearch_SearchIndex_Content_Edit"
    ValidateRequest="false" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Theme="Default" Title="Search Index - Edit" CodeFile="SearchIndex_Content_Edit.aspx.cs" %>

<%@ Register Src="~/CMSModules/SmartSearch/Controls/UI/SearchIndex_Content_Edit.ascx"
    TagName="ContentEdit" TagPrefix="cms" %>
    <%@ Register Src="~/CMSModules/SmartSearch/Controls/UI/SearchIndex_Forum_Edit.ascx"
    TagName="ForumEdit" TagPrefix="cms" %>
    <%@ Register Src="~/CMSModules/SmartSearch/Controls/UI/SearchIndex_CustomTable_Edit.ascx"
    TagName="CustomTableEdit" TagPrefix="cms" %>
<asp:content id="cntBody" contentplaceholderid="plcContent" runat="server">
    <cms:ContentEdit ID="ContentEdit" runat="server" Visible="false" StopProcessing="true" />
    <cms:ForumEdit ID="forumEdit" runat="server" Visible="false" StopProcessing="true" />
    <cms:CustomTableEdit ID="customTableEdit" runat="server" Visible="false" StopProcessing="true" />
</asp:content>
