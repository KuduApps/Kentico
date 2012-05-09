<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master"
    Title="Ab test properties" Inherits="CMSModules_OnlineMarketing_Pages_Tools_MVTest_Edit"
    Theme="Default" CodeFile="Edit.aspx.cs" %>
<%@ Register Src="~/CMSModules/OnlineMarketing/Controls/UI/MVTest/Edit.ascx"
    TagName="MVTestEdit" TagPrefix="cms" %>
    
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <cms:MVTestEdit ID="editElem" runat="server" IsLiveSite="false" />
</asp:Content>