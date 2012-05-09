<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Delete.aspx.cs" Inherits="CMSModules_ContactManagement_Pages_Tools_Activities_Activity_Delete"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Contact - Delete" %>

<%@ Register Src="~/CMSAdminControls/UI/PageElements/PageTitle.ascx" TagName="PageTitle"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/AsyncControl.ascx" TagName="AsyncControl" TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/AsyncBackground.ascx" TagName="AsyncBackground"
    TagPrefix="cms" %>
<asp:Content ContentPlaceHolderID="plcBeforeBody" runat="server" ID="cntBeforeBody">
    <asp:Panel runat="server" ID="pnlLog" Visible="false">
        <cms:AsyncBackground ID="backgroundElem" runat="server" />
        <div class="AsyncLogArea">
            <div>
                <asp:Panel ID="pnlAsyncBody" runat="server" CssClass="PageBody">
                    <asp:Panel ID="pnlTitleAsync" runat="server" CssClass="PageHeader">
                        <cms:PageTitle ID="titleElemAsync" runat="server" SetWindowTitle="false" />
                    </asp:Panel>
                    <asp:Panel ID="pnlCancel" runat="server" CssClass="PageHeaderLine">
                        <cms:LocalizedButton runat="server" ID="btnCancel" CssClass="SubmitButton" EnableViewState="false"
                            ResourceString="general.cancel" />
                    </asp:Panel>
                    <asp:Panel ID="pnlAsyncContent" runat="server" CssClass="PageContent">
                        <cms:AsyncControl ID="ctlAsync" runat="server" MaxLogLines="1000" />
                    </asp:Panel>
                </asp:Panel>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
<asp:Content ID="plcContent" ContentPlaceHolderID="plcBeforeContent" runat="server"
    EnableViewState="false">
    <asp:Panel runat="server" ID="pnlContent" CssClass="PageContent" EnableViewState="false">
        <asp:Label ID="lblError" runat="server" CssClass="ErrorLabel" EnableViewState="false"
            Visible="false" />
        <asp:Panel ID="pnlDelete" runat="server" EnableViewState="false">
            <cms:LocalizedLabel ID="lblQuestion" runat="server" CssClass="ContentLabel" EnableViewState="false"
                Font-Bold="true" ResourceString="om.activity.deletequestion" />
            <br />
            <br />
            <br />
            <cms:LocalizedButton ID="btnOk" runat="server" CssClass="SubmitButton" OnClick="btnOK_Click"
                ResourceString="general.yes" EnableViewState="false" />
            <cms:LocalizedButton ID="btnNo" runat="server" CssClass="SubmitButton" ResourceString="general.no"
                EnableViewState="false" />
        </asp:Panel>
    </asp:Panel>
    <asp:Literal ID="ltlScript" runat="server" EnableViewState="false" />
</asp:Content>
