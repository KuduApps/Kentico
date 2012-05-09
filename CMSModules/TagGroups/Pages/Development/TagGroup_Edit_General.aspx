<%@ Page Language="C#" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" AutoEventWireup="true" Inherits="CMSModules_TagGroups_Pages_Development_TagGroup_Edit_General"
    Title="Tag group - Properties" Theme="Default" CodeFile="TagGroup_Edit_General.aspx.cs" %>

<%@ Register Src="~/CMSModules/TagGroups/Controls/TagEdit.ascx" TagName="TagGroupEdit"
    TagPrefix="cms" %>
<asp:Content ID="Content1" ContentPlaceHolderID="plcContent" runat="Server">
    <cms:TagGroupEdit ID="groupEdit" runat="server" IsEdit="true" />
</asp:Content>
