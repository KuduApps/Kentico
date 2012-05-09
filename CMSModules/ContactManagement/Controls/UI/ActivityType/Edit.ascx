<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_ContactManagement_Controls_UI_ActivityType_Edit"
    CodeFile="Edit.ascx.cs" %>
<%@ Register Src="~/CMSFormControls/System/LocalizableTextBox.ascx" TagName="LocalizableTextBox"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/ContactManagement/FormControls/FormControlsSelector.ascx" TagName="FormControlsSelector" TagPrefix="cms" %>
<cms:UIForm runat="server" ID="EditForm" ObjectType="om.activitytype" RedirectUrlAfterCreate="Tab_General.aspx?typeid={%EditedObject.ID%}&saved=1"
    IsLiveSite="false">
    <LayoutTemplate>
        <cms:FormField runat="server" ID="fDispName" Field="ActivityTypeDisplayName" FormControl="LocalizableTextBox"
            ResourceString="general.displayname" DisplayColon="true" />
        <cms:FormField runat="server" ID="fName" Field="ActivityTypeName" FormControl="TextBoxControl"
            ResourceString="general.codename" DisplayColon="true" />
        <cms:FormField runat="server" Layout="Inline" ID="fDesc" Field="ActivityTypeDescription">
            <tr>
                <td>
                    <cms:FormLabel runat="server" ID="lDescription" ResourceString="general.description"
                        DisplayColon="true" />
                </td>
                <td>
                    <cms:LocalizableTextBox ID="tDescription" runat="server" TextMode="MultiLine" CssClass="TextAreaField" />
                </td>
                <td>
                    <cms:FormErrorLabel runat="server" ID="eDescription" />
                </td>
            </tr>
        </cms:FormField>
        <cms:FormField runat="server" ID="fIsCustom" Field="ActivityTypeIsCustom" FormControl="CheckBoxControl"
            ResourceString="general.iscustom" DisplayColon="true" Visible="false" Value="true" />
        <cms:FormField runat="server" ID="fManualCreationAllowed" Field="ActivityTypeManualCreationAllowed"
            FormControl="CheckBoxControl" ResourceString="om.activitytype.manualcreationallowed"
            DisplayColon="true" Value="true" />
        <cms:FormField runat="server" ID="fEnable" Field="ActivityTypeEnabled" FormControl="CheckBoxControl"
            ResourceString="general.enabled" DisplayColon="true" Value="true" />
            
        <tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr>
        <tr>
            <td><cms:LocalizedLabel runat="server" ID="lblScoring" ResourceString="om.activitytype.scoringcontrols" CssClass="Title" DisplayColon="true" EnableViewState="false" /></td>
            <td></td>
            <td></td>
        </tr>
        <cms:FormField runat="server" ID="fMainFormControl" Field="ActivityTypeMainFormControl" >
            <tr>
                <td>
                    <cms:FormLabel runat="server" ID="lblMainControl" ResourceString="om.activitytype.mainformcontrol"
                        DisplayColon="true" />
                </td>
                <td>
                    <cms:FormControlsSelector runat="server" ID="ucMainControl" ShowControlsForInteger="true" ShowAdditionalControls="selectdocument" />
                </td>
                <td>
                </td>
            </tr>        
        </cms:FormField>
        <cms:FormField runat="server" ID="fDetailFormControl" Field="ActivityTypeDetailFormControl">
            <tr>
                <td>
                    <cms:FormLabel runat="server" ID="lblDetailControl" ResourceString="om.activitytype.detailformcontrol"
                        DisplayColon="true" />
                </td>
                <td>
                    <cms:FormControlsSelector runat="server" ID="ucDetailControl" ShowControlsForInteger="true" />
                </td>
                <td>
                </td>
            </tr>        
        </cms:FormField>
    </LayoutTemplate>
    <SecurityCheck Resource="CMS.ContactManagement" Permission="ManageActivities" />
</cms:UIForm>
