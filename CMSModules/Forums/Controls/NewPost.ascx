<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_Forums_Controls_NewPost" CodeFile="NewPost.ascx.cs" %>
<%@ Register TagPrefix="cms" Namespace="CMS.ExtendedControls" Assembly="CMS.ExtendedControls" %>
<%@ Register Src="~/CMSFormControls/Inputs/SecurityCode.ascx" TagName="SecurityCode" TagPrefix="cms" %>
<asp:Label ID="lblHeader" runat="server" EnableViewState="false" CssClass="Title" />
<asp:Panel runat="server" ID="pnlReplyPost" CssClass="PostReply" Visible="false">
    <div class="PostPreviewSubject">
        <asp:Label runat="server" ID="lblSubjectPreview" />
    </div>
    <br />
    <div>
        <asp:Label runat="server" ID="lblTextPreview" />
    </div>
</asp:Panel>
<div class="FormPadding">
    <asp:Label runat="server" ID="lblError"  CssClass="ErrorLabel" ForeColor="Red" EnableViewState="false"
        Visible="false" />
    <asp:Panel runat="server" ID="pnlInfo" Visible="false" CssClass="PostEditInfo">
    <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false" />
    </asp:Panel>
    <table class="PostForm">
        <asp:PlaceHolder runat="server" ID="plcUserName">
            <tr>
                <td class="ItemLabel">
                    <cms:LocalizedLabel ID="lblUserName" runat="server" ResourceString="general.username"
                        AssociatedControlID="txtUserName" DisplayColon="true" />
                </td>
                <td>
                    <cms:CMSTextBox ID="txtUserName" runat="server" CssClass="TextboxItemShort" MaxLength="200" />
                    <cms:CMSRequiredFieldValidator ID="rfvUserName" runat="server" ControlToValidate="txtUserName"
                        Display="Static" ValidationGroup="NewPostforum" />
                </td>
            </tr>
        </asp:PlaceHolder>
        <asp:PlaceHolder runat="server" ID="plcNickName">
            <tr>
                <td class="ItemLabel">
                    <asp:Label ID="lblNickName" runat="server" />
                </td>
                <td>
                    <asp:Label runat="server" ID="lblNickNameValue" />
                </td>
            </tr>
        </asp:PlaceHolder>
        <tr>
            <td class="ItemLabel">
                <cms:LocalizedLabel ID="lblEmail" runat="server" EnableViewState="false" ResourceString="general.email"
                    AssociatedControlID="txtEmail" DisplayColon="true" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtEmail" runat="server" CssClass="TextboxItemShort" MaxLength="100" />
                <cms:CMSRegularExpressionValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail"
                    Display="Static" ValidationGroup="NewPostforum" />
                <cms:CMSRequiredFieldValidator ID="rfvEmailRequired" runat="server" ControlToValidate="txtEmail"
                    Enabled="false" Display="Static" ValidationGroup="NewPostforum" />
            </td>
        </tr>
        <tr>
            <td class="ItemLabel">
                <cms:LocalizedLabel ID="lblSubject" runat="server" EnableViewState="false" ResourceString="general.subject"
                    AssociatedControlID="txtSubject" DisplayColon="true" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtSubject" runat="server" CssClass="TextboxItem" MaxLength="450" />
                <cms:CMSRequiredFieldValidator ID="rfvSubject" runat="server" ControlToValidate="txtSubject"
                    Display="Static" ValidationGroup="NewPostforum" />
            </td>
        </tr>
        <asp:PlaceHolder runat="server" ID="plcThreadType" Visible="false">
            <tr>
                <td class="ItemLabel">
                    <asp:Label ID="lblThreadType" runat="server" />
                </td>
                <td>
                    <cms:LocalizedRadioButton ID="radTypeDiscussion" runat="server" GroupName="type"
                        ResourceString="forum.settings.discussionthread" Checked="true" />
                    <cms:LocalizedRadioButton ID="radTypeQuestion" runat="server" GroupName="type" ResourceString="forum.settings.questionthread" />
                </td>
            </tr>
        </asp:PlaceHolder>
        <tr>
            <td class="ItemLabel">
                <cms:LocalizedLabel ID="lblText" runat="server" EnableViewState="false" />
            </td>
            <td>
                <cms:BBEditor ID="ucBBEditor" runat="server" />
                <cms:CMSRequiredFieldValidator ID="rfvText" runat="server" ControlToValidate="ucBBEditor"
                    Display="Dynamic" ValidationGroup="NewPostforum" />
                <cms:CMSHtmlEditor ID="htmlTemplateBody" runat="server" Width="500px" Height="200px" Visible="true" />
            </td>
        </tr>
        <asp:PlaceHolder runat="server" ID="plcSignature">
            <tr>
                <td class="ItemLabel">
                    <asp:Label ID="lblSignature" runat="server" EnableViewState="false" AssociatedControlID="txtSignature" />
                </td>
                <td>
                    <cms:CMSTextBox ID="txtSignature" runat="server" CssClass="SignatureAreaItem" TextMode="MultiLine"
                        Rows="3" />
                </td>
            </tr>
        </asp:PlaceHolder>
        <asp:PlaceHolder runat="server" ID="plcCaptcha">
            <tr>
                <td class="ItemLabel">
                    <cms:LocalizedLabel ID="lblCaptcha" runat="server" EnableViewState="false" />
                </td>
                <td>
                    <asp:Panel ID="pnlCaptcha" runat="server" DefaultButton="btnOk">
                        <cms:SecurityCode ID="SecurityCode1" runat="server" ShowAfterText="true" />
                    </asp:Panel>
                </td>
            </tr>
        </asp:PlaceHolder>
        <asp:PlaceHolder runat="server" ID="SubscribeHolder">
            <tr>
                <td class="ItemLabel">
                    <asp:Label ID="lblSubscribe" runat="server" AssociatedControlID="chkSubscribe" EnableViewState="false" />
                </td>
                <td>
                    <asp:CheckBox runat="server" ID="chkSubscribe" />
                </td>
            </tr>
        </asp:PlaceHolder>
        <asp:PlaceHolder runat="server" ID="plcAttachFile">
            <tr>
                <td class="ItemLabel">
                    <asp:Label ID="lblAttachFile" AssociatedControlID="chkAttachFile" runat="server" EnableViewState="false" />
                </td>
                <td>
                    <asp:CheckBox runat="server" ID="chkAttachFile" />
                </td>
            </tr>
        </asp:PlaceHolder>
        <tr>
            <td>
            </td>
            <td>
                <cms:CMSButton ID="btnOk" runat="server" CssClass="SubmitButton" OnClick="btnOK_Click"
                    ValidationGroup="NewPostforum" /><cms:CMSButton ID="btnCancel" 
                    runat="server" CssClass="SubmitButton" OnClick="btnCancel_Click" /><cms:CMSButton ID="btnPreview" runat="server" CssClass="SubmitButton" OnClick="btnPreview_Click"
                    ValidationGroup="NewPostforum" />
            </td>
        </tr>
    </table>
</div>
<asp:Literal ID="ltlScript" runat="server" EnableViewState="false" />
