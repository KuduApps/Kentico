<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_Ecommerce_FormControls_ManufacturerSelector"
    CodeFile="ManufacturerSelector.ascx.cs" %>
<%@ Register Src="~/CMSAdminControls/UI/UniSelector/UniSelector.ascx" TagName="UniSelector"
    TagPrefix="cms" %>
<cms:UniSelector ID="uniSelector" runat="server" DisplayNameFormat="{%ManufacturerDisplayName%}"
    ReturnColumnName="ManufacturerID" ObjectType="ecommerce.manufacturer" ResourcePrefix="manufacturerselector"
    SelectionMode="SingleDropDownList" AllowEmpty="false" />
