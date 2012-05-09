<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_SmartSearch_Controls_UI_SearchIndex_StopWordsCustomAnalyzer" CodeFile="SearchIndex_StopWordsCustomAnalyzer.ascx.cs" %>
<tr runat="server" id="stopWordsRow">
    <td class="FieldLabel">
        <cms:LocalizedLabel runat="server" ID="lblStopWords" EnableViewState="false" ResourceString="srch.index.stopwords"
            DisplayColon="true" />
    </td>
    <td>
        <asp:DropDownList ID="drpStopWords" runat="server" CssClass="DropDownFieldSmall">
        </asp:DropDownList>
    </td>
</tr>
<tr runat="server" id="customAnalyzerAssemblyName">
    <td class="FieldLabel">
        <cms:LocalizedLabel runat="server" ID="lblCustomAnalyzerAssemblyName" EnableViewState="false"
            ResourceString="srch.index.customanalyzerassembly" DisplayColon="true" />
    </td>
    <td>
        <cms:CMSTextBox ID="txtCustomAnalyzerAssemblyName" runat="server" CssClass="TextBoxField" MaxLength="200" />
    </td>
</tr>
<tr runat="server" id="customAnalyzerClassName">
    <td class="FieldLabel">
        <cms:LocalizedLabel runat="server" ID="lblCustomClassName" EnableViewState="false"
            ResourceString="srch.index.customanalyzeraclassname" DisplayColon="true" />
    </td>
    <td>
        <cms:CMSTextBox ID="txtCustomAnalyzerClassName" runat="server" CssClass="TextBoxField" MaxLength="200" />
    </td>
</tr>
