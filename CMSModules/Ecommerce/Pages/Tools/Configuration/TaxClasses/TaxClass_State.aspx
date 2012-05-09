<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Ecommerce_Pages_Tools_Configuration_TaxClasses_TaxClass_State"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" CodeFile="TaxClass_State.aspx.cs" %>

<asp:Content ID="cntControls" runat="server" ContentPlaceHolderID="plcSiteSelector">
    <table>
        <tr>
            <td>
                <cms:LocalizedLabel ID="lblCountry" runat="server" EnableViewState="false" ResourceString="taxclass_state.lblcountry"
                    DisplayColon="true"></cms:LocalizedLabel>
            </td>
            <td>
                <cms:LocalizedDropDownList ID="drpCountry" CssClass="DropDownField" runat="server"
                    AutoPostBack="true" OnSelectedIndexChanged="drpCountry_SelectedIndexChanged"
                    EnableViewState="true" />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="plcContent" runat="Server">
    <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
        Visible="false" />
    <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
        Visible="false" />
    <asp:GridView ID="gvStates" runat="server" OnDataBound="gvStates_DataBound" CellPadding="3"
        CssClass="UniGridGrid" AutoGenerateColumns="false" EnableViewState="false">
        <HeaderStyle HorizontalAlign="Left" CssClass="UniGridHead" Wrap="false" />
        <AlternatingRowStyle CssClass="OddRow" />
        <RowStyle CssClass="EvenRow" />
        <Columns>
            <asp:TemplateField>
                <ItemStyle Wrap="false" />
                <HeaderStyle Wrap="false" />
                <ItemTemplate>
                    <asp:Label ID="lblOrderStatus" runat="server" Text='<%# HTMLHelper.HTMLEncode(CMSContext.ResolveMacros(Eval("StateDisplayName").ToString())) %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="StateCode">
                <ItemStyle Wrap="false" />
                <HeaderStyle Wrap="false" />
            </asp:BoundField>
            <asp:TemplateField>
                <HeaderStyle Width="200px" />
                <ItemStyle Wrap="false" />
                <ItemTemplate>
                    <cms:CMSTextBox ID="txtTaxValue" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TaxValue") %>'
                        MaxLength="10" CssClass="ShortTextBox" OnTextChanged="txtTaxValue_Changed" EnableViewState="false"></cms:CMSTextBox >
                    <asp:Label ID="lblCurrency" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                <ItemStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:CheckBox ID="chkIsFlatValue" runat="server" Checked='<%# ConvertToBoolean(DataBinder.Eval(Container.DataItem, "IsFlatValue")) %>'
                        OnCheckedChanged="chkIsFlatValue_Changed" EnableViewState="false" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="StateID">
                <ItemStyle />
            </asp:BoundField>
            <asp:TemplateField>
                <HeaderStyle Width="100%" />
                <ItemTemplate>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
