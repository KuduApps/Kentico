<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_MessageBoards_Content_Properties_Tree"
    Theme="default" MasterPageFile="~/CMSMasterPages/UI/Tree.master" Title="Message boards - tree" CodeFile="Tree.aspx.cs" %>
<%@ Register src="~/CMSAdminControls/UI/PageElements/HeaderActions.ascx" tagname="HeaderActions" tagprefix="cms" %>

<%@ Register Src="~/CMSModules/Content/Controls/ContentTree.ascx" TagName="ContentTree" TagPrefix="cms" %>
<asp:Content ID="cntMenu" runat="server" ContentPlaceHolderID="plcMenu">
    <div style="position: absolute; left:-20px; width: 100%; z-index: 2;">
        <div style="position: absolute; left: 20px;">
            <asp:Panel ID="pnlMenu" runat="server" CssClass="TreeMenu TreeMenuPadding" Height="25px">
                <asp:Panel ID="pnlMenuContent" runat="server" CssClass="TreeMenuContent">
                    <cms:HeaderActions ID="actionsElem" runat="server" />
                </asp:Panel>
            </asp:Panel>
        </div>
    </div>
</asp:Content>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcTree">

    <script type="text/javascript">
    //<![CDATA[

        // Refresh node action
        function RefreshNode(nodeName, nodeId)
        {
            currentNode.firstChild.innerHTML = nodeName;
            // Dynamically create onclick event
            currentNode.onclick = function() { SelectNode(nodeName, this); };
        }

    //]]>
    </script>

    <asp:HiddenField ID="hdnBoardId" runat="server" />
    <asp:TreeView ID="treeElem" runat="server" ShowLines="True" CssClass="ContentTree"
        EnableViewState="False" />
    <asp:Literal ID="ltlScript" runat="server" EnableViewState="false" />
</asp:Content>
