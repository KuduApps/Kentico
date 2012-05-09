<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_Content_FormControls_Tags_TagSelector"
    CodeFile="TagSelector.ascx.cs" %>
<asp:Panel ID="pnlTagSelector" runat="server" DefaultButton="btnHidden">
    <table cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <cms:CMSTextBox ID="txtTags" runat="server" EnableViewState="true" CssClass="TextBoxField" />
            </td>
            <td>
                <cms:CMSButton ID="btnSelect" runat="server" EnableViewState="false" CssClass="ContentButton" />
            </td>
        </tr>
    </table>
    <ajaxToolkit:AutoCompleteExtender runat="server" ID="autoComplete" TargetControlID="txtTags"
        ServiceMethod="TagsAutoComplete" ServicePath="TagSelectorService.asmx" MinimumPrefixLength="1"
        CompletionInterval="1000" EnableCaching="true" CompletionSetCount="20" DelimiterCharacters=", "
        UseContextKey="true" CompletionListCssClass="autocomplete_completionListElement"
        CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
        OnClientItemSelected="itemSelected" OnClientShown="resetPosition">
    </ajaxToolkit:AutoCompleteExtender>
    <div>
        <cms:LocalizedLabel ID="lblInfoText" runat="server" ResourceString="tags.tagsselector.examples"
            Font-Italic="true" EnableViewState="false" />
    </div>
    <cms:CMSButton ID="btnHidden" runat="server" EnableViewState="false" Style="display: none;"
        OnClientClick="return false;" />
</asp:Panel>
