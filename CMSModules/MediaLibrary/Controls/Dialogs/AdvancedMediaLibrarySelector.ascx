<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_MediaLibrary_Controls_Dialogs_AdvancedMediaLibrarySelector" CodeFile="AdvancedMediaLibrarySelector.ascx.cs" %>
<%@ Register Src="~/CMSFormControls/Sites/SiteSelector.ascx" TagName="SiteSelector" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/MediaLibrary/FormControls/MediaLibrarySelector.ascx"
    TagName="LibrarySelector" TagPrefix="cms" %>
<table>
    <tr>
        <td>
            <cms:LocalizedLabel ID="lblSite" runat="server" DisplayColon="true" EnableViewState="false"
                ResourceString="general.site" CssClass="FieldLabel"></cms:LocalizedLabel>
        </td>
        <td>
            <cms:SiteSelector ID="siteSelector" runat="server" IsLiveSite="false" AllowAll="false" AllowEmpty="false"
                StopProcessing="true" UseCodeNameForSelection="false" OnlyRunningSites="true" />
        </td>
    </tr>
    <asp:PlaceHolder ID="plcGroupSelector" runat="server" Visible="true">
        <tr>
            <td>
                <cms:LocalizedLabel ID="lblGroup" runat="server" DisplayColon="true" EnableViewState="false"
                    ResourceString="general.group" CssClass="FieldLabel"></cms:LocalizedLabel>
            </td>
            <td>
                <asp:PlaceHolder ID="pnlGroupSelector" runat="server"></asp:PlaceHolder>
            </td>
        </tr>
    </asp:PlaceHolder>
    <tr>
        <td>
            <cms:LocalizedLabel ID="lblLibrary" runat="server" DisplayColon="true" EnableViewState="false"
                ResourceString="dialogs.media.library" CssClass="FieldLabel"></cms:LocalizedLabel>
        </td>
        <td>
            <cms:LibrarySelector ID="librarySelector" runat="server" NoneWhenEmpty="true" AddCurrentLibraryRecord="false"
                UseAutoPostBack="true" OnSelectedLibraryChanged="librarySelector_SelectedLibraryChanged"
                UseLibraryNameForSelection="false" />
        </td>
    </tr>
</table>
