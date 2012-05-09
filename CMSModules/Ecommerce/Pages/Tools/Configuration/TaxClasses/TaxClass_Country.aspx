<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Ecommerce_Pages_Tools_Configuration_TaxClasses_TaxClass_Country"
    Theme="Default" EnableEventValidation="false" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master"
    CodeFile="TaxClass_Country.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plcContent" runat="Server">
    <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
        Visible="false" />
    <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
        Visible="false" />
    <asp:GridView ID="GridViewCountries" runat="server" AutoGenerateColumns="false" OnDataBound="GridViewCountries_DataBound"
        CellPadding="3" CssClass="UniGridGrid" EnableViewState="false">
        <HeaderStyle HorizontalAlign="Left" CssClass="UniGridHead" Wrap="false" />
        <AlternatingRowStyle CssClass="OddRow" />
        <RowStyle CssClass="EvenRow" />
        <Columns>
            <asp:TemplateField>
                <ItemStyle Wrap="false" />
                <HeaderStyle Wrap="false" />
                <ItemTemplate>
                    <asp:Label ID="lblName" runat="server" Text='<%# HTMLHelper.HTMLEncode(CMSContext.ResolveMacros(Eval("CountryDisplayName").ToString())) %>' />
                </ItemTemplate>
            </asp:TemplateField>
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
            <asp:BoundField DataField="CountryID">
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
