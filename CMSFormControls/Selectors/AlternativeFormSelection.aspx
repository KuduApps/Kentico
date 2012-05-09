<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="CMSFormControls_Selectors_AlternativeFormSelection" ValidateRequest="false"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/Dialogs/ModalDialogPage.master"
    Title="Alternative form selection" CodeFile="AlternativeFormSelection.aspx.cs" %>

<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:Panel runat="server" ID="pnlContent" CssClass="PageContent">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td style="vertical-align: top; white-space: nowrap;" colspan="2">
                    <cms:LocalizedLabel ID="lblClass" runat="server" ResourceString="general.class" DisplayColon="true" />
                    &nbsp;
                    <asp:DropDownList ID="drpClass" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpClass_SelectedIndexChanged"
                        CssClass="DropDownField" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:ListBox ID="lstAlternativeForms" runat="server" CssClass="DesignerListBox" />
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
<asp:Content ID="plcFooter" ContentPlaceHolderID="plcFooter" runat="server">
    <div class="FloatRight">
        <cms:LocalizedButton ID="btnOk" runat="server" CssClass="SubmitButton" ResourceString="general.ok" />
        <cms:LocalizedButton ID="btnCancel" runat="server" CssClass="SubmitButton" ResourceString="general.cancel" />
        <cms:LocalizedHidden ID="constNoSelection" runat="server" Value="{$altforms_selectaltform.noitemselected$}" />
        <asp:Literal ID="ltlScript" runat="server" />
    </div>

    <script type="text/javascript">
        //<![CDATA[                  
        function SelectCurrentAlternativeForm(txtClientId, lblClientId) {
            if (lstAlternativeForms.selectedIndex != -1) {
                wopener.SelectAltForm(lstAlternativeForms.options[lstAlternativeForms.selectedIndex].value, txtClientId, lblClientId);
                window.close();
            }
            else {
                alert(document.getElementById('constNoSelection').value);
            }
        }

        function Cancel() {
            window.close();
        }
        //]]>
    </script>

</asp:Content>
