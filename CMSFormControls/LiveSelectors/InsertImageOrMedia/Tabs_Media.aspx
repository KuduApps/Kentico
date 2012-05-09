<%@ Page Language="C#" Theme="Default" AutoEventWireup="true"
    Inherits="CMSFormControls_LiveSelectors_InsertImageOrMedia_Tabs_Media" EnableEventValidation="false" CodeFile="Tabs_Media.aspx.cs" %>

<%@ Register Src="~/CMSModules/Content/Controls/Dialogs/LinkMediaSelector/LinkMediaSelector.ascx"
    TagName="LinkMedia" TagPrefix="cms" %>
<%@ Register TagPrefix="cms" Namespace="CMS.UIControls" Assembly="CMS.UIControls" %>
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
        .ImageExtraClass
        {
            position: absolute;
        }
        .ImageTooltip
        {
            border: 1px solid #ccc;
            background-color: #fff;
            padding: 3px;
            display: block;
        }
    </style>
</head>
<body class="<%=mBodyClass%>">
    <form id="form1" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="scriptManager" runat="server" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <div class="LiveSiteDialog">
        <cms:CMSUpdatePanel ID="uplContent" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <cms:LinkMedia ID="linkMedia" runat="server" IsLiveSite="true" />
            </ContentTemplate>
        </cms:CMSUpdatePanel>
    </div>
    <asp:Literal ID="ltlScript" runat="server" EnableViewState="false" />
    </form>
</body>
</html>
