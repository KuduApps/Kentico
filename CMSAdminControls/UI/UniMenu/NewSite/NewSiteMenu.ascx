<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSAdminControls_UI_UniMenu_NewSite_NewSiteMenu"
    CodeFile="NewSiteMenu.ascx.cs" %>
<%@ Register Src="~/CMSAdminControls/UI/UniMenu/UniMenuButtons.ascx" TagName="UniMenuButtons"
    TagPrefix="cms" %>
<table cellpadding="0" cellspacing="0"">
    <tr>
        <td valign="top">
            <cms:UniMenuButtons ID="buttonsBig" runat="server" EnableViewState="false" />
        </td>
        <td valign="top">
            <asp:Panel ID="pnlSmallButtons" runat="server" EnableViewState="false" CssClass="ReducedButtons">
                <cms:UniMenuButtons ID="buttonsSmall" runat="server" EnableViewState="false" MaximumItems="2" />
            </asp:Panel>
        </td>
    </tr>
</table>
