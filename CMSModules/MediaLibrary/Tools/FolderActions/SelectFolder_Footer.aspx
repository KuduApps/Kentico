<%@ Page Language="C#" Theme="Default" AutoEventWireup="true"
    Inherits="CMSModules_MediaLibrary_Tools_FolderActions_SelectFolder_Footer" EnableEventValidation="false" CodeFile="SelectFolder_Footer.aspx.cs" %>

<%@ Register Src="~/CMSModules/MediaLibrary/Controls/MediaLibrary/FolderActions/SelectFolderFooter.ascx"
    TagName="FolderFooter" TagPrefix="cms" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>Copy / Move</title>
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
    <ajaxToolkit:ToolkitScriptManager ID="scriptManager" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <div class="MediaLibrary">
        <cms:FolderFooter ID="folderFooter" runat="server"></cms:FolderFooter>
    </div>
    </form>
</body>
</html>
