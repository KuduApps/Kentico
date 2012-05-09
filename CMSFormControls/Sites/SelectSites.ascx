<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSFormControls_Sites_SelectSites" CodeFile="SelectSites.ascx.cs" %>

<%@ Register Src="~/CMSFormControls/Sites/selectsite.ascx" TagName="SelectSite" TagPrefix="cms" %>

<cms:SelectSite ID="selectSite" ShortID="ss" runat="server" AllowMultipleSelection="true" />
