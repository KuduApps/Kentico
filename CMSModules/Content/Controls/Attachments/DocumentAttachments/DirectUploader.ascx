<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_Content_Controls_Attachments_DocumentAttachments_DirectUploader"
    CodeFile="DirectUploader.ascx.cs" %>
<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/Content/Controls/Attachments/DirectFileUploader/DirectFileUploader.ascx"
    TagName="DirectFileUploader" TagPrefix="cms" %>
<cms:cmsupdatepanel id="updPanel" runat="server" updatemode="Conditional">
    <ContentTemplate>
        <div class="AttachmentsList">
            <div class="New">
                <asp:PlaceHolder ID="plcUploader" runat="server">
                    <cms:DirectFileUploader ID="newAttachmentElem" runat="server" InsertMode="true" UploadMode="DirectSingle" />
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="plcUploaderDisabled" runat="server">
                    <asp:Image ID="imgDisabled" runat="server" CssClass="IconDisabled" EnableViewState="false" />
                    <cms:LocalizedLabel ID="lblDisabled" CssClass="NewAttachmentDisabled" ResourceString="attach.uploadfile"
                        runat="server" EnableViewState="false" />
                </asp:PlaceHolder>
            </div>
            <cms:AttachmentsDataSource ID="dsAttachments" runat="server" GetBinary="false" IsLiveSite="false" />
            <asp:Label ID="lblError" runat="server" CssClass="ErrorLabel" Visible="false" EnableViewState="false" />
            <asp:Label ID="lblInfo" runat="server" CssClass="InfoLabel" Visible="false" EnableViewState="false" />
            <asp:Panel ID="pnlGrid" runat="server">
                <cms:UniGrid ID="gridAttachments" runat="server" GridName="~/CMSModules/Content/Controls/Attachments/DocumentAttachments/DirectUploader.xml" />
            </asp:Panel>
            <div>
                <cms:CMSButton ID="hdnPostback" CssClass="HiddenButton" runat="server" EnableViewState="false" />
                <asp:HiddenField ID="hdnAttachName" runat="server" />
            </div>
        </div>
    </ContentTemplate>
</cms:cmsupdatepanel>
<cms:cmsbutton id="hdnFullPostback" cssclass="HiddenButton" runat="server" enableviewstate="false" />
