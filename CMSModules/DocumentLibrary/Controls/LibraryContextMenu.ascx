<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LibraryContextMenu.ascx.cs"
    Inherits="CMSModules_DocumentLibrary_Controls_LibraryContextMenu" %>
<%@ Register Assembly="CMS.UIControls" Namespace="CMS.UIControls" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/Content/Controls/Attachments/DirectFileUploader/DirectFileUploader.ascx"
    TagName="DirectFileUploader" TagPrefix="cms" %>
<cms:ContextMenu ID="libraryMenuElem" runat="server" Dynamic="true">
    <asp:Panel runat="server" ID="pnlLibraryMenu" CssClass="LibraryContextMenu" EnableViewState="false">
        <asp:Panel runat="server" ID="pnlEdit" CssClass="Item" Visible="false">
            <asp:Panel runat="server" ID="pnlEditPadding" CssClass="ItemPadding">
            </asp:Panel>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlUpload" CssClass="Item" Visible="false">
            <asp:Panel runat="server" ID="pnlUploadPadding">
                <cms:DirectFileUploader ID="updateAttachment" runat="server" InsertMode="false" UploadMode="DirectSingle" EnableSilverlightUploader="false" />
            </asp:Panel>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlLocalize" CssClass="Item" Visible="false">
            <asp:Panel runat="server" ID="pnlLocalizePadding" CssClass="ItemPadding">
                <asp:Image runat="server" ID="imgLocalize" CssClass="Icon" EnableViewState="false" />&nbsp;<cms:LocalizedLabel
                    runat="server" ID="lblLocalize" CssClass="Name" EnableViewState="false" ResourceString="LibraryContextMenu.Localize" />
            </asp:Panel>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlSep1" CssClass="Separator" />
        <asp:Panel runat="server" ID="pnlCopy" CssClass="Item" Visible="false">
            <asp:Panel runat="server" ID="pnlCopyPadding" CssClass="ItemPadding">
                <asp:Image runat="server" ID="imgCopy" CssClass="Icon" EnableViewState="false" />&nbsp;<cms:LocalizedLabel
                    runat="server" ID="lblCopy" CssClass="Name" EnableViewState="false" ResourceString="general.copy" />
            </asp:Panel>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlDelete" CssClass="Item" Visible="false">
            <asp:Panel runat="server" ID="pnlDeletePadding" CssClass="ItemPadding">
                <asp:Image runat="server" ID="imgDelete" CssClass="Icon" EnableViewState="false" />&nbsp;<cms:LocalizedLabel
                    runat="server" ID="lblDelete" CssClass="Name" EnableViewState="false" ResourceString="general.delete" />
            </asp:Panel>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlOpen" CssClass="Item" Visible="false">
            <asp:Panel runat="server" ID="pnlOpenPadding" CssClass="ItemPadding">
                <asp:Image runat="server" ID="imgOpen" CssClass="Icon" EnableViewState="false" />&nbsp;<cms:LocalizedLabel
                    runat="server" ID="lblOpen" CssClass="Name" EnableViewState="false" ResourceString="general.open" />
            </asp:Panel>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlSep2" CssClass="Separator" />
        <asp:Panel runat="server" ID="pnlProperties" CssClass="Item" Visible="false">
            <asp:Panel runat="server" ID="pnlPropertiesPadding" CssClass="ItemPadding">
                <asp:Image runat="server" ID="imgProperties" CssClass="Icon" EnableViewState="false" />&nbsp;<cms:LocalizedLabel
                    runat="server" ID="lblProperties" CssClass="Name" EnableViewState="false" ResourceString="general.properties" />
            </asp:Panel>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlPermissions" CssClass="Item" Visible="false">
            <asp:Panel runat="server" ID="pnlPermissionsPadding" CssClass="ItemPadding">
                <asp:Image runat="server" ID="imgPermissions" CssClass="Icon" EnableViewState="false" />&nbsp;<cms:LocalizedLabel
                    runat="server" ID="lblPermissions" CssClass="Name" EnableViewState="false" ResourceString="general.permissions" />
            </asp:Panel>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlVersionHistory" CssClass="Item" Visible="false">
            <asp:Panel runat="server" ID="pnlVersionHistoryPadding" CssClass="ItemPadding">
                <asp:Image runat="server" ID="imgVersionHistory" CssClass="Icon" EnableViewState="false" />&nbsp;<cms:LocalizedLabel
                    runat="server" ID="lblVersionHistory" CssClass="Name" EnableViewState="false"
                    ResourceString="LibraryContextMenu.VersionHistory" />
            </asp:Panel>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlSep3" CssClass="Separator" />
        <asp:Panel runat="server" ID="pnlCheckOut" CssClass="Item" Visible="false">
            <asp:Panel runat="server" ID="pnlCheckOutPadding" CssClass="ItemPadding">
                <asp:Image runat="server" ID="imgCheckOut" CssClass="Icon" EnableViewState="false" />&nbsp;<cms:LocalizedLabel
                    runat="server" ID="lblCheckOut" CssClass="Name" EnableViewState="false" ResourceString="general.checkout" />
            </asp:Panel>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlCheckIn" CssClass="Item" Visible="false">
            <asp:Panel runat="server" ID="pnlCheckInPadding" CssClass="ItemPadding">
                <asp:Image runat="server" ID="imgCheckIn" CssClass="Icon" EnableViewState="false" />&nbsp;<cms:LocalizedLabel
                    runat="server" ID="lblCheckIn" CssClass="Name" EnableViewState="false" ResourceString="general.checkin" />
            </asp:Panel>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlUndoCheckout" CssClass="Item" Visible="false">
            <asp:Panel runat="server" ID="pnlUndoCheckoutPadding" CssClass="ItemPadding">
                <asp:Image runat="server" ID="imgUndoCheckout" CssClass="Icon" EnableViewState="false" />&nbsp;<cms:LocalizedLabel
                    runat="server" ID="lblUndoCheckout" CssClass="Name" EnableViewState="false" ResourceString="general.undocheckout" />
            </asp:Panel>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlSubmitToApproval" CssClass="Item" Visible="false">
            <asp:Panel runat="server" ID="pnlSubmitToApprovalPadding" CssClass="ItemPadding">
                <asp:Image runat="server" ID="imgSubmitToApproval" CssClass="Icon" EnableViewState="false" />&nbsp;<cms:LocalizedLabel
                    runat="server" ID="lblSubmitToApproval" CssClass="Name" EnableViewState="false"
                    ResourceString="LibraryContextMenu.SubmitToApproval" />
            </asp:Panel>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlReject" CssClass="Item" Visible="false">
            <asp:Panel runat="server" ID="pnlRejectPadding" CssClass="ItemPadding">
                <asp:Image runat="server" ID="imgReject" CssClass="Icon" EnableViewState="false" />&nbsp;<cms:LocalizedLabel
                    runat="server" ID="lblReject" CssClass="Name" EnableViewState="false" ResourceString="general.reject" />
            </asp:Panel>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlArchive" CssClass="ItemLast" Visible="false">
            <asp:Panel runat="server" ID="pnlArchivePadding" CssClass="ItemPadding">
                <asp:Image runat="server" ID="imgArchive" CssClass="Icon" EnableViewState="false" />&nbsp;<cms:LocalizedLabel
                    runat="server" ID="lblArchive" CssClass="Name" EnableViewState="false" ResourceString="general.archive" />
            </asp:Panel>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlNoAction" CssClass="ItemLast" Visible="false">
            <asp:Panel runat="server" ID="pnlNoActionPadding" CssClass="ItemPadding">
                <cms:LocalizedLabel runat="server" ID="lblNoAction" CssClass="Name" EnableViewState="false"
                    ResourceString="documentlibrary.noaction" />
            </asp:Panel>
        </asp:Panel>
    </asp:Panel>
</cms:ContextMenu>
