<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSFormControls_Cultures_SiteCultureSelectorAll" CodeFile="SiteCultureSelectorAll.ascx.cs" %>
<%@ Register Src="~/CMSFormControls/Cultures/SiteCultureSelector.ascx" TagName="SiteCultureSelector"
    TagPrefix="cms" %>
<cms:SiteCultureSelector ID="SiteCultureSelector" runat="server" AddDefaultRecord="false"
    AddAllRecord="true" />
