<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSSiteManager_Sites_CultureChange"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/Dialogs/ModalDialogPage.master"
    Title="Site - Change culture" CodeFile="CultureChange.aspx.cs" %>

<%@ Register Src="~/CMSFormControls/Cultures/SiteCultureSelector.ascx" TagName="SiteCultureSelector"
    TagPrefix="cms" %>
<asp:Content ID="cntContent" ContentPlaceHolderID="plcContent" runat="Server">
    <asp:Panel runat="server" ID="pnlContent" CssClass="PageContent">
        <asp:Label ID="lblError" runat="server" CssClass="ErrorLabel" EnableViewState="false"
            Visible="false" />
        <asp:Panel ID="pnlForm" runat="server">
            <table style="margin-left: auto; margin-right: auto;">
                <tr>
                    <td>
                        <asp:Label ID="lblNewCulture" runat="server" CssClass="ContentLabel" EnableViewState="false" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <cms:SiteCultureSelector runat="server" ID="cultureSelector" IsLiveSite="false" AddDefaultRecord="false" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:CheckBox ID="chkDocuments" runat="server" CssClass="ContentCheckBox" Checked="true" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </asp:Panel>
    <asp:Literal runat="server" ID="ltlScript" EnableViewState="false" />
</asp:Content>
<asp:Content ID="cntFooter" ContentPlaceHolderID="plcFooter" runat="server">
    <div class="FloatRight">
        <cms:LocalizedButton ID="btnOk" runat="server" CssClass="SubmitButton" EnableViewState="False"
            OnClick="btnOk_Click" /><cms:LocalizedButton ID="btnCancel" runat="server" CssClass="SubmitButton"
                OnClientClick="window.close(); return false;" EnableViewState="False" />
    </div>
</asp:Content>
