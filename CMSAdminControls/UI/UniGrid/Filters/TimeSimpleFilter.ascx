<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSAdminControls_UI_UniGrid_Filters_TimeSimpleFilter" CodeFile="TimeSimpleFilter.ascx.cs" %>
<cms:DateTimePicker ID="dtmTimeFrom" runat="server" />
<cms:LocalizedLabel ID="lblTimeBetweenAnd" runat="server" ResourceString="eventlog.timebetweenand"
    style="padding: 0px 7px;" />
<cms:DateTimePicker ID="dtmTimeTo" runat="server" />
