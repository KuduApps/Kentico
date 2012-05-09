<%@ Page Language="C#" AutoEventWireup="true"
    EnableEventValidation="false" MasterPageFile="~/CMSMasterPages/UI/EmptyPage.master"
    Title="Media library - Files" Inherits="CMSModules_MediaLibrary_Tools_Library_Edit_Files"
    Theme="Default" CodeFile="Library_Edit_Files.aspx.cs" %>

<%@ Register Src="~/CMSModules/MediaLibrary/Controls/MediaLibrary/MediaLibrary.ascx"
    TagName="MediaLibrary" TagPrefix="cms" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <ajaxToolkit:ToolkitScriptManager ID="scriptManager" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <asp:Panel runat="server" ID="pnlTabsBody" CssClass="TabsPageBody LightTabs">
        <asp:Panel runat="server" ID="pnlTabsScroll" CssClass="TabsPageScrollArea">
            <asp:Panel runat="server" ID="pnltab" CssClass="tabspagecontent">
                <cms:CMSUpdatePanel ID="pnlUpdateSelectMedia" runat="server" UpdateMode="Conditional"
                    ChildrenAsTriggers="false">
                    <ContentTemplate>
                        <cms:MediaLibrary ID="libraryElem" ShortID="l" runat="server" DisplayFilesCount="true" IsLiveSite="false" DisplayMode="Simple" />
                    </ContentTemplate>
                </cms:CMSUpdatePanel>
            </asp:Panel>
        </asp:Panel>
    </asp:Panel>

    <script type="text/javascript">
            //<![CDATA[
            function Refresh() {
              window.location.replace(window.location);
            }
            //]]>
    </script>

</asp:Content>
