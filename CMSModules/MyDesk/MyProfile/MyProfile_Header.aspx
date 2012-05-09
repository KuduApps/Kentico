<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_MyDesk_MyProfile_MyProfile_Header" Theme="Default" CodeFile="MyProfile_Header.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/PageElements/PageTitle.ascx" TagName="PageTitle" TagPrefix="cms" %>
<%@ Register TagPrefix="cms" Namespace="CMS.UIControls" Assembly="CMS.UIControls" %>
<%@ Register Src="~/CMSAdminControls/UI/PageElements/FrameResizer.ascx" TagName="FrameResizer" TagPrefix="cms" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>MyProfile - Header</title>
    <style type="text/css">
        body
        {
            margin: 0px;
            padding: 0px;
            height: 100%;
        }
    </style>
</head>
<body class="<%=mBodyClass%>">
    <form id="form1" runat="server" enableviewstate="false">
    <cms:FrameResizer ID="frmResizer" runat="server" MinSize="6,*" Vertical="True" CssPrefix="Vertical" />
    <asp:Panel ID="pnlBody" runat="server" CssClass="TabsPageHeader">
        <asp:Panel ID="pnlHeader" runat="server" CssClass="PageHeader" EnableViewState="false">
            <cms:PageTitle ID="titleElem" runat="server" />
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlTabs" CssClass="TabsPageTabs" EnableViewState="false">
            <asp:Panel runat="server" ID="pnlLeft" CssClass="TabsLeft">
                &nbsp;
            </asp:Panel>
            <asp:Panel runat="server" ID="pnlPropTabs" CssClass="TabsTabs LightTabs">
                <asp:Panel runat="server" ID="pnlWhite" CssClass="TabsWhite">
                    <cms:UITabs ID="tabElem" runat="server" UseClientScript="true" ModuleName="CMS.MyDesk"
                        ElementName="MyProfile" />
                </asp:Panel>
            </asp:Panel>
            <asp:Panel runat="server" ID="pnlRight" CssClass="TabsRight">
                &nbsp;
            </asp:Panel>
        </asp:Panel>
        <asp:Panel runat="server" ID="PanelSeparator" CssClass="HeaderSeparator">
            &nbsp;
        </asp:Panel>
    </asp:Panel>
    <asp:Literal runat="server" ID="ltlScript" EnableViewState="false" />
    </form>
</body>
</html>
