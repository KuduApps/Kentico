<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSAdminControls_UI_Selectors_ItemSelection" CodeFile="ItemSelection.ascx.cs" %>
<div style="width: 100%">
    <br />
    <table cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <strong>
                    <asp:Label ID="lblLeftColumn" runat="server" /></strong>
            </td>
            <td style="width: 100%">
                &nbsp;
            </td>
            <td>
                <strong>
                    <asp:Label ID="lblRightColumn" runat="server" /></strong>
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:ListBox ID="lstLeftColumn" runat="server" Height="200px" Width="250px" SelectionMode="Multiple" />
            </td>
            <td align="center" style="width: 100%">
                <table cellspacing="0" cellpadding="1">
                    <tr>
                        <td>
                            <cms:CMSButton ID="btnMoveRight" runat="server" Text=">" CssClass="ShortButton" Font-Bold="True"
                                Font-Size="Larger" OnClick="MoveRightButton_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <cms:CMSButton ID="btnMoveLeft" runat="server" Text="<" CssClass="ShortButton" Font-Bold="True"
                                Font-Size="Larger" OnClick="MoveLeftButton_Click" />
                        </td>
                    </tr>
                </table>
            </td>
            <td align="center" style="vertical-align: middle;">
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <asp:ListBox ID="lstRightColumn" runat="server" Height="200px" Width="250px" SelectionMode="Multiple"
                                OnSelectedIndexChanged="lstRightColumn_SelectedIndexChanged"></asp:ListBox>
                        </td>
                        <asp:PlaceHolder ID="plcButtons" runat="server">
                            <td>
                                <table cellspacing="0" cellpadding="1">
                                    <tr>
                                        <td>
                                            <cms:CMSButton ID="btnUp" runat="server" CssClass="ShortButton" OnClick="btnUp_Click"
                                                Enabled="False" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <cms:CMSButton ID="btnDown" runat="server" CssClass="ShortButton" OnClick="btnDown_Click"
                                                Enabled="False" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </asp:PlaceHolder>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</div>
