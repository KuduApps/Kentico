<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FieldAppearance.ascx.cs"
    Inherits="CMSModules_AdminControls_Controls_Class_FieldEditor_FieldAppearance" %>
<%@ Register Src="~/CMSFormControls/System/UserControlTypeSelector.ascx" TagPrefix="cms"
    TagName="TypeSelector" %>
<%@ Register Src="~/CMSFormControls/System/LocalizableTextBox.ascx" TagName="LocalizableTextBox"
    TagPrefix="cms" %>
<asp:Panel ID="pnlAppearance" runat="server" Enabled="true" CssClass="FieldPanel">
    <table>
        <asp:PlaceHolder ID="plcPublicField" runat="server">
            <tr>
                <td class="FieldLabel">
                    <cms:LocalizedLabel ID="lblPublicField" runat="server" EnableViewState="false" ResourceString="TemplateDesigner.PublicField" />
                </td>
                <td>
                    <asp:CheckBox ID="chkPublicField" runat="server" CssClass="CheckBoxMovedLeft" />
                </td>
            </tr>
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="plcVisibility" runat="server" Visible="false">
            <tr>
                <td class="FieldLabel">
                    <cms:LocalizedLabel ID="lblVisibility" runat="server" EnableViewState="false" ResourceString="TemplateDesigner.Visibility" />
                </td>
                <td>
                    <cms:VisibilityControl ID="ctrlVisibility" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="FieldLabel">
                    <cms:LocalizedLabel ID="lblVisibilityControl" runat="server" EnableViewState="false"
                        ResourceString="TemplateDesigner.VisibilityControl" />
                </td>
                <td>
                    <asp:DropDownList ID="drpVisibilityControl" runat="server" DataValueField="UserControlCodeName"
                        DataTextField="UserControlDisplayName" CssClass="DropDownField" />
                </td>
            </tr>
            <tr>
                <td class="FieldLabel">
                    <cms:LocalizedLabel ID="lblChangeVisibility" runat="server" EnableViewState="false"
                        ResourceString="TemplateDesigner.ChangeVisibility" />
                </td>
                <td>
                    <asp:CheckBox ID="chkChangeVisibility" runat="server" CssClass="CheckBoxMovedLeft" />
                </td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
        </asp:PlaceHolder>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel ID="lblFieldCaption" runat="server" EnableViewState="false" ResourceString="TemplateDesigner.FieldCaption" />
            </td>
            <td>
                <cms:LocalizableTextBox ID="txtFieldCaption" runat="server" CssClass="TextBoxField" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel ID="lblTypeSelector" runat="server" EnableViewState="false" ResourceString="fieldeditor.formfieldtype"
                    DisplayColon="true" />
            </td>
            <td>
                <cms:CMSUpdatePanel ID="pnlUpdate" runat="server">
                    <ContentTemplate>
                        <cms:TypeSelector ID="drpTypeSelector" runat="server" CssClass="DropDownField" AutoPostBack="true"
                            IncludeAllItem="true" />
                    </ContentTemplate>
                </cms:CMSUpdatePanel>
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel ID="lblField" runat="server" EnableViewState="false" ResourceString="objecttype.cms_formusercontrol"
                    DisplayColon="true" />
            </td>
            <td>
                <asp:DropDownList ID="drpField" runat="server" AutoPostBack="True" DataTextField="Text"
                    DataValueField="Value" CssClass="DropDownField" OnSelectedIndexChanged="drpFieldType_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel ID="lblFieldDescription" runat="server" EnableViewState="false"
                    ResourceString="TemplateDesigner.FieldDescription" />
            </td>
            <td>
                <cms:LocalizableTextBox ID="txtDescription" runat="server" CssClass="TextAreaField"
                    TextMode="MultiLine" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel ID="lblHasDepending" runat="server" EnableViewState="false" ResourceString="fieldeditor.hasdepending"
                    DisplayColon="true" />
            </td>
            <td>
                <asp:CheckBox ID="chkHasDepending" runat="server" CssClass="CheckBoxMovedLeft" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel ID="lblDependsOn" runat="server" EnableViewState="false" ResourceString="fieldeditor.dependson"
                    DisplayColon="true" />
            </td>
            <td>
                <asp:CheckBox ID="chkDependsOn" runat="server" CssClass="CheckBoxMovedLeft" />
            </td>
        </tr>
    </table>
</asp:Panel>
