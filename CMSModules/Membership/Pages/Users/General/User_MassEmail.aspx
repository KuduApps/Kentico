<%@ Page Language="C#" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" AutoEventWireup="true"
    Inherits="CMSModules_Membership_Pages_Users_General_User_MassEmail" Title="Mass email"
    Theme="Default" CodeFile="User_MassEmail.aspx.cs" %>

<%@ Register Src="~/CMSModules/Membership/FormControls/Users/selectuser.ascx" TagName="UserSelector"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/Membership/FormControls/Roles/selectrole.ascx" TagName="RoleSelector"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSFormControls/Sites/SiteSelector.ascx" TagName="SiteSelector"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/AdminControls/Controls/MetaFiles/FileUploader.ascx" TagName="FileUploader"
    TagPrefix="cms" %>
<asp:Content ID="cntSite" ContentPlaceHolderID="plcSiteSelector" runat="Server">
    <table>
        <tr>
            <td>
                <asp:Label ID="lblSite" runat="server" CssClass="FieldLabel" EnableViewState="false" />
            </td>
            <td>
                <cms:SiteSelector ID="siteSelector" runat="server" IsLiveSite="false" AllowAll="true" />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="cntBody" ContentPlaceHolderID="plcContent" runat="Server">
    <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
        Visible="false" />
    <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
        Visible="false" />
    <table>
        <tr>
            <td>
                <asp:Label ID="lblFrom" runat="server" CssClass="FieldLabel" />
            </td>
            <td style="width: 100%">
                <cms:CMSTextBox ID="txtFrom" runat="server" CssClass="TextBoxField" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblSubject" runat="server" CssClass="FieldLabel" />
            </td>
            <td style="width: 100%">
                <cms:CMSTextBox ID="txtSubject" runat="server" CssClass="TextBoxField" MaxLength="450" />
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td colspan="2">
                <br />
                <cms:LocalizedLabel ID="lblRecipients" runat="server" CssClass="FieldLabel" EnableViewState="false"
                    ResourceString="userlist.recipients" DisplayColon="true" />
                <cms:CMSUpdatePanel ID="pnlUpdate" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <cms:CMSAccordion ID="ajaxAccordion" runat="Server" CssClass="MassEmailSelector"
                            ContentCssClass="MassEmailSelectorSub" HeaderCssClass="MenuHeaderItem" HeaderSelectedCssClass="MenuHeaderItemSelected">
                            <Panes>
                                <cms:CMSAccordionPane ID="pnlAccordionUsers" ShortID="u" runat="server" CssClass="SelectorItem">
                                    <Header>
                                        <div class="HeaderInner">
                                            <cms:LocalizedLabel ID="lblUsers" runat="server" EnableViewState="false" ResourceString="general.users" />
                                        </div>
                                    </Header>
                                    <Content>
                                        <div class="ContentInner">
                                            <cms:UserSelector ID="users" ShortID="su" runat="server" SelectionMode="Multiple" />
                                        </div>
                                    </Content>
                                </cms:CMSAccordionPane>
                                <cms:CMSAccordionPane ID="pnlAccordionRoles" ShortID="r" runat="server" 
                                    CssClass="SelectorItem">
                                    <Header>
                                        <div class="HeaderInner">
                                            <cms:LocalizedLabel ID="lblRoles" runat="server" EnableViewState="false" ResourceString="general.roles" />
                                        </div>
                                    </Header>
                                    <Content>
                                        <div class="ContentInner">
                                            <cms:RoleSelector UserFriendlyMode="true" ID="roles" ShortID="sr" runat="server" IsLiveSite="false" />
                                        </div>
                                    </Content>
                                </cms:CMSAccordionPane>
                                <cms:CMSAccordionPane ID="pnlAccordionGroups" ShortID="g" runat="server" Visible="false"
                                    CssClass="SelectorItem">
                                    <Header>
                                        <div class="HeaderInner">
                                            <cms:LocalizedLabel ID="lblGroups" runat="server" EnableViewState="false" ResourceString="general.groups" />
                                        </div>
                                    </Header>
                                    <Content>
                                        <div class="ContentInner">
                                            <asp:PlaceHolder runat="server" ID="plcGroupSelector"></asp:PlaceHolder>
                                        </div>
                                    </Content>
                                </cms:CMSAccordionPane>
                            </Panes>
                        </cms:CMSAccordion>
                    </ContentTemplate>
                </cms:CMSUpdatePanel>
            </td>
        </tr>
        <asp:PlaceHolder runat="server" ID="plcText">
            <tr>
                <td colspan="2">
                    <br />
                    <cms:LocalizedLabel ID="lblText" runat="server" CssClass="FieldLabel" EnableViewState="false"
                        ResourceString="general.text" DisplayColon="true" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <cms:CMSHtmlEditor ID="htmlText" runat="server" Width="625" Height="400px" />
                </td>
            </tr>
        </asp:PlaceHolder>
        <asp:PlaceHolder runat="server" ID="plcPlainText">
            <tr>
                <td colspan="2">
                    <cms:LocalizedLabel ID="lblPlainText" runat="server" CssClass="FieldLabel" EnableViewState="false"
                        ResourceString="general.plaintext" DisplayColon="true" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <cms:CMSTextBox ID="txtPlainText" runat="server" CssClass="TextAreaLarge" TextMode="MultiLine" />
                </td>
            </tr>
        </asp:PlaceHolder>
        <tr>
            <td colspan="2">
                <asp:Panel ID="pnlAttachments" runat="server">
                    <cms:FileUploader ID="uploader" runat="server" />
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <br />
                <cms:CMSButton ID="btnSend" runat="server" CssClass="LongSubmitButton" />
            </td>
            <td class="RightAlign TextRight">
                <br />
                <cms:LocalizedButton ID="btnClear" runat="server" CssClass="LongSubmitButton" ResourceString="general.clear" OnClick="btnClear_Click"  Visible="false" EnableViewState="false" />
            </td>
        </tr>
    </table>
</asp:Content>
