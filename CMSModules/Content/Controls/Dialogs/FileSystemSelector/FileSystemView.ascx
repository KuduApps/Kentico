<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_Content_Controls_Dialogs_FileSystemSelector_FileSystemView" CodeFile="FileSystemView.ascx.cs" %>
<%@ Register Src="~/CMSModules/Content/Controls/Dialogs/LinkMediaSelector/Search.ascx"
    TagName="DialogSearch" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/Content/Controls/Dialogs/FileSystemSelector/InnerFileSystemView.ascx"
    TagName="InnerFileSystemView" TagPrefix="cms" %>
<asp:PlaceHolder ID="plcListingInfo" runat="server" Visible="false" EnableViewState="false">
    <div class="DialogListingInfo">
        <asp:Label ID="lblListingInfo" runat="server" CssClass="InfoLabel"></asp:Label>
    </div>
</asp:PlaceHolder>
<div class="DialogViewContentTop">
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td style="vertical-align: top;" class="TextLeft">
                <cms:CMSUpdatePanel ID="pnlUpdateDialog" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <cms:DialogSearch ID="dialogSearch" runat="server" />
                    </ContentTemplate>
                </cms:CMSUpdatePanel>
            </td>
            <td style="vertical-align: top;" class="TextRight">
                <asp:PlaceHolder ID="plcParentButton" runat="server">
                    <asp:LinkButton ID="btnParent" runat="server" CssClass="MenuItemEditSmall">
                        <asp:Image ID="imgParent" runat="server" />
                        <% =mSaveText %>
                        <asp:HiddenField ID="hdnLastNodeParentID" runat="server" />
                    </asp:LinkButton>
                </asp:PlaceHolder>
            </td>
        </tr>
    </table>
</div>
<div class="DialogViewContentBottom">
    <cms:CMSUpdatePanel ID="pnlUpdateView" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <cms:InnerFileSystemView ID="innermedia" runat="server" />
        </ContentTemplate>
    </cms:CMSUpdatePanel>
</div>
