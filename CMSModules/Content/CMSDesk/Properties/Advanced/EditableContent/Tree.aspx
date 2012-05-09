<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Content_CMSDesk_Properties_Advanced_EditableContent_Tree"
    Theme="default" MasterPageFile="~/CMSMasterPages/UI/Tree.master" CodeFile="Tree.aspx.cs" %>

<%@ Register Src="~/CMSModules/Content/Controls/ContentTree.ascx" TagName="ContentTree"
    TagPrefix="cms" %>
<asp:Content ID="plcBeforeTree" runat="server" ContentPlaceHolderID="plcBeforeTree">
    <div class="MenuBox" style="width: 100%;">
        <asp:Panel ID="pnlMenu" runat="server" CssClass="TreeMenu" Style="margin-bottom: 5px;">
            <asp:Panel ID="pnlMenuContent" runat="server" CssClass="TreeMenuContent">
                <table width="100%">
                    <tr>
                        <td style="width: 50%">
                            <asp:Panel runat="server" ID="pnlNewItem">
                                <cms:CMSImage ID="imgNewItem" runat="server" CssClass="NewItemImage" />
                                <cms:LocalizedHyperlink ID="lnkNewItem" runat="server" NavigateUrl="#" CssClass="NewItemLinkDisabled"
                                    ResourceString="editablecontent.newitem" />
                            </asp:Panel>
                        </td>
                        <td>
                            <asp:Panel runat="server" ID="pnlDeleteItem">
                                <cms:CMSImage ID="imgDeleteItem" runat="server" CssClass="NewItemImage" />
                                <asp:HyperLink ID="lnkDeleteItem" runat="server" NavigateUrl="#" CssClass="NewItemLinkDisabled" />
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:Panel>
    </div>
</asp:Content>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcTree">
    <asp:Panel ID="pnlContent" runat="server" CssClass="WebPartTree">
        <asp:HiddenField ID="hdnCurrentNodeType" runat="server" />
        <asp:HiddenField ID="hdnCurrentNodeName" runat="server" />

        <script type="text/javascript">
            //<![CDATA[
            var currentNode = null;
            var treeUrl = '<%=ResolveUrl("~/CMSModules/Content/CMSDesk/Properties/Advanced/EditableContent/tree.aspx") + "?nodeid=" + nodeId %>';
            var isAuthorizedToModify = <%= IsAuthorizedToModify.ToString().ToLower() %>;

            // Refresh node action
            function RefreshNode(nodeName, nodeType, nodeId) {
                if (currentNode != null) {
                    currentNode.firstChild.innerHTML = nodeName;
                    // Dynamically create onclick event
                    currentNode.onclick = function() { SelectNode(nodeName, nodeType, this); };
                }
            }


            // Opens confirmation dialog and delete item 
            function DeleteItem() {
                if (confirm('<%= GetString("editablecontent.confirmdelete") %>')) {
                    document.location.replace(treeUrl + '&deleteItem=true&nodename=' + document.getElementById('<%=  hdnCurrentNodeName.ClientID %>').value + '&nodetype=' + document.getElementById('<%= hdnCurrentNodeType.ClientID %>').value);
                }
                return false;
            }


            // Opens 'Create new' menu on main panel
            function CreateNew() {
                parent.frames['main'].CreateNew(document.getElementById('<%= hdnCurrentNodeType.ClientID %>').value);
                return false;
            }


            // Sets proper image for 'new item' link and disable delete link for root item
            function UpdateMenu(selectedNodeName, selectedItemType) {
                var newItemImage = document.getElementById('<%= imgNewItem.ClientID %>');
                var deleteItemImage = document.getElementById('<%= imgDeleteItem.ClientID %>');
                var deleteItem = document.getElementById('<%= lnkDeleteItem.ClientID %>');
                var newItem = document.getElementById('<%= lnkNewItem.ClientID %>');
                var deletePanel = document.getElementById('<%= pnlDeleteItem.ClientID %>');

                var usesWorkflow = <%= UsesWorkflow.ToString().ToLower() %>;
                var canDelete = true;
                if (usesWorkflow) {
                    canDelete = <%= DocumentIsCheckOuted.ToString().ToLower() %>;
                }

                var isDisabled = (selectedNodeName == '') || (selectedNodeName == null) || !isAuthorizedToModify || !canDelete;

                if (isDisabled) {
                    deleteItem.className = 'NewItemLinkDisabled';
                    deleteItem.onclick = null;
                    deleteItemImage.src = '<%= GetImageUrl("Objects/CMS_WebPart/deletedisabled.png")%>';

                } else {
                    deleteItem.className = 'NewItemLink';
                    deleteItem.onclick = DeleteItem;
                    deleteItemImage.src = '<%= GetImageUrl("Objects/CMS_WebPart/delete.png")%>';
                }
                deletePanel.disabled = isDisabled;
                deleteItem.disabled = isDisabled;
                if (isAuthorizedToModify) 
                {
                    newItem.className = 'NewItemLink';
                    newItem.onclick = CreateNew;

                    if (selectedItemType == 'webpart') {
                        newItemImage.src = '<%= GetImageUrl("CMSModules/CMS_Content/EditableContent/editablewebpart.png")%>';
                    } 
                    else {
                        newItemImage.src = '<%= GetImageUrl("CMSModules/CMS_Content/EditableContent/addeditableitemsmall.png") %>';
                    }
                }
                else {
                    newItem.className = 'NewItemLinkDisabled';
                    newItem.onclick = null;

                    if (selectedItemType == 'webpart') {
                        newItemImage.src = '<%= GetImageUrl("CMSModules/CMS_Content/EditableContent/editablewebpartdisabled.png")%>';
                    } 
                    else {
                        newItemImage.src = '<%= GetImageUrl("CMSModules/CMS_Content/EditableContent/addeditableitemsmalldisabled.png") %>';
                    }
                }
            }

            // Selects node action
            function SelectNode(nodeName, nodeType, nodeElem) {
                if (currentNode == null) {
                    currentNode = document.getElementById('treeSelectedNode');
                }
                if ((currentNode != null) && (nodeElem != null)) {
                    currentNode.className = 'ContentTreeItem';
                }
                parent.frames['main'].SelectNode(nodeName, nodeType);
                document.getElementById('<%= hdnCurrentNodeName.ClientID%>').value = nodeName;
                document.getElementById('<%= hdnCurrentNodeType.ClientID %>').value = nodeType;

                if (nodeElem != null) {
                    currentNode = nodeElem;
                    if (currentNode != null) {
                        currentNode.className = 'ContentTreeSelectedItem';
                    }
                }

                UpdateMenu(nodeName, nodeType);
            }

            //]]>
        </script>

        <asp:Literal ID="ltlScript" runat="server" EnableViewState="false" />
        <asp:Panel runat="server" ID="pnlBody" CssClass="ContentTree">
            <asp:TreeView ID="webpartsTree" runat="server" ShowLines="True" />
            <br />
            <asp:TreeView ID="regionsTree" runat="server" ShowLines="True" />
        </asp:Panel>
    </asp:Panel>
</asp:Content>
