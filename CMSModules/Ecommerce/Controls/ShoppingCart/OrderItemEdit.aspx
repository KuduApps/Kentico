<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Ecommerce_Controls_ShoppingCart_OrderItemEdit"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/Dialogs/ModalDialogPage.master"
    Title="Shopping cart - Order item edit" CodeFile="OrderItemEdit.aspx.cs" %>

<%@ Register Src="~/CMSModules/Ecommerce/Controls/UI/PriceSelector.ascx" TagName="PriceSelector"
    TagPrefix="cms" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">

    <script type="text/javascript">
        //<![CDATA[

        function CloseAndRefresh(url) {
            wopener.window.location.replace(url);
            window.close();
        }

        //]]>
    </script>

    <asp:Panel ID="pnlContent" runat="server" CssClass="PageContent">
        <asp:Label ID="lblError" runat="server" EnableViewState="false" Visible="false" CssClass="ErrorLabel" />
        <table>
            <%-- Product name --%>
            <tr>
                <td>
                    <asp:Label ID="lblSKUName" runat="server" EnableViewState="false" />
                </td>
                <td>
                    <cms:CMSTextBox ID="txtSKUName" runat="server" CssClass="TextBoxField" MaxLength="450" />
                    <div>
                        <cms:CMSRequiredFieldValidator ID="rfvSKUName" runat="server" ControlToValidate="txtSKUName"
                            Display="Dynamic" EnableViewState="false" />
                    </div>
                </td>
            </tr>
            <%-- Price --%>
            <tr>
                <td>
                    <asp:Label ID="lblSKUPrice" runat="server" EnableViewState="false" />
                </td>
                <td>
                    <cms:PriceSelector ID="txtSKUPrice" runat="server" ValidatorOnNewLine="true" />
                </td>
            </tr>
            <%-- Units --%>
            <tr>
                <td>
                    <asp:Label ID="lblSKUUnits" runat="server" EnableViewState="false" />
                </td>
                <td>
                    <cms:CMSTextBox ID="txtSKUUnits" runat="server" CssClass="TextBoxField" MaxLength="9" />
                    <div>
                        <cms:CMSRequiredFieldValidator ID="rfvSKUUnits" runat="server" ControlToValidate="txtSKUUnits"
                            Display="Dynamic" />
                    </div>
                </td>
            </tr>
            <%-- Is private --%>
            <asp:PlaceHolder runat="server" ID="plcIsPrivate" Visible="false">
                <tr>
                    <td>
                        <cms:LocalizedLabel ID="LocalizedLabel1" runat="server" EnableViewState="false" ResourceString="orderitemedit.isprivate"
                            DisplayColon="true" />
                    </td>
                    <td>
                        <asp:CheckBox runat="server" ID="chkIsPrivate" Checked="false" />
                    </td>
                </tr>
            </asp:PlaceHolder>
            <%-- Text option --%>
            <asp:PlaceHolder runat="server" ID="plcItemText" Visible="false">
                <tr>
                    <td style="vertical-align: top;">
                        <cms:LocalizedLabel runat="server" ID="lblItemText" EnableViewState="false" ResourceString="general.text"
                            DisplayColon="true" />
                    </td>
                    <td>
                        <cms:CMSTextBox ID="txtItemText" runat="server" CssClass="TextBoxField" Visible="false" />
                        <cms:CMSTextBox ID="txtItemMultiText" runat="server" CssClass="TextAreaField" Visible="false"
                            TextMode="MultiLine" />
                    </td>
                </tr>
            </asp:PlaceHolder>
            <%-- Valid to --%>
            <asp:PlaceHolder runat="server" ID="plcValidTo" Visible="false">
                <tr>
                    <td>
                        <cms:LocalizedLabel runat="server" EnableViewState="false" ResourceString="orderitemedit.validto"
                            DisplayColon="true" />
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblValidTo" />
                    </td>
                </tr>
            </asp:PlaceHolder>
        </table>
    </asp:Panel>
    <asp:Literal ID="ltlScript" runat="server" EnableViewState="false" />
</asp:Content>
<asp:Content ID="plcFooter" runat="server" ContentPlaceHolderID="plcFooter" EnableViewState="false">
    <div class="FloatRight">
        <cms:CMSButton ID="btnOk" runat="server" OnClick="btnOk_Click" CssClass="SubmitButton"
            EnableViewState="false" />
        <cms:CMSButton ID="btnCancel" runat="server" CssClass="SubmitButton" EnableViewState="false"
            OnClientClick="window.close(); return false;" />
    </div>
</asp:Content>
