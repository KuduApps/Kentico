<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSFormControls_Selectors_FontSelector" CodeFile="~/CMSFormControls/Selectors/FontSelector.ascx.cs" %>

<cms:CMSUpdatePanel RenderMode= "Block"  ID="pnlFontSelector" runat="server">     
     <ContentTemplate>           
        <cms:CMSTextBox runat="server" CssClass="TextBoxField" ID="txtFontType" readonly="true"  />                  
        <asp:Button runat ="server" CssClass="ContentButton" ID="btnChangeFontType" />
        <asp:Button runat="server" CssClass="ContentButton" ID="btnClearFont" 
             onclick="btnClearFont_Click" />
        <asp:HiddenField runat="server" ID="hfValue" />                             
    </ContentTemplate>
</cms:CMSUpdatePanel>


