<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_Friends_CMSPages_FriendshipManagement" Title="Friendship management"
    ValidateRequest="false" Theme="Default" CodeFile="FriendshipManagement.aspx.cs" %>

<%@ Register Src="~/CMSWebParts/Community/Friends/FriendshipManagement.ascx" TagName="FriendshipManagement"
    TagPrefix="cms" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="headElem" runat="server" enableviewstate="false">
    <title>Friendship management</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <cms:FriendshipManagement ID="managementElem" runat="server" />
    </div>
    </form>
</body>
</html>
