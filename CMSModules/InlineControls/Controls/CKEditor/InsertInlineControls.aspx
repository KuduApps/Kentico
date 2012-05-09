<%@ Page Language="C#" AutoEventWireup="true" Theme="Default" CodeFile="InsertInlineControls.aspx.cs"
    Inherits="CMSModules_InlineControls_Controls_CKEditor_InsertInlineControls"
    MasterPageFile="~/CMSMasterPages/UI/EmptyPage.master" %>

<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <style>
        body
        {
            background-color: Transparent !important;
        }
    </style>

    <script type="text/javascript" language="javascript">
        //<![CDATA[
        function InsertUserControl(charValue) {
            var oDialog = window.parent.CMSPlugin.currentDialog;
            if (oDialog) {
                oDialog._.editor.insertHtml(charValue || "");
            }
            oDialog.hide();
        }
        //]]>
    </script>

    <div class="LiveSiteDialog">
        <table border="0" cellpadding="0" cellspacing="4" width="100%">
            <tr>
                <td colspan="2">
                    <asp:Label ID="lblAvailableControls" runat="server" EnableViewState="false" /><br />
                </td>
            </tr>
            <tr>
                <td style="vertical-align: top;">
                    <asp:ListBox ID="lstControls" runat="server" Height="230" Width="200" DataTextField="ControlDisplayName"
                        DataValueField="ControlName" AutoPostBack="true" OnSelectedIndexChanged="lstControls_SelectedIndexChanged" />
                </td>
                <td class="PropertiesContent">
                    <asp:Label ID="lblControlName" runat="server" CssClass="PropertiesControlName" EnableViewState="false" />
                    <asp:Label ID="lblControlDescription" runat="server" CssClass="PropertiesControlDescription"
                        EnableViewState="false" />
                </td>
            </tr>
            <tr>
                <td>
                    <cms:CMSButton ID="btnInsert" runat="server" CssClass="SubmitButton" OnClick="btnOK_Click" />
                </td>
                <td>
                    <asp:Label ID="lblControlParametrName" runat="server" EnableViewState="false" />
                    <cms:CMSTextBox ID="txtControlParametrName" runat="server" MaxLength="200" CssClass="SmallTextBox"
                        EnableViewState="false" />
                </td>
            </tr>
        </table>
    </div>
    <asp:Literal ID="ltlScript" runat="server" />
</asp:Content>
