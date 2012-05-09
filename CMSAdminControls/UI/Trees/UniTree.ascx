<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSAdminControls_UI_Trees_UniTree"
    CodeFile="UniTree.ascx.cs" %>
<div id="<%= this.ClientID %>">
    <asp:Panel runat="server" ID="pnlMain" CssClass="TreeMain">
        <asp:Panel runat="server" ID="pnltreeTree" CssClass="TreeTree">
            <cms:UITreeView runat="server" ID="treeElem" ShortID="tv" OnTreeNodePopulate="treeElem_TreeNodePopulate"
                NodeStyle-CssClass="UniTreeNode" ShowLines="true" />
            <asp:HiddenField ID="hdnSelectedItem" runat="server" EnableViewState="true" />
            <div style="display: none;">
                <cms:CMSUpdatePanel ID="pnlUpdateSelected" runat="server">
                    <ContentTemplate>
                        <cms:CMSButton ID="btnItemSelected" runat="server" EnableViewState="false" OnClick="btnItemSelected_Click" />
                    </ContentTemplate>
                </cms:CMSUpdatePanel>
            </div>
        </asp:Panel>
    </asp:Panel>
</div>
<asp:Literal runat="server" ID="ltlScript" EnableViewState="false"></asp:Literal>
