<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false"
    Inherits="CMSModules_PortalEngine_UI_Layout_PageTemplateSelector" MasterPageFile="~/CMSMasterPages/UI/Dialogs/ModalSimplePage.master"
    Theme="Default" Title="Select page template" CodeFile="PageTemplateSelector.aspx.cs" %>

<%@ Register Src="~/CMSModules/PortalEngine/Controls/Layout/PageTemplateSelector.ascx"
    TagName="PageTemplateSelector" TagPrefix="cms" %>
<asp:Content runat="server" ContentPlaceHolderID="plcContent" ID="content">
    <asp:Panel runat="server" ID="pnlSelector">
        <cms:PageTemplateSelector runat="server" ID="selectElem" ShowEmptyCategories="false" IsLiveSite="false" />
        <table id="__ButtonsArea" cellpadding="0" cellspacing="0" class="ButtonsArea">
            <tr>
                <td>
                    <cms:LocalizedButton runat="server" ID="btnOk" ResourceString="general.ok" CssClass="SubmitButton"
                        EnableViewState="false" OnClientClick="SelectCurrentPageTemplate();return false;">
                    </cms:LocalizedButton>
                    <cms:LocalizedButton runat="server" ID="btnCancel" ResourceString="general.cancel"
                        EnableViewState="false" CssClass="SubmitButton" OnClientClick="Cancel()"></cms:LocalizedButton>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
