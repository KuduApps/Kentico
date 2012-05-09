<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_ContactManagement_Controls_UI_Contact_Membership_CustomersFilter"
    CodeFile="CustomersFilter.ascx.cs" %>
<%@ Register Src="~/CMSAdminControls/UI/UniGrid/Filters/TimeSimpleFilter.ascx" TagName="TimeSimpleFilter"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/UI/UniGrid/Filters/TextSimpleFilter.ascx" TagName="TextSimpleFilter"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSFormControls/Sites/SiteSelector.ascx" TagName="SiteSelector"
    TagPrefix="cms" %>
<asp:Panel ID="pnl" runat="server" DefaultButton="btnSearch">
    <table cellpadding="0" cellspacing="2">
        <asp:PlaceHolder runat="server" ID="plcSite" Visible="false">
            <tr>
                <td>
                    <cms:LocalizedLabel ID="lblSite" runat="server" EnableViewState="false" ResourceString="general.site"
                        DisplayColon="true" />
                </td>
                <td>
                    <cms:SiteSelector ID="siteSelector" runat="server" IsLiveSite="false" />
                </td>
            </tr>
        </asp:PlaceHolder>
        <asp:PlaceHolder runat="server" ID="plcCon" Visible="false">
            <tr>
                <td>
                    <cms:LocalizedLabel ID="lblContact" runat="server" EnableViewState="false" ResourceString="om.activity.contactname"
                        DisplayColon="true" />
                </td>
                <td>
                    <cms:TextSimpleFilter ID="fltContact" runat="server" Column="ContactFullNameJoined" />
                </td>
            </tr>
        </asp:PlaceHolder>
        <tr>
            <td class="ContactLabel">
                <cms:LocalizedLabel ID="lblFirstName" runat="server" ResourceString="general.firstname"
                    DisplayColon="true" />
            </td>
            <td>
                <cms:TextSimpleFilter ID="fltFirstName" runat="server" Column="CustomerFirstName" />
            </td>
        </tr>
        <tr>
            <td class="ContactLabel">
                <cms:LocalizedLabel ID="lblLastName" runat="server" ResourceString="general.lastname"
                    DisplayColon="true" />
            </td>
            <td>
                <cms:TextSimpleFilter ID="fltLastName" runat="server" Column="CustomerLastName" />
            </td>
        </tr>
        <tr>
            <td class="ContactLabel">
                <cms:LocalizedLabel ID="lblEmail" runat="server" ResourceString="general.email"
                    DisplayColon="true" />
            </td>
            <td>
                <cms:TextSimpleFilter ID="fltEmail" runat="server" Column="CustomerEmail" />
            </td>
        </tr>        
        <tr>
            <td>
            </td>
            <td>
                <cms:LocalizedButton ID="btnSearch" runat="server" CssClass="ContentButton" ResourceString="general.search" />
            </td>
        </tr>
    </table>
    <br />
</asp:Panel>
