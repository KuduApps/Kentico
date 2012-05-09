<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FormControlsSelector.ascx.cs"
    Inherits="CMSModules_ContactManagement_FormControls_FormControlsSelector" %>
<%@ Register Src="~/CMSAdminControls/UI/UniSelector/UniSelector.ascx" TagName="UniSelector"
    TagPrefix="cms" %>
<cms:UniSelector ID="ucType" ShortID="us" ObjectType="cms.formusercontrol" runat="server"
    ReturnColumnName="userControlCodeName" SelectionMode="SingleDropDownList" OrderBy="UserControlDisplayName"
    LocalizeItems="true" AllowEmpty="true" AllowAll="false" IsLiveSite="false" />
