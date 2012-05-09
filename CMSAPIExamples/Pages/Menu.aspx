<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Menu.aspx.cs" Theme="Default"
    Inherits="CMSAPIExamples_Pages_Menu" %>

<%@ Register Src="~/CMSAdminControls/UI/Trees/UniTree.ascx" TagPrefix="cms" TagName="UniTree" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>API examples - menu</title>
    <style type="text/css">
        html, body
        {
            margin: 0px;
            padding: 0px;
            height: 100%;
            overflow: hidden;
        }
    </style>
</head>
<body class="TreeBody <%=mBodyClass%>">

    <script type="text/javascript" language="javascript">
        //<![CDATA[
        function DisplayPage(url, element) {
            // Change page
            window.parent.frames['content'].location = url;
            // Set selected item in tree
            $j('span[name=treeNode]').each(function() {
                var jThis = $j(this);
                jThis.removeClass('ContentTreeSelectedItem');
                if (!jThis.hasClass('ContentTreeItem')) {
                    jThis.addClass('ContentTreeItem');
                }
                if (this.id == element.id) {
                    jThis.addClass('ContentTreeSelectedItem');
                }
            });
        }
        //]]>
    </script>

    <form id="form1" runat="server">
    <div>
        <div class="TreeArea">
            <div class="TreeAreaTree">
                <asp:Label ID="lblError" runat="server" EnableViewState="false" CssClass="ErrorLabel"
                    Visible="false" />
                <asp:TreeView ID="treeView" runat="server" ShowLines="true" ShowExpandCollapse="true"
                    CssClass="ContentTree">
                </asp:TreeView>
            </div>
        </div>
        <div style="cursor: default;" class="TreeBorder">
            &nbsp;</div>
    </div>
    </form>
</body>
</html>
