<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSWebParts_Newsletters_NewsletterSubscriptionWebPart" CodeFile="~/CMSWebParts/Newsletters/NewsletterSubscriptionWebPart.ascx.cs" %>
<%@ Register Src="~/CMSFormControls/Inputs/SecurityCode.ascx" TagName="SecurityCode" TagPrefix="cms" %>
<asp:Panel ID="pnlSubscription" runat="server" DefaultButton="btnSubmit" CssClass="Subscription">
    <asp:Label runat="server" ID="lblInfo" CssClass="InfoMessage" EnableViewState="false"
        Visible="false" />
    <asp:Label runat="server" ID="lblError" CssClass="ErrorMessage" EnableViewState="false"
        Visible="false" />
    <div class="NewsletterSubscription">
        <table cellspacing="0" cellpadding="0" border="0" class="Table">
            <asp:PlaceHolder runat="server" ID="plcFirstName">
                <tr>
                    <td>
                        <cms:LocalizedLabel ID="lblFirstName" runat="server" AssociatedControlID="txtFirstName"
                            EnableViewState="false" />
                    </td>
                    <td>
                        <cms:CMSTextBox ID="txtFirstName" runat="server" CssClass="SubscriptionTextbox" MaxLength="200" />
                    </td>
                </tr>
            </asp:PlaceHolder>
            <asp:PlaceHolder runat="server" ID="plcLastName">
                <tr>
                    <td>
                        <cms:LocalizedLabel ID="lblLastName" runat="server" AssociatedControlID="txtLastName"
                            EnableViewState="false" />
                    </td>
                    <td>
                        <cms:CMSTextBox ID="txtLastName" runat="server" CssClass="SubscriptionTextbox" MaxLength="200" />
                    </td>
                </tr>
            </asp:PlaceHolder>
            <asp:PlaceHolder runat="server" ID="plcEmail">
                <tr>
                    <td>
                        <cms:LocalizedLabel ID="lblEmail" runat="server" AssociatedControlID="txtEmail" EnableViewState="false" />
                    </td>
                    <td>
                        <cms:CMSTextBox ID="txtEmail" runat="server" CssClass="SubscriptionTextbox" MaxLength="400" />
                    </td>
                </tr>
            </asp:PlaceHolder>
            <asp:PlaceHolder runat="server" ID="plcNwsList">
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:CheckBoxList runat="server" ID="chklNewsletters" CssClass="NewsletterList" />
                    </td>
                </tr>
            </asp:PlaceHolder>
            <asp:PlaceHolder runat="server" ID="plcCaptcha">
                <tr>
                    <td>
                        <cms:LocalizedLabel ID="lblCaptcha" runat="server" AssociatedControlID="scCaptcha"
                            EnableViewState="false" />
                    </td>
                    <td>
                        <cms:SecurityCode ID="scCaptcha" GenerateNumberEveryTime="false" runat="server" />
                    </td>
                </tr>
            </asp:PlaceHolder>
            <asp:PlaceHolder ID="pnlButtonSubmit" runat="server">
                <tr>
                    <td>
                    </td>
                    <td align="right">
                        <cms:LocalizedButton ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" CssClass="SubscriptionButton"
                            EnableViewState="false" />
                    </td>
                </tr>
            </asp:PlaceHolder>
            <asp:PlaceHolder ID="pnlImageSubmit" runat="server">
                <tr>
                    <td>
                    </td>
                    <td align="right">
                        <asp:ImageButton ID="btnImageSubmit" runat="server" OnClick="btnSubmit_Click" EnableViewState="false" />
                    </td>
                </tr>
            </asp:PlaceHolder>
        </table>
    </div>
</asp:Panel>
