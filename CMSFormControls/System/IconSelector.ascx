<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSFormControls_System_IconSelector" CodeFile="IconSelector.ascx.cs" %>
<cms:CMSUpdatePanel ID="pnlUpdateContent" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <div class="Hidden">
            <cms:CMSUpdatePanel ID="pnlUpdateHidden" runat="server">
                <ContentTemplate>
                    <asp:Literal ID="ltlScript" runat="server" EnableViewState="false"></asp:Literal>
                    <asp:HiddenField ID="hdnAction" runat="server"></asp:HiddenField>
                    <asp:HiddenField ID="hdnArgument" runat="server"></asp:HiddenField>
                    <asp:Button ID="hdnButton" runat="server" OnClick="hdnButton_Click" CssClass="HiddenButton"
                        EnableViewState="false" />
                </ContentTemplate>
            </cms:CMSUpdatePanel>
        </div>
        <asp:Label ID="lblError" runat="server" CssClass="ErrorLabel" />
        <cms:CMSUpdatePanel ID="pnlUpdate" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="IconSelector">
                    <asp:RadioButton ID="radPredefinedIcon" runat="server" GroupName="iconType" />
                    <div class="PredefinedIcons">
                        <asp:Panel ID="pnlMain" runat="server" CssClass="Table">
                            <asp:Literal ID="ltlFolders" runat="server" EnableViewState="false" />
                        </asp:Panel>
                        <asp:Panel ID="pnlChild" runat="server" CssClass="Table">
                            <cms:CMSUpdatePanel ID="pnlUpdateIcons" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:Literal ID="ltlIcons" runat="server" EnableViewState="false" />
                                </ContentTemplate>
                            </cms:CMSUpdatePanel>
                        </asp:Panel>
                    </div>
                    <table style="padding-top:7px">
                        <tr>
                            <td>
                                <asp:RadioButton ID="radCustomIcon" runat="server" GroupName="iconType" />
                            </td>
                            <td>
                                <cms:MediaSelector ID="mediaSelector" runat="server" CssClass="Media" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:RadioButton ID="radDoNotDisplay" runat="server" GroupName="iconType" />
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </cms:CMSUpdatePanel>
    </ContentTemplate>
</cms:CMSUpdatePanel>
