<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_Ecommerce_Controls_ShoppingCart_ShoppingCart" CodeFile="ShoppingCart.ascx.cs" %>
<asp:Panel ID="pnlShoppingCart" runat="server" DefaultButton="btnNext">
    <asp:Label ID="lblError" runat="server" EnableViewState="false" CssClass="ErrorLabel"
        Visible="false" />
    <asp:Label ID="lblInfo" runat="server" EnableViewState="false" CssClass="InfoLabel"
        Visible="false" />
    <table class="CartTable" style="width: 100%" cellpadding="0" cellspacing="0">
        <tr>
            <td colspan="2">
                <table class="CartStepTable" style="width: 100%"  cellspacing="0" cellpadding="3" border="0">
                    <tr class="UniGridHead">
                        <th class="CartStepHeader">
                            <asp:Label ID="lblStepTitle" runat="server" EnableViewState="false" />
                        </th>
                    </tr>
                    <asp:PlaceHolder ID="plcCheckoutProcess" runat="server" EnableViewState="false">
                        <tr class="CartStepBody">
                            <td colspan="2" align="center" style="padding: 0px; text-align:center;">
                                <asp:PlaceHolder ID="plcStepImages" runat="server" EnableViewState="false" />
                            </td>
                        </tr>
                    </asp:PlaceHolder>
                    <tr class="CartStepBody">
                        <td style="padding-top: 0px;">
                            <asp:Panel ID="plcCartStep" runat="server" CssClass="CartStepPanel">
                                <asp:Panel ID="pnlCartStepInner" runat="server" CssClass="CartStepInnerPanel" />
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="TextLeftt" style="padding-top: 10px;">
                <cms:CMSButton ID="btnBack" runat="server" OnClick="btnBack_Click" CssClass="SubmitButton"
                    ValidationGroup="ButtonBack" EnableViewState="false" />
            </td>
            <td class="TextRight" style="padding-top: 10px;">
                <cms:CMSButton ID="btnNext" runat="server" OnClick="btnNext_Click" CssClass="SubmitButton"
                    ValidationGroup="ButtonNext" EnableViewState="false" />
            </td>
        </tr>
    </table>
</asp:Panel>
