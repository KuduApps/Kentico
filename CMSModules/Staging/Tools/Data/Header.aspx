<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Staging_Tools_Data_Header"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/EmptyPage.master" CodeFile="Header.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/PageElements/PageTitle.ascx" TagName="PageTitle" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/Staging/FormControls/ServerSelector.ascx" TagName="ServerSelector" TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/UI/PageElements/FrameResizer.ascx" TagName="FrameResizer" TagPrefix="cms" %>
<asp:Content ID="plcContent" runat="server" ContentPlaceHolderID="plcContent">
    <cms:FrameResizer ID="frmResizer" runat="server" MinSize="6, *" Vertical="True" CssPrefix="Vertical" />
    <asp:Panel ID="pnlTitle" runat="server" CssClass="PageHeader">
        <cms:PageTitle ID="titleElem" runat="server" HelpTopicName="staging_data" EnableViewState="false" />
    </asp:Panel>
    <asp:Panel ID="pnlServers" runat="server" CssClass="PageHeaderLine SiteHeaderLine">
        <table style="width: 100%;" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <cms:LocalizedLabel ID="lblServers" runat="server" EnableViewState="false" ResourceString="Tasks.SelectServer" />&nbsp;
                </td>
                <td style="width: 100%;">
                    <cms:ServerSelector ID="selectorElem" runat="server" IsLiveSite="false" />
                </td>
                <td class="TextRight" style="white-space: nowrap;">
                    <asp:Panel runat="server" ID="pnlComplete">
                        <asp:Image ID="imgComplete" runat="server" CssClass="NewItemImage" />
                        <cms:LocalizedLinkButton ID="btnComplete" runat="server" CssClass="NewItemLink" OnClientClick="return CompleteSync();"
                            EnableViewState="false" ResourceString="Tasks.CompleteSync" />
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <div class="HeaderSeparator">
    </div>

    <script type="text/javascript">
        //<![CDATA[

        var currentServerId = 0;
        var currentNodeId = '';

        function ChangeServer(value) {
            currentServerId = value;
            SelectNode(currentNodeId);
        }

        function SelectNode(nodeId) {
            currentNodeId = nodeId;
            parent.frames['tasksContent'].location = 'Tasks.aspx?serverid=' + currentServerId + '&objecttype=' + nodeId;
        }

        function CompleteSync() {
            parent.frames['tasksContent'].CompleteSync();
            return false;
        }
        //]]>
    </script>

    <%--<asp:Literal ID="ltlScript" runat="server" EnableViewState="false" />--%>
</asp:Content>
