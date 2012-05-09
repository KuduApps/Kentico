<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Content_CMSDesk_Search_default"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Content - Search" CodeFile="default.aspx.cs" %>

<%@ Register Src="~/CMSModules/Content/Controls/SearchDialog.ascx" TagName="SearchDialog"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/SmartSearch/Controls/SearchResults.ascx" TagName="SearchResults"
    TagPrefix="cms" %>
<asp:Content ID="Content1" ContentPlaceHolderID="plcContent" runat="server">
    <div class="MenuBox">
        <cms:CMSUpdatePanel ID="pnlUpdate" runat="server">
            <ContentTemplate>
                <cms:SearchDialog ID="searchDialog" runat="server" />
            </ContentTemplate>
        </cms:CMSUpdatePanel>
        <br />
    </div>
    <asp:Panel runat="server" ID="pnlBody">
        <asp:Panel ID="pnlResultsSQL" runat="server" CssClass="SearchResults">
            <asp:Label ID="lblSearchInfo" runat="server" EnableViewState="false" />
            <cms:CMSSearchResults ID="repSearchSQL" runat="server" Path="/%" CheckPermissions="true" />
            <cms:SearchResults ID="repSmartSearch" runat="server" Path="/%" CheckPermissions="true" />
        </asp:Panel>
    </asp:Panel>

    <script type="text/javascript">
        //<![CDATA[
        // Select item action for transformation
        function SelectItem(nodeId, culture) {
            if (nodeId != 0) {
                parent.parent.location.href = "../../../../cmsdesk/default.aspx?section=content&action=edit&nodeid=" + nodeId + "&culture=" + culture;
            }
        }
        //]]>
    </script>

</asp:Content>
