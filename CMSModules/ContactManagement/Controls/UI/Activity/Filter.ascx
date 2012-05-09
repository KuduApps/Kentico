<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_ContactManagement_Controls_UI_Activity_Filter"
    CodeFile="Filter.ascx.cs" %>
<%@ Register Src="~/CMSAdminControls/UI/UniGrid/Filters/TextSimpleFilter.ascx" TagName="TextSimpleFilter"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/UI/UniGrid/Filters/TimeSimpleFilter.ascx" TagName="TimeSimpleFilter"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSFormControls/Sites/SiteSelector.ascx" TagName="SiteSelector"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/ContactManagement/FormControls/ActivityTypeSelector.ascx"
    TagName="ActivityTypeSel" TagPrefix="cms" %>
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
        <tr>
            <td>
                <cms:LocalizedLabel ID="lblType" runat="server" EnableViewState="false" ResourceString="general.type" DisplayColon="true" />
            </td>
            <td>
                <cms:ActivityTypeSel ID="drpType" runat="server" CssClass="DropDownFieldFilter" />
            </td>
        </tr>
        <tr>
            <td>
                <cms:LocalizedLabel ID="lblName" runat="server" EnableViewState="false" ResourceString="om.activity.gridtitle"
                    DisplayColon="true" />
            </td>
            <td>
                <cms:TextSimpleFilter ID="fltName" runat="server" Column="ActivityTitle" />
            </td>
        </tr>
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
        <asp:PlaceHolder runat="server" ID="plcIp" Visible="false">
            <tr>
                <td>
                    <cms:LocalizedLabel ID="lblIP" runat="server" EnableViewState="false" ResourceString="om.activity.ipaddress"
                        DisplayColon="true" />
                </td>
                <td>
                    <cms:TextSimpleFilter ID="fltIP" runat="server" Column="ActivityIPAddress" />
                </td>
            </tr>
        </asp:PlaceHolder>
        <tr>
            <td>
                <cms:LocalizedLabel ID="lblTimeBetween" runat="server" EnableViewState="false" ResourceString="eventlog.timebetween"
                    DisplayColon="true" />
            </td>
            <td>
                <cms:TimeSimpleFilter ID="fltTimeBetween" runat="server" Column="ActivityCreated" />
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
