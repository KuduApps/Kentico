<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_MediaLibrary_Controls_MediaLibrary_MediaFileSingleImport"
    CodeFile="MediaFileSingleImport.ascx.cs" %>
<%@ Register Src="~/CMSFormControls/System/LocalizableTextBox.ascx" TagName="LocalizableTextBox"
    TagPrefix="cms" %>
<cms:JQueryTabContainer ID="pnlTabs" runat="server" CssClass="Dialog_Tabs">
    <cms:JQueryTab ID="tabImport" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlNewFileContent" runat="server">
                <div class="PageHeaderLine">
                    <asp:Image ID="imgNewInfo" runat="server" EnableViewState="false" ImageAlign="AbsMiddle" />&nbsp;
                    <asp:Label ID="lblNewInfo" runat="server" EnableViewState="false" />
                </div>
                <div style="padding: 5px;">
                    <cms:CMSUpdatePanel ID="pnlUpdate" runat="server">
                        <ContentTemplate>
                            <table width="100%">
                                <tr>
                                    <td colspan="3">
                                        <asp:Label ID="lblErrorNew" runat="server" Visible="false" CssClass="ErrorLabel"
                                            EnableViewState="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <cms:LocalizedLabel ID="lblNewFileName" runat="server" EnableViewState="false" ResourceString="general.filename"
                                            DisplayColon="true" />
                                    </td>
                                    <td>
                                        <cms:CMSTextBox ID="txtNewFileName" runat="server" CssClass="TextBoxField" MaxLength="250"
                                            EnableViewState="false" />
                                    </td>
                                    <td style="width: 100%;">
                                        <cms:CMSRequiredFieldValidator ID="rfvNewFileName" runat="server" ControlToValidate="txtNewFileName"
                                            ValidationGroup="NewFileValidation" Display="Dynamic" EnableViewState="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <cms:LocalizedLabel ID="lnlNewFileTitle" runat="server" EnableViewState="false" ResourceString="media.file.filetitle"
                                            DisplayColon="false" />
                                    </td>
                                    <td style="white-space:nowrap;">
                                        <cms:LocalizableTextBox ID="txtNewFileTitle" runat="server" CssClass="TextBoxField"
                                            MaxLength="250" EnableViewState="false" />
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top;">
                                        <cms:LocalizedLabel ID="lblNewDescription" runat="server" EnableViewState="false"
                                            ResourceString="general.description" DisplayColon="true" />
                                    </td>
                                    <td style="white-space:nowrap;">
                                        <cms:LocalizableTextBox ID="txtNewDescripotion" runat="server" CssClass="TextAreaField"
                                            TextMode="MultiLine" EnableViewState="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <cms:LocalizedButton ID="btnNew" runat="server" CssClass="ContentButton" OnClick="btnNew_Click"
                                            ValidationGroup="NewFileValidation" EnableViewState="false" ResourceString="general.import" />
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </cms:CMSUpdatePanel>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </cms:JQueryTab>
</cms:JQueryTabContainer>
