<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_Ecommerce_FormControls_SupplierSelector" CodeFile="SupplierSelector.ascx.cs" %>
<%@ Register src="~/CMSAdminControls/UI/UniSelector/UniSelector.ascx" tagname="UniSelector" tagprefix="cms" %>

<cms:CMSUpdatePanel ID="pnlUpdate" runat="server">
    <ContentTemplate>
        <cms:UniSelector ID="uniSelector" runat="server" DisplayNameFormat="{%SupplierDisplayName%}"
            ObjectType="ecommerce.supplier" ResourcePrefix="supplierselector" ReturnColumnName="SupplierID"
            SelectionMode="SingleDropDownList" AllowEmpty="false" />
    </ContentTemplate>
</cms:CMSUpdatePanel>
