<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_OnlineMarketing_Controls_Content_VariantSlider"
    CodeFile="VariantSlider.ascx.cs" %>
<asp:Panel ID="pnlVariations" runat="server" CssClass="VariantSlider" EnableViewState="false">
    <asp:PlaceHolder ID="plcSliderPanel" runat="server">
        <div class="SliderItem">
            <asp:Panel ID="pnlLeft" runat="server" CssClass="SliderLeft">
            </asp:Panel>
        </div>
        <div class="FloatLeft">
            <asp:Panel ID="pnlSlider" runat="server" CssClass="SliderBarPanel">
                <cms:CMSTextBox ID="txtSlider" runat="server" EnableViewState="false"></cms:CMSTextBox>
                <ajaxToolkit:SliderExtender ID="sliderExtender" runat="server" TargetControlID="txtSlider"
                    BoundControlID="hdnSliderPosition" EnableHandleAnimation="true" Length="50"
                    EnableViewState="false">
                </ajaxToolkit:SliderExtender>
                <cms:CMSTextBox ID="hdnSliderPosition" runat="server" EnableViewState="false"></cms:CMSTextBox>
            </asp:Panel>
        </div>
        <div class="SliderItem">
            <asp:Panel ID="pnlRight" runat="server" CssClass="SliderRight">
            </asp:Panel>
        </div>
        <div class="SliderItem">
            <div class="SliderPartLabel">
                <div class="FloatLeft">
                    <asp:Label ID="lblPart" runat="server" Text="1"></asp:Label>
                </div>
                <div class="FloatLeft">/</div>
                <div class="FloatLeft">
                    <asp:Label ID="lblTotal" runat="server" Text="1"></asp:Label>
                </div>
            </div>
        </div>
        <div class="SliderItemSeparator">
        </div>
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="plcAddVariant" runat="server">
        <div class="SliderItem">
            <div class="SliderItemPadding">
                <asp:ImageButton ID="imgAddVariant" runat="server" CssClass="SliderBtn"/>
            </div>
        </div>
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="plcRemoveVariant" runat="server">
        <div class="SliderItem">
            <div class="SliderItemPadding">
                <asp:ImageButton ID="imgRemoveVariant" runat="server" CssClass="SliderBtnEnabled" />
                <asp:ImageButton ID="imgRemoveVariantDisabled" runat="server" CssClass="SliderBtnDisabled" />
            </div>
        </div>
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="plcVariantList" runat="server">
        <div class="SliderItem">
            <div class="SliderItemPadding">
                <asp:ImageButton ID="imgVariantList" runat="server" CssClass="SliderBtn"/>
            </div>
        </div>
    </asp:PlaceHolder>
    <asp:Literal ID="ltrScript" runat="server" EnableViewState="false" />
</asp:Panel>
