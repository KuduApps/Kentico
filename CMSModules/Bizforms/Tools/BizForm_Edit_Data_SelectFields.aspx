<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_BizForms_Tools_BizForm_Edit_Data_SelectFields" Theme="Default"
    MasterPageFile="~/CMSMasterPages/UI/Dialogs/ModalDialogPage.master" Title="BizForm Edit Data - Select Fields" CodeFile="BizForm_Edit_Data_SelectFields.aspx.cs" %>

<asp:Content ID="cntActions" runat="server" ContentPlaceHolderID="plcActions">
    <asp:LinkButton runat="server" ID="btnSelectAll" CssClass="NewItemLink" EnableViewState="false"
        OnClientClick="SelectAll(); return false;" />
</asp:Content>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <div class="PageContent">
        <asp:CheckBoxList ID="chkListFields" runat="server" />
    </div>

    <script type="text/javascript">

        // Closes modal dialog and refresh parent window
        function CloseAndRefresh() {
            window.close();
            wopener.location.replace(wopener.location);
        }

        // Closes modal dialog
        function Close() {
            window.close();
        }

        // Selects all checkboxes
        function SelectAll() {
            var items = document.forms[0].elements;
            for (i = 0; i < items.length; ++i) {
                if (items[i].type == 'checkbox') {
                    items[i].checked = true;
                }
            }
        }
                        
    </script>

    <asp:Literal ID="ltlScript" runat="server" />
</asp:Content>
<asp:Content ID="cntFooter" runat="server" ContentPlaceHolderID="plcFooter">
    <div class="FloatRight">
        <cms:CMSButton ID="btnOk" runat="server" CssClass="SubmitButton" OnClick="btnOK_Click" /><cms:CMSButton
            ID="btnCancel" runat="server" CssClass="SubmitButton" OnClientClick="Close();" />
    </div>
</asp:Content>
