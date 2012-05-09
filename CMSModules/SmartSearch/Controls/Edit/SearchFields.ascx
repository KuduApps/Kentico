<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_SmartSearch_Controls_Edit_SearchFields"
    CodeFile="SearchFields.ascx.cs" %>
<%@ Register Src="~/CMSModules/SmartSearch/Controls/Edit/ClassFields.ascx" TagName="ClassFields"
    TagPrefix="cms" %>
<asp:Panel ID="pnlBody" runat="server">
    <cms:LocalizedLabel runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
        Visible="false" ResourceString="general.changessaved" />
    <cms:LocalizedLabel runat="server" ID="lblRebuildInfo" CssClass="InfoLabel" EnableViewState="false"
        Visible="false" ResourceString="searchindex.requiresrebuild" />
    <table>
        <tr>
            <td>
                <cms:LocalizedLabel runat="server" ID="lblTitleField" ResourceString="srch.titlefield"
                    DisplayColon="true" />
            </td>
            <td>
                <asp:DropDownList ID="drpTitleField" runat="server" EnableViewState="true" CssClass="DropDownField">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <cms:LocalizedLabel runat="server" ID="lblContentField" ResourceString="srch.contentfield"
                    DisplayColon="true" />
            </td>
            <td>
                <asp:DropDownList ID="drpContentField" runat="server" EnableViewState="true" CssClass="DropDownField">
                </asp:DropDownList>
            </td>
        </tr>
        <asp:PlaceHolder runat="server" ID="plcImage">
            <tr>
                <td>
                    <cms:LocalizedLabel runat="server" ID="lblImageField" ResourceString="srch.imagefield"
                        DisplayColon="true" />
                </td>
                <td>
                    <asp:DropDownList ID="drpImageField" runat="server" EnableViewState="true" CssClass="DropDownField">
                    </asp:DropDownList>
                </td>
            </tr>
        </asp:PlaceHolder>
        <tr>
            <td>
                <cms:LocalizedLabel runat="server" ID="lblDateField" ResourceString="srch.datefield"
                    DisplayColon="true" />
            </td>
            <td>
                <asp:DropDownList ID="drpDateField" runat="server" EnableViewState="true" CssClass="DropDownField">
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    <div runat="server" id="pnlIndent" visible="false" class="SearchFieldsIndentPanel">
    </div>
    <cms:ClassFields ID="ClassFields" runat="server" Visible="true" />
</asp:Panel>
