<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RelatedDocuments.ascx.cs"
    Inherits="CMSModules_Content_FormControls_Relationships_RelatedDocuments" %>
<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<div class="RelatedDocuments">
    <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
        Visible="false" />
    <asp:Panel ID="pnlNewLink" runat="server" Style="margin-bottom: 5px;">
        <cms:CMSImage ID="imgNewRelationship" runat="server" ImageAlign="AbsMiddle" CssClass="NewItemImage"
            EnableViewState="false" /><cms:LocalizedHyperlink ID="lnkNewRelationship" runat="server" EnableViewState="false"
            ResourceString="Relationship.AddRelatedDocument" />
    </asp:Panel>
    <div Style="margin-bottom: 5px;">
        <cms:CMSUpdatePanel ID="pnlUpdate" runat="server">
            <ContentTemplate>
                <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
                    Visible="false" />
                <cms:UniGrid ID="UniGridRelationship" runat="server" GridName="~/CMSModules/Content/FormControls/Relationships/RelatedDocuments_List.xml"
                    OrderBy="RelationshipDisplayName" IsLiveSite="false" />
            </ContentTemplate>
        </cms:CMSUpdatePanel>
    </div>
    <asp:HiddenField ID="hdnSelectedNodeId" runat="server" Value="" />
</div>
