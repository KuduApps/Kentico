<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_AdminControls_Controls_MetaFiles_FileList"
    CodeFile="FileList.ascx.cs" %>
<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/Content/Controls/Attachments/DirectFileUploader/DirectFileUploader.ascx"
    TagName="DirectFileUploader" TagPrefix="cms" %>
<cms:CMSUpdatePanel ID="updPanel" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <div class="AttachmentsList">
            <%-- Uploaders --%>
            <asp:PlaceHolder ID="plcUploader" runat="server">
                <cms:CMSFileUpload ID="uploader" runat="server" />
                <cms:CMSButton ID="btnUpload" runat="server" OnClick="btnUpload_Click" CssClass="ContentButton"
                    EnableViewState="false" />
            </asp:PlaceHolder>
            <asp:PlaceHolder ID="plcDirectUploder" runat="server">
                <div class="New">
                    <cms:DirectFileUploader ID="newMetafileElem" runat="server" InsertMode="true" UploadMode="DirectSingle" />
                </div>
            </asp:PlaceHolder>
            <asp:PlaceHolder ID="plcUploaderDisabled" runat="server">
                <div class="New">
                    <div class="NewAttachment">
                        <asp:Image ID="imgDisabled" runat="server" CssClass="IconDisabled" EnableViewState="false" />
                        <cms:LocalizedLabel ID="lblDisabled" CssClass="NewAttachmentDisabled" ResourceString="attach.uploadfile"
                            runat="server" EnableViewState="false" />
                    </div>
                    <div class="Clear"></div>
                </div>
            </asp:PlaceHolder>
            <asp:Label ID="lblError" runat="server" Visible="false" CssClass="ErrorLabel" EnableViewState="false" />
            <%-- Meta files list --%>
            <asp:PlaceHolder runat="server" ID="plcGridFiles" Visible="true">
                <cms:UniGrid runat="server" ID="gridFiles" GridName="~/CMSModules/AdminControls/Controls/MetaFiles/FileList.xml"
                    OrderBy="MetaFileName" Columns="MetaFileID,MetaFileGUID,MetaFileObjectType,MetaFileObjectID,MetaFileGroupName,MetaFileName,MetaFileExtension,MetaFileSize,MetaFileImageWidth,MetaFileImageHeight,MetaFileTitle,MetaFileDescription" />
            </asp:PlaceHolder>
            <cms:CMSButton ID="hdnPostback" runat="server" CssClass="HiddenButton" EnableViewState="false"
                OnClick="hdnPostback_Click" />
            <asp:HiddenField ID="hdnField" runat="server" />
        </div>
    </ContentTemplate>
</cms:CMSUpdatePanel>
<cms:CMSButton id="hdnFullPostback" cssclass="HiddenButton" runat="server" enableviewstate="false"
    onclick="hdnPostback_Click" />
