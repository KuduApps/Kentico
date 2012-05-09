<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_MediaLibrary_Controls_Dialogs_MediaView"
    CodeFile="MediaView.ascx.cs" %>
<%@ Register Src="~/CMSModules/Content/Controls/Dialogs/LinkMediaSelector/Search.ascx"
    TagName="DialogSearch" TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/Content/Controls/Dialogs/LinkMediaSelector/InnerMediaView.ascx"
    TagName="InnerMediaView" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/Content/Controls/Attachments/DirectFileUploader/DirectFileUploader.ascx"
    TagName="DirectFileUploader" TagPrefix="cms" %>
<asp:PlaceHolder ID="plcListingInfo" runat="server" Visible="false" EnableViewState="false">
    <div class="DialogListingInfo">
        <asp:Label ID="lblListingInfo" runat="server" CssClass="InfoLabel" EnableViewState="false"></asp:Label>
    </div>
</asp:PlaceHolder>
<div class="DialogViewContentTop">
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="TextLeft">
                <cms:CMSUpdatePanel ID="pnlUpdateDialog" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <cms:DialogSearch ID="dialogSearch" runat="server" />
                    </ContentTemplate>
                </cms:CMSUpdatePanel>
            </td>
            <asp:PlaceHolder ID="plcExtStorage" runat="server" Visible="false">
                <td class="TextRight">
                    <asp:LinkButton ID="lnkExtStoragePrepare" runat="server" CssClass="MenuItemEditSmall" EnableViewState="false">
                        <asp:Image ID="imgExtStoragePrepare" runat="server" />
                        <span id="spnExtStoragePrepare" runat="server"></span>
                    </asp:LinkButton>
                </td>
            </asp:PlaceHolder>
            <asp:PlaceHolder ID="plcParentButton" runat="server">
                <td class="TextRight">
                    <asp:LinkButton ID="btnParent" runat="server" CssClass="MenuItemEditSmall" EnableViewState="false">
                        <asp:Image ID="imgParent" runat="server" />
                        <span id="spnParent" runat="server"></span>
                        <asp:HiddenField ID="hdnLastNodeParentID" runat="server" />
                    </asp:LinkButton>
                </td>
            </asp:PlaceHolder>
        </tr>
    </table>
</div>
<div class="DialogViewContentBottom">
    <cms:CMSUpdatePanel ID="pnlUpdateView" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <cms:InnerMediaView ID="innermedia" ShortID="v" runat="server" />
        </ContentTemplate>
    </cms:CMSUpdatePanel>
</div>
