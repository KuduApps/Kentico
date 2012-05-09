<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSAdminControls_EditingFormControl"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/Dialogs/ModalDialogPage.master"
    Title="Edit field value" CodeFile="EditingFormControl.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/Macros/MacroSelector.ascx" TagPrefix="cc1"
    TagName="MacroSelector" %>
<%@ Register Src="~/CMSAdminControls/UI/Macros/MacroEditor.ascx" TagName="MacroEditor"
    TagPrefix="cms" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">

    <script type="text/javascript">
        //<![CDATA[        

        // Removes new lines at the end of the given text
        function trimNewLines(text) {
            return text.replace(/(\r\n|\n|\r)+$/, '');
        }

        // Removes macro
        function removeMacro(selId, controlPanelId, selPanelId) {
            wopener.setNestedControlValue(selId, controlPanelId, '', selPanelId);
        }
        //]]>
    </script>

    <asp:Panel runat="server" ID="pnlContent" CssClass="PageContent">
        <cms:MacroEditor ID="macroEditor" runat="server" />
        <br />
        <cc1:MacroSelector ID="macroSelector" runat="server" JavaScripFunction="insertMacro"
            ShowExtendedControls="true" TopOffset="35" />
    </asp:Panel>
    <asp:Literal runat="server" ID="ltlScript" EnableViewState="false" />
</asp:Content>
<asp:Content ID="cntFooter" ContentPlaceHolderID="plcFooter" runat="server">
    <div class="FloatLeft">
        <cms:CMSButton ID="btnRemove" runat="server" CssClass="LongSubmitButton" EnableViewState="false" />
    </div>
    <div class="FloatRight">
        <cms:CMSButton ID="btnOk" runat="server" CssClass="SubmitButton" EnableViewState="false" />
        <cms:CMSButton ID="btnCancel" runat="server" CssClass="SubmitButton" OnClientClick="window.close(); return false;"
            EnableViewState="false" />
    </div>
</asp:Content>
