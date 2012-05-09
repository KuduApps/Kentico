<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OptionsSelector.ascx.cs"
    Inherits="CMSFormControls_System_OptionsSelector" %>
<cms:LocalizedRadioButton ID="radOptions" runat="server" Checked="True" GroupName="optionsquery"
    ResourceString="templatedesigner.dropdownlistoptions" CssClass="FieldBlockLabel" EnableViewState="false" />
<cms:LocalizedRadioButton ID="radSQL" runat="server" GroupName="optionsquery" ResourceString="templatedesigner.dropdownlistsql"
    CssClass="FieldBlockLabel" EnableViewState="false" />
<cms:CMSTextBox ID="txtValue" runat="server" CssClass="TextAreaField" TextMode="MultiLine"></cms:CMSTextBox >
