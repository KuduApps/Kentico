<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false"
    Inherits="CMSModules_PortalEngine_UI_WebParts_WebPartSelector" MasterPageFile="~/CMSMasterPages/UI/Dialogs/ModalSimplePage.master"
    Theme="Default" Title="Select web part" CodeFile="WebPartSelector.aspx.cs" %>

<%@ Register Src="~/CMSModules/PortalEngine/Controls/WebParts/WebPartSelector.ascx"
    TagName="WebPartSelector" TagPrefix="cms" %>
<asp:Content runat="server" ContentPlaceHolderID="plcContent" ID="content">
    <asp:Panel runat="server" ID="pnlSelector">
        <cms:WebPartSelector runat="server" ID="selectElem" IsLiveSite="false" />
        <table cellpadding="0" cellspacing="0" id="__ButtonsArea" class="ButtonsArea">
            <tr>
                <td>
                    <cms:LocalizedButton runat="server" ID="btnOk" ResourceString="general.ok" CssClass="SubmitButton"
                        EnableViewState="false" OnClientClick="SelectCurrentWebPart();return false;"></cms:LocalizedButton>
                
                    <cms:LocalizedButton runat="server" ID="btnCancel" ResourceString="general.cancel"
                        EnableViewState="false" CssClass="SubmitButton" OnClientClick="Cancel()"></cms:LocalizedButton>
                </td>
            </tr>
        </table>
        <cms:LocalizedHidden ID="hdnMessage" runat="server" Value="{$PortalEngine-WebPartSelection.NoWebPartSelected$}"
            EnableViewState="false" />
    </asp:Panel>
</asp:Content>
