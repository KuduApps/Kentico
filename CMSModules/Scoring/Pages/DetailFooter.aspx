<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DetailFooter.aspx.cs" Inherits="CMSModules_Scoring_Pages_DetailFooter"
    Theme="default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Detail footer</title>
    <style type="text/css">
        body
        {
            margin: 0px;
            padding: 0px;
            height: 100%;
            background-color: #f5f3ec;
        }
    </style>
</head>
<body class="<%=mBodyClass%> Buttons">
    <form id="form1" runat="server">
    <asp:Panel runat="server" ID="pnlScroll" CssClass="ButtonPanel">
        <div class="FloatRight">
            <cms:LocalizedButton ID="btnClose" runat="server" CssClass="SubmitButton" EnableViewState="false"
                OnClientClick="parent.window.close(); if (parent.wopener != null && parent.wopener.Refresh != null) { parent.wopener.Refresh(); } return false;" ResourceString="general.close" />
        </div>
    </asp:Panel>
    </form>
</body>
</html>
