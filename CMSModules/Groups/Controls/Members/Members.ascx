<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_Groups_Controls_Members_Members" CodeFile="Members.ascx.cs" %>
<%@ Register src="~/CMSAdminControls/UI/PageElements/HeaderActions.ascx" tagname="HeaderActions" tagprefix="cms" %>
<%@ Register Src="~/CMSModules/Groups/Controls/Members/MemberEdit.ascx" TagName="MemberEdit" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/Groups/Controls/Members/MemberList.ascx" TagName="MemberList" TagPrefix="cms" %>
<asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
    Visible="false" />
<asp:PlaceHolder ID="plcList" runat="server">
    <cms:HeaderActions ID="actionsElem" runat="server" />
    <cms:MemberList ID="memberListElem" runat="server" />
</asp:PlaceHolder>
<asp:PlaceHolder ID="plcEdit" runat="server" Visible="false">
    <asp:Panel ID="pnlEditActions" runat="server" CssClass="PageHeaderLine">
        <asp:LinkButton ID="lnkEditBack" runat="server" CausesValidation="false" /><asp:Label
            ID="lblEditBack" runat="server" />
    </asp:Panel>
    <cms:MemberEdit ID="memberEditElem" runat="server" />
</asp:PlaceHolder>
<asp:Literal ID="ltlScript" runat="server" />