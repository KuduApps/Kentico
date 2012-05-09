<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSFormControls_Cultures_SelectCulture" CodeFile="SelectCulture.ascx.cs" %>
<%@ Register src="~/CMSAdminControls/UI/UniSelector/UniSelector.ascx" tagname="UniSelector" tagprefix="cms" %>
<cms:CMSUpdatePanel ID="pnlUpdate" runat="server">
    <ContentTemplate>
        <cms:UniSelector ID="uniSelector" ShortID="sc" runat="server" ResourcePrefix="cultureselect" ObjectType="cms.culture"
            OrderBy="CultureName" AllowEditTextBox="true" SelectionMode="SingleTextBox" />
    </ContentTemplate>
</cms:CMSUpdatePanel>
