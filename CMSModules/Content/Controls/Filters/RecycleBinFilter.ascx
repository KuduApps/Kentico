<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RecycleBinFilter.ascx.cs"
    Inherits="CMSModules_Content_Controls_Filters_RecycleBinFilter" %>
<%@ Register Src="~/CMSModules/Membership/FormControls/Users/SelectUser.ascx" TagName="SelectUser"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/UI/UniGrid/Filters/TextSimpleFilter.ascx" TagName="TextSimpleFilter"
    TagPrefix="cms" %>
<asp:Panel ID="pnlContent" runat="server" DefaultButton="btnShow">
    <table>
        <asp:PlaceHolder ID="plcUsers" runat="server">
            <tr>
                <td class="FieldLabel">
                    <cms:LocalizedLabel ID="lblUsers" runat="server" DisplayColon="true" ResourceString="general.user" />
                </td>
                <td>
                    <cms:SelectUser ID="userSelector" runat="server" IsLiveSite="false" SelectionMode="SingleDropDownList"
                        AllowAll="true" AllowEmpty="false" ShowSiteFilter="false" />
                </td>
            </tr>
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="plcNameFilter" runat="server">
            <tr>
                <td class="FieldLabel">
                    <cms:LocalizedLabel ID="lblName" runat="server" DisplayColon="true" ResourceString="general.documentname" />
                </td>
                <td>
                    <cms:TextSimpleFilter ID="nameFilter" runat="server" Column="VersionDocumentName" />
                </td>
            </tr>
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="plcPathFilter" runat="server">
            <tr>
                <td class="FieldLabel">
                    <cms:LocalizedLabel ID="lblPath" runat="server" DisplayColon="true" ResourceString="general.documentnamepath" />
                </td>
                <td>
                    <cms:TextSimpleFilter ID="pathFilter" runat="server" Column="[CMS_VersionHistory].[DocumentNamePath]" />
                </td>
            </tr>
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="plcClass" runat="server">
            <tr>
                <td class="FieldLabel">
                    <cms:LocalizedLabel ID="lblClass" runat="server" DisplayColon="true" ResourceString="general.documenttype" />
                </td>
                <td>
                    <cms:TextSimpleFilter ID="classFilter" runat="server" Column="ClassDisplayName" />
                </td>
            </tr>
        </asp:PlaceHolder>
        <tr>
            <td>
            </td>
            <td>
                <cms:LocalizedButton ID="btnShow" runat="server" ResourceString="general.show" CssClass="ContentButton"
                    OnClick="btnShow_Click" />
            </td>
        </tr>
    </table>
</asp:Panel>
