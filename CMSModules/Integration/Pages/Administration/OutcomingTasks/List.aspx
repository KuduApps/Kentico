<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master"
    Title="Integration task list" Inherits="CMSModules_Integration_Pages_Administration_OutcomingTasks_List"
    Theme="Default" CodeFile="List.aspx.cs" %>

<%@ Register Src="~/CMSModules/Integration/Controls/UI/IntegrationTask/List.ascx"
    TagName="IntegrationTaskList" TagPrefix="cms" %>
<%@ Register TagPrefix="cms" TagName="ConnectorSelector" Src="~/CMSModules/Integration/FormControls/ConnectorSelector.ascx" %>
<asp:Content runat="server" ID="cntSiteSelector" ContentPlaceHolderID="plcSiteSelector">
    <table>
        <tr>
            <td>
                <cms:LocalizedLabel ID="lblConnector" runat="server" ResourceString="integration.connector"
                    EnableViewState="false" DisplayColon="true" />
            </td>
            <td>
                <cms:ConnectorSelector ID="connectorSelector" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <cms:IntegrationTaskList ID="listElem" runat="server" IsLiveSite="false" TasksAreInbound="false" />
</asp:Content>
