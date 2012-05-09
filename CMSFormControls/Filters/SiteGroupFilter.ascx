<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SiteGroupFilter.ascx.cs"
    Inherits="CMSFormControls_Filters_SiteGroupFilter" %>
<%@ Register Src="~/CMSFormControls/Filters/SiteFilter.ascx" TagName="SiteFilter"
    TagPrefix="cms" %>
<asp:Panel CssClass="Filter" runat="server" ID="pnlSearch">
    <table>
        <asp:PlaceHolder ID="plcSite" runat="server">
            <tr>
                <td>
                    <cms:LocalizedLabel ID="lblSite" runat="server" EnableViewState="false" />&nbsp;
                </td>
                <td>
                    <cms:SiteFilter ID="siteFilter" runat="server" IsLiveSite="false" />
                </td>
            </tr>
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="plcGroup" runat="server">
            <tr>
                <td>
                    <cms:LocalizedLabel ID="lblGroup" runat="server" EnableViewState="false" />
                </td>
                <td>
                    <asp:DropDownList ID="drpGroup" runat="server" OnSelectedIndexChanged="Filter_Changed"
                        AutoPostBack="true" CssClass="DropDownField" />
                </td>
            </tr>
        </asp:PlaceHolder>
    </table>
</asp:Panel>
