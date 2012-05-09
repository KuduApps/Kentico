<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Ecommerce_Pages_Tools_Customers_Customer_Edit_Credit_Edit"
    MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Theme="Default" Title="Credit event edit"
    CodeFile="Customer_Edit_Credit_Edit.aspx.cs" %>

<%@ Register Src="~/CMSModules/Ecommerce/Controls/UI/PriceSelector.ascx" TagName="PriceSelector"
    TagPrefix="cms" %>
<asp:Content ContentPlaceHolderID="plcContent" ID="content" runat="server">
    <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
        Visible="false" />
    <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
        Visible="false" />
    <table style="vertical-align: top">
        <tr>
            <td class="FieldLabel">
                <asp:Label runat="server" ID="lblEventName" EnableViewState="false" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtEventName" runat="server" CssClass="TextBoxField" MaxLength="200"
                    EnableViewState="false" />
                <cms:CMSRequiredFieldValidator ID="rfvEventName" runat="server" ControlToValidate="txtEventName"
                    EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <asp:Label runat="server" ID="lblEventCreditChange" EnableViewState="false" />
            </td>
            <td>
                <cms:PriceSelector ID="txtEventCreditChange" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <asp:Label runat="server" ID="lblEventDate" EnableViewState="false" />
            </td>
            <td>
                <cms:DateTimePicker ID="dtPickerEventDate" runat="server" EditTime="false" EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top; padding-top: 5px" class="FieldLabel">
                <asp:Label runat="server" ID="lblEventDescription" EnableViewState="false" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtEventDescription" runat="server" TextMode="MultiLine" CssClass="TextAreaField"
                    EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <cms:CMSButton runat="server" ID="btnOk" OnClick="btnOK_Click" EnableViewState="false"
                    CssClass="SubmitButton" />
            </td>
        </tr>
    </table>
</asp:Content>
