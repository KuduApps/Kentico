<%@ Page Language="C#" AutoEventWireup="true"
    MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Inherits="CMSSiteManager_Development_WebTemplates_WebTemplate_Export"
    Theme="Default" CodeFile="WebTemplate_Export.aspx.cs" %>

<%@ Register Src="~/CMSFormControls/Sites/SiteSelector.ascx" TagName="SiteSelector" TagPrefix="cms" %>
<asp:Content ContentPlaceHolderID="plcContent" ID="content" runat="server">
    <asp:Label ID="lblInfo" runat="server" EnableViewState="false" Visible="false" CssClass="InfoLabel" />
    <asp:Label runat="server" ID="lblError" EnableViewState="false" Visible="false" CssClass="ErrorLabel" />
    <table>
        <tbody>
            <tr>
                <td>
                    <cms:LocalizedLabel ID="lblSite" runat="server" EnableViewState="false" ResourceString="general.site"
                        DisplayColon="true" />
                </td>
                <td>
                    <cms:SiteSelector ID="siteSelector" runat="server" IsLiveSite="false" />
                </td>
            </tr>
            <tr>
                <td>
                    <cms:LocalizedLabel ID="lblExclude" runat="server" EnableViewState="false" ResourceString="Administration-WebTemplate_Export.lblExclude" />
                </td>
                <td>
                    <cms:CMSTextBox ID="txtExcluded" runat="server" CssClass="TextBoxField" Text="test" />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <cms:LocalizedButton ID="btnExport" runat="server" OnClick="btnExport_Click" ResourceString="Administration-WebTemplate_Export.btnExport"
                        EnableViewState="false" />
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>
