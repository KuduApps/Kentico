<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Filter.ascx.cs" Inherits="CMSModules_ContactManagement_Controls_UI_Contact_Filter" %>
<%@ Register Src="~/CMSAdminControls/UI/UniGrid/Filters/TextSimpleFilter.ascx" TagName="TextSimpleFilter"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/ContactManagement/FormControls/ContactStatusSelector.ascx"
    TagName="ContactStatusSelector" TagPrefix="cms" %>
<%@ Register Src="~/CMSFormControls/Sites/SiteSelector.ascx" TagName="SiteSelector"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSFormControls/Sites/SiteOrGlobalSelector.ascx" TagName="SiteOrGlobalSelector"
    TagPrefix="cms" %>
<asp:Panel ID="pnlFilter" runat="server" DefaultButton="btnSearch">
    <table cellpadding="0" cellspacing="2">
        <tr>
            <td>
                <cms:LocalizedLabel ID="lblName" runat="server" ResourceString="general.firstname"
                    DisplayColon="true" EnableViewState="false" />
            </td>
            <td>
                <cms:TextSimpleFilter ID="fltFirstName" runat="server" Column="ContactFirstName" />
            </td>
        </tr>
        <asp:PlaceHolder ID="plcMiddle" runat="server" Visible="false">
            <tr>
                <td>
                    <cms:LocalizedLabel ID="lblMiddleName" runat="server" ResourceString="general.middlename"
                        DisplayColon="true" EnableViewState="false" />
                </td>
                <td>
                    <cms:TextSimpleFilter ID="fltMiddleName" runat="server" Column="ContactMiddleName" />
                </td>
            </tr>
        </asp:PlaceHolder>
        <tr>
            <td>
                <cms:LocalizedLabel ID="lblLastName" runat="server" ResourceString="general.lastname"
                    DisplayColon="true" EnableViewState="false" />
            </td>
            <td>
                <cms:TextSimpleFilter ID="fltLastName" runat="server" Column="ContactLastName" />
            </td>
        </tr>
        <tr>
            <td>
                <cms:LocalizedLabel ID="lblEmail" runat="server" ResourceString="general.email" DisplayColon="true"
                    EnableViewState="false" />
            </td>
            <td>
                <cms:TextSimpleFilter ID="fltEmail" runat="server" Column="ContactEmail" />
            </td>
        </tr>
        <tr>
            <td>
                <cms:LocalizedLabel ID="lblContactStatus" runat="server" ResourceString="om.contactstatus"
                    DisplayColon="true" EnableViewState="false" />
            </td>
            <td>
                <cms:ContactStatusSelector ID="fltContactStatus" runat="server" IsLiveSite="false" />
            </td>
        </tr>
        <tr>
            <td>
                <cms:LocalizedLabel ID="lblMonitored" runat="server" ResourceString="general.show"
                    DisplayColon="true" EnableViewState="false" />
            </td>
            <td>
                <asp:RadioButtonList ID="radMonitored" runat="server" RepeatDirection="Horizontal">
                </asp:RadioButtonList>
            </td>
        </tr>
        <asp:PlaceHolder ID="plcSite" runat="server" Visible="false">
            <tr>
                <td>
                    <cms:LocalizedLabel ID="lblSite" runat="server" ResourceString="general.site" DisplayColon="true"
                        EnableViewState="false" />
                </td>
                <td>
                    <cms:SiteSelector ID="siteSelector" runat="server" IsLiveSite="false" AllowGlobal="true"
                        Visible="false" DropDownCSSClass="DropDownFieldFilter" />
                    <cms:SiteOrGlobalSelector ID="siteOrGlobalSelector" runat="server" IsLiveSite="false"
                        Visible="false" DropDownCSSClass="DropDownFieldFilter" AutoPostBack="false" />
                </td>
            </tr>
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="plcAdvancedSearch" runat="server" Visible="false">
            <tr>
                <td>
                    <cms:LocalizedLabel ID="lblOwner" runat="server" ResourceString="om.contact.owner"
                        DisplayColon="true" EnableViewState="false" />
                </td>
                <td>
                    <cms:TextSimpleFilter ID="fltOwner" runat="server" Column="FullName" />
                </td>
            </tr>
            <tr>
                <td>
                    <cms:LocalizedLabel ID="lblCountry" runat="server" ResourceString="general.country"
                        DisplayColon="true" EnableViewState="false" />
                </td>
                <td>
                    <cms:TextSimpleFilter ID="fltCountry" runat="server" Column="CountryDisplayName" />
                </td>
            </tr>
            <tr>
                <td>
                    <cms:LocalizedLabel ID="lblState" runat="server" ResourceString="general.state" DisplayColon="true"
                        EnableViewState="false" />
                </td>
                <td>
                    <cms:TextSimpleFilter ID="fltState" runat="server" Column="StateDisplayName" />
                </td>
            </tr>
            <tr>
                <td>
                    <cms:LocalizedLabel ID="lblCity" runat="server" ResourceString="general.city" DisplayColon="true"
                        EnableViewState="false" />
                </td>
                <td>
                    <cms:TextSimpleFilter ID="fltCity" runat="server" Column="ContactCity" />
                </td>
            </tr>
            <tr>
                <td>
                    <cms:LocalizedLabel ID="lblPhone" runat="server" ResourceString="general.phone" DisplayColon="true"
                        EnableViewState="false" />
                </td>
                <td>
                    <cms:TextSimpleFilter ID="fltPhone" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <cms:LocalizedLabel ID="lblIP" runat="server" ResourceString="eventloglist.ipaddress"
                        DisplayColon="true" EnableViewState="false" />
                </td>
                <td>
                    <cms:CMSTextBox ID="txtIP" runat="server" CssClass="LongFilterTextBox" MaxLength="15" />
                </td>
            </tr>
            <asp:PlaceHolder ID="plcMerged" runat="server">
                <tr>
                    <td>
                        <cms:LocalizedLabel ID="lblMerged" runat="server" ResourceString="om.contact.mergedcontact"
                            DisplayColon="true" EnableViewState="false" />
                    </td>
                    <td>
                        <cms:LocalizedCheckBox ID="chkMerged" runat="server" ResourceString="om.contact.alsomerged" />
                    </td>
                </tr>
            </asp:PlaceHolder>
            <asp:PlaceHolder ID="plcChildren" runat="server" Visible="false">
                <tr>
                    <td>
                        <cms:LocalizedLabel ID="lblChildren" runat="server" ResourceString="om.contact.listchildren"
                            DisplayColon="true" EnableViewState="false" />
                    </td>
                    <td>
                        <cms:LocalizedCheckBox ID="chkChildren" runat="server" ResourceString="om.contact.listchildrencheck" />
                    </td>
                </tr>
            </asp:PlaceHolder>            
        </asp:PlaceHolder>
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
<asp:Panel ID="pnlAdvanced" runat="server" Visible="true">
    <asp:Image runat="server" ID="imgShowSimpleFilter" CssClass="NewItemImage" />
    <asp:LinkButton ID="lnkShowSimpleFilter" runat="server" OnClick="lnkShowSimpleFilter_Click" />
</asp:Panel>
<asp:Panel ID="pnlSimple" runat="server" Visible="false">
    <asp:Image runat="server" ID="imgShowAdvancedFilter" CssClass="NewItemImage" />
    <asp:LinkButton ID="lnkShowAdvancedFilter" runat="server" OnClick="lnkShowAdvancedFilter_Click" />
</asp:Panel>
<br />
