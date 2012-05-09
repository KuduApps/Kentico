<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NewPage.aspx.cs" Inherits="CMSModules_OnlineMarketing_Pages_Content_ABTesting_ABVariant_NewPage"
    Theme="Default" %>

<%@ Register Src="~/CMSModules/OnlineMarketing/Controls/UI/ABVariant/NewPage.ascx"
    TagName="NewPage" TagPrefix="cms" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server" enableviewstate="false">
    <title>New A/B test variant</title>
    <style type="text/css">
        body
        {
            margin: 0px;
            padding: 0px;
            height: 100%;
        }
    </style>
</head>
<body>
    <form runat="server" id="form1">
    <asp:Panel runat="server" ID="pnlDisabled" CssClass="PageHeaderLine" Visible="false">
        <asp:Label runat="server" ID="lblDisabled" EnableViewState="false" />
        <asp:Label runat="server" ID="lblABTestingDisabled" EnableViewState="false" />
    </asp:Panel>
    <ajaxToolkit:ToolkitScriptManager runat="server" ID="scriptManager" />
    <cms:NewPage runat="server" ID="ucNewPage" />
    </form>
</body>
</html>
