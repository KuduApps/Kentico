<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_Blogs_Controls_BlogCommentEdit"
    CodeFile="BlogCommentEdit.ascx.cs" %>
<%@ Register Src="~/CMSFormControls/Inputs/SecurityCode.ascx" TagName="SecurityCode"
    TagPrefix="cms" %>
<div class="CommentFormContainer">
    <asp:Panel ID="pnlInfo" runat="server">
        <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
            Visible="false" />
        <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
            Visible="false" />
    </asp:Panel>
    <table width="100%" class="CommentForm">
        <tr>
            <td colspan="2">
                <div class="BlogCommentName <%= LiveSiteCss %>">
                    <asp:Label ID="lblName" runat="server" AssociatedControlID="txtName" EnableViewState="false" />
                </div>
                <asp:Panel ID="pnlName" runat="server" DefaultButton="btnOk">
                    <cms:CMSTextBox ID="txtName" runat="server" CssClass="TextBoxField" MaxLength="200"
                        EnableViewState="false" /><br />
                </asp:Panel>
                <div class="BlogRequiredValidator">
                    <cms:CMSRequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtName"
                        Display="Dynamic" EnableViewState="false" />
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <div class="BlogCommentEmail <%= LiveSiteCss %>">
                    <cms:LocalizedLabel ID="lblEmail" runat="server" EnableViewState="false" ResourceString="general.email"
                        DisplayColon="true" AssociatedControlID="txtEmail" />
                </div>
                <asp:Panel ID="pnlEmail" runat="server" DefaultButton="btnOk">
                    <cms:CMSTextBox ID="txtEmail" runat="server" CssClass="TextBoxField" MaxLength="250"
                        EnableViewState="false" /><br />
                </asp:Panel>
                <div class="BlogRequiredValidator">
                    <cms:CMSRequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail"
                        Display="Dynamic" EnableViewState="false" />
                    <cms:CMSRegularExpressionValidator ID="revEmailValid" runat="server" ControlToValidate="txtEmail"
                        Display="Dynamic" EnableViewState="false" />
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <div class="BlogCommentUrl <%= LiveSiteCss %>">
                    <asp:Label ID="lblUrl" runat="server" AssociatedControlID="txtUrl" EnableViewState="false" />
                </div>
                <asp:Panel ID="pnlUrl" runat="server" DefaultButton="btnOk">
                    <cms:CMSTextBox ID="txtUrl" runat="server" CssClass="TextBoxField" MaxLength="450"
                        EnableViewState="false" />
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top;" colspan="2">
                <div class="BlogCommentComments <%= LiveSiteCss %>">
                    <asp:Label ID="lblComments" runat="server" AssociatedControlID="txtComments" EnableViewState="false" />
                </div>
                <cms:CMSTextBox ID="txtComments" runat="server" CssClass="TextAreaField" Rows="4"
                    TextMode="MultiLine" EnableViewState="false" /><br />
                <div class="BlogRequiredValidator">
                    <cms:CMSRequiredFieldValidator ID="rfvComments" runat="server" ControlToValidate="txtComments"
                        Display="Dynamic" EnableViewState="false" />
                </div>
            </td>
        </tr>
        <%-- Advanced mode --%>
        <asp:PlaceHolder ID="plcAdvancedMode" runat="server" Visible="false">
            <%-- Comment approved --%>
            <tr>
                <td colspan="2">
                    <div class="BlogCommentApproved">
                        <asp:Label ID="lblApproved" AssociatedControlID="chkApproved" runat="server" EnableViewState="false" />
                    </div>
                    <asp:Panel ID="pnlApproved" runat="server" DefaultButton="btnOk">
                        <asp:CheckBox ID="chkApproved" CssClass="CheckBoxMovedLeft" runat="server" EnableViewState="false" />
                    </asp:Panel>
                </td>
            </tr>
            <%-- Comment is spam --%>
            <tr>
                <td>
                    <div class="BlogCommentIsSpam">
                        <asp:Label ID="lblSpam" runat="server" AssociatedControlID="chkSpam" EnableViewState="false" />
                    </div>
                    <asp:Panel ID="pnlSpam" runat="server" DefaultButton="btnOk">
                        <asp:CheckBox ID="chkSpam" CssClass="CheckBoxMovedLeft" runat="server" EnableViewState="false" />
                    </asp:Panel>
                </td>
            </tr>
            <%-- Comment inserted --%>
            <tr>
                <td>
                    <div class="BlogCommentInserted">
                        <asp:Label ID="lblInserted" runat="server" EnableViewState="false" />
                    </div>
                    <asp:Panel ID="pnlInserted" runat="server" DefaultButton="btnOk">
                        <asp:Label ID="lblInsertedDate" runat="server" />
                    </asp:Panel>
                </td>
            </tr>
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="plcChkSubscribe" runat="server">
            <tr>
                <td>
                    <cms:LocalizedCheckBox ID="chkSubscribe" runat="server" CssClass="CheckBoxMovedLeft"
                        ResourceString="Blog.CommentEdit.Subscribe" EnableViewState="false" />
                </td>
            </tr>
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="plcCaptcha" runat="server" Visible="false" EnableViewState="false">
            <tr>
                <td>
                    <asp:Panel ID="pnlCaptcha" runat="server" DefaultButton="btnOk">
                        <cms:SecurityCode ID="ctrlCaptcha" runat="server" />
                    </asp:Panel>
                </td>
            </tr>
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="plcButtons" runat="server">
            <tr>
                <td colspan="2">
                    <div class="BlogRequiredValidator">
                        <cms:CMSButton ID="btnOk" runat="server" CssClass="SubmitButton" OnClick="btnOk_Click"
                            EnableViewState="false" />
                    </div>
                </td>
            </tr>
        </asp:PlaceHolder>
    </table>
</div>
