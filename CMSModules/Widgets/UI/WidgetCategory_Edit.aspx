<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_Widgets_UI_WidgetCategory_Edit" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master"
    Title="Widget category - Edit" Theme="Default" CodeFile="WidgetCategory_Edit.aspx.cs" %>

<%@ Register Src="~/CMSModules/Widgets/Controls/WidgetCategoryEdit.ascx" TagName="CategoryEdit"
    TagPrefix="cms" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <cms:CategoryEdit ID="categoryEdit" runat="server" />
</asp:Content>
