<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_ImportExport_Controls_Import_Site_media_library"
    CodeFile="media_library.ascx.cs" %>

<script type="text/javascript">
    //<![CDATA[
    function medCheckChange() {
        medChck2.disabled = !medChck1.checked;
        medChck2.checked = false;
    }

    function medInitCheckboxes() {
        if (!medChck1.checked) {
            medChck2.disabled = true;
        }
    }
    //]]>
</script>

<asp:Panel runat="server" ID="pnlCheck" CssClass="WizardHeaderLine" BackColor="transparent">
    <div>
        <asp:CheckBox ID="chkFiles" runat="server" />
    </div>
    <div>
        <asp:CheckBox ID="chkPhysicalFiles" runat="server" />
    </div>
</asp:Panel>
<asp:Literal ID="ltlScript" EnableViewState="false" runat="Server" />