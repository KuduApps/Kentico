<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_Content_Controls_Attachments_DocumentAttachments_DocumentAttachmentsList"
    CodeFile="DocumentAttachmentsList.ascx.cs" %>
<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/Content/Controls/Attachments/DirectFileUploader/DirectFileUploader.ascx"
    TagName="DirectFileUploader" TagPrefix="cms" %>
<cms:CMSUpdatePanel ID="updPanel" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <div class="AttachmentsList">
            <div class="New">
                <asp:PlaceHolder ID="plcUploader" runat="server">
                    <cms:DirectFileUploader ID="newAttachmentElem" runat="server" InsertMode="true" UploadMode="DirectMultiple"
                        Multiselect="true" ShowProgress="true" />
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="plcUploaderDisabled" runat="server">
                    <asp:Image ID="imgDisabled" runat="server" CssClass="IconDisabled" EnableViewState="false" /><cms:LocalizedLabel
                        ID="lblDisabled" CssClass="NewAttachmentDisabled" ResourceString="attach.newattachment"
                        runat="server" EnableViewState="false" />
                </asp:PlaceHolder>
            </div>
            <cms:AttachmentsDataSource ID="dsAttachments" runat="server" GetBinary="true" AutomaticColumns="true"
                IsLiveSite="false" />
            <asp:Label ID="lblError" runat="server" CssClass="ErrorLabel" Visible="false" EnableViewState="false" />
            <asp:Label ID="lblInfo" runat="server" CssClass="InfoLabel" Visible="false" EnableViewState="false" />
            <asp:Panel ID="pnlGrid" runat="server">
                <asp:Panel ID="pnlFilter" runat="server" DefaultButton="btnFilter" Visible="false">
                    <cms:CMSTextBox ID="txtFilter" runat="server" CssClass="SelectorTextBox" Width="250" />&nbsp;<cms:LocalizedButton
                        ID="btnFilter" runat="server" CssClass="ContentButton" ResourceString="general.search" /><br />
                    <br />
                </asp:Panel>
                <cms:UniGrid ID="gridAttachments" ShortID="g" runat="server" ExportFileName="cms_attachment"
                    GridName="~/CMSModules/Content/Controls/Attachments/DocumentAttachments/DocumentAttachmentsList.xml" />
                <cms:LocalizedLabel ID="lblNoData" runat="server" ResourceString="attach.nodata"
                    Visible="false" />
            </asp:Panel>
            <div>
                <cms:CMSButton ID="hdnPostback" CssClass="HiddenButton" runat="server" EnableViewState="false" />
            </div>
        </div>
    </ContentTemplate>
</cms:CMSUpdatePanel>
<cms:CMSButton ID="hdnFullPostback" CssClass="HiddenButton" runat="server" EnableViewState="false" />