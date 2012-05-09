<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_MediaLibrary_Controls_Dialogs_DirectFileUploader_DirectMediaFileUploaderControl"
    CodeFile="DirectMediaFileUploaderControl.ascx.cs" %>
<div id="uploaderDiv" style="display: none;">
    <cms:CMSFileUpload ID="ucFileUpload" runat="server" />
</div>
<cms:CMSButton ID="btnHidden" runat="server" OnClick="btnHidden_Click" EnableViewState="false" />
