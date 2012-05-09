<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSAdminControls_Validation_HTMLValidator"
    CodeFile="HTMLValidator.ascx.cs" %>
<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagPrefix="cms" TagName="UniGrid" %>
<%@ Register Src="~/CMSAdminControls/UI/PageElements/HeaderActions.ascx" TagName="HeaderActions"
    TagPrefix="cms" %>
<%@ Register Namespace="CMS.UIControls.UniGridConfig" TagPrefix="ug" Assembly="CMS.UIControls" %>
<asp:Panel ID="pnlActions" runat="server" CssClass="Validation ContentEditMenu PageHeaderLine"
    EnableViewState="false">
    <div class="LeftAlign">
        <table cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <asp:LinkButton ID="lnkValidate" runat="server" OnClick="lnkValidate_Click" CssClass="MenuItemEdit"
                        EnableViewState="false">
                        <cms:CMSImage ID="imgValidate" runat="server" EnableViewState="false" />
                        <cms:LocalizedLabel ID="lblValidate" runat="server" EnableViewState="false" />
                    </asp:LinkButton>
                </td>
                <td>
                    <asp:LinkButton ID="lnkViewCode" runat="server" EnableViewState="false" CssClass="MenuItemEdit">
                        <cms:CMSImage ID="imgViewCode" runat="server" EnableViewState="false" />
                        <cms:LocalizedLabel ID="lblViewCode" runat="server" EnableViewState="false" />
                    </asp:LinkButton>
                </td>
                <td>
                    <asp:LinkButton ID="lnkNewWindow" runat="server" EnableViewState="false" CssClass="MenuItemEdit">
                        <cms:CMSImage ID="imgNewWindow" runat="server" EnableViewState="false" />
                        <cms:LocalizedLabel ID="lblNewWindow" runat="server" EnableViewState="false" />
                    </asp:LinkButton>
                </td>
                <td>
                    <asp:LinkButton ID="lnkExportToExcel" runat="server" EnableViewState="false" OnClick="lnkExportToExcel_Click"
                        CssClass="MenuItemEdit">
                        <cms:CMSImage ID="imgExportToExcel" runat="server" EnableViewState="false" />
                        <cms:LocalizedLabel ID="lblExportToExcel" runat="server" EnableViewState="false" />
                    </asp:LinkButton>
                </td>
            </tr>
        </table>
    </div>
</asp:Panel>
<asp:Panel ID="pnlGrid" runat="server" CssClass="Validation PageContent">
    <cms:CMSUpdateProgress ID="up" runat="server" HandlePostback="true" EnableViewState="false" />
    <cms:LocalizedLabel ID="lblError" runat="server" CssClass="ErrorLabel" EnableViewState="false"
        Visible="false" />
    <asp:Panel ID="pnlStatus" runat="server" CssClass="Status" Visible="false">
        <asp:Image runat="server" ID="imgStatus" AlternateText="Status image" EnableViewState="false" />
        <cms:LocalizedLabel ID="lblStatus" runat="server" EnableViewState="false" />
    </asp:Panel>
    <cms:LocalizedLabel ID="lblResults" runat="server" EnableViewState="false" class="Results"
        Visible="false" DisplayColon="true" />
    <cms:UniGrid ID="gridValidationResult" runat="server">
        <GridColumns>
            <ug:Column Source="line" Caption="$validation.html.line$" Wrap="false" />
            <ug:Column Source="col" Caption="$validation.html.col$" Wrap="false" AllowSorting="false" />
            <ug:Column Source="message" Caption="$validation.html.message$" AllowSorting="false"
                Width="30%" />
            <ug:Column Source="explanation" ExternalSourceName="explanation" Caption="$validation.html.explanation$"
                Width="40%" AllowSorting="false" />
            <ug:Column Source="source" ExternalSourceName="source" Caption="$validation.html.source$"
                Wrap="true" AllowSorting="false" />
        </GridColumns>
        <PagerConfig DefaultPageSize="10" />
    </cms:UniGrid>
</asp:Panel>
<asp:HiddenField ID="hdnHTML" runat="server" EnableViewState="false" />
