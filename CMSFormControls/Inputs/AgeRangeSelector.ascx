<%@ Control Language="C#" AutoEventWireup="true" CodeFile="~/CMSFormControls/Inputs/AgeRangeSelector.ascx.cs"
    Inherits="CMSFormControls_Inputs_AgeRangeSelector" %>
<cms:LocalizedLabel runat="server" ID="lblBetween" ResourceString="ageselector.between" />
<cms:CMSTextBox runat="server" ID="txtBetween" CssClass="ShortTextBox" />
<cms:LocalizedLabel runat="server" ID="lbland" ResourceString="eventlog.timebetweenand" />
<cms:CMSTextBox runat="server" ID="txtDays" CssClass="ShortTextBox" />
<cms:LocalizedLabel runat="server" ID="lblDays" ResourceString="header.trialexpiresdays" />