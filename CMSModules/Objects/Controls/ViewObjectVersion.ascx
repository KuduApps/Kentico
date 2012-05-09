<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ViewObjectVersion.ascx.cs"
    Inherits="CMSModules_Objects_Controls_ViewObjectVersion" %>
<%@ Register Src="~/CMSModules/Objects/Controls/ViewObjectDataSet.ascx" TagName="ViewDataSet"
    TagPrefix="cms" %>
<asp:Panel ID="pnlControl" runat="server">
    <asp:Panel ID="pnlAdditionalControls" runat="server" CssClass="PageHeaderLine SiteHeaderLine">
        <cms:LocalizedLabel ID="lblCompareTo" runat="server" ResourceString="content.compareto"
            DisplayColon="true" EnableViewState="false" />
        <cms:LocalizedDropDownList ID="drpCompareTo" runat="server" CssClass="DropDownField"
            AutoPostBack="true" />
        <cms:LocalizedCheckBox ID="chkDisplayAllData" runat="server" ResourceString="objectversioning.viewversion.displayalldata"
            AutoPostBack="true" />
    </asp:Panel>
</asp:Panel>
<asp:Panel ID="pnlBody" runat="server" CssClass="PageContent">
    <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
        Visible="false" />
    <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
        Visible="false" />
    <div class="ObjectVersioning">
        <cms:ViewDataSet ID="viewDataSet" runat="server" ShortID="vd" />
    </div>
</asp:Panel>
