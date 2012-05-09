<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PanelMenuCategory.ascx.cs"
    Inherits="CMSAdminControls_UI_PanelMenu_PanelMenuCategory" %>
<asp:Panel ID="pnlCategory" runat="server" CssClass="PanelMenuCategory">
    <cms:CMSImage ID="imgCategoryImage" runat="server" CssClass="CategoryImage" />
    <div class="CategoryMenu">
        <cms:LocalizedHyperlink ID="lnkCategoryTitle" runat="server" CssClass="CategoryTitle" />
        <ul>
            <asp:Repeater ID="rptCategoryActions" runat="server">
                <ItemTemplate>
                    <li>
                        <cms:LocalizedHyperlink ID="lnkCategoryAction" runat="server" CssClass="CategoryAction"
                            ResourceString="<%# ((string[])Container.DataItem)[0] %>" NavigateUrl="<%# ((string[])Container.DataItem)[1] %>"
                            OnClientClick="<%# ((string[])Container.DataItem)[2] %>" />
                    </li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
    </div>
    <div class="ClearBoth">
    </div>
</asp:Panel>
