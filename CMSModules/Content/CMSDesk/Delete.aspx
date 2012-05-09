<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Content_CMSDesk_Delete"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Content - Delete"
    CodeFile="Delete.aspx.cs" %>

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
                        <cms:CMSButton runat="server" ID="btnCancel" CssClass="SubmitButton" />
                    </asp:Panel>
                    <asp:Panel ID="pnlAsyncContent" runat="server" CssClass="PageContent">
                        <cms:AsyncControl ID="ctlAsync" runat="server" />
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
                Font-Bold="true" />
            <br />
            <br id="brSeparator" runat="server" />
            <asp:Panel ID="pnlDocList" runat="server" Visible="false" CssClass="ScrollableList"
                EnableViewState="false">
                <asp:Label ID="lblDocuments" runat="server" CssClass="ContentLabel" EnableViewState="true" />
            </asp:Panel>
            <asp:PlaceHolder ID="plcCheck" runat="server" EnableViewState="false">
                <br />
                <div>
                    <asp:CheckBox ID="chkDestroy" runat="server" CssClass="ContentCheckbox" Visible="false"
                        EnableViewState="false" />
                </div>
                <div>
                    <asp:CheckBox ID="chkAllCultures" runat="server" CssClass="ContentCheckbox" EnableViewState="false" />
                </div>
            </asp:PlaceHolder>
            <asp:Panel ID="pnlDeleteSKU" runat="server" EnableViewState="false">
                <asp:CheckBox ID="chkDeleteSKU" runat="server" CssClass="ContentCheckBox" Visible="false" />
            </asp:Panel>
            <br />
            <cms:LocalizedButton ID="btnOk" runat="server" CssClass="SubmitButton" OnClick="btnOK_Click"
                ResourceString="general.yes" EnableViewState="false" /><cms:LocalizedButton ID="btnNo"
                    runat="server" CssClass="SubmitButton" ResourceString="general.no" EnableViewState="false" />
        </asp:Panel>
    </asp:Panel>
    <asp:Literal ID="ltlScript" runat="server" EnableViewState="false" />
</asp:Content>
