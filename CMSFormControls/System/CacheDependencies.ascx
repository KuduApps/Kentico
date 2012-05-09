<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSFormControls_System_CacheDependencies" CodeFile="CacheDependencies.ascx.cs" %>
<%@ Register Src="~/CMSFormControls/Inputs/LargeTextArea.ascx" TagName="LargeTextArea" TagPrefix="uc1" %>
<asp:CheckBox runat="server" ID="chkDependencies" /><br />
<uc1:LargeTextArea ID="txtDependencies" runat="server" />
