<%@ Control Language="C#" AutoEventWireup="true" CodeFile="cms_user.ascx.cs" Inherits="CMSModules_ImportExport_Controls_Import_cms_user" %>

<script type="text/javascript">
    //<![CDATA[
    function CheckChange() {
        if (dashboardChck2 != null) {
            dashboardChck2.disabled = !dashboardChck1.checked;
            dashboardChck2.checked = false;
        }
    }

    function InitCheckboxes() {
        if (!dashboardChck1.checked && (dashboardChck2 != null)) {
            dashboardChck2.disabled = true;
        }
    }
    //]]>
</script>
<asp:Panel runat="server" ID="pnlCheck" CssClass="WizardHeaderLine" BackColor="transparent">
    <div>
        <asp:CheckBox ID="chkObject" runat="server" />
    </div>
    <div>
        <asp:CheckBox ID="chkSiteObjects" runat="server" />
    </div>
</asp:Panel>
<asp:Literal ID="ltlScript" EnableViewState="false" runat="Server" />