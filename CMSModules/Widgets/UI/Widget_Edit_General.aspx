<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_Widgets_UI_Widget_Edit_General" MasterPageFile="~/CMSMasterPages/UI/EmptyPage.master"
    Title="Widget Edit - General" Theme="Default" CodeFile="Widget_Edit_General.aspx.cs" %>

<%@ Register Src="~/CMSModules/Widgets/Controls/WidgetGeneral.ascx" TagName="WidgetGeneral"
    TagPrefix="cms" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <cms:WidgetGeneral id="widgetGeneral" runat="server" />
</asp:Content>
