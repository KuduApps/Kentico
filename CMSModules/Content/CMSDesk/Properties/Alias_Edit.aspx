<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Content_CMSDesk_Properties_Alias_Edit"
    Theme="Default" CodeFile="Alias_Edit.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/PageElements/PageTitle.ascx" TagName="PageTitle"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSFormControls/Cultures/SiteCultureSelector.ascx" TagName="SiteCultureSelector"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/Content/Controls/editmenu.ascx" TagName="editmenu"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSFormControls/Documents/DocumentURLPath.ascx" TagName="DocumentURLPath"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/WebAnalytics/FormControls/SelectCampaign.ascx" TagName="SelectCampaign"
    TagPrefix="cms" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>Properties - Page</title>
    <style type="text/css">
        body
        {
            margin: 0px;
            padding: 0px;
            height: 100%;
        }
    </style>
</head>
<body class="VerticalTabsBody <%=mBodyClass%>">
    <form id="form1" runat="server">
    <asp:Panel ID="pnlHeader" runat="server" CssClass="PageHeader SimpleHeader">
        <cms:PageTitle ID="pageAlias" runat="server" />
    </asp:Panel>
    <asp:Panel ID="pnlContent" runat="server" CssClass="PageContent">
        <asp:Label ID="lblError" runat="server" EnableViewState="false" CssClass="ErrorLabel" /><asp:Label
            ID="lblInfo" runat="server" EnableViewState="false" CssClass="InfoLabel" />
        <asp:Panel ID="pnlForm" runat="server" CssClass="PageContent">
            <cms:DocumentURLPath runat="server" ID="ctrlURL" HideCustom="true" />
            <table>
                <tr>
                    <td class="FieldLabel" style="width: 125px;">
                        <asp:Label ID="lblDocumentCulture" runat="server" />
                    </td>
                    <td>
                        <cms:SiteCultureSelector runat="server" ID="cultureSelector" IsLiveSite="false" />
                    </td>
                </tr>
                <tr>
                    <td class="FieldLabel">
                        <asp:Label ID="lblURLExtensions" runat="server" />
                    </td>
                    <td>
                        <cms:CMSTextBox ID="txtURLExtensions" runat="server" CssClass="TextBoxField" MaxLength="100" /><br />
                    </td>
                </tr>
                <tr>
                    <td class="FieldLabel">
                        <asp:Label ID="lblTrackCampaign" runat="server" />
                    </td>
                    <td>
                        <cms:SelectCampaign runat="server" ID="usSelectCampaign" IsLiveSite="false" SelectionMode="SingleTextBox" />
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <cms:CMSButton ID="btnOk" runat="server" CssClass="SubmitButton" OnClick="btnOK_Click" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </asp:Panel>
    </form>
</body>
</html>
