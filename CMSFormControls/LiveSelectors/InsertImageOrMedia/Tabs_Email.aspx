<%@ Page Language="C#" AutoEventWireup="true" Theme="Default"
    Inherits="CMSFormControls_LiveSelectors_InsertImageOrMedia_Tabs_Email" EnableEventValidation="false" CodeFile="Tabs_Email.aspx.cs" %>

<%@ Register Src="~/CMSModules/Content/Controls/Dialogs/Properties/HTMLEmailProperties.ascx"
    TagName="HTMLEmailProperties" TagPrefix="cms" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server" enableviewstate="false">
    <title>Insert image or media - e-mail</title>
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
    <ajaxToolkit:ToolkitScriptManager ID="scrManager" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <div class="LiveSiteDialog">
        <cms:HTMLEmailProperties ID="emailProperties" runat="server" IsLiveSite="true" />
        <asp:Literal ID="ltlScript" runat="server" EnableViewState="false" />
    </div>
    </form>
</body>
</html>
