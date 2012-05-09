<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_System_Debug_System_DebugThreads"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="System - SQL"
    CodeFile="System_DebugThreads.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/PageElements/PageTitle.ascx" TagName="PageTitle"
    TagPrefix="cms" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">

    <script type="text/javascript">
        //<![CDATA[
        function alert() {
        }
        //]]>
    </script>

    <asp:HiddenField runat="server" ID="hdnGuid" EnableViewState="false" />
    <asp:Button runat="server" ID="btnCancel" CssClass="HiddenButton" OnClick="btnCancel_Click" />
    <div class="AlignRight">
        <cms:CMSButton runat="server" ID="btnRunDummy" OnClick="btnRunDummy_Click" CssClass="LongButton" />
    </div>
    <asp:Label ID="lblError" runat="server" CssClass="ErrorLabel" EnableViewState="false"
        Visible="false" />
    <asp:Label ID="lblInfo" runat="server" CssClass="InfoLabel" EnableViewState="false" />
    <ajaxToolkit:ToolkitScriptManager runat="server" ID="manScript" ScriptMode="Release" />
    <cms:CMSUpdatePanel runat="server" ID="pnlUpdate">
        <ContentTemplate>
            <cms:PageTitle ID="titleThreads" runat="server" TitleCssClass="SubTitleHeader" />
            <br />
            <asp:GridView runat="server" ID="gridThreads" EnableViewState="false" GridLines="Horizontal"
                AutoGenerateColumns="false" Width="100%" CellPadding="3" ShowFooter="true" CssClass="UniGridGrid">
                <HeaderStyle HorizontalAlign="Left" CssClass="UniGridHead" Wrap="false" />
                <RowStyle Wrap="false" />
                <Columns>
                    <asp:TemplateField>
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <%# GetIndex() %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <%# GetActions(Eval("HasLog"), Eval("ThreadGUID"), Eval("Status")) %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderStyle Width="100%" />
                        <ItemTemplate>
                            <strong>
                                <%# Eval("MethodClassName") %></strong>.<%# Eval("MethodName") %><br />
                            <%# Eval("RequestUrl") %>
                        </ItemTemplate>
                        <FooterTemplate>
                            <strong>
                                <%# cmsVersion %></strong>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderStyle HorizontalAlign="Center" CssClass="NW" />
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <%# Eval("ThreadID") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <%# Eval("Status") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemStyle CssClass="NW" />
                        <ItemTemplate>
                            <%# Eval("ThreadStarted") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderStyle HorizontalAlign="Right" />
                        <ItemStyle HorizontalAlign="Right" />
                        <FooterStyle HorizontalAlign="Right" />
                        <ItemTemplate>
                            <%# GetDuration(Eval("ThreadStarted"), null)%></ItemTemplate>
                        <FooterTemplate>
                            <strong>
                                <%# GetDurationString(totalDuration) %></strong>
                        </FooterTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <br />
            <cms:PageTitle ID="titleFinished" runat="server" TitleCssClass="SubTitleHeader" />
            <br />
            <asp:GridView runat="server" ID="gridFinished" EnableViewState="false" GridLines="Horizontal"
                AutoGenerateColumns="false" Width="100%" CellPadding="3" ShowFooter="true" CssClass="UniGridGrid">
                <HeaderStyle HorizontalAlign="Left" CssClass="UniGridHead" Wrap="false" />
                <RowStyle Wrap="false" />
                <Columns>
                    <asp:TemplateField>
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <%# GetIndex() %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderStyle Width="100%" />
                        <ItemTemplate>
                            <strong>
                                <%# Eval("MethodClassName") %></strong>.<%# Eval("MethodName") %><br />
                            <%# Eval("RequestUrl") %>
                        </ItemTemplate>
                        <FooterTemplate>
                            <strong>
                                <%# cmsVersion %></strong>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderStyle HorizontalAlign="Center" CssClass="NW" />
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <%# Eval("ThreadID") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <%# Eval("Status") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemStyle CssClass="NW" />
                        <ItemTemplate>
                            <%# Eval("ThreadStarted") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemStyle CssClass="NW" />
                        <ItemTemplate>
                            <%# Eval("ThreadFinished") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderStyle HorizontalAlign="Right" />
                        <ItemStyle HorizontalAlign="Right" />
                        <FooterStyle HorizontalAlign="Right" />
                        <ItemTemplate>
                            <%# GetDuration(Eval("ThreadStarted"), Eval("ThreadFinished"))%></ItemTemplate>
                        <FooterTemplate>
                            <strong>
                                <%# GetDurationString(totalDuration) %></strong>
                        </FooterTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:Timer runat="server" ID="timRefresh" Enabled="true" Interval="1000" OnTick="timRefresh_Tick" />
        </ContentTemplate>
    </cms:CMSUpdatePanel>
</asp:Content>
