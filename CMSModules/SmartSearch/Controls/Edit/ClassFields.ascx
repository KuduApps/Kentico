<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_SmartSearch_Controls_Edit_ClassFields" CodeFile="ClassFields.ascx.cs" %>
<asp:Panel ID="pnlBody" runat="server">
    <cms:LocalizedLabel runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
        Visible="false" ResourceString="general.changessaved" />
    <div runat="server" id="pnlButton" class="ClassFieldsButtonPanel">
        <cms:LocalizedLinkButton runat="server" ID="btnAutomatically" ResourceString="srch.automatically"
            Visible="false" />
    </div>
    <asp:Panel ID="pnlContent" runat="server">
    </asp:Panel>
</asp:Panel>
