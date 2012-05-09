<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_PortalEngine_Controls_WebParts_WebpartBinding" CodeFile="WebpartBinding.ascx.cs" %>
<style type="text/css">
    .GridContent
    {
        margin: 10px 10px 10px 10px;
        height: 160px;
        overflow: auto;
        border: 1px solid #cccccc;
    }
</style>

<asp:Panel runat="server" ID="pnlScroll" CssClass="TabsPageScrollArea">
    <asp:Panel runat="server" ID="pnlTab" CssClass="TabsPageContent">
        <div class="GridContent">
            <asp:GridView ID="gvBinding" runat="server" AutoGenerateColumns="false" CellPadding="3"
                CssClass="UniGridGrid">
                <HeaderStyle HorizontalAlign="Left" CssClass="UniGridHead" />
                <Columns>
                    <asp:TemplateField HeaderText="Action">
                        <HeaderStyle Width="100" Wrap="false" />
                        <ItemTemplate>
                            <asp:ImageButton runat="server" CausesValidation="false" ID="lnkDelete" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"LocalProperty") %>'
                                CommandName="delete" OnCommand="lnkDelete_OnCommand" OnClientClick="return DeleteConfirm();" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="LocalProperty">
                        <HeaderStyle Width="200" Wrap="false" />
                    </asp:BoundField>
                    <asp:BoundField DataField="SourceProperty">
                        <HeaderStyle Wrap="false" />
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
        </div>
        <table style="margin: 10px;">
            <tr>
                <td style="white-space: nowrap" colspan="3">
                    <strong>
                        <asp:Label ID="lblCaption" runat="server" /></strong>
                </td>
            </tr>
            <tr>
                <td style="white-space: nowrap">
                    <asp:Label ID="lblProperty" runat="server" />
                </td>
                <td>
                    <asp:DropDownList ID="drpProperty" runat="server" Width="305" />
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="white-space: nowrap">
                    <asp:Label ID="lblSourceId" runat="server" />
                </td>
                <td>
                    <cms:CMSTextBox ID="txtSourceId" runat="server" Width="300" />
                </td>
                <td>
                    <cms:CMSRequiredFieldValidator ID="rfvSourceId" runat="server" ControlToValidate="txtSourceId" />
                </td>
            </tr>
            <tr>
                <td style="white-space: nowrap">
                    <asp:Label ID="lblSourceProprety" runat="server" />
                </td>
                <td>
                    <cms:CMSTextBox ID="txtSourceProperty" runat="server" Width="300" />
                </td>
                <td style="white-space: nowrap">
                    <cms:CMSRequiredFieldValidator ID="rfvSourceProperty" runat="server" ControlToValidate="txtSourceProperty" />
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <cms:LocalizedButton ID="btnOk" CssClass="ContentButton" runat="server" ResourceString="general.add" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Literal ID="ltlScript" runat="server" EnableViewState="false" />
    <cms:CMSButton ID="btnOnApply" runat="server" Visible="false" />
    <cms:CMSButton ID="btnOnOK" runat="server" Visible="false" />
</asp:Panel>

<script type="text/javascript">

        function DeleteConfirm()
        {
           return confirm(deleteConfirmation); 
        }
        
</script>

