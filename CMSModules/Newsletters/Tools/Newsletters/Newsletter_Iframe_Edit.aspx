<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Newsletters_Tools_Newsletters_Newsletter_Iframe_Edit"
    Theme="Default" ValidateRequest="false" CodeFile="Newsletter_Iframe_Edit.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>Edit issue</title>
</head>
<body>
    <div id="CMSHeaderDiv">
        <div id="CKEditorToolbar">
        </div>
    </div>

    <script type="text/javascript">
        //<![CDATA[
        var toolbarElem = document.getElementById('CKEditorToolbar');

        function ClearToolbar() {
            toolbarElem.innerHTML = '';
            toolbarElem.style.height = '';
        }
        
        // Set id of curently focused region to focusedRegionID (declared in LoadRegionList() in code behind)
        function RememberFocusedRegion() {
            for (i = 0; i < regions.length; i++) //regions array is declared in LoadRegionList() in code behind
            {
                if (window.CKEDITOR != null) {
                    var oEditor = window.CKEDITOR.instances[regions[i]];
                    if ((oEditor != null) && (oEditor.focusManager.hasFocus)) {
                        focusedRegionID = regions[i];
                        break;
                    }
                }
            }
        }

        // Insert desired HTML at the current cursor position of the CK editor
        function InsertHTML(htmlString) {
            if (focusedRegionID == '') //focusedRegionID is declared in LoadRegionList() in code behind
            {
                RememberFocusedRegion();
            }

            if (focusedRegionID != '') //focusedRegionID is declared in LoadRegionList() in code behind
            {
                if (window.CKEDITOR != null) {
                    // Get the editor instance that we want to interact with.
                    var oEditor = window.CKEDITOR.instances[focusedRegionID];
                    if (oEditor != null) {
                        // Check the active editing mode.
                        if (oEditor.mode == 'wysiwyg') {
                            // Insert the desired HTML.
                            oEditor.focus();
                            oEditor.insertHtml(htmlString);
                        }
                        else {
                            alert('You must be on WYSIWYG mode!');
                        }
                    }
                }
            }
            return false;
        }

        //]]>
    </script>

    <form id="form1" runat="server">
    <asp:Label runat="server" ID="lblInfo" EnableViewState="false" Visible="false" />
    <asp:Literal ID="ltlScript" runat="server" EnableViewState="false" />
    <asp:PlaceHolder ID="plcContent" runat="server" />
    <cms:CMSButton runat="server" ID="btnHidden" EnableViewState="false" CssClass="HiddenButton"
        OnClick="btnHidden_Click" />
    <asp:HiddenField runat="server" ID="hdnIssueId" />
    <asp:HiddenField runat="server" ID="hdnNext" />
    <asp:HiddenField ID="hdnNewsletterSubject" runat="server" />
    <asp:HiddenField ID="hdnNewsletterShowInArchive" runat="server" />
    <asp:Literal runat="server" ID="ltlScript2" EnableViewState="false" />
    <div id="CMSFooterDiv"><div id="CKFooter"></div></div>
    </form>
</body>
</html>
