<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSFormControls_Inputs_USphone" CodeFile="USphone.ascx.cs" %>
(<cms:CMSTextBox runat="server" ID="txt1st" MaxLength="3" Width="25" />)&nbsp;<cms:CMSTextBox
    runat="server" ID="txt2nd" MaxLength="3" Width="25" />-<cms:CMSTextBox runat="server"
        ID="txt3rd" MaxLength="4" Width="30" /><cms:LocalizedLabel ID="lbl2nd" AssociatedControlID="txt2nd"
            EnableViewState="false" runat="server" ResourceString="USPhone.2nd" /><cms:LocalizedLabel ID="lbl3rd"
                AssociatedControlID="txt3rd" EnableViewState="false" ResourceString="USPhone.3rd" runat="server" />