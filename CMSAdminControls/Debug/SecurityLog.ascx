<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSAdminControls_Debug_SecurityLog"
    CodeFile="SecurityLog.ascx.cs" %>
<%@ Import Namespace="CMS.GlobalHelper" %>
<div style="<%=mLogStyle%>">
    <asp:Literal runat="server" ID="ltlInfo" EnableViewState="false" />
    <asp:GridView runat="server" ID="gridSec" EnableViewState="false" GridLines="Both"
        AutoGenerateColumns="false" Width="100%" CellPadding="3" ShowFooter="true" BorderStyle="Solid"
        BorderColor="#cccccc" BackColor="#ffffff">
        <HeaderStyle HorizontalAlign="Center" BackColor="#e8e8e8" BorderColor="#cccccc" BorderStyle="Solid"
            BorderWidth="1" />
        <AlternatingRowStyle BackColor="#f4f4f4" />
        <FooterStyle BackColor="#e8e8e8" BorderColor="#cccccc" BorderStyle="Solid" BorderWidth="1" />
        <RowStyle HorizontalAlign="Center" />
        <Columns>
            <asp:TemplateField>
                <HeaderStyle Wrap="false" BorderColor="#cccccc" BorderStyle="Solid" BorderWidth="1" />
                <FooterStyle BorderColor="#cccccc" BorderStyle="Solid" BorderWidth="1" />
                <ItemStyle BorderColor="#cccccc" BorderStyle="Solid" BorderWidth="1" />
                <ItemTemplate>
                    <strong>
                        <%# GetIndex(Eval("Indent"))%>
                        <%# GetDuplicity(Eval("Duplicit"), Eval("Indent"))%>
                    </strong>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemStyle BorderColor="#cccccc" BorderStyle="Solid" BorderWidth="1" />
                <HeaderStyle BorderColor="#cccccc" BorderStyle="Solid" BorderWidth="1" HorizontalAlign="Left" />
                <FooterStyle BorderColor="#cccccc" BorderStyle="Solid" BorderWidth="1" />
                <ItemTemplate>
                    <%# GetUserName(Eval("UserName"), Eval("Indent")) %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemStyle BorderColor="#cccccc" BorderStyle="Solid" BorderWidth="1" HorizontalAlign="Left"
                    Wrap="false" />
                <HeaderStyle BorderColor="#cccccc" BorderStyle="Solid" BorderWidth="1" HorizontalAlign="Left" />
                <FooterStyle BorderColor="#cccccc" BorderStyle="Solid" BorderWidth="1" />
                <ItemTemplate>
                    <%# GetBeginIndent(Eval("Indent")) %><strong><%# Eval("SecurityOperation") %></strong><%# GetEndIndent(Eval("Indent")) %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemStyle BorderColor="#cccccc" BorderStyle="Solid" BorderWidth="1" />
                <HeaderStyle BorderColor="#cccccc" BorderStyle="Solid" BorderWidth="1" HorizontalAlign="Left" />
                <FooterStyle BorderColor="#cccccc" BorderStyle="Solid" BorderWidth="1" />
                <ItemTemplate>
                    <%# GetResult(Eval("Result"), Eval("Indent")) %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemStyle BorderColor="#cccccc" BorderStyle="Solid" BorderWidth="1" />
                <HeaderStyle BorderColor="#cccccc" BorderStyle="Solid" BorderWidth="1" HorizontalAlign="Left" />
                <FooterStyle BorderColor="#cccccc" BorderStyle="Solid" BorderWidth="1" />
                <ItemTemplate>
                    <%# Eval("Resource") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemStyle BorderColor="#cccccc" BorderStyle="Solid" BorderWidth="1" />
                <HeaderStyle BorderColor="#cccccc" BorderStyle="Solid" BorderWidth="1" HorizontalAlign="Left" />
                <FooterStyle BorderColor="#cccccc" BorderStyle="Solid" BorderWidth="1" />
                <ItemTemplate>
                    <%# Eval("Name") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemStyle BorderColor="#cccccc" BorderStyle="Solid" BorderWidth="1" />
                <HeaderStyle BorderColor="#cccccc" BorderStyle="Solid" BorderWidth="1" HorizontalAlign="Left" />
                <FooterStyle BorderColor="#cccccc" BorderStyle="Solid" BorderWidth="1" />
                <ItemTemplate>
                    <%# Eval("SiteName") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemStyle BorderColor="#cccccc" BorderStyle="Solid" BorderWidth="1" HorizontalAlign="Left" />
                <HeaderStyle BorderColor="#cccccc" BorderStyle="Solid" BorderWidth="1" HorizontalAlign="Center" />
                <FooterStyle BorderColor="#cccccc" BorderStyle="Solid" BorderWidth="1" />
                <ItemTemplate>
                    <%# GetContext(Eval("Context")) %>
                </ItemTemplate>
                <FooterTemplate>
                    <strong>
                        <%# CMSVersion %></strong>
                </FooterTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</div>
