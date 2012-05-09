<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_Groups_Controls_GroupEdit"
    CodeFile="GroupEdit.ascx.cs" %>

<%@ Register Src="~/CMSFormControls/System/LocalizableTextBox.ascx" TagName="LocalizableTextBox" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/Content/FormControls/Documents/selectdocument.ascx"
    TagName="SelectDocument" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/Groups/FormControls/GroupPictureEdit.ascx" TagName="GroupPictureEdit"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSFormControls/SelectCssStylesheet.ascx" TagName="SelectCssStylesheet"
    TagPrefix="cms" %>
<asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
    Visible="false" />
<asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
    Visible="false" />
<table>
    <asp:PlaceHolder runat="server" ID="plcAdvanceOptions">
        <tr>
            <td>
                <cms:LocalizedLabel ID="lblDisplayName" AssociatedControlID="txtDisplayName" runat="server"
                    CssClass="FieldLabel" ResourceString="general.displayname" DisplayColon="true"
                    EnableViewState="false" />
            </td>
            <td>
                <!-- Do not disable view state - simple mode -->
                <cms:LocalizableTextBox ID="txtDisplayName" runat="server" MaxLength="200" CssClass="TextBoxField" />
                <cms:CMSRequiredFieldValidator ID="rfvDisplayName" runat="server" ControlToValidate="txtDisplayName:textbox"
                    Display="Dynamic" ValidationGroup="GroupEdit" EnableViewState="false" />
            </td>
        </tr>
        <asp:PlaceHolder runat="server" ID="plcCodeName">
            <tr>
                <td>
                    <cms:LocalizedLabel ID="lblCodeName" AssociatedControlID="txtCodeName" runat="server"
                        CssClass="FieldLabel" ResourceString="general.codename" DisplayColon="true" EnableViewState="false" />
                </td>
                <td>
                    <!-- Do not disable view state - simple mode -->
                    <cms:CMSTextBox ID="txtCodeName" runat="server" MaxLength="100" CssClass="TextBoxField" />
                    <cms:CMSRequiredFieldValidator ID="rfvCodeName" runat="server" ControlToValidate="txtCodeName"
                        Display="Dynamic" ValidationGroup="GroupEdit" EnableViewState="false" />
                </td>
            </tr>
        </asp:PlaceHolder>
    </asp:PlaceHolder>
    <tr>
        <td>
            <cms:LocalizedLabel ID="lblDescription" AssociatedControlID="txtDescription" runat="server"
                CssClass="FieldLabel" ResourceString="general.description" DisplayColon="true"
                EnableViewState="false" />
        </td>
        <td>
            <cms:LocalizableTextBox ID="txtDescription" runat="server" TextMode="MultiLine" CssClass="TextAreaField"
                EnableViewState="false" />
        </td>
    </tr>
    <asp:PlaceHolder ID="plcGroupLocation" runat="server" Visible="false">
        <tr>
            <td>
                <cms:LocalizedLabel ID="lblGroupPageURL" runat="server" CssClass="FieldLabel" ResourceString="group.group.grouppagelocation"
                    DisplayColon="true" EnableViewState="false" />
            </td>
            <td>
                <cms:SelectDocument ID="groupPageURLElem" runat="server" />
            </td>
        </tr>
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="plcStyleSheetSelector" runat="server">
        <tr>
            <td>
                <cms:LocalizedLabel ID="lblStyleSheetName" runat="server" CssClass="FieldLabel" ResourceString="community.group.theme"
                    DisplayColon="true" EnableViewState="false" />
            </td>
            <td>
                <cms:SelectCssStylesheet ID="ctrlSiteSelectStyleSheet" runat="server"  ReturnColumnName="StyleSheetID"/>
            </td>
        </tr>
    </asp:PlaceHolder>
    <tr>
        <td>
            <cms:LocalizedLabel ID="lblGroupAvatar" runat="server" CssClass="FieldLabel" ResourceString="group.group.avatar"
                DisplayColon="true" EnableViewState="false" />
        </td>
        <td>
            <cms:GroupPictureEdit ID="groupPictureEdit" runat="server" MaxSideSize="100" />
        </td>
    </tr>
    <tr>
        <td>
            <br />
            <cms:LocalizedLabel ID="lblApproveMembers" runat="server" CssClass="FieldLabel" ResourceString="group.group.approvemembers"
                DisplayColon="true" EnableViewState="false" />
        </td>
        <td>
            <br />
            <table class="RadioGroup">
                <tr>
                    <td>
                        <cms:LocalizedRadioButton ID="radMembersAny" runat="server" GroupName="approvemembers"
                            Checked="true" ResourceString="group.group.approveany" CssClass="RadioButtonMovedLeft"
                            EnableViewState="false" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <cms:LocalizedRadioButton ID="radMembersApproved" runat="server" GroupName="approvemembers"
                            ResourceString="group.group.approveapproved" CssClass="RadioButtonMovedLeft"
                            EnableViewState="false" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <cms:LocalizedRadioButton ID="radMembersInvited" runat="server" GroupName="approvemembers"
                            ResourceString="group.group.approveinvited" CssClass="RadioButtonMovedLeft" EnableViewState="false" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            <br />
            <cms:LocalizedLabel ID="lblContentAccess" runat="server" CssClass="FieldLabel" ResourceString="group.group.contentaccess"
                DisplayColon="true" EnableViewState="false" />
        </td>
        <td>
            <table class="RadioGroup">
                <tr>
                    <td>
                        <cms:LocalizedRadioButton ID="radAnybody" runat="server" GroupName="contentaccess"
                            Checked="true" ResourceString="group.group.accessanybody" CssClass="RadioButtonMovedLeft"
                            EnableViewState="false" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <cms:LocalizedRadioButton ID="radSiteMembers" runat="server" GroupName="contentaccess"
                            ResourceString="group.group.accesssitemembers" CssClass="RadioButtonMovedLeft"
                            EnableViewState="false" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <cms:LocalizedRadioButton ID="radGroupMembers" runat="server" GroupName="contentaccess"
                            ResourceString="group.group.accessgroupmembers" CssClass="RadioButtonMovedLeft"
                            EnableViewState="false" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            <cms:LocalizedLabel ID="lblJoinLeave" AssociatedControlID="chkJoinLeave" runat="server"
                CssClass="FieldLabel" ResourceString="group.group.sendjoinleave" DisplayColon="true"
                EnableViewState="false" />
        </td>
        <td>
            <asp:CheckBox ID="chkJoinLeave" runat="server" EnableViewState="false" />
        </td>
    </tr>
    <tr>
        <td>
            <cms:LocalizedLabel ID="lblWaitingForApproval" AssociatedControlID="chkWaitingForApproval"
                runat="server" CssClass="FieldLabel" ResourceString="group.group.sendwaitingforapproval"
                DisplayColon="true" EnableViewState="false" />
        </td>
        <td>
            <asp:CheckBox ID="chkWaitingForApproval" runat="server" EnableViewState="false" />
        </td>
    </tr>
    <asp:PlaceHolder ID="plcCreatedBy" runat="server" Visible="false">
        <tr>
            <td>
                <cms:LocalizedLabel ID="lblCreatedByTitle" runat="server" CssClass="FieldLabel" ResourceString="group.group.createdby"
                    DisplayColon="true" EnableViewState="false" />
            </td>
            <td>
                <cms:LocalizedLabel ID="lblCreatedByValue" runat="server" />
            </td>
        </tr>
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="plcApprovedBy" runat="server" Visible="false">
        <tr>
            <td>
                <cms:LocalizedLabel ID="lblApprovedByTitle" runat="server" CssClass="FieldLabel"
                    ResourceString="group.group.approvedby" DisplayColon="true" EnableViewState="false" />
            </td>
            <td>
                <cms:LocalizedLabel ID="lblApprovedByValue" runat="server" />
            </td>
        </tr>
    </asp:PlaceHolder>
    <asp:PlaceHolder runat="server" ID="plcOnline" Visible="false">
    <tr>
        <td class="FieldLabel">
            <cms:LocalizedLabel ID="lblLogActivity" runat="server" EnableViewState="false" ResourceString="group.group.logactivity"
                DisplayColon="true" AssociatedControlID="chkLogActivity" />
        </td>
        <td>
            <asp:CheckBox ID="chkLogActivity" runat="server" Checked="true" />
        </td>
    </tr>
    </asp:PlaceHolder>
    <tr>
        <td>
            &nbsp;
        </td>
        <td>
            <cms:CMSButton ID="btnSave" runat="server" CssClass="SubmitButton" EnableViewState="false"
                OnClick="btnSave_Click" ValidationGroup="GroupEdit" />
        </td>
    </tr>
</table>
