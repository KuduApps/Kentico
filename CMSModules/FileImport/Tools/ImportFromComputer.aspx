<%@ Page Title="Tools - File import - Import from computer" Language="C#" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master"
    AutoEventWireup="true" CodeFile="ImportFromComputer.aspx.cs" Inherits="CMSModules_FileImport_Tools_ImportFromComputer"
    Theme="Default" %>

<%@ Register Src="~/CMSAdminControls/Silverlight/MultiFileUploader/MultiFileUploader.ascx"
    TagName="MultiFileUploader" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/Content/FormControls/Documents/SelectSinglePath.ascx"
    TagName="SelectSinglePath" TagPrefix="cms" %>
<%@ Register Src="~/CMSFormControls/Cultures/SiteCultureSelector.ascx" TagName="SiteCultureSelector"
    TagPrefix="cms" %>
<asp:Content ID="Content1" ContentPlaceHolderID="plcBeforeBody" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="plcControls" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="plcActions" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="plcBeforeContent" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="plcContent" runat="server">
    <asp:Panel ID="pnlImportControls" runat="server">
        <cms:CMSUpdatePanel ID="pnlSelectors" runat="server">
            <ContentTemplate>
                <table border="0">
                    <tr>
                        <td>
                            <cms:LocalizedLabel ID="LocalizedLabel1" runat="server" ResourceString="Tools.FileImport.TargetAliasPath"
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
                                UseCultureCode="true" IsLiveSite="false" PostbackOnChange="false" />
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
                </table>
            </ContentTemplate>
        </cms:CMSUpdatePanel>
    </asp:Panel>
    <cms:MultiFileUploader ID="uploaderElem" runat="server" UploadMode="Grid" Multiselect="true"
        SourceType="Content" Width="100%">
        <AlternateContent>
            <cms:LocalizedLabel ID="lblNoSilverlight" runat="server" ResourceString="fileimport.nosilverlight"
                CssClass="InfoLabel" />
        </AlternateContent>
    </cms:MultiFileUploader>
</asp:Content>
