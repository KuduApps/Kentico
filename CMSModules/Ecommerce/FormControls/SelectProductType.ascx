<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SelectProductType.ascx.cs"
    Inherits="CMSModules_Ecommerce_FormControls_SelectProductType" %>
<cms:LocalizedDropDownList ID="drpProductType" runat="server" UseResourceStrings="true"
    OnSelectedIndexChanged="drpProductType_SelectedIndexChanged" CssClass="DropDownField" />
