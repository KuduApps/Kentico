<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSFormControls_Filters_DocumentFilter"
    CodeFile="DocumentFilter.ascx.cs" %>
<%@ Register Src="~/CMSFormControls/Sites/SiteSelector.ascx" TagName="SiteSelector"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/UI/UniGrid/Filters/TextSimpleFilter.ascx" TagName="TextSimpleFilter"
    TagPrefix="cms" %>
<table>
    <asp:PlaceHolder ID="plcSites" runat="server">
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel ID="lblSites" runat="server" DisplayColon="true" ResourceString="general.site" />
            </td>
            <td>
                <cms:SiteSelector ID="siteSelector" runat="server" IsLiveSite="false" OnlyRunningSites="false"
                    UseCodeNameForSelection="true" />
            </td>
        </tr>
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="plcPath" runat="server">
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel ID="lblName" runat="server" DisplayColon="true" ResourceString="general.documentname" />
            </td>
            <td>
                <cms:TextSimpleFilter ID="nameFilter" runat="server" Column="DocumentName" />
            </td>
        </tr>
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="plcClass" runat="server">
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel ID="lblClass" runat="server" DisplayColon="true" ResourceString="general.documenttype" />
            </td>
            <td>
                <cms:TextSimpleFilter ID="classFilter" runat="server" Column="ClassDisplayName" />
            </td>
        </tr>
    </asp:PlaceHolder>
    <tr>
        <td>
        </td>
        <td>
            <cms:LocalizedButton ID="btnShow" runat="server" ResourceString="general.show" CssClass="ContentButton"
                OnClick="btnShow_Click" />
        </td>
    </tr>
</table>
