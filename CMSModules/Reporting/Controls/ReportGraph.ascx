<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_Reporting_Controls_ReportGraph"
    CodeFile="ReportGraph.ascx.cs" %>
<div id="graphDiv" runat="server" class="ReportGraphDiv">
    <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
        Visible="false" />
    <cms:ContextMenuContainer runat="server" ID="menuCont" MenuID="">
        <asp:Chart runat="server" ID="ucChart" EnableViewState="false" />
        <asp:HiddenField ID="hdnValues" runat="server" EnableViewState="true" />
        <asp:Button runat="server" ID="btnRefresh" />
        <asp:Image runat="server" ID="imgGraph" AlternateText="Report graph" EnableViewState="false"
            Visible="false" />
        <asp:Label runat="server" ID="lblError" EnableViewState="false" CssClass="InlineControlError"
            Visible="false" />
    </cms:ContextMenuContainer>
</div>
