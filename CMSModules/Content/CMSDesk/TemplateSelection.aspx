<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Content_CMSDesk_TemplateSelection"
    Theme="Default" CodeFile="TemplateSelection.aspx.cs" %>

<%@ Register Src="New/TemplateSelection.ascx" TagName="TemplateSelection" TagPrefix="cms" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>Template selection</title>
    <style type="text/css">
        body
        {
            margin: 0px;
            padding: 0px;
        }
        .PTSelection .HeaderRow
        {
            line-height: normal !important;
        }
    </style>
</head>
<body class="<%=mBodyClass%>">
    <form id="form1" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="scriptManager" runat="server" />
    <asp:Literal ID="ltlScript" runat="server" EnableViewState="false" />
    <asp:Panel runat="server" ID="pnlBody" CssClass="PageBody">
        <div class="NewPageDialog">
            <asp:Panel ID="pnlContent" runat="server" CssClass="PageContentFrame">
                <div class="PTSelection">
                    <div style="padding: 5px 5px 0px 5px;">
                        <asp:Label ID="lblInfo" runat="server" CssClass="InfoLabel" EnableViewState="false"
                            Visible="false" />
                        <asp:Label ID="lblError" runat="server" CssClass="ErrorLabel" EnableViewState="false" />
                    </div>
                    <table cellpadding="0" cellspacing="0" class="Table" border="0">
                        <tr class="HeaderRow">
                            <td class="LeftBorder">
                            </td>
                            <td style="vertical-align: top;" class="Header">
                                <asp:Label runat="server" ID="lblChoose" /><br />
                            </td>
                            <td class="RightBorder">
                            </td>
                        </tr>
                    </table>
                    <table cellpadding="0" cellspacing="0" class="Table" border="0">
                        <tr class="Row">
                            <td style="vertical-align: top;" class="Content">
                                <cms:TemplateSelection ID="selTemplate" runat="server" />
                            </td>
                        </tr>
                    </table>
                    <div class="Footer">
                    </div>
                </div>
            </asp:Panel>
        </div>
    </asp:Panel>
    </form>
</body>
</html>
