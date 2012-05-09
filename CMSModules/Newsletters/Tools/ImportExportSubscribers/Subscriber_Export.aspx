<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Newsletters_Tools_ImportExportSubscribers_Subscriber_Export"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Tools - Newsletter subscribers"
    CodeFile="Subscriber_Export.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/UniSelector/UniSelector.ascx" TagName="UniSelector"
    TagPrefix="cms" %>
<asp:Content ContentPlaceHolderID="plcContent" ID="content" runat="server">
    <cms:CMSUpdatePanel ID="pnlUpdate" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Label ID="lblInfo" runat="server" CssClass="InfoLabel" EnableViewState="false"
                Visible="false" />
            <strong>
                <cms:LocalizedLabel ID="lblSelectSub" runat="server" CssClass="ContentLabel" EnableViewState="false"
                    ResourceString="Subscriber_Export.lblSelectSub" />
            </strong>
            <br />
            <br />
            <cms:UniSelector ID="usNewsletters" runat="server" IsLiveSite="false" ObjectType="Newsletter.Newsletter"
                SelectionMode="Multiple" ResourcePrefix="newsletterselect" />
            <br />
            <br />
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <cms:LocalizedLabel ID="lblSubscribers" runat="server" CssClass="ContentLabel" EnableViewState="false"
                            DisplayColon="true" ResourceString="newsletter.exportsubscribers" />
                    </td>
                    <td>
                        <cms:LocalizedRadioButtonList ID="rblExportList" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="0" />
                            <asp:ListItem Value="1" />
                            <asp:ListItem Value="2" />
                        </cms:LocalizedRadioButtonList>
                    </td>
                </tr>
            </table>
            <br />
            <br />
            <strong>
                <cms:LocalizedLabel ID="lblExportedSub" runat="server" CssClass="ContentLabel" EnableViewState="false"
                    ResourceString="Subscriber_Export.lblExportedSub" />
            </strong>
            <br />
            <br />
            <cms:CMSTextBox ID="txtExportSub" runat="server" TextMode="MultiLine" CssClass="TextAreaLarge"
                Enabled="false" EnableViewState="false" />
            <br />
            <br />
            <cms:LocalizedButton ID="btnExport" runat="server" CssClass="LongSubmitButton" EnableViewState="false"
                ResourceString="Subscriber_Export.btnExport" OnClick="btnExport_Click" />
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnExport" EventName="Click" />
        </Triggers>
    </cms:CMSUpdatePanel>
</asp:Content>
