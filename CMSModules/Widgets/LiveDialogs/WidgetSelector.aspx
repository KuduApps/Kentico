<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" Inherits="CMSModules_Widgets_LiveDialogs_WidgetSelector"
    MasterPageFile="~/CMSMasterPages/LiveSite/Dialogs/ModalSimplePage.master" Theme="Default" Title="Add widget" CodeFile="WidgetSelector.aspx.cs" %>

<%@ Register Src="~/CMSModules/Widgets/Controls/WidgetSelector.ascx" TagName="WidgetSelector"
    TagPrefix="cms" %>
<asp:Content ID="content" runat="server" ContentPlaceHolderID="plcContent">
    <asp:Panel runat="server" ID="pnlSelector">
        <cms:WidgetSelector runat="server" ID="selectElem" IsLiveSite="true" />
        <table cellpadding="0" cellspacing="0" id="__ButtonsArea" class="ButtonsArea">
            <tr>
                <td>
                    <cms:LocalizedButton runat="server" ID="btnOk" ResourceString="general.ok" CssClass="SubmitButton"
                        EnableViewState="false" OnClientClick="SelectCurrentWidget(); return false;" />                    
                    <cms:LocalizedButton runat="server" ID="btnCancel" ResourceString="general.cancel"
                        EnableViewState="false" CssClass="SubmitButton" OnClientClick="Cancel(); return false;" />                    
                </td>
            </tr>
        </table>
        <cms:LocalizedHidden ID="hdnMessage" runat="server" Value="{$widgets.NoWidgetSelected$}" EnableViewState="false"  />
    </asp:Panel>
</asp:Content>
