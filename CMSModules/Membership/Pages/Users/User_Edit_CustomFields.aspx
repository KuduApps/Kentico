<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_Membership_Pages_Users_User_Edit_CustomFields" Theme="Default"
    MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="User edit - Custom fields" CodeFile="User_Edit_CustomFields.aspx.cs" %>

<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <div class="UserCustomFields">
        <cms:LocalizedLabel runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
            Visible="false" ResourceString="General.ChangesSaved" />
            <cms:LocalizedLabel runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
            Visible="false" ResourceString="Administration-User_List.ErrorGlobalAdmin" />
        <asp:PlaceHolder runat="server" ID="plcUserCustomFields"><strong>
            <cms:LocalizedLabel runat="server" ID="lblUserCustomFields" EnableViewState="false"
                ResourceString="adm.user.customfields" />
        </strong>
            <cms:DataForm ID="formUserCustomFields" runat="server" IsLiveSite="false" />
        </asp:PlaceHolder>
        <div style="padding: 15px">
        </div>
        <asp:PlaceHolder runat="server" ID="plcUserSettingsCustomFields"><strong>
            <cms:LocalizedLabel runat="server" ID="lblUserSettingsCustomFields" EnableViewState="false"
                ResourceString="adm.usersettings.customfields" />
        </strong>
            <cms:DataForm ID="formUserSettingsCustomFields" runat="server" IsLiveSite="false" />
        </asp:PlaceHolder>
    </div>
</asp:Content>
