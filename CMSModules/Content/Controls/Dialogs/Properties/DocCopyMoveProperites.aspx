<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Content_Controls_Dialogs_Properties_DocCopyMoveProperites"
    Theme="Default" CodeFile="DocCopyMoveProperites.aspx.cs" %>

<%@ Register Src="~/CMSModules/Content/Controls/Dialogs/Properties/CopyMoveLinkProperties.ascx"
    TagName="CopyMoveLinkProperties" TagPrefix="cms" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Document Copy, Move, Link actions</title>
</head>
<body class="<%= mBodyClass %>">
    <form id="form1" runat="server">
    <cms:CopyMoveLinkProperties ID="copyMoveLinkElem" runat="server" />
    </form>
</body>
</html>
