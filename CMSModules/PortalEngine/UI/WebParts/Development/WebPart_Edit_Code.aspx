<%@ Page Language="C#" AutoEventWireup="true"
    MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Inherits="CMSModules_PortalEngine_UI_WebParts_Development_WebPart_Edit_Code"
    Theme="Default" EnableEventValidation="false" CodeFile="WebPart_Edit_Code.aspx.cs" %>

<asp:Content ContentPlaceHolderID="plcContent" ID="content" runat="server">
    <cms:LocalizedLabel runat="server" ID="lblBaseControl" ResourceString="WebPartCode.BaseControl" /> <cms:CMSTextBox runat="server" id="txtBaseControl" EnableViewState="false" CssClass="TextBoxField" />
    <cms:LocalizedButton runat="server" ID="btnGenerate" CssClass="LongButton" 
        ResourceString="WebPartCode.Generate" onclick="btnGenerate_Click" />
    <br />
    <br />
    <strong><cms:LocalizedLabel runat="server" ID="lblASCX" ResourceString="WebPartCode.ASCX" /></strong><br />
    <cms:ExtendedTextArea ID="txtASCX" runat="server" EnableViewState="false" ReadOnly="true"
        EditorMode="Advanced" Width="95%" Height="50px" ShowToolbar="false" />
    <br />
    <br />
    <strong><cms:LocalizedLabel runat="server" ID="lblCode" ResourceString="WebPartCode.Code" /></strong><br />
    <cms:ExtendedTextArea ID="txtCS" runat="server" EnableViewState="false" ReadOnly="true"
        EditorMode="Advanced" Language="CSharp" Width="95%" Height="600px" />
</asp:Content>
