<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_ImportExport_Controls_Import___objects__"
    CodeFile="__objects__.ascx.cs" %>

<script type="text/javascript">
    //<![CDATA[
    function CheckChange() {
        for (i = 0; i < im_g_childIDs.length; i++) {
            var child = document.getElementById(im_g_childIDs[i]);
            if (child != null) {
                var name = im_g_childIDNames[i];
                child.disabled = !im_g_parent.checked;
                if (name == 'asbl') {
                    child.checked = false;
                }
                else {
                    child.checked = im_g_parent.checked;
                }
            }
        }
    }

    function InitCheckboxes() {
        if (!im_g_parent.checked) {
            for (i = 0; i < im_g_childIDs.length; i++) {
                var child = document.getElementById(im_g_childIDs[i]);
                if (child != null) {
                    child.disabled = true;
                }
            }
        }
    }
    //]]>
</script>

<asp:Panel runat="server" ID="pnlWarning" CssClass="WizardHeaderLine" BackColor="transparent"
    Visible="false">
    <span style="color: #ff0000;">
        <asp:Label ID="lblWarning" runat="server" EnableViewState="false" /></span>
</asp:Panel>
<asp:Panel runat="server" ID="pnlInfo" CssClass="WizardHeaderLine" BackColor="transparent">
    <asp:Label ID="lblInfo2" runat="server" EnableViewState="false" /><br />
    <br />
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
                <asp:LinkButton ID="lnkSelectNew" runat="server" OnClick="lnkSelectNew_Click" />
            </td>
            <td style="text-align: center;">
                <asp:LinkButton ID="lnkSelectNone" runat="server" OnClick="lnkSelectNone_Click" />
            </td>
        </tr>
    </table>
    <br />
</asp:Panel>
<asp:Panel runat="server" ID="pnlCheck" CssClass="WizardHeaderLine" BackColor="transparent">
    <strong>
        <asp:Label ID="lblSettings" runat="server" EnableViewState="false" /></strong><br />
    <br />
    <asp:PlaceHolder runat="server" ID="plcSite" Visible="false">
        <asp:PlaceHolder ID="plcExistingSite" runat="Server" Visible="false">
            <div>
                <asp:CheckBox ID="chkUpdateSite" runat="server" />
            </div>
        </asp:PlaceHolder>
        <div>
            <asp:CheckBox ID="chkBindings" runat="server" />
        </div>
        <div>
            <asp:CheckBox ID="chkRunSite" runat="server" />
        </div>
        <div>
            <asp:CheckBox ID="chkDeleteSite" runat="server" />
        </div>
    </asp:PlaceHolder>
    <asp:PlaceHolder runat="server" ID="plcOverwriteQueries" Visible="false">
        <div>
            <asp:CheckBox ID="chkOverwriteSystemQueries" runat="server" />
        </div>
    </asp:PlaceHolder>
    <div>
        <asp:CheckBox ID="chkSkipOrfans" runat="server" />
    </div>
    <div>
        <asp:CheckBox ID="chkImportTasks" runat="server" />
    </div>
    <br />
    <div>
        <asp:CheckBox ID="chkCopyFiles" runat="server" />
    </div>
    <div>
        <asp:CheckBox ID="chkCopyGlobalFiles" runat="server" />
    </div>
    <div>
        <asp:CheckBox ID="chkCopyAssemblies" runat="server" />
    </div>
    <asp:PlaceHolder ID="plcSiteFiles" runat="server" Visible="false">
        <div>
            <asp:CheckBox ID="chkCopySiteFiles" runat="server" />
        </div>
    </asp:PlaceHolder>
    <br />
    <div>
        <asp:CheckBox ID="chkLogSync" runat="server" />
    </div>
    <div>
        <asp:CheckBox ID="chkLogInt" runat="server" />
    </div>
    <br />
</asp:Panel>
<asp:Literal ID="ltlScript" EnableViewState="false" runat="Server" />
