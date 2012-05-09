<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Content_CMSDesk_Properties_LinkedDocs"
    Theme="Default" CodeFile="LinkedDocs.aspx.cs" %>
<%@ Register src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" tagname="UniGrid" tagprefix="cms" %>
<%@ Register src="~/CMSAdminControls/UI/PageElements/Help.ascx" tagname="Help" tagprefix="cms" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>Relationship - list</title>
    <style type="text/css">
        body
        {
            margin: 0px;
            padding: 0px;
            height: 100%;
        }
    </style>
</head>
<body class="VerticalTabsBody <%=mBodyClass%>">
    <form id="form1" runat="server">
    <asp:Panel ID="pnlBody" runat="server" CssClass="VerticalTabsPageBody">
        <asp:Panel ID="pnlMenu" runat="server" CssClass="ContentEditMenu">
            <table width="100%">
                <tr>
                    <td>
                        <div style="height: 24px; padding: 5px;">
                        </div>
                    </td>
                    <td class="TextRight">
                        <cms:Help ID="helpElem" runat="server" TopicName="linked_docs" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnlContent" runat="server" CssClass="PageContent PropertiesPanel">
            <asp:Label ID="lblInfo" runat="server" CssClass="InfoLabel" />
            <asp:Label ID="lblError" runat="server" CssClass="ErrorLabel" Visible="false" />
            <br />
            <cms:UniGrid ID="gridDocs" runat="server" GridName="LinkedDocs.xml" IsLiveSite="false" ExportFileName="cms_document"/>
        </asp:Panel>
    </asp:Panel>

    <script type="text/javascript">
        //<![CDATA[
        // Select item action
        function SelectItem(nodeId, parentNodeId) {
            if (nodeId != 0) {
                parent.SelectNode(nodeId);
                parent.RefreshTree(parentNodeId, nodeId);
                //parent.location.href = "DocumentFrameset.aspx?action=listing&nodeid=" + nodeId;
            }
        }

        // Refresh action
        function RefreshTree(nodeId, selectNodeId) {
            parent.RefreshTree(nodeId, selectNodeId);
        }
        //]]>
    </script>

    <asp:Literal ID="ltlScript" runat="server" EnableViewState="false" />
    </form>
</body>
</html>
