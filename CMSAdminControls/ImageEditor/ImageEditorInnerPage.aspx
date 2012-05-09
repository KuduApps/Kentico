<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ImageEditorInnerPage.aspx.cs"
    Inherits="CMSAdminControls_ImageEditor_ImageEditorInnerPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Image editor content</title>
    <style type="text/css">
        .jcrop-holder
        {
            text-align: left;
        }
        .jcrop-vline, .jcrop-hline
        {
            font-size: 0;
            position: absolute;
            background: white;
        }
        .jcrop-vline
        {
            height: 100%;
            width: 1px !important;
        }
        .jcrop-hline
        {
            width: 100%;
            height: 1px !important;
        }
        .jcrop-handle
        {
            font-size: 1px;
            width: 7px !important;
            height: 7px !important;
            border: 1px solid #eee;
            background-color: #333;
        }
        .IE7 .jcrop-handle
        {
            width: 9px;
            height: 9px;
        }
        .jcrop-tracker
        {
            width: 100%;
            height: 100%;
        }
    </style>
</head>
<body class="<%=mBodyClass%>">
    <form id="form1" runat="server">
    <asp:Image ID="imgContent" runat="server" EnableViewState="false" />
    </form>

    <script type="text/javascript" language="javascript">
        //<![CDATA[
        var jcrop_api = null;
        jQuery(window).bind('load',
            function() {
                // Initialize crop after image is loaded
                if (window.parent.initializeCrop) {
                    initCrop();
                }
            });
        //]]>
    </script>

</body>
</html>
