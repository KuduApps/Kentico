<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_Ecommerce_FormControls_AddressSelector" CodeFile="AddressSelector.ascx.cs" %>
<%@ Register src="~/CMSAdminControls/UI/UniSelector/UniSelector.ascx" tagname="UniSelector" tagprefix="cms" %>

<cms:CMSUpdatePanel ID="pnlUpdate" runat="server">
    <ContentTemplate>
        <cms:UniSelector ID="uniSelector" runat="server" DisplayNameFormat="{%AddressName%}"
            ObjectType="ecommerce.address" ResourcePrefix="addressselector" ReturnColumnName="AddressID"
            SelectionMode="SingleDropDownList" />
    </ContentTemplate>
</cms:CMSUpdatePanel>
