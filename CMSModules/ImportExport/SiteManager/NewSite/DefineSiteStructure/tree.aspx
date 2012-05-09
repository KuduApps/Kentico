<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_ImportExport_SiteManager_NewSite_DefineSiteStructure_tree"
    EnableEventValidation="false" Theme="Default" CodeFile="tree.aspx.cs" %>

<%@ Register Src="~/CMSModules/Content/Controls/ContentTree.ascx" TagName="ContentTree"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/UI/Trees/TreeBorder.ascx" TagName="TreeBorder"
    TagPrefix="cms" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server" enableviewstate="false">
    <title>Content - Tree</title>
    <style type="text/css">
        body
        {
            margin: 0px;
            padding: 0px;
            height: 100%;
        }
    </style>
</head>
<body class="TreeBody <%=mBodyClass%>">
    <form id="form1" runat="server">
    <asp:Panel runat="server" ID="pnlBody" CssClass="ContentTree TreeAreaTree">
        <cms:TreeBorder ID="borderElem" runat="server" MinSize="10,*" FramesetName="colsFrameset" />
        <div class="TreeArea">
            <cms:ContentTree ID="treeContent" runat="server" MaxTreeNodes="100000" AllowMarks="false"
                IsLiveSite="false" />
        </div>
    </asp:Panel>
    <asp:HiddenField runat="server" ID="siteName" />
    <input type="hidden" id="hdnAction" name="hdnAction" />
    <input type="hidden" id="hdnParam1" name="hdnParam1" />
    <input type="hidden" id="hdnParam2" name="hdnParam2" />
    </form>
</body>
</html>
