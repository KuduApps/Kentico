<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_Ecommerce_FormControls_DiscountLevelSelector"
    CodeFile="DiscountLevelSelector.ascx.cs" %>
<%@ Register Src="~/CMSAdminControls/UI/UniSelector/UniSelector.ascx" TagName="UniSelector"
    TagPrefix="cms" %>
<cms:UniSelector ID="uniSelector" runat="server" DisplayNameFormat="{%DiscountLevelDisplayName%}"
    ReturnColumnName="DiscountLevelID" ObjectType="ecommerce.discountlevel" ResourcePrefix="discountlevelselector"
    SelectionMode="SingleDropDownList" AllowEmpty="false" />
