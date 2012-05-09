<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_Ecommerce_FormControls_CurrencySelector" CodeFile="CurrencySelector.ascx.cs" %>
<%@ Register src="~/CMSAdminControls/UI/UniSelector/UniSelector.ascx" tagname="UniSelector" tagprefix="cms" %>

<cms:CMSUpdatePanel ID="pnlUpdate" runat="server">
    <ContentTemplate>
        <cms:UniSelector ID="uniSelector" runat="server" DisplayNameFormat="{%CurrencyDisplayName%}"
            ObjectType="ecommerce.currency" ResourcePrefix="currencyselector"
            SelectionMode="SingleDropDownList" ReturnColumnName="CurrencyID" />
    </ContentTemplate>
</cms:CMSUpdatePanel>
