<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSSiteManager_TrialVersion"
    Theme="Default" CodeFile="TrialVersion.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>Trial version</title>
    <style type="text/css">
			A
			{
				color:white;
				text-decoration: underline;
			}
		</style>
</head>
<body style="margin-top: 0; background: #D87E36; color: White;" class="<%=mBodyClass%>">
    <form id="form1" runat="server">
        <div>
            <asp:Literal ID="ltlText" runat="server" EnableViewState="false" /></div>
    </form>
</body>
</html>
