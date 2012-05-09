<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Membership_Pages_Users_User_Edit_Settings"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="User edit - Custom fields"
    CodeFile="User_Edit_Settings.aspx.cs" %>

<%@ Register Src="~/CMSModules/Membership/FormControls/Users/UserPictureEdit.ascx"
    TagName="UserPictureFormControl" TagPrefix="upfc" %>
<%@ Register Src="~/CMSFormControls/TimeZones/TimeZoneSelector.ascx" TagName="TimeZoneSelector"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/Badges/FormControls/BadgeSelector.ascx" TagName="BadgeSelector"
    TagPrefix="cms" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
        Visible="false" />
    <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
        Visible="false" />
    <asp:PlaceHolder ID="plcTable" runat="server">
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblNickName" runat="server" EnableViewState="false" />
                </td>
                <td>
                    <cms:CMSTextBox ID="txtNickName" MaxLength="200" runat="server" CssClass="TextBoxField" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblUserPicture" runat="server" EnableViewState="false" />
                </td>
                <td>
                    <upfc:UserPictureFormControl ID="UserPictureFormControl" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblUserSignature" runat="server" EnableViewState="false" />
                </td>
                <td>
                    <cms:CMSTextBox runat="server" ID="txtUserSignature" CssClass="TextAreaField" TextMode="MultiLine" />
                </td>
            </tr>
            <tr>
                <td>
                    <cms:LocalizedLabel ID="lblUserDescription" runat="server" EnableViewState="false"
                        ResourceString="Administration-User_Edit_General.UserDescription" DisplayColon="true" />
                </td>
                <td>
                    <cms:CMSTextBox runat="server" ID="txtUserDescription" CssClass="TextAreaField" TextMode="MultiLine" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblURLReferrer" runat="server" EnableViewState="false" />
                </td>
                <td>
                    <cms:CMSTextBox ID="txtURLReferrer" MaxLength="450" runat="server" CssClass="TextBoxField" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblCampaign" runat="server" EnableViewState="false" />
                </td>
                <td>
                    <cms:CMSTextBox ID="txtCampaign" MaxLength="200" runat="server" CssClass="TextBoxField" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblMessageNotifEmail" runat="server" EnableViewState="false" />
                </td>
                <td>
                    <cms:CMSTextBox ID="txtMessageNotifEmail" MaxLength="200" runat="server" CssClass="TextBoxField" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblTimeZone" runat="server" EnableViewState="false" />
                </td>
                <td>
                    <cms:TimeZoneSelector ID="timeZone" runat="server" UseZoneNameForSelection="false" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblBadge" runat="server" EnableViewState="false" />
                </td>
                <td>
                    <cms:BadgeSelector ID="badgeSelector" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblUserActivityPoints" runat="server" EnableViewState="false" />
                </td>
                <td>
                    <cms:CMSTextBox ID="txtUserActivityPoints" MaxLength="9" runat="server" CssClass="TextBoxField" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblUserLiveID" runat="server" EnableViewState="false" />
                </td>
                <td>
                    <cms:CMSTextBox ID="txtUserLiveID" MaxLength="50" runat="server" CssClass="TextBoxField" />
                </td>
            </tr>
            <tr>
                <td>
                    <cms:LocalizedLabel ID="lblFacebookUserID" runat="server" EnableViewState="false"
                        DisplayColon="true" ResourceString="adm.user.lblfacebookid" />
                </td>
                <td>
                    <cms:CMSTextBox ID="txtFacebookUserID" MaxLength="100" runat="server" CssClass="TextBoxField" />
                </td>
            </tr>
            <tr>
                <td>
                    <cms:LocalizedLabel ID="lblUserOpenID" runat="server" EnableViewState="false" DisplayColon="true" />
                </td>
                <td>
                    <cms:CMSTextBox ID="txtOpenID" runat="server" MaxLength="450" CssClass="TextBoxField" />
                </td>
            </tr>
            <tr>
                <td>
                    <cms:LocalizedLabel ID="lblLinkedInUserID" runat="server" EnableViewState="false"
                        DisplayColon="true" />
                </td>
                <td>
                    <cms:CMSTextBox ID="txtLinkedInID" runat="server" MaxLength="450" CssClass="TextBoxField" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblActivationDate" runat="server" EnableViewState="false" />
                </td>
                <td>
                    <cms:DateTimePicker ID="activationDate" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblActivatedByUser" runat="server" EnableViewState="false" />
                </td>
                <td>
                    <asp:Label ID="lblUserFullName" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblRegInfo" runat="server" EnableViewState="false" />
                </td>
                <td>
                    <asp:PlaceHolder runat="server" ID="plcUserLastLogonInfo" EnableViewState="false" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblUserGender" runat="server" EnableViewState="false" />
                </td>
                <td>
                    <asp:RadioButtonList ID="rbtnlGender" runat="server" RepeatDirection="Horizontal" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblUserDateOfBirth" runat="server" EnableViewState="false" />
                </td>
                <td>
                    <cms:DateTimePicker ID="dtUserDateOfBirth" runat="server" EditTime="false" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblPosition" runat="server" EnableViewState="false" />
                </td>
                <td>
                    <cms:CMSTextBox ID="txtPosition" runat="server" MaxLength="200" CssClass="TextBoxField" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblUserSkype" runat="server" EnableViewState="false" />
                </td>
                <td>
                    <cms:CMSTextBox ID="txtUserSkype" runat="server" MaxLength="100" CssClass="TextBoxField" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblUserIM" runat="server" EnableViewState="false" />
                </td>
                <td>
                    <cms:CMSTextBox ID="txtUserIM" runat="server" MaxLength="100" CssClass="TextBoxField" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblUserPhone" runat="server" EnableViewState="false" />
                </td>
                <td>
                    <cms:CMSTextBox ID="txtPhone" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <cms:LocalizedLabel ID="lblLogActivities" runat="server" ResourceString="adm.user.lbllogactivities"
                        DisplayColon="true" EnableViewState="false" />
                </td>
                <td>
                    <asp:CheckBox ID="chkLogActivities" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblWaitingForActivation" runat="server" EnableViewState="false" />
                </td>
                <td>
                    <asp:CheckBox ID="chkWaitingForActivation" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblUserShowSplashScreen" runat="server" EnableViewState="false" />
                </td>
                <td>
                    <asp:CheckBox ID="chkUserShowSplashScreen" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblUserForumPosts" runat="server" EnableViewState="false" />
                </td>
                <td>
                    <asp:Label ID="lblUserForumPostsValue" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblUserBlogPosts" runat="server" EnableViewState="false" />
                </td>
                <td>
                    <asp:Label ID="lblUserBlogPostsValue" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblUserBlogComments" runat="server" EnableViewState="false" />
                </td>
                <td>
                    <asp:Label ID="lblUserBlogCommentsValue" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblUserMessageBoardPosts" runat="server" EnableViewState="false" />
                </td>
                <td>
                    <asp:Label ID="lblUserMessageBoardPostsValue" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <cms:CMSButton ID="btnOk" runat="server" OnClick="ButtonOK_Click" CssClass="SubmitButton"
                        EnableViewState="false" />
                </td>
            </tr>
        </table>
    </asp:PlaceHolder>
</asp:Content>
