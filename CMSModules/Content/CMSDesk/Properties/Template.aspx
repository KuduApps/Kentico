<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Content_CMSDesk_Properties_Template"
    Theme="Default" CodeFile="Template.aspx.cs" %>
    
<%@ Register Src="~/CMSAdminControls/UI/PageElements/Help.ascx" TagName="Help" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/PortalEngine/FormControls/PageTemplates/PageTemplateLevels.ascx"
    TagName="PageTemplateLevel" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/Content/Controls/editmenu.ascx" TagName="editmenu"
    TagPrefix="cms" %>
<%@ Register TagPrefix="cms" Namespace="CMS.UIControls" Assembly="CMS.UIControls" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>Properties - Template</title>
    <style type="text/css">
        body
        {
            margin: 0px;
            padding: 0px;
            height: 100%;
        }
        .MenuItemEdit, .MenuItemEditSmall
        {
            padding-right: 0px !important;
        }
        .Gecko .visibility
        {
            visibility: hidden;
        }
        .IE7 .visibility
        {
            display: none;
        }
    </style>
</head>
<body class="VerticalTabsBody <%=mBodyClass%>">
    <form id="form1" runat="server">
    <asp:Panel ID="pnlBody" runat="server" CssClass="VerticalTabsPageBody">
        <asp:Panel ID="pnlTab" runat="server" CssClass="VerticalTabsPageContent">
            <asp:Panel runat="server" ID="pnlMenu" CssClass="ContentEditMenu" EnableViewState="false">
                <table width="100%">
                    <tr>
                        <td class="MenuItemEdit">
                            <asp:LinkButton ID="lnkSaveDoc" runat="server" OnClick="lnkSave_Click">
                                <cms:CMSImage ID="imgSaveDoc" runat="server" />
                                <%=mSaveDoc%>
                            </asp:LinkButton>
                        </td>
                        <td class="TextRight">
                            <cms:Help ID="helpElem" runat="server" TopicName="template" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="pnlContent" runat="server" CssClass="PageContent PropertiesPanel">
                <asp:Label ID="lblInfo" runat="server" EnableViewState="false" CssClass="InfoLabel"
                    Visible="false" />
                <asp:Panel runat="server" ID="pnlActions" CssClass="NodePermissions">
                    <input type="hidden" id="isPortal" name="isPortal" value="true" />
                    <input type="hidden" id="isReusable" name="isReusable" value="" />
                    <input type="hidden" id="isAdHoc" name="isAdHoc" value="" />
                    <input type="hidden" id="SelectedTemplateId" name="SelectedTemplateId" value="" />
                    <input type="hidden" id="InheritedTemplateId" name="InheritedTemplateId" value="" />
                    <input type="hidden" id="Saved" name="Saved" value="false" />
                    <input type="hidden" id="TemplateDisplayName" name="TemplateDisplayName" value="" />
                    <input type="hidden" id="TemplateDescription" name="TemplateDescription" value="" />
                    <input type="hidden" id="TemplateCategory" name="TemplateCategory" value="" />
                    <input type="hidden" id="TextTemplate" name="TextTemplate" value="" />
                    <asp:Literal ID="ltlPreInitScript" runat="server" EnableViewState="false" />
                    <asp:Literal ID="ltlInitScript" runat="server" EnableViewState="false" />
                    <div style="padding: 5px;">
                        <cms:CMSTextBox ID="txtTemplate" runat="server" ReadOnly="True" CssClass="SelectorTextBox"
                            Width="350" /><cms:CMSButton ID="btnSelect" runat="server" CssClass="ContentButton"
                                EnableViewState="false" />
                    </div>
                    <div style="padding: 5px;">
                        <table cellspacing="0" cellpadding="0">
                            <cms:UIPlaceHolder ID="plcUISave" runat="server" ModuleName="CMS.Content" ElementName="Template.SaveAsNew">
                                <tr>
                                    <td>
                                        <asp:LinkButton ID="btnSave" runat="server" CssClass="MenuItemEdit" EnableViewState="false">
                                            <cms:CMSImage ID="imgSave" runat="server" />
                                            <%=mSave%>
                                        </asp:LinkButton>
                                    </td>
                                </tr>
                            </cms:UIPlaceHolder>
                            <cms:UIPlaceHolder ID="plcUIInherit" runat="server" ModuleName="CMS.Content" ElementName="Template.Inherit">
                                <tr>
                                    <td>
                                        <asp:LinkButton ID="btnInherit" runat="server" CssClass="MenuItemEdit" OnClick="btnInherit_Click"
                                            EnableViewState="false">
                                            <cms:CMSImage ID="imgInherit" runat="server" />
                                            <%=mInherit%>
                                        </asp:LinkButton>
                                    </td>
                                </tr>
                            </cms:UIPlaceHolder>
                            <cms:UIPlaceHolder ID="plcUIClone" runat="server" ModuleName="CMS.Content" ElementName="Template.CloneAdHoc">
                                <tr>
                                    <td>
                                        <asp:LinkButton ID="btnClone" runat="server" CssClass="MenuItemEdit" OnClick="btnClone_Click"
                                            EnableViewState="false">
                                            <cms:CMSImage ID="imgClone" runat="server" />
                                            <%=mClone%>
                                        </asp:LinkButton>
                                    </td>
                                </tr>
                            </cms:UIPlaceHolder>
                            <cms:UIPlaceHolder ID="plcUIEdit" runat="server" ModuleName="CMS.Content" ElementName="Template.EditProperties">
                                <tr>
                                    <td>
                                        <asp:LinkButton ID="btnEditTemplateProperties" runat="server" CssClass="MenuItemEdit"
                                            OnClick="btnClone_Click" EnableViewState="false">
                                            <cms:CMSImage ID="imgEditTemplateProperties" runat="server" />
                                            <%=mEditTemplateProperties%>
                                        </asp:LinkButton>
                                    </td>
                                </tr>
                            </cms:UIPlaceHolder>
                        </table>
                    </div>
                </asp:Panel>
                <br />
                <cms:UIPlaceHolder ID="plcUILevels" runat="server" ModuleName="CMS.Content" ElementName="Template.InheritContent">
                    <asp:Panel runat="server" ID="pnlInherits" CssClass="NodePermissions">
                        <cms:PageTemplateLevel runat="server" ID="inheritElem"></cms:PageTemplateLevel>
                    </asp:Panel>
                </cms:UIPlaceHolder>
            </asp:Panel>
        </asp:Panel>
    </asp:Panel>
    <asp:Literal ID="ltlElemScript" runat="server" EnableViewState="false" />
    <asp:Literal ID="ltlScript" runat="server" EnableViewState="false" />
    <cms:LocalizedHidden ID="constAdhocPage" runat="server" Value="{$PageProperties.AdHocPage$}" />
    </form>
</body>
</html>