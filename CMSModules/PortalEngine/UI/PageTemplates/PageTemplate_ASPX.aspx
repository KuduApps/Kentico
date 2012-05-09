<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_PortalEngine_UI_PageTemplates_PageTemplate_ASPX"
    Theme="Default" ValidateRequest="false" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master"
    Title="Page Template Edit - Header" CodeFile="PageTemplate_ASPX.aspx.cs" %>

<%@ Register Src="~/CMSFormControls/Sites/SiteSelector.ascx" TagName="SiteSelector"
    TagPrefix="cms" %>
<asp:Content ID="cntControls" runat="server" ContentPlaceHolderID="plcSiteSelector">
    <table>
        <tr>
            <td>
                <asp:RadioButton GroupName="Code" runat="server" ID="radSlave" AutoPostBack="true"
                    Checked="true" />
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:RadioButton GroupName="Code" runat="server" ID="radMaster" AutoPostBack="true" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:RadioButton GroupName="Code" runat="server" ID="radTemplate" AutoPostBack="true" />
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:RadioButton GroupName="Code" runat="server" ID="radTemplateOnly" AutoPostBack="true" />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:Panel ID="pnlContainer" runat="Server" DefaultButton="btnRefresh">
        <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
            Visible="false" />
        <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
            Visible="false" />
        <table>
            <tr>
                <td colspan="3">
                    <asp:Label runat="server" ID="lblSaveInfo" CssClass="InfoLabel" EnableViewState="false" />
                </td>
            </tr>
            <tr>
                <td style="width: 100px;">
                    <cms:LocalizedLabel ID="lblSite" runat="server" ResourceString="general.site" DisplayColon="true" />
                </td>
                <td>
                    <cms:SiteSelector ID="siteSelector" runat="server" IsLiveSite="false" />
                </td>
            </tr>
            <asp:PlaceHolder runat="server" ID="plcMasterTemplate">
                <tr>
                    <td>
                        <asp:Label ID="lblMaster" runat="server" />
                    </td>
                    <td>
                        <cms:CMSTextBox runat="server" ID="txtMaster" Text="MainMenu" CssClass="TextBoxField" />
                    </td>
                    <td>
                    </td>
                </tr>
            </asp:PlaceHolder>
            <tr>
                <td>
                    <asp:Label ID="lblName" runat="server" />
                </td>
                <td colspan="2">
                    <cms:CMSTextBox runat="server" ID="txtName" CssClass="TextBoxField" />
                    <cms:CMSButton runat="server" ID="btnSave" OnClick="btnSave_Click" CssClass="LongButton" />
                    <cms:CMSButton runat="server" ID="btnRefresh" CssClass="LongButton" />
                </td>
            </tr>
        </table>
        <br />
        <table style="width: 100%">
            <tr>
                <td>
                    <asp:Label runat="server" ID="lblCodeInfo" CssClass="InfoLabel" EnableViewState="false" />
                    <cms:ExtendedTextArea ID="txtCode" runat="server" EnableViewState="false" ReadOnly="true"
                        EditorMode="Advanced" Width="100%" Height="300px" />
                    <br />
                    <br />
                    <asp:Label runat="server" ID="lblCodeBehindInfo" CssClass="InfoLabel" EnableViewState="false" />
                    <cms:ExtendedTextArea ID="txtCodeBehind" runat="server" EnableViewState="false" ReadOnly="true"
                        EditorMode="Advanced" Language="CSharp" Width="100%" Height="300px" />
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
