<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_MediaLibrary_Controls_LiveControls_MediaFileUploader" CodeFile="MediaFileUploader.ascx.cs" %>
<%@ Register Src="~/CMSAdminControls/Silverlight/MultiFileUploader/MultiFileUploader.ascx" TagPrefix="cms" TagName="MultiFileUploader" %>
    
<div class="MediaFileUploader">
    <cms:LocalizedLabel runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
        Visible="false" />
    <cms:LocalizedLabel runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
        Visible="false" />

    <cms:MultiFileUploader ID="mfuDirectUploader" runat="server" Height="16" Width="110" UploadMode="DirectSingle" Multiselect="true">
        <AlternateContent>
        </AlternateContent>
    </cms:MultiFileUploader>
        
    <table>
        <tr>
            <td>
                <cms:LocalizedLabel ID="lblFile" runat="server" EnableViewState="false" ResourceString="media.library.uploadfile"
                    DisplayColon="true" />
            </td>
            <td>
                <cms:CMSFileUpload ID="fileUploader" runat="server" />
            </td>
            <td id="cellUpload" runat="server">
                <cms:LocalizedButton ID="btnUpload" runat="server" ResourceString="general.upload"
                    CssClass="ContentButton" EnableViewState="false" />
            </td>
        </tr>
        <asp:PlaceHolder ID="plcPreview" runat="server" Visible="false">
            <tr>
                <td>
                    <cms:LocalizedLabel ID="lblPreview" runat="server" DisplayColon="true" EnableViewState="false"
                        ResourceString="media.library.uploadpreview" />
                </td>
                <td>
                    <cms:CMSFileUpload ID="previewUploader" runat="server" />
                </td>
            </tr>
        </asp:PlaceHolder>
    </table>
    
    <cms:CMSButton ID="btnHidden" runat="server" CssClass="HiddenButton" OnClick="btnHidden_Click"
            EnableViewState="false" />
</div>
