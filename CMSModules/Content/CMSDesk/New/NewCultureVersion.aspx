<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_Content_CMSDesk_New_NewCultureVersion" Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master"
    Title="Content - New culture version" CodeFile="NewCultureVersion.aspx.cs" %>

<asp:Content ContentPlaceHolderID="plcContent" runat="server">
    <asp:Label ID="lblInfo" runat="server" CssClass="ContentLabel" EnableViewState="false"
        Font-Bold="true" />
    <asp:Label ID="lblError" runat="server" CssClass="ErrorLabel" EnableViewState="false"
        Visible="false" />
    <br />
    <br />
    <asp:Panel runat="server" ID="pnlNewVersion">
        <table>
            <tr>
                <td>
                    <asp:RadioButton ID="radEmpty" runat="server" GroupName="NewVersion" Checked="true"
                        EnableViewState="false" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:RadioButton ID="radCopy" runat="server" GroupName="NewVersion" EnableViewState="false" />
                </td>
            </tr>
            <tr id="divCultures" style="display: none;">
                <td>
                    <asp:Panel runat="server" ID="pnlCultures" CssClass="SoftSelectionBorder">
                        <asp:ListBox runat="server" ID="lstCultures" DataTextField="DocumentCulture" DataValueField="DocumentID"
                            CssClass="ContentListBoxLow" />
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <cms:CMSButton ID="btnOk" runat="server" CssClass="LongSubmitButton" EnableViewState="false" />
                </td>
            </tr>
        </table>
        <asp:Literal ID="ltlScript" runat="server" EnableViewState="false" />

        <script type="text/javascript">
            //<![CDATA[
            var divCulturesElem = document.getElementById('divCultures');
            var divCulturesStyle = divCulturesElem.style ? divCulturesElem.style : divCulturesElem;

            // Displays the content based on current selection
            function ShowSelection() {
                if (radCopyElem.checked) {
                    divCulturesStyle.display = "block";
                }
                else {
                    divCulturesStyle.display = "none";
                }
            }

            // Initializes the new culture action
            function NewDocument(nodeId) {
                if (radCopyElem.checked) {
                    document.location.replace("../edit/editframeset.aspx?action=newculture&nodeid=" + nodeId + "&sourcedocumentid=" + lstCulturesElem.options[lstCulturesElem.selectedIndex].value);
                }
                else {
                    document.location.replace("../edit/editframeset.aspx?action=newculture&nodeid=" + nodeId);
                }
            }
            //]]>
        </script>

    </asp:Panel>
</asp:Content>
