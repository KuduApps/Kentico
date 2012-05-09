<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Content_CMSDesk_Edit_EditMenu"
    Theme="Default" CodeFile="EditMenu.aspx.cs" %>

<%@ Register Src="~/CMSModules/Content/Controls/EditMenu.ascx" TagName="editmenu"
    TagPrefix="cms" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>Content - Edit</title>
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
    <asp:Panel runat="server" ID="Panel1" CssClass="EditMenuBody" EnableViewState="false">
        <asp:Panel runat="server" ID="pnlMenu" CssClass="ContentEditMenu">
            <div style="padding: 4px 0px 0px 4px;">
                <cms:editmenu ID="menuElem" ShortID="m" runat="server" ShowProperties="false" ShowSpellCheck="true" />
            </div>
        </asp:Panel>
    </asp:Panel>
    </form>
</body>
</html>
