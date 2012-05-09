<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSFormControls_Cultures_UICultureSelector" CodeFile="UICultureSelector.ascx.cs" %>
<%@ Register Src="~/CMSAdminControls/UI/UniSelector/UniSelector.ascx" TagName="UniSelector" TagPrefix="cms" %>
<cms:UniSelector ID="uniSelector" ShortID="sc" runat="server" LocalizeItems="true" ObjectType="CMS.UICulture" ReturnColumnName="UICultureCode"
    OrderBy="UICultureName ASC"  />
