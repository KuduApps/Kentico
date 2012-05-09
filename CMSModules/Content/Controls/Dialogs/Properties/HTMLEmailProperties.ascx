<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_Content_Controls_Dialogs_Properties_HTMLEmailProperties" CodeFile="HTMLEmailProperties.ascx.cs" %>
<%@ Register src="~/CMSAdminControls/UI/PageElements/Help.ascx" tagname="Help" tagprefix="cms" %>
<%@ Register Src="~/CMSModules/Content/Controls/Dialogs/General/WidthHeightSelector.ascx" TagPrefix="cms"
    TagName="WidthHeightSelector" %>

<script type="text/javascript" language="javascript">
    function insertItem() {
        RaiseHiddenPostBack();
    }      
</script>

<asp:Literal ID="ltlScript" runat="server" EnableViewState="false" />

<div class="HTMLEmailProperties">
    <div class="LeftAlign" style="width: 600px;">
        <cms:CMSUpdatePanel ID="plnEmailUpdate" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
                    Visible="false" />
                <table style="vertical-align: top; white-space: nowrap;" width="100%">
                    <asp:PlaceHolder runat="server" ID="plcLinkText">
                        <tr>
                            <td>
                                <cms:LocalizedLabel ID="lblLinkText" runat="server" EnableViewState="false" ResourceString="dialogs.link.text"
                                    DisplayColon="true" />
                            </td>
                            <td>
                                <cms:CMSTextBox runat="server" ID="txtLinkText" CssClass="VeryLongTextBox" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                &nbsp;
                            </td>
                        </tr>
                    </asp:PlaceHolder>
                    <tr>
                        <td>
                            <cms:LocalizedLabel ID="lblTo" runat="server" EnableViewState="false" ResourceString="dialogs.email.to"
                                DisplayColon="true" />
                        </td>
                        <td>
                            <cms:CMSTextBox runat="server" ID="txtTo" CssClass="VeryLongTextBox" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <cms:LocalizedLabel ID="lblCc" runat="server" EnableViewState="false" ResourceString="dialogs.email.cc"
                                DisplayColon="true" />
                        </td>
                        <td>
                            <cms:CMSTextBox runat="server" ID="txtCc" CssClass="VeryLongTextBox" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <cms:LocalizedLabel ID="lblBcc" runat="server" EnableViewState="false" ResourceString="dialogs.email.bcc"
                                DisplayColon="true" />
                        </td>
                        <td>
                            <cms:CMSTextBox runat="server" ID="txtBcc" CssClass="VeryLongTextBox" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <cms:LocalizedLabel ID="lblSubject" runat="server" EnableViewState="false" ResourceString="dialogs.email.subject"
                                DisplayColon="true" />
                        </td>
                        <td>
                            <cms:CMSTextBox runat="server" ID="txtSubject" CssClass="VeryLongTextBox" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr style="vertical-align: top;">
                        <td>
                            <cms:LocalizedLabel ID="lblBody" runat="server" EnableViewState="false" ResourceString="dialogs.email.body"
                                DisplayColon="true" />
                        </td>
                        <td>
                            <cms:CMSTextBox runat="server" ID="txtBody" CssClass="TextAreaBody" TextMode="MultiLine" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </cms:CMSUpdatePanel>
        <div class="Hidden">
            <cms:CMSUpdatePanel ID="plnEmailButtonsUpdate" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <cms:CMSButton ID="hdnButton" runat="server" OnClick="hdnButton_Click" CssClass="HiddenButton" EnableViewState="false" />
                    <cms:CMSButton ID="hdnButtonUpdate" runat="server" OnClick="hdnButtonUpdate_Click" CssClass="HiddenButton" EnableViewState="false" />
                </ContentTemplate>
            </cms:CMSUpdatePanel>
        </div>
    </div>
    <div class="RightAlign">
        <cms:Help ID="helpElem" runat="server" TopicName="dialogs_link_email" />
    </div>
</div>
