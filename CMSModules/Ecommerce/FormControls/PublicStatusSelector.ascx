<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_Ecommerce_FormControls_PublicStatusSelector"
    CodeFile="PublicStatusSelector.ascx.cs" %>
<%@ Register Src="~/CMSAdminControls/UI/UniSelector/UniSelector.ascx" TagName="UniSelector"
    TagPrefix="cms" %>
<cms:CMSUpdatePanel ID="pnlUpdate" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <cms:UniSelector ID="uniSelector" runat="server" DisplayNameFormat="{%PublicStatusDisplayName%}"
            ObjectType="ecommerce.publicstatus" ResourcePrefix="publicstatusselector" SelectionMode="SingleDropDownList"
            AllowEmpty="false" />
    </ContentTemplate>
</cms:CMSUpdatePanel>
