<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSAdminControls_Debug_AllLog"
    EnableViewState="false" CodeFile="AllLog.ascx.cs" %>
<div style="<%=mLogStyle%>">
    <asp:Literal runat="server" ID="ltlInfo" EnableViewState="false" />
    <asp:GridView runat="server" ID="gridDebug" EnableViewState="false" GridLines="Both"
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
                        <%# Eval("Index")%>
                    </strong>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderStyle Wrap="false" BorderColor="#cccccc" BorderStyle="Solid" BorderWidth="1"
                    HorizontalAlign="Left" />
                <FooterStyle BorderColor="#cccccc" BorderStyle="Solid" BorderWidth="1" />
                <ItemStyle BorderColor="#cccccc" BorderStyle="Solid" BorderWidth="1" />
                <ItemTemplate>
                    <%# Eval("DebugType")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderStyle Wrap="false" BorderColor="#cccccc" BorderStyle="Solid" BorderWidth="1"
                    HorizontalAlign="Left" Width="99%" />
                <FooterStyle BorderColor="#cccccc" BorderStyle="Solid" BorderWidth="1" />
                <ItemStyle BorderColor="#cccccc" BorderStyle="Solid" BorderWidth="1" />
                <ItemTemplate>
                    <%# Eval("Information")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderStyle Wrap="false" BorderColor="#cccccc" HorizontalAlign="Center" BorderStyle="Solid"
                    BorderWidth="1" />
                <FooterStyle BorderColor="#cccccc" BorderStyle="Solid" BorderWidth="1" />
                <ItemStyle Wrap="false" BorderColor="#cccccc" HorizontalAlign="Center" BorderStyle="Solid"
                    BorderWidth="1" />
                <ItemTemplate>
                    <%# Eval("Result")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderStyle Wrap="false" HorizontalAlign="Center" BorderColor="#cccccc" BorderStyle="Solid"
                    BorderWidth="1" />
                <FooterStyle BorderColor="#cccccc" BorderStyle="Solid" BorderWidth="1" />
                <ItemStyle BorderColor="#cccccc" BorderStyle="Solid" BorderWidth="1" />
                <ItemTemplate>
                    <%# Eval("Context") %>
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
                    <%# Eval("TotalDuration")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderStyle Wrap="false" Width="70px" HorizontalAlign="Center" BorderColor="#cccccc"
                    BorderStyle="Solid" BorderWidth="1" />
                <ItemStyle Wrap="false" HorizontalAlign="Right" BorderColor="#cccccc" BorderStyle="Solid"
                    BorderWidth="1" />
                <FooterStyle Wrap="false" HorizontalAlign="Right" BorderColor="#cccccc" BorderStyle="Solid"
                    BorderWidth="1" />
                <ItemTemplate>
                    <%# Eval("Duration")%>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</div>
