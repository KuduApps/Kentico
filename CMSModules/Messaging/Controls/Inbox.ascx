<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_Messaging_Controls_Inbox"
    CodeFile="Inbox.ascx.cs" %>
<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/Messaging/Controls/SendMessage.ascx" TagName="SendMessage"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/Messaging/Controls/ViewMessage.ascx" TagName="ViewMessage"
    TagPrefix="cms" %>
<cms:CMSUpdatePanel ID="pnlInbox" runat="server">
    <ContentTemplate>
        <div class="MessagingBox">
            <asp:Label ID="lblInfo" runat="Server" CssClass="Info" Visible="false" EnableViewState="false" />
            <asp:Label ID="lblError" runat="Server" CssClass="Error" Visible="false" EnableViewState="false" />
            <asp:Panel ID="pnlBackToList" runat="Server" CssClass="BackToList PageTitleBreadCrumbsPadding" Visible="false">
                <cms:LocalizedLinkButton ID="btnBackToList" runat="server" EnableViewState="false"
                    ResourceString="Messaging.BackToList" />
                    <span class="TitleBreadCrumbSeparator">&nbsp;</span>
                <cms:LocalizedLabel ID="lblBackToList" runat="server" ResourceString="messaging.newmessage"
                    EnableViewState="false" />
                <br />
                <br />
            </asp:Panel>
            <asp:Panel ID="pnlNew" runat="server" CssClass="NewPanel" Visible="false">
                <div class="NewMessageHeader">
                    <cms:LocalizedLabel ID="lblNewMessageHeader" runat="server" EnableViewState="false"
                        ResourceString="Messaging.NewMessage" />
                </div>
                <cms:SendMessage ID="ucSendMessage" runat="server" />
            </asp:Panel>
            <asp:Panel ID="pnlView" runat="server" CssClass="ViewPanel" Visible="false">
                <asp:Panel ID="pnlActions" CssClass="MessageActionsPanel" runat="server" EnableViewState="false">
                    <cms:CMSImage ID="imgReply" runat="server" EnableViewState="false" ImageAlign="Middle" CssClass="NewItemImage" /><cms:LocalizedLinkButton ID="btnReply" runat="Server" EnableViewState="false" ResourceString="Messaging.Reply" CssClass="NewItemLink" />&nbsp;
                    <cms:CMSImage ID="imgForward" runat="server" EnableViewState="false" ImageAlign="Middle" CssClass="NewItemImage" /><cms:LocalizedLinkButton ID="btnForward" runat="Server" EnableViewState="false" ResourceString="Messaging.Forward" CssClass="NewItemLink" />&nbsp;
                    <cms:CMSImage ID="imgDelete" runat="server" EnableViewState="false" ImageAlign="Middle" CssClass="NewItemImage" /><cms:LocalizedLinkButton ID="btnDelete" runat="Server" EnableViewState="false" ResourceString="Messaging.Delete" CssClass="NewItemLink" />
                </asp:Panel>
                <br />
                <div class="ViewMessageHeader">
                    <cms:LocalizedLabel ID="lblViewMessageHeader" runat="server" EnableViewState="false"
                        ResourceString="Messaging.ViewMessage" />
                </div>
                <cms:ViewMessage ID="ucViewMessage" runat="server" StopProcessing="true" />
            </asp:Panel>
            <asp:Panel ID="pnlList" runat="server" CssClass="ListPanel">
                <asp:Panel ID="pnlGeneralActions" runat="Server" CssClass="GeneralActions">
                    <cms:CMSImage ID="imgNew" runat="server" EnableViewState="false" ImageAlign="Middle" CssClass="NewItemImage" /><cms:LocalizedLinkButton ID="btnNewMessage" runat="Server" CssClass="NewMessage"
                        EnableViewState="false" ResourceString="Messaging.NewMessage" />
                </asp:Panel>
                <br />
                <cms:UniGrid runat="server" ID="inboxGrid" ShortID="g" GridName="~/CMSModules/Messaging/Controls/Inbox.xml"
                    DelayedReload="true" OrderBy="MessageSent DESC" />
            </asp:Panel>
            <asp:Panel ID="pnlAction" runat="server" CssClass="FooterInfo">
                <div class="LeftAlign">
                    <asp:DropDownList ID="drpWhat" runat="server" CssClass="DropDownFieldSmall" />
                    <asp:DropDownList ID="drpAction" runat="server" CssClass="DropDownFieldSmall" />
                    <cms:LocalizedButton ID="btnOk" runat="server" ResourceString="general.ok" CssClass="SubmitButton SelectorButton"
                        EnableViewState="false" />
                </div>
                <div class="RightAlign">
                    <asp:Label ID="lblFooter" runat="Server" EnableViewState="false" />
                </div>
                <br class="ClearBoth" />
                <br />
                <asp:Label ID="lblActionInfo" runat="server" EnableViewState="false" CssClass="InfoLabel" />
            </asp:Panel>
        </div>
        <asp:Button ID="btnHidden" runat="server" CssClass="HiddenButton" EnableViewState="false" />
        <asp:HiddenField ID="hdnValue" runat="server" EnableViewState="false" />
    </ContentTemplate>
</cms:CMSUpdatePanel>
