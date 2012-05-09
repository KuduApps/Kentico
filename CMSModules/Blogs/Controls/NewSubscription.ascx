<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_Blogs_Controls_NewSubscription" CodeFile="NewSubscription.ascx.cs" %>
<asp:Panel ID="pnlContent" runat="server" CssClass="BoardNewPost">
    <table>
        <tr>
            <td>
                <asp:Label ID="lblInfo" runat="server" CssClass="InfoLabel" Visible="false" EnableViewState="false" />
                <asp:Label ID="lblError" runat="server" CssClass="ErrorLabel" Visible="false" EnableViewState="false" />
                <asp:Panel runat="server" ID="pnlPadding" CssClass="FormPadding" DefaultButton="btnOK">
                    <table class="BoardForm">
                        <tr>
                            <td class="ItemLabel" style="vertical-align: top;">
                                <cms:LocalizedLabel ID="lblEmail" runat="server" AssociatedControlID="txtEmail" EnableViewState="false" />
                            </td>
                            <td>
                                <cms:CMSTextBox ID="txtEmail" runat="server" CssClass="TextBoxField" MaxLength="100" /><br />
                                <cms:CMSRequiredFieldValidator ID="rfvEmailRequired" runat="server" ControlToValidate="txtEmail"
                                    Display="Dynamic" EnableViewState="false" />
                                <cms:CMSRegularExpressionValidator ID="revEmailValid" runat="server" ControlToValidate="txtEmail"
                                    Display="Dynamic" EnableViewState="false" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <cms:LocalizedButton ID="btnOk" runat="server" CssClass="SubmitButton" OnClick="btnOK_Click"
                                    EnableViewState="false" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Panel>
