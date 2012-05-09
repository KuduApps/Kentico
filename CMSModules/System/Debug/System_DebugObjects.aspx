<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_System_Debug_System_DebugObjects" Theme="Default"
    MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="System - SQL" CodeFile="System_DebugObjects.aspx.cs" %>

<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <div class="AlignRight">
        <cms:CMSButton runat="server" ID="btnClear" OnClick="btnClear_Click" CssClass="LongButton"
            EnableViewState="false" />
    </div>
    <asp:Label ID="lblError" runat="server" CssClass="ErrorLabel" EnableViewState="false"
        Visible="false" />
    <asp:Label ID="lblInfo" runat="server" CssClass="InfoLabel" EnableViewState="false" />
    <asp:GridView runat="server" ID="gridHashtables" EnableViewState="false" GridLines="Horizontal"
        AutoGenerateColumns="false" Width="100%" CellPadding="3" ShowFooter="true" CssClass="UniGridGrid">
        <HeaderStyle HorizontalAlign="Left" CssClass="UniGridHead" />
        <RowStyle CssClass="EvenRow" />
        <AlternatingRowStyle CssClass="OddRow" />
        <Columns>
            <asp:TemplateField>
            <HeaderStyle Wrap="false"/>
            <ItemStyle Wrap="false" />
                <ItemTemplate>
                    <%# Eval("TableName") %>
                </ItemTemplate>
                <FooterTemplate>
                    <strong>
                        <%# cmsVersion %></strong>
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <%# GetTableCount(Eval("ObjectCount")) %></ItemTemplate>
                <FooterTemplate>
                    <strong>
                        <%# totalTableObjects %></strong>
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderStyle Width="100%" />
                <ItemTemplate>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <br />
    <asp:GridView runat="server" ID="gridObjects" EnableViewState="false" GridLines="Horizontal"
        AutoGenerateColumns="false" Width="100%" CellPadding="3" ShowFooter="true" CssClass="UniGridGrid">
        <HeaderStyle HorizontalAlign="Left" CssClass="UniGridHead" />
        <RowStyle CssClass="EvenRow" />
        <AlternatingRowStyle CssClass="OddRow" />
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <%# Eval("ObjectType") %>
                </ItemTemplate>
                <FooterTemplate>
                    <strong>
                        <%# cmsVersion %></strong>
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <%# GetCount(Eval("ObjectCount")) %></ItemTemplate>
                <FooterTemplate>
                    <strong>
                        <%# totalObjects %></strong>
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderStyle Width="100%" />
                <ItemTemplate>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
