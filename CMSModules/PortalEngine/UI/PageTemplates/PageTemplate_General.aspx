<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_PortalEngine_UI_PageTemplates_PageTemplate_General"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Page Template Edit - General"
    CodeFile="PageTemplate_General.aspx.cs" %>

<%@ Register Src="~/CMSFormControls/System/LocalizableTextBox.ascx" TagName="LocalizableTextBox" TagPrefix="cms" %>

<%@ Register Src="~/CMSModules/AdminControls/Controls/MetaFiles/File.ascx" TagName="File" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/PortalEngine/FormControls/PageTemplates/PageTemplateLevels.ascx"
    TagName="InheritLevels" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/PortalEngine/Controls/PageTemplates/SelectPageTemplate.ascx"
    TagName="SelectPageTemplate" TagPrefix="cms" %>
<%@ Register Src="~/CMSFormControls/System/UserControlSelector.ascx" TagPrefix="cms"
    TagName="FileSystemSelector" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false" />
    <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
        Visible="false" />
    <table>
        <tr>
            <td class="FieldLabel">
                <asp:Label ID="lblTemplateDisplayName" runat="server" />
            </td>
            <td>
                <cms:LocalizableTextBox ID="txtTemplateDisplayName" runat="server" CssClass="TextBoxField" MaxLength="200" /><br />
                <cms:CMSRequiredFieldValidator ID="rfvTemplateDisplayName" runat="server" ControlToValidate="txtTemplateDisplayName:textbox"
                    Display="dynamic" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <asp:Label ID="lblTemplateCodeName" runat="server" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtTemplateCodeName" runat="server" CssClass="TextBoxField" MaxLength="100" /><br />
                <cms:CMSRequiredFieldValidator ID="rfvTemplateCodeName" runat="server" ControlToValidate="txtTemplateCodeName"
                    Display="dynamic" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <asp:Label ID="lblTemplateCategory" runat="server" />
            </td>
            <td>
                <cms:SelectPageTemplate ID="categorySelector" runat="server" ShowAdHocCategory="true"
                    EnableCategorySelection="true" ShowTemplates="false" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <asp:Label ID="lblTemplateDescription" runat="server" />
            </td>
            <td>
                <cms:LocalizableTextBox ID="txtTemplateDescription" runat="server" TextMode="MultiLine" CssClass="TextAreaField" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblUploadFile" runat="server" />
            </td>
            <td>
                <cms:File ID="UploadFile" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <asp:Label ID="lblTemplateType" runat="server" />
            </td>
            <td>
                <cms:LocalizedDropDownList runat="server" ID="drpPageType" AutoPostBack="true" OnSelectedIndexChanged="radAspx_CheckedChange"
                    CssClass="DropDownField">
                </cms:LocalizedDropDownList>
            </td>
        </tr>
        <asp:PlaceHolder runat="server" ID="plcAspx">
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="FieldLabel">
                    <cms:LocalizedLabel ID="lblTemplateFileName" runat="server" ResourceString="general.filename"
                        DisplayColon="true" />
                </td>
                <td>
                    <cms:FileSystemSelector ID="FileSystemSelector" runat="server" />
                </td>
            </tr>
        </asp:PlaceHolder>
        <asp:PlaceHolder runat="server" ID="plcPortal">
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="FieldLabel">
                    <asp:Label ID="lblShowAsMasterTemplate" runat="server" />
                </td>
                <td>
                    <asp:CheckBox ID="chkShowAsMasterTemplate" runat="server" />
                </td>
            </tr>
            <tr runat="server" id="inheritLevels">
                <td class="FieldLabel" style="vertical-align: top; padding-top: 3px;">
                    <asp:Label ID="lblInheritLevels" runat="server" />
                </td>
                <td>
                    <cms:InheritLevels ID="lvlElem" runat="server" />
                </td>
            </tr>
        </asp:PlaceHolder>
    </table>
</asp:Content>
