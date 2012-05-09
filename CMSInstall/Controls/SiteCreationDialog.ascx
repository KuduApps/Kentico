<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSInstall_Controls_SiteCreationDialog" CodeFile="SiteCreationDialog.ascx.cs" %>
<asp:HiddenField ID="hdnName" runat="server" />
<asp:HiddenField ID="hdnLastSelected" runat="server" />
<asp:Literal ID="ltlScript" EnableViewState="false" runat="server" />

<script type="text/javascript">
    //<![CDATA[      
    function SelectTemplate(id, name) {
        if (id != '') {
            if (hdnLastSelected.value != '') {
                var lastElem = document.getElementById(hdnLastSelected.value);
                if (lastElem != null) {
                    lastElem.className = 'InstallItem';
                }
            }

            var elem = document.getElementById(id);
            if (elem != null) {
                elem.className = 'InstallSelectedItem';
            }
            hdnLastSelected.value = id;
            hdnField.value = name;
        }
    }
    //]]>
</script>

<asp:PlaceHolder ID="plcInfo" runat="Server" Visible="false">
    <asp:Label ID="lblInfo" runat="server" CssClass="ContentLabel" /><br />
    <br />
</asp:PlaceHolder>
<table class="InstallWizardNewSite" border="0" cellpadding="0" cellspacing="0" width="100%">
    <tr style="vertical-align: top;">
        <td colspan="2">
            <asp:RadioButton ID="radTemplate" runat="server" Checked="true" GroupName="SiteCreation" />
        </td>
    </tr>
    <asp:PlaceHolder ID="plcTemplates" runat="server">
        <tr>
            <td colspan="2" style="padding: 2px 0 0 23px;">
                <div style="overflow: auto; height: 228px; border: 1px solid #cccccc; background: white;
                    margin-bottom: 5px;">
                    <div style="margin: 5px 0px 5px 0px;">
                        <asp:Repeater ID="rptSites" runat="server">
                            <ItemTemplate>
                                <%# GetItemHTML(Container.DataItem) %>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </td>
        </tr>
    </asp:PlaceHolder>
    <tr style="vertical-align: top;">
        <td colspan="2">
            <asp:RadioButton ID="radWizard" runat="server" GroupName="SiteCreation" />
        </td>
    </tr>
    <tr style="vertical-align: top;">
        <td colspan="2">
            <asp:RadioButton ID="radExisting" runat="server" GroupName="SiteCreation" />
        </td>
    </tr>
</table>
<asp:Literal ID="ltlScriptAfter" EnableViewState="false" runat="server" />
