<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Staging_Tools_Data_TaskSeparator"
    Theme="Default" EnableViewState="false" CodeFile="TaskSeparator.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>Staging - Task</title>
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
    <asp:Panel ID="PanelBody" runat="server" CssClass="PageBody" EnableViewState="false">
        <asp:Panel ID="pnlContent" runat="server" CssClass="PageContent" EnableViewState="false">
            <asp:Label runat="server" ID="lblInfo" EnableViewState="false" />
        </asp:Panel>
    </asp:Panel>
    </form>
</body>
</html>
