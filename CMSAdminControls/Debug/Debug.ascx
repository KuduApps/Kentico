<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSAdminControls_Debug_Debug"
    CodeFile="Debug.ascx.cs" %>
<%@ Register Src="~/CMSAdminControls/Debug/QueryLog.ascx" TagName="QueryLog" TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/Debug/CacheLog.ascx" TagName="CacheLog" TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/Debug/FilesLog.ascx" TagName="FilesLog" TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/Debug/ViewState.ascx" TagName="ViewState" TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/Debug/SecurityLog.ascx" TagName="SecurityLog"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/Debug/MacroLog.ascx" TagName="MacroLog" TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/Debug/AnalyticsLog.ascx" TagName="AnalyticsLog" TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/Debug/RequestLog.ascx" TagName="RequestLog"
    TagPrefix="cms" %>
<!--LOG-->
<div>
    <cms:QueryLog ID="logSQL" runat="server" EnableViewState="false" />
    <cms:CacheLog ID="logCache" runat="server" EnableViewState="false" />
    <cms:FilesLog ID="logFiles" runat="server" EnableViewState="false" />
    <cms:ViewState ID="logState" runat="server" EnableViewState="false" />
    <cms:SecurityLog ID="logSec" runat="server" EnableViewState="false" />
    <cms:MacroLog ID="logMac" runat="server" EnableViewState="false" />
    <cms:AnalyticsLog ID="logAn" runat="server" EnableViewState="false" />
    <cms:RequestLog ID="logReq" runat="server" EnableViewState="false" />
</div>
<!--LOGEND-->
