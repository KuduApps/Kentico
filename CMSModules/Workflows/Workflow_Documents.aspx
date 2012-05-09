<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master"
    Inherits="CMSModules_Workflows_Workflow_Documents" Title="Workflow - Documents"
    ValidateRequest="false" Theme="Default" CodeFile="Workflow_Documents.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/UI/PageElements/PageTitle.ascx" TagName="PageTitle"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSFormControls/Filters/DocumentFilter.ascx" TagName="DocumentFilter"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/AsyncControl.ascx" TagName="AsyncControl" TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/AsyncBackground.ascx" TagName="AsyncBackground"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/AdminControls/Controls/Documents/Documents.ascx" TagName="Documents"
    TagPrefix="cms" %>
<asp:Content ContentPlaceHolderID="plcContent" ID="content" runat="server">
    <asp:Panel runat="server" ID="pnlLog" Visible="false">
        <cms:AsyncBackground ID="backgroundElem" runat="server" />
        <div class="AsyncLogArea">
            <div>
                <asp:Panel ID="pnlAsyncBody" runat="server" CssClass="PageBody">
                    <asp:Panel ID="pnlTitleAsync" runat="server" CssClass="PageHeader" EnableViewState="false">
                        <cms:PageTitle ID="titleElemAsync" runat="server" EnableViewState="false" />
                    </asp:Panel>
                    <asp:Panel ID="pnlCancel" runat="server" CssClass="PageHeaderLine" EnableViewState="false">
                        <cms:CMSButton runat="server" ID="btnCancel" EnableViewState="false" CssClass="SubmitButton" />
                    </asp:Panel>
                    <asp:Panel ID="pnlAsyncContent" runat="server" CssClass="PageContent">
                        <cms:AsyncControl ID="ctlAsync" runat="server" />
                    </asp:Panel>
                </asp:Panel>
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlContent" runat="server">
        <asp:Label ID="lblError" runat="server" CssClass="ErrorLabel" EnableViewState="false"
            Visible="false" />
        <asp:Label ID="lblInfo" runat="server" CssClass="InfoLabel" EnableViewState="false"
            Visible="false" />
        <asp:PlaceHolder ID="plcFilter" runat="server">
            <cms:DocumentFilter ID="filterDocuments" runat="server" LoadSites="true" AllowSiteAutopostback="false" />
            <br />
        </asp:PlaceHolder>
        <cms:CMSUpdatePanel ID="pnlUpdate" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <cms:Documents ID="docElem" runat="server" ListingType="WorkflowDocuments" IsLiveSite="false" />
                <asp:Panel ID="pnlFooter" runat="server" CssClass="MassAction">
                    <asp:DropDownList ID="drpWhat" runat="server" CssClass="DropDownFieldSmall" />
                    <asp:DropDownList ID="drpAction" runat="server" CssClass="DropDownField" />
                    <cms:LocalizedButton ID="btnOk" runat="server" CssClass="SubmitButton SelectorButton" ResourceString="general.ok"
                        OnClick="btnOk_OnClick" />
                    <br />
                    <br />
                    <asp:Label ID="lblValidation" runat="server" CssClass="InfoLabel" EnableViewState="false" />
                </asp:Panel>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnOk" />
            </Triggers>
        </cms:CMSUpdatePanel>
    </asp:Panel>
    <asp:Literal ID="ltlScript" runat="server" EnableViewState="false" />
</asp:Content>
