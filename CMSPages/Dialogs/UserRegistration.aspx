<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="CMSPages_Dialogs_UserRegistration" CodeFile="UserRegistration.aspx.cs" %>

<%@ Register Src="~/CMSModules/Membership/Controls/RegistrationApproval.ascx" TagName="RegistrationApproval"
    TagPrefix="cms" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>User registration approval page</title>
</head>
<body>
    <form id="form1" runat="server">
        <cms:RegistrationApproval id="RegistrationApproval" runat="server" />
    </form>
</body>
</html>
