<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_Forums_Controls_LiveControls_Groups" CodeFile="Groups.ascx.cs" %>
<%@ Register src="~/CMSAdminControls/UI/PageElements/HeaderActions.ascx" tagname="HeaderActions" tagprefix="cms" %>
<%@ Register Src="~/CMSModules/Forums/Controls/LiveControls/Group.ascx" TagName="GroupEdit" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/Forums/Controls/Groups/GroupNew.ascx" TagName="GroupNew" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/Forums/Controls/Forums/ForumGroupList.ascx" TagName="GroupList" TagPrefix="cms" %>
<asp:Panel ID="pnlBody" runat="server" CssClass="PageBody">
    <asp:PlaceHolder ID="plcList" runat="server">
        <asp:Panel ID="pnlListActions" runat="server">
            <cms:HeaderActions ID="actionsElem" runat="server" />
        </asp:Panel>
        <asp:Panel ID="pnlListContent" runat="server">
            <cms:GroupList ID="groupList" runat="server" />
        </asp:Panel>
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="plcEdit" runat="server" >
        <asp:Panel ID="pnlEditHeader" runat="server" CssClass="PageHeaderLine">
            <asp:LinkButton ID="lnkEditBack" runat="server" CausesValidation="false" /><asp:Label
                ID="lblEditBack" runat="server" /><br />
        </asp:Panel>
        <cms:GroupEdit ID="groupEdit" runat="server" />
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="plcNew" runat="server" >
        <asp:Panel ID="pnlNewHeader" runat="server" CssClass="PageHeaderLine">
            <asp:LinkButton ID="lnkNewBack" runat="server" CausesValidation="false" /><asp:Label
                ID="lblNewBack" runat="server" /><br />
        </asp:Panel>
        <asp:Panel ID="pnlNewContent" runat="server">
            <cms:GroupNew ID="groupNew" runat="server" />
        </asp:Panel>
    </asp:PlaceHolder>
</asp:Panel>
