<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_MediaLibrary_Controls_MediaLibrary_FolderActions_FolderActions" CodeFile="FolderActions.ascx.cs" %>
<asp:Panel ID="pnlActions" runat="server" >
    <table style="width:100%;" cellpadding="0" cellspacing="0">
        <tr>
            <asp:PlaceHolder ID="plcDelete" runat="server">
                <td>
                    <asp:Image ID="imgDelete" runat="server" CssClass="NewItemImage" EnableViewState="false" />
                    <asp:LinkButton ID="lnkDelete" runat="server" CssClass="NewItemLink" OnClick="lnkDelete_Click"
                        EnableViewState="false">
                    </asp:LinkButton>
                </td>
            </asp:PlaceHolder>
            <asp:PlaceHolder ID="plcCopy" runat="server">
                <td>
                    <asp:Image ID="imgCopy" runat="server" CssClass="NewItemImage" EnableViewState="false" />
                    <asp:LinkButton ID="lnkCopy" runat="server" CssClass="NewItemLink" EnableViewState="false">
                    </asp:LinkButton>
                </td>
            </asp:PlaceHolder>
            <asp:PlaceHolder ID="plcMove" runat="server">
                <td>
                    <asp:Image ID="imgMove" runat="server" CssClass="NewItemImage" EnableViewState="false" />
                    <asp:LinkButton ID="lnkMove" runat="server" CssClass="NewItemLink" EnableViewState="false">
                    </asp:LinkButton>
                </td>
            </asp:PlaceHolder>
        </tr>
    </table>
</asp:Panel>
<asp:Literal ID="ltlScrip" runat="server" />