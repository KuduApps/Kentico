<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_Newsletters_Tools_Newsletters_Newsletter_ContentEditorHeader"
    CodeFile="Newsletter_ContentEditorHeader.ascx.cs" %>
<table width="99%" class="NewsletterHeaderTable">
    <tr>
        <td>
            <div id="CKEditorToolbar">
            </div>
        </td>
    </tr>
</table>

<script type="text/javascript">
    //<![CDATA[
    var toolbarElem = document.getElementById('CKEditorToolbar');

    function ClearToolbar() {
        toolbarElem.innerHTML = '';
        toolbarElem.style.height = '';
    }

    function RememberFocusedRegion() {
        if ((window.frames['iframeContent'] != null) && (window.frames['iframeContent'].RememberFocusedRegion != null)) {
            window.frames['iframeContent'].RememberFocusedRegion();
        }
    }
    //]]>
</script>

