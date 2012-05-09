<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSFormControls_CountrySelector" CodeFile="CountrySelector.ascx.cs" %>
<%@ Register src="~/CMSAdminControls/UI/UniSelector/UniSelector.ascx" tagname="UniSelector" tagprefix="cms" %>
<cms:CMSUpdatePanel ID="pnlUpdate" runat="server">
    <ContentTemplate>
        <div style="padding-top: 2px; padding-bottom: 2px;">
            <cms:UniSelector ID="uniSelectorCountry" runat="server" DisplayNameFormat="{%CountryDisplayName%}"
                ObjectType="cms.country" ResourcePrefix="countryselector" AllowAll="false" AllowEmpty="false" />
        </div>
        <asp:PlaceHolder runat="server" ID="plcStates">
            <div style="padding-top: 2px; padding-bottom: 2px;">
                <cms:UniSelector ID="uniSelectorState" runat="server" DisplayNameFormat="{%StateDisplayName%}"
                    ObjectType="cms.state" ResourcePrefix="stateselector" />
            </div>
        </asp:PlaceHolder>
    </ContentTemplate>
</cms:CMSUpdatePanel>
