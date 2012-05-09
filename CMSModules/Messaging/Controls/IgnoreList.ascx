<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_Messaging_Controls_IgnoreList"
    CodeFile="IgnoreList.ascx.cs" %>
<%@ Register Src="~/CMSModules/Membership/FormControls/Users/selectuser.ascx" TagName="SelectUser"
    TagPrefix="cms" %>
<cms:CMSUpdatePanel ID="pnlIgnoreList" runat="server">
    <ContentTemplate>
        <div class="MessagingBox">
            <asp:Panel ID="pnlInfo" CssClass="Info" Visible="false" runat="server">
                <asp:Label runat="server" ID="lblInfo" EnableViewState="false" CssClass="InfoLabel" />
                <asp:Label runat="server" ID="lblError" EnableViewState="false" CssClass="ErrorLabel" />
            </asp:Panel>
            <div class="ListPanel">
                <cms:LocalizedLabel ID="lblAvialable" runat="server" CssClass="BoldInfoLabel" ResourceString="ignorelist.available"
                    DisplayColon="true" EnableViewState="false" />
                <cms:SelectUser ID="usUsers" runat="server" SelectionMode="Multiple" />
            </div>
        </div>
    </ContentTemplate>
</cms:CMSUpdatePanel>
