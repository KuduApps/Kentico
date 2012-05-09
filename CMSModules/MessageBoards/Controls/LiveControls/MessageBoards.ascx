<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_MessageBoards_Controls_LiveControls_MessageBoards"
    CodeFile="MessageBoards.ascx.cs" %>
<%@ Register Src="~/CMSModules/MessageBoards/Controls/LiveControls/Boards.ascx" TagName="Boards"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/MessageBoards/Controls/Messages/MessageList.ascx"
    TagName="Messages" TagPrefix="cms" %>
    <asp:Panel runat="server" ID="pnlTabsMain" CssClass="TabsPageHeader">
        <asp:Panel runat="server" ID="pnlTabsContainer" CssClass="TabsPageTabs">
            <asp:Panel runat="server" ID="pnlTabs" CssClass="TabsTabs">
                <asp:Panel runat="server" ID="pnlWhite" CssClass="TabsWhite">
                    <cms:BasicTabControl ID="tabElem" runat="server" TabControlLayout="Horizontal" UseClientScript="true"
                        UsePostback="true" />
                </asp:Panel>
            </asp:Panel>
        </asp:Panel>
    </asp:Panel>
    <asp:PlaceHolder ID="tabMessages" runat="server">
        <asp:Panel ID="plcMessages" CssClass="TabBody" runat="server">
            <cms:Messages ID="messages" runat="server" />
        </asp:Panel>
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="tabBoards" runat="server" Visible="false">
        <asp:Panel ID="plcBoards" CssClass="TabBody" runat="server">
            <cms:Boards ID="boards" runat="server" />
        </asp:Panel>
    </asp:PlaceHolder>
