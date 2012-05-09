<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSFormControls_System_FieldSelector"
    CodeFile="FieldSelector.ascx.cs" %>
<cms:CMSTextBox ID="txtField" runat="server" MaxLength="200" CssClass="SelectorTextBox" /><cms:localizedbutton
    id="btnSelect" runat="server" cssclass="ContentButton" resourcestring="general.select" /><cms:localizedbutton
        id="btnClear" runat="server" cssclass="ContentButton" resourcestring="general.clear" />
<asp:HiddenField runat="server" ID="hdnField" />
