<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_Notifications_Development_Templates_Template_Edit_Text"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Templates edit - text" CodeFile="Template_Edit_Text.aspx.cs" %>

<%@ Register Src="~/CMSModules/Notifications/Controls/TemplateText.ascx" TagName="TemplateText"
    TagPrefix="cms" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" Visible="false" />
    <asp:PlaceHolder runat="server" ID="plcMacros" Visible="false">
        <table>
            <tr>
                <td>
                    <div style="width: 215px; white-space: nowrap;">
                        <a style="text-decoration: none;" href="#" onclick="document.getElementById('macrosHelp').style.display = (document.getElementById('macrosHelp').style.display == 'none')? 'block' : 'none';">
                            <asp:Label ID="lnkMoreMacros" runat="server" CssClass="InfoLabel" EnableViewState="false" /></a>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div id="macrosHelp" style="display: none; margin-bottom: 10px;">
                        <strong>
                            <cms:LocalizedLabel ID="lblHelpHeader" runat="server" CssClass="InfoLabel" DisplayColon="true"
                                EnableViewState="false" /></strong>
                        <asp:Table ID="tblHelp" runat="server" EnableViewState="false" GridLines="Horizontal"
                            CellPadding="3" CellSpacing="0" Width="100%">
                        </asp:Table>
                    </div>
                </td>
            </tr>
        </table>
    </asp:PlaceHolder>
    <cms:TemplateText runat="server" ID="templateTextElem" />
</asp:Content>
