<%@ Page Title="" Language="C#" MasterPageFile="~/CMSAPIExamples/Pages/APIExamplesPage.Master"
    Theme="Default" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="CMSAPIExamples_Code_Development_Modules_Default" %>

<%@ Register Src="~/CMSAPIExamples/Controls/APIExample.ascx" TagName="APIExample" TagPrefix="cms" %>
<%@ Register Src="~/CMSAPIExamples/Controls/APIExampleSection.ascx" TagName="APIExampleSection" TagPrefix="cms" %>
<%@ Register Assembly="CMS.UIControls" Namespace="CMS.UIControls" TagPrefix="cms" %>
<asp:Content ID="contentLeft" ContentPlaceHolderID="plcLeftContainer" runat="server">
    <%-- Section: Modules --%>
    <cms:APIExampleSection ID="secManModules" runat="server" Title="Modules" />        
    <%-- Module --%>
    <cms:APIExamplePanel ID="pnlCreateModule" runat="server" GroupingText="Module">
        <cms:APIExample ID="apiCreateModule" runat="server" ButtonText="Create module" InfoMessage="Module 'My new module' was created." />
        <cms:APIExample ID="apiGetAndUpdateModule" runat="server" ButtonText="Get and update module" APIExampleType="ManageAdditional" InfoMessage="Module 'My new module' was updated." ErrorMessage="Module 'My new module' was not found." />
        <cms:APIExample ID="apiGetAndBulkUpdateModules" runat="server" ButtonText="Get and bulk update modules" APIExampleType="ManageAdditional" InfoMessage="All modules matching the condition were updated." ErrorMessage="Modules matching the condition were not found." />
    </cms:APIExamplePanel>    
    <%-- Module on site --%>
    <cms:APIExamplePanel ID="pnlAddModuleToSite" runat="server" GroupingText="Module on site">
        <cms:APIExample ID="apiAddModuleToSite" runat="server" ButtonText="Add module to site" InfoMessage="Module 'My new module' was added to the current site." ErrorMessage="Module 'My new module' was not found." />
        <cms:APIExample ID="apiGetAndBulkUpdateSiteModules" runat="server" ButtonText="Get and bulk update site modules" APIExampleType="ManageAdditional" InfoMessage="All modules from the current site matching the condition were updated." ErrorMessage="Modules from the current site matching the condition were not found." />
    </cms:APIExamplePanel>
    
    <%-- Section: Permissions --%>
    <cms:APIExampleSection ID="secManPermissions" runat="server" Title="Permissions" />
    <%-- Permission --%>
    <cms:APIExamplePanel ID="pnlCreatePermission" runat="server" GroupingText="Permission">
        <cms:APIExample ID="apiCreatePermission" runat="server" ButtonText="Create permission" InfoMessage="Permission 'My new permission' was created." ErrorMessage="Module 'My new module' was not found." />
        <cms:APIExample ID="apiGetAndUpdatePermission" runat="server" ButtonText="Get and update permission" APIExampleType="ManageAdditional" InfoMessage="Permission 'My new permission' was updated." ErrorMessage="Permission 'My new permission' was not found." />
        <cms:APIExample ID="apiGetAndBulkUpdatePermissions" runat="server" ButtonText="Get and bulk update permissions" APIExampleType="ManageAdditional" InfoMessage="All permissions matching the condition were updated." ErrorMessage="Permissions matching the condition were not found." />
    </cms:APIExamplePanel>    
    <%-- Role permission --%>
    <cms:APIExamplePanel ID="pnlAddPermissionToRole" runat="server" GroupingText="Role permission">
        <cms:APIExample ID="apiAddPermissionToRole" runat="server" ButtonText="Add permission to role" InfoMessage="Permission 'My new permission' was added to role 'CMS Desk administrators'." ErrorMessage="Permission 'My new permission' or role 'CMS Desk administrators' were not found." />        
    </cms:APIExamplePanel>
    
    <%-- Section: UI elements --%>
    <cms:APIExampleSection ID="secManUIelements" runat="server" Title="UI elements" />    
    <%-- UI element --%>
    <cms:APIExamplePanel ID="pnlCreateUIElement" runat="server" GroupingText="UI element">
        <cms:APIExample ID="apiCreateUIElement" runat="server" ButtonText="Create element" InfoMessage="Element 'My new element' was created." ErrorMessage="Module 'My new module' was not found." />
        <cms:APIExample ID="apiGetAndUpdateUIElement" runat="server" ButtonText="Get and update element" APIExampleType="ManageAdditional" InfoMessage="Element 'My new element' was updated." ErrorMessage="Element 'My new element' was not found." />
        <cms:APIExample ID="apiGetAndBulkUpdateUIElements" runat="server" ButtonText="Get and bulk update elements" APIExampleType="ManageAdditional" InfoMessage="All elements matching the condition were updated." ErrorMessage="Elements matching the condition were not found." />
    </cms:APIExamplePanel>
    <%-- Role UI element --%>
    <cms:APIExamplePanel ID="pnlAddUIElementToRole" runat="server" GroupingText="Role UI element">
        <cms:APIExample ID="apiAddUIElementToRole" runat="server" ButtonText="Add element to role" InfoMessage="Element 'My new element' was added to role 'CMS Desk administrators'." ErrorMessage="Element 'My new element' or role 'CMS Desk administrators' were not found." />        
    </cms:APIExamplePanel>
</asp:Content>
<asp:Content ID="contentRight" ContentPlaceHolderID="plcRightContainer" runat="server">
    <%-- Section: UI elements --%>
    <cms:APIExampleSection ID="secCleanUIElements" runat="server" Title="UI elements" /> 
    <%-- Role UI element --%>
    <cms:APIExamplePanel ID="pnlRemoveUIElementFromRole" runat="server" GroupingText="Role UI element">
        <cms:APIExample ID="apiRemoveUIElementFromRole" runat="server" ButtonText="Remove element from role" APIExampleType="CleanUpMain" InfoMessage="Element 'My new element' was removed from role 'CMS Desk administrators'." ErrorMessage="Element 'My new element', role 'CMS Desk administrators' or their relationship were not found." />
    </cms:APIExamplePanel>    
    <%-- UI element --%>
    <cms:APIExamplePanel ID="pnlDeleteUIElement" runat="server" GroupingText="UI element">
        <cms:APIExample ID="apiDeleteUIElement" runat="server" ButtonText="Delete element" APIExampleType="CleanUpMain" InfoMessage="Element 'My new element' and all its dependencies were deleted." ErrorMessage="Element 'My new element' was not found." />
    </cms:APIExamplePanel>
    
    <%-- Section: Permissions --%>
    <cms:APIExampleSection ID="secCleanPermissions" runat="server" Title="Permissions" />     
     <%-- Role permission --%>
    <cms:APIExamplePanel ID="pnlRemovePermissionFromRole" runat="server" GroupingText="Role permission">
        <cms:APIExample ID="apiRemovePermissionFromRole" runat="server" ButtonText="Remove permission from role" APIExampleType="CleanUpMain" InfoMessage="Permission 'My new permission' was removed from role 'CMS Desk administrators'." ErrorMessage="Permission 'My new permission', role 'CMS Desk administrators' or their relationship were not found." />
    </cms:APIExamplePanel>    
    <%-- Permission --%>
    <cms:APIExamplePanel ID="pnlDeletePermission" runat="server" GroupingText="Permission">
        <cms:APIExample ID="apiDeletePermission" runat="server" ButtonText="Delete permission" APIExampleType="CleanUpMain" InfoMessage="Permission 'My new permission' and all its dependencies were deleted." ErrorMessage="Permission 'My new permission' was not found." />
    </cms:APIExamplePanel>
    
    <%-- Section: Modules --%>
    <cms:APIExampleSection ID="secCleanModules" runat="server" Title="Modules" />     
    <%-- Module on site --%>
    <cms:APIExamplePanel ID="pnlRemoveModuleFromSite" runat="server" GroupingText="Module on site">
        <cms:APIExample ID="apiRemoveModuleFromSite" runat="server" ButtonText="Remove module from site" APIExampleType="CleanUpMain" InfoMessage="Module 'My new module' was removed from the current site." ErrorMessage="Module 'My new module' was not found." />
    </cms:APIExamplePanel>    
    <%-- Module --%>
    <cms:APIExamplePanel ID="pnlDeleteModule" runat="server" GroupingText="Module">
        <cms:APIExample ID="apiDeleteModule" runat="server" ButtonText="Delete module" APIExampleType="CleanUpMain" InfoMessage="Module 'My new module' and all its dependencies were deleted." ErrorMessage="Module 'My new module' was not found." />
    </cms:APIExamplePanel>
</asp:Content>
