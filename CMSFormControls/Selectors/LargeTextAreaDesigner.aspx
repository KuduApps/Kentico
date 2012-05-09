<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LargeTextAreaDesigner.aspx.cs"
    Inherits="CMSFormControls_Selectors_LargeTextAreaDesigner" Theme="Default" MasterPageFile="~/CMSMasterPages/UI/Dialogs/ModalDialogPage.master"
    Title="Edit text" %>

<%@ Register Src="~/CMSAdminControls/UI/Macros/MacroEditor.ascx" TagName="MacroEditor"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/UI/Macros/MacroSelector.ascx" TagPrefix="cms"
    TagName="MacroSelector" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:Panel runat="server" ID="pnlContent" CssClass="PageContent">
        <cms:MacroEditor ID="txtText" Height="470px" Width="99%" runat="server" />

        <script type="text/javascript">
            //<![CDATA[
            // Retrieves the text from the form control
            function load() {
                var elem = wopener.document.getElementById('<%= ScriptHelper.GetString(QueryHelper.GetString("editorid", string.Empty),false) %>');
                if (wopener && elem) {
                    var txtArea = document.getElementById('<%= txtText.Editor.ClientID %>');
                    var openerValue = elem.value;
                    if (txtArea) {
                        txtArea.value = openerValue;
                    }
                }
                return false;
            }
            
            // Returns the text to the form control
            function set() {
                var elem = wopener.document.getElementById('<%= ScriptHelper.GetString(QueryHelper.GetString("editorid", string.Empty),false) %>');
                if (wopener && elem) {
                    if ((typeof(<%= txtText.Editor.EditorID %>) != 'undefined') && (<%= txtText.Editor.EditorID %> != null))
                    {   elem.value = <%= txtText.Editor.EditorID %> .getValue();
                    }
                    else
                    {
                        elem.value = document.getElementById('<%= txtText.Editor.ClientID %>').value;
                    }
                    if(elem.onchange)
                    {
                        elem.onchange();
                    }
                }
                window.close();
            }

            load();
            //]]>
        </script>

        <br />
        <br />
        <cms:MacroSelector ID="macroSelectorElem" runat="server" ShowExtendedControls="true"
            ShowMacroTreeAbove="true" IsLiveSite="false" />
    </asp:Panel>
</asp:Content>
<asp:Content ID="cntFooter" ContentPlaceHolderID="plcFooter" runat="server">
    <div class="FloatRight">
        <cms:LocalizedButton ID="btnOk" runat="server" CssClass="SubmitButton" ResourceString="general.ok"
            OnClientClick="set(); return false;" />
        <cms:LocalizedButton ID="btnCancel" runat="server" CssClass="SubmitButton" ResourceString="general.cancel"
            OnClientClick="window.close(); return false;" />
    </div>
</asp:Content>
