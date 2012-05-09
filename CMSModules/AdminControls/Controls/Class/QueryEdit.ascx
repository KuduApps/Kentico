<%@ Control Language="C#" AutoEventWireup="true" 
            Inherits="CMSModules_AdminControls_Controls_Class_QueryEdit" CodeFile="QueryEdit.ascx.cs" %>

<%@ Register src="~/CMSAdminControls/UI/PageElements/Help.ascx" TagName="Help" TagPrefix="cms" %>
<%@ Register src="~/CMSAdminControls/UI/Selectors/LoadGenerationSelector.ascx" TagName="LoadGenerationSelector" TagPrefix="cms" %>
<%@ Register src="~/CMSFormControls/Filters/DocTypeFilter.ascx" TagName="DocTypeFilter" TagPrefix="cms" %>

<script type="text/javascript">
    function ToggleHelp() {
        var helpTable = document.getElementById('<%= tblHelp.ClientID %>');
        helpTable.style.display = (helpTable.style.display == 'none') ? 'table' : 'none';        
    }
</script>

<asp:PlaceHolder runat="server" ID="plcDocTypeFilter">
    <asp:Panel runat="server" ID="pnlDocTypeFilter" CssClass="PageHeaderLine">
        <cms:DocTypeFilter runat="server" ID="filter" RenderTableTag="true" EnableViewState="true" />
    </asp:Panel>
</asp:PlaceHolder>

<asp:Panel ID="pnlContainer" runat="server">

    <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false" Visible="false" />
    <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false" Visible="false" />

    <table style="width: 98%">
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel runat="server" ID="lblQueryName" EnableViewState="false" ResourceString="queryedit.queryname"
                    DisplayColon="true" />
            </td>
            <td>
                <table width="99%">
                    <tr>
                        <td>
                            <cms:CMSTextBox runat="server" ID="txtQueryName" CssClass="TextBoxField" MaxLength="100"
                                EnableViewState="true" />
                            <cms:CMSRequiredFieldValidator ID="RequiredFieldValidatorQueryName" runat="server" EnableViewState="false"
                                ControlToValidate="txtQueryName" Display="dynamic" />
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel runat="server" ID="lblQueryType" EnableViewState="false" ResourceString="queryedit.querytype"
                    DisplayColon="true" />
            </td>
            <td>
                <cms:LocalizedRadioButtonList runat="server" ID="rblQueryType" RepeatDirection="Horizontal"
                    UseResourceStrings="true" EnableViewState="false">
                    <asp:ListItem Selected="True" Value="SQLQuery" Text="queryedit.querytypetext" />
                    <asp:ListItem Value="StoredProcedure" Text="queryedit.querytypesp" />
                </cms:LocalizedRadioButtonList>
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel runat="server" ID="lblTransaction" EnableViewState="false" ResourceString="queryedit.requirestransaction"
                    DisplayColon="true" />
            </td>
            <td>
                <asp:CheckBox runat="server" ID="chbTransaction" EnableViewState="false" />
            </td>
        </tr>
        <asp:PlaceHolder runat="server" ID="plcIsCustom" EnableViewState="false" Visible="false">
            <tr>
                <td class="FieldLabel">
                    <cms:LocalizedLabel runat="server" ID="lblIsCustom" EnableViewState="false" ResourceString="queryedit.iscustom"
                        DisplayColon="true" />
                </td>
                <td>
                    <asp:CheckBox ID="chckIsCustom" runat="server" Checked="true" />
                </td>
            </tr>
        </asp:PlaceHolder>
        <tr>
            <td style="vertical-align: text-top; padding-top: 5px" class="FieldLabel">
                <cms:LocalizedLabel runat="server" ID="lblQueryText" EnableViewState="false" ResourceString="queryedit.querytextvalue"
                    DisplayColon="true" />
            </td>
            <td style="width: 90%">
                <cms:LocalizedButton ID="btnGenerate" runat="server" CssClass="XLongButton" OnClick="btnGenerate_Click"
                    ResourceString="queryedit.generate" EnableViewState="false" Visible="false" />
                <cms:ExtendedTextArea runat="server" ID="txtQueryText" EnableViewState="false" EditorMode="Advanced"
                    Language="SQL" Height="280px" Width="100%" />
            </td>
        </tr>
        <asp:PlaceHolder runat="server" ID="plcLoadGeneration" EnableViewState="false">
            <tr>
                <td class="FieldLabel">
                    <cms:LocalizedLabel runat="server" ID="lblLoadGeneration" EnableViewState="false"
                        ResourceString="LoadGeneration.Title" DisplayColon="true" />
                </td>
                <td>
                    <cms:LoadGenerationSelector ID="drpGeneration" runat="server" />
                </td>
            </tr>
        </asp:PlaceHolder>
        <tr>
            <td colspan="2">
                <br />
                <cms:LocalizedLinkButton ID="lnkHelp" runat="server" OnClientClick="ToggleHelp(); return false;"
                    ResourceString="queryedit.helpheader" EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Table ID="tblHelp" runat="server" EnableViewState="false" GridLines="Horizontal"
                    CellPadding="3" CellSpacing="0" CssClass="UniGridGrid" Width="100%">
                </asp:Table>
            </td>
        </tr>
    </table>
</asp:Panel>
