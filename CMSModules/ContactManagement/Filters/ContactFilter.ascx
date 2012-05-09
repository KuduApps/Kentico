<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ContactFilter.ascx.cs"
    Inherits="CMSModules_ContactManagement_Filters_ContactFilter" %>
<%@ Register Src="~/CMSAdminControls/UI/UniGrid/Filters/TextSimpleFilter.ascx" TagName="TextSimpleFilter"
    TagPrefix="cms" %>
<cms:LocalizedLabel ID="lblContact" runat="server" DisplayColon="true" ResourceString="om.contact.entersearch" />
<cms:CMSTextBox ID="txtContact" runat="server" CssClass="TextBoxField" />
<cms:LocalizedButton ID="btnSearch" runat="server" ResourceString="general.search"
    CssClass="ContentButton" OnClick="btnSearch_Click" />
