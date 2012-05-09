<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Content_CMSDesk_Edit_EditPage"
    Theme="Default" CodeFile="EditPage.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>Edit page</title>
    <style type="text/css">
        body, html
        {
            margin: 0px;
            padding: 0px;
            height: 100%;
            overflow: hidden;
        }
    </style>
</head>
<body class="<%=mBodyClass%>" onbeforeunload="if (window.ConfirmClose) { return ConfirmClose(event); }">
    <form id="form1" runat="server">
    <iframe width="100%" height="100%" id="pageview" name="pageview" scrolling="auto"
        frameborder="0" enableviewstate="false" src="<%=viewpage%>" onload="<%=loadScript %>"
        style="position: absolute; z-index: 9998;"></iframe>
    </form>

    <script type="text/javascript" language="javascript">
        //<![CDATA[
        if ((window.frames['pageview'] != null) && window.frames['pageview'].InitializePage) {
            window.frames['pageview'].InitializePage();
        }
        //]]>
    </script>

</body>
</html>
