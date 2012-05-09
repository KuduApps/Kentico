<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LargeTextArea.ascx.cs" Inherits="CMSFormControls_Inputs_LargeTextArea" %>

<cms:ExtendedTextArea ID="txtArea" runat="server" Width="256px" Height="50px" CssClass="LargeTextAreaTextBox" />
<cms:CMSButton ID="btnMore" runat="server" Text="..." CssClass="XShortButton LargeTextAreaButton" EnableViewState="false" />   