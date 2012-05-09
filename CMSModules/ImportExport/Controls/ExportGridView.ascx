<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_ImportExport_Controls_ExportGridView"
    CodeFile="ExportGridView.ascx.cs" %>
<%@ Register Src="~/CMSAdminControls/UI/UniGrid/Controls/UniGridPager.ascx" TagName="UniGridPager"
    TagPrefix="cms" %>
<asp:PlaceHolder ID="plcGrid" runat="server">
    <cms:CMSUpdatePanel runat="server" ID="pnlUpdate">
        <ContentTemplate>
            <cms:CMSPanel ID="pnlGrid" ShortID="p" runat="server">
                <asp:PlaceHolder runat="server" ID="plcObjects">
                    <asp:Panel ID="pnlLinks" runat="server" EnableViewState="false"><asp:LinkButton ID="btnAll" runat="server" OnClick="btnAll_Click" />&nbsp;
                        <asp:LinkButton ID="btnNone" runat="server" OnClick="btnNone_Click" />&nbsp;
                        <asp:LinkButton ID="btnDefault" runat="server" OnClick="btnDefault_Click" />
                        <br />
                        <br />
                    </asp:Panel>
                    <asp:Label ID="lblCategoryCaption" runat="Server" />
                    <cms:UIGridView ID="gvObjects" ShortID="go" runat="server" AutoGenerateColumns="False"
                        GridLines="Horizontal" CssClass="UniGridGrid" CellPadding="1" Width="100%" EnableViewState="false">
                        <HeaderStyle HorizontalAlign="Left" Wrap="False" CssClass="UniGridHead" />
                        <Columns>
                            <asp:TemplateField>
                                <HeaderStyle Width="50" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemTemplate>
                                    <input type="checkbox" id='<%# GetCheckBoxId(Eval(codeNameColumnName)) %>'
                                        name='<%# GetCheckBoxName(Eval(codeNameColumnName)) %>'
                                        <%# IsChecked(Eval(codeNameColumnName)) %> />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderStyle Width="100%" />
                                <ItemStyle Wrap="false" />
                                <ItemTemplate>
                                    <asp:Label ID="lblName" runat="server" ToolTip='<%# HttpUtility.HtmlEncode(ValidationHelper.GetString(Eval(codeNameColumnName), "")) %>'
                                        Text='<%# HttpUtility.HtmlEncode(TextHelper.LimitLength(ResHelper.LocalizeString(GetName(Eval(codeNameColumnName), Eval(displayNameColumnName))), 75)) %>' />
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
                <asp:Label ID="lblNoData" runat="Server" />
            </cms:CMSPanel>
        </ContentTemplate>
    </cms:CMSUpdatePanel>
</asp:PlaceHolder>
