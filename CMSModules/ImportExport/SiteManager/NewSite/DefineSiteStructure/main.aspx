<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_ImportExport_SiteManager_NewSite_DefineSiteStructure_main"
    Theme="Default" CodeFile="main.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/PageElements/PageTitle.ascx" TagName="PageTitle"
    TagPrefix="cms" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server" enableviewstate="false">
    <title>New site - define site structure</title>
    <style>
        body
        {
            margin: 0px;
        }
    </style>
</head>
<body class="<%=mBodyClass%>">

    <script type="text/javascript">
        //<![CDATA[

        var selectedNodeId = 0;

        // Refresh action
        function RefreshNode(nodeId, selectNodeId) {
            parent.frames['definestructuretree'].RefreshNode(nodeId, selectNodeId);
        }

        // Selects the node within the tree
        function SelectNode(nodeId) {
            parent.frames['definestructuretree'].SelectNode(nodeId, null);
        }

        function OnSelectPageTemplate(templateId, templateName, selectorid) {
            if (templateId != 0) {
                document.getElementById('txtPageTemplate').value = templateName;
                document.getElementById('SelectedPageTemplateId').value = templateId;
            }
            return false;
        }

        function SetSelectedNodeId(nodeId) {
            selectedNodeId = nodeId;
        }

        function GetSelectedNodeId() {
            return selectedNodeId;
        }

        function InheritPageTemplate(btn) {
            btn.disabled = true;
            document.getElementById('txtPageTemplate').value = document.getElementById('InheritFromParent').value;
            document.getElementById('SelectedPageTemplateId').value = -1;
        }
        //]]>                        
    </script>

    <asp:Literal ID="ltlScript" runat="server" EnableViewState="false" />
    <form id="form1" runat="server" style="background-color: White; width: 100%; height: 100%;">
    <div style="height: 100%; width: 100%; overflow: auto;">
        <asp:Panel ID="PanelTitle" runat="server" CssClass="PageHeader">
            <div class="Wizard">
                <cms:PageTitle ID="PageTitle1" runat="server" />
            </div>
        </asp:Panel>
        <table style="margin-top: 10px;" class="ObjectContent">
            <tr>
                <td>
                    <cms:LocalizedLabel ID="lblPageName" runat="server" ResourceString="DefineSiteStructure.PageName" />
                </td>
                <td>
                    <cms:CMSTextBox CssClass="TextBoxField" Width="250" ID="txtPageName" runat="server"
                        MaxLength="450" />
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <cms:LocalizedLabel ID="lblPageTemplate" runat="server" ResourceString="DefineSiteStructure.PageTemplate" />
                </td>
                <td colspan="2">
                    <cms:CMSTextBox CssClass="TextBoxField" Width="250" ID="txtPageTemplate" runat="server"
                        ReadOnly="true" /><cms:LocalizedButton ID="btnSelectPageTemplate" CssClass="ContentButton"
                            runat="server" CausesValidation="false" ResourceString="DefineSiteStructure.SelectPageTemplate" /><cms:CMSButton
                                ID="btnInheritFromParent" CssClass="LongButton" OnClientClick="InheritPageTemplate(this);return false;"
                                runat="server" Width="150" CausesValidation="false" />
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td colspan="2">
                    <cms:LocalizedButton ID="btnSave" CssClass="SubmitButton" runat="server" OnClick="btnSave_Click"
                        ResourceString="general.ok" />
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:RequiredFieldValidator ID="reqItemName" runat="server" ControlToValidate="txtPageName">&nbsp;</asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="reqItemPageTemplate" runat="server" ControlToValidate="txtPageTemplate">&nbsp;</asp:RequiredFieldValidator>
                    <asp:ValidationSummary ID="reqSummary" runat="server" DisplayMode="List" ShowMessageBox="true"
                        ShowSummary="false" />
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="InheritFromParent" runat="server" />
        <asp:HiddenField ID="SelectedPageTemplateId" runat="server" />
    </div>
    </form>
</body>
</html>
