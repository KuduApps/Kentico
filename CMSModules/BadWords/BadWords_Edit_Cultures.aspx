<%@ Page Language="C#" AutoEventWireup="true"
    MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Inherits="CMSModules_BadWords_BadWords_Edit_Cultures"
    Theme="Default" CodeFile="BadWords_Edit_Cultures.aspx.cs" %>
<%@ Register src="~/CMSAdminControls/UI/UniSelector/UniSelector.ascx" tagname="UniSelector" tagprefix="cms" %>


<asp:Content ContentPlaceHolderID="plcContent" ID="content" runat="server">
    <cms:CMSUpdatePanel ID="pnlUpdate" runat="server">
        <ContentTemplate>
            <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
                Visible="false" />
            <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
                Visible="false" />
            <table>
                <tr>
                    <td>
                        <cms:LocalizedRadioButton ID="radAll" runat="server" ResourceString="badwords.culturesall"
                            GroupName="grpCultures" AutoPostBack="true" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <cms:LocalizedRadioButton ID="radSelected" runat="server" ResourceString="badwords.culturesselected"
                            GroupName="grpCultures" AutoPostBack="true" />
                    </td>
                </tr>
            </table>
            <br />
            <cms:UniSelector ID="usWordCultures" runat="server" IsLiveSite="false" ObjectType="cms.culture"
                SelectionMode="Multiple" ResourcePrefix="cultureselect" OrderBy="CultureName" />
        </ContentTemplate>
    </cms:CMSUpdatePanel>
</asp:Content>
