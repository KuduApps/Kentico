<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_ImportExport_Controls_ImportSiteDetails"
    CodeFile="ImportSiteDetails.ascx.cs" %>
<%@ Register Src="~/CMSFormControls/System/LocalizableTextBox.ascx" TagName="LocalizableTextBox"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSFormControls/Sites/SiteSelector.ascx" TagName="SiteSelector"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSFormControls/Cultures/SiteCultureSelector.ascx" TagName="CultureSelector"
    TagPrefix="cms" %>
<table>
    <tr>
        <td style="width: 5px;">
            &nbsp;
        </td>
        <td colspan="2">
            <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" Visible="false" EnableViewState="false" />
        </td>
    </tr>
    <asp:PlaceHolder runat="server" ID="plcNewSelection">
        <tr>
            <td colspan="3">
                <asp:RadioButton runat="server" ID="radNewSite" Checked="true" GroupName="Site" AutoPostBack="true" />
            </td>
        </tr>
    </asp:PlaceHolder>
    <asp:PlaceHolder runat="server" ID="plcNewSite">
        <tr>
            <td style="width: 5px;">
                &nbsp;
            </td>
            <td nowrap="nowrap">
                <asp:Label ID="lblSiteDisplayName" runat="server" EnableViewState="false" />
            </td>
            <td>
                <cms:LocalizableTextBox CssClass="TextBoxField" ID="txtSiteDisplayName" runat="server" MaxLength="200" />&nbsp;
                <cms:CMSRequiredFieldValidator ID="rfvSiteDisplayName" runat="server" ControlToValidate="txtSiteDisplayName:textbox" />
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td nowrap="nowrap">
                <asp:Label ID="lblSiteName" runat="server" EnableViewState="false" />
            </td>
            <td>
                <cms:CMSTextBox CssClass="TextBoxField" ID="txtSiteName" runat="server" MaxLength="100" />&nbsp;
                <cms:CMSRequiredFieldValidator ID="rfvSiteName" runat="server" ControlToValidate="txtSiteName" />
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <asp:Label ID="lblDomain" runat="server" EnableViewState="false" />
            </td>
            <td>
                <cms:CMSTextBox CssClass="TextBoxField" ID="txtDomain" runat="server" MaxLength="400" />&nbsp;
                <cms:CMSRequiredFieldValidator ID="rfvDomain" runat="server" ControlToValidate="txtDomain" />
            </td>
        </tr>
        <asp:PlaceHolder runat="server" ID="plcCulture" Visible="false">
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    <asp:Label ID="lblCulture" runat="server" EnableViewState="false" />
                </td>
                <td>
                    <cms:CultureSelector runat="server" ID="cultureElem" DisplayAllCultures="true" IsLiveSite="false"
                        UseCultureCode="true" />
                </td>
            </tr>
        </asp:PlaceHolder>
    </asp:PlaceHolder>
    <asp:PlaceHolder runat="server" ID="plcExisting">
        <asp:PlaceHolder runat="server" ID="plcExistingSelection">
            <tr>
                <td colspan="3">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:RadioButton runat="server" ID="radExisting" GroupName="Site" AutoPostBack="true" />
                </td>
            </tr>
        </asp:PlaceHolder>
        <tr>
            <td>
            </td>
            <td>
                <asp:Label ID="lblSite" runat="server" EnableViewState="false" />
            </td>
            <td>
                <cms:SiteSelector ID="siteSelector" runat="server" IsLiveSite="false" />
            </td>
        </tr>
        <asp:Literal runat="server" ID="ltlScript" EnableViewState="false" />
    </asp:PlaceHolder>
</table>
