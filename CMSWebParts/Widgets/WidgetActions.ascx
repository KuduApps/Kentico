<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSWebParts_Widgets_WidgetActions"
    CodeFile="~/CMSWebParts/Widgets/WidgetActions.ascx.cs" %>
<%@ Register Src="~/CMSAdminControls/UI/PageElements/Help.ascx" TagName="Help" TagPrefix="cms" %>
<asp:Panel runat="server" ID="pnlWidgetActions">
    <asp:Panel runat="server" CssClass="ActionWidgetBackground FloatLeft" Visible="false" ID="pnlAdd">
        <asp:HyperLink ID="lnkAddWidget" NavigateUrl="#" CssClass="AddWidget" runat="server"
            Visible="false" EnableViewState="false" />
    </asp:Panel>
    <asp:Panel runat="server" ID="pnlReset" CssClass="ActionWidgetBackground FloatLeft"
        Visible="false">
        <asp:LinkButton ID="btnReset" runat="server" CssClass="ResetWidget" EnableViewState="false" />
    </asp:Panel>
</asp:Panel>
<asp:Panel runat="server" ID="pnlContextHelp" Visible="false" CssClass="WidgetActionContextHelp">
    <cms:Help ID="helpElem" runat="server" TopicName="dashboard" />
</asp:Panel>
<asp:Panel runat="server" ID="pnlClear" CssClass="ClearBoth">
</asp:Panel>
