<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_ImportExport_Controls_ExportConfiguration"
    CodeFile="ExportConfiguration.ascx.cs" %>
<%@ Register Src="~/CMSFormControls/Sites/SiteSelector.ascx" TagName="SiteSelector"
    TagPrefix="cms" %>
<cms:CMSUpdatePanel ID="pnlUpdate" runat="server" UpdateMode="Always">
    <ContentTemplate>
        <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" Visible="false" EnableViewState="false" />
        <table>
            <tr>
                <td>
                    <cms:LocalizedLabel runat="server" ID="lblFileName" EnableViewState="false" ResourceString="general.filename"
                        DisplayColon="true" />
                </td>
                <td>
                    <cms:CMSTextBox runat="server" ID="txtFileName" CssClass="TextBoxField" />
                </td>
            </tr>
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
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
            <asp:PlaceHolder ID="plcNone" runat="server" Visible="false">
                <tr>
                    <td colspan="2">
                        <asp:RadioButton ID="radNone" runat="server" GroupName="Export" AutoPostBack="false" />
                    </td>
                </tr>
            </asp:PlaceHolder>
            <tr>
                <td colspan="2">
                    <asp:RadioButton ID="radAll" runat="server" GroupName="Export" AutoPostBack="false" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:RadioButton ID="radDate" runat="server" GroupName="Export" AutoPostBack="false" /><br />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <cms:DateTimePicker runat="server" ID="dtDate" DisplayNow="false" DisplayNA="false"
                        RenderDisableScript="true" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:RadioButton ID="radExport" runat="server" GroupName="Export" Enabled="false"
                        AutoPostBack="false" />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <asp:ListBox runat="server" ID="lstExports" CssClass="ContentListBoxLow" />
                </td>
            </tr>
        </table>
    </ContentTemplate>
</cms:CMSUpdatePanel>
