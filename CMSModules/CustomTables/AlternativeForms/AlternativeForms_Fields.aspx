<%@ Page Language="C#" AutoEventWireup="true"
    MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="ALternative forms - fields"
    Inherits="CMSModules_CustomTables_AlternativeForms_AlternativeForms_Fields"
    Theme="Default" CodeFile="AlternativeForms_Fields.aspx.cs" %>
    
<%@ Register Src="~/CMSModules/AdminControls/Controls/Class/AlternativeFormFieldEditor.ascx" TagName="AlternativeFormFieldEditor" TagPrefix="cms" %>    

<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <cms:AlternativeFormFieldEditor ID="altFormFieldEditor" runat="server" /> 
</asp:Content>
