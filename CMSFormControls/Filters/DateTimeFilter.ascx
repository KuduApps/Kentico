<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DateTimeFilter.ascx.cs"
    Inherits="CMSFormControls_Filters_DateTimeFilter" %>
<cms:LocalizedLabel ID="lblGreater" runat="server" ResourceString="general.between"
    EnableViewState="false" />
<cms:DateTimePicker ID="dtmTimeFrom" runat="server" />
<cms:LocalizedLabel ID="lblTimeBetweenAnd" runat="server" ResourceString="general.and" />
<cms:DateTimePicker ID="dtmTimeTo" runat="server" />
