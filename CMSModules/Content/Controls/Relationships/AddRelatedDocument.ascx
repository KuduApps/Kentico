<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AddRelatedDocument.ascx.cs"
    Inherits="CMSModules_Content_Controls_Relationships_AddRelatedDocument" %>
<%@ Register Src="~/CMSModules/Content/FormControls/Relationships/selectRelationshipNames.ascx"
    TagName="RelationshipNameSelector" TagPrefix="cms" %>
<asp:Panel runat="server" ID="pnlContainer">
    <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
        Visible="false" />
    <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
        Visible="false" />
    <asp:Table ID="TableRelationship" CssClass="UniGridGrid TableRelationship" runat="server" BorderStyle="solid"
        BorderWidth="1" CellPadding="3" CellSpacing="0" Width="100%">
        <asp:TableHeaderRow CssClass="UniGridHead">
            <asp:TableHeaderCell ID="leftCell" HorizontalAlign="Left" runat="server" />
            <asp:TableHeaderCell ID="middleCell" HorizontalAlign="Center" runat="server" />
            <asp:TableHeaderCell ID="rightCell" HorizontalAlign="Right" runat="server" />
        </asp:TableHeaderRow>
        <asp:TableRow>
            <asp:TableCell Width="40%" CssClass="LeftTableColumn" BorderWidth="0">
                <asp:Panel ID="pnlLeftCurrentNode" runat="server" CssClass="AddRelatedDocumentsCurrentNode">
                    <asp:Label ID="lblLeftNode" runat="server" />
                </asp:Panel>
                <asp:Panel ID="pnlLeftSelectedNode" runat="server">
                    <cms:CMSTextBox ID="txtLeftNode" runat="server" CssClass="TextBoxField" Width="220" />
                </asp:Panel>
            </asp:TableCell>
            <asp:TableCell HorizontalAlign="Left" Width="20%" BorderWidth="0">
                <cms:CMSUpdatePanel ID="pnlUpdate" runat="server">
                    <ContentTemplate>
                        <asp:Label ID="lblRelName" runat="server" />
                        <cms:RelationshipNameSelector ID="relNameSelector" runat="server" ReturnColumnName="RelationshipNameID"
                            AllowedForObjects="false" />
                    </ContentTemplate>
                </cms:CMSUpdatePanel>
            </asp:TableCell>
            <asp:TableCell Width="40%" CssClass="RightTableColumn" BorderWidth="0">
                <asp:Panel ID="pnlRightCurrentNode" runat="server" CssClass="AddRelatedDocumentsCurrentNode">
                    <asp:Label ID="lblRightNode" runat="server" />
                </asp:Panel>
                <asp:Panel ID="pnlRightSelectedNode" runat="server">
                    <cms:CMSTextBox ID="txtRightNode" runat="server" CssClass="TextBoxField" Width="220" />
                </asp:Panel>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell Width="40%" CssClass="LeftTableColumn" BorderWidth="0">
                <asp:Panel ID="pnlLeftSelectButton" runat="server">
                    <cms:LocalizedButton ID="btnLeftNode" runat="server" ResourceString="Relationship.SelectDocument"
                        CssClass="LongButton" />
                </asp:Panel>
            </asp:TableCell>
            <asp:TableCell HorizontalAlign="center" Width="20%" BorderWidth="0">
                <cms:LocalizedButton ID="btnSwitchSides" runat="server" OnClientClick="SwitchSides();return false;"
                    CssClass="LongButton" ResourceString="Relationship.SwitchSides" />
            </asp:TableCell>
            <asp:TableCell Width="40%" CssClass="RightTableColumn" BorderWidth="0">
                <asp:Panel ID="pnlRightSelectButton" runat="server">
                    <cms:LocalizedButton ID="btnRightNode" runat="server" ResourceString="Relationship.SelectDocument"
                        CssClass="LongButton" />
                </asp:Panel>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <br />
    <table width="100%">
        <tr>
            <td align="right">
                <cms:LocalizedButton ID="btnOk" runat="server" Text="" OnClick="btnOk_Click" CssClass="SubmitButton"
                    ResourceString="General.OK" />
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hdnSelectedNodeId" runat="server" Value="" />
    <asp:HiddenField ID="hdnCurrentOnLeft" runat="server" Value="true" />
</asp:Panel>
