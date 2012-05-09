<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSAdminControls_Debug_RequestLog"
    CodeFile="RequestLog.ascx.cs" %>
<%@ Import Namespace="CMS.GlobalHelper" %>
<%@ Register Src="ValuesTable.ascx" TagName="ValuesTable" TagPrefix="cms" %>
<div style="<%=mLogStyle%>">
    <asp:Literal runat="server" ID="ltlInfo" EnableViewState="false" />
    <asp:GridView runat="server" ID="gridCache" EnableViewState="false" GridLines="Both"
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
                        <%# GetIndex() %>
                    </strong>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderStyle Wrap="false" BorderColor="#cccccc" BorderStyle="Solid" BorderWidth="1"
                    HorizontalAlign="Left" />
                <FooterStyle BorderColor="#cccccc" BorderStyle="Solid" BorderWidth="1" />
                <ItemStyle BorderColor="#cccccc" BorderStyle="Solid" BorderWidth="1" />
                <ItemTemplate>
                    <%# GetBeginIndent(Eval("Indent")) %><%# Eval("Method") %><%# GetEndIndent(Eval("Indent")) %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderStyle Wrap="false" BorderColor="#cccccc" BorderStyle="Solid" Width="100%"
                    BorderWidth="1" HorizontalAlign="Left" />
                <FooterStyle BorderColor="#cccccc" BorderStyle="Solid" BorderWidth="1" />
                <ItemStyle BorderColor="#cccccc" BorderStyle="Solid" BorderWidth="1" />
                <ItemTemplate>
                    <%# Eval("Parameter") %>
                </ItemTemplate>
                <FooterTemplate>
                    <strong>
                        <%# CMSVersion %></strong>
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderStyle Wrap="false" BorderColor="#cccccc" BorderStyle="Solid" BorderWidth="1"
                    HorizontalAlign="Right" />
                <FooterStyle BorderColor="#cccccc" BorderStyle="Solid" BorderWidth="1" HorizontalAlign="Right" />
                <ItemStyle BorderColor="#cccccc" BorderStyle="Solid" BorderWidth="1" HorizontalAlign="Right" />
                <ItemTemplate>
                    <%# GetFromStart(Eval("Time")) %>
                    <br />
                </ItemTemplate>
                <FooterTemplate>
                    <strong>
                        <%# this.TotalDuration.ToString("F3") %>
                    </strong>
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderStyle Wrap="false" BorderColor="#cccccc" BorderStyle="Solid" BorderWidth="1"
                    HorizontalAlign="Right" />
                <FooterStyle BorderColor="#cccccc" BorderStyle="Solid" BorderWidth="1" HorizontalAlign="Right" />
                <ItemStyle BorderColor="#cccccc" BorderStyle="Solid" BorderWidth="1" HorizontalAlign="Right" />
                <ItemTemplate>
                    <%# GetDuration(Eval("Duration")) %><br />
                    <%# GetDurationChart(Eval("Duration"), 0.005, 0, 0) %>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <cms:ValuesTable ID="tblReqC" runat="server" EnableViewState="False" />
    <cms:ValuesTable ID="tblResC" runat="server" EnableViewState="False" />
    <cms:ValuesTable ID="tblVal" runat="server" EnableViewState="False" />
</div>
