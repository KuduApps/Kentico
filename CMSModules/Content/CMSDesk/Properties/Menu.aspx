<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Content_CMSDesk_Properties_Menu"
    Theme="Default" CodeFile="Menu.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/PageElements/Help.ascx" TagName="Help" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/Content/Controls/editmenu.ascx" TagName="editmenu"
    TagPrefix="cms" %>
<%@ Register TagPrefix="cms" Namespace="CMS.UIControls" Assembly="CMS.UIControls" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>Properties - menu</title>
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
        function PassiveRefresh() { }

        function RefreshTree(expandNodeId, selectNodeId) {
            // Update tree
            parent.RefreshTree(expandNodeId, selectNodeId);
        }

        function enableTextBoxes(txtId) {
            var txt1 = document.getElementById('txtJavaScript');
            var txt2 = document.getElementById('txtUrl');
            var lblIna = document.getElementById('lblInactivate');
            var txtIna = document.getElementById('txtInactivate');

            if ((txtId == '') || (txtId == 'inactive')) {
                if (txt1 != null) {
                    txt1.disabled = true;
                }

                if (txt2 != null) {
                    txt2.disabled = true;
                }

                if ((txtId == 'inactive') && (lblIna != null) && (txtIna != null)) {
                    lblIna.style.display = "inline";
                    txtIna.style.display = "inline";
                }
                else if ((lblIna != null) && (txtIna != null)) {
                    lblIna.style.display = "none";
                    txtIna.style.display = "none";
                }

            }

            if ((txtId == 'java') && (lblIna != null) && (txtIna != null)) {
                lblIna.style.display = "none";
                txtIna.style.display = "none";

                if (txt1 != null) {
                    txt1.disabled = false;
                }

                if (txt2 != null) {
                    txt2.disabled = true;
                }
            }

            if ((txtId == 'url') && (lblIna != null) && (txtIna != null)) {
                lblIna.style.display = "none";
                txtIna.style.display = "none";
                
                if (txt1 != null) {
                    txt1.disabled = true;
                }

                if (txt2 != null) {
                    txt2.disabled = false;
                }
            }
        }
        //]]>
    </script>

    <asp:Panel runat="server" ID="pnlBody" CssClass="VerticalTabsPageBody">
        <asp:Panel runat="server" ID="pnlTab" CssClass="VerticalTabsPageContent">
            <asp:Panel runat="server" ID="pnlMenu" CssClass="ContentEditMenu">
                <table width="100%">
                    <tr>
                        <td>
                            <cms:editmenu ID="menuElem" runat="server" ShowApprove="true" ShowReject="true" ShowSubmitToApproval="true"
                                ShowProperties="false" EnablePassiveRefresh="false" OnLocalSave="btnOK_Click" />
                        </td>
                        <td class="TextRight">
                            <cms:Help ID="helpElem" runat="server" TopicName="menu" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="pnlContent" runat="server" CssClass="PageContent PropertiesPanel">
                <asp:Label ID="lblWorkflowInfo" runat="server" CssClass="InfoLabel" EnableViewState="false"
                    Visible="false" />
                <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
                    Visible="false" /><asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
                        Visible="false" />
                <asp:Panel ID="pnlForm" runat="server">
                    <cms:UIPlaceHolder ID="pnlUIBasicProperties" runat="server" ModuleName="CMS.Content"
                        ElementName="Menu.BasicProperties">
                        <asp:Panel ID="pnlBasicProperties" runat="server" CssClass="NodePermissions" GroupingText="Basic properties">
                            <table>
                                <tr>
                                    <td class="FieldLabel">
                                        <cms:LocalizedLabel ID="lblMenuCaption" runat="server" EnableViewState="false" ResourceString="MenuProperties.Caption" />
                                    </td>
                                    <td>
                                        <cms:CMSTextBox ID="txtMenuCaption" runat="server" CssClass="TextBoxField" MaxLength="200" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="FieldLabel">
                                        <cms:LocalizedLabel ID="lblShowInNavigation" runat="server" ResourceString="MenuProperties.ShowInNavigation" />
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkShowInNavigation" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="FieldLabel">
                                        <cms:LocalizedLabel ID="lblShowInSitemap" runat="server" ResourceString="MenuProperties.ShowInSitemap" />
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkShowInSitemap" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <br />
                    </cms:UIPlaceHolder>
                    <%-- Menu action --%>
                    <cms:UIPlaceHolder ID="pnlUIActions" runat="server" ModuleName="CMS.Content" ElementName="Menu.Actions">
                        <asp:Panel ID="pnlActions" runat="server" CssClass="NodePermissions" GroupingText="Menu actions">
                            <table>
                                <tr>
                                    <td>
                                        <cms:LocalizedRadioButton runat="server" ID="radStandard" GroupName="MenuActionGroup"
                                            Checked="true" ResourceString="MenuProperties.Standard" />
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <cms:LocalizedRadioButton runat="server" GroupName="MenuActionGroup" ID="radInactive"
                                            ResourceString="MenuProperties.Inactive" />
                                        <div style="text-align: right; padding-bottom: 5px;">
                                            <span id="lblInactivate" style="display: none; padding-left: 20px;">
                                                <cms:LocalizedLabel runat="server" ID="lblUrlInactive" ResourceString="MenuProperties.InactivateUrl" />
                                            </span>
                                        </div>
                                    </td>
                                    <td style="vertical-align: bottom;">
                                        <span id="txtInactivate" style="display: none">
                                            <cms:CMSTextBox runat="server" ID="txtUrlInactive" MaxLength="450" CssClass="TextBoxField" />
                                        </span>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <cms:LocalizedRadioButton runat="server" GroupName="MenuActionGroup" ID="radJavascript"
                                            ResourceString="MenuProperties.Javascript" />
                                    </td>
                                    <td>
                                        <cms:CMSTextBox runat="server" ID="txtJavaScript" CssClass="TextBoxField" MaxLength="450" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td style="padding-bottom: 7px;">
                                        <cms:LocalizedLabel ID="lblJavaScript" runat="server" ResourceString="MenuProperties.JavaScriptHelp"
                                            Font-Italic="true" EnableViewState="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <cms:LocalizedRadioButton runat="server" GroupName="MenuActionGroup" ID="radUrl"
                                            ResourceString="MenuProperties.Url" />
                                    </td>
                                    <td>
                                        <cms:CMSTextBox runat="server" ID="txtUrl" CssClass="TextBoxField" MaxLength="450" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <cms:LocalizedLabel ID="lblUrl" runat="server" ResourceString="MenuProperties.UrlHelp"
                                            Font-Italic="true" EnableViewState="false" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <br />
                    </cms:UIPlaceHolder>
                    <cms:UIPlaceHolder ID="pnlUIDesign" runat="server" ModuleName="CMS.Content" ElementName="Menu.Design">
                        <%-- Menu item design --%>
                        <asp:Panel ID="pnlDesign" runat="server" CssClass="NodePermissions" GroupingText="Menu design">
                            <table>
                                <tr>
                                    <td colspan="2">
                                        <cms:LocalizedLabel ID="lblMenuItemDesign" runat="server" Font-Bold="true" ResourceString="MenuProperties.Design" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="FieldLabel">
                                        <cms:LocalizedLabel ID="lblMenuItemStyle" runat="server" EnableViewState="false"
                                            ResourceString="MenuProperties.Style" />
                                    </td>
                                    <td>
                                        <cms:CMSTextBox ID="txtMenuItemStyle" runat="server" CssClass="TextBoxField" MaxLength="100" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="FieldLabel">
                                        <cms:LocalizedLabel ID="lblCssClass" runat="server" EnableViewState="false" ResourceString="MenuProperties.CssClass" />
                                    </td>
                                    <td>
                                        <cms:CMSTextBox ID="txtCssClass" runat="server" CssClass="TextBoxField" MaxLength="100" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="FieldLabel">
                                        <cms:LocalizedLabel ID="lblMenuItemLeftmage" runat="server" EnableViewState="false"
                                            ResourceString="MenuProperties.LeftImage" />
                                    </td>
                                    <td>
                                        <cms:CMSTextBox ID="txtMenuItemLeftImage" runat="server" CssClass="TextBoxField" MaxLength="200" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="FieldLabel">
                                        <cms:LocalizedLabel ID="lblMenuItemImage" runat="server" EnableViewState="false"
                                            ResourceString="MenuProperties.Image" />
                                    </td>
                                    <td>
                                        <cms:CMSTextBox ID="txtMenuItemImage" runat="server" CssClass="TextBoxField" MaxLength="200" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="FieldLabel">
                                        <cms:LocalizedLabel ID="lblMenuItemRightImage" runat="server" EnableViewState="false"
                                            ResourceString="MenuProperties.RightImage" />
                                    </td>
                                    <td>
                                        <cms:CMSTextBox ID="txtMenuItemRightImage" runat="server" CssClass="TextBoxField" MaxLength="200" />
                                    </td>
                                </tr>
                                <%-- Menu item mouse over --%>
                                <tr>
                                    <td colspan="2">
                                        <br />
                                        <cms:LocalizedLabel ID="lblMenuItemDesignOnMouseOver" runat="server" Font-Bold="true"
                                            ResourceString="MenuProperties.MouseOver" EnableViewState="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="FieldLabel">
                                        <cms:LocalizedLabel ID="lblMenuItemStyleMouseOverr" runat="server" EnableViewState="false"
                                            ResourceString="MenuProperties.Style" />
                                    </td>
                                    <td>
                                        <cms:CMSTextBox ID="txtMenuItemStyleMouseOver" runat="server" CssClass="TextBoxField"
                                            MaxLength="50" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="FieldLabel">
                                        <cms:LocalizedLabel ID="lblCssClassMouseOver" runat="server" EnableViewState="false"
                                            ResourceString="MenuProperties.CssClass" />
                                    </td>
                                    <td>
                                        <cms:CMSTextBox ID="txtCssClassMouseOver" runat="server" CssClass="TextBoxField" MaxLength="100" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="FieldLabel">
                                        <cms:LocalizedLabel ID="lblMenuItemLeftmageMouseOver" runat="server" EnableViewState="false"
                                            ResourceString="MenuProperties.LeftImage" />
                                    </td>
                                    <td>
                                        <cms:CMSTextBox ID="txtMenuItemLeftImageMouseOver" runat="server" CssClass="TextBoxField"
                                            MaxLength="200" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="FieldLabel">
                                        <cms:LocalizedLabel ID="lblMenuItemImageMouseOver" runat="server" EnableViewState="false"
                                            ResourceString="MenuProperties.Image" />
                                    </td>
                                    <td>
                                        <cms:CMSTextBox ID="txtMenuItemImageMouseOver" runat="server" CssClass="TextBoxField"
                                            MaxLength="200" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="FieldLabel">
                                        <cms:LocalizedLabel ID="lblMenuItemRightImageMouseOver" runat="server" EnableViewState="false"
                                            ResourceString="MenuProperties.RightImage" />
                                    </td>
                                    <td>
                                        <cms:CMSTextBox ID="txtMenuItemRightImageMouseOver" runat="server" CssClass="TextBoxField"
                                            MaxLength="200" />
                                    </td>
                                </tr>
                                <%-- Menu item highlighted --%>
                                <tr>
                                    <td colspan="2">
                                        <br />
                                        <cms:LocalizedLabel ID="lblMenuItemDesignHighlighted" runat="server" Font-Bold="true"
                                            ResourceString="MenuProperties.Highlight" EnableViewState="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="FieldLabel">
                                        <cms:LocalizedLabel ID="lblMenuItemStyleHighlight" runat="server" EnableViewState="false"
                                            ResourceString="MenuProperties.Style" />
                                    </td>
                                    <td>
                                        <cms:CMSTextBox ID="txtMenuItemStyleHighlight" runat="server" CssClass="TextBoxField"
                                            MaxLength="200" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="FieldLabel">
                                        <cms:LocalizedLabel ID="lblCssClassHighlight" runat="server" EnableViewState="false"
                                            ResourceString="MenuProperties.CssClass" />
                                    </td>
                                    <td>
                                        <cms:CMSTextBox ID="txtCssClassHighlight" runat="server" CssClass="TextBoxField" MaxLength="50" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="FieldLabel">
                                        <cms:LocalizedLabel ID="lblMenuItemLeftmageHighlight" runat="server" EnableViewState="false"
                                            ResourceString="MenuProperties.LeftImage" />
                                    </td>
                                    <td>
                                        <cms:CMSTextBox ID="txtMenuItemLeftImageHighlight" runat="server" CssClass="TextBoxField"
                                            MaxLength="200" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="FieldLabel">
                                        <cms:LocalizedLabel ID="lblMenuItemImageHighlight" runat="server" EnableViewState="false"
                                            ResourceString="MenuProperties.Image" />
                                    </td>
                                    <td>
                                        <cms:CMSTextBox ID="txtMenuItemImageHighlight" runat="server" CssClass="TextBoxField"
                                            MaxLength="200" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="FieldLabel">
                                        <cms:LocalizedLabel ID="lblMenuItemRightImageHighlight" runat="server" EnableViewState="false"
                                            ResourceString="MenuProperties.RightImage" />
                                    </td>
                                    <td>
                                        <cms:CMSTextBox ID="txtMenuItemRightImageHighlight" runat="server" CssClass="TextBoxField"
                                            MaxLength="200" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <cms:LocalizedButton ID="btnOk" runat="server" CssClass="HiddenButton" OnClick="btnOK_Click"
                                            ResourceString="general.ok" EnableViewState="false" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </cms:UIPlaceHolder>
                </asp:Panel>
            </asp:Panel>
        </asp:Panel>
    </asp:Panel>
    <asp:Literal runat="server" ID="ltlScript" EnableViewState="false" />

    <script type="text/javascript">
        //<![CDATA[
        // Refresh action
        function RefreshNode(nodeId, selectNodeId) {
            parent.parent.parent.frames['contenttree'].RefreshNode(nodeId, selectNodeId);
        }
        //]]>                        
    </script>

    </form>
</body>
</html>
