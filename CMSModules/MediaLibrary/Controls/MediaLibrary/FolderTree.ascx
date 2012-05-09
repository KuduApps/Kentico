<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_MediaLibrary_Controls_MediaLibrary_FolderTree"
    CodeFile="FolderTree.ascx.cs" %>
<asp:Label runat="server" ID="lblError" ForeColor="Red" EnableViewState="false" />
<asp:TreeView ID="treeElem" runat="server" ShowLines="true" ShowExpandCollapse="true"
    CssClass="ContentTree MediaLibraryTree">
    <HoverNodeStyle CssClass="HoveredFolder" />
    <RootNodeStyle CssClass="RootFolder" />
    <LeafNodeStyle CssClass="LeafFolder" />
    <NodeStyle CssClass="Folder ContentTreeItem" />
    <ParentNodeStyle CssClass="ParentFolder" />
    <SelectedNodeStyle CssClass="SelectedFolder ContentTreeSelectedItem" />
</asp:TreeView>
<asp:HiddenField ID="hdnPath" runat="server" />
<div style="clear: both">
</div>
