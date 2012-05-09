<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Tab_Conversions.aspx.cs"
    MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Theme="Default" Inherits="CMSModules_WebAnalytics_Pages_Tools_Campaign_Tab_Conversions" %>

<%@ Register Src="~/CMSAdminControls/UI/UniSelector/UniSelector.ascx" TagName="UniSelector"
    TagPrefix="cms" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
        Visible="false" />
    <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" Visible="false" EnableViewState="false" />
    <table>
        <tr>
            <td>
                <cms:LocalizedRadioButton ID="rbAllConversions" runat="server" ResourceString="campaign.allconversions"
                    GroupName="grpConversions" CssClass="RadioButtonMovedLeft" AutoPostBack="true" />
            </td>
        </tr>
        <tr>
            <td>
                <cms:LocalizedRadioButton ID="rbSelectedConversions" runat="server" ResourceString="campaign.selectedconversions"
                    GroupName="grpConversions" CssClass="RadioButtonMovedLeft" AutoPostBack="true" />
            </td>
        </tr>
    </table>
    <br />
    <asp:PlaceHolder ID="plcTable" runat="server">
        <asp:Label ID="lblAvialable" runat="server" CssClass="BoldInfoLabel" EnableViewState="false" />
        <cms:UniSelector ID="usConversions" runat="server" IsLiveSite="false" ObjectType="analytics.conversion"
            SelectionMode="Multiple" ResourcePrefix="conversionselect" />
    </asp:PlaceHolder>
</asp:Content>
