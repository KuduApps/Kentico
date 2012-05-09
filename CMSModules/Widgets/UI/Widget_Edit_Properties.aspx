<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_Widgets_UI_Widget_Edit_Properties" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master"
    Title="Widget Edit - Properties" Theme="Default" CodeFile="Widget_Edit_Properties.aspx.cs" %>

<%@ Register Src="~/CMSModules/Widgets/Controls/WidgetPropertiesEdit.ascx" TagName="WidgetPropertiesEdit"
    TagPrefix="cms" %>
    
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <cms:WidgetPropertiesEdit ID="widgetProperties" runat="server" />
</asp:Content>
