SET IDENTITY_INSERT [CMS_WebPartLayout] ON
INSERT INTO [CMS_WebPartLayout] ([WebPartLayoutID], [WebPartLayoutCodeName], [WebPartLayoutDisplayName], [WebPartLayoutDescription], [WebPartLayoutCode], [WebPartLayoutCheckedOutFilename], [WebPartLayoutCheckedOutByUserID], [WebPartLayoutCheckedOutMachineName], [WebPartLayoutVersionGUID], [WebPartLayoutWebPartID], [WebPartLayoutGUID], [WebPartLayoutLastModified], [WebPartLayoutCSS]) VALUES (121, N'filter', N'filter', N'', N'<%@ Control Language="C#" AutoEventWireup="true" CodeFile="~/CMSWebParts/Search/cmscompletesearchdialog.ascx.cs" Inherits="CMSWebParts_Search_cmscompletesearchdialog" %>
<div class="SearchDialog">
    <cms:CMSSearchDialog ID="srchDialog" runat="server" />
</div>
<div class="SearchResults">
    <cms:CMSSearchResults ID="srchResults" runat="server" FilterName="SearchDialog" />
</div>', NULL, NULL, NULL, N'a2b6bbe5-f660-4aad-8929-5c73efac3e7d', 173, '3239ff22-011a-4bc9-b35d-7d967dcc7b47', '20080819 17:14:18', NULL)
INSERT INTO [CMS_WebPartLayout] ([WebPartLayoutID], [WebPartLayoutCodeName], [WebPartLayoutDisplayName], [WebPartLayoutDescription], [WebPartLayoutCode], [WebPartLayoutCheckedOutFilename], [WebPartLayoutCheckedOutByUserID], [WebPartLayoutCheckedOutMachineName], [WebPartLayoutVersionGUID], [WebPartLayoutWebPartID], [WebPartLayoutGUID], [WebPartLayoutLastModified], [WebPartLayoutCSS]) VALUES (115, N'EcommerceSite', N'Ecommerce site', N'', N'<%@ Control Language="C#" AutoEventWireup="true" CodeFile="~/CMSWebParts/Ecommerce/ShoppingCart/ShoppingCartMiniPreviewWebPart.ascx.cs"
    Inherits="CMSWebParts_Ecommerce_ShoppingCart_ShoppingCartMiniPreviewWebPart" %>
<cms:cmsupdatepanel id="upnlAjax" runat="server">
    <ContentTemplate>
<table cellspacing="0">
    <tr>
        <td colspan="3" style="padding-right: 5px; height:38px; padding-left:15px;">
<asp:Literal runat="server" ID="ltlRTLFix" text="<%# rtlFix %>" EnableViewState="false"></asp:Literal> 
<asp:PlaceHolder ID="plcShoppingCart" runat="server" EnableViewState="false">
                <asp:HyperLink ID="lnkShoppingCart" runat="server" CssClass="ShoppingCartLink"></asp:HyperLink>
            </asp:PlaceHolder>
            <asp:PlaceHolder ID="plcMyAccount" runat="server" EnableViewState="false">|&nbsp;<asp:HyperLink ID="lnkMyAccount"
                runat="server" CssClass="ShoppingCartLink"></asp:HyperLink>
            </asp:PlaceHolder>
            <asp:PlaceHolder ID="plcMyWishlist" runat="server" EnableViewState="false">|&nbsp;<asp:HyperLink ID="lnkMyWishlist"
                runat="server" CssClass="ShoppingCartLink"></asp:HyperLink>
            </asp:PlaceHolder>
        </td>
    </tr>
    <asp:PlaceHolder ID="plcTotalPrice" runat="server" EnableViewState="false">
    <tr>
  <td align="left" style="padding-left:15px; width:22%;">
      <asp:Image ID="imgCartIcon" runat="server" CssClass="ShoppingCartIcon" />
  </td>
        <td align="left" style="font-weight:bold;">
            <asp:Label ID="lblTotalPriceTitle" runat="server" Text="" CssClass="SmallTextLabel"></asp:Label>
        </td>
        <td align="right" style="padding-right: 5px;font-weight:bold;">
            <asp:Label ID="lblTotalPriceValue" runat="server" Text="" CssClass="SmallTextLabel"></asp:Label>
        </td>
    </tr>
    </asp:PlaceHolder>
</table>
    </ContentTemplate>
</cms:cmsupdatepanel>', N'', NULL, N'', N'b291bab6-3c1e-4323-9f85-9b38b73b4324', 221, 'c023897f-17cf-46d1-a71f-ff9104fd23f5', '20110809 19:07:40', N'')
INSERT INTO [CMS_WebPartLayout] ([WebPartLayoutID], [WebPartLayoutCodeName], [WebPartLayoutDisplayName], [WebPartLayoutDescription], [WebPartLayoutCode], [WebPartLayoutCheckedOutFilename], [WebPartLayoutCheckedOutByUserID], [WebPartLayoutCheckedOutMachineName], [WebPartLayoutVersionGUID], [WebPartLayoutWebPartID], [WebPartLayoutGUID], [WebPartLayoutLastModified], [WebPartLayoutCSS]) VALUES (223, N'Simple', N'Simple', N'', N'<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSWebParts_Ecommerce_Wishlist" CodeFile="~/CMSWebParts/Ecommerce/Wishlist.ascx.cs" %>
<div class="WishlistTable">                          
  <div class="CartStepBody">
    <asp:Label ID="lblTitle" runat="server" Visible="false" EnableViewState="false" />
    <asp:Panel ID="pnlWishlist" runat="server" CssClass="CartStepPanel">
    <asp:Panel ID="pnlWishlistInner" runat="server" CssClass="CartStepInnerPanel" >
      <asp:Label ID="lblInfo" runat="server" CssClass="WishlistInfo" EnableViewState="false" Visible="false" />
        <cms:queryrepeater id="repeater" runat="server" />
      </asp:Panel>
    </asp:Panel>
  </div>
  <div class="btnContinue">
    <cms:CMSButton ID="btnContinue" runat="server" OnClick="btnContinue_Click" CssClass="LongButton" />
  </div>
</div>
<asp:Literal ID="ltlScript" runat="server" EnableViewState="false" />', NULL, NULL, NULL, N'132b84f8-033f-4e3e-8494-b00b3404064e', 239, '6c1a98c7-ed73-4c0b-b0a2-df0efdc877c1', '20110527 15:11:26', N'')
INSERT INTO [CMS_WebPartLayout] ([WebPartLayoutID], [WebPartLayoutCodeName], [WebPartLayoutDisplayName], [WebPartLayoutDescription], [WebPartLayoutCode], [WebPartLayoutCheckedOutFilename], [WebPartLayoutCheckedOutByUserID], [WebPartLayoutCheckedOutMachineName], [WebPartLayoutVersionGUID], [WebPartLayoutWebPartID], [WebPartLayoutGUID], [WebPartLayoutLastModified], [WebPartLayoutCSS]) VALUES (169, N'fsa', N'fas', N'', N'<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSWebParts_UserContributions_ContributionList" CodeFile="~/CMSWebParts/UserContributions/ContributionList.ascx.cs" %>
<%@ Register Src="~/CMSModules/Content/Controls/UserContributions/ContributionList.ascx" TagName="ContributionList" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/Content/Controls/UserContributions/EditForm.ascx" TagName="ContributionEdit" TagPrefix="cms" %>
<cms:ContributionList ID="list" runat="server"  />', NULL, NULL, NULL, N'83640ffe-7927-4088-8565-b06dbe4ff5d1', 243, '8b53582e-ddad-44e6-b309-acfa1a136283', '20110212 11:52:08', NULL)
INSERT INTO [CMS_WebPartLayout] ([WebPartLayoutID], [WebPartLayoutCodeName], [WebPartLayoutDisplayName], [WebPartLayoutDescription], [WebPartLayoutCode], [WebPartLayoutCheckedOutFilename], [WebPartLayoutCheckedOutByUserID], [WebPartLayoutCheckedOutMachineName], [WebPartLayoutVersionGUID], [WebPartLayoutWebPartID], [WebPartLayoutGUID], [WebPartLayoutLastModified], [WebPartLayoutCSS]) VALUES (124, N'Block', N'Block', N'', N'<%@ Control Language="C#" AutoEventWireup="true" CodeFile="~/CMSWebParts/Membership/Logon/LogonMiniForm.ascx.cs"
    Inherits="CMSWebParts_Membership_Logon_LogonMiniForm" %>
<asp:Login ID="loginElem" runat="server" DestinationPageUrl="~/Default.aspx" EnableViewState="false">
    <LayoutTemplate>
        <asp:Panel ID="pnlLogonMiniForm" runat="server" DefaultButton="btnLogon" EnableViewState="false">
            <cms:LocalizedLabel ID="lblUserName" runat="server" AssociatedControlID="UserName" EnableViewState="false" />
            <asp:TextBox ID="UserName" runat="server" CssClass="LogonField" EnableViewState="false" />
            <asp:RequiredFieldValidator ID="rfvUserNameRequired" runat="server" ControlToValidate="UserName"
                ValidationGroup="Login1" Display="Dynamic" EnableViewState="false">*</asp:RequiredFieldValidator>
            <cms:LocalizedLabel ID="lblPassword" runat="server" AssociatedControlID="Password" EnableViewState="false" />
            <asp:TextBox ID="Password" runat="server" TextMode="Password" CssClass="LogonField" EnableViewState="false" />
            <cms:LocalizedButton ID="btnLogon" ResourceString="LogonForm.LogOnButton" runat="server" CommandName="Login" ValidationGroup="Login1" EnableViewState="false" />
            <asp:ImageButton ID="btnImageLogon" runat="server" Visible="false" CommandName="Login"
                ValidationGroup="Login1" EnableViewState="false" /><br />
            <asp:Label ID="FailureText" CssClass="ErrorLabel" runat="server" EnableViewState="false" />
        </asp:Panel>
    </LayoutTemplate>
</asp:Login>', NULL, NULL, NULL, N'122763c8-4624-409a-beeb-63e356174c03', 525, '100f8305-87b9-4d28-bd8f-08bd1d9497a5', '20100420 09:51:10', NULL)
SET IDENTITY_INSERT [CMS_WebPartLayout] OFF
