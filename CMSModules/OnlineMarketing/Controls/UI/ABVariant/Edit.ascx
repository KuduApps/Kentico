<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_OnlineMarketing_Controls_UI_ABVariant_Edit" CodeFile="Edit.ascx.cs" %>

<%@ Register Src="~/CMSFormControls/System/LocalizableTextBox.ascx" TagName="LocalizableTextBox" TagPrefix="cms" %>
<%@Register Src="~/CMSModules/Content/FormControls/Documents/SelectSinglePath.ascx" TagName="pathSelector" TagPrefix="cms" %>    
    
<cms:LocalizedLabel runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
    Visible="false" />
<cms:LocalizedLabel runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
    Visible="false" />
<table style="vertical-align: top">
    <tr>
        <td class="FieldLabel">
            <cms:LocalizedLabel ID="lblABVariantDisplayName" runat="server" EnableViewState="false" DisplayColon="true"
                ResourceString="abtesting.variantdisplayname" AssociatedControlID="txtABVariantDisplayName" />
        </td>
        <td>
            <cms:LocalizableTextBox ID="txtABVariantDisplayName" runat="server" CssClass="TextBoxField" MaxLength="110"
                EnableViewState="false" />
            <cms:CMSRequiredFieldValidator ID="rfvABVariantDisplayName" runat="server" Display="Dynamic"
                ControlToValidate="txtABVariantDisplayName:textbox" ValidationGroup="vgVariant" EnableViewState="false" />
        </td>
    </tr>
    <tr>
        <td class="FieldLabel">
            <cms:LocalizedLabel ID="lblABVariantName" runat="server" EnableViewState="false" DisplayColon="true"
                ResourceString="abtesting.variantcodename" AssociatedControlID="txtABVariantName" />
        </td>
        <td>
            <cms:CMSTextBox ID="txtABVariantName" runat="server" CssClass="TextBoxField" MaxLength="50"
                EnableViewState="false" />
            <cms:CMSRequiredFieldValidator ID="rfvABVariantName" runat="server" Display="Dynamic"
                ControlToValidate="txtABVariantName" ValidationGroup="vgVariant" EnableViewState="false" />
        </td>
    </tr>
    <tr>
        <td class="FieldLabel">
            <cms:LocalizedLabel ID="lblABVariantPath" runat="server" EnableViewState="false" DisplayColon="true"
                ResourceString="abtesting.abtestpage" AssociatedControlID="ucPath" />
        </td>
        <td>
                <cms:pathSelector runat="server" ID="ucPath" IsLiveSite ="false" />
        </td>
    </tr>
    <tr>
        <td>
        </td>
        <td>
            <cms:CMSButton runat="server" ID="btnOk" EnableViewState="false" CssClass="SubmitButton"
                OnClick="btnOk_Click" ValidationGroup="vgVariant" />
        </td>
    </tr>
</table>