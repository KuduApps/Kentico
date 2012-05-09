<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_MediaLibrary_Controls_Dialogs_DirectFileUploader_FileUpload" CodeFile="FileUpload.ascx.cs" %>
<asp:Panel ID="pnlBody" runat="server">
    <asp:Panel ID="pnlContent" CssClass="PageContent" Style="height: 70px; padding-top: 0px;
        padding-bottom: 0px;" runat="server">
        <cms:localizedlabel id="lblError" cssclass="ErrorLabel" runat="server" enableviewstate="false"
            visible="false" />
        <table width="100%">
            <tr>
                <td>
                    <cms:cmsfileupload id="ucFileUpload" runat="server" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="pblButtons" CssClass="PageFooterLine" runat="server">
        <div class="FloatRight">
            <cms:localizedbutton id="btnOk" onclick="btnOK_Click" runat="server" resourcestring="general.ok"
                cssclass="SubmitButton" enableviewstate="false" />
            <cms:localizedbutton id="btnCancel" onclientclick="javascript:window.close();" runat="server"
                resourcestring="general.cancel" cssclass="SubmitButton" enableviewstate="false" />
        </div>
    </asp:Panel>
</asp:Panel>
