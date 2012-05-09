<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_Content_FormControls_Relationships_SelectRelationshipNames" CodeFile="SelectRelationshipNames.ascx.cs" %>
<%@ Register src="~/CMSAdminControls/UI/UniSelector/UniSelector.ascx" tagname="UniSelector" tagprefix="cms" %>

<cms:CMSUpdatePanel ID="pnlUpdate" runat="server">
    <ContentTemplate>
        <cms:UniSelector ID="uniSelector" runat="server" ObjectType="cms.relationshipname"
            ResourcePrefix="unirelationshipnameselector" AllowAll="false" SelectionMode="SingleDropDownList"
            DisplayNameFormat="{%RelationshipDisplayName%}" />
    </ContentTemplate>
</cms:CMSUpdatePanel>
