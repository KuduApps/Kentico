<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Groups_Tools_Forums_Posts_ForumPost_Tree"
    Theme="Default" CodeFile="ForumPost_Tree.aspx.cs" %>

<%@ Register Src="~/CMSModules/Forums/Controls/PostTree.ascx" TagName="PostTree" TagPrefix="cms" %>
<%@ Register src="~/CMSAdminControls/UI/Trees/TreeBorder.ascx" tagname="TreeBorder" tagprefix="cms" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>Forums - Forum Tree</title>
    <style type="text/css">
        body
        {
            margin: 0px;
            padding: 0px;
            height: 100%;
            width: 100%;
            overflow: hidden;
        }
    </style>
</head>
<body class="TreeBody <%=mBodyClass%>" style="border-color: red; border-width: thin;">
    <form id="form1" runat="server">
    <asp:Panel runat="server" ID="pnlBody" CssClass="ContentTree">
        <cms:TreeBorder ID="borderElem" runat="server" MinSize="10,*" FramesetName="colsFrameset" />
        <div class="TreeArea">
            <div class="TreeAreaTree">
                <cms:PostTree ID="treeElem" runat="server" ItemSelectedItemCssClass="ContentTreeSelectedItem" />
            </div>
        </div>
    </asp:Panel>
    <asp:Literal ID="ltlScript" runat="server" EnableViewState="false" />
    </form>
</body>
</html>
