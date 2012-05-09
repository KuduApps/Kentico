<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_Membership_FormControls_Users_MultipleCategoriesSelector" CodeFile="MultipleCategoriesSelector.ascx.cs" %>
<%@ Register Src="~/CMSModules/Categories/Controls/MultipleCategoriesSelector.ascx" TagName="MultipleCategoriesSelector"
    TagPrefix="cms" %>
<div style="border: 1px solid #cccccc; padding: 5px;">
    <cms:MultipleCategoriesSelector ID="categorySelector" runat="server" FormControlMode="true" />
</div>
