<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_PortalEngine_UI_PageTemplates_PageTemplate_Layouts"
    Theme="Default" ValidateRequest="false" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master"
    Title="Page template - Layouts" CodeFile="PageTemplate_Layouts.aspx.cs" %>

<%@ Register Src="~/CMSModules/PortalEngine/FormControls/PageLayouts/PageLayoutSelector.ascx"
    TagName="layoutSelector" TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/UI/Macros/MacroEditor.ascx" TagName="MacroEditor"
    TagPrefix="cms" %>
<asp:Content runat="server" ContentPlaceHolderID="plcBeforeContent">
    <asp:Panel runat="server" ID="pnlType" CssClass="PageHeaderLine">
        <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
            Visible="false" />
        <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
            Visible="false" />
        <table>
            <tr>
                <td>
                    <asp:RadioButton runat="server" ID="radShared" GroupName="LayoutGroup" AutoPostBack="True"
                        OnCheckedChanged="radShared_CheckedChanged" />&nbsp;
                </td>
                <td>
                    <cms:layoutSelector runat="server" ID="selectShared" IsLiveSite="false" OnChanged="selectShared_Changed" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:RadioButton runat="server" ID="radCustom" GroupName="LayoutGroup" AutoPostBack="True"
                        OnCheckedChanged="radShared_CheckedChanged" />
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:PlaceHolder ID="plcLayout" runat="server">
        <asp:Label runat="server" ID="lblAspxInfo" CssClass="InfoLabel" EnableViewState="false"
            Visible="false" />
        <asp:Panel ID="pnlCheckOutInfo" runat="server" CssClass="InfoLabel" Visible="false">
            <asp:Label runat="server" ID="lblCheckOutInfo" />
            <br />
        </asp:Panel>
        <asp:PlaceHolder ID="plcContent" runat="server">
            <table style="width: 100%">
                <tr>
                    <td class="FieldLabel">
                        <asp:Label ID="lblType" runat="server" EnableViewState="false" />
                    </td>
                    <td style="width: 100%;">
                        <asp:DropDownList runat="server" ID="drpType" AutoPostBack="true" />
                    </td>
                </tr>
                <tr>
                    <td class="FieldLabel" style="vertical-align: top;">
                        <asp:Label ID="lbLayoutCode" runat="server" EnableViewState="false" />
                    </td>
                    <td>
                        <div class="PlaceholderInfoLine">
                            <asp:PlaceHolder runat="server" ID="plcHint">
                                <asp:Literal ID="ltlHint" runat="server" EnableViewState="false" /><br />
                                <br />
                            </asp:PlaceHolder>
                            <asp:PlaceHolder runat="server" ID="plcDirectives">
                                <asp:Label runat="server" ID="ltlDirectives" EnableViewState="false" CssClass="LayoutDirectives" /><br />
                            </asp:PlaceHolder>
                            <asp:PlaceHolder runat="server" ID="plcVirtualInfo" Visible="false">
                                <br />
                                <asp:Label runat="server" ID="lblVirtualInfo" CssClass="ErrorLabel" EnableViewState="false" />
                            </asp:PlaceHolder>
                        </div>
                        <cms:MacroEditor ID="txtCustom" runat="server" EnableViewState="true" Width="98%"
                            Height="300px" />
                    </td>
                </tr>
                <asp:PlaceHolder runat="server" ID="plcCssLink">
                    <tr id="cssLink">
                        <td class="FieldLabel">
                        </td>
                        <td>
                            <cms:LocalizedLinkButton runat="server" ID="lnkStyles" EnableViewState="false" ResourceString="general.addcss"
                                OnClientClick="document.getElementById('editCss').style.display = 'table-row'; document.getElementById('cssLink').style.display = 'none'; return false;" />
                        </td>
                    </tr>
                </asp:PlaceHolder>
                <tr id="editCss" style="<%= (plcCssLink.Visible ? "display: none": "") %>">
                    <td class="FieldLabel">
                        <cms:LocalizedLabel runat="server" ID="lblLayoutCSS" ResourceString="Container_Edit.ContainerCSS"
                            DisplayColon="true" EnableViewState="false" />
                    </td>
                    <td style="width: 85%;">
                        <cms:ExtendedTextArea ID="txtCustomCSS" runat="server" EnableViewState="true" EditorMode="Advanced"
                            Language="CSS" Width="98%" Height="200px" />
                    </td>
                </tr>
            </table>
        </asp:PlaceHolder>
    </asp:PlaceHolder>
</asp:Content>
