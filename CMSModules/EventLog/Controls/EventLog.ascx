<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EventLog.ascx.cs" Inherits="CMSModules_EventLog_Controls_EventLog" %>
<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/EventLog/Controls/EventFilter.ascx" TagName="EventFilter"
    TagPrefix="cms" %>
<cms:CMSUpdatePanel ID="pnlUpdate" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:PlaceHolder ID="plcFilter" runat="server">
            <cms:EventFilter runat="server" ID="cntFilter" ShortID="f" />
            <br />
        </asp:PlaceHolder>
        <cms:UniGrid runat="server" ID="gridEvents" GridName="~/CMSModules/EventLog/Controls/EventLog.xml"
            OrderBy="EventTime DESC" IsLiveSite="false" ObjectType="cms.eventlog" />
        <asp:HiddenField ID="hdnIdentificator" runat="server" EnableViewState="false" />
    </ContentTemplate>
</cms:CMSUpdatePanel>
