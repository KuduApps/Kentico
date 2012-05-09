<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSFormControls_System_SelectColumns" CodeFile="SelectColumns.ascx.cs" %>
<cms:CMSTextBox ID="txtColumns" runat="server" ReadOnly="True" CssClass="SelectorTextBox" /><cms:CMSButton 
    ID="btnDesign" runat="server" Text="Select" CssClass="ContentButton" />
<asp:HiddenField ID="hdnSelectedColumns" runat="server" />
<asp:HiddenField ID="hdnProperties" runat="server" />
<asp:Literal ID="ltlScript" runat="server" />
<asp:Literal ID="ltlClass" runat="server" />
<asp:Literal ID="ltlGetColumns" runat="server" />
<asp:Literal ID="ltlMyModal" runat="server" />
