<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_AdminControls_Controls_Class_NewClassWizard"
    CodeFile="NewClassWizard.ascx.cs" %>
<%@ Register Src="~/CMSFormControls/System/LocalizableTextBox.ascx" TagName="LocalizableTextBox"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/UI/UniSelector/UniSelector.ascx" TagName="UniSelector"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/AdminControls/Controls/Class/FieldEditor/FieldEditor.ascx"
    TagName="FieldEditor" TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/Wizard/Header.ascx" TagName="WizardHeader" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/SmartSearch/Controls/Edit/SearchFields.ascx" TagName="SearchFields"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSFormControls/Classes/SelectClass.ascx" TagName="SelectClass"
    TagPrefix="cms" %>
<table class="GlobalWizard" border="0" cellpadding="0" cellspacing="0">
    <tr class="Top">
        <td class="Left">
            &nbsp;
        </td>
        <td class="Center">
            <cms:WizardHeader ID="ucHeader" runat="server" />
        </td>
        <td class="Right">
            &nbsp;
        </td>
    </tr>
    <tr class="Middle">
        <td class="Center" colspan="3">
            <div id="wzdBody">
                <asp:Wizard ID="wzdNewDocType" runat="server" DisplaySideBar="false" OnNextButtonClick="wzdNewDocType_NextButtonClick"
                    OnFinishButtonClick="wzdNewDocType_FinishButtonClick" CssClass="Wizard">
                    <StartNavigationTemplate>
                        <div id="buttonsDiv" class="WizardButtons">
                            <cms:LocalizedButton UseSubmitBehavior="True" ID="StepNextButton" runat="server"
                                CommandName="MoveNext" Text="{$general.next$} >" CssClass="SubmitButton" />
                        </div>
                    </StartNavigationTemplate>
                    <StepNavigationTemplate>
                        <div id="buttonsDiv" class="WizardButtons">
                            <cms:LocalizedButton UseSubmitBehavior="True" ID="StepNextButton" runat="server"
                                CommandName="MoveNext" Text="{$general.next$} >" CssClass="SubmitButton" />
                        </div>
                    </StepNavigationTemplate>
                    <FinishNavigationTemplate>
                        <div id="buttonsDiv" class="WizardButtons">
                            <cms:LocalizedButton UseSubmitBehavior="True" ID="StepFinishButton" runat="server"
                                CommandName="MoveComplete" ResourceString="general.finish" CssClass="SubmitButton" />
                        </div>
                    </FinishNavigationTemplate>
                    <WizardSteps>
                        <%-- Step 1 --%>
                        <asp:WizardStep ID="wzdStep1" runat="server" AllowReturn="false">
                            <asp:Panel ID="pnlWzdStep1" runat="server" CssClass="GlobalWizardStep">
                                <asp:Label ID="lblErrorStep1" runat="server" CssClass="ErrorLabel" EnableViewState="false"
                                    Visible="false"></asp:Label>
                                <table>
                                    <tr>
                                        <td class="FieldLabel">
                                            <asp:Label runat="server" ID="lblDisplayName" />
                                        </td>
                                        <td>
                                            <cms:LocalizableTextBox runat="server" ID="txtDisplayName" CssClass="TextBoxField"
                                                MaxLength="100" />
                                            <cms:CMSRequiredFieldValidator ID="rfvDisplayName" runat="server" ControlToValidate="txtDisplayName:textbox"
                                                Display="dynamic" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: text-top; padding-top: 5px" class="FieldLabel">
                                            <asp:Label runat="server" ID="lblFullCodeName" />
                                        </td>
                                        <td>
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <cms:CMSTextBox runat="server" ID="txtNamespaceName" CssClass="TextBoxField" MaxLength="49" />
                                                    </td>
                                                    <td>
                                                        .
                                                    </td>
                                                    <td>
                                                        <cms:CMSTextBox runat="server" ID="txtCodeName" CssClass="TextBoxField" MaxLength="50" />
                                                    </td>
                                                </tr>
                                                <tr style="color: Green">
                                                    <td>
                                                        <asp:Label runat="server" ID="lblNamespaceName" />
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:Label runat="server" ID="lblCodeName" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3">
                                                        <cms:CMSRequiredFieldValidator ID="rfvNamespaceName" runat="server" EnableViewState="false"
                                                            ControlToValidate="txtNamespaceName" Display="dynamic" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3">
                                                        <cms:CMSRegularExpressionValidator ID="revNameSpaceName" runat="server" EnableViewState="false"
                                                            Display="dynamic" ControlToValidate="txtNamespaceName" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3">
                                                        <cms:CMSRequiredFieldValidator ID="rfvCodeName" runat="server" EnableViewState="false"
                                                            ControlToValidate="txtCodeName" Display="dynamic" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3">
                                                        <cms:CMSRegularExpressionValidator ID="revCodeName" runat="server" EnableViewState="false"
                                                            Display="dynamic" ControlToValidate="txtCodeName" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </asp:WizardStep>
                        <%-- Step 2 --%>
                        <asp:WizardStep ID="wzdStep2" runat="server" AllowReturn="false">
                            <asp:Panel ID="pnlWzdStep2" runat="server" CssClass="GlobalWizardStep">
                                <asp:Label ID="lblErrorStep2" runat="server" CssClass="ErrorLabel" EnableViewState="false"
                                    Visible="false"></asp:Label>
                                <table>
                                    <tr>
                                        <td colspan="3">
                                            <asp:RadioButton ID="radCustom" runat="server" Checked="True" GroupName="DocType" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 30px;">
                                        </td>
                                        <td class="FieldLabel">
                                            <asp:Label ID="lblTableName" runat="server" />
                                        </td>
                                        <td>
                                            <cms:CMSTextBox ID="txtTableName" runat="server" CssClass="TextBoxField" MaxLength="100" />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblTableNameError" runat="server" CssClass="ErrorLabel" />
                                            <%--<cms:CMSRequiredFieldValidator ID="rfvTableName" runat="server" ControlToValidate="txtTableName" Display="dynamic"  />--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 30px;">
                                        </td>
                                        <td class="FieldLabel">
                                            <asp:Label ID="lblPKName" runat="server" />
                                        </td>
                                        <td>
                                            <cms:CMSTextBox ID="txtPKName" runat="server" CssClass="TextBoxField" MaxLength="100" />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblPKNameError" runat="server" CssClass="ErrorLabel" />
                                            <%--<cms:CMSRequiredFieldValidator ID="rfvPKName" runat="server" ControlToValidate="txtPKName" Display="dynamic"  />--%>
                                        </td>
                                    </tr>
                                    <asp:PlaceHolder runat="server" ID="plcDocTypeOptions" Visible="false">
                                        <tr>
                                            <td style="width: 30px;">
                                            </td>
                                            <td class="FieldLabel">
                                                <cms:LocalizedLabel runat="server" ID="lblInherits" EnableViewState="false" ResourceString="DocumentType.InheritsFrom" />
                                            </td>
                                            <td>
                                                <cms:SelectClass ID="selInherits" runat="server" DisplayNoneValue="true" />
                                            </td>
                                            <td>
                                                <asp:Label ID="Label2" runat="server" CssClass="ErrorLabel" />
                                                <%--<cms:CMSRequiredFieldValidator ID="rfvPKName" runat="server" ControlToValidate="txtPKName" Display="dynamic"  />--%>
                                            </td>
                                        </tr>
                                    </asp:PlaceHolder>
                                    <asp:PlaceHolder ID="plcMNClassOptions" runat="server" Visible="false">
                                        <tr>
                                            <td style="width: 30px; margin-top: 10px;">
                                            </td>
                                            <td class="FieldLabel" style="padding-top: 10px">
                                                <asp:Label ID="lblIsMNTable" runat="server" />
                                            </td>
                                            <td style="padding-top: 10px">
                                                <asp:CheckBox ID="chbIsMNTable" runat="server" Checked="False" />
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                    </asp:PlaceHolder>
                                    <asp:PlaceHolder ID="plcCustomTablesOptions" runat="server" Visible="false">
                                        <tr>
                                            <td style="width: 30px; margin-top: 10px;">
                                            </td>
                                            <td class="FieldLabel" style="padding-top: 10px">
                                                <asp:Label ID="lblItemCreatedBy" runat="server" />
                                            </td>
                                            <td style="padding-top: 10px">
                                                <asp:CheckBox ID="chkItemCreatedBy" runat="server" Checked="true" />
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                            </td>
                                            <td class="FieldLabel">
                                                <asp:Label ID="lblItemCreatedWhen" runat="server" />
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="chkItemCreatedWhen" runat="server" Checked="true" />
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                            </td>
                                            <td class="FieldLabel">
                                                <asp:Label ID="lblItemModifiedBy" runat="server" />
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="chkItemModifiedBy" runat="server" Checked="true" />
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                            </td>
                                            <td class="FieldLabel">
                                                <asp:Label ID="lblItemModifiedWhen" runat="server" />
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="chkItemModifiedWhen" runat="server" Checked="true" />
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                            </td>
                                            <td class="FieldLabel">
                                                <asp:Label ID="lblItemOrder" runat="server" />
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="chkItemOrder" runat="server" Checked="true" />
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                    </asp:PlaceHolder>
                                    <tr>
                                        <td colspan="3">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <asp:RadioButton ID="radContainer" runat="server" GroupName="DocType" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </asp:WizardStep>
                        <%-- Step 3 --%>
                        <asp:WizardStep ID="wzdStep3" runat="server" AllowReturn="false">
                            <asp:Panel ID="pnlWzdStep3" runat="server" CssClass="GlobalWizardStep FieldEditorPanel"
                                Height="400px">
                                <asp:Label ID="lblErrorStep3" runat="server" CssClass="ErrorLabel" EnableViewState="false"
                                    Visible="false"></asp:Label>
                                <cms:FieldEditor ID="FieldEditor" runat="server" DisplaySourceFieldSelection="false" />
                            </asp:Panel>
                        </asp:WizardStep>
                        <%-- Step 4 --%>
                        <asp:WizardStep ID="wzdStep4" runat="server" AllowReturn="false">
                            <asp:Panel ID="pnlWzdStep4" runat="server" CssClass="GlobalWizardStep">
                                <table>
                                    <tr>
                                        <td class="FieldLabel">
                                            <asp:Label ID="lblSelectField" runat="server" />&nbsp;
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="lstFields" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </asp:WizardStep>
                        <%-- Step 5 --%>
                        <asp:WizardStep ID="wzdStep5" runat="server" AllowReturn="false">
                            <asp:Panel ID="pnlWzdStep5" runat="server" CssClass="GlobalWizardStep">
                                <cms:UniSelector ID="usParentTypes" runat="server" IsLiveSite="false" ObjectType="cms.documenttype"
                                    SelectionMode="Multiple" ResourcePrefix="allowedclasscontrol" DisplayNameFormat="{%ClassDisplayName%} ({%ClassName%})" />
                            </asp:Panel>
                        </asp:WizardStep>
                        <%-- Step 6 --%>
                        <asp:WizardStep ID="wzdStep6" runat="server" AllowReturn="false">
                            <asp:Panel ID="pnlWzdStep6" runat="server" CssClass="GlobalWizardStep">
                                <asp:Label ID="lblErrorStep6" runat="server" CssClass="ErrorLabel" EnableViewState="false"
                                    Visible="false" />
                                <cms:UniSelector ID="usSites" runat="server" IsLiveSite="false" ObjectType="cms.site"
                                    SelectionMode="Multiple" ResourcePrefix="sitesselect" />
                            </asp:Panel>
                        </asp:WizardStep>
                        <%-- Step 7 --%>
                        <asp:WizardStep ID="wzdStep7" runat="server" AllowReturn="false">
                            <asp:Panel ID="pnlWzdStep7" runat="server" CssClass="GlobalWizardStep">
                                <cms:SearchFields ID="SearchFields" runat="server" RebuildIndexResourceString="" />
                            </asp:Panel>
                        </asp:WizardStep>
                        <%-- Step 8 --%>
                        <asp:WizardStep ID="wzdStep8" runat="server" AllowReturn="false">
                            <asp:Panel ID="pnlWzdStep8" runat="server" CssClass="GlobalWizardStep">
                                <asp:Label runat="server" ID="lblInfoStep8" />
                                <br />
                                <br />
                                <asp:Label runat="server" ID="lblDocumentCreated" CssClass="WizardFinishedStep" />
                                <asp:Label runat="server" ID="lblTableCreated" CssClass="WizardFinishedStep" />
                                <asp:Label runat="server" ID="lblEditingFormCreated" CssClass="WizardFinishedStep" />
                                <asp:Label runat="server" ID="lblChildTypesAdded" CssClass="WizardFinishedStep" />
                                <asp:Label runat="server" ID="lblSitesSelected" CssClass="WizardFinishedStep" />
                                <asp:Label runat="server" ID="lblQueryCreated" CssClass="WizardFinishedStep" />
                                <asp:Label runat="server" ID="lblTransformationCreated" CssClass="WizardFinishedStep" />
                                <asp:Label runat="server" ID="lblPermissionNameCreated" CssClass="WizardFinishedStep" />
                                <asp:Label runat="server" ID="lblDefaultIconCreated" CssClass="WizardFinishedStep" />
                                <asp:Label runat="server" ID="lblSearchSpecificationCreated" CssClass="WizardFinishedStep" />
                            </asp:Panel>
                        </asp:WizardStep>
                    </WizardSteps>
                </asp:Wizard>
            </div>
        </td>
    </tr>
    <tr class="Bottom">
        <td class="Left">
            &nbsp;
        </td>
        <td class="Center">
            &nbsp;
        </td>
        <td class="Right">
            &nbsp;
        </td>
    </tr>
</table>
