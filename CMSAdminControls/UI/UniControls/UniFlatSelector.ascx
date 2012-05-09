<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSAdminControls_UI_UniControls_UniFlatSelector" CodeFile="UniFlatSelector.ascx.cs" %>
<div id="<%= this.ClientID %>" class="UniFlatSelector">   
<script type="text/javascript" language="javascript">
    //<![CDATA[
        // Initialize variables
        var selectedFlatItem = null;
        var selectedValue = null;
        var selectedItemName = null;
    //]]>
</script> 
    <asp:Panel runat="server" ID="pnlSearch" CssClass="UniFlatSearchPanel" DefaultButton="btnSearch">
        <cms:LocalizedLabel runat="server" ID="lblSearch" AssociatedControlID="txtSearch" CssClass="ContentLabel"></cms:LocalizedLabel>
        <cms:CMSTextBox runat="server" ID="txtSearch" CssClass="TextBoxField" MaxLength="200"></cms:CMSTextBox >
        <cms:LocalizedButton runat="server" ID="btnSearch" ResourceString="general.search"
            OnClick="btnSearch_Click" CssClass="ContentButton" EnableViewState="false" />
        <cms:LocalizedCheckBox ID="chkSearch" runat="server" Visible="false" CssClass="UniFlatSearchCheckBox" AutoPostBack="true" />
    </asp:Panel>
    <cms:CMSUpdatePanel runat="server" ID="pnlUpdate">
        <ContentTemplate>
            <asp:Panel runat="server" ID="pnlFlatArea" CssClass="UniFlatContent">
                <asp:Panel runat="server" ID="pnlRepeater" CssClass="UniFlatContentItems">                             
                    <asp:Panel runat="server" ID="pnlLabel" CssClass="SelectorNoResults" Visible="false"
                        EnableViewState="false">
                        <cms:LocalizedLabel runat="server" ID="lblError" CssClass="ErrorLabel" Visible="false" EnableViewState="false" />  
                        <asp:Label runat="server" ID="lblNoRecords" Text="uniflatselector.norecords"
                            EnableViewState="false" Visible="false" />
                    </asp:Panel>
                    <cms:QueryRepeater runat="server" ID="repItems" OnItemDataBound="repItems_ItemDataBound">
                    </cms:QueryRepeater>
                    <asp:HiddenField ID="hdnSelectedItem" runat="server" EnableViewState="false"/>
                </asp:Panel>
            </asp:Panel>
            <asp:Panel runat="server" ID="pnlPager" CssClass="UniFlatPager">
                <cms:UniPager ID="pgrItems" runat="server" PageControl="repItems" PageSize="10" GroupSize="10"
                    PagerMode="PostBack" HidePagerForSinglePage="true">
                    <PreviousGroupTemplate>
                        <span class="UniFlatPagerPreviousGroup"><a href="<%# Eval("PreviousGroupUrl") %>">...</a>&nbsp;
                        </span>
                    </PreviousGroupTemplate>
                    <PageNumbersTemplate>
                        <span class="UniFlatPagerPage"><a href="<%# Eval("PageUrl") %>">
                            <%# Eval("Page") %></a> </span>
                    </PageNumbersTemplate>
                    <PageNumbersSeparatorTemplate>
                        &nbsp;
                    </PageNumbersSeparatorTemplate>
                    <CurrentPageTemplate>
                        <span class="UniFlatPagerCurrentPage">
                            <%# Eval("Page") %>
                        </span>
                    </CurrentPageTemplate>
                    <NextGroupTemplate>
                        <span class="UniFlatPagerNextGroup">&nbsp;<a href="<%# Eval("NextGroupUrl")  %>">...</a>                               
                        </span>
                    </NextGroupTemplate>
                </cms:UniPager>
            </asp:Panel>
            <asp:HiddenField ID="hdnItemsCount" runat="server" />
            <asp:Button runat="server" ID="btnUpdate" CssClass="HiddenButton" />
        </ContentTemplate>
    </cms:CMSUpdatePanel>
</div>
