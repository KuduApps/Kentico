<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSWebParts_SmartSearch_SearchFilter" CodeFile="~/CMSWebParts/SmartSearch/SearchFilter.ascx.cs" %>
<cms:LocalizedLabel runat="server" ID="lblError" Visible="false" CssClass="ErrorLabel"></cms:LocalizedLabel>

<cms:LocalizedLabel ID="lblFilter" runat="server" Display="false" EnableViewState="false" ResourceString="srch.filter" />
<asp:DropDownList runat="server" ID="drpFilter" Visible="false"  CssClass="DropDownField"></asp:DropDownList>
<asp:RadioButtonList runat=server ID="radlstFilter" Visible="false" CssClass="ContentCheckBoxList" ></asp:RadioButtonList>
<asp:CheckBoxList runat="server" ID="chklstFilter" Visible="false" CssClass="ContentRadioButtonList" ></asp:CheckBoxList>
