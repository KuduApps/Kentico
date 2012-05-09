<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_SmartSearch_SearchIndex_Search"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" CodeFile="SearchIndex_Search.aspx.cs" %>

<%@ Register Src="SearchTransformationItem.ascx" TagName="SearchTransformationItem"
    TagPrefix="cms" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <table>
        <tr>
            <td>
                <cms:LocalizedLabel runat="server" ID="lblSearchFor" AssociatedControlID="txtSearchFor"
                    CssClass="FieldLabel" DisplayColon="true" ResourceString="srch.dialog.searchfor"></cms:LocalizedLabel>
            </td>
            <td>
                <cms:CMSTextBox runat="server" ID="txtSearchFor" CssClass="TextBoxField" MaxLength="1000"></cms:CMSTextBox >
            </td>
        </tr>
        <tr>
            <td>
                <cms:LocalizedLabel runat="server" ID="lblSearchMode" AssociatedControlID="drpSearchMode"
                    CssClass="FieldLabel" DisplayColon="true" ResourceString="srch.dialog.mode"></cms:LocalizedLabel>
            </td>
            <td>
                <asp:DropDownList runat="server" ID="drpSearchMode" CssClass="DropDownField">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td><cms:LocalizedButton runat="server" ID="btnSearch" CssClass="ContentButton" OnClick="btnSearch_Click" ResourceString="general.search" />
            </td>
        </tr>
    </table>
    <asp:Label runat="server" ID="lblNoResults" EnableViewState="false" Visible="false"></asp:Label><br />
    <cms:BasicRepeater runat="server" ID="repSearchResults">
        <ItemTemplate>
            <cms:SearchTransformationItem ID="srchItem" runat="server" />
        </ItemTemplate>
    </cms:BasicRepeater>
    <cms:UniPager runat="server" ID="pgrSearch" PageControl="repSearchResults" HidePagerForSinglePage="true">
        <CurrentPageTemplate>
            <strong>
                <%# Eval("Page") %></strong>
        </CurrentPageTemplate>
        <PageNumbersTemplate>
            <a href="<%# Eval("PageURL") %>">
                <%# Eval("Page") %></a>
        </PageNumbersTemplate>
        <NextGroupTemplate>
            <a href="<%# Eval("NextGroupURL") %>">...</a>
        </NextGroupTemplate>
        <PreviousGroupTemplate>
            <a href="<%# Eval("PreviousGroupURL") %>">...</a>
        </PreviousGroupTemplate>
        <LayoutTemplate>
            <asp:PlaceHolder runat="server" ID="plcFirstPage"></asp:PlaceHolder>
            <asp:PlaceHolder runat="server" ID="plcPreviousPage"></asp:PlaceHolder>
            &nbsp;
            <asp:PlaceHolder runat="server" ID="plcPreviousGroup"></asp:PlaceHolder>
            <asp:PlaceHolder runat="server" ID="plcPageNumbers"></asp:PlaceHolder>
            <asp:PlaceHolder runat="server" ID="plcNextGroup"></asp:PlaceHolder>
            &nbsp;
            <asp:PlaceHolder runat="server" ID="plcNextPage"></asp:PlaceHolder>
            <asp:PlaceHolder runat="server" ID="plcLastPage"></asp:PlaceHolder>
            <%-- Results <%# Eval("FirstOnPage")%> - <%# Eval("LastOnPage") %> of <%# Eval("Items")%><br /> --%>
            Pages:
            <%# Eval("CurrentPage") %>
            of
            <%# Eval("Pages") %><br />
        </LayoutTemplate>
    </cms:UniPager>
</asp:Content>
