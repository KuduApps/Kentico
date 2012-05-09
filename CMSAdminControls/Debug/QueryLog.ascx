<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSAdminControls_Debug_QueryLog"
    CodeFile="QueryLog.ascx.cs" %>
<%@ Import Namespace="CMS.GlobalHelper" %>
<div style="<%=mLogStyle%>">
    <asp:Literal runat="server" ID="ltlInfo" EnableViewState="false" />
    <asp:GridView runat="server" ID="gridQueries" EnableViewState="false" GridLines="Both"
        AutoGenerateColumns="false" Width="100%" CellPadding="3" ShowFooter="true" BorderStyle="Solid"
        BorderColor="#cccccc" BackColor="#ffffff">
        <HeaderStyle HorizontalAlign="Left" BackColor="#e8e8e8" />
        <AlternatingRowStyle BackColor="#f4f4f4" />
        <FooterStyle BackColor="#e8e8e8" />
        <Columns>
            <asp:TemplateField>
                <HeaderStyle Wrap="false" BorderColor="#cccccc" BorderStyle="Solid" BorderWidth="1"
                    HorizontalAlign="Left" />
                <FooterStyle BorderColor="#cccccc" BorderStyle="Solid" BorderWidth="1" />
                <ItemStyle BorderColor="#cccccc" BorderStyle="Solid" BorderWidth="1" HorizontalAlign="Center" />
                <ItemTemplate>
                    <strong>
                        <%# GetIndex(Eval("QueryResultsSize"), Eval("QueryParametersSize"), Eval("QueryText"))%>
                        <%# GetDuplicity(Eval("Duplicit"), Eval("QueryText"))%>
                    </strong>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderStyle BorderColor="#cccccc" BorderStyle="Solid" BorderWidth="1" HorizontalAlign="Left" />
                <FooterStyle BorderColor="#cccccc" BorderStyle="Solid" BorderWidth="1" />
                <ItemStyle BorderColor="#cccccc" BorderStyle="Solid" BorderWidth="1" />
                <ItemTemplate>
                    <%# GetInformation(Eval("ConnectionString"), Eval("ConnectionOp"), Eval("QueryName"), Eval("QueryText"), Eval("QueryParameters"), Eval("QueryParametersSize"), Eval("QueryResults"), Eval("QueryResultsSize"), MaxSize)%>
                </ItemTemplate>
                <FooterTemplate>
                    <strong>
                        <%# String.Format(ResHelper.GetString("QueryLog.Total", null, "QueryLog.Total {0} / {1} / {2}"), index, CMS.SettingsProvider.SqlHelperClass.GetSizeString(this.TotalParamSize), CMS.SettingsProvider.SqlHelperClass.GetSizeString(this.TotalSize))%>
                    </strong>
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderStyle Wrap="false" HorizontalAlign="Center" BorderColor="#cccccc" BorderStyle="Solid"
                    BorderWidth="1" />
                <FooterStyle BorderColor="#cccccc" BorderStyle="Solid" BorderWidth="1" />
                <ItemStyle BorderColor="#cccccc" BorderStyle="Solid" BorderWidth="1" />
                <ItemTemplate>
                    <%# GetContext(Eval("Context")) %>
                </ItemTemplate>
                <FooterTemplate>
                    <strong>
                        <%# CMSVersion %></strong>
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderStyle Wrap="false" Width="70px" HorizontalAlign="Center" BorderColor="#cccccc"
                    BorderStyle="Solid" BorderWidth="1" />
                <ItemStyle Wrap="false" HorizontalAlign="Right" BorderColor="#cccccc" BorderStyle="Solid"
                    BorderWidth="1" />
                <FooterStyle Wrap="false" HorizontalAlign="Right" BorderColor="#cccccc" BorderStyle="Solid"
                    BorderWidth="1" />
                <ItemTemplate>
                    <%# GetDuration(Eval("QueryDuration")) %><br />
                    <%# GetDurationChart(Eval("QueryDuration"), 0.005, 0, 0) %>
                </ItemTemplate>
                <FooterTemplate>
                    <strong>
                        <%# this.TotalDuration.ToString("F3") %>
                    </strong>
                </FooterTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</div>
