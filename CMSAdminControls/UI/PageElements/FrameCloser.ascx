<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSAdminControls_UI_PageElements_FrameCloser" CodeFile="FrameCloser.ascx.cs" %>

<img src="<%= minimizeUrl %>" alt="" id="imgMinimize" onclick="Minimize();" />
<img src="<%= maximizeUrl %>" alt="" id="imgMaximize" onclick="Maximize();" style="display: none;" />

<asp:Literal ID="ltlScript" runat="server" />

<script type="text/javascript"> 
//<![CDATA[
    var elemMinimize = document.getElementById('imgMinimize');
    var elemMaximize = document.getElementById('imgMaximize');
    var originalCols = null;

    function Minimize()
    {
        var fs = parent.document.getElementById('colsFrameset');
        if (fs)
        {
            originalCols = fs.cols;
            fs.cols = minCols;
            elemMinimize.style.display = 'none';
            elemMaximize.style.display = 'inline';
        }
    }

    function Maximize()
    {
        var fs = parent.document.getElementById('colsFrameset');
        if (fs)
        {
            fs.cols = originalCols;
            elemMinimize.style.display = 'inline';
            elemMaximize.style.display = 'none';
        }
    }
//]]>
</script>