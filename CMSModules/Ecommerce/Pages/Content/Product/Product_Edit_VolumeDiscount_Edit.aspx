<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Ecommerce_Pages_Content_Product_Product_Edit_VolumeDiscount_Edit"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/Dialogs/ModalDialogPage.master"
    Title="Product edit - volume discount edit" CodeFile="Product_Edit_VolumeDiscount_Edit.aspx.cs" %>

<%@ Register TagPrefix="cms" Namespace="CMS.UIControls" Assembly="CMS.UIControls" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <div class="PageContent">
        <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
            Visible="false" />
        <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
            Visible="false" />
        <table style="vertical-align: top">
            <tr>
                <td class="FieldLabel" style="height: 32px">
                    <asp:Label runat="server" ID="lblVolumeDiscountMinCount" EnableViewState="false" />
                </td>
                <td style="height: 32px">
                    <cms:CMSTextBox ID="txtVolumeDiscountMinCount" runat="server" CssClass="TextBoxField"
                        MaxLength="9" />&nbsp;&nbsp;
                    <div>
                        <cms:CMSRequiredFieldValidator ID="rfvMinCount" runat="server" ControlToValidate="txtVolumeDiscountMinCount"
                            Display="Dynamic" EnableViewState="false" /><cms:CMSRangeValidator ID="rvMinCount" runat="server"
                                ControlToValidate="txtVolumeDiscountMinCount" MinimumValue="1" Type="Integer"
                                Display="Dynamic" EnableViewState="false"></cms:CMSRangeValidator>
                    </div>
                </td>
            </tr>
            <tr>
                <td class="FieldLabel">
                    <asp:Label runat="server" ID="lblVolumeDiscountValue" EnableViewState="false" />
                </td>
                <td>
                    <asp:RadioButton ID="radDiscountRelative" runat="server" GroupName="group1" />
                    <asp:RadioButton ID="radDiscountAbsolute" runat="server" GroupName="group1" />
                </td>
            </tr>
            <tr>
                <td class="FieldLabel">
                </td>
                <td>
                    <cms:CMSTextBox ID="txtVolumeDiscountValue" runat="server" CssClass="TextBoxField" MaxLength="9" />&nbsp;<asp:Label
                        ID="lblCurrency" runat="server" />&nbsp;&nbsp;
                    <div>
                        <cms:CMSRequiredFieldValidator ID="rfvDiscountValue" runat="server" ControlToValidate="txtVolumeDiscountValue"
                            Display="Dynamic" EnableViewState="false" /><cms:CMSRangeValidator ID="rvDiscountValue"
                                runat="server" ControlToValidate="txtVolumeDiscountValue" Type="Double" Display="Dynamic"
                                MinimumValue="0" EnableViewState="false"></cms:CMSRangeValidator>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <asp:Literal ID="ltlScript" runat="server" EnableViewState="false" />
</asp:Content>
<asp:Content ID="cntFooter" runat="server" ContentPlaceHolderID="plcFooter">
    <div class="FloatRight">
        <cms:CMSButton runat="server" ID="btnOk" OnClick="btnOK_Click" EnableViewState="false"
            CssClass="SubmitButton" /><cms:CMSButton runat="server" ID="btnCancel" OnClientClick="window.close();" EnableViewState="false"
            CssClass="SubmitButton" />
    </div>
</asp:Content>
