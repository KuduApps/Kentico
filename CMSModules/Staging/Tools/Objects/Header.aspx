<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Staging_Tools_Objects_Header"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/EmptyPage.master" CodeFile="Header.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/PageElements/PageTitle.ascx" TagName="PageTitle" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/Staging/FormControls/ServerSelector.ascx" TagName="ServerSelector" TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/UI/PageElements/FrameResizer.ascx" TagName="FrameResizer" TagPrefix="cms" %>
<asp:Content ID="plcContent" runat="server" ContentPlaceHolderID="plcContent">
    <cms:FrameResizer ID="frmResizer" runat="server" MinSize="6, *" Vertical="True" CssPrefix="Vertical" />
    <asp:Panel ID="pnlTitle" runat="server" CssClass="PageHeader">
        <cms:PageTitle ID="titleElem" runat="server" HelpTopicName="staging_objects" EnableViewState="false" />
    </asp:Panel>
    <asp:Panel ID="pnlServers" runat="server" CssClass="PageHeaderLine SiteHeaderLine">
        <table cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <cms:LocalizedLabel ID="lblServers" runat="server" EnableViewState="false" ResourceString="Tasks.SelectServer" />&nbsp;
                </td>
                <td>
                    <cms:ServerSelector ID="selectorElem" runat="server" IsLiveSite="false" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <div class="HeaderSeparator">
    </div>

    <script type="text/javascript">
        //<![CDATA[
        var serversElem = document.getElementById('<%=selectorElem.DropDownList.ClientID %>');
        var currentServerId = 0;
        var currentNodeId = '';
        var currentSiteId = -1;

        function ChangeServer(value) {
            currentServerId = value;
            SelectNode(currentNodeId, currentSiteId);
        }

        function SelectNode(nodeId, siteId) {
            currentNodeId = nodeId;
            currentSiteId = siteId;
            parent.frames['tasksContent'].location = 'Tasks.aspx?serverid=' + currentServerId + '&objecttype=' + nodeId + '&siteid=' + siteId;
        }
        //]]>
    </script>

    <asp:Literal ID="ltlScript" runat="server" EnableViewState="false" />
</asp:Content>
