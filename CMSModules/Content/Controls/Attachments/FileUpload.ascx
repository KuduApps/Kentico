<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_Content_Controls_Attachments_FileUpload" CodeFile="FileUpload.ascx.cs" %>
<asp:Panel ID="pnlBody" runat="server">
    <asp:Panel ID="pnlContent" CssClass="PageContent" Style="height: 70px; padding-top: 0px;
        padding-bottom: 0px;" runat="server">
        <cms:LocalizedLabel ID="lblError" CssClass="ErrorLabel" runat="server" EnableViewState="false"
            Visible="false" />
        <cms:CMSFileUpload ID="ucFileUpload" runat="server" />
    </asp:Panel>
    <asp:Panel ID="pblButtons" CssClass="PageFooterLine" runat="server">
        <div class="FloatRight">
            <cms:LocalizedButton ID="btnOk" OnClick="btnOK_Click" runat="server" ResourceString="general.ok"
                CssClass="SubmitButton" EnableViewState="false" /><cms:LocalizedButton ID="btnCancel"
                    OnClientClick="window.close(); return false;" runat="server" ResourceString="general.cancel"
                    CssClass="SubmitButton" EnableViewState="false" />
        </div>
    </asp:Panel>
</asp:Panel>
