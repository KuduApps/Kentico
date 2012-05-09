<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSAdminControls_UI_UniGrid_Controls_UniGridPager"
    CodeFile="UniGridPager.ascx.cs" %>
<table class="UniGridPager" cellpadding="0" cellspacing="0">
    <tr>
        <td style="width: 100%; white-space: nowrap;">
            <cms:UniPager ID="pagerElem" ShortID="p" runat="server">
                <FirstPageTemplate>
                    <td>
                        <a class="UnigridPagerFirst" href="<%# Eval("FirstURL") %>">&nbsp;</a>
                    </td>
                </FirstPageTemplate>
                <PreviousPageTemplate>
                    <td>
                        <a class="UnigridPagerPrev" href="<%# Eval("PreviousURL") %>">&nbsp;</a>
                    </td>
                </PreviousPageTemplate>
                <PreviousGroupTemplate>
                    <td>
                        <a class="UnigridPagerPage" href="<%# Eval("PreviousGroupURL") %>">...</a>
                    </td>
                </PreviousGroupTemplate>
                <PageNumbersTemplate>
                    <td>
                        <a class="UnigridPagerPage" href="<%# Eval("PageURL") %>">
                            <%# Eval("Page") %></a>
                    </td>
                </PageNumbersTemplate>
                <PageNumbersSeparatorTemplate>
                </PageNumbersSeparatorTemplate>
                <CurrentPageTemplate>
                    <td>
                        <span class="UnigridPagerSelectedPage">
                            <%# Eval("Page") %></span>
                    </td>
                </CurrentPageTemplate>
                <NextGroupTemplate>
                    <td>
                        <a class="UnigridPagerPage" href="<%# Eval("NextGroupURL") %>">...</a>
                    </td>
                </NextGroupTemplate>
                <NextPageTemplate>
                    <td>
                        <a class="UnigridPagerNext" href="<%# Eval("NextURL") %>">&nbsp;</a>
                    </td>
                </NextPageTemplate>
                <LastPageTemplate>
                    <td>
                        <a class="UnigridPagerLast" href="<%# Eval("LastURL") %>">&nbsp;</a>
                    </td>
                </LastPageTemplate>
                <DirectPageTemplate>
                    <td style="white-space: nowrap;">
                        <div class="UnigridPagerPageSize">
                            <cms:LocalizedLabel ID="lblPage" runat="server" ResourceString="UniGrid.Page" />
                            &nbsp;
                            <cms:CMSTextBox ID="txtPage" runat="server" Style="width: 25px;" />
                            <asp:DropDownList ID="drpPage" runat="server" Style="width: 50px;" />
                            &nbsp;/&nbsp;
                            <%# Eval("Pages") %>
                        </div>
                    </td>
                </DirectPageTemplate>
                <LayoutTemplate>
                    <table cellspacing="0" cellpadding="0">
                        <tr>
                            <asp:PlaceHolder runat="server" ID="plcFirstPage"></asp:PlaceHolder>
                            <asp:PlaceHolder runat="server" ID="plcPreviousPage"></asp:PlaceHolder>
                            <td style="white-space: nowrap;" class="UnigridPagerPages">
                                <table class="UniGridPagerNoSeparator" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <asp:PlaceHolder runat="server" ID="plcPreviousGroup"></asp:PlaceHolder>
                                        <asp:PlaceHolder runat="server" ID="plcPageNumbers"></asp:PlaceHolder>
                                        <asp:PlaceHolder runat="server" ID="plcNextGroup"></asp:PlaceHolder>
                                    </tr>
                                </table>
                            </td>
                            <asp:PlaceHolder runat="server" ID="plcNextPage"></asp:PlaceHolder>
                            <asp:PlaceHolder runat="server" ID="plcLastPage"></asp:PlaceHolder>
                            <asp:PlaceHolder runat="server" ID="plcDirectPage"></asp:PlaceHolder>
                        </tr>
                    </table>
                </LayoutTemplate>
            </cms:UniPager>
            <asp:PlaceHolder ID="plcSpace" runat="server" Visible="false">&nbsp;</asp:PlaceHolder>
        </td>
        <asp:PlaceHolder ID="plcPageSize" runat="server">
            <td style="white-space: nowrap;" class="UniGridPagerNoSeparator">
                <div class="UnigridPagerPageSize">
                    <cms:LocalizedLabel ID="lblPageSize" runat="server" EnableViewState="false" ResourceString="UniGrid.ItemsPerPage"
                        AssociatedControlID="drpPageSize" />
                    &nbsp;
                    <asp:DropDownList ID="drpPageSize" runat="server" AutoPostBack="true" />
                </div>
            </td>
        </asp:PlaceHolder>
    </tr>
</table>
