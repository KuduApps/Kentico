<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_System_Files_System_FilesAttachments"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Administration - System - Files - Attachments"
    CodeFile="System_FilesAttachments.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/UI/PageElements/PageTitle.ascx" TagName="PageTitle"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSFormControls/Sites/SiteSelector.ascx" TagName="SiteSelector"
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
                    <asp:Panel ID="pnlCancel" runat="server" CssClass="PageHeaderLine SiteHeaderLine">
                        <cms:LocalizedButton runat="server" ID="btnCancel" ResourceString="general.cancel"
                            CssClass="SubmitButton" />
                    </asp:Panel>
                    <asp:Panel ID="pnlAsyncContent" runat="server" CssClass="PageContent">
                        <cms:AsyncControl ID="ctlAsync" runat="server" />
                    </asp:Panel>
                </asp:Panel>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
<asp:Content ID="plcBefore" runat="server" ContentPlaceHolderID="plcBeforeContent">
    <asp:Panel runat="server" ID="Panel1" CssClass="PageHeaderLine SiteHeaderLine">
        <cms:LocalizedLabel runat="server" ID="lblSite" EnableViewState="false" DisplayColon="true"
            ResourceString="General.Site" />
        <cms:SiteSelector ID="siteSelector" runat="server" IsLiveSite="false" />
    </asp:Panel>
</asp:Content>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:Panel runat="server" ID="pnlContent">
        <cms:CMSUpdatePanel ID="pnlUpdate" runat="server">
            <ContentTemplate>
                <asp:Label ID="lblGridInfo" runat="server" CssClass="InfoLabel" EnableViewState="false" />
                <asp:Label ID="lblGridError" runat="server" CssClass="ErrorLabel" EnableViewState="false" />
                <cms:UniGrid runat="server" ID="gridFiles" GridName="Attachments.xml" OrderBy="AttachmentName, AttachmentID"
                    ObjectType="cms.attachment" Columns="AttachmentID, AttachmentName, AttachmentExtension, AttachmentSize, AttachmentGUID, AttachmentSiteID, AttachmentLastModified, AttachmentImageWidth, AttachmentImageHeight, CASE WHEN AttachmentBinary IS NULL THEN 0 ELSE 1 END AS HasBinary" />
                <asp:Panel ID="pnlFooter" runat="server" CssClass="MassAction">
                    <asp:DropDownList ID="drpWhat" runat="server" CssClass="DropDownFieldSmall">
                        <asp:ListItem Text="Selected files" Value="selected" />
                        <asp:ListItem Text="All files" Value="all" />
                    </asp:DropDownList>
                    <asp:DropDownList ID="drpAction" runat="server" CssClass="DropDownFieldSmall" />
                    <cms:LocalizedButton ID="btnOk" runat="server" ResourceString="general.ok" CssClass="SubmitButton SelectorButton"
                        EnableViewState="false" OnClick="btnOK_Click" />
                    <br />
                    <br />
                </asp:Panel>
            </ContentTemplate>
        </cms:CMSUpdatePanel>
    </asp:Panel>
</asp:Content>
