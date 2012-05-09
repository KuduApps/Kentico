<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_ImportExport_Controls_Import_Site_cms_form"
    CodeFile="cms_form.ascx.cs" %>

<script type="text/javascript">
    //<![CDATA[
    function bizCheckChange() {
        bizChck2.disabled = !bizChck1.checked;
        bizChck2.checked = false;
    }

    function bizInitCheckboxes() {
        if (!bizChck1.checked) {
            bizChck2.disabled = true;
        }
    }
    //]]>
</script>

<asp:Panel runat="server" ID="pnlCheck" CssClass="WizardHeaderLine" BackColor="transparent">
    <div>
        <asp:CheckBox ID="chkObject" runat="server" Visible="false" />
    </div>
    <div>
        <asp:CheckBox ID="chkPhysicalFiles" runat="server" />
    </div>
    <br />
    <asp:Label ID="lblInfo" runat="server" />
</asp:Panel>
<asp:Literal ID="ltlScript" EnableViewState="false" runat="Server" />