<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_RecycleBin_Controls_RecycleBin"
    CodeFile="RecycleBin.ascx.cs" %>
<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/UI/PageElements/PageTitle.ascx" TagName="PageTitle"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/AsyncControl.ascx" TagName="AsyncControl" TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/AsyncBackground.ascx" TagName="AsyncBackground"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/Content/Controls/Filters/RecycleBinFilter.ascx" TagName="RecycleBinFilter"
    TagPrefix="cms" %>
<asp:Panel runat="server" ID="pnlLog" Visible="false">
    <cms:AsyncBackground ID="backgroundElem" runat="server" />
    <div class="AsyncLogArea">
        <div>
            <asp:Panel ID="pnlAsyncBody" runat="server" CssClass="PageBody">
                <asp:Panel ID="pnlTitleAsync" runat="server" CssClass="PageHeader" EnableViewState="false">
                    <cms:PageTitle ID="titleElemAsync" runat="server" EnableViewState="false" />
                </asp:Panel>
                <asp:Panel ID="pnlCancel" runat="server" CssClass="PageHeaderLine" EnableViewState="false">
                    <cms:LocalizedButton runat="server" ID="btnCancel" ResourceString="general.cancel"
                        CssClass="SubmitButton" EnableViewState="false" />
                </asp:Panel>
                <asp:Panel ID="pnlAsyncContent" runat="server" CssClass="PageContent">
                    <cms:AsyncControl ID="ctlAsync" runat="server" />
                </asp:Panel>
            </asp:Panel>
        </div>
    </div>
</asp:Panel>
<cms:CMSUpdatePanel ID="pnlUpdate" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:Label ID="lblError" runat="server" CssClass="ErrorLabel" EnableViewState="false"
            Visible="false" />
        <asp:Label ID="lblInfo" runat="server" CssClass="InfoLabel" EnableViewState="false"
            Visible="false" />
        <div>
            <asp:PlaceHolder ID="plcFilter" runat="server">
                <cms:RecycleBinFilter ID="filterBin" runat="server" LoadUsers="true" />
                <br />
            </asp:PlaceHolder>
            <cms:UniGrid ID="ugRecycleBin" runat="server" GridName="~/CMSModules/RecycleBin/Controls/RecycleBin.xml"
                IsLiveSite="false" HideControlForZeroRows="true" />
            <asp:Panel ID="pnlFooter" runat="server" CssClass="MassAction">
                <asp:DropDownList ID="drpWhat" runat="server" CssClass="DropDownFieldSmall" />
                <asp:DropDownList ID="drpAction" runat="server" CssClass="DropDownFieldSmall" />
                <cms:LocalizedButton ID="btnOk" runat="server" ResourceString="general.ok" CssClass="SubmitButton SelectorButton"
                    EnableViewState="false" OnClick="btnOk_OnClick" />
                <asp:Label ID="lblValidation" runat="server" CssClass="InfoLabel" EnableViewState="false"
                    Style="padding-top: 10px;display:none;" />
            </asp:Panel>
        </div>
    </ContentTemplate>
</cms:CMSUpdatePanel>
