<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_Forums_Tools_Subscriptions_ForumSubscription_New" Theme="Default"
    MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" CodeFile="ForumSubscription_New.aspx.cs" %>

<asp:Content ContentPlaceHolderID="plcContent" ID="content" runat="server">
    <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
        Visible="false" />
    <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
        Visible="false" />
    <br />
    <table style="vertical-align: top">
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel runat="server" ID="lblSubscriptionEmail" EnableViewState="false"
                    ResourceString="general.email" DisplayColon="true" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtSubscriptionEmail" runat="server" CssClass="TextBoxField" MaxLength="100" />
                <cms:CMSRequiredFieldValidator ID="rfvSubscriptionEmail" runat="server" ErrorMessage=""
                    ControlToValidate="txtSubscriptionEmail"></cms:CMSRequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <br />
                <cms:CMSButton runat="server" ID="btnOk" OnClick="btnOK_Click" EnableViewState="false"
                    CssClass="ContentButton" />
            </td>
        </tr>
    </table>
</asp:Content>
