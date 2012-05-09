<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_Integration_Controls_UI_Connectors_Edit"
    CodeFile="Edit.ascx.cs" %>
<cms:UIForm runat="server" ID="EditForm" ObjectType="integration.connector" RedirectUrlAfterCreate="Edit.aspx?connectorid={%EditedObject.ID%}&saved=1"
    CheckFieldEmptiness="true">
    <LayoutTemplate>
        <cms:FormField runat="server" ID="fConnectorDisplayName" Field="ConnectorDisplayName" FormControl="LocalizableTextBox"
            ResourceString="general.displayname" DisplayColon="true" ShowRequiredMark="true" />
        <cms:FormField runat="server" ID="fConnectorName" Field="ConnectorName" FormControl="TextBoxControl"
            ResourceString="general.codename" DisplayColon="true" ShowRequiredMark="true" CheckUnique="true" />
        <cms:FormField runat="server" ID="fConnectorAssemblyName" Field="ConnectorAssemblyName" FormControl="TextBoxControl"
            ResourceString="integration.assemblyname" DisplayColon="true" ShowRequiredMark="true" />
        <cms:FormField runat="server" ID="fConnectorClassName" Field="ConnectorClassName" FormControl="TextBoxControl"
            ResourceString="integration.classname" DisplayColon="true" ShowRequiredMark="true" />
        <cms:FormField runat="server" ID="fConnectorEnabled" Field="ConnectorEnabled" FormControl="CheckBoxControl"
            ResourceString="general.enabled" DisplayColon="true" Value="true" />
        <cms:FormSubmit runat="server" ID="fSubmit" />
    </LayoutTemplate>
    <SecurityCheck Resource="CMS.Integration" Permission="modify" />
</cms:UIForm>
