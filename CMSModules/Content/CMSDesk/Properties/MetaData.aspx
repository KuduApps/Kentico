<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Content_CMSDesk_Properties_MetaData"
    Theme="Default" CodeFile="MetaData.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/PageElements/Help.ascx" TagName="Help" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/Content/Controls/editmenu.ascx" TagName="editmenu"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/Content/FormControls/Tags/TagGroupSelector.ascx" TagName="TagGroupSelector"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/Content/FormControls/Tags/TagSelector.ascx" TagName="TagSelector"
    TagPrefix="cms" %>
<%@ Register TagPrefix="cms" Namespace="CMS.UIControls" Assembly="CMS.UIControls" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server" enableviewstate="false">
    <title>Properties - Page</title>
    <style type="text/css">
        body
        {
            margin: 0px;
            padding: 0px;
            height: 100%;
        }
    </style>

    <script type="text/javascript">
        //<![CDATA[
        function RefreshTree(expandNodeId, selectNodeId) {
            // Update tree
            parent.RefreshTree(expandNodeId, selectNodeId);
        }
        //]]>
    </script>

</head>
<body class="VerticalTabsBody <%=mBodyClass%>">
    <form id="form1" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="scriptManager" runat="server" />
    <asp:Panel ID="pnlTab" runat="server" CssClass="VerticalTabsPageBody">
        <asp:Panel runat="server" ID="pnlMenu" CssClass="ContentEditMenu">
            <table width="100%">
                <tr>
                    <td>
                        <cms:editmenu ID="menuElem" runat="server" ShowApprove="true" ShowReject="true" ShowSubmitToApproval="true"
                            ShowProperties="false" OnLocalSave="btnOK_Click" EnablePassiveRefresh="false" />
                    </td>
                    <td class="TextRight">
                        <cms:Help ID="helpElem" runat="server" TopicName="metadata" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnlContent" runat="server" CssClass="PageContent PropertiesPanel">
            <asp:Label ID="lblWorkflowInfo" runat="server" CssClass="InfoLabel" EnableViewState="false"
                Visible="false" />
            <asp:Label ID="lblError" runat="server" EnableViewState="false" CssClass="ErrorLabel"
                Visible="false" />
            <asp:Label ID="lblInfo" runat="server" EnableViewState="false" CssClass="InfoLabel"
                Visible="false" />
            <asp:Panel ID="pnlForm" runat="server">
                <cms:CMSUpdatePanel ID="ucPanel" runat="server">
                    <ContentTemplate>
                        <cms:UIPlaceHolder ID="pnlUIPage" runat="server" ModuleName="CMS.Content" ElementName="MetaData.Page">
                            <asp:Panel ID="pnlPageSettings" runat="server" CssClass="NodePermissions" GroupingText="Page settings">
                                <table>
                                    <tr>
                                        <td class="FieldLabel">
                                            <asp:Label ID="lblPageTitle" runat="server" EnableViewState="false" />
                                        </td>
                                        <td>
                                            <cms:CMSTextBox ID="txtTitle" runat="server" CssClass="TextBoxField" />
                                            <div>
                                                <asp:CheckBox runat="server" ID="chkTitle" AutoPostBack="true" OnCheckedChanged="chkTitle_CheckedChanged" />
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="FieldLabel">
                                            <asp:Label ID="lblPageDescription" runat="server" EnableViewState="false" />
                                        </td>
                                        <td>
                                            <cms:CMSTextBox ID="txtDescription" runat="server" CssClass="TextAreaField" TextMode="MultiLine" />
                                            <div>
                                                <asp:CheckBox runat="server" ID="chkDescription" AutoPostBack="true" OnCheckedChanged="chkDescription_CheckedChanged" />
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="FieldLabel">
                                            <asp:Label ID="lblPageKeywords" runat="server" EnableViewState="false" />
                                        </td>
                                        <td>
                                            <cms:CMSTextBox ID="txtKeywords" runat="server" CssClass="TextAreaField" TextMode="MultiLine" />
                                            <div>
                                                <asp:CheckBox runat="server" ID="chkKeyWords" AutoPostBack="true" OnCheckedChanged="chkKeyWords_CheckedChanged" />
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <br />
                        </cms:UIPlaceHolder>
                        <cms:UIPlaceHolder ID="pnlUITags" runat="server" ModuleName="CMS.Content" ElementName="MetaData.Tags">
                            <asp:Panel ID="pnlTags" runat="server" CssClass="NodePermissions" GroupingText="Tags">
                                <table>
                                    <tr>
                                        <td colspan="2">
                                            <asp:Label ID="lblTagInfo" runat="server" Visible="false" CssClass="infoLabel" EnableViewState="false" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="FieldLabel">
                                            <asp:Label ID="lblTagGroupSelector" runat="server" EnableViewState="false" />
                                        </td>
                                        <td>
                                            <cms:TagGroupSelector runat="server" ID="tagGroupSelectorElem" UseAutoPostback="true"
                                                UseGroupNameForSelection="false" UseDropdownList="true" />
                                            <asp:CheckBox runat="server" ID="chkTagGroupSelector" AutoPostBack="true" OnCheckedChanged="chkTagGroupSelector_CheckedChanged" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="FieldLabel" style="vertical-align: top;">
                                            <asp:Label ID="lblTagSelector" runat="server" EnableViewState="false" />
                                        </td>
                                        <td>
                                            <cms:TagSelector runat="server" ID="tagSelectorElem" IsLiveSite="false" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </cms:UIPlaceHolder>
                    </ContentTemplate>
                </cms:CMSUpdatePanel>
            </asp:Panel>
        </asp:Panel>
    </asp:Panel>
    <cms:CMSButton ID="btnOk" runat="server" OnClick="btnOK_Click" CssClass="HiddenButton"
        EnableViewState="false" />
    <asp:Literal ID="ltlScript" runat="server" EnableViewState="false" />
    </form>
</body>
</html>
