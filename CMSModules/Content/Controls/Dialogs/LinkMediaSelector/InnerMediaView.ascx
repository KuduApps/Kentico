<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_Content_Controls_Dialogs_LinkMediaSelector_InnerMediaView"
    CodeFile="InnerMediaView.ascx.cs" %>
<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/Content/Controls/Attachments/DirectFileUploader/DirectFileUploader.ascx"
    TagName="DirectFileUploader" TagPrefix="cms" %>
<%@ Register Src="~/CMSInlineControls/ImageControl.ascx" TagName="ImageControl" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/Content/Controls/Dialogs/LinkMediaSelector/PageSize.ascx"
    TagName="PageSize" TagPrefix="cms" %>

<script type="text/javascript">
    //<![CDATA[       
    // Confirm mass delete
    function MassConfirm(dropdown, msg) {
        var drop = document.getElementById(dropdown);
        if (drop != null) {
            if (drop.value == "delete") {
                return confirm(msg);
            }
            return true;
        }
        return true;
    }

    function SetLibParentAction(argument) {
        // Raise select action
        SetAction('morefolderselect', argument);
        RaiseHiddenPostBack();
    }
    //]]>
</script>

<div id="<%= this.ClientID %>">
    <asp:Literal ID="ltlScript" runat="server" EnableViewState="false"></asp:Literal>
    <div class="DialogViewArea" style="height: 100%;">
        <asp:Label ID="lblInfo" runat="server" CssClass="InfoLabel" Visible="false" EnableViewState="false"></asp:Label>
        <asp:PlaceHolder ID="plcViewArea" runat="server">
            <asp:PlaceHolder ID="plcListView" runat="server" Visible="false">
                <div class="ListView">
                    <cms:UniGrid ID="gridList" ShortID="g" runat="server" />
                </div>
            </asp:PlaceHolder>
            <asp:PlaceHolder ID="plcTilesView" runat="server" Visible="false">
                <div class="TilesView">
                    <cms:BasicRepeater ID="repTilesView" runat="server">
                        <ItemTemplate>
                            <div class="DialogTileItemShadow">
                                <div id="<%# GetID(Container.DataItem) %>" class="DialogTileItem">
                                    <asp:Panel ID="pnlTiles" runat="server" CssClass="DialogTileItemBox" EnableViewState="false">
                                        <asp:Panel ID="pnlImageContainer" runat="server" CssClass="DialogTileItemImageContainer">
                                            <div class="DialogTileItemImage">
                                                <asp:Image ID="imgElem" runat="server" />
                                            </div>
                                        </asp:Panel>
                                        <div class="DialogTileItemInfo">
                                            <asp:PlaceHolder ID="plcDocumentName" runat="server">
                                                <asp:Label ID="lblDocumentName" runat="server"></asp:Label>
                                                <br />
                                            </asp:PlaceHolder>
                                            <asp:Label ID="lblFileName" runat="server"></asp:Label>
                                            <br />
                                            <div class="DialogTileItemInfoGreyText">
                                                <asp:Label ID="lblTypeValue" runat="server"></asp:Label>
                                                <br />
                                                <asp:Label ID="lblSizeValue" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                    <div class="DialogTileItemActions">
                                        <table cellpadding="0" cellspacing="2">
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ID="btnSelect" runat="server" EnableViewState="false" />
                                                </td>
                                                <asp:PlaceHolder ID="plcSelectSubDocs" runat="server" EnableViewState="false">
                                                    <td>
                                                        <asp:ImageButton ID="btnSelectSubDocs" runat="server" EnableViewState="false" />
                                                    </td>
                                                </asp:PlaceHolder>
                                                <td>
                                                    <asp:ImageButton ID="btnView" runat="server" EnableViewState="false" />
                                                </td>
                                                <asp:PlaceHolder ID="plcContentEdit" runat="server" EnableViewState="false">
                                                    <td>
                                                        <asp:ImageButton ID="btnContentEdit" runat="server" />
                                                    </td>
                                                </asp:PlaceHolder>
                                                <asp:PlaceHolder ID="plcAttachmentActions" runat="server">
                                                    <td>
                                                        <asp:ImageButton ID="btnDelete" runat="server" EnableViewState="false" />
                                                    </td>
                                                    <asp:PlaceHolder ID="plcAttachmentUpdtAction" runat="server" EnableViewState="false">
                                                        <td>
                                                            <asp:ImageButton ID="btnEdit" runat="server" EnableViewState="false" />
                                                        </td>
                                                        <td>
                                                            <cms:DirectFileUploader ID="dfuElem" runat="server" UploadMode="DirectSingle" Width="16px"
                                                                Height="16px" />
                                                        </td>
                                                    </asp:PlaceHolder>
                                                    <td>
                                                        <asp:PlaceHolder ID="plcWebDAV" runat="server" EnableViewState="false"></asp:PlaceHolder>
                                                    </td>
                                                    <asp:PlaceHolder ID="plcLibraryUpdtAction" runat="server" EnableViewState="false">
                                                        <td>
                                                            <cms:DirectFileUploader ID="dfuElemLib" runat="server" UploadMode="DirectSingle"
                                                                Width="16px" Height="16px" />
                                                            <asp:PlaceHolder ID="plcWebDAVMfi" runat="server" EnableViewState="false"></asp:PlaceHolder>
                                                            <asp:Panel ID="pnlDisabledUpdate" runat="server">
                                                            </asp:Panel>
                                                        </td>
                                                    </asp:PlaceHolder>
                                                </asp:PlaceHolder>
                                                <asp:PlaceHolder ID="plcSelectSubFolders" runat="server">
                                                    <td>
                                                        <asp:ImageButton ID="imgSelectSubFolders" runat="server" EnableViewState="false" />
                                                    </td>
                                                </asp:PlaceHolder>
                                            </tr>
                                        </table>
                                        <asp:PlaceHolder ID="plcSelectionBox" runat="server" Visible="false">
                                            <cms:LocalizedCheckBox ID="chkSelected" CssClass="TilesMultipleSelection" EnableViewState="false"
                                                runat="server" />
                                            <asp:HiddenField ID="hdnItemName" runat="server" />
                                        </asp:PlaceHolder>
                                        <div class="DialogTilesClear">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </cms:BasicRepeater>
                    <div class="DialogTilesClear">
                    </div>
                    <div class="DialogPager">
                        <cms:UniPager ID="pagerElemTiles" runat="server" DirectPageControlID="txtPage">
                            <PreviousPageTemplate>
                                <td>
                                    <a class="UnigridPagerPrev" href="<%# Eval("PreviousURL") %>">&nbsp;</a>
                                </td>
                            </PreviousPageTemplate>
                            <PreviousGroupTemplate>
                                <td>
                                    <a class="UnigridPagerPage" href="<%# Eval("PreviousGroupURL") %>">...</a>
                                </td>
                            </PreviousGroupTemplate>
                            <PageNumbersTemplate>
                                <td>
                                    <a class="UnigridPagerPage" href="<%# Eval("PageURL") %>">
                                        <%# Eval("Page") %></a>
                                </td>
                            </PageNumbersTemplate>
                            <PageNumbersSeparatorTemplate>
                            </PageNumbersSeparatorTemplate>
                            <CurrentPageTemplate>
                                <td>
                                    <span class="UnigridPagerSelectedPage">
                                        <%# Eval("Page") %></span>
                                </td>
                            </CurrentPageTemplate>
                            <NextGroupTemplate>
                                <td>
                                    <a class="UnigridPagerPage" href="<%# Eval("NextGroupURL") %>">...</a>
                                </td>
                            </NextGroupTemplate>
                            <NextPageTemplate>
                                <td>
                                    <a class="UnigridPagerNext" href="<%# Eval("NextURL") %>">&nbsp;</a>
                                </td>
                            </NextPageTemplate>
                            <LayoutTemplate>
                                <table cellspacing="0" cellpadding="0">
                                    <tr>
                                        <asp:PlaceHolder runat="server" ID="plcPreviousPage"></asp:PlaceHolder>
                                        <td style="white-space: nowrap;" class="UnigridPagerPages">
                                            <table class="UniGridPagerNoSeparator" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <asp:PlaceHolder runat="server" ID="plcPreviousGroup"></asp:PlaceHolder>
                                                    <asp:PlaceHolder runat="server" ID="plcPageNumbers"></asp:PlaceHolder>
                                                    <asp:PlaceHolder runat="server" ID="plcNextGroup"></asp:PlaceHolder>
                                                </tr>
                                            </table>
                                        </td>
                                        <asp:PlaceHolder runat="server" ID="plcNextPage"></asp:PlaceHolder>
                                    </tr>
                                </table>
                            </LayoutTemplate>
                        </cms:UniPager>
                    </div>
                    <div class="DialogPageSize">
                        <cms:PageSize ID="pageSizeTiles" runat="server" />
                    </div>
                </div>
            </asp:PlaceHolder>
            <asp:PlaceHolder ID="plcThumbnailsView" runat="server" Visible="false">
                <div class="ThumbnailsView">
                    <cms:BasicRepeater ID="repThumbnailsView" runat="server">
                        <ItemTemplate>
                            <div class="DialogThumbnailItemShadow">
                                <div id="<%# GetID(Container.DataItem) %>" class="DialogThumbnailItem">
                                    <asp:Panel ID="pnlThumbnails" runat="server" CssClass="DialogThumbnailItemBox" EnableViewState="false">
                                        <asp:Panel ID="pnlImageContainer" runat="server" CssClass="DialogThumbItemImageContainer"
                                            EnableViewState="false">
                                            <table cellpadding="0" cellspacing="0" border="0" class="DialogThumbnailItemImage">
                                                <tr>
                                                    <td>
                                                        <asp:Image ID="imgFile" runat="server" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                        <div class="DialogThumbnailItemInfo">
                                            <asp:Label ID="lblFileName" runat="server" EnableViewState="false"></asp:Label>
                                        </div>
                                    </asp:Panel>
                                    <div class="DialogThumbnailActions" enableviewstate="false">
                                        <table cellpadding="0" cellspacing="2">
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ID="btnSelect" runat="server" EnableViewState="false" />
                                                </td>
                                                <asp:PlaceHolder ID="plcSelectSubDocs" runat="server" EnableViewState="false">
                                                    <td>
                                                        <asp:ImageButton ID="btnSelectSubDocs" runat="server" EnableViewState="false" />
                                                    </td>
                                                </asp:PlaceHolder>
                                                <td>
                                                    <asp:ImageButton ID="btnView" runat="server" EnableViewState="false" />
                                                </td>
                                                <asp:PlaceHolder ID="plcContentEdit" runat="server" EnableViewState="false">
                                                    <td>
                                                        <asp:ImageButton ID="btnContentEdit" runat="server" />
                                                    </td>
                                                </asp:PlaceHolder>
                                                <asp:PlaceHolder ID="plcAttachmentActions" runat="server">
                                                    <td>
                                                        <asp:ImageButton ID="btnDelete" runat="server" EnableViewState="false" />
                                                    </td>
                                                    <asp:PlaceHolder ID="plcAttachmentUpdtAction" runat="server" EnableViewState="false">
                                                        <td>
                                                            <asp:ImageButton ID="btnEdit" runat="server" />
                                                        </td>
                                                        <td>
                                                            <cms:DirectFileUploader ID="dfuElem" runat="server" UploadMode="DirectSingle" Width="16px"
                                                                Height="16px" />
                                                        </td>
                                                    </asp:PlaceHolder>
                                                    <td>
                                                        <asp:PlaceHolder ID="plcWebDAV" runat="server" EnableViewState="false"></asp:PlaceHolder>
                                                    </td>
                                                    <asp:PlaceHolder ID="plcLibraryUpdtAction" runat="server" EnableViewState="false">
                                                        <td>
                                                            <cms:DirectFileUploader ID="dfuElemLib" runat="server" />
                                                            <asp:PlaceHolder ID="plcWebDAVMfi" runat="server" EnableViewState="false"></asp:PlaceHolder>
                                                            <asp:Panel ID="pnlDisabledUpdate" runat="server">
                                                            </asp:Panel>
                                                        </td>
                                                    </asp:PlaceHolder>
                                                </asp:PlaceHolder>
                                                <asp:PlaceHolder ID="plcSelectSubFolders" runat="server">
                                                    <td>
                                                        <asp:ImageButton ID="imgSelectSubFolders" runat="server" EnableViewState="false" />
                                                    </td>
                                                </asp:PlaceHolder>
                                            </tr>
                                        </table>
                                        <div class="DialogTilesClear">
                                        </div>
                                    </div>
                                </div>
                                <asp:PlaceHolder ID="plcSelectionBox" runat="server" Visible="false">
                                    <cms:LocalizedCheckBox ID="chkSelected" CssClass="ThumbMultipleSelection" EnableViewState="false"
                                        runat="server" />
                                    <asp:HiddenField ID="hdnItemName" runat="server" />
                                </asp:PlaceHolder>
                            </div>
                        </ItemTemplate>
                    </cms:BasicRepeater>
                    <div class="DialogTilesClear">
                    </div>
                    <div class="DialogPager">
                        <cms:UniPager ID="pagerElemThumbnails" runat="server" DirectPageControlID="txtPage">
                            <PreviousPageTemplate>
                                <td>
                                    <a class="UnigridPagerPrev" href="<%# Eval("PreviousURL") %>">&nbsp;</a>
                                </td>
                            </PreviousPageTemplate>
                            <PreviousGroupTemplate>
                                <td>
                                    <a class="UnigridPagerPage" href="<%# Eval("PreviousGroupURL") %>">...</a>
                                </td>
                            </PreviousGroupTemplate>
                            <PageNumbersTemplate>
                                <td>
                                    <a class="UnigridPagerPage" href="<%# Eval("PageURL") %>">
                                        <%# Eval("Page") %></a>
                                </td>
                            </PageNumbersTemplate>
                            <PageNumbersSeparatorTemplate>
                            </PageNumbersSeparatorTemplate>
                            <CurrentPageTemplate>
                                <td>
                                    <span class="UnigridPagerSelectedPage">
                                        <%# Eval("Page") %></span>
                                </td>
                            </CurrentPageTemplate>
                            <NextGroupTemplate>
                                <td>
                                    <a class="UnigridPagerPage" href="<%# Eval("NextGroupURL") %>">...</a>
                                </td>
                            </NextGroupTemplate>
                            <NextPageTemplate>
                                <td>
                                    <a class="UnigridPagerNext" href="<%# Eval("NextURL") %>">&nbsp;</a>
                                </td>
                            </NextPageTemplate>
                            <LayoutTemplate>
                                <table cellspacing="0" cellpadding="0">
                                    <tr>
                                        <asp:PlaceHolder runat="server" ID="plcPreviousPage"></asp:PlaceHolder>
                                        <td style="white-space: nowrap;" class="UnigridPagerPages">
                                            <table class="UniGridPagerNoSeparator" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <asp:PlaceHolder runat="server" ID="plcPreviousGroup"></asp:PlaceHolder>
                                                    <asp:PlaceHolder runat="server" ID="plcPageNumbers"></asp:PlaceHolder>
                                                    <asp:PlaceHolder runat="server" ID="plcNextGroup"></asp:PlaceHolder>
                                                </tr>
                                            </table>
                                        </td>
                                        <asp:PlaceHolder runat="server" ID="plcNextPage"></asp:PlaceHolder>
                                    </tr>
                                </table>
                            </LayoutTemplate>
                        </cms:UniPager>
                    </div>
                    <div class="DialogPageSize">
                        <cms:PageSize ID="pageSizeThumbs" runat="server" />
                    </div>
                </div>
            </asp:PlaceHolder>
            <asp:PlaceHolder ID="plcMassAction" runat="server" Visible="false">
                <div class="DialogMassActions" style="clear: both;">
                    <div class="DialogMassActionsContent">
                        <asp:DropDownList ID="drpActionFiles" runat="server" CssClass="DropDownFieldSmall" />
                        <div class="DialogMassActionsDropdown">
                            <asp:DropDownList ID="drpActions" runat="server" CssClass="DropDownFieldSmall" />
                            <cms:LocalizedButton ID="btnActions" runat="server" CssClass="SubmitButton" EnableViewState="false"
                                ResourceString="general.ok" />
                        </div>
                    </div>
                </div>
            </asp:PlaceHolder>
        </asp:PlaceHolder>
    </div>
    <asp:HiddenField ID="hdnItemToColorize" runat="server" />
    <input id="hdnFileOrigName" type="hidden" />
</div>
