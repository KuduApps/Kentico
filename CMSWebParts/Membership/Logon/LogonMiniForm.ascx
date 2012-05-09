<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSWebParts_Membership_Logon_LogonMiniForm" CodeFile="~/CMSWebParts/Membership/Logon/LogonMiniForm.ascx.cs" %>
<asp:Login ID="loginElem" runat="server" DestinationPageUrl="~/Default.aspx" EnableViewState="false">
    <LayoutTemplate>
        <asp:Panel ID="pnlLogonMiniForm" runat="server" DefaultButton="btnLogon" EnableViewState="false">
            <cms:LocalizedLabel ID="lblUserName" runat="server" AssociatedControlID="UserName" EnableViewState="false" />
            <cms:CMSTextBox ID="UserName" runat="server" CssClass="LogonField" EnableViewState="false" />
            <cms:CMSRequiredFieldValidator ID="rfvUserNameRequired" runat="server" ControlToValidate="UserName"
                 Display="Dynamic" EnableViewState="false">*</cms:CMSRequiredFieldValidator>
            <cms:LocalizedLabel ID="lblPassword" runat="server" AssociatedControlID="Password" EnableViewState="false" />
            <cms:CMSTextBox ID="Password" runat="server" TextMode="Password" CssClass="LogonField" EnableViewState="false" />
            <cms:LocalizedButton ID="btnLogon" runat="server" ResourceString="LogonForm.LogOnButton" CommandName="Login"  EnableViewState="false" />
            <asp:ImageButton ID="btnImageLogon" runat="server" Visible="false" CommandName="Login"
                EnableViewState="false" />
            <asp:Label ID="FailureText" CssClass="ErrorLabel" runat="server" EnableViewState="false" />
        </asp:Panel>
    </LayoutTemplate>
</asp:Login>
