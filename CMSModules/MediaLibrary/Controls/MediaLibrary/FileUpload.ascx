<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_MediaLibrary_Controls_MediaLibrary_FileUpload" CodeFile="FileUpload.ascx.cs" %>
<%@ Register Src="~/CMSModules/Content/Controls/editmenu.ascx" TagName="EditMenu"
    TagPrefix="cms" %>

<script type="text/javascript">
    //<![CDATA[
    // Refresh the parent and close the current winbdow
    function RefreshAndClose(path) {

        wopener.RefreshPage(path);
        window.close();
    }

    // Refresh the parent
    function RefreshParent(path) {
        wopener.RefreshPage(path);
    }
    //]]>
</script>

<asp:Panel runat="server" ID="pnlBody" CssClass="PageBody">
    <asp:Panel runat="server" ID="pnlMenu" CssClass="ContentEditMenu">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <cms:EditMenu ID="menuElem" runat="server" ShowApprove="false" ShowReject="false"
                        ShowSubmitToApproval="false" ShowProperties="false" OnLocalSave="btnOk_Click"
                        EnablePassiveRefresh="false" ShowSave="true" ShowCreateAnother="true" AllowSave="true"
                        Action="new" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <br />
    <asp:Panel ID="pnlContent" runat="server" CssClass="PageContentFrame">
        <asp:Label ID="lblInfo" runat="server" CssClass="ContentLabel" EnableViewState="false" />
        <asp:Label ID="lblError" runat="server" EnableViewState="false" ForeColor="Red" />
        <asp:Panel ID="pnlForm" runat="server">
            <table>
                <tr>
                    <td>
                        <cms:LocalizedLabel ID="lblUploadFile" runat="server" ResourceString="media.fileupload.uploadfile"
                            DisplayColon="true" ShowRequiredMark="true" EnableViewState="false"></cms:LocalizedLabel>
                    </td>
                    <td>
                        <cms:CMSFileUpload ID="FileUpload" runat="server" Width="456px" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <cms:LocalizedLabel ID="lblUploadPreview" runat="server" ResourceString="media.fileupload.uploadpreview"
                            DisplayColon="true" EnableViewState="false"></cms:LocalizedLabel>
                    </td>
                    <td>
                        <cms:CMSFileUpload ID="PreviewUpload" runat="server" Width="456px" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <cms:LocalizedLabel ID="lblFileDescription" runat="server" ResourceString="media.fileupload.filedescription"
                            DisplayColon="true" EnableViewState="false"></cms:LocalizedLabel>
                    </td>
                    <td>
                        <cms:CMSTextBox ID="txtFileDescription" runat="server" MaxLength="500" TextMode="MultiLine"
                            Rows="6" Width="450px"></cms:CMSTextBox >
                    </td>
                </tr>
            </table>
            <cms:CMSButton ID="btnOk" runat="server" CssClass="HiddenButton" OnClick="btnOk_Click"
                EnableViewState="false" />
        </asp:Panel>
    </asp:Panel>
</asp:Panel>
<asp:Literal ID="ltlScript" runat="server" EnableViewState="False" />
<asp:Literal ID="ltlPostBackScript" runat="server" EnableViewState="False" />