<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_PortalEngine_Controls_WebParts_WebPartFlatSelector"
    CodeFile="WebPartFlatSelector.ascx.cs" %>
<%@ Register Src="~/CMSAdminControls/UI/UniControls/UniFlatSelector.ascx" TagName="UniFlatSelector"
    TagPrefix="cms" %>

<script type="text/javascript">

    // Javacript after async postback
    function pageLoad() {
        // Resizes area
        if (jQuery.isFunction(window.resizearea)) {
            resizearea();
        }
        
        // Uniflat search
        var timer = null;
        SetupSearch();
    }      
</script>

<cms:CMSUpdatePanel runat="server" ID="pnlUpdate" UpdateMode="Conditional">
    <ContentTemplate>
        <cms:UniFlatSelector ID="flatElem" runat="server">
            <HeaderTemplate>
                <div class="SelectorFlatItems">
            </HeaderTemplate>
            <ItemTemplate>
                <div class="SelectorEnvelope">
                    <div class="SelectorFlatImage">
                        <img alt="WebPart image" src="<%# flatElem.GetFlatImageUrl(Eval("MetafileGUID")) %>" />
                    </div>
                    <span class="SelectorFlatText">
                        <%# HTMLHelper.HTMLEncode(ResHelper.LocalizeString(Convert.ToString(Eval("WebpartDisplayName")))) %></span>
                </div>
            </ItemTemplate>
            <FooterTemplate>
                <div style="clear: both">
                </div>
                </div>
            </FooterTemplate>
        </cms:UniFlatSelector>
        <div class="SelectorFlatDescription">
            <asp:Panel runat="server" ID="pnlDescription" CssClass="FlatDescription">
                <div class="SelectorName">
                    <asp:Literal runat="server" ID="litCategory" EnableViewState="false"></asp:Literal>
                </div>
            </asp:Panel>
        </div>
    </ContentTemplate>
</cms:CMSUpdatePanel>
