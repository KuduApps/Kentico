<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_AbuseReport_AbuseReport_List"
    MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Theme="Default" CodeFile="AbuseReport_List.aspx.cs" %>

<%@ Register Src="~/CMSFormControls/Sites/SiteSelector.ascx" TagName="SiteSelector"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/AbuseReport/Controls/AbuseReportList.ascx" TagName="AbuseReportList"
    TagPrefix="cms" %>
<asp:Content ID="cntBody" ContentPlaceHolderID="plcContent" runat="server">
    <cms:CMSUpdatePanel ID="pnlUpdate" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table>
                <tr>
                    <td class="FieldLabel">
                        <cms:LocalizedLabel ID="lblTitle" runat="server" ResourceString="general.title" DisplayColon="true"
                            EnableViewState="false" />
                    </td>
                    <td>
                        <cms:CMSTextBox ID="txtTitle" runat="server" CssClass="TextBoxField" MaxLength="50" />
                    </td>
                </tr>
                <tr>
                    <td class="FieldLabel">
                        <cms:LocalizedLabel ID="lblStatus" runat="server" ResourceString="abuse.status" DisplayColon="true"
                            EnableViewState="false" />
                    </td>
                    <td>
                        <cms:LocalizedDropDownList ID="drpStatus" runat="server" CssClass="DropDownField"
                            AutoPostBack="false" />
                    </td>
                </tr>
                <asp:PlaceHolder ID="plcSites" runat="Server">
                    <tr>
                        <td class="FieldLabel">
                            <cms:LocalizedLabel ID="lblSites" runat="server" ResourceString="general.site" DisplayColon="true"
                                EnableViewState="false" />
                        </td>
                        <td>
                            <cms:SiteSelector ID="siteSelector" runat="server" IsLiveSite="false" />
                        </td>
                    </tr>
                </asp:PlaceHolder>
                <tr>
                    <td>
                    </td>
                    <td>
                        <cms:LocalizedButton ID="btnShow" runat="server" ResourceString="general.show" CssClass="ContentButton"
                            EnableViewState="false" />
                    </td>
                </tr>
            </table>
            <br />
            <cms:AbuseReportList ID="ucAbuseReportList" runat="server" />
        </ContentTemplate>
    </cms:CMSUpdatePanel>
</asp:Content>
