<%@ Page Language="C#" AutoEventWireup="true" Theme="Default" CodeFile="Analytics_DataManagement.aspx.cs"
    Inherits="CMSModules_WebAnalytics_Tools_Analytics_DataManagement" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="plcBeforeContent">
    <asp:Panel runat="server" ID="pnlDisabled" CssClass="PageHeaderLine" Visible="false">
        <asp:Label runat="server" ID="lblDisabled" EnableViewState="false" />
    </asp:Panel>
</asp:Content>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <cms:CMSUpdatePanel ID="pnlUpdate" runat="server">
        <ContentTemplate>
            <cms:LocalizedLabel runat="server" ID="lblError" EnableViewState="false" Visible="false"
                CssClass="ErrorLabel"></cms:LocalizedLabel>
            <cms:LocalizedLabel runat="server" ID="lblInfo" EnableViewState="false" Visible="false"
                CssClass="InfoLabel"></cms:LocalizedLabel>
            <asp:Timer ID="timeRefresh" runat="server" Interval="3000" EnableViewState="false" Enabled="false" />
            <asp:Literal runat="server" ID="ltrProgress" EnableViewState="false" />
            <asp:Panel runat="server" ID="pnlRemoveData">
                <table>
                    <tr>
                        <td>
                            <cms:LocalizedLabel runat="server" ID="lblDelete" ResourceString="analytics.statisticslist.columns.codename"
                                DisplayColon="true"></cms:LocalizedLabel>
                        </td>
                        <td>
                            <cms:LocalizedDropDownList AutoPostBack="true" CssClass="DropDownField" runat="server"
                                ID="drpDeleteObjects">
                            </cms:LocalizedDropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <cms:LocalizedLabel runat="server" ID="lblInterval" ResourceString="general.avaibledata"
                                DisplayColon="true"></cms:LocalizedLabel>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblIntervalInfo" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <cms:LocalizedLabel runat="server" ID="lblDeleteFrom" ResourceString="general.from"
                                DisplayColon="true" />
                        </td>
                        <td>
                            <cms:DateTimePicker runat="server" ID="ucDeleteFrom" EditTime="false" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <cms:LocalizedLabel runat="server" ID="lblDeleteTo" ResourceString="general.to" DisplayColon="true" />
                        </td>
                        <td>
                            <cms:DateTimePicker runat="server" ID="ucDeleteTo" EditTime="false" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <cms:LocalizedButton runat="server" ID="btnDelete" CssClass="SubmitButton" ResourceString="analyt.settings.deletebtn"
                                OnClick="btnDelete_Click" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <br />
            <br />
            <asp:Panel runat="server" ID="pnlGenerateData">
                <table>
                    <tr>
                        <td>
                            <cms:LocalizedLabel runat="server" ID="lblSampleDataObject" ResourceString="analytics.statisticslist.columns.codename"
                                DisplayColon="true"></cms:LocalizedLabel>
                        </td>
                        <td>
                            <cms:LocalizedDropDownList CssClass="DropDownField" runat="server" ID="drpGenerateObejcts">
                            </cms:LocalizedDropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <cms:LocalizedLabel runat="server" ID="lblFrom" ResourceString="general.from" DisplayColon="true" />
                        </td>
                        <td>
                            <cms:DateTimePicker runat="server" ID="ucSampleFrom" EditTime="false" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <cms:LocalizedLabel runat="server" ID="lblTo" ResourceString="general.to" DisplayColon="true" />
                        </td>
                        <td>
                            <cms:DateTimePicker runat="server" ID="ucSampleTo" EditTime="false" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <cms:LocalizedButton runat="server" ID="btnGenerate" CssClass="SubmitButton" ResourceString="analyt.settings.generatebtn"
                                OnClick="btnGenerate_Click" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
    </cms:CMSUpdatePanel>
</asp:Content>
