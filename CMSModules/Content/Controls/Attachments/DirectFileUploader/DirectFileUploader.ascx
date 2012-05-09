<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_Content_Controls_Attachments_DirectFileUploader_DirectFileUploader"
    CodeFile="DirectFileUploader.ascx.cs" %>
<%@ Register Src="~/CMSAdminControls/Silverlight/MultiFileUploader/MultiFileUploader.ascx"
    TagPrefix="cms" TagName="MultiFileUploader" %>
<div id="containerDiv" runat="server">
    <asp:Panel ID="pnlInnerDiv" runat="server" CssClass="InnerDiv" EnableViewState="false">
        <asp:Literal ID="ltlUploadImage" runat="server" EnableViewState="false" />
        <asp:Literal ID="ltlInnerDiv" runat="server" EnableViewState="false" />
    </asp:Panel>
    <div class="UploaderDiv" style="position: absolute; top: 0; left: 0px;">
        <cms:MultiFileUploader ID="mfuDirectUploader" runat="server">
            <AlternateContent>
                <iframe id="uploaderFrame" runat="server" frameborder="0" scrolling="no" marginheight="0"
                    marginwidth="0" enableviewstate="false" style="vertical-align: middle; width: 0;
                    height: 0;" />
            </AlternateContent>
        </cms:MultiFileUploader>
    </div>
    <asp:Panel ID="pnlLoading" class="LoadingDiv" runat="server">
        <asp:Image ID="imgLoading" runat="server" EnableViewState="false" /><asp:Label ID="lblLoading"
            runat="server" EnableViewState="false" /><asp:Label ID="lblProgress" runat="server"
                EnableViewState="false" />
    </asp:Panel>
</div>
<asp:Literal ID="ltlScript" runat="server" EnableViewState="false" />
