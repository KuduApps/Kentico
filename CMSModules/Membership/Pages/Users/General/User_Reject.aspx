<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Membership_Pages_Users_General_User_Reject"
    MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Theme="Default" CodeFile="User_Reject.aspx.cs" %>

<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:Literal ID="ltlScript1" runat="server" />
    <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
        Visible="false" />
    <cms:LocalizedLabel ID="lblReason" runat="server" DisplayColon="true" ResourceString="administration.users.reason" /><br />
    <cms:CMSTextBox ID="txtReason" runat="server" CssClass="TextAreaLarge" TextMode="MultiLine"
        MaxLength="1000" />
    <br />
    <div>
        <cms:LocalizedCheckBox ID="chkSendEmail" runat="server" Checked="true" ResourceString="administration.users.email" /></div>
    <br />
    <cms:LocalizedButton ID="btnReject" runat="server" CssClass="ContentButton" ResourceString="general.reject" />
    <cms:LocalizedButton ID="btnCancel" runat="server" CssClass="ContentButton" ResourceString="general.cancel" />
    <asp:Literal ID="ltlScript" runat="server" />
</asp:Content>
