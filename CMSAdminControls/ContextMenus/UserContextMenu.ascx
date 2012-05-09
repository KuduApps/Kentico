<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSAdminControls_ContextMenus_UserContextMenu" CodeFile="UserContextMenu.ascx.cs" %>
<asp:Panel runat="server" ID="pnlUserContextMenu">
    <asp:Repeater runat="server" ID="repItem">
        <ItemTemplate>
            <asp:Panel runat="server" ID="pnlItem" CssClass="Item">
                <asp:Panel runat="server" ID="pnlItemPadding" CssClass="ItemPadding">
                    <asp:Image runat="server" ID="imgItem" CssClass="Icon" EnableViewState="false" ImageUrl='<%# UIHelper.GetImageUrl(this.Page, "Design/Controls/ContextMenu/User/" + Convert.ToString(DataBinder.Eval(Container.DataItem, "ActionIcon"))) %>' />&nbsp;
                    <asp:Label runat="server" ID="lblItem" CssClass="Name" EnableViewState="false" Text='<%# DataBinder.Eval(Container.DataItem, "ActionDisplayName") %>' />
                </asp:Panel>
            </asp:Panel>
        </ItemTemplate>
    </asp:Repeater>
</asp:Panel>
<asp:HiddenField ID="hdnSelectedId" runat="server" />
