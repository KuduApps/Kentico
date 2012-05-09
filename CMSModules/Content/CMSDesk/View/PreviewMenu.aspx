<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Content_CMSDesk_View_PreviewMenu"
    Theme="Default" CodeFile="PreviewMenu.aspx.cs" %>

<%@ Register Src="~/CMSModules/Content/Controls/editmenu.ascx" TagName="editmenu" TagPrefix="cms" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>Content - Edit</title>
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

        <script type="text/javascript"> 
        //<![CDATA[
            // Passive refresh
            function PassiveRefresh(nodeId)
            {
                if (parent.frames['editview'] != null)
                {
                    parent.frames['editview'].location.replace(parent.frames['editview'].location);
                }
            }

            // Refresh tree
            function RefreshTree(expandNodeId, selectNodeId) {
                if (parent.RefreshTree) {
                    parent.RefreshTree(expandNodeId, selectNodeId);
                }
            }
        //]]>
        </script>

        <asp:Panel runat="server" ID="Panel1" CssClass="EditMenuBody" EnableViewState="false">
            <asp:Panel runat="server" ID="pnlMenu" CssClass="ContentEditMenu">
                <cms:editmenu ID="menuElem" runat="server" ShowProperties="false" ShowSave="false" ShowCheckOut="false" />
            </asp:Panel>
        </asp:Panel>
        
        <asp:Literal runat="server" ID="ltlScript" EnableViewState="false" />
    </form>
</body>
</html>
