<%@ Page Title="Administration menu" Language="C#" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master"
    AutoEventWireup="true" CodeFile="Menu.aspx.cs" Inherits="CMSDesk_Administration_Menu"
    Theme="Default" %>

<%@ Register Src="~/CMSAdminControls/UI/UIProfiles/UIPanelMenu.ascx" TagPrefix="cms"
    TagName="UIPanelMenu" %>
<asp:Content ID="pnlContent" ContentPlaceHolderID="plcContent" runat="server">
    <table class="PanelMenuWrapper" cellspacing="0" cellpadding="0">
        <tbody>
            <tr>
                <td>
                    <cms:UIPanelMenu ID="administrationUiPanelMenu" runat="server" ModuleName="CMS.Administration"
                        ColumnsCount="3" />
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>
