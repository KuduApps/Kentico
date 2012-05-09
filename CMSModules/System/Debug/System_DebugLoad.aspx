<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_System_Debug_System_DebugLoad"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Group list"
    MaintainScrollPositionOnPostback="true" CodeFile="System_DebugLoad.aspx.cs" %>

<%@ Register Src="~/CMSModules/Membership/FormControls/Users/SelectUserName.ascx"
    TagName="UserSelector" TagPrefix="cms" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">

    <script type="text/javascript">
        //<![CDATA[
        function alert() {
        }
        //]]>
    </script>

    <ajaxToolkit:ToolkitScriptManager runat="server" ID="manScript" ScriptMode="Release" />
    <cms:CMSUpdatePanel runat="server" ID="pnlInfo">
        <ContentTemplate>
            <asp:Label ID="lblInfo" runat="server" CssClass="InfoLabel" EnableViewState="false" />
            <asp:Label ID="lblError" runat="server" CssClass="ErrorLabel" EnableViewState="false" />
            <asp:Timer runat="server" ID="timRefresh" Interval="1000" Enabled="true" />
            <br />
        </ContentTemplate>
    </cms:CMSUpdatePanel>
    <cms:CMSUpdatePanel runat="server" ID="pnlBody" UpdateMode="Conditional">
        <ContentTemplate>
            <table>
                <tr>
                    <td>
                        <cms:LocalizedLabel runat="server" ID="lblThreads" EnableViewState="false" ResourceString="DebugLoad.Threads" />
                    </td>
                    <td>
                        <cms:CMSTextBox runat="server" CssClass="ShortTextBox" ID="txtThreads" Text="10" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <cms:LocalizedLabel runat="server" ID="lblIterations" EnableViewState="false" ResourceString="DebugLoad.Iterations"
                            DisplayColon="true" />
                    </td>
                    <td>
                        <cms:CMSTextBox runat="server" CssClass="ShortTextBox" ID="txtIterations" Text="1000" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <cms:LocalizedLabel runat="server" ID="lblDuration" EnableViewState="false" ResourceString="DebugLoad.Duration" />
                    </td>
                    <td>
                        <cms:CMSTextBox runat="server" CssClass="ShortTextBox" ID="txtDuration" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <cms:LocalizedLabel runat="server" ID="lblInterval" EnableViewState="false" ResourceString="DebugLoad.Interval" />
                    </td>
                    <td>
                        <cms:CMSTextBox runat="server" CssClass="ShortTextBox" ID="txtInterval" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <cms:LocalizedLabel runat="server" ID="lblUserName" EnableViewState="false" ResourceString="DebugLoad.UserName"
                            DisplayColon="true" />
                    </td>
                    <td>
                        <cms:UserSelector runat="server" ID="userElem" AllowEmpty="true" AllowAll="false"
                            IsLiveSite="false" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <cms:LocalizedLabel runat="server" ID="lblSplitUrls" EnableViewState="false" ResourceString="DebugLoad.SplitUrls"
                            DisplayColon="true" />
                    </td>
                    <td>
                        <asp:CheckBox runat="server" ID="chkSplitUrls" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <cms:LocalizedLabel runat="server" ID="lblUserAgent" EnableViewState="false" ResourceString="DebugLoad.UserAgent"
                            DisplayColon="true" />
                    </td>
                    <td>
                        <cms:CMSTextBox runat="server" CssClass="TextAreaField" TextMode="MultiLine" ID="txtUserAgent"
                            Text="Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.1; WOW64; Trident/5.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; InfoPath.2; .NET4.0C; .NET4.0E; MS-RTC LM 8)" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <cms:LocalizedLabel runat="server" ID="lblURLs" EnableViewState="false" ResourceString="DebugLoad.URLs" />
                    </td>
                    <td>
                        <cms:CMSTextBox runat="server" CssClass="TextAreaField" ID="txtURLs" TextMode="MultiLine"
                            Text="~/Home.aspx" />
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <cms:CMSButton runat="server" ID="btnStart" CssClass="LongSubmitButton" OnClick="btnStart_Click" /><cms:CMSButton
                            runat="server" ID="btnStop" CssClass="LongSubmitButton" OnClick="btnStop_Click" /><cms:CMSButton
                                runat="server" ID="btnReset" CssClass="LongSubmitButton" OnClick="btnReset_Click" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </cms:CMSUpdatePanel>
</asp:Content>
