<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_Newsletters_Tools_Newsletters_Newsletter_Issue_New_Preview"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/Dialogs/ModalDialogPage.master"
    Title="Newsletter - New issue" CodeFile="Newsletter_Issue_New_Preview.aspx.cs" %>

<%@ Register Src="Newsletter_Preview.ascx" TagPrefix="cms"
    TagName="Newsletter_Preview"  %>
<%@ Register Src="~/CMSAdminControls/Wizard/Header.ascx" TagPrefix="cms" 
    TagName="WizardHeader" %>

<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">

    <script type="text/javascript">
        //<![CDATA[
        function RefreshPage() {
            wopener.RefreshPage();
        }
        //]]>
    </script>

    <asp:Literal ID="ltlScript" runat="server" />
    <div style="padding: 10px;">
        <table class="GlobalWizard NewsletterWizard" cellspacing="0">
            <tr class="Top">
                <td class="Left">
                    &nbsp;
                </td>
                <td class="Center">
                    <cms:WizardHeader ID="ucHeader" runat="server" />
                </td>
                <td class="Right">
                    &nbsp;
                </td>
            </tr>
            <tr class="Middle">
                <td class="Center" colspan="3">
                    <div id="wzdBody">
                        <table class="Wizard" cellspacing="0" cellpadding="0" border="0" style="border-collapse: collapse;">
                            <tbody>
                                <tr style="height: 100%;">
                                    <td>
                                        <div class="NewsletterWizardStep">
                                            <table style="width: 98%" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <cms:Newsletter_Preview ID="Newsletter_Preview1" runat="server" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="ButtonRow">
                                        <div id="buttonsDiv">
                                            <cms:CMSButton ID="btnBack" runat="server" CssClass="SubmitButton" OnClick="btnBack_Click" />
                                            <cms:CMSButton ID="btnNext" runat="server" CssClass="SubmitButton" OnClick="btnNext_Click" />
                                        </div>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </td>
            </tr>
            <tr class="Bottom">
                <td class="Left">
                    &nbsp;
                </td>
                <td class="Center">
                    &nbsp;
                </td>
                <td class="Right">
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="cntFooter" runat="server" ContentPlaceHolderID="plcFooter">
    <div class="FloatRight">
        <cms:CMSButton ID="btnClose" runat="server" CssClass="SubmitButton" OnClientClick="window.close();RefreshPage();return false;" />
    </div>
</asp:Content>
