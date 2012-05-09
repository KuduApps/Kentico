<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Content_CMSDesk_Properties_Advanced_EditableContent_Main"
    Theme="default" CodeFile="Main.aspx.cs" %>

<%@ Register Src="~/CMSModules/Content/Controls/editmenu.ascx" TagName="editmenu"
    TagPrefix="cms" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>Editable content - main</title>
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
    <cms:CMSPageManager ID="pageManagerElem" runat="server" Visible="false" />
    <asp:Panel runat="server" ID="pnlMenu" CssClass="ContentEditMenu" Visible="false">
        <cms:editmenu ID="menuElem" runat="server" ShowApprove="true" ShowReject="true" ShowSubmitToApproval="true"
            ShowProperties="false" OnLocalSave="btnSave_Click" EnablePassiveRefresh="false"
            ShowCreateAnother="false" />
    </asp:Panel>
    <asp:Panel ID="pnlContent" runat="server" CssClass="PageContent">
        <asp:Panel ID="pnlEditableContent" runat="server" Visible="false">
            <table style="width: 100%">
                <tr>
                    <td colspan="2">
                        <asp:Label ID="lblInfo" runat="server" EnableViewState="false" CssClass="InfoLabel"
                            Visible="false" />
                        <asp:Label ID="lblWorkflow" runat="server" EnableViewState="false" CssClass="InfoLabel"
                            Visible="false" />
                        <asp:Label ID="lblError" runat="server" EnableViewState="false" CssClass="ErrorLabel"
                            Visible="false" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <cms:LocalizedLabel ID="lblEditControl" runat="server" EnableViewState="false" CssClass="FieldLabel"
                            ResourceString="EditableContent.EditControl" />
                    </td>
                    <td style="width: 100%;">
                        <asp:DropDownList ID="drpEditControl" runat="server" AutoPostBack="true" CssClass="DropDownField" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <cms:LocalizedLabel ID="lblName" runat="server" EnableViewState="false" CssClass="FieldLabel"
                            ResourceString="General.CodeName" DisplayColon="true" />
                    </td>
                    <td style="width: 100%;">
                        <cms:CMSTextBox ID="txtName" runat="server" CssClass="TextBoxField" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <cms:ExtendedTextArea Visible="false" ID="txtAreaContent" runat="server" Height="330px"
                            TextMode="MultiLine" Width="720px" />
                        <cms:CMSHtmlEditor Visible="false" ID="htmlContent" runat="server" Width="720px"
                            Height="350px" />
                        <cms:CMSEditableImage Visible="false" ID="imageContent" runat="server" Width="720"
                            ImageWidth="720" IsLiveSite="false" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <cms:LocalizedLabel ID="lblContent" runat="server" EnableViewState="false" CssClass="FieldLabel"
                            ResourceString="header.content" DisplayColon="true" Visible="false" />
                    </td>
                    <td>
                        <cms:CMSTextBox ID="txtContent" runat="server" Visible="false" CssClass="TextBoxField" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </asp:Panel>
    <table style="width: 100%">
        <tr>
            <td class="TextRight">
                <cms:CMSButton ID="btnCheckIn" runat="server" OnClick="btnCheckIn_Click" CssClass="HiddenButton" />
            </td>
        </tr>
    </table>

    <script type="text/javascript">
        //<![CDATA[
        var mainUrl = '';

        // Selects specified node
        function SelectNode(nodeName, nodeType) {
            document.location.replace(mainUrl + "&nodename=" + nodeName + "&nodetype=" + nodeType);
        }

        // Selects specified after saving
        function SelectNodeAfterImageSave(nodeName, nodeType) {
            document.location.replace(mainUrl + "&nodename=" + nodeName + "&nodetype=" + nodeType + "&imagesaved=true");
        }

        // Refreshes after saving
        function RefreshNode(nodeName, nodeType, nodeId) {
            parent.frames['tree'].RefreshNode(nodeName, nodeType, nodeId);
        }

        // Opens menu for creating new item
        function CreateNew(nodetype) {
            document.location.replace(mainUrl + "&createNew=true&nodetype=" + nodetype);
        }
        //]]>
    </script>

    <asp:Literal ID="ltlScript" runat="server" EnableViewState="false" />
    </form>
</body>
</html>
