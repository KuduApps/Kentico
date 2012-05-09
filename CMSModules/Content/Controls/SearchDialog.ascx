<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_Content_Controls_SearchDialog" CodeFile="SearchDialog.ascx.cs" %>
<%@ Register Src="~/CMSFormControls/Cultures/SiteCultureSelector.ascx" TagName="SiteCultureSelector"
    TagPrefix="cms" %>
<asp:Panel ID="pnlSearch" runat="server" DefaultButton="btnSearch">
    <div class="FilterItem">
        <div class="FilterItemTitle">
            <cms:LocalizedLabel ID="lblIndexes" runat="server" ResourceString="Search.lblSiteIndexes"
                EnableViewState="false" DisplayColon="true" /></div>
        <cms:LocalizedDropDownList runat="server" ID="drpIndexes" CssClass="DropDownField" />
    </div>
    <br />
    <div class="FilterItem">
        <div class="FilterItemTitle">
            <cms:LocalizedLabel runat="server" ID="lblSearchFor" AssociatedControlID="txtSearchFor"
                CssClass="FieldLabel" EnableViewState="false" ResourceString="search.lblsearch" /></div>
        <cms:CMSTextBox runat="server" ID="txtSearchFor" CssClass="TextBoxField" MaxLength="1000" />
        <cms:CMSRequiredFieldValidator ID="rfvText" runat="server" ControlToValidate="txtSearchFor"
            EnableViewState="false" Display="Dynamic" />
    </div>
    <div class="FilterItem">
        <div class="FilterItemTitle">
            <cms:LocalizedLabel ID="lblSearchMode" runat="server" ResourceString="srch.dialog.mode"
                DisplayColon="true" EnableViewState="false" /></div>
        <cms:LocalizedDropDownList ID="drpSearchMode" runat="server" CssClass="DropDownField" />
    </div>    
    <asp:PlaceHolder ID="plcLang" runat="server">
        <div class="FilterItem">
            <div class="FilterItemTitle">
                <cms:LocalizedLabel ID="lblLanguage" runat="server" ResourceString="general.language"
                    EnableViewState="false" DisplayColon="true" /></div>
            <asp:DropDownList ID="drpLanguage" runat="server" CssClass="ContentDropdown" Width="143px" />
            <cms:SiteCultureSelector runat="server" ID="cultureElem" />
        </div>
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="plcPublished" runat="server">
        <div class="FilterItem">
            <div class="FilterItemTitle">
                &nbsp;</div>
            <cms:LocalizedCheckBox ID="chkOnlyPublished" runat="server" ResourceString="filter.onlypublished" />
        </div>
    </asp:PlaceHolder>
    <div class="FilterItem">
        <div class="FilterItemTitle">
            &nbsp;</div>
        <cms:LocalizedButton runat="server" ID="btnSearch" CssClass="ContentButton" ResourceString="general.search" />
    </div>
</asp:Panel>
