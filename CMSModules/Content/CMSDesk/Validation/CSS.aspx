<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CSS.aspx.cs" Inherits="CMSModules_Content_CMSDesk_Validation_CSS"
    Theme="Default" %>
<%@ Register Src="~/CMSAdminControls/Validation/CssValidator.ascx" TagName="CSSValidator"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/UI/PageElements/Help.ascx" TagName="Help" TagPrefix="cms" %>
    
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server" enableviewstate="false">
    <title>Validation - CSS</title>
    <style type="text/css">
        body
        {
            margin: 0px;
            padding: 0px;
            height: 100%;
        }
        a:hover, a:active, a:focus
        {
            outline: none;
        }
    </style>
</head>
<body class="VerticalTabsBody <%=mBodyClass%>">
    <form id="form1" runat="server">
    <asp:Panel runat="server" ID="pnlBody" CssClass="VerticalTabsPageBody">
            <cms:CSSValidator ID="validator" runat="server" />
            <div class="ViewHelp">
            <cms:Help ID="helpElem" runat="server" TopicName="cssvalidator" HelpName="helpTopic"
                EnableViewState="false" />
        </div>
    </asp:Panel>
    </form>
</body>
</html>
