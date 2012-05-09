<%@ Page Language="C#" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" AutoEventWireup="true" Inherits="CMSModules_TagGroups_Pages_Development_TagGroup_New"
    Title="Tag Group - New" Theme="Default" CodeFile="TagGroup_New.aspx.cs" %>

<%@ Register Src="~/CMSModules/TagGroups/Controls/TagEdit.ascx" TagName="GroupEdit"
    TagPrefix="cms" %>
<asp:Content ContentPlaceHolderID="plcContent" runat="Server">
    <cms:GroupEdit ID="groupEdit" runat="server" IsEdit="false" />
</asp:Content>
