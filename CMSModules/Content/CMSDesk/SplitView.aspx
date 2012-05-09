<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Content_CMSDesk_SplitView"
    CodeFile="SplitView.aspx.cs" %>

<%@ Register Src="~/CMSModules/Content/Controls/SplitView/Documents/DocumentSplitView.ascx"
    TagName="DocumentSplitView" TagPrefix="cms" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>CMSDesk - Split view</title>
    <asp:Literal runat="server" ID="ltlScript" EnableViewState="false" />
</head>
<cms:DocumentSplitView ID="docSplitView" runat="server" />
</html>
