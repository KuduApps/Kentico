<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CMSMasterPages/UI/Dialogs/ModalDialogPage.master"
    Title="Activity - Details" Inherits="CMSModules_ContactManagement_Pages_Tools_Activities_Activity_Activity_Details"
    Theme="Default" CodeFile="Activity_Details.aspx.cs" %>

<%@ Register Src="~/CMSModules/ContactManagement/Controls/UI/Activity/Details.ascx"
    TagName="Details" TagPrefix="cms" %>
<%@ Register Src="~/CMSFormControls/Basic/HtmlAreaControl.ascx" TagName="HtmlAreaControl"
    TagPrefix="cms" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:PlaceHolder ID="plcDetails" runat="server">
        <div class="PageContent">
            <asp:Panel runat="server" ID="pnlGen" CssClass="ActivityPanel">
                <table border="0" cellpadding="0" cellspacing="5" class="tblEventLogDetail">
                    <tr>
                        <td class="ActivityDetailsLabel">
                            <cms:LocalizedLabel ID="lblType" runat="server" ResourceString="om.activity.type"
                                DisplayColon="true" EnableViewState="false" />
                        </td>
                        <td>
                            <asp:Label ID="lblTypeVal" runat="server" CssClass="FieldLabel" EnableViewState="false" />
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="ActivityDetailsLabel">
                            <cms:LocalizedLabel ID="lblTitle" runat="server" ResourceString="om.activity.title"
                                DisplayColon="true" EnableViewState="false" />
                        </td>
                        <td>
                            <cms:CMSTextBox ID="txtTitle" runat="server" ReadOnly="true" CssClass="ActivityCommentBox"
                                MaxLength="250" />
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="ActivityDetailsLabel">
                            <cms:LocalizedLabel ID="lblContact" runat="server" ResourceString="om.activity.contactname"
                                DisplayColon="true" EnableViewState="false" />
                        </td>
                        <td>
                            <asp:Label ID="lblContactVal" runat="server" CssClass="FieldLabel" EnableViewState="false" />
                            <asp:ImageButton runat="server" ID="btnContact" CssClass="UnigridActionButton" EnableViewState="false" style="margin:0 5px" />
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="ActivityDetailsLabel">
                            <cms:LocalizedLabel ID="lblDate" runat="server" ResourceString="om.activity.date"
                                DisplayColon="true" EnableViewState="false" />
                        </td>
                        <td>
                            <asp:Label ID="lblDateVal" runat="server" CssClass="FieldLabel" EnableViewState="false" />
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="ActivityDetailsLabel">
                            <cms:LocalizedLabel ID="lblURL" runat="server" ResourceString="om.activity.url" DisplayColon="true"
                                EnableViewState="false" />
                        </td>
                        <td>
                            <cms:CMSTextBox ID="txtURL" runat="server" ReadOnly="true" CssClass="ActivityCommentBox" />
                        </td>
                        <td>
                            <asp:HyperLink runat="server" ID="btnView" Target="_blank" Visible="false" EnableViewState="false" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ActivityDetailsLabel">
                            <cms:LocalizedLabel ID="lblURLreferrer" runat="server" ResourceString="om.activity.urlreferrer"
                                DisplayColon="true" EnableViewState="false" />
                        </td>
                        <td>
                            <asp:Label ID="lblURLReferrerVal" runat="server" CssClass="FieldLabel" EnableViewState="false" />
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="ActivityDetailsLabel">
                            <cms:LocalizedLabel ID="lblSite" runat="server" ResourceString="om.activity.site"
                                DisplayColon="true" EnableViewState="false" />
                        </td>
                        <td>
                            <asp:Label ID="lblSiteVal" runat="server" CssClass="FieldLabel" EnableViewState="false" />
                        </td>
                        <td>
                        </td>
                    </tr>
                    <asp:PlaceHolder ID="plcActivityValue" runat="server" Visible="false">
                    <tr>
                        <td class="ActivityDetailsLabel">
                            <cms:LocalizedLabel ID="lblActivityValue" runat="server" ResourceString="om.activity.value"
                                DisplayColon="true" EnableViewState="false" />
                        </td>
                        <td>
                            <asp:Label ID="lblValue" runat="server" CssClass="FieldLabel" EnableViewState="false" />
                        </td>
                        <td>
                        </td>
                    </tr>     
                    </asp:PlaceHolder>
                    <asp:PlaceHolder runat="server" ID="plcCampaign" Visible="false">
                        <tr>
                            <td class="ActivityDetailsLabel">
                                <cms:LocalizedLabel ID="lblCampaign" runat="server" ResourceString="om.activity.campaign"
                                    DisplayColon="true" EnableViewState="false" />
                            </td>
                            <td>
                                <asp:Label ID="lblCampaignVal" runat="server" CssClass="FieldLabel" EnableViewState="false" />
                            </td>
                            <td>
                            </td>
                        </tr>
                    </asp:PlaceHolder>
                </table>
            </asp:Panel>
            <cms:Details runat="server" ID="cDetails" CssClass="ActivityPanel" />
            <asp:Panel ID="pnlComment" runat="server">
                <table>
                    <tr>
                        <td class="ActivityDetailsLabel" style="vertical-align: top;">
                            <cms:LocalizedLabel ID="lblComment" runat="server" ResourceString="om.activity.comment"
                                DisplayColon="true" EnableViewState="false" />
                        </td>
                        <td class="ActivityDetailsCommentBox">
                            <cms:CMSHtmlEditor ID="txtComment" runat="server" ToolbarSet="Basic" IsLiveSite="false" />
                            <div class="MiddleButton">
                                <cms:LocalizedButton ID="btnStamp" runat="server" ResourceString="om.account.stamp"
                                    CssClass="ContentButton" EnableViewState="false" /></div>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
    </asp:PlaceHolder>
</asp:Content>
<asp:Content ID="cntFooter" ContentPlaceHolderID="plcFooter" runat="server">
    <div class="FloatLeft">
        <cms:CMSButton runat="server" ID="btnPrevious" CssClass="SubmitButton" Enabled="false"
            EnableViewState="false" /><cms:CMSButton runat="server" ID="btnNext" CssClass="SubmitButton"
                Enabled="false" EnableViewState="false" /></div>
    <div class="FloatRight">
        <cms:LocalizedButton runat="server" ID="btnSave" CssClass="SubmitButton" EnableViewState="false"
            ResourceString="general.save" Visible="false" />
        <cms:LocalizedButton ID="btnClose" runat="server" CssClass="SubmitButton" ResourceString="general.close"
            OnClientClick="window.close();return false;" EnableViewState="false" />
    </div>
</asp:Content>
