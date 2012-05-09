<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_Newsletters_Tools_Newsletters_Newsletter_Issue_Preview" Theme="Default" CodeFile="Newsletter_Issue_Preview.aspx.cs" %>

<%@ Register Src="Newsletter_Preview.ascx" TagName="Newsletter_Preview" TagPrefix="cms" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>Tools - New newsletter issue</title>
    <style type="text/css">
		body
		{
			margin: 0px;
			padding: 0px;
			height:100%; 
		}
	</style>
    <base target="_self" />
</head>
<body class="<%=mBodyClass%>">
    <form id="form1" runat="server">
        <asp:Panel runat="server" ID="pnlBody" CssClass="TabsPageBody">
            <asp:Panel runat="server" ID="pnlContainer" CssClass="TabsPageContainer">
                <asp:Panel runat="server" ID="pnlScroll" CssClass="TabsPageScrollArea2">
                    <asp:Panel runat="server" ID="pnlTab" CssClass="TabsPageContent">
                        <asp:Panel runat="server" ID="pnlContent" CssClass="PageContent">
				            <asp:Label ID="lblSent" runat="server" EnableViewState="false" CssClass="InfoLabel" />
				            <br />
                            <cms:Newsletter_Preview ID="preview" runat="server" />
                        </asp:Panel>
                    </asp:Panel>
                </asp:Panel>
            </asp:Panel>
        </asp:Panel>
    </form>
</body>
</html>