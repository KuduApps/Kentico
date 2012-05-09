<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSFormControls_Filters_DocTypeFilter"
    CodeFile="DocTypeFilter.ascx.cs" %>
<%@ Register Src="~/CMSAdminControls/UI/UniSelector/UniSelector.ascx" TagName="UniSelector"
    TagPrefix="cms" %>
<tr>
    <td>
        <cms:LocalizedLabel ID="lblClassType" runat="server" ResourceString="queryselection.lblclasstype"
            EnableViewState="false" />
    </td>
    <td>
        <asp:DropDownList ID="drpClassType" runat="server" CssClass="DropDownField" AutoPostBack="True"
            OnSelectedIndexChanged="drpClassType_SelectedIndexChanged" />
    </td>
</tr>
<tr>
    <td>
        <asp:Label ID="lblDocType" runat="server" />
    </td>
    <td>
        <cms:UniSelector ID="uniSelector" runat="server" />
    </td>
</tr>
