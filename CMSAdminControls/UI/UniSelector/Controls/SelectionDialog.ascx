<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSAdminControls_UI_UniSelector_Controls_SelectionDialog" CodeFile="SelectionDialog.ascx.cs" %>
<%@ Register src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" tagname="UniGrid" tagprefix="cms" %>


<asp:Panel ID="pnlBody" runat="server" CssClass="UniSelectorDialogBody">
    <asp:Panel ID="pnlFilter" runat="server" CssClass="PageHeaderLine" Visible="false">
    </asp:Panel>
    <cms:CMSUpdatePanel runat="server" ID="pnlUpdate" UpdateMode="Always">
        <ContentTemplate>
            <asp:Panel ID="pnlSearch" runat="server" CssClass="PageHeaderLine" Visible="false"
                DefaultButton="btnSearch">
                    <cms:LocalizedLabel ID="lblSearch" runat="server" EnableViewState="False" />
                    <cms:CMSTextBox ID="txtSearch" runat="server" CssClass="TextBoxField"  /><cms:LocalizedButton
                            ID="btnSearch" runat="server" CssClass="ContentButton" EnableViewState="False" />
            </asp:Panel>
            <asp:Panel ID="pnlAll" runat="server" CssClass="PageHeaderLine" Visible="false" EnableViewState="false">
                <asp:LinkButton runat="server" ID="lnkSelectAll" OnClick="lnkSelectAll_Click" />&nbsp;&nbsp;
                <asp:LinkButton runat="server" ID="lnkDeselectAll" OnClick="lnkDeselectAll_Click" />
            </asp:Panel>
            <asp:Panel runat="server" ID="pnlContent">
                <div class="UniSelectorDialogGridArea">
                    <div class="UniSelectorDialogGridPadding">
                        <cms:UniGrid ID="uniGrid" runat="server" PageSize="10,25,50,100" />
                        <div class="ClearBoth" ></div>
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </cms:CMSUpdatePanel>
    <cms:CMSUpdatePanel runat="server" ID="pnlHidden" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:HiddenField ID="hidItem" runat="server" EnableViewState="false" />
            <asp:HiddenField ID="hidName" runat="server" EnableViewState="false" />
        </ContentTemplate>
    </cms:CMSUpdatePanel>
</asp:Panel>
<asp:Literal runat="server" ID="ltlScript" EnableViewState="false" />
