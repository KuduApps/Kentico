<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_ImportExport_Controls_ExportGridTasks"
    CodeFile="ExportGridTasks.ascx.cs" %>
<%@ Register Src="~/CMSAdminControls/UI/UniGrid/Controls/UniGridPager.ascx" TagName="UniGridPager"
    TagPrefix="cms" %>
<asp:PlaceHolder ID="plcGrid" runat="server">
    <cms:CMSUpdatePanel runat="server" ID="pnlUpdate">
        <ContentTemplate>
            <cms:CMSPanel ID="pnlGrid" ShortID="p" runat="server">
                <asp:PlaceHolder runat="server" ID="plcTasks">
                    <br />
                    <strong>
                        <asp:Label ID="lblTasks" runat="Server" /></strong>
                    <br />
                    <br />
                    &nbsp;
                    <asp:LinkButton ID="btnAllTasks" runat="Server" EnableViewState="false" OnClick="btnAll_Click" />&nbsp;
                    <asp:LinkButton ID="btnNoneTasks" runat="Server" EnableViewState="false" OnClick="btnNone_Click" /><br />
                    <br />
                    <cms:UIGridView ID="gvTasks" ShortID="gt" runat="server" AutoGenerateColumns="False"
                        GridLines="Horizontal" CssClass="UniGridGrid" CellPadding="1" Width="100%" EnableViewState="false">
                        <HeaderStyle HorizontalAlign="Left" Wrap="False" CssClass="UniGridHead" />
                        <Columns>
                            <asp:TemplateField>
                                <HeaderStyle Width="50" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <input type="checkbox" id='<%# GetCheckBoxId(Eval("TaskID")) %>' name='<%# GetCheckBoxName(Eval("TaskID")) %>'
                                        <%# IsTaskChecked(Eval("TaskID")) %> />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderStyle Width="100%" />
                                <ItemStyle Wrap="false" />
                                <ItemTemplate>
                                    <asp:Label ID="lblName" runat="server" EnableViewState="false" ToolTip='<%# HttpUtility.HtmlEncode(ValidationHelper.GetString(Eval("TaskTitle"), "")) %>'
                                        Text='<%# HttpUtility.HtmlEncode(TextHelper.LimitLength(ResHelper.LocalizeString(ValidationHelper.GetString(Eval("TaskTitle"), "")), 60)) %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="TaskType">
                                <ItemStyle Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TaskTime">
                                <ItemStyle Wrap="false" />
                            </asp:BoundField>
                        </Columns>
                        <RowStyle CssClass="OddRow" />
                        <AlternatingRowStyle CssClass="EvenRow" />
                    </cms:UIGridView>
                    <cms:UniGridPager ID="pagerElem" ShortID="pg" runat="server" DefaultPageSize="10"
                        DisplayPager="true" VisiblePages="5" />
                    <asp:HiddenField runat="server" ID="hdnAvailableTasks" Value="" EnableViewState="false" />
                </asp:PlaceHolder>
            </cms:CMSPanel>
        </ContentTemplate>
    </cms:CMSUpdatePanel>
    <br />
    <br />
</asp:PlaceHolder>
