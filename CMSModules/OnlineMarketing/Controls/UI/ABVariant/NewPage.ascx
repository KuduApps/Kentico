<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NewPage.ascx.cs" Inherits="CMSModules_OnlineMarketing_Controls_UI_ABVariant_NewPage" %>
<%@ Register Src="~/CMSModules/Content/FormControls/Documents/SelectSinglePath.ascx"
    TagName="pathSelector" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/OnlineMarketing/FormControls/SelectABTest.ascx" TagName="ABTestSelector"
    TagPrefix="cms" %>
<div class="PageContent">
    <asp:Label ID="lblError" CssClass="ErrorLabel" Visible="false" EnableViewState="false"
        runat="server" />
    <table>
        <tr>
            <td>
                <cms:LocalizedLabel runat="server" ID="lblDocumentName" ResourceString="general.documentname"
                    DisplayColon="true" />
            </td>
            <td>
                <cms:CMSTextBox runat="server" ID="txtDocumentName" CssClass="TextBoxField" MaxLength="100" />
                <cms:CMSRequiredFieldValidator ID="rfvDocumentName" Display="Dynamic" runat="server"
                    ControlToValidate="txtDocumentName" />
            </td>
        </tr>
        <tr>
            <td>
                <cms:LocalizedLabel runat="server" ID="lblSaveTo" ResourceString="om.saveto" DisplayColon="true" />
            </td>
            <td>
                <cms:pathSelector runat="server" ID="ucPath" IsLiveSite="false" />
            </td>
        </tr>
        <tr>
            <td>
                <cms:LocalizedLabel runat="server" ID="lblAssignTo" ResourceString="om.assigntoabtest"
                    DisplayColon="true" />
            </td>
            <td>
                <cms:ABTestSelector runat="server" ID="ucABTestSelector" IsLiveSite="false" />
            </td>
        </tr>
        <tr>
            <td>
                <cms:LocalizedLabel runat="server" ID="lblShowInNavigation" ResourceString="om.showinnavigation"
                    DisplayColon="true" />
            </td>
            <td>
                <asp:CheckBox runat="server" ID="chkShowInNavigation" />
            </td>
        </tr>
        <tr>
            <td>
                <cms:LocalizedLabel runat="server" ID="lblShowInSiteMap" ResourceString="om.showinsitemap"
                    DisplayColon="true" />
            </td>
            <td>
                <asp:CheckBox runat="server" ID="chkShowInSiteMap" />
            </td>
        </tr>
        <tr>
            <td>
                <cms:LocalizedLabel runat="server" ID="lblExcludeFromSearch" ResourceString="om.newvariantexcludefromsearch"
                    DisplayColon="true"  />
            </td>
            <td>
                <asp:CheckBox runat="server" ID="chkExcludeFromSearch" Checked= "true" />
            </td>
        </tr>
    </table>
</div>
