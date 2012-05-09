<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_PortalEngine_FormControls_PageTemplates_LevelTree" CodeFile="LevelTree.ascx.cs" %>

<script type="text/javascript">
    //<![CDATA[
    function hideCheckBoxToolTips(treeId) {
        var tree = document.getElementById(treeId);
        if (tree != null) {
            var inputs = tree.getElementsByTagName("input");
            var input;
            for (input in inputs) {
                if (inputs[input].type == "checkbox") {
                    inputs[input].title = "";
                }
            }
        }
    }
    //]]>
</script>

<div class="InheritLevels">
    <asp:TreeView ID="treeElem" runat="server" ShowCheckBoxes="All" ShowLines="true"
        ShowExpandCollapse="false" />
</div>
<asp:Literal ID="ltlScript" runat="server" EnableViewState="false" />