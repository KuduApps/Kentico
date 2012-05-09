<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSAdminControls_UI_Trees_MacroTree"
    EnableViewState="false" CodeFile="MacroTree.ascx.cs" %>
<cms:UITreeView runat="server" ID="treeElem" ShortID="t" ShowLines="true" PopulateNodesFromClient="true" OnTreeNodePopulate="treeElem_TreeNodePopulate" EnableViewState="false" />
