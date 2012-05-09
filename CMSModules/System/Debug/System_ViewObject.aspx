<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_System_Debug_System_ViewObject"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/Dialogs/ModalDialogPage.master"
    MaintainScrollPositionOnPostback="true" CodeFile="System_ViewObject.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/System/ViewObject.ascx" TagName="ViewObject"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/UI/PageElements/HeaderActions.ascx" TagName="HeaderActions"
    TagPrefix="cms" %>
<%@ Register Src="CacheItemsGrid.ascx" TagName="CacheItemsGrid" TagPrefix="cms" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:Panel ID="pnlActions" runat="server" CssClass="PageHeaderLine" EnableViewState="false"
        Visible="false">
        <cms:HeaderActions ID="actionsElem" runat="server" />
    </asp:Panel>
    <asp:Panel ID="pnlCacheItem" runat="server" CssClass="PageHeaderLine" EnableViewState="false"
        Visible="false">
        <table>
            <tr>
                <td>
                    <strong>
                        <cms:LocalizedLiteral runat="server" ID="ltlKeyTitle" ResourceString="Administration-System.CacheInfoKey"
                            EnableViewState="false" DisplayColon="true" /></strong>
                </td>
                <td>
                    <strong>
                        <asp:Literal runat="server" ID="ltlKey" EnableViewState="false" />
                    </strong>
                </td>
            </tr>
            <tr>
                <td>
                    <strong>
                        <cms:LocalizedLiteral runat="server" ID="ltlExpirationTitle" ResourceString="Administration-System.CacheInfoExpiration"
                            EnableViewState="false" DisplayColon="true" /></strong>
                </td>
                <td>
                    <asp:Literal runat="server" ID="ltlExpiration" EnableViewState="false" />
                </td>
            </tr>
            <tr>
                <td>
                    <strong>
                        <cms:LocalizedLiteral runat="server" ID="ltlPriorityTitle" ResourceString="Administration-System.CacheInfoPriority"
                            EnableViewState="false" DisplayColon="true" /></strong>
                </td>
                <td>
                    <asp:Literal runat="server" ID="ltlPriority" EnableViewState="false" />
                </td>
            </tr>
            <asp:PlaceHolder runat="server" ID="plcDependencies">
                <tr>
                    <td style="vertical-align: top">
                        <strong>
                            <cms:LocalizedLiteral runat="server" ID="ltlDependenciesTitle" ResourceString="Administration-System.CacheInfoDependencies"
                                EnableViewState="false" DisplayColon="true" /></strong>
                    </td>
                    <td>
                        <cms:LocalizedLiteral runat="server" ID="ltlDependencies" EnableViewState="false"
                            ResourceString="Administration-System.CacheInfoNoDependencies" />
                        <cms:CacheItemsGrid ID="gridDependencies" ShortID="gd" runat="server" ShowDummyItems="true" />
                    </td>
                </tr>
            </asp:PlaceHolder>
        </table>
    </asp:Panel>
    <asp:Panel runat="server" ID="pnlBody" CssClass="PageContent">
        <asp:Label ID="lblError" runat="server" CssClass="ErrorLabel" EnableViewState="false" />
        <cms:ViewObject ID="objElem" ShortID="o" runat="server" />
    </asp:Panel>
</asp:Content>
<asp:Content ID="cntFooter" ContentPlaceHolderID="plcFooter" runat="server">
    <div class="FloatRight">
        <cms:LocalizedButton ID="btnCancel" runat="server" ResourceString="General.Close"
            CssClass="SubmitButton" OnClientClick="window.close(); return false;" EnableViewState="false" />
    </div>
</asp:Content>
