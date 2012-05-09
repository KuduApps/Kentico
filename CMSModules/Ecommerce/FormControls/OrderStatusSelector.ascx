<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_Ecommerce_FormControls_OrderStatusSelector"
    CodeFile="OrderStatusSelector.ascx.cs" %>
<%@ Register Src="~/CMSAdminControls/UI/UniSelector/UniSelector.ascx" TagName="UniSelector"
    TagPrefix="cms" %>
<cms:UniSelector ID="uniSelector" runat="server" DisplayNameFormat="{%StatusDisplayName%}"
    ObjectType="ecommerce.orderstatus" ResourcePrefix="orderstatusselector" SelectionMode="SingleDropDownList"
    AllowEmpty="false" />
