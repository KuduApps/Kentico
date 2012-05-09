<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_Newsletters_Tools_Newsletters_Newsletter_ContentEditor" CodeFile="Newsletter_ContentEditor.ascx.cs" %>
<iframe id="iframeContent" name="iframeContent" style="width: 99%; height: 465px;
    border: solid 1px #eee; background-color: #fff;" frameborder="0" src="<%=frameSrc%>"
    onload="SetIFrameHeight();"></iframe>

<script type="text/javascript">
//<![CDATA[
    function SetIFrameHeight() {
        var height;
        try {
            height = iframeHeight;
        }
        catch(err) {
            height = '465px';
        }
        if (height != null) {
            document.getElementById('iframeContent').style.height = height;
        }
    }
//]]>
</script>

