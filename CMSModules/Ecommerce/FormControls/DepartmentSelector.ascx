<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_Ecommerce_FormControls_DepartmentSelector"
    CodeFile="DepartmentSelector.ascx.cs" %>
<%@ Register Src="~/CMSAdminControls/UI/UniSelector/UniSelector.ascx" TagName="UniSelector"
    TagPrefix="cms" %>
<cms:CMSUpdatePanel ID="pnlUpdate" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <cms:UniSelector ID="uniSelector" runat="server" DisplayNameFormat="{%DepartmentDisplayName%}"
            ObjectType="ecommerce.department" ResourcePrefix="departmentselector" SelectionMode="SingleDropDownList" ReturnColumnName="DepartmentID" />
    </ContentTemplate>
</cms:CMSUpdatePanel>
