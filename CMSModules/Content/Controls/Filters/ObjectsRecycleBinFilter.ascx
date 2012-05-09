<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ObjectsRecycleBinFilter.ascx.cs"
    Inherits="CMSModules_Content_Controls_Filters_ObjectsRecycleBinFilter" %>
<%@ Register Src="~/CMSModules/Membership/FormControls/Users/SelectUser.ascx" TagName="SelectUser"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/UI/UniGrid/Filters/TextSimpleFilter.ascx" TagName="TextSimpleFilter"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/Objects/FormControls/BinObjectTypeSelector.ascx" TagName="ObjectTypeSelector"
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
                        AllowEmpty="false" AllowAll="true" ShowSiteFilter="false" />
                </td>
            </tr>
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="plcNameFilter" runat="server">
            <tr>
                <td class="FieldLabel">
                    <cms:LocalizedLabel ID="lblName" runat="server" DisplayColon="true" ResourceString="general.objectname" />
                </td>
                <td>
                    <cms:TextSimpleFilter ID="nameFilter" runat="server" Column="VersionObjectDisplayName" />
                </td>
            </tr>
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="plcObjectTypeFilter" runat="server">
            <tr>
                <td class="FieldLabel">
                    <cms:LocalizedLabel ID="lblObjectType" runat="server" DisplayColon="true" ResourceString="general.objecttype" />
                </td>
                <td>
                    <cms:CMSUpdatePanel ID="pnlObjType" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <cms:ObjectTypeSelector ID="objTypeSelector" runat="server" />
                        </ContentTemplate>
                    </cms:CMSUpdatePanel>
                </td>
            </tr>
        </asp:PlaceHolder>
        <tr>
            <td>
            </td>
            <td>
                <cms:LocalizedButton ID="btnShow" runat="server" ResourceString="general.show" CssClass="ContentButton" />
            </td>
        </tr>
    </table>
</asp:Panel>
