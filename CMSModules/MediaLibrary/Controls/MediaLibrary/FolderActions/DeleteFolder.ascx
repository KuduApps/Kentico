<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_MediaLibrary_Controls_MediaLibrary_FolderActions_DeleteFolder" CodeFile="DeleteFolder.ascx.cs" %>
<table width="100%">
    <tr>
        <td class="MediaLibraryTitleLine">
            <asp:Image ID="imgTitle" runat="server" CssClass="PageTitleImage" EnableViewState="false" />
            <cms:LocalizedLabel ID="lblTitle" runat="server" DisplayColon="false" ResourceString="media.folder.delete"
                CssClass="PageTitle" EnableViewState="false"></cms:LocalizedLabel>
        </td>
    </tr>
    <tr>
        <td style="width: 100%">
            &nbsp;</td>
    </tr>
    <tr>
        <td>
            <cms:LocalizedLabel ID="lblInfo" runat="server" DisplayColon="false" Visible="false"
                CssClass="InfoLabel" EnableViewState="false"></cms:LocalizedLabel>
            <cms:LocalizedLabel ID="lblError" runat="server" DisplayColon="false" Visible="false"
                CssClass="ErrorLabel" EnableViewState="false"></cms:LocalizedLabel></td>
    </tr>
    <tr>
        <td>
            <cms:LocalizedLabel ID="lblDeleteInfo" runat="server" CssClass="FieldLabel" EnableViewState="false" />
        </td>
    </tr>
    <tr>
        <td>
            <div style="padding-top: 8px;">
                <cms:CMSButton ID="btnOk" runat="server" OnClick="btnOk_Click" CssClass="SubmitButton"
                    EnableViewState="false" />
                <cms:CMSButton ID="btnCancel" runat="server" OnClick="btnCancel_Click" CssClass="ContentButton"
                    EnableViewState="false" />
            </div>
        </td>
    </tr>
</table>
