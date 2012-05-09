<%@ Page Language="C#" AutoEventWireup="true"
    MasterPageFile="~/CMSMasterPages/UI/Dialogs/ModalDialogPage.master" Title="EventLog - Details"
    Inherits="CMSModules_EventLog_EventLog_Details" Theme="Default" CodeFile="EventLog_Details.aspx.cs" %>

<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <div class="PageContent">
        <table border="0" cellpadding="0" cellspacing="5" class="tblEventLogDetail">
            <tr>
                <td>
                    <cms:LocalizedLabel ID="lblEventID" runat="server" ResourceString="EventLogDetails.EventID"
                        EnableViewState="false" />
                </td>
                <td>
                    <asp:Label ID="lblEventIDValue" runat="server" CssClass="FieldLabel" EnableViewState="false" />
                </td>
            </tr>
            <tr>
                <td>
                    <cms:LocalizedLabel ID="lblEventType" runat="server" ResourceString="EventLogDetails.EventType"
                        EnableViewState="false" />
                </td>
                <td>
                    <asp:Label ID="lblEventTypeValue" runat="server" EnableViewState="false" />
                </td>
            </tr>
            <tr>
                <td>
                    <cms:LocalizedLabel ID="lblEventTime" runat="server" ResourceString="EventLogDetails.EventTime"
                        EnableViewState="false" />
                </td>
                <td>
                    <asp:Label ID="lblEventTimeValue" runat="server" EnableViewState="false" />
                </td>
            </tr>
            <tr>
                <td>
                    <cms:LocalizedLabel ID="lblSource" runat="server" ResourceString="EventLogDetails.Source"
                        EnableViewState="false" />
                </td>
                <td>
                    <asp:Label ID="lblSourceValue" runat="server" EnableViewState="false" />
                </td>
            </tr>
            <tr>
                <td>
                    <cms:LocalizedLabel ID="lblEventCode" runat="server" ResourceString="EventLogDetails.EventCode"
                        EnableViewState="false" />
                </td>
                <td>
                    <asp:Label ID="lblEventCodeValue" runat="server" EnableViewState="false" />
                </td>
            </tr>
            <asp:PlaceHolder runat="server" ID="plcUserID" EnableViewState="false">
                <tr>
                    <td>
                        <cms:LocalizedLabel ID="lblUserID" runat="server" ResourceString="EventLogDetails.UserID"
                            EnableViewState="false" />
                    </td>
                    <td>
                        <asp:Label ID="lblUserIDValue" runat="server" CssClass="FieldLabel" EnableViewState="false" />
                    </td>
                </tr>
            </asp:PlaceHolder>
            <asp:PlaceHolder runat="server" ID="plcUserName" EnableViewState="false">
                <tr>
                    <td>
                        <cms:LocalizedLabel ID="lblUserName" runat="server" ResourceString="general.username"
                            EnableViewState="false" DisplayColon="true" />
                    </td>
                    <td>
                        <asp:Label ID="lblUserNameValue" runat="server" CssClass="FieldLabel" EnableViewState="false" />
                    </td>
                </tr>
            </asp:PlaceHolder>
            <tr>
                <td>
                    <cms:LocalizedLabel ID="lblIPAddress" runat="server" ResourceString="EventLogDetails.IPAddress"
                        EnableViewState="false" />
                </td>
                <td>
                    <asp:Label ID="lblIPAddressValue" runat="server" EnableViewState="false" />
                </td>
            </tr>
            <asp:PlaceHolder runat="server" ID="plcNodeID" EnableViewState="false">
                <tr>
                    <td>
                        <cms:LocalizedLabel ID="lblNodeID" runat="server" ResourceString="EventLogDetails.NodeID"
                            EnableViewState="false" />
                    </td>
                    <td>
                        <asp:Label ID="lblNodeIDValue" runat="server" EnableViewState="false" />
                    </td>
                </tr>
            </asp:PlaceHolder>
            <asp:PlaceHolder runat="server" ID="plcNodeName" EnableViewState="false">
                <tr>
                    <td>
                        <cms:LocalizedLabel ID="lblNodeName" runat="server" ResourceString="EventLogDetails.NodeName"
                            EnableViewState="false" />
                    </td>
                    <td>
                        <asp:Label ID="lblNodeNameValue" runat="server" EnableViewState="false" />
                    </td>
                </tr>
            </asp:PlaceHolder>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    <cms:LocalizedLabel ID="lblEventDescription" runat="server" ResourceString="general.description"
                        DisplayColon="true" EnableViewState="false" />
                </td>
                <td>
                    <asp:Label ID="lblEventDescriptionValue" runat="server" EnableViewState="false" />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                </td>
            </tr>
            <asp:PlaceHolder runat="server" ID="plcSite" EnableViewState="false">
                <tr>
                    <td>
                        <cms:LocalizedLabel ID="lblSiteName" runat="server" ResourceString="general.sitename"
                            DisplayColon="true" EnableViewState="false" />
                    </td>
                    <td>
                        <asp:Label ID="lblSiteNameValue" runat="server" EnableViewState="false" />
                    </td>
                </tr>
            </asp:PlaceHolder>
            <tr>
                <td>
                    <cms:LocalizedLabel ID="lblMachineName" runat="server" ResourceString="EventLogDetails.MachineName"
                        EnableViewState="false" />
                </td>
                <td>
                    <asp:Label ID="lblMachineNameValue" runat="server" EnableViewState="false" />
                </td>
            </tr>
            <tr>
                <td>
                    <cms:LocalizedLabel ID="lblEventUrl" runat="server" ResourceString="EventLogDetails.EventUrl"
                        EnableViewState="false" />
                </td>
                <td>
                    <asp:Label ID="lblEventUrlValue" runat="server" EnableViewState="false" />
                </td>
            </tr>
            <tr>
                <td>
                    <cms:LocalizedLabel ID="lblUrlReferrer" runat="server" ResourceString="EventLogDetails.UrlReferrer"
                        EnableViewState="false" DisplayColon="true" />
                </td>
                <td>
                    <asp:Label ID="lblUrlReferrerValue" runat="server" EnableViewState="false" />
                </td>
            </tr>
            <tr>
                <td>
                    <cms:LocalizedLabel ID="lblUserAgent" runat="server" ResourceString="EventLogDetails.UserAgent"
                        EnableViewState="false" DisplayColon="true" />
                </td>
                <td>
                    <asp:Label ID="lblUserAgentValue" runat="server" EnableViewState="false" />
                </td>
            </tr>            
        </table>
    </div>
</asp:Content>
<asp:Content ID="cntFooter" ContentPlaceHolderID="plcFooter" runat="server">
    <div class="FloatLeft">
        <table cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <cms:CMSButton runat="server" ID="btnPrevious" CssClass="SubmitButton" Enabled="false"
                        EnableViewState="false" /><cms:CMSButton runat="server" ID="btnNext" CssClass="SubmitButton"
                            Enabled="false" EnableViewState="false" />
                </td>
                <td>&nbsp;&nbsp;&nbsp;</td>
                <td>
                    <asp:HyperLink ID="lnkExport" runat="server" Visible="false" />
                </td>
            </tr>
        </table>
    </div>
    <div class="FloatRight">
        <cms:LocalizedButton ID="btnClose" runat="server" CssClass="SubmitButton" ResourceString="general.close"
            OnClientClick="window.close(); return false;" EnableViewState="false" />
    </div>
</asp:Content>
