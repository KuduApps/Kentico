<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSFormControls_System_LinkDialogSelector" CodeFile="LinkDialogSelector.ascx.cs" %>
<cms:localizedradiobutton id="radUrlNo" runat="server" resourcestring="general.no"
    groupname="EnableURL" />
&nbsp;
<cms:localizedradiobutton id="radUrlSimple" runat="server" resourcestring="forum.settings.simpledialog"
    groupname="EnableURL" />
&nbsp;
<cms:localizedradiobutton id="radUrlAdvanced" runat="server" resourcestring="forum.settings.advanceddialog"
    groupname="EnableURL" />
