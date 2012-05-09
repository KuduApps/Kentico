<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_PortalEngine_UI_PageTemplates_PageTemplate_Design"
    ValidateRequest="false" MaintainScrollPositionOnPostback="true" EnableEventValidation="false"
    CodeFile="PageTemplate_Design.aspx.cs" %>

<%=DocType%>
<html xmlns="http://www.w3.org/1999/xhtml" <%=XmlNamespace%>>
<head id="Head1" runat="server" enableviewstate="false">
    <title id="Title1" runat="server">My site</title>
    <asp:Literal runat="server" ID="ltlTags" EnableViewState="false" />
    <style type="text/css">
        body
        {
            margin: 0px;
            padding: 0px;
            height: 100%;
            font-family: Arial;
            font-size: small;
        }
    </style>
</head>
<body class="<%=BodyClass%>" <%=BodyParameters%>>
    <form id="form1" runat="server">
    <asp:PlaceHolder runat="server" ID="plcManagers">
        <cms:CMSPortalManager ID="manPortal" runat="server" EnableViewState="false" />
        <ajaxToolkit:ToolkitScriptManager ID="manScript" runat="server" ScriptMode="Release"
            EnableViewState="false" />
    </asp:PlaceHolder>
    <cms:CMSPagePlaceholder ID="plc" runat="server" />
    </form>
</body>
</html>
