<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_ImportExport_Controls_ImportGridView"
    CodeFile="ImportGridView.ascx.cs" %>
<%@ Register Src="~/CMSAdminControls/UI/UniGrid/Controls/UniGridPager.ascx" TagName="UniGridPager"
    TagPrefix="cms" %>
<asp:PlaceHolder ID="plcGrid" runat="server">
    <cms:CMSUpdatePanel runat="server" ID="pnlUpdate">
        <ContentTemplate>
            <asp:Panel ID="pnlGrid" runat="server" CssClass="ObjectContent">
                <asp:PlaceHolder runat="server" ID="plcObjects">
                    <asp:Label ID="lblInfo" runat="server" EnableViewState="false" /><br />
                    <br />
                    <asp:Panel ID="pnlLinks" runat="server">
                        &nbsp;
                        <asp:LinkButton ID="btnAll" runat="server" OnClick="btnAll_Click" />&nbsp;
                        <asp:LinkButton ID="btnNone" runat="server" OnClick="btnNone_Click" />&nbsp;
                        <asp:LinkButton ID="btnDefault" runat="server" OnClick="btnDefault_Click" />
                        <br />
                    </asp:Panel>
                    <asp:Label ID="lblCategoryCaption" runat="Server" />
                    <cms:UIGridView ID="gvObjects" ShortID="go" runat="server" AutoGenerateColumns="False"
                        GridLines="Horizontal" CssClass="UniGridGrid" CellPadding="1" Width="100%" EnableViewState="false">
                        <HeaderStyle CssClass="UniGridHead" Wrap="False" />
                        <Columns>
                            <asp:TemplateField>
                                <HeaderStyle Width="50" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Right" Wrap="false" />
                                <ItemTemplate>
                                    <div style="padding-right: 15px;">
                                        <%# IsInConflict(Eval(codeNameColumnName)) %>
                                        <input type="checkbox" id='<%# GetCheckBoxId(Eval(codeNameColumnName)) %>' name='<%# GetCheckBoxName(Eval(codeNameColumnName)) %>'
                                            <%# IsChecked(Eval(codeNameColumnName)) %> />
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Wrap="false" />
                                <ItemTemplate>
                                    <asp:Label ID="lblName" runat="server" ToolTip='<%# HttpUtility.HtmlEncode(ValidationHelper.GetString(Eval(codeNameColumnName), "")) %>'
                                        Text='<%# HttpUtility.HtmlEncode(GetName(Eval(codeNameColumnName), Eval(displayNameColumnName))) %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <RowStyle CssClass="OddRow" />
                        <AlternatingRowStyle CssClass="EvenRow" />
                    </cms:UIGridView>
                    <cms:UniGridPager ID="pagerElem" ShortID="pg" runat="server" DefaultPageSize="10"
                        DisplayPager="true" VisiblePages="5" />
                    <asp:HiddenField runat="server" ID="hdnAvailableItems" Value="" EnableViewState="false" />
                </asp:PlaceHolder>
                <asp:Label ID="lblNoData" runat="Server" /><br />
            </asp:Panel>
        </ContentTemplate>
    </cms:CMSUpdatePanel>
</asp:PlaceHolder>
