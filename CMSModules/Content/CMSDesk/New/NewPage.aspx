<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Content_CMSDesk_New_NewPage"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/EmptyPage.master" Title="Content - New page"
    EnableEventValidation="false" CodeFile="NewPage.aspx.cs" %>

<%@ Register Src="TemplateSelection.ascx" TagName="TemplateSelection" TagPrefix="cms" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:Literal ID="ltlScript" runat="server" EnableViewState="false" />
    <div class="NewPageDialog">
        <asp:Panel ID="pnlContent" runat="server" CssClass="PageContentFrame">
            <div class="PTSelection">
                <div style="padding: 5px 5px 0px 5px;">
                    <asp:Label ID="lblInfo" runat="server" CssClass="InfoLabel" EnableViewState="false"
                        Visible="false" />
                    <asp:Label ID="lblError" runat="server" CssClass="ErrorLabel" EnableViewState="false" />
                </div>
                <table cellpadding="0" cellspacing="0" class="Table" border="0">
                    <tr class="HeaderRow">
                        <td class="LeftBorder">
                        </td>
                        <td style="vertical-align: top;" class="Header">
                            <cms:LocalizedLabel ID="lblPageName" runat="server" ResourceString="NewPage.PageName" />
                            <cms:CMSTextBox ID="txtPageName" runat="server" CssClass="TextBoxField" /><br />
                        </td>
                        <td class="RightBorder">
                        </td>
                    </tr>
                </table>
                <table cellpadding="0" cellspacing="0" class="Table" border="0">
                    <tr class="Row">
                        <td style="vertical-align: top;" class="Content">
                            <cms:TemplateSelection ID="selTemplate" runat="server" />
                        </td>
                    </tr>
                </table>
                <div class="Footer">
                </div>
            </div>
        </asp:Panel>
    </div>
</asp:Content>
