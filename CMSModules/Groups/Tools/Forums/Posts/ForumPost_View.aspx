<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Groups_Tools_Forums_Posts_ForumPost_View"
    Theme="Default" ValidateRequest="false" CodeFile="ForumPost_View.aspx.cs" %>

<%@ Register Src="~/CMSModules/Forums/Controls/Posts/PostView.ascx" TagName="PostView"
    TagPrefix="cms" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>Forums - Forum Tree</title>
    <style type="text/css">
        body
        {
            margin: 0px;
            padding: 0px;
            height: 100%;
            width: 100%;
            overflow: auto;
        }
    </style>
</head>
<body class="<%=mBodyClass%>">
    <form id="form1" runat="server">
        <div class="PostView">
            <cms:PostView ID="postView" runat="server" />
        </div>
    </form>
</body>
</html>
