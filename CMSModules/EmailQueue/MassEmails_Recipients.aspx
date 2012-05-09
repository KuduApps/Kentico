<%@ Page Language="C#" AutoEventWireup="true"
    MasterPageFile="~/CMSMasterPages/UI/Dialogs/ModalDialogPage.master" Title="MassEmails - Recipients"
    Inherits="CMSModules_EmailQueue_MassEmails_Recipients" Theme="Default" CodeFile="MassEmails_Recipients.aspx.cs" %>
<%@ Register src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" tagname="UniGrid" tagprefix="cms" %>

<asp:Content ID="cntControls" ContentPlaceHolderID="plcControls" runat="server">
    <asp:Image ID="imgDeleted" CssClass="NewItemImage" runat="server" />
    <cms:LocalizedLinkButton ID="btnDeleteSelected" CssClass="NewItemLink" ResourceString="emailqueue.queue.deleteselected"
        runat="server" />
</asp:Content>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <div class="UniSelectorDialogGridArea">
        <cms:UniGrid ID="gridElem" runat="server" GridName="MassEmails_Recipients.xml" OrderBy="UserID" IsLiveSite="false" />
    </div>
</asp:Content>
<asp:Content ID="cntFooter" ContentPlaceHolderID="plcFooter" runat="server">
    <div class="FloatRight">
        <cms:LocalizedButton ID="btnClose" runat="server" CssClass="SubmitButton" ResourceString="general.close"
            OnClientClick="window.close();return false;" EnableViewState="false" />
    </div>
</asp:Content>
