<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_Ecommerce_FormControls_SKUSelector"
    CodeFile="SKUSelector.ascx.cs" %>
<%@ Register Src="~/CMSAdminControls/UI/UniSelector/UniSelector.ascx" TagName="UniSelector"
    TagPrefix="cms" %>
<cms:CMSUpdatePanel ID="pnlUpdate" runat="server">
    <ContentTemplate>
        <cms:UniSelector ID="uniSelector" runat="server" DisplayNameFormat="{%SKUName%}"
            ObjectType="ecommerce.sku" ResourcePrefix="productselector" SelectionMode="SingleTextBox"
            AllowEditTextBox="false" AllowEmpty="false" />
    </ContentTemplate>
</cms:CMSUpdatePanel>
