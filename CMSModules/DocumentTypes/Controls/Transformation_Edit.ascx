<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Transformation_Edit.ascx.cs"
    Inherits="CMSModules_DocumentTypes_Controls_Transformation_Edit" %>
<cms:LocalizedLabel runat="server" ID="lblInfo" Visible="false" />
<cms:LocalizedLabel runat="server" ID="lblError" CssClass="ErrorLabel" Visible="false" />
<table class="TransformationNewTable">
    <cms:LocalizedLabel runat="server" ID="LocalizedLabel1" Visible="false" />
    <tr>
        <td>
            <cms:LocalizedLabel ID="lblName" runat="server" ResourceString="transformationedit.transformationname" />
        </td>
        <td>
            <cms:CMSTextBox ID="txtName" runat="server" CssClass="TextBoxField" MaxLength="100" />
            <cms:CMSRequiredFieldValidator ID="rfvCodeName" ControlToValidate="txtName" Display="Dynamic"
                runat="server" />
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
        <td>
            <cms:LocalizedButton ID="btnOK" runat="server" OnClick="btnOK_Click" CssClass="SubmitButton"
                ResourceString="general.ok" />
        </td>
    </tr>
</table>
