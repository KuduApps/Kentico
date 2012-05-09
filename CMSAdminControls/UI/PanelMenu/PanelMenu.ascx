<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PanelMenu.ascx.cs" Inherits="CMSAdminControls_UI_PanelMenu_PanelMenu" %>
<%@ Register Src="~/CMSAdminControls/UI/PanelMenu/PanelMenuCategory.ascx" TagName="PanelMenuCategory"
    TagPrefix="cms" %>
<asp:Panel ID="pnlPanelMenu" runat="server" CssClass="PanelMenu">
    <asp:Repeater ID="rptColumns" runat="server">
        <ItemTemplate>
            <asp:Panel ID="pnlColumn" runat="server" CssClass="Column">
                <asp:Repeater ID="rptCategories" runat="server" DataSource="<%# Container.DataItem %>">
                    <ItemTemplate>
                        <cms:PanelMenuCategory ID="panelMenuCategory" runat="server" CategoryTitle="<%# ((object[])Container.DataItem)[0] %>"
                            CategoryName="<%# ((object[])Container.DataItem)[1] %>" CategoryURL="<%# ((object[])Container.DataItem)[2] %>"
                            CategoryImageUrl="<%# ((object[])Container.DataItem)[3] %>" CategoryTooltip="<%# ((object[])Container.DataItem)[4] %>"
                            CategoryActions="<%# ((object[])Container.DataItem)[5] %>" />
                    </ItemTemplate>
                </asp:Repeater>
            </asp:Panel>
        </ItemTemplate>
    </asp:Repeater>
    <div class="ClearBoth">
    </div>
</asp:Panel>

<script type="text/javascript">
    //<![CDATA[
    function SelectRibbonButton(name) {
        if ((parent.frames[0] !== null) && (parent.frames[0].selectMenuButton !== null)) {
            parent.frames[0].selectMenuButton(name);
        }
    }
    //]]>
</script>

