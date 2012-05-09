<%@ Page Title="Synchronization - Documents list" Language="C#" Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master"
    AutoEventWireup="true" Inherits="CMSModules_Staging_Tools_Tasks_DocumentsList"
    MaintainScrollPositionOnPostback="true" CodeFile="DocumentsList.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<asp:Content ID="Content3" ContentPlaceHolderID="plcContent" runat="Server">
    <asp:Panel ID="pnlListingInfo" runat="server" EnableViewState="false">
        <asp:Label ID="lblListingInfo" runat="server" CssClass="InfoLabel" EnableViewState="false" />
    </asp:Panel>
    <table cellpadding="0" cellspacing="0" width="100%" style="margin-bottom: 6px;">
        <tr>
            <td>
                <asp:Panel ID="pnlSearch" runat="server" CssClass="LeftAlign">
                    <cms:LocalizedLabel ID="lblSearch" runat="server" ResourceString="general.search"
                        DisplayColon="true" EnableViewState="false" />
                    <cms:CMSTextBox ID="txtSearch" runat="server" /><cms:LocalizedButton ID="btnSearch"
                        runat="server" ResourceString="general.search" CssClass="ContentButton" />
                </asp:Panel>
            </td>
            <td>
                <asp:Panel ID="pnlParentDocument" runat="server" CssClass="RightAlign" EnableViewState="false">
                    <asp:HyperLink ID="lnkUpperDoc" runat="server" NavigateUrl="#" EnableViewState="false">
                        <asp:Image ID="imgUpperDoc" runat="server" EnableViewState="false" CssClass="NewItemImage" />
                        <cms:LocalizedLabel ID="lblUpperDoc" runat="server" ResourceString="synchronization.upperdoc"
                            EnableViewState="false" />
                    </asp:HyperLink>
                </asp:Panel>
            </td>
        </tr>
    </table>
    <asp:Panel ID="pnlInfo" runat="server" Visible="false">
        <cms:LocalizedLabel ID="lblInfo" runat="server" CssClass="InfoLabel" EnableViewState="false" />
    </asp:Panel>
    <asp:Panel ID="pnlUniGrid" runat="server">
        <cms:UniGrid ID="uniGrid" runat="server" GridName="~/CMSModules/Staging/Tools/Tasks/DocumentsList.xml"
            OrderBy="DocumentName" IsLiveSite="false" ExportFileName="cms_document" />
    </asp:Panel>
</asp:Content>
