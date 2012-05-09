<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_SmartSearch_Controls_UI_SearchIndex_Forum_Edit" CodeFile="SearchIndex_Forum_Edit.ascx.cs" %>
<%@ Register Src="~/CMSFormControls/Sites/SiteSelector.ascx" TagName="SelectSite" TagPrefix="cms" %>
<cms:CMSUpdatePanel runat="server" ID="pnlForumEdit" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
            Visible="false" />
        <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
            Visible="false" />
        <asp:Panel runat="server" ID="pnlDisabled" CssClass="DisabledInfoPanel" Visible="false">
        <cms:LocalizedLabel runat="server" ID="lblDisabled" EnableViewState="false" ResourceString="srch.searchdisabledinfo"
            CssClass="InfoLabel"></cms:LocalizedLabel>
        </asp:Panel>
        <table>
            <tr>
                <td class="FieldLabel">
                    <cms:LocalizedLabel runat="server" ID="lblSite" EnableViewState="false" ResourceString="srch.index.site"
                        DisplayColon="true" />
                </td>
                <td>
                    <cms:SelectSite IsLiveSite="false" ID="selSite" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="FieldLabel">
                    <cms:LocalizedLabel runat="server" ID="lblForum" EnableViewState="false" ResourceString="srch.index.forum"
                        DisplayColon="true" />
                </td>
                <td>
                    <asp:PlaceHolder runat="server" ID="plcForumSelector" />
                </td>
            </tr>
            <asp:PlaceHolder runat="server" ID="plcForumsInfo">
                <tr>
                    <td>
                    </td>
                    <td>
                        <cms:LocalizedLabel runat="server" ID="lblClassNamesInfo" ResourceString="srch.index.forumsinfo"
                            CssClass="ContentLabelItalic"></cms:LocalizedLabel>
                    </td>
                </tr>
            </asp:PlaceHolder>
            <tr>
                <td>
                </td>
                <td>
                    <br />
                    <cms:LocalizedButton runat="server" ID="btnOk" OnClick="btnOK_Click" EnableViewState="false"
                        CssClass="SubmitButton" ResourceString="General.OK" />
                </td>
            </tr>
        </table>
    </ContentTemplate>
</cms:CMSUpdatePanel>
