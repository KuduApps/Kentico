<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_ImportExport_Controls_ImportGridTasks"
    CodeFile="ImportGridTasks.ascx.cs" %>
<%@ Register Src="~/CMSAdminControls/UI/UniGrid/Controls/UniGridPager.ascx" TagName="UniGridPager"
    TagPrefix="cms" %>
<asp:PlaceHolder ID="plcGrid" runat="server">
    <cms:CMSUpdatePanel runat="server" ID="pnlUpdate">
        <ContentTemplate>
            <asp:Panel ID="pnlGrid" runat="server" CssClass="ObjectContent">
                <asp:PlaceHolder runat="server" ID="plcTasks">
                    <br />
                    <asp:Label ID="lblTasks" runat="Server" Font-Bold="true" />
                    <br />
                    <asp:Panel ID="pnlTaskLinks" runat="server">
                        &nbsp;
                        <asp:LinkButton ID="btnAllTasks" runat="Server" EnableViewState="false" OnClick="btnAll_Click" />&nbsp;
                        <asp:LinkButton ID="btnNoneTasks" runat="Server" EnableViewState="false" OnClick="btnNone_Click" /><br />
                        <br />
                    </asp:Panel>
                    <cms:UIGridView ID="gvTasks" ShortID="gt" runat="server" AutoGenerateColumns="False"
                        GridLines="Horizontal" CssClass="UniGridGrid" CellPadding="1" Width="100%" EnableViewState="false">
                        <HeaderStyle CssClass="UniGridHead" Wrap="False" />
                        <Columns>
                            <asp:TemplateField>
                                <HeaderStyle Width="50" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="right" />
                                <ItemTemplate>
                                    <div style="padding-right: 15px;">
                                        <input type="checkbox" id='<%# GetCheckBoxId(Eval("TaskID")) %>' name='<%# GetCheckBoxName(Eval("TaskID")) %>'
                                            <%# IsTaskChecked(Eval("TaskID")) %> />
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="TaskTitle">
                                <ItemStyle Wrap="false" />
                            </asp:BoundField>
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
            </asp:Panel>
        </ContentTemplate>
    </cms:CMSUpdatePanel>
    <br />
    <br />
</asp:PlaceHolder>
