<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSAdminControls_Debug_ValuesTable" CodeFile="ValuesTable.ascx.cs" %>
<%@ Import Namespace="CMS.GlobalHelper" %>
<div>
    <asp:Literal runat="server" ID="ltlInfo" EnableViewState="false" />
    <asp:GridView runat="server" ID="gridValues" EnableViewState="false" GridLines="Both"
        AutoGenerateColumns="False" Width="100%" CellPadding="3" BorderStyle="Solid" BorderColor="#cccccc" BackColor="#ffffff">
        <HeaderStyle HorizontalAlign="Left" BackColor="#e8e8e8" Wrap="false" BorderColor="#cccccc" BorderStyle="Solid" BorderWidth="1" />
        <AlternatingRowStyle BackColor="#f4f4f4" />
        <Columns>
            <asp:TemplateField>
                <HeaderStyle Wrap="false" BorderColor="#cccccc" BorderStyle="Solid" BorderWidth="1" Width="10" />
                <FooterStyle BorderColor="#cccccc" BorderStyle="Solid" BorderWidth="1" />
                <ItemStyle BorderColor="#cccccc" BorderStyle="Solid" BorderWidth="1" HorizontalAlign="Center" />
                <ItemTemplate>
                    <strong>
                        <%# GetIndex() %>
                    </strong>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</div>
