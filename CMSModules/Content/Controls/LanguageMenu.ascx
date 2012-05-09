<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_Content_Controls_LanguageMenu"
    CodeFile="LanguageMenu.ascx.cs" %>
<%@ Register Src="~/CMSFormControls/Cultures/SiteCultureSelector.ascx" TagName="SiteCultureSelector"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/UI/UniMenu/UniMenuButtons.ascx" TagName="UniMenuButtons"
    TagPrefix="cms" %>
<div class="ActionButtons">
    <table cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <asp:Panel runat="server" ID="pnlLang" CssClass="ContentMenuLang">
                    <cms:SiteCultureSelector runat="server" ID="cultureSelector" ShortID="l" IsLiveSite="false" />
                </asp:Panel>
                <cms:UniMenuButtons ID="buttons" ShortID="b" runat="server" EnableViewState="false"
                    AllowSelection="true" CheckChanges="true" />
            </td>
            <td>
                <div class="ContentMenuSplitSeparator">
                </div>
            </td>
            <td>
                <div class="ContentMenuSplit">
                    <cms:UniMenuButtons ID="splitView" ShortID="s" runat="server" EnableViewState="false"
                        AllowToggle="true" CheckChanges="true" />
                </div>
            </td>
        </tr>
    </table>
</div>
