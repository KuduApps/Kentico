<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_ImportExport_Controls_Import_Site_cms_document" CodeFile="cms_document.ascx.cs" %>

<script type="text/javascript">
    //<![CDATA[
    function CheckChange() {
        for (i = 0; i < childIDs.length; i++) {
            var child = document.getElementById(childIDs[i]);
            if (child != null) {
                child.disabled = !parent.checked;
                child.checked = parent.checked;
            }
        }
    }

    function InitCheckboxes() {
        if (!parent.checked) {
            for (i = 0; i < childIDs.length; i++) {
                var child = document.getElementById(childIDs[i]);
                if (child != null) {
                    child.disabled = true;
                }
            }
        }
    }
    //]]>
</script>

<asp:Panel runat="server" ID="pnlCheck" CssClass="WizardHeaderLine" BackColor="transparent">
    <asp:CheckBox ID="chkDocuments" runat="server" /><br />
</asp:Panel>
<asp:Panel runat="server" ID="pnlDocumentData" CssClass="WizardHeaderLine" BackColor="transparent">
    <div>
        <asp:CheckBox ID="chkDocumentsHistory" runat="server" />
    </div>
    <div>
        <asp:CheckBox ID="chkRelationships" runat="server" />
    </div>
    <div>
        <asp:CheckBox ID="chkACLs" runat="server" />
    </div>
    <div>
        <asp:CheckBox ID="chkUserPersonalizations" runat="server" />
    </div>
</asp:Panel>
<asp:Panel runat="server" ID="pnlModules" CssClass="WizardHeaderLine" BackColor="transparent">
    <div>
        <asp:CheckBox ID="chkBlogComments" runat="server" />
    </div>
    <div>
        <asp:CheckBox ID="chkEventAttendees" runat="server" />
    </div>
</asp:Panel>
<asp:Literal ID="ltlScript" EnableViewState="false" runat="Server" />
