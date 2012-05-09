<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_Content_Controls_Dialogs_FileSystemSelector_Menu"
    CodeFile="Menu.ascx.cs" %>
<%@ Register Src="~/CMSAdminControls/UI/PageElements/Help.ascx" TagName="Help" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/Content/Controls/Attachments/DirectFileUploader/DirectFileUploader.ascx"
    TagName="DirectFileUploader" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/Content/Controls/Dialogs/LinkMediaSelector/MenuButton.ascx"
    TagName="MenuButton" TagPrefix="cms" %>
<asp:Panel ID="pnlActions" runat="server" CssClass="DialogMenu">
    <cms:CMSUpdatePanel ID="pnlUpdateActionsMenu" runat="server" UpdateMode="Conditional"
        ChildrenAsTriggers="false">
        <ContentTemplate>
            <asp:PlaceHolder ID="plcLeft" runat="server">
                <asp:Panel runat="server" ID="pnlLeft" CssClass="DialogMenuLeft">
                    <div class="LeftAlign">
                        <cms:MenuButton ID="menuBtnNewFolder" runat="server" CssClass="MenuItemLeft" CssHoverClass="MenuItemLeftOver"
                            CssDisabledClass="DialogsFolderDisabled" />
                    </div>
                    <div class="LeftAlign DeleteFolder">
                        <cms:MenuButton ID="menuBtnDeleteFolder" runat="server" CssClass="MenuItemLeft" CssHoverClass="MenuItemLeftOver"
                            CssDisabledClass="DialogsDeleteFolderDisabled" />
                    </div>
                </asp:Panel>
            </asp:PlaceHolder>
        </ContentTemplate>
    </cms:CMSUpdatePanel>
    <asp:Panel runat="server" ID="pnlRight" CssClass="DialogMenuRight">
        <asp:PlaceHolder ID="plcDirectFileUploader" runat="server">
            <div class="LeftAlign DialogFileUploader">
                <div id="dialogsUploaderDiv">
                    <cms:DirectFileUploader ID="fileUploader" runat="server" InsertMode="true" IncludeNewItemInfo="true"
                        CheckPermissions="false" ImageWidth="110" ImageHeight="36" InnerLoadingDivHtml="&nbsp;" />
                </div>
                <div id="dialogsUploaderDisabledDiv" class="DialogsUploaderDisabled" style="display: none;">
                    <img id="imgUploaderDisabled" runat="server" /><div class="DialogsFileDisabledTitle">
                        New file</div>
                </div>
            </div>
        </asp:PlaceHolder>
    </asp:Panel>
    <asp:Panel runat="server" ID="pnlHelp" CssClass="DialogMenuHelp" EnableViewState="false">
        <cms:Help ID="helpElem" runat="server" TopicName="dialogs_content" />
    </asp:Panel>
    <asp:Literal ID="ltlScript" runat="server" EnableViewState="false"></asp:Literal>
    <asp:HiddenField ID="hdnLastSelectedTab" runat="server" />
</asp:Panel>
