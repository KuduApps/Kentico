<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_MessageBoards_Controls_Messages_MessageEdit"
    CodeFile="MessageEdit.ascx.cs" %>
<%@ Register Src="~/CMSFormControls/Inputs/SecurityCode.ascx" TagName="SecurityCode"
    TagPrefix="cms" %>
<asp:Panel ID="pnlMessageEdit" runat="server" CssClass="MessageEdit">
    <table>
        <tr>
            <td>
                <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
                    Visible="false" />
                <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
                    Visible="false" />
                <table width="100%">
                    <asp:PlaceHolder ID="plcRating" runat="server">
                        <tr>
                            <td>
                                <asp:Label ID="lblRating" runat="server" EnableViewState="false" CssClass="FieldLabel" />
                            </td>
                            <td>
                                <asp:Panel ID="pnlRating" runat="server" CssClass="BoardCntRating" />
                            </td>
                        </tr>
                    </asp:PlaceHolder>
                    <tr>
                        <td>
                            <asp:Label ID="lblUserName" AssociatedControlID="txtUserName" runat="server" EnableViewState="false"
                                CssClass="FieldLabel" />
                        </td>
                        <td>
                            <cms:CMSTextBox ID="txtUserName" runat="server" CssClass="TextBoxField" EnableViewState="false"
                                MaxLength="250" ProcessMacroSecurity="false" /><br />
                            <cms:CMSRequiredFieldValidator ID="rfvUserName" runat="server" ControlToValidate="txtUserName"
                                ValidationGroup="MessageEdit" Display="Dynamic" EnableViewState="false" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblURL" AssociatedControlID="txtURL" runat="server" EnableViewState="false"
                                CssClass="FieldLabel" />
                        </td>
                        <td>
                            <cms:CMSTextBox ID="txtURL" runat="server" CssClass="TextBoxField" EnableViewState="false"
                                MaxLength="450" ProcessMacroSecurity="false" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblEmail" AssociatedControlID="txtEmail" runat="server" EnableViewState="false"
                                CssClass="FieldLabel" />
                        </td>
                        <td>
                            <cms:CMSTextBox ID="txtEmail" runat="server" CssClass="TextBoxField" EnableViewState="false"
                                MaxLength="250" ProcessMacroSecurity="false" /><br />
                            <cms:CMSRegularExpressionValidator ID="revEmailValid" runat="server" ControlToValidate="txtEmail"
                                Display="Dynamic" EnableViewState="false" />
                            <cms:CMSRequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail"
                                Display="Dynamic" EnableViewState="false" />
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top;">
                            <asp:Label ID="lblMessage" AssociatedControlID="txtMessage" runat="server" EnableViewState="false"
                                CssClass="FieldLabel" />
                        </td>
                        <td>
                            <cms:CMSTextBox ID="txtMessage" runat="server" CssClass="TextAreaField" TextMode="MultiLine"
                                EnableViewState="false" ProcessMacroSecurity="false" /><br />
                            <cms:CMSRequiredFieldValidator ID="rfvMessage" runat="server" ControlToValidate="txtMessage"
                                ValidationGroup="MessageEdit" Display="Dynamic" EnableViewState="false" />
                        </td>
                    </tr>
                    <asp:PlaceHolder ID="plcAdvanced" runat="server" Visible="false">
                        <asp:PlaceHolder ID="plcApproved" runat="server" EnableViewState="false">
                            <tr>
                                <td>
                                    <asp:Label ID="lblApproved" AssociatedControlID="chkApproved" runat="server" EnableViewState="false"
                                        CssClass="FieldLabel" />
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkApproved" runat="server" CssClass="CheckBoxMovedLeft" EnableViewState="false" />
                                </td>
                            </tr>
                        </asp:PlaceHolder>
                        <tr>
                            <td>
                                <asp:Label ID="lblSpam" AssociatedControlID="chkSpam" runat="server" EnableViewState="false"
                                    CssClass="FieldLabel" />
                            </td>
                            <td>
                                <asp:CheckBox ID="chkSpam" runat="server" CssClass="CheckBoxMovedLeft" EnableViewState="false" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblInsertedCaption" runat="server" EnableViewState="false" Visible="false"
                                    CssClass="FieldLabel" />
                            </td>
                            <td>
                                <asp:Label ID="lblInserted" runat="server" Visible="false" />
                            </td>
                        </tr>
                    </asp:PlaceHolder>
                    <asp:PlaceHolder ID="plcChkSubscribe" runat="server" EnableViewState="false">
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <asp:CheckBox ID="chkSubscribe" runat="server" CssClass="CheckBoxMovedLeft" EnableViewState="false" />
                            </td>
                        </tr>
                    </asp:PlaceHolder>
                    <asp:PlaceHolder ID="pnlCaptcha" runat="server" Visible="false">
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <asp:Panel ID="pnlCaptchaObj" runat="server" DefaultButton="btnOk">
                                    <cms:SecurityCode ID="captchaElem" runat="server" />
                                </asp:Panel>
                            </td>
                        </tr>
                    </asp:PlaceHolder>
                    <asp:PlaceHolder ID="pnlOkButton" runat="server" Visible="true">
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <cms:LocalizedButton ID="btnOk" runat="server" CssClass="SubmitButton" OnClick="btnOk_Click"
                                    EnableViewState="false" ResourceString="general.add" />
                            </td>
                        </tr>
                    </asp:PlaceHolder>
                </table>
            </td>
        </tr>
    </table>
    <asp:PlaceHolder ID="plcFooter" runat="server" Visible="false">
        <div class="PageFooterLine">
            <div class="FloatRight">
                <cms:LocalizedButton ID="btnOkFooter" runat="server" CssClass="SubmitButton" OnClick="btnOk_Click"
                    EnableViewState="false" ResourceString="general.add" /><cms:LocalizedButton ID="btnCancel"
                        runat="server" ResourceString="general.cancel" CssClass="SubmitButton" OnClientClick="window.close(); return false;"
                        EnableViewState="False" />
            </div>
        </div>
    </asp:PlaceHolder>
    <asp:Literal ID="ltlScript" runat="server" EnableViewState="false" />
</asp:Panel>
