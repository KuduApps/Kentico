<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_Membership_FormControls_Users_UserPictureEdit" CodeFile="UserPictureEdit.ascx.cs" %>
<%@ Register Src="~/CMSAdminControls/UI/UserPicture.ascx" TagName="UserPicture" TagPrefix="cms" %>
<asp:Literal ID="ltlScript" runat="server" EnableViewState="false" />
<table cellpadding="0" cellspacing="0">
    <tr>
        <td>
            <div id="<%= divId %>" style="display: none">
            </div>
            <asp:PlaceHolder runat="server" ID="plcImageActions" Visible="false">
                <div id="<%= placeholderId %>">
                    <cms:UserPicture ID="picUser" runat="server" Visible="true" />
                    &nbsp;<asp:ImageButton runat="server" ID="btnDeleteImage" EnableViewState="false" /><br />
                </div>
            </asp:PlaceHolder>
            <div class="Uploader">
                <asp:Label runat="server" ID="lblUploader" CssClass="UploaderLabel" EnableViewState="false" />
                <cms:CMSFileUpload runat="server" CssClass="UploaderInputFile" ID="uplFilePicture" />
            </div>
        </td>
    </tr>
    <tr>
        <td style="padding-left: 3px; padding-top: 3px;">
            <asp:LinkButton runat="server" ID="btnShowGallery" EnableViewState="false" />
            <asp:HiddenField runat="Server" ID="hiddenAvatarGuid" />
            <asp:HiddenField runat="Server" ID="hiddenDeleteAvatar" />
        </td>
    </tr>
</table>
