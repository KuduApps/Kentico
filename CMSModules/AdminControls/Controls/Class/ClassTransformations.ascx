<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_AdminControls_Controls_Class_ClassTransformations" CodeFile="ClassTransformations.ascx.cs" %>
<%@ Register src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" tagname="UniGrid" tagprefix="cms" %>
<asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
    Visible="false" />
<asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
    Visible="false" />
<asp:PlaceHolder ID="plcTransformations" runat="server">
    <cms:UniGrid runat="server" ID="uniGrid" OrderBy="TransformationName" IsLiveSite="true" />
</asp:PlaceHolder>
