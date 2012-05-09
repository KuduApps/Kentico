<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSAdminControls_UI_Macros_ObjectBrowser"
    Title="Untitled Page" ValidateRequest="false" Theme="default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master"
    CodeFile="ObjectBrowser.aspx.cs" %>

<%@ Register Src="../Trees/MacroTree.ascx" TagName="MacroTree" TagPrefix="cms" %>
<%@ Register Src="MacroEditor.ascx" TagName="MacroEditor" TagPrefix="cms" %>
<asp:Content runat="server" ContentPlaceHolderID="plcBeforeContent">
    <div class="PageHeaderLine">
        <cms:MacroEditor ID="editorElem" runat="server" Text="CMSContext.Current" />
        <cms:LocalizedButton runat="server" id="btnRefresh" 
            ResourceString="general.Refresh" onclick="btnRefresh_Click" />
    </div>
</asp:Content>
<asp:Content ID="cntContent" ContentPlaceHolderID="plcContent" runat="Server">
    <cms:MacroTree ID="treeElem" runat="server" />
    <br />
    <br />
    <asp:Textbox runat="server" ID="txtOutput" TextMode="MultiLine" Height="400" Width="800" />
</asp:Content>
