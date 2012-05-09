<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_SmartSearch_Controls_UI_SearchIndex_New" CodeFile="SearchIndex_New.ascx.cs" %>

<%@ Register Src="~/CMSFormControls/System/LocalizableTextBox.ascx" TagName="LocalizableTextBox" TagPrefix="cms" %>
<%@ Register src="SearchIndex_StopWordsCustomAnalyzer.ascx" tagname="StopCustomControl" tagprefix="cms" %>
<asp:Panel ID="pnlBody" runat="server">
    <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
        Visible="false" />
    <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
        Visible="false" />
    <asp:Panel runat="server" ID="pnlPathTooLong" CssClass="DisabledInfoPanel" Visible="false">
        <cms:LocalizedLabel runat="server" ID="lblPathTooLong" EnableViewState="false" ResourceString="srch.pathtoolong"
            CssClass="ErrorLabel"></cms:LocalizedLabel>
    </asp:Panel>
    <asp:Panel runat="server" ID="pnlContent" Visible="false">
        <table>
            <tr>
                <td class="FieldLabel">
                    <cms:LocalizedLabel runat="server" ID="lblDisplayName" EnableViewState="false" ResourceString="general.displayname"
                        DisplayColon="true" />
                </td>
                <td>
                    <cms:LocalizableTextBox ID="txtDisplayName" runat="server" CssClass="TextBoxField" MaxLength="200" />
                    <cms:CMSRequiredFieldValidator ID="rfvDisplayName" runat="server" ControlToValidate="txtDisplayName:textbox"
                        ValidationGroup="Required" />
                </td>
            </tr>
            <tr>
                <td class="FieldLabel">
                    <cms:LocalizedLabel runat="server" ID="lblCodeName" EnableViewState="false" ResourceString="general.codename"
                        DisplayColon="true" />
                </td>
                <td>
                    <cms:CMSTextBox ID="txtCodeName" runat="server" CssClass="TextBoxField" MaxLength="200" />
                    <cms:CMSRequiredFieldValidator ID="rfvCodeName" runat="server" ControlToValidate="txtCodeName"
                        ValidationGroup="Required" />
                </td>
            </tr>
            <tr>
                <td class="FieldLabel">
                    <cms:LocalizedLabel runat="server" ID="lblType" EnableViewState="false" ResourceString="srch.index.type"
                        DisplayColon="true" />
                </td>
                <td>
                    <asp:DropDownList ID="drpType" runat="server" CssClass="DropDownField">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="FieldLabel">
                    <cms:LocalizedLabel runat="server" ID="lblAnalyzer" EnableViewState="false" ResourceString="srch.index.analyzer"
                        DisplayColon="true" />
                </td>
                <td>
                    <asp:DropDownList ID="drpAnalyzer" runat="server" CssClass="DropDownFieldSmall">
                    </asp:DropDownList>
                </td>
            </tr>
            <cms:StopCustomControl runat="server" ID="stopCustomControl" />
            
            <%-- 
// Community indexing is not yet supported
      <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel runat="server" ID="lblCommunity" EnableViewState="false" ResourceString="srch.index.community" DisplayColon="true" />
            </td>
            <td>
                <asp:CheckBox ID="chkCommunity" runat="server" />
            </td>
        </tr> --%>
            <tr>
                <td>
                </td>
                <td>
                    <cms:LocalizedCheckBox runat="server" ID="chkAddIndexToCurrentSite" />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <cms:CMSButton runat="server" ID="btnOk" OnClick="btnOK_Click" EnableViewState="false"
                        CssClass="SubmitButton" ValidationGroup="Required" />
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Panel>
