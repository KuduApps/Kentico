<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSAdminControls_SpellChecker_SpellCheck" CodeFile="SpellCheck.ascx.cs" %>
<asp:Panel ID="SpellingBody" Style="margin: 0px" runat="server">
    <asp:HiddenField ID="WordIndex" runat="server" Value="0" />
    <asp:HiddenField ID="CurrentText" runat="server" />
    <asp:HiddenField ID="IgnoreList" runat="server" />
    <asp:HiddenField ID="ReplaceKeyList" runat="server" />
    <asp:HiddenField ID="ReplaceValueList" runat="server" />
    <asp:HiddenField ID="ElementIndex" runat="server" Value="-1" />
    <asp:HiddenField ID="SpellMode" runat="server" Value="load" />
    <asp:Panel runat="server" ID="SuggestionForm" CssClass="PageContent">
        <asp:Label ID="lblError" runat="server" EnableViewState="false" CssClass="ErrorLabel"
            Visible="false" />
        <table cellspacing="0" cellpadding="2" width="100%">
            <tbody>
                <tr>
                    <td style="width: 250px">
                        <em>
                            <asp:Label ID="lblNotInDictionary" runat="server" /></em>
                    </td>
                    <td>
                        <cms:CMSButton ID="IgnoreButton" OnClick="IgnoreButton_Click" runat="server" EnableViewState="False"
                            Enabled="False" CssClass="ContentButton"></cms:CMSButton>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="CurrentWord" runat="server" Font-Bold="True" ForeColor="Red" />
                    </td>
                    <td>
                        <cms:CMSButton ID="IgnoreAllButton" OnClick="IgnoreAllButton_Click" runat="server"
                            EnableViewState="False" Enabled="False" CssClass="ContentButton"></cms:CMSButton>
                    </td>
                </tr>
                <tr>
                    <td>
                        <em>
                            <asp:Label ID="lblChangeTo" runat="server" /></em>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        <cms:CMSTextBox ID="ReplacementWord" runat="server" EnableViewState="False" Enabled="False"
                            CssClass="TextBoxField" Columns="30" Width="230px" />
                    </td>
                    <td>
                        <cms:LocalizedButton ID="btnAdd" OnClick="AddButton_Click" runat="server" EnableViewState="False"
                            Enabled="False" CssClass="ContentButton" ResourceString="general.add" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <em>
                            <asp:Label ID="lblSuggestions" runat="server" /></em>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td rowspan="5">
                        <asp:ListBox ID="Suggestions" runat="server" EnableViewState="False" Enabled="False"
                            CssClass="ContentListBoxLow" Width="230px"></asp:ListBox>
                    </td>
                    <td>
                        <cms:CMSButton ID="ReplaceButton" OnClick="ReplaceButton_Click" runat="server" EnableViewState="False"
                            Enabled="False" CssClass="ContentButton"></cms:CMSButton>
                    </td>
                </tr>
                <tr>
                    <td>
                        <cms:CMSButton ID="ReplaceAllButton" OnClick="ReplaceAllButton_Click" runat="server"
                            EnableViewState="False" Enabled="False" CssClass="ContentButton"></cms:CMSButton>
                    </td>
                </tr>
                <tr>
                    <td>
                        <cms:CMSButton ID="RemoveButton" OnClick="RemoveButton_Click" runat="server" EnableViewState="False"
                            Enabled="False" CssClass="ContentButton"></cms:CMSButton>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        <cms:CMSButton ID="btnCancel" OnClientClick="closeWindow()" runat="server" EnableViewState="False"
                            CssClass="SubmitButton"></cms:CMSButton>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Label ID="StatusText" runat="Server" ForeColor="DimGray" Font-Size="8pt" />
                    </td>
                </tr>
            </tbody>
        </table>
    </asp:Panel>
    <%-- </form>--%>
</asp:Panel>
