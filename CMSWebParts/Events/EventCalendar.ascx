<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSWebParts_Events_EventCalendar" CodeFile="~/CMSWebParts/Events/EventCalendar.ascx.cs" %>
<div class="Calendar">
    <cms:CMSCalendar ID="calItems" runat="server" EnableViewState="false" />
</div>
<div class="EventDetail">
    <cms:CMSRepeater ID="repEvent" runat="server" Visible="false" StopProcessing="true" EnableViewState="false" />
</div>
