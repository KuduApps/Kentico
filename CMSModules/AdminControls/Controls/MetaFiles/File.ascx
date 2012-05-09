<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_AdminControls_Controls_MetaFiles_File"
    CodeFile="File.ascx.cs" %>
<%@ Register Namespace="CMS.UIControls.UniGridConfig" TagPrefix="ug" Assembly="CMS.UIControls" %>
<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/Content/Controls/Attachments/DirectFileUploader/DirectFileUploader.ascx"
    TagName="DirectFileUploader" TagPrefix="cms" %>
<asp:PlaceHolder ID="plcOldUploader" runat="server">
    <div class="Uploader">
        <cms:Uploader ID="uploader" runat="server" BorderStyle="none" RequireDeleteConfirmation="true" />
        <asp:Label ID="lblErrorUploader" runat="server" Visible="false" CssClass="ErrorLabel"
            EnableViewState="false" />
    </div>
</asp:PlaceHolder>
<cms:CMSUpdatePanel ID="updPanel" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:Panel ID="pnlAttachmentList" runat="server">
            <div class="New">
                <asp:PlaceHolder ID="plcUploader" runat="server">
                    <cms:DirectFileUploader ID="newMetafileElem" runat="server" InsertMode="true" UploadMode="DirectSingle" />
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="plcUploaderDisabled" runat="server">
                    <asp:Image ID="imgDisabled" runat="server" CssClass="IconDisabled" EnableViewState="false" />
                    <cms:LocalizedLabel ID="lblDisabled" CssClass="NewAttachmentDisabled" ResourceString="attach.uploadfile"
                        runat="server" EnableViewState="false" />
                </asp:PlaceHolder>
            </div>
            <asp:Label ID="lblError" runat="server" CssClass="ErrorLabel" Visible="false" EnableViewState="false" />
            <asp:Label ID="lblInfo" runat="server" CssClass="InfoLabel" Visible="false" EnableViewState="false" />
            <asp:Panel ID="pnlGrid" runat="server">
                <cms:UniGrid ID="gridFile" runat="server" ObjectType="cms.metafile" OrderBy="MetaFileName"
                    HideControlForZeroRows="true" Columns="MetaFileID,MetaFileGUID,MetaFileObjectType,MetaFileObjectID,MetaFileGroupName,MetaFileName,MetaFileExtension,MetaFileSize,MetaFileImageWidth,MetaFileImageHeight,MetaFileTitle,MetaFileDescription">
                    <GridActions>
                        <ug:Action Name="edit" ExternalSourceName="edit" Caption="$General.Edit$" Icon="Edit.png" />
                        <ug:Action Name="delete" ExternalSourceName="delete" Caption="$General.Delete$" Icon="Delete.png"
                            Confirmation="$General.ConfirmDelete$" />
                    </GridActions>
                    <GridColumns>
                        <ug:Column Source="##ALL##" ExternalSourceName="update" Caption="$general.update$"
                            Wrap="false" Style="text-align: center;" CssClass="UniGridActions" AllowSorting="false" />
                        <ug:Column Source="##ALL##" ExternalSourceName="name" Caption="$general.filename$"
                            Wrap="false" Width="100%" AllowSorting="false" />
                        <ug:Column Source="MetaFileSize" ExternalSourceName="size" Caption="$general.size$"
                            Wrap="false" AllowSorting="false" />
                    </GridColumns>
                    <PagerConfig DisplayPager="false" ShowPageSize="false" />
                </cms:UniGrid>
            </asp:Panel>
            <div>
                <cms:CMSButton ID="hdnPostback" CssClass="HiddenButton" runat="server" EnableViewState="false" />
                <asp:HiddenField ID="hdnAttachName" runat="server" />
            </div>
        </asp:Panel>
    </ContentTemplate>
</cms:CMSUpdatePanel>
<cms:cmsbutton id="hdnFullPostback" cssclass="HiddenButton" runat="server" enableviewstate="false" />
