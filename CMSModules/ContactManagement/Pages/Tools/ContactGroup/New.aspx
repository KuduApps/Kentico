<%@ Page Language="C#" AutoEventWireup="true" CodeFile="New.aspx.cs" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master"
    Title="Contact group properties" Inherits="CMSModules_ContactManagement_Pages_Tools_ContactGroup_New" Theme="Default" %>
    
<%@ Register Src="~/CMSModules/ContactManagement/Controls/UI/ContactGroup/Edit.ascx" TagName="ContactGroupEdit" TagPrefix="cms" %>
    
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <cms:ContactGroupEdit ID="editElem" runat="server" IsLiveSite="false" />
</asp:Content>