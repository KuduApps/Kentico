<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master"
    Title="Tools - File import - Import from server" Inherits="CMSModules_FileImport_Tools_ImportFromServer"
    Theme="Default" CodeFile="ImportFromServer.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/PageElements/PageTitle.ascx" TagName="PageTitle"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/Content/FormControls/Documents/SelectSinglePath.ascx"
    TagName="SelectSinglePath" TagPrefix="cms" %>
<%@ Register Src="~/CMSFormControls/Cultures/SiteCultureSelector.ascx" TagName="SiteCultureSelector"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/AsyncControl.ascx" TagName="AsyncControl" TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/AsyncBackground.ascx" TagName="AsyncBackground"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/UI/UniGrid/Filters/TextSimpleFilter.ascx" TagName="TextFilter"
    TagPrefix="cms" %>

<asp:Content runat="server" ContentPlaceHolderID="plcActions" ID="plcActions">
<asp:Image ID="imgExternalStoragePrepare" runat="server" CssClass="NewItemImage" EnableViewState="false" />
<asp:LinkButton ID="lnkExternalStoragePrepare" CssClass="NewItemLink" runat="server" EnableViewState="false" OnClick="lnkExternalStoragePrepare_Click">LinkButton</asp:LinkButton>
</asp:Content>
<asp:Content ContentPlaceHolderID="plcBeforeBody" runat="server" ID="cntBeforeBody">
    <asp:Panel runat="server" ID="pnlLog" Visible="false">
        <cms:AsyncBackground ID="backgroundElem" runat="server" />
        <div class="AsyncLogArea">
            <div>
                <asp:Panel ID="pnlAsyncBody" runat="server" CssClass="PageBody">
                    <asp:Panel ID="pnlTitleAsync" runat="server" CssClass="PageHeader">
                        <cms:PageTitle ID="titleElemAsync" runat="server" />
                    </asp:Panel>
                    <asp:Panel ID="pnlCancel" runat="server" CssClass="PageHeaderLine">
                        <cms:CMSButton runat="server" ID="btnCancel" CssClass="SubmitButton" />
                    </asp:Panel>
                    <asp:Panel ID="pnlAsyncContent" runat="server" CssClass="PageContent">
                        <cms:AsyncControl ID="ctlAsync" runat="server" />
                    </asp:Panel>
                </asp:Panel>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:Panel runat="server" ID="pnlContent" CssClass="PageContent">
        <asp:Literal ID="ltlScript" runat="server" EnableViewState="false" />
        <input type="hidden" id="targetNodeId" name="targetNodeId" />
        <cms:LocalizedLabel runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
            Visible="false" />
        <cms:LocalizedLabel runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
            Visible="false" />
        <asp:PlaceHolder ID="plcImportContent" runat="server">
            <asp:Panel ID="pnlTitle" runat="server">
                <asp:Label ID="lblTitle" runat="server" EnableViewState="false" /><br />
                <br />
            </asp:Panel>
            <asp:PlaceHolder ID="plcImportList" runat="server">
                <asp:HiddenField ID="hdnSelected" runat="server" />
                <asp:HiddenField ID="hdnValue" runat="server" />
                <asp:Panel ID="pnlGrid" runat="server">
                    <table class="FileImportFilter">
                        <tr>
                            <td class="FieldLabel">
                                <cms:LocalizedLabel ID="lblFilter" ResourceString="general.name" AssociatedControlID="ucFilter"
                                    runat="server" />:
                            </td>
                            <td>
                                <cms:TextFilter runat="server" ID="ucFilter" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <asp:Button ID="btnFilter" runat="server" />
                            </td>
                        </tr>
                    </table>
                    <cms:UniGrid ID="gridImport" runat="server" GridName="FileImport.xml" DelayedReload="true" />
                </asp:Panel>
                <br />
                <asp:Panel ID="pnlCount" runat="server">
                    <b>
                        <asp:Label ID="lblTotal" runat="server" EnableViewState="false" />
                        &nbsp;
                        <asp:Label ID="lblSelected" runat="server" EnableViewState="false" />&nbsp;<asp:Label
                            ID="lblSelectedValue" runat="server" EnableViewState="false" />
                    </b>
                </asp:Panel>
                <cms:FileSystemDataSource ID="fileSystemDataSource" runat="server" IncludeSubDirs="true"
                    CacheMinutes="0" />
            </asp:PlaceHolder>
            <br />
            <br />
            <asp:Panel ID="pnlImportControls" runat="server">
                <table border="0">
                    <tr>
                        <td>
                            <cms:LocalizedLabel ID="lblTargetAliasPath" runat="server" ResourceString="Tools.FileImport.TargetAliasPath"
                                DisplayColon="true" EnableViewState="false" />
                        </td>
                        <td colspan="2">
                            <cms:SelectSinglePath runat="server" ID="pathElem" IsLiveSite="false" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <cms:LocalizedLabel ID="lblSelectCulture" runat="server" ResourceString="general.culture"
                                DisplayColon="true" EnableViewState="false" />
                        </td>
                        <td colspan="2">
                            <cms:SiteCultureSelector runat="server" ID="cultureSelector" AddDefaultRecord="false"
                                IsLiveSite="false" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <cms:LocalizedLabel ID="lblDeleteImported" runat="server" ResourceString="Tools.FileImport.DeleteImported"
                                DisplayColon="true" EnableViewState="false" />
                        </td>
                        <td colspan="2">
                            <asp:CheckBox ID="chkDeleteImported" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <cms:LocalizedLabel ID="lblIncludeExtension" runat="server" ResourceString="Tools.FileImport.RemoveExtension"
                                DisplayColon="true" EnableViewState="false" />
                        </td>
                        <td colspan="2">
                            <asp:CheckBox ID="chkIncludeExtension" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td colspan="2">
                            <cms:LocalizedButton ID="btnStartImport" runat="server" ResourceString="Tools.FileImport.StartImport"
                                CssClass="LongSubmitButton" OnClick="btnStartImport_Click" EnableViewState="false" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:PlaceHolder>
    </asp:Panel>
</asp:Content>
