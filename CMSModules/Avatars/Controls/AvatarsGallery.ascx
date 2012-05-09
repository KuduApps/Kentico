<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_Avatars_Controls_AvatarsGallery" CodeFile="AvatarsGallery.ascx.cs" %>
<asp:Literal runat="server" ID="ltlScript" />
<div class="DefaultAvatarSelector">
    <asp:Panel runat="Server" ID="pnlAvatars" ScrollBars="Auto">
        <asp:HiddenField ID="hiddenAvatarGuid" runat="server" />
        <cms:LocalizedLabel runat="server" ID="lblInfo" Visible="false" EnableViewState="false" />
        <cms:BasicRepeater ID="repAvatars" runat="server">
            <HeaderTemplate>
                <table class="DefaultvatarSelectorTable">
                    <tr>
            </HeaderTemplate>
            <ItemTemplate>
                <td>
                    <img src="<%# avatarUrl + Eval("AvatarGuid")%>" title="<%# Eval("AvatarName") %>" alt="<%# Eval("AvatarName") %>"
                        id="<%# "avat" + Eval("AvatarGuid")%>" style="border: 5px solid #FFFFFF;" onclick="markImage(this.id);" />
                </td>
            </ItemTemplate>
            <FooterTemplate>
                </tr></table>
            </FooterTemplate>
        </cms:BasicRepeater>
    </asp:Panel>
</div>
<table class="AvatarSelectorPager">
    <tr>
        <cms:UniPager ID="pgrAvatars" runat="server" QueryStringKey="tpage" PageControl="repAvatars"
            PageSize="5" GroupSize="10" PagerMode="Querystring" HidePagerForSinglePage="true"
            EnableViewState="false">
            <PreviousGroupTemplate>
                <td>
                    <a href="<%# CreateUrl("tpage", Eval("PreviousGroup")) %>">...</a>
                </td>
            </PreviousGroupTemplate>
            <PageNumbersTemplate>
                <td>
                    <a href="<%# CreateUrl("tpage", Eval("Page")) %>">
                        <%# Eval("Page") %>
                    </a>
                </td>
            </PageNumbersTemplate>
            <CurrentPageTemplate>
                <td>
                    <span>
                        <%# Eval("Page") %>
                    </span>
                </td>
            </CurrentPageTemplate>
            <NextGroupTemplate>
                <td>
                    <a href="<%# CreateUrl("tpage", Eval("NextGroup"))  %>">...</a>
                </td>
            </NextGroupTemplate>
        </cms:UniPager>
    </tr>
</table>
<asp:PlaceHolder ID="plcButtons" runat="server">
    <div class="PageFooterLine">
        <div class="FloatRight">
            <cms:LocalizedButton runat="Server" CssClass="SubmitButton" ID="btnOk" ResourceString="general.ok"
                EnableViewState="false" /><cms:LocalizedButton runat="server" CssClass="SubmitButton"
                    ID="btnCancel" ResourceString="general.cancel" EnableViewState="false" />
        </div>
    </div>
</asp:PlaceHolder>
