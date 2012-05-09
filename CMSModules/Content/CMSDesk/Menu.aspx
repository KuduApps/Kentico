<%@ Page Language="C#" AutoEventWireup="true" Theme="Default" Inherits="CMSModules_Content_CMSDesk_Menu"
    CodeFile="Menu.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/PageElements/Help.ascx" TagName="Help" TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/UI/UniMenu/UniMenu.ascx" TagName="UniMenu" TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/UI/PageElements/FrameResizer.ascx" TagName="FrameResizer"
    TagPrefix="cms" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>Content - Menu</title>
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
    <form id="form1" runat="server">
    <asp:PlaceHolder runat="server" ID="scriptManager" />
    <input type="hidden" id="selectedNodeId" name="selectedNodeId" value="<%=selectedNodeId%>" />
    <input type="hidden" id="currentMode" name="currentMode" value="edit" />
    <asp:Literal ID="ltlData" runat="server" EnableViewState="false" />
    <asp:Literal ID="ltlScript" runat="server" EnableViewState="false" />
    <cms:FrameResizer ID="frmResizer" runat="server" MinSize="6, *, 30;0, 0, 0, *" Vertical="True"
        CssPrefix="Vertical" />
    <asp:Panel runat="server" ID="pnlBody" CssClass="ContentMenu">
        <asp:Panel runat="server" ID="pnlLeft" CssClass="ContentMenuLeft">
            <cms:UniMenu ID="contentMenu" ShortID="m" runat="server" />
        </asp:Panel>
    </asp:Panel>
    <asp:Panel runat="server" ID="pnlHelp" CssClass="ContentMenuHelp" EnableViewState="false">
        <cms:Help ID="helpElem" runat="server" TopicName="mode_selection" />
    </asp:Panel>
    </form>

    <script type="text/javascript">
        var selNodeElem = document.getElementById('selectedNodeId');
        var curModeElem = document.getElementById('currentMode');
        var imagesUrl = document.getElementById('imagesUrl').value;
    </script>

</body>
</html>
