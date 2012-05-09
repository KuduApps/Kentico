<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_MessageBoards_Controls_Boards_BoardSubscription" CodeFile="BoardSubscription.ascx.cs" %>
<%@ Register Src="~/CMSModules/Membership/FormControls/Users/selectuser.ascx" TagName="UserSelector" TagPrefix="cms" %>
<cms:CMSUpdatePanel runat="server" ID="pnlUpdate" UpdateMode="Always">
    <ContentTemplate>
        <asp:Label ID="lblInfo" runat="server" CssClass="InfoLabel" Visible="false" EnableViewState="false" />
        <asp:Label ID="lblError" runat="server" CssClass="ErrorLabel" Visible="false" EnableViewState="false" />
        <table>
            <tr>
                <td colspan="3">
                    <asp:RadioButton ID="radAnonymousSubscription" runat="server" Visible="true" AutoPostBack="true"
                        GroupName="subscription" />
                </td>
            </tr>
            <tr>
                <td style="width: 24px;">
                    &nbsp;
                </td>
                <td>
                    <asp:Label ID="lblEmailAnonymous" runat="server" CssClass="FieldLabel" EnableViewState="false" />
                </td>
                <td>
                    <cms:CMSTextBox ID="txtEmailAnonymous" MaxLength="250" runat="server" CssClass="TextBoxField"
                        EnableViewState="false" />
                    <cms:CMSRequiredFieldValidator ID="rfvEmailAnonymous" runat="server" ControlToValidate="txtEmailAnonymous"
                        Display="Dynamic" Enabled="false" EnableViewState="false" />
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:RadioButton ID="radRegisteredSubscription" runat="server" Visible="true" AutoPostBack="true"
                        GroupName="subscription" />
                </td>
            </tr>
            <tr>
                <td style="width: 24px;">
                    &nbsp;
                </td>
                <td>
                    <asp:Label ID="lblUserRegistered" runat="server" CssClass="FieldLabel" EnableViewState="false" />
                </td>
                <td>
                    <cms:UserSelector ID="userSelector" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="width: 24px;">
                    &nbsp;
                </td>
                <td>
                    <asp:Label ID="lblEmailRegistered" runat="server" EnableViewState="false" />
                </td>
                <td>
                    <cms:CMSTextBox ID="txtEmailRegistered" runat="server" MaxLength="250" CssClass="TextBoxField"
                        EnableViewState="false" />
                    <cms:CMSRequiredFieldValidator ID="rfvEmailRegistered" runat="server" ControlToValidate="txtEmailRegistered"
                        Display="Dynamic" Enabled="false" EnableViewState="false" ValidationGroup="Email" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
                <td>
                    <cms:CMSButton ID="btnOk" runat="server" CssClass="SubmitButton" OnClick="btnOk_Click"
                        EnableViewState="false" ValidationGroup="Email" />
                </td>
            </tr>
        </table>
        <asp:Literal ID="ltlScripMail" runat="server" EnableViewState="false" />
    </ContentTemplate>
</cms:CMSUpdatePanel>

<script type="text/javascript">
    //<![CDATA[
    function SetTextForUser(mText, mId, mText2, mId2) {
        var elem2 = document.getElementById(mId2);
        var elem1 = document.getElementById(mId);

        if (elem1 != null) {
            elem1.value = mText;
        }

        if (elem2 != null) {
            elem2.value = mText2;
        }

        GetUsersEmail(mText2);

        return false;
    }
    //]]>             
</script>

