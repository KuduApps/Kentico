<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Content_CMSDesk_View_Listing"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Content - Listing"
    CodeFile="Listing.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<%@ Register Src="~/CMSFormControls/Cultures/SiteCultureSelector.ascx" TagName="SiteCultureSelector"
    TagPrefix="cms" %>
<asp:Content ID="cntContent" ContentPlaceHolderID="plcContent" runat="server">
    <cms:CMSUpdatePanel ID="pnlUpdate" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="pnlFilter" runat="server" DefaultButton="btnShow">
                <table>
                    <tr>
                        <td class="FieldLabel">
                            <cms:LocalizedLabel ID="lblName" runat="server" ResourceString="general.documentname"
                                DisplayColon="true" />
                        </td>
                        <td>
                            <asp:DropDownList ID="drpOperator" runat="server" CssClass="ContentDropdown" />
                            <cms:CMSTextBox ID="txtName" runat="server" Width="265" MaxLength="100" />
                        </td>
                    </tr>
                    <tr>
                        <td class="FieldLabel">
                            <cms:LocalizedLabel ID="lblType" runat="server" ResourceString="general.documenttype"
                                DisplayColon="true" />
                        </td>
                        <td>
                            <asp:DropDownList ID="drpOperator2" runat="server" CssClass="ContentDropdown" />
                            <cms:CMSTextBox ID="txtType" runat="server" Width="265" MaxLength="100" />
                        </td>
                    </tr>
                    <asp:PlaceHolder ID="plcLang" runat="server">
                        <tr>
                            <td style="vertical-align: bottom;">
                                <cms:LocalizedLabel ID="lblLanguage" runat="server" ResourceString="general.language"
                                    DisplayColon="true" />
                            </td>
                            <td>
                                <asp:DropDownList ID="drpLanguage" runat="server" CssClass="SelectorDropDown" Width="145px" />
                                <cms:SiteCultureSelector runat="server" ID="cultureElem" IsLiveSite="false" />
                            </td>
                        </tr>
                    </asp:PlaceHolder>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <cms:LocalizedButton ID="btnShow" runat="server" ResourceString="general.show" CssClass="ContentButton"
                                OnClick="btnShow_OnClick" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <br />
        </ContentTemplate>
    </cms:CMSUpdatePanel>
    <cms:CMSUpdatePanel ID="pnlGrid" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="Listing">
                <cms:UniGrid ID="gridDocuments" runat="server" ShortID="g" GridName="Listing.xml"
                    EnableViewState="true" DelayedReload="true" IsLiveSite="false" ExportFileName="cms_document" />
            </div>
            <asp:Panel ID="pnlFooter" runat="server" CssClass="MassAction">
                <asp:DropDownList ID="drpWhat" runat="server" CssClass="DropDownFieldSmall" />
                <asp:DropDownList ID="drpAction" runat="server" CssClass="DropDownFieldSmall" />
                <cms:LocalizedButton ID="btnOk" runat="server" ResourceString="general.ok" CssClass="SubmitButton SelectorButton"
                    EnableViewState="false" />
                <br />
                <br />
            </asp:Panel>
            <asp:Label ID="lblInfo" runat="server" CssClass="InfoLabel" EnableViewState="false" />
            <asp:HiddenField ID="hdnIdentificator" runat="server" EnableViewState="false" />
        </ContentTemplate>
    </cms:CMSUpdatePanel>
</asp:Content>
