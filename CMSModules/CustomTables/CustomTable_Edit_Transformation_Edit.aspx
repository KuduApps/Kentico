<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_CustomTables_CustomTable_Edit_Transformation_Edit" ValidateRequest="false"
    Theme="Default" EnableEventValidation="false" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master"
    Title="Transformation Edit" CodeFile="CustomTable_Edit_Transformation_Edit.aspx.cs" %>
<%@ Register Src="~/CMSModules/AdminControls/Controls/Class/TransformationEdit.ascx" TagName="TransformationEdit"
    TagPrefix="cms" %>
    
<asp:Content ID="Content1" ContentPlaceHolderID="plcContent" runat="Server">
    <%-- Parameters must be set here in ASPX --%>
    <cms:TransformationEdit ID="transformationEdit" runat="server" EditingPage="CustomTable_Edit_Transformation_Edit.aspx"
        ListPage="~/CMSModules/CustomTables/CustomTable_Edit_Transformation_List.aspx"
        ParameterName="customtableid" />
</asp:Content>
