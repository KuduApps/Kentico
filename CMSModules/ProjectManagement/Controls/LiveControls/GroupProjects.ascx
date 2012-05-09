<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_ProjectManagement_Controls_LiveControls_GroupProjects" CodeFile="GroupProjects.ascx.cs" %>
<%@ Register Src="~/CMSModules/ProjectManagement/Controls/UI/Project/List.ascx" TagName="ProjectList"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/ProjectManagement/Controls/LiveControls/GroupProjectEdit.ascx"
    TagName="ProjectEdit" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/ProjectManagement/Controls/UI/Project/Edit.ascx" TagName="ProjectNew"
    TagPrefix="cms" %>    
<%@ Register Src="~/CMSAdminControls/UI/PageElements/HeaderActions.ascx" TagName="HeaderActions"
    TagPrefix="cms" %>
<asp:Panel ID="pnlBody" runat="server" CssClass="PageBody">
    <asp:PlaceHolder ID="plcList" runat="server">
        <asp:Panel ID="pnlListActions" runat="server">
            <cms:HeaderActions ID="actionsElem" runat="server" />
        </asp:Panel>
        <asp:Panel ID="pnlListContent" runat="server">
            <cms:ProjectList ID="ucProjectList" runat="server" />
        </asp:Panel>
    </asp:PlaceHolder>
</asp:Panel>
<asp:PlaceHolder ID="plcEdit" runat="server">
    <asp:Panel ID="pnlEditHeader" runat="server" CssClass="PageHeaderLine">
        <asp:LinkButton ID="lnkEditBack" runat="server" CausesValidation="false" /><asp:Label runat="server" ID="lblEditBack" /><br />
    </asp:Panel>
    <cms:ProjectEdit ID="ucProjectEdit" runat="server" />    
</asp:PlaceHolder>
<asp:PlaceHolder ID="plcNew" runat="server">
    <asp:Panel ID="pnlNewHeader" runat="server" CssClass="PageHeaderLine">
        <asp:LinkButton ID="lnkNewBack" runat="server" CausesValidation="false" /><asp:Label runat="server" ID="lblNewBack" /><br />
    </asp:Panel>
    <cms:ProjectNew ID="ucProjectNew" runat="server" />    
</asp:PlaceHolder>