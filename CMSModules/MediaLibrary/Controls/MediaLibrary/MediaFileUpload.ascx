<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_MediaLibrary_Controls_MediaLibrary_MediaFileUpload"
    CodeFile="MediaFileUpload.ascx.cs" %>
<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/Content/Controls/Attachments/DirectFileUploader/DirectFileUploader.ascx"
    TagName="DirectFileUploader" TagPrefix="cms" %>
<cms:CMSUpdatePanel ID="updPanel" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <div class="AttachmentsList">
            <asp:PlaceHolder ID="plcUploader" runat="server">
                <cms:DirectFileUploader ID="newFileElem" runat="server" InsertMode="true" UploadMode="DirectSingle" />
            </asp:PlaceHolder>
            <asp:PlaceHolder ID="plcUploaderDisabled" runat="server">
                <asp:Image ID="imgDisabled" runat="server" CssClass="IconDisabled" EnableViewState="false" /><cms:LocalizedLabel
                    ID="lblDisabled" CssClass="NewAttachmentDisabled" ResourceString="attach.uploadfile"
                    runat="server" EnableViewState="false" />
            </asp:PlaceHolder>
            <asp:Label ID="lblError" runat="server" CssClass="ErrorLabel" Visible="false" EnableViewState="false" />
            <asp:Label ID="lblInfo" runat="server" CssClass="InfoLabel" Visible="false" EnableViewState="false" />
            <asp:Panel ID="pnlGrid" runat="server">
                <cms:UniGrid ID="gridAttachments" runat="server" />
            </asp:Panel>
            <div>
                <cms:CMSButton ID="hdnPostback" CssClass="HiddenButton" runat="server" EnableViewState="false" />
                <asp:HiddenField ID="hdnFileName" runat="server" />
            </div>
        </div>
    </ContentTemplate>
</cms:CMSUpdatePanel>
<cms:CMSButton ID="hdnFullPostback" CssClass="HiddenButton" runat="server" EnableViewState="false" />
