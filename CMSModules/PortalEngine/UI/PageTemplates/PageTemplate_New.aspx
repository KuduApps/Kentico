<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_PortalEngine_UI_PageTemplates_PageTemplate_New" Theme="Default"
    MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Page Templates - New Page Template" CodeFile="PageTemplate_New.aspx.cs" %>

<%@ Register Src="~/CMSFormControls/System/LocalizableTextBox.ascx" TagName="LocalizableTextBox" TagPrefix="cms" %>

<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:Label runat="server" ID="lblError" ForeColor="red" EnableViewState="false" Visible="false" />
    <asp:Literal ID="ltlScript" runat="server" />
    <table>
        <tr>
            <td class="FieldLabel">
                <asp:Label ID="lblTemplateDisplayName" runat="server" />
            </td>
            <td>
                <cms:LocalizableTextBox ID="txtTemplateDisplayName" runat="server" CssClass="TextBoxField" MaxLength="200"  />
                <cms:CMSRequiredFieldValidator ID="rfvTemplateDisplayName" runat="server" ControlToValidate="txtTemplateDisplayName:textbox" Display="dynamic" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <asp:Label ID="lblTemplateCodeName" runat="server" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtTemplateCodeName" runat="server" CssClass="TextBoxField" MaxLength="100"  />
                <cms:CMSRequiredFieldValidator ID="rfvTemplateCodeName" runat="server" ControlToValidate="txtTemplateCodeName" Display="dynamic" />
            </td>
        </tr>
        <tr>
            <td style="vertical-align:top; padding-top:5px" class="FieldLabel">
                <asp:Label ID="lblTemplateDescription" runat="server" />
            </td>
            <td>
                <cms:LocalizableTextBox ID="txtTemplateDescription" runat="server" TextMode="MultiLine" CssClass="TextAreaField" />
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>
                <cms:CMSButton ID="btnOk" runat="server" CssClass="SubmitButton" OnClick="btnOK_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
