<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_Ecommerce_FormControls_InternalStatusSelector"
    CodeFile="InternalStatusSelector.ascx.cs" %>
<%@ Register Src="~/CMSAdminControls/UI/UniSelector/UniSelector.ascx" TagName="UniSelector"
    TagPrefix="cms" %>
<cms:CMSUpdatePanel ID="pnlUpdate" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <cms:UniSelector ID="uniSelector" runat="server" DisplayNameFormat="{%InternalStatusDisplayName%}"
            ObjectType="ecommerce.internalstatus" ResourcePrefix="internalstatusselector"
            SelectionMode="SingleDropDownList" />
    </ContentTemplate>
</cms:CMSUpdatePanel>
