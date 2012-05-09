<%@ Page Language="C#" AutoEventWireup="True" CodeFile="SubscriptionApproval.aspx.cs"
    Inherits="CMSModules_Newsletters_CMSPages_SubscriptionApproval" Theme="Default" %>

<%@ Register Src="~/CMSModules/Newsletters/Controls/SubscriptionApproval.ascx" TagName="SubscriptionApproval"
    TagPrefix="cms" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head" runat="server" enableviewstate="false">
    <title>Subscription approval</title>
</head>
<body>
    <form id="form1" runat="server">
    <cms:SubscriptionApproval ID="subscriptionApproval" runat="server" Visible="true" />
    </form>
</body>
</html>
