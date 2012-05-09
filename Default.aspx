<%@ Page Language="C#" AutoEventWireup="true" Inherits="_Default"
    Theme="Default" ValidateRequest="false" CodeFile="Default.aspx.cs" %>

<%=DocType%>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <asp:literal runat="server" id="ltlTags" enableviewstate="false" />
</head>
<body class="<%=BodyClass%>" <%=BodyParameters%>>
    <form id="form1" runat="server">
        <cms:CMSPageManager ID="manPage" runat="server" />
        <asp:Label runat="server" ID="lblText" EnableViewState="false" />
    </form>
</body>
</html>
