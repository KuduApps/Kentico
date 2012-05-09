<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSMessages_RestorePostback" Theme="Default" ValidateRequest="false" ViewStateEncryptionMode="Never" EnableEventValidation="false" CodeFile="RestorePostback.aspx.cs" %>
<%@ Register src="~/CMSAdminControls/UI/PageElements/PageTitle.ascx" tagname="PageTitle" tagprefix="cms" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>Restore submitted form</title>
    <style type="text/css">
        body
        {
            margin: 0px;
            padding: 0px;
            height: 100%;
        }
    </style>
	<base target="_self" />
</head>
<body class="<%=mBodyClass%>" <%=mBodyParams%>>
    <form id="form1" runat="server">
    ##formstart##
    <asp:Panel ID="PanelBody" runat="server" CssClass="PageBody">
        <asp:Panel ID="PanelTitle" runat="server" CssClass="PageHeader">
            <cms:PageTitle ID="titleElem" runat="server" />
        </asp:Panel>
        <asp:Panel ID="PanelContent" runat="server" CssClass="PageContent">
            <asp:Label runat="server" ID="lblInfo" EnableViewState="false" />
            <br />
            <asp:Literal runat="server" ID="ltlValues" EnableViewState="false" />
            <br />
            <asp:PlaceHolder runat="server" ID="plcOK">
                <input type="submit" value="<%= okText %>" class="SubmitButton" />
            </asp:PlaceHolder>
            <input type="submit" value="<%= cancelText %>" class="SubmitButton" onclick="document.location.replace(document.location.href); return false;" />
        </asp:Panel>
    </asp:Panel>
    ##formend##
    </form>
</body>
</html>
