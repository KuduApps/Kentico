<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_DocumentTypes_Pages_Development_DocumentType_Edit_Form" Theme="Default"
    ValidateRequest="false" EnableEventValidation="false" CodeFile="DocumentType_Edit_Form.aspx.cs" %>

<%@ Register Src="~/CMSModules/AdminControls/Controls/Class/Layout.ascx" TagName="Layout" TagPrefix="cms" %>    

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>Document Type Edit - Form</title>
    <style type="text/css">
		body
		{
			margin: 0px;
			padding: 0px;
			height: 100%; 
		}
	</style>
</head>
<body class="TabsBody <%=mBodyClass%>">
    <form id="form1" runat="server">
        <asp:Panel runat="server" ID="pnlTabsBody" CssClass="TabsPageBody">
            <asp:Panel runat="server" ID="pnlTabsScroll" CssClass="TabsPageScrollArea">
                <asp:Panel runat="server" ID="pnlTab" CssClass="TabsPageContent">
                    <cms:Layout ID="layoutElem" runat="server" />
                </asp:Panel>
            </asp:Panel>
        </asp:Panel>
    </form>
</body>
</html>
  
