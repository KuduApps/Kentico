<%@ Page Language="C#" Theme="Default" AutoEventWireup="true"
    Inherits="CMSFormControls_Selectors_InsertImageOrMedia_Tabs_Media" EnableEventValidation="false" CodeFile="Tabs_Media.aspx.cs" %>

<%@ Register Src="~/CMSModules/Content/Controls/Dialogs/LinkMediaSelector/LinkMediaSelector.ascx"
    TagName="LinkMedia" TagPrefix="cms" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server" enableviewstate="false">
    <title>Insert image or media - content</title>
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
    <ajaxToolkit:ToolkitScriptManager ID="scriptManager" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <cms:CMSUpdatePanel ID="pnlUpdateSelectMedia" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <cms:LinkMedia ID="linkMedia" runat="server" IsLiveSite="false" />
        </ContentTemplate>
    </cms:CMSUpdatePanel>
    <asp:Literal ID="ltlScript" runat="server" EnableViewState="false" />
    </form>
</body>
</html>
