<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_ImportExport_Controls_Export___objects__"
    CodeFile="__objects__.ascx.cs" %>

<script type="text/javascript">
    //<![CDATA[
    function CheckChange() {
        for (i = 0; i < ex_g_childIDs.length; i++) {
            var child = document.getElementById(ex_g_childIDs[i]);
            if (child != null) {
                child.disabled = !ex_g_parent.checked;
                child.checked = ex_g_parent.checked;
            }
        }
    }

    function InitCheckboxes() {
        if (!ex_g_parent.checked) {
            for (i = 0; i < ex_g_childIDs.length; i++) {
                var child = document.getElementById(ex_g_childIDs[i]);
                if (child != null) {
                    child.disabled = true;
                }
            }
        }
    }
    //]]>
</script>

<asp:Panel runat="server" ID="pnlInfo" CssClass="WizardHeaderLine" BackColor="transparent">
    <asp:Label ID="lblInfo" runat="server" EnableViewState="false" />
</asp:Panel>
<asp:Panel runat="server" ID="pnlSelection" CssClass="WizardHeaderLine" BackColor="transparent">
    <strong>
        <asp:Label ID="lblSelection" runat="server" EnableViewState="false" /></strong><br />
    <table width="100%">
        <tr>
            <td style="text-align: center;">
                <asp:LinkButton ID="lnkSelectDefault" runat="server" OnClick="lnkSelectDefault_Click" />
            </td>
            <td style="text-align: center;">
                <asp:LinkButton ID="lnkSelectAll" runat="server" OnClick="lnkSelectAll_Click" />
            </td>
            <td style="text-align: center;">
                <asp:LinkButton ID="lnkSelectNone" runat="server" OnClick="lnkSelectNone_Click" />
            </td>
        </tr>
    </table>
</asp:Panel>
<asp:Panel runat="server" ID="pnlCheck" CssClass="WizardHeaderLine" BackColor="transparent"
    EnableViewState="false">
    <asp:Label ID="lblSettings" runat="server" EnableViewState="false" Font-Bold="true" /><br />
    <br />
    <div style="padding-left: 20px;">
        <table>
            <tr>
                <td>
                    <asp:CheckBox ID="chkExportTasks" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <asp:CheckBox ID="chkCopyFiles" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:CheckBox ID="chkCopyGlobalFiles" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:CheckBox ID="chkCopyAssemblies" runat="server" />
                </td>
            </tr>
            <asp:PlaceHolder ID="plcSiteFiles" runat="server">
                <tr>
                    <td>
                        <asp:CheckBox ID="chkCopySiteFiles" runat="server" />
                    </td>
                </tr>
            </asp:PlaceHolder>
            <tr>
                <td>
                    <asp:CheckBox ID="chkCopyASPXTemplatesFolder" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:CheckBox ID="chkCopyForumCustomLayoutsFolder" runat="server" />
                </td>
            </tr>
        </table>
    </div>
</asp:Panel>
<asp:Literal ID="ltlScript" EnableViewState="false" runat="Server" />
