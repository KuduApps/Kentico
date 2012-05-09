<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Content_CMSDesk_Properties_Languages"
    Title="Languages" ValidateRequest="false" Theme="Default" CodeFile="Languages.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/UI/PageElements/Help.ascx" TagName="Help" TagPrefix="cms" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server" enableviewstate="false">
    <title>Languages</title>
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

    <script type="text/javascript">
        //<![CDATA[
        // Redirect item
        function RedirectItem(nodeId, culture) {
            if (nodeId != 0) {
                if (parent != null) {
                    if (parent.parent != null) {
                        if (parent.parent.parent != null) {
                            if (parent.parent.parent.parent != null) {
                                parent.parent.parent.parent.location.href = "../../../../CMSDesk/default.aspx?section=content&action=edit&mode=editform&nodeid=" + nodeId + "&culture=" + culture;
                            }
                        }
                    }
                }
            }
        }
        //]]>
    </script>

    <asp:Literal ID="ltlScript" runat="server" EnableViewState="false" />
    <asp:Panel ID="pnlBody" runat="server" CssClass="VerticalTabsPageBody">
        <asp:Panel ID="pnlMenu" runat="server" CssClass="ContentEditMenu">
            <table width="100%">
                <tr>
                    <td>
                        <div style="height: 24px; padding: 5px;">
                        </div>
                    </td>
                    <td class="TextRight">
                        <cms:Help ID="helpElem" runat="server" TopicName="doc_languages" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnlContent" runat="server" CssClass="PageContent">
            <asp:Label ID="lblInfo" runat="server" CssClass="InfoLabel" EnableViewState="false" />
            <asp:Panel ID="pnlLanguages" runat="server" CssClass="Languages">
                <cms:UniGrid ID="gridLanguages" runat="server" GridName="Languages.xml" GridLines="Horizontal"
                    IsLiveSite="false" ExportFileName="cms_document" />
            </asp:Panel>
        </asp:Panel>
    </asp:Panel>
    </form>
</body>
</html>
