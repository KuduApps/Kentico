<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_AdminControls_Controls_Class_TransformationEdit"
    CodeFile="TransformationEdit.ascx.cs" %>
<%@ Register Src="~/CMSAdminControls/UI/Macros/MacroEditor.ascx" TagName="MacroEditor"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/UI/PageElements/Help.ascx" TagName="Help" TagPrefix="cms" %>
<%@ Register Src="~/CMSFormControls/Filters/DocTypeFilter.ascx" TagName="DocTypeFilter"
    TagPrefix="cms" %>
<asp:PlaceHolder runat="server" ID="plcDocTypeFilter" Visible="false">
    <asp:Panel runat="server" ID="pnlDocTypeFilter" CssClass="PageHeaderLine">
        <cms:DocTypeFilter runat="server" ID="filter" RenderTableTag="true" EnableViewState="true" />
    </asp:Panel>
</asp:PlaceHolder>
<asp:Panel ID="pnlContent" runat="server">
    <asp:Panel ID="pnlCheckOutInfo" runat="server" CssClass="InfoLabel" EnableViewState="false">
        <asp:Label runat="server" ID="lblCheckOutInfo" />
        <br />
    </asp:Panel>
    <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
        Visible="false" />
    <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
        Visible="false" />
    <asp:PlaceHolder ID="plcControl" runat="server">
        <table style="width: 100%">
            <tr>
                <td class="FieldLabel">
                    <cms:LocalizedLabel runat="server" ID="lblTransformationName" EnableViewState="false"
                        ResourceString="DocumentType_Edit_Transformation_Edit.TransformName" />
                </td>
                <td colspan="2" style="width: 100%">
                    <cms:CMSTextBox runat="server" ID="tbTransformationName" CssClass="TextBoxField"
                        MaxLength="100" />
                    <cms:CMSRequiredFieldValidator ID="RequiredFieldValidatorTransformationName" runat="server"
                        ControlToValidate="tbTransformationName" Display="dynamic" EnableViewState="false" />
                </td>
            </tr>
            <tr>
                <td class="FieldLabel">
                    <cms:LocalizedLabel runat="server" ID="lblTransformationType" EnableViewState="false"
                        ResourceString="DocumentType_Edit_Transformation_Edit.TransformType" />
                </td>
                <td colspan="2">
                    <asp:DropDownList runat="server" ID="drpType" AutoPostBack="true" OnSelectedIndexChanged="drpTransformationType_SelectedIndexChanged"
                        EnableViewState="true" CssClass="ShortDropDownList" />
                </td>
            </tr>
            <tr>
                <td class="FieldLabel">
                    <cms:LocalizedLabel runat="server" ID="lblTransformationCode" EnableViewState="false"
                        ResourceString="DocumentType_Edit_Transformation_Edit.TransformCode" />
                </td>
                <td colspan="2">
                    <asp:PlaceHolder runat="server" ID="plcInfo">
                        <div class="PlaceholderInfoLine">
                            <asp:Label runat="server" ID="lblTransformationInfo" CssClass="InfoLabel" EnableViewState="false" />
                            <asp:Literal runat="server" ID="ltlDirectives" EnableViewState="false" />
                            <asp:PlaceHolder runat="server" ID="plcVirtualInfo" Visible="false">
                                <br />
                                <asp:Label runat="server" ID="lblVirtualInfo" CssClass="ErrorLabel" EnableViewState="false" />
                            </asp:PlaceHolder>
                        </div>
                    </asp:PlaceHolder>
                    <cms:CMSHtmlEditor runat="server" ID="tbWysiwyg" Width="99%" Height="300" Visible="false" />
                    <cms:MacroEditor runat="server" ID="txtCode" ShortID="e" Visible="false" />
                    <br />
                    <div>
                        <asp:PlaceHolder ID="plcTransformationCode" runat="server">
                            <asp:DropDownList runat="server" ID="drpTransformationCode" AutoPostBack="false"
                                CssClass="ShortDropDownList" EnableViewState="true" />
                        </asp:PlaceHolder>
                        <cms:LocalizedButton runat="server" ID="btnDefaultTransformation" OnClick="btnDefaultTransformation_Click"
                            ResourceString="DocumentType_Edit_Transformation_Edit.ButtonDefault" CausesValidation="false"
                            EnableViewState="false" CssClass="XXLongButton" />
                    </div>
                    <br />
                </td>
            </tr>
            <asp:PlaceHolder runat="server" ID="plcCssLink">
                <tr id="cssLink">
                    <td class="FieldLabel">
                    </td>
                    <td colspan="2">
                        <cms:LocalizedLinkButton runat="server" ID="lnkStyles" EnableViewState="false" ResourceString="general.addcss"
                            OnClientClick="document.getElementById('editCss').style.display = 'table-row'; document.getElementById('cssLink').style.display = 'none'; return false;" />
                    </td>
                </tr>
            </asp:PlaceHolder>
            <tr id="editCss" style="<%= (plcCssLink.Visible ? "display: none": "") %>">
                <td class="FieldLabel">
                    <cms:LocalizedLabel runat="server" ID="lblCSS" ResourceString="Container_Edit.ContainerCSS"
                        DisplayColon="true" EnableViewState="false" />
                </td>
                <td colspan="2">
                    <cms:ExtendedTextArea ID="txtCSS" runat="server" EnableViewState="false" EditorMode="Advanced"
                        Language="CSS" Width="99%" Height="200px" />
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    <table>
                        <tr>
                            <td>
                                <cms:Help ID="helpElem" runat="server" EnableViewState="false" />
                            </td>
                            <td>
                                <span>
                                    <cms:LocalizedHyperlink ID="lnkHelp" runat="server" EnableViewState="false" ResourceString="documenttype_edit_transformation.help" />
                                </span>
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                </td>
            </tr>
        </table>
    </asp:PlaceHolder>
</asp:Panel>
