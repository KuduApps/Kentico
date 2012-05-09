<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSFormControls_Cultures_SiteCultureSelector" CodeFile="SiteCultureSelector.ascx.cs" %>
<%@ Register src="~/CMSAdminControls/UI/UniSelector/UniSelector.ascx" tagname="UniSelector" tagprefix="cms" %>
<cms:CMSUpdatePanel ID="pnlUpdate" runat="server">
    <ContentTemplate>
        <cms:UniSelector ID="uniSelector" ShortID="sc" runat="server" DisplayNameFormat="{%CultureName%}"
            OrderBy="CultureName" ObjectType="cms.culture" ResourcePrefix="cultureselect"
            AllowEmpty="false" AllowAll="false" LocalizeItems="true" SelectionMode="SingleDropDownList" />
    </ContentTemplate>
</cms:CMSUpdatePanel>
