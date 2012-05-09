<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_PortalEngine_Controls_Layout_PageTemplateFlatSelector" CodeFile="PageTemplateFlatSelector.ascx.cs" %>
<%@ Register Src="~/CMSAdminControls/UI/UniControls/UniFlatSelector.ascx" TagName="UniFlatSelector"
    TagPrefix="cms" %>

<script type="text/javascript">
    //<![CDATA[
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
    //]]>
</script>

<cms:CMSUpdatePanel runat="server" ID="pnlUpdate" UpdateMode="Conditional">
    <ContentTemplate>
        <cms:UniFlatSelector ID="flatElem" runat="server">
            <HeaderTemplate>
                <div class="SelectorFlatItems">
            </HeaderTemplate>
            <ItemTemplate>
                <div class="SelectorEnvelope" style="overflow: hidden">
                    <div class="SelectorFlatImage">
                        <img alt="Template image" src="<%# flatElem.GetFlatImageUrl(Eval("MetaFileGUID")) %>" />
                    </div>
                    <span class="SelectorFlatText">
                        <%# HTMLHelper.HTMLEncode(Convert.ToString(Eval("PageTemplateDisplayName")))%></span>
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
