<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_DocumentTypes_Pages_Development_DocumentType_Edit_Transformation_Edit"
    ValidateRequest="false" Theme="Default" EnableEventValidation="false" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master"
    Title="Transformation Edit" CodeFile="DocumentType_Edit_Transformation_Edit.aspx.cs" %>

<%@ Register Src="~/CMSModules/AdminControls/Controls/Class/TransformationEdit.ascx"
    TagName="TransformationEdit" TagPrefix="cms" %>
<asp:Content ID="Content1" ContentPlaceHolderID="plcContent" runat="Server">
    <asp:Panel ID="pnlContainer" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="ScriptManager1" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <%-- Parameters must be set here in ASPX --%>
        <cms:TransformationEdit ID="transformationEdit" runat="server" ListPage="~/CMSModules/DocumentTypes/Pages/Development/DocumentType_Edit_Transformation_List.aspx"
            ParameterName="documenttypeid" IsLiveSite="false" />
    </asp:Panel>
</asp:Content>
