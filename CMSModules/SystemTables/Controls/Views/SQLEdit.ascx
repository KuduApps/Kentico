<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_SystemTables_Controls_Views_SQLEdit"
    CodeFile="SQLEdit.ascx.cs" %>
<%@ Register Src="~/CMSAdminControls/UI/PageElements/Help.ascx" TagName="Help" TagPrefix="cms" %>
<asp:Label runat="server" ID="lblWarning" CssClass="ErrorLabel" EnableViewState="false"
    Visible="false" />
<asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
    Visible="false" />
<asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
    Visible="false" />
<asp:PlaceHolder ID="plcGenerate" runat="server" Visible="false">
    <cms:LocalizedButton ID="btnGenerate" runat="server" CssClass="XLongButton" OnClick="btnGenerate_Click"
        ResourceString="sysdev.views.generateskeleton" EnableViewState="false" />
    <br />
    <br />
</asp:PlaceHolder>
<asp:Label ID="lblCreateLbl" runat="server" />
<cms:CMSTextBox ID="txtObjName" CssClass="TextBoxField" runat="server" MaxLength="128" /><br />
<asp:PlaceHolder ID="plcParams" runat="server">
    <cms:CMSTextBox ID="txtParams" runat="server" TextMode="MultiLine" Height="100px" Width="100%" /><br />
</asp:PlaceHolder>
<asp:Literal ID="lblBegin" runat="server" />
<cms:ExtendedTextArea runat="server" ID="txtSQLText" EnableViewState="false" EditorMode="Advanced"
    Language="SQL" Height="400px" Width="100%" />
<asp:Label ID="lblEnd" runat="server" />
<br />
<br />
<br />
<cms:LocalizedButton runat="server" ID="btnOk" EnableViewState="false" OnClick="btnOK_Click"
    ResourceString="general.ok" CssClass="SubmitButton" />
