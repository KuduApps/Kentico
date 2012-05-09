<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSAdminControls_Debug_ViewState"
    EnableViewState="false" CodeFile="ViewState.ascx.cs" %>
<div style="<%=mLogStyle%>">
    <asp:Literal runat="server" ID="ltlInfo" EnableViewState="false" />
    <asp:GridView runat="server" ID="gridStates" EnableViewState="false" GridLines="Both"
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
                    Width="200" HorizontalAlign="Left" />
                <FooterStyle BorderColor="#cccccc" BorderStyle="Solid" BorderWidth="1" />
                <ItemStyle BorderColor="#cccccc" BorderStyle="Solid" BorderWidth="1" />
                <ItemTemplate>
                    <%# TextHelper.EnsureLineEndings(ValidationHelper.GetString(Eval("ID"), ""), "<br />")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderStyle Wrap="false" BorderColor="#cccccc" BorderStyle="Solid" BorderWidth="1"
                    Width="1%" HorizontalAlign="Left" />
                <FooterStyle BorderColor="#cccccc" BorderStyle="Solid" BorderWidth="1" />
                <ItemStyle BorderColor="#cccccc" BorderStyle="Solid" BorderWidth="1" HorizontalAlign="Center" />
                <ItemTemplate>
                    <%# ColourYesNo(Eval("IsDirty"))%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderStyle Wrap="false" BorderColor="#cccccc" BorderStyle="Solid" BorderWidth="1"
                    Width="99%" HorizontalAlign="Left" />
                <FooterStyle BorderColor="#cccccc" BorderStyle="Solid" BorderWidth="1" />
                <ItemStyle BorderColor="#cccccc" BorderStyle="Solid" BorderWidth="1" />
                <ItemTemplate>
                    <%# TextHelper.EnsureLineEndings(ValidationHelper.GetString(Eval("ViewState"), ""), "<br />")%>
                </ItemTemplate>
                <FooterTemplate>
                    <strong>
                        <%# CMSVersion %></strong>
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderStyle Wrap="false" BorderColor="#cccccc" BorderStyle="Solid" BorderWidth="1"
                    Width="99%" HorizontalAlign="Left" />
                <FooterStyle BorderColor="#cccccc" BorderStyle="Solid" BorderWidth="1" Wrap="false" />
                <ItemStyle BorderColor="#cccccc" BorderStyle="Solid" BorderWidth="1" HorizontalAlign="Right" />
                <ItemTemplate>
                    <%# DataHelper.GetSizeString(ValidationHelper.GetInteger(Eval("ViewStateSize"), 0)) %><br />
                    <%# GetSizeChart(Eval("ViewStateSize"), 0, 0, 0)%>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:PlaceHolder runat="server" ID="plcTotalSize" EnableViewState="false" Visible="<%# this.DisplayTotalSize %>">
                        <cms:LocalizedLabel runat="server" ID="lblTotal" EnableViewState="false" ResourceString="ViewStateLog.Total" />
                        <strong>

                            <script type="text/javascript">
                                //<![CDATA[
                                var stateElem = document.getElementById("__VIEWSTATE");
                                if (stateElem != null) {
                                    document.write(stateElem.value.length);
                                }
                                //]]>
                            </script>

                        </strong>
                        <cms:LocalizedLabel runat="server" ID="lblTotalBytes" EnableViewState="false" ResourceString="ViewStateLog.TotalBytes" />
                    </asp:PlaceHolder>
                </FooterTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</div>
