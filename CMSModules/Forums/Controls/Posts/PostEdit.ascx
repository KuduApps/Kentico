<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_Forums_Controls_Posts_PostEdit" CodeFile="PostEdit.ascx.cs" %>
<%@ Register Src="ForumPost.ascx" TagName="ForumPost" TagPrefix="uc2" %>
<%@ Register TagPrefix="cms" Namespace="CMS.ExtendedControls" Assembly="CMS.ExtendedControls" %>
<asp:Panel ID="pnlContent" runat="server" CssClass="ForumNewPost">
    <asp:Label ID="lblHeader" runat="server" EnableViewState="false" CssClass="Title" />
    <asp:Panel runat="server" ID="pnlReplyPost" CssClass="PostReply" Visible="false">
        <div class="ForumFlat">
            <uc2:forumpost id="ForumPost1" runat="server" />
        </div>
    </asp:Panel>
    <div class="FormPadding">
        <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" ForeColor="Red" EnableViewState="false"
            Visible="false" />
        <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" ForeColor="Red" EnableViewState="false"
            Visible="false" />
        <table class="PostForm">
            <tr>
                <td class="ItemLabel">
                    <cms:localizedlabel id="lblUserName" runat="server" resourcestring="general.username"
                        displaycolon="true" />
                </td>
                <td>
                    <cms:CMSTextBox ID="txtUserName" runat="server" CssClass="TextboxItemShort" MaxLength="200" />
                    <cms:CMSRequiredFieldValidator ID="rfvUserName" runat="server" ControlToValidate="txtUserName"
                        Display="Dynamic" ValidationGroup="NewPostforum" />
                </td>
            </tr>
            <tr>
                <td class="ItemLabel">
                    <cms:localizedlabel id="lblEmail" runat="server" enableviewstate="false" resourcestring="general.email"
                        displaycolon="true" />
                </td>
                <td>
                    <cms:CMSTextBox ID="txtEmail" runat="server" CssClass="TextboxItemShort" MaxLength="100" />
                    <cms:CMSRegularExpressionValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail"
                        Display="Dynamic" ValidationGroup="NewPostforum" />
                    <cms:CMSRequiredFieldValidator ID="rfvEmailRequired" runat="server" ControlToValidate="txtEmail"
                        Enabled="false" Display="Dynamic" ValidationGroup="NewPostforum" />
                </td>
            </tr>
            <tr>
                <td class="ItemLabel">
                    <cms:localizedlabel id="lblSubject" runat="server" enableviewstate="false" resourcestring="general.subject"
                        displaycolon="true" />
                </td>
                <td>
                    <cms:CMSTextBox ID="txtSubject" runat="server" CssClass="TextboxItem" MaxLength="450" />
                    <cms:CMSRequiredFieldValidator ID="rfvSubject" runat="server" ControlToValidate="txtSubject"
                        Display="Dynamic" ValidationGroup="NewPostforum"></cms:CMSRequiredFieldValidator>
                </td>
            </tr>
            <asp:PlaceHolder runat="server" ID="plcThreadType" Visible="false">
                <tr>
                    <td class="ItemLabel">
                        <asp:Label ID="lblThreadType" runat="server" />
                    </td>
                    <td>
                        <cms:localizedradiobutton id="radTypeDiscussion" runat="server" groupname="type"
                            resourcestring="forum.settings.discussionthread" checked="true" />
                        <cms:localizedradiobutton id="radTypeQuestion" runat="server" groupname="type" resourcestring="forum.settings.questionthread" />
                    </td>
                </tr>
            </asp:PlaceHolder>
            <tr>
                <td class="ItemLabel">
                    <cms:localizedlabel id="lblText" runat="server" enableviewstate="false" />
                </td>
                <td>
                    <cms:bbeditor id="ucBBEditor" runat="server" enableviewstate="false" />
                    <cms:CMSRequiredFieldValidator ID="rfvText" runat="server" ControlToValidate="ucBBEditor"
                        Display="Dynamic" ValidationGroup="NewPostforum" />
                    <cms:cmshtmleditor id="htmlTemplateBody" runat="server" width="500px" height="200px"
                        visible="false" />
                </td>
            </tr>
            <tr>
                <td class="ItemLabel">
                    <asp:Label ID="lblSignature" runat="server" EnableViewState="false" />
                </td>
                <td>
                    <cms:CMSTextBox ID="txtSignature" runat="server" CssClass="SignatureAreaItem" TextMode="MultiLine"
                        Rows="3"/>
                </td>
            </tr>
            <asp:PlaceHolder ID="plcIsAnswer" runat="server" Visible="false">
                <tr>
                    <td class="ItemLabel">
                        <asp:Label ID="lblPostIsAnswerLabel" runat="server" EnableViewState="false" />
                    </td>
                    <td>
                        <cms:CMSTextBox ID="txtPostIsAnswer" runat="server" CssClass="TextBoxField" MaxLength="9" />
                    </td>
                </tr>
            </asp:PlaceHolder>
            <asp:PlaceHolder ID="plcIsNotAnswer" runat="server" Visible="false">
                <tr>
                    <td class="ItemLabel">
                        <asp:Label ID="lblPostIsNotAnswerLabel" runat="server" EnableViewState="false" />
                    </td>
                    <td>
                        <cms:CMSTextBox ID="txtPostIsNotAnswer" runat="server" CssClass="TextBoxField" MaxLength="9" />
                    </td>
                </tr>
            </asp:PlaceHolder>
            <asp:PlaceHolder ID="plcSubscribe" runat="server">
                <tr>
                    <td class="ItemLabel">
                        <asp:Label ID="lblSubscribe" runat="server" EnableViewState="false" />
                    </td>
                    <td>
                        <asp:CheckBox runat="server" ID="chkSubscribe" />
                    </td>
                </tr>
            </asp:PlaceHolder>
            <tr>
                <td>
                </td>
                <td>
                    <cms:cmsbutton id="btnOk" runat="server" cssclass="SubmitButton" onclick="btnOK_Click"
                        validationgroup="NewPostforum" /><cms:cmsbutton 
                            id="btnCancel" runat="server" cssclass="SubmitButton" onclick="btnCancel_Click" /><cms:cmsbutton 
                                id="btnPreview" runat="server" cssclass="SubmitButton" onclick="btnPreview_Click" validationgroup="NewPostforum" />
                </td>
            </tr>
        </table>
    </div>
</asp:Panel>
<asp:Literal ID="ltlScript" runat="server" EnableViewState="false" />
