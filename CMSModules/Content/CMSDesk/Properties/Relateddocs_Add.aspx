<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Content_CMSDesk_Properties_Relateddocs_Add"
    Theme="Default" CodeFile="Relateddocs_Add.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/PageElements/PageTitle.ascx" TagName="PageTitle"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/Content/Controls/Relationships/AddRelatedDocument.ascx"
    TagName="AddRelatedDocument" TagPrefix="cms" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>Document relationship</title>
    <style type="text/css">
        body
        {
            margin: 0px;
            padding: 0px;
            height: 100%;
        }
    </style>
</head>
<body class="VerticalTabsBody <%=mBodyClass%>">
    <form id="form1" runat="server">
    <asp:PlaceHolder ID="plcMain" runat="server" />
    <asp:Panel ID="pnlBody" runat="server" CssClass="VerticalTabsPageBody NoBreadcrumbsBackground">
        <asp:Panel ID="pnlHeader" runat="server" CssClass="PageHeader">
            <cms:PageTitle ID="titleElem" runat="server" />
        </asp:Panel>
        <div class="PageContent">
            <cms:AddRelatedDocument ID="addRelatedDocument" runat="server" />
        </div>
    </asp:Panel>
    </form>
</body>
</html>
