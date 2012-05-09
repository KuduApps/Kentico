<%@ Page Language="C#" AutoEventWireup="true" Title="Online marketing settings" CodeFile="Default.aspx.cs"
    Theme="Default" Inherits="CMSModules_Content_CMSDesk_OnlineMarketing_Settings_Default" %>

<%@ Register Src="~/CMSAdminControls/UI/PageElements/Help.ascx" TagName="Help" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/WebAnalytics/FormControls/SelectConversion.ascx" TagName="ConversionSelector"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/WebAnalytics/FormControls/SelectCampaign.ascx" TagName="SelectCampaign"
    TagPrefix="cms" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server" enableviewstate="false">
    <title>Online marketing settings</title>
    <style type="text/css">
        body
        {
            margin: 0px;
            padding: 0px;
            height: 100%;
        }
    </style>
</head>
<body class="<%=mBodyClass%>">
    <form id="form1" runat="server">
    <asp:Panel runat="server" ID="pnlWarning" Visible="False" CssClass="PageHeaderLine">
        <asp:Label runat="server" ID="lblWarning" />
    </asp:Panel>
    <div class="PageContent">
        <asp:Label ID="lblInfo" runat="server" CssClass="InfoLabel" EnableViewState="false"
            Visible="false" />
        <asp:Label ID="lblError" runat="server" Visible="false" CssClass="ErrorLabel" EnableViewState="false" />
        <table>
            <tr>
                <td class="FieldLabel" style="vertical-align: top;">
                    <cms:LocalizedLabel ID="lblCampaign" runat="server" EnableViewState="false" ResourceString="doc.urls.trackcampaign"
                        DisplayColon="true" />
                </td>
                <td>
                    <cms:SelectCampaign runat="server" ID="usSelectCampaign" IsLiveSite="false" SelectionMode="SingleTextBox" />
                </td>
            </tr>
            <tr>
                <td>
                    <cms:LocalizedLabel ID="lblConversionName" runat="server" EnableViewState="false"
                        ResourceString="om.trackconversionname" DisplayColon="true" />
                </td>
                <td>
                    <cms:ConversionSelector runat="server" ID="ucConversionSelector" />
                </td>
            </tr>
            <tr>
                <td>
                    <cms:LocalizedLabel ID="lblConversionValue" runat="server" EnableViewState="false"
                        ResourceString="om.trackconversionvalue" DisplayColon="true" />
                </td>
                <td>
                    <cms:CMSTextBox ID="txtConversionValue" runat="server" CssClass="TextBoxField" MaxLength="100" />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    <cms:LocalizedButton ResourceString="general.save" ID="btnSave" CssClass="SubmitButton"
                        runat="server" OnClick="btnSave_Click" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
