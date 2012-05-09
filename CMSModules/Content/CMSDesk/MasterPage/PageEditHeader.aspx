<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Content_CMSDesk_MasterPage_PageEditHeader"
    Theme="Default" CodeFile="PageEditHeader.aspx.cs" %>

<%@ Register TagPrefix="cms" Namespace="CMS.skmMenuControl" Assembly="CMS.skmMenuControl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>Content - Edit</title>
    <style type="text/css">
        body
        {
            margin: 0px;
            padding: 0px;
            height: 100%;
        }
    </style>
</head>
<body class="<%=mBodyClass%>">
    <form id="form1" runat="server">

    <script type="text/javascript">
        //<![CDATA[
        // Save content
        function SaveMasterPage() {
            if (parent.frames['masterpagecontent'] != null) {
                parent.frames['masterpagecontent'].SaveDocument();
            }
        }
        //]]>
    </script>

    <asp:Panel runat="server" ID="Panel1" CssClass="EditMenuBody" EnableViewState="false">
        <asp:Panel runat="server" ID="pnlMenu" CssClass="ContentEditMenu">
            <cms:Menu ID="menuElem" runat="server" />
        </asp:Panel>
    </asp:Panel>
    </form>
</body>
</html>
