<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_MediaLibrary_Controls_MediaLibrary_FolderActions_EditFolder" CodeFile="EditFolder.ascx.cs" %>
<asp:Panel ID="pnlFolderEdit" runat="server" EnableViewState="false">
    <cms:JQueryTabContainer ID="pnlTabs" runat="server" CssClass="Dialog_Tabs">
        <cms:JQueryTab ID="tabGeneral" runat="server">
            <ContentTemplate>
                <asp:Panel ID="pnlContent" runat="server">
                    <table width="100%">
                        <tr>
                            <td colspan="2" class="FolderEditInfoArea">
                                <cms:LocalizedLabel ID="lblInfo" runat="server" DisplayColon="false" Visible="false"
                                    CssClass="InfoLabel" EnableViewState="false"></cms:LocalizedLabel>
                                <cms:LocalizedLabel ID="lblError" runat="server" CssClass="ErrorLabel" DisplayColon="false"
                                    EnableViewState="false" Visible="false"></cms:LocalizedLabel>
                            </td>
                        </tr>
                        <tr>
                            <td style="white-space: nowrap;" class="FolderEditLabelArea">
                                <cms:LocalizedLabel ID="lblFolderName" runat="server" CssClass="FieldLabel" DisplayColon="true"
                                    ResourceString="media.folder.foldername" EnableViewState="false"></cms:LocalizedLabel>
                            </td>
                            <td style="width: 100%">
                                <cms:CMSTextBox ID="txtFolderName" runat="server" CssClass="TextBoxField" MaxLength="50"
                                    EnableViewState="false" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <cms:CMSRequiredFieldValidator ID="rfvFolderName" runat="server" Display="Dynamic" ControlToValidate="txtFolderName"
                                    ValidationGroup="btnOk" EnableViewState="false"></cms:CMSRequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td colspan="2">
                                <div id="divButtons" runat="server" enableviewstate="false" style="padding-top: 8px;">
                                    <cms:CMSButton ID="btnOk" runat="server" ValidationGroup="btnOk" OnClick="btnOk_Click"
                                        CssClass="SubmitButton" EnableViewState="false" />
                                    <asp:PlaceHolder ID="plcCancelArea" runat="server">
                                        <cms:CMSButton ID="btnCancel" runat="server" OnClick="btnCancel_Click" CssClass="SubmitButton"
                                            EnableViewState="false" />
                                    </asp:PlaceHolder>
                                </div>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </ContentTemplate>
        </cms:JQueryTab>
    </cms:JQueryTabContainer>
    <asp:Literal ID="ltlScript" runat="server" EnableViewState="false" />
</asp:Panel>
