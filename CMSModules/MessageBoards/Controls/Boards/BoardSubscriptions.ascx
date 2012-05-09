<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_MessageBoards_Controls_Boards_BoardSubscriptions"
    CodeFile="BoardSubscriptions.ascx.cs" %>
<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<cms:UniGrid ID="boardSubscriptions" runat="server" GridName="~/CMSModules/MessageBoards/Tools/Boards/BoardSubscriptions.xml"
    Columns="SubscriptionID, SubscriptionEmail, UserName" OrderBy="SubscriptionEmail" />
