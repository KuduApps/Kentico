<%@ Control Language="C#" AutoEventWireup="True" CodeFile="ValidationSettings.ascx.cs"
    Inherits="CMSModules_AdminControls_Controls_Class_FieldEditor_ValidationSettings" %>
    
<%@ Register Src="~/CMSFormControls/System/LocalizableTextBox.ascx" TagName="LocalizableTextBox"
    TagPrefix="cms" %>
<asp:Panel ID="pnlSectionValidation" runat="server" Visible="false" CssClass="FieldPanel">
    <table>
        <asp:PlaceHolder runat="server" ID="plcSpellCheck" Visible="false">
            <tr>
                <td class="EditingFormLabelCell">
                    <cms:LocalizedLabel ID="lblSpellCheck" runat="server" EnableViewState="false" ResourceString="TemplateDesigner.SpellCheck" />
                </td>
                <td>
                    <asp:CheckBox ID="chkSpellCheck" runat="server" CssClass="CheckBoxMovedLeft" Checked="true" />
                </td>
            </tr>
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="plcTextValidation" runat="server" Visible="false">
            <tr>
                <td class="EditingFormLabelCell">
                    <cms:LocalizedLabel ID="lblRegExpr" runat="server" EnableViewState="false" ResourceString="TemplateDesigner.RegularExpression" />
                </td>
                <td>
                    <cms:CMSTextBox ID="txtRegExpr" runat="server" CssClass="TextBoxField" />
                </td>
            </tr>
            <asp:PlaceHolder runat="server" ID="plcMinMaxLengthValidation">
                <tr>
                    <td class="EditingFormLabelCell">
                        <cms:LocalizedLabel ID="lblMinLength" runat="server" EnableViewState="false" ResourceString="TemplateDesigner.MinLength" />
                    </td>
                    <td>
                        <cms:CMSTextBox ID="txtMinLength" runat="server" CssClass="TextBoxField" MaxLength="9" />
                    </td>
                </tr>
                <tr>
                    <td class="EditingFormLabelCell">
                        <cms:LocalizedLabel ID="lblMaxLength" runat="server" EnableViewState="false" ResourceString="TemplateDesigner.MaxLength" />
                    </td>
                    <td>
                        <cms:CMSTextBox ID="txtMaxLength" runat="server" CssClass="TextBoxField" MaxLength="9" />
                    </td>
                </tr>
            </asp:PlaceHolder>
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="plcNumberValidation" runat="server" Visible="false">
            <tr>
                <td class="EditingFormLabelCell">
                    <cms:LocalizedLabel ID="lblMinValue" runat="server" EnableViewState="false" ResourceString="TemplateDesigner.MinValue" />
                </td>
                <td>
                    <cms:CMSTextBox ID="txtMinValue" runat="server" CssClass="TextBoxField" MaxLength="20" />
                </td>
            </tr>
            <tr>
                <td class="EditingFormLabelCell">
                    <cms:LocalizedLabel ID="lblMaxValue" runat="server" EnableViewState="false" ResourceString="TemplateDesigner.MaxValue" />
                </td>
                <td>
                    <cms:CMSTextBox ID="txtMaxValue" runat="server" CssClass="TextBoxField" MaxLength="20" />
                </td>
            </tr>
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="plcDateTimeValidation" runat="server" Visible="false">
            <tr>
                <td class="EditingFormLabelCell">
                    <cms:LocalizedLabel ID="lblFrom" runat="server" EnableViewState="false" ResourceString="TemplateDesigner.From" />
                </td>
                <td>
                    <cms:DateTimePicker ID="dateFrom" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="EditingFormLabelCell">
                    <cms:LocalizedLabel ID="lblTo" runat="server" EnableViewState="false" ResourceString="TemplateDesigner.To" />
                </td>
                <td>
                    <cms:DateTimePicker ID="dateTo" runat="server" />
                </td>
            </tr>
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="plcErrorMessageValidation" runat="server" Visible="false">
            <tr>
                <td class="EditingFormLabelCell">
                    <cms:LocalizedLabel ID="lblErrorMessage" runat="server" EnableViewState="false" ResourceString="TemplateDesigner.ErrorMessage" />
                </td>
                <td>
                    <cms:LocalizableTextBox ID="txtErrorMessage" runat="server" CssClass="TextBoxField" />
                </td>
            </tr>
        </asp:PlaceHolder>
    </table>
</asp:Panel>
