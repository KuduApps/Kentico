<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Blogs_CMSPages_Comment_Edit"
    Theme="Default" MasterPageFile="~/CMSMasterPages/LiveSite/Dialogs/ModalDialogPage.master"
    Title="Comment edit" CodeFile="Comment_Edit.aspx.cs" %>
<%@ Register src="~/CMSAdminControls/UI/PageElements/PageTitle.ascx" tagname="PageTitle" tagprefix="cms" %>

<%@ Register Src="~/CMSModules/Blogs/Controls/BlogCommentEdit.ascx" TagName="BlogCommentEdit" TagPrefix="cms" %>

<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:Panel runat="server" ID="pnlBody" CssClass="PageBody">
        <asp:Panel runat="server" ID="pnlTitle" CssClass="PageHeader">
            <cms:PageTitle ID="titleElem" runat="server" EnableViewState="false" />
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlContent" CssClass="PageContent">
            <cms:BlogCommentEdit ID="ctrlCommentEdit" runat="server" DisplayButtons="false" AdvancedMode="true" />
        </asp:Panel>
    </asp:Panel>
    <asp:Literal ID="ltlScript" runat="server" EnableViewState="false" />
</asp:Content>
<asp:Content ID="cntFooter" ContentPlaceHolderID="plcFooter" runat="server">
    <div class="FloatRight">
        <cms:CMSButton ID="btnOk" runat="server" CssClass="SubmitButton"
            EnableViewState="false" /><cms:LocalizedButton ID="btnClose" runat="server" CssClass="SubmitButton"
                ResourceString="general.close" OnClientClick="window.close(); return false;" />
    </div>
</asp:Content>
