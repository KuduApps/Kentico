<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_Groups_Controls_GroupInvite" CodeFile="GroupInvite.ascx.cs" %>
<%@ Register Src="~/CMSModules/Membership/FormControls/Users/selectuser.ascx" TagName="SelectUser" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/Groups/FormControls/CommunityGroupSelector.ascx" TagName="GroupSelector"
    TagPrefix="cms" %>
<table class="GroupInviteTable">
    <tr>
        <td colspan="2" class="InfoArea">
            <asp:Label runat="server" ID="lblInfo" EnableViewState="false" CssClass="InfoLabel"
                Visible="false" />
            <asp:Label runat="server" ID="lblError" EnableViewState="false" CssClass="ErrorLabel"
                Visible="false" />
        </td>
    </tr>
    <asp:PlaceHolder ID="plcUserType" runat="server">
        <tr>
            <td style="width: 100px;">
                <cms:LocalizedLabel ID="lblUserType" runat="server" EnableViewState="false" ResourceString="groupinvitation.invitationtype"
                    DisplayColon="true" CssClass="FieldLabel" />
            </td>
            <td>
                <table>
                    <tr>
                        <td>
                            <cms:LocalizedRadioButton AutoPostBack="true" ID="radSiteMember" GroupName="grpUserType"
                                ResourceString="invitation.existingsitemember" runat="server" Checked="true" />
                        </td>
                        <td>
                            <cms:LocalizedRadioButton AutoPostBack="true" ID="radNewUser" GroupName="grpUserType"
                                ResourceString="invitation.viaemail" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="plcEmail" runat="server">
        <tr>
            <td style="width: 100px;">
                <cms:LocalizedLabel ID="lblEmail" runat="server" EnableViewState="false" ResourceString="general.email"
                    DisplayColon="true" CssClass="FieldLabel" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtEmail" runat="server" Width="385" CssClass="TextBoxField" /><br />
                <cms:CMSRegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail"
                    Display="dynamic" EnableViewState="false" />
                <cms:CMSRequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail"
                    Display="dynamic" EnableViewState="false" />
            </td>
        </tr>
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="plcUserSelector" runat="server">
        <tr>
            <td style="width: 100px;">
                <cms:LocalizedLabel ID="lblUser" runat="server" EnableViewState="false" ResourceString="general.username"
                    DisplayColon="true" CssClass="FieldLabel" />
            </td>
            <td>
                <cms:SelectUser ID="userSelector" runat="server" />
            </td>
        </tr>
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="plcGroupSelector" runat="server">
        <tr>
            <td style="width: 100px;">
                <cms:LocalizedLabel ID="lblGroups" runat="server" EnableViewState="false" ResourceString="general.groups"
                    DisplayColon="true" CssClass="FieldLabel" />
            </td>
            <td>
                <cms:GroupSelector runat="server" ID="groupSelector" />
            </td>
        </tr>
    </asp:PlaceHolder>
    <tr>
        <td style="width: 100px; vertical-align: top;">
            <cms:LocalizedLabel ID="lblComment" runat="server" ResourceString="general.comment"
                DisplayColon="true" CssClass="FieldLabel" EnableViewState="false" />
        </td>
        <td>
            <cms:CMSTextBox ID="txtComment" runat="server" CssClass="BodyField" TextMode="MultiLine"
                Width="385" Height="100" EnableViewState="false" />
        </td>
    </tr>
    <asp:PlaceHolder ID="plcButtons" runat="server">
        <tr>
            <td>
            </td>
            <td>
                <cms:CMSButton runat="server" CssClass="SubmitButton" ID="btnInvite" EnableViewState="false" />
                <cms:LocalizedButton CssClass="SubmitButton" ID="btnCancel" runat="server" ResourceString="General.cancel"
                    EnableViewState="false" />
            </td>
        </tr>
    </asp:PlaceHolder>
</table>

<script type="text/javascript">
    //<![CDATA[

    function Close() {
        window.close();
    }

    function CloseAndRefresh() {
        if (wopener != null) {
            if (wopener.ReloadPage != null) {
                wopener.ReloadPage();
            }
        }
        Close();
    }
    //]]>
</script>

