<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_AdminControls_Controls_Class_Layout" CodeFile="Layout.ascx.cs" %>
<asp:Panel ID="pnlActions" runat="server" CssClass="PageHeaderLine" EnableViewState="false">
    <asp:LinkButton ID="lnkSave" runat="server" OnClientClick=" return CheckContent(); "
        CssClass="ContentSaveLinkButton">
        <asp:Image ID="imgSave" runat="server" CssClass="NewItemImage" />
        <%=mSave%>
    </asp:LinkButton>
</asp:Panel>
<asp:Panel ID="pnlContent" runat="server" CssClass="PageContent">
    <asp:Label ID="lblInfo" runat="server" CssClass="InfoLabel" EnableViewState="false"
        Visible="false" />
    <asp:CheckBox ID="chkCustomLayout" runat="server" AutoPostBack="true" OnCheckedChanged="chkCustomLayout_CheckedChanged" />
    <br />
    <asp:Panel ID="pnlCustomLayout" runat="server" Visible="false">
        <table>
            <tr>
                <td colspan="2" class="GenerateButtonPadding">
                    <cms:CMSButton ID="btnGenerateLayout" runat="server" CssClass="XLongButton" OnClientClick="SetContent(GenerateTableLayout()); return false;"
                        EnableViewState="false" />
                </td>
            </tr>
            <tr>
                <td rowspan="2" style="width: 550px; vertical-align: top;">
                    <cms:CMSHtmlEditor ID="htmlEditor" runat="server" Width="550px" Height="340px" />
                </td>
                <td style="vertical-align: top;padding-left: 7px;" class="RightColumn">
                    <asp:Label ID="lblAvailableFields" runat="server" EnableViewState="false" CssClass="AvailableFieldsTitle" />
                    <asp:ListBox ID="lstAvailableFields" runat="server" CssClass="FieldsList" Width="152px" />
                </td>
            </tr>
            <tr>
                <td style="vertical-align: bottom; padding-bottom: 3px;" class="RightColumn">
                    <table cellspacing="0" cellpadding="1">
                        <tr>
                            <td>
                                <cms:CMSButton ID="btnInsertLabel" runat="server" CssClass="LongButton" OnClientClick="InsertAtCursorPosition('$$label:' + lstAvailFieldsElem.value + '$$'); return false;"
                                    EnableViewState="false" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <cms:CMSButton ID="btnInsertInput" runat="server" CssClass="LongButton" OnClientClick="InsertAtCursorPosition('$$input:' + lstAvailFieldsElem.value + '$$'); return false;"
                                    EnableViewState="false" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <cms:CMSButton ID="btnInsertValLabel" runat="server" CssClass="LongButton" OnClientClick="InsertAtCursorPosition('$$validation:' + lstAvailFieldsElem.value + '$$'); return false;"
                                    EnableViewState="false" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <cms:CMSButton ID="btnInsertSubmitButton" runat="server" CssClass="LongButton" OnClientClick="InsertAtCursorPosition('$$submitbutton$$'); return false;"
                                    EnableViewState="false" />
                            </td>
                        </tr>
                        <asp:PlaceHolder runat="server" ID="plcVisibility" EnableViewState="false" Visible="false">
                            <tr>
                                <td>
                                    <cms:CMSButton ID="btnInsertVisibility" runat="server" CssClass="LongButton" OnClientClick="InsertAtCursorPosition('$$visibility:' + lstAvailFieldsElem.value + '$$'); return false;"
                                        EnableViewState="false" />
                                </td>
                            </tr>
                        </asp:PlaceHolder>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Panel>
<asp:Literal ID="ltlConfirmDelete" runat="server" EnableViewState="false" />
<asp:Literal ID="ltlAlertExist" runat="server" EnableViewState="false" />
<asp:Literal ID="ltlAlertExistFinal" runat="server" EnableViewState="false" />
<asp:Literal ID="ltlAvailFieldsElement" runat="server" EnableViewState="false" />
<asp:Literal ID="ltlHtmlEditorID" runat="server" EnableViewState="false" />