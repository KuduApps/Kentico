<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Content_CMSDesk_Properties_General"
    Theme="Default" MaintainScrollPositionOnPostback="true" CodeFile="General.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/PageElements/Help.ascx" TagName="Help" TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/ContentRating/Controls/Stars.ascx" TagName="Rating"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSFormControls/SelectCssStylesheet.ascx" TagName="SelectCssStylesheet"
    TagPrefix="cms" %>
<%@ Register TagPrefix="cms" Namespace="CMS.UIControls" Assembly="CMS.UIControls" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>Properties - General</title>
    <style type="text/css">
        body
        {
            margin: 0px;
            padding: 0px;
            height: 100%;
            width: 100%;
        }
        .MenuItemEdit
        {
            padding-right: 0px !important;
        }
    </style>
</head>
<body class="VerticalTabsBody <%=mBodyClass%>">
    <form id="form1" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="scriptMan" runat="server" />
    <asp:Panel runat="server" ID="pnlBody" CssClass="VerticalTabsPageBody CMSDeskProperties">
        <asp:Panel runat="server" ID="pnlTab" CssClass="VerticalTabsPageContent">
            <asp:Panel runat="server" ID="pnlMenu" CssClass="ContentEditMenu" EnableViewState="false">
                <table width="100%">
                    <tr>
                        <td class="MenuItemEdit">
                            <asp:LinkButton ID="lnkSave" runat="server" OnClick="lnkSave_Click">
                                <asp:Image ID="imgSave" runat="server" />
                                <%=mSave%>
                            </asp:LinkButton>
                        </td>
                        <td class="TextRight">
                            <cms:Help ID="helpElem" runat="server" TopicName="general" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="pnlContent" runat="server" CssClass="PageContent PropertiesPanel">
                <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
                    Visible="false" /><asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
                        Visible="false" />
                <cms:UIPlaceHolder ID="pnlUIDesign" runat="server" ModuleName="CMS.Content" ElementName="General.Design">
                    <asp:Panel ID="pnlDesign" runat="server" CssClass="NodePermissions" GroupingText="Design">
                        <table>
                            <tr>
                                <td class="FieldLabel" style="width: 115px;">
                                    <asp:Label ID="lblCssStyle" runat="server" CssClass="ContentLabel" EnableViewState="false" />
                                </td>
                                <td>
                                    <cms:SelectCssStylesheet IsLiveSite="false" ID="ctrlSiteSelectStyleSheet" runat="server" />
                                    <asp:CheckBox runat="server" ID="chkCssStyle" AutoPostBack="true" OnCheckedChanged="chkCssStyle_CheckedChanged" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <br />
                </cms:UIPlaceHolder>
                <cms:UIPlaceHolder ID="pnlUIOther" runat="server" ModuleName="CMS.Content" ElementName="General.OtherProperties">
                    <asp:Panel ID="pnlOther" runat="server" CssClass="NodePermissions" GroupingText="Document properties">
                        <table>
                            <tr>
                                <td class="FieldLabel" style="width: 115px;">
                                    <asp:Label ID="lblNameTitle" runat="server" EnableViewState="false" />
                                </td>
                                <td>
                                    <asp:Label ID="lblName" runat="server" EnableViewState="false" />
                                </td>
                            </tr>
                            <tr>
                                <td class="FieldLabel">
                                    <asp:Label ID="lblTypeTitle" runat="server" EnableViewState="false" />
                                </td>
                                <td>
                                    <asp:Label ID="lblType" runat="server" EnableViewState="false" />
                                </td>
                            </tr>
                            <tr>
                                <td class="FieldLabel">
                                    <asp:Label ID="lblCreatedByTitle" runat="server" EnableViewState="false" />
                                </td>
                                <td>
                                    <asp:Label ID="lblCreatedBy" runat="server" EnableViewState="false" />
                                </td>
                            </tr>
                            <tr>
                                <td class="FieldLabel">
                                    <asp:Label ID="lblCreatedTitle" runat="server" EnableViewState="false" />
                                </td>
                                <td>
                                    <asp:Label ID="lblCreated" runat="server" EnableViewState="false" />
                                </td>
                            </tr>
                            <tr>
                                <td class="FieldLabel">
                                    <asp:Label ID="lblLastModifiedByTitle" runat="server" EnableViewState="false" />
                                </td>
                                <td>
                                    <asp:Label ID="lblLastModifiedBy" runat="server" EnableViewState="false" />
                                </td>
                            </tr>
                            <tr>
                                <td class="FieldLabel">
                                    <asp:Label ID="lblLastModifiedTitle" runat="server" EnableViewState="false" />
                                </td>
                                <td>
                                    <asp:Label ID="lblLastModified" runat="server" EnableViewState="false" />
                                </td>
                            </tr>
                            <asp:PlaceHolder ID="plcRating" runat="server">
                                <tr>
                                    <td class="FieldLabel">
                                        <cms:LocalizedLabel ID="lblContentRating" runat="server" ResourceString="GeneralProperties.ContentRating"
                                            DisplayColon="true" EnableViewState="false" />
                                    </td>
                                    <td class="Uploader">
                                        <table style="width: 100%" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td class="RatingControlCell">
                                                    <cms:Rating ID="ratingControl" runat="server" />
                                                </td>
                                                <td>
                                                    <cms:CMSButton ID="btnResetRating" runat="server" CssClass="ContentButton" OnClick="btnResetRating_Click"
                                                        EnableViewState="false" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:Label ID="lblContentRatingResult" runat="server" EnableViewState="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </asp:PlaceHolder>
                            <tr>
                                <td class="FieldLabel">
                                    <asp:Label ID="lblNodeIDTitle" runat="server" EnableViewState="false" />
                                </td>
                                <td>
                                    <asp:Label ID="lblNodeID" runat="server" EnableViewState="false" />
                                </td>
                            </tr>
                            <tr>
                                <td class="FieldLabel">
                                    <asp:Label ID="lblDocIDTitle" runat="server" EnableViewState="false" />
                                </td>
                                <td>
                                    <asp:Label ID="lblDocID" runat="server" EnableViewState="false" />
                                </td>
                            </tr>
                            <tr>
                                <td class="FieldLabel">
                                    <asp:Label ID="lblGUIDTitle" runat="server" EnableViewState="false" />
                                </td>
                                <td>
                                    <asp:Label ID="lblGUID" runat="server" EnableViewState="false" />
                                </td>
                            </tr>
                            <tr>
                                <td class="FieldLabel">
                                    <asp:Label ID="lblDocGUIDTitle" runat="server" EnableViewState="false" />
                                </td>
                                <td>
                                    <asp:Label ID="lblDocGUID" runat="server" EnableViewState="false" />
                                </td>
                            </tr>
                            <tr>
                                <td class="FieldLabel">
                                    <asp:Label ID="lblAliasPathTitle" runat="server" EnableViewState="false" />
                                </td>
                                <td>
                                    <asp:Label ID="lblAliasPath" runat="server" EnableViewState="false" />
                                </td>
                            </tr>
                            <tr>
                                <td class="FieldLabel">
                                    <asp:Label ID="lblCultureTitle" runat="server" EnableViewState="false" />
                                </td>
                                <td>
                                    <asp:Label ID="lblCulture" runat="server" EnableViewState="false" />
                                </td>
                            </tr>
                            <tr>
                                <td class="FieldLabel">
                                    <asp:Label ID="lblNamePathTitle" runat="server" EnableViewState="false" />
                                </td>
                                <td>
                                    <asp:Label ID="lblNamePath" runat="server" EnableViewState="false" />
                                </td>
                            </tr>
                            <tr>
                                <td class="FieldLabel">
                                    <asp:Label ID="lblLiveURLTitle" runat="server" EnableViewState="false" />
                                </td>
                                <td>
                                    <asp:HyperLink ID="lnkLiveURL" runat="server" Target="_blank" EnableViewState="false" />
                                </td>
                            </tr>
                            <asp:PlaceHolder ID="plcPreview" runat="server" Visible="false">
                                <tr>
                                    <td class="FieldLabel" style="vertical-align:bottom;">
                                        <asp:Label ID="lblPreviewURLTitle" runat="server" EnableViewState="false" />
                                    </td>
                                    <td>
                                        <cms:CMSUpdatePanel ID="pnlUpdatePreviewUrl" runat="server" UpdateMode="conditional">
                                            <ContentTemplate>
                                                <cms:LocalizedLabel ID="lblNoPreviewGuid" runat="server" EnableViewState="false"
                                                    Visible="false" />
                                                <asp:HyperLink ID="lnkPreviewURL" runat="server" Target="_blank" EnableViewState="false" />
                                                <asp:ImageButton runat="server" ID="btnResetPreviewGuid" EnableViewState="false" />
                                                <asp:Label ID="lblPreviewLink" runat="server" Visible="false" />
                                            </ContentTemplate>
                                        </cms:CMSUpdatePanel>
                                    </td>
                                </tr>
                            </asp:PlaceHolder>
                            <tr>
                                <td class="FieldLabel">
                                    <asp:Label ID="lblPublishedTitle" runat="server" EnableViewState="false" />
                                </td>
                                <td>
                                    <asp:Label ID="lblPublished" runat="server" EnableViewState="false" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <br />
                </cms:UIPlaceHolder>
                <cms:UIPlaceHolder ID="pnlUIOwner" runat="server" ModuleName="CMS.Content" ElementName="General.Owner">
                    <asp:Panel ID="pnlOwner" runat="server" CssClass="NodePermissions" GroupingText="Owner">
                        <table>
                            <tr>
                                <td class="FieldLabel" style="width: 115px;">
                                    <asp:Label ID="lblOwnerTitle" runat="server" EnableViewState="false" />
                                </td>
                                <td>
                                    <asp:Label ID="lblOwner" runat="server" EnableViewState="false" />
                                    <asp:PlaceHolder runat="server" ID="plcUsrOwner"></asp:PlaceHolder>
                                </td>
                            </tr>
                            <asp:PlaceHolder ID="plcOwnerGroup" runat="server" Visible="false" />
                        </table>
                    </asp:Panel>
                    <br />
                </cms:UIPlaceHolder>
                <cms:UIPlaceHolder ID="pnlUICache" runat="server" ModuleName="CMS.Content" ElementName="General.Cache">
                    <asp:Panel ID="pnlCache" runat="server" CssClass="NodePermissions" GroupingText="Cache">
                        <cms:CMSUpdatePanel ID="ucCache" runat="server">
                            <ContentTemplate>
                                <table>
                                    <tr>
                                        <td colspan="2">
                                            <asp:Label runat="server" ID="lblCacheInfo" EnableViewState="false" Visible="false" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:RadioButton GroupName="useCache" ID="radInherit" runat="server" AutoPostBack="True"
                                                OnCheckedChanged="radInherit_CheckedChanged" />
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="FieldLabel" style="width: 115px;">
                                            <asp:RadioButton GroupName="useCache" ID="radYes" runat="server" AutoPostBack="True"
                                                OnCheckedChanged="radInherit_CheckedChanged" />
                                        </td>
                                        <td>
                                            <table style="width: 100%; margin-top: 2px;" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td style="text-align: left; width: 100px;">
                                                        <asp:Label ID="lblCacheMinutes" runat="server" EnableViewState="False" />
                                                    </td>
                                                    <td>
                                                        <cms:CMSTextBox ID="txtCacheMinutes" runat="server" CssClass="ShortTextBox" MaxLength="8" /><cms:LocalizedButton
                                                            runat="server" ID="btnClear" ResourceString="GeneralProperties.ClearCache" CssClass="LongButton"
                                                            OnClick="btnClear_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:RadioButton GroupName="useCache" ID="radNo" runat="server" AutoPostBack="True"
                                                OnCheckedChanged="radInherit_CheckedChanged" />
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </cms:CMSUpdatePanel>
                    </asp:Panel>
                    <br />
                </cms:UIPlaceHolder>
                <cms:UIPlaceHolder ID="pnlUISearch" runat="server" ModuleName="CMS.Content" ElementName="General.Search">
                    <asp:Panel ID="pnlSearch" runat="server" GroupingText="Search" CssClass="NodePermissions"
                        EnableViewState="false">
                        <table>
                            <tr>
                                <td class="FieldLabel" colspan="2">
                                    <asp:CheckBox ID="chkExcludeFromSearch" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <br />
                </cms:UIPlaceHolder>
                <cms:UIPlaceHolder ID="pnlUIOnlineMarketing" runat="server" ModuleName="CMS.Content"
                    ElementName="General.OnlineMarketing">
                    <asp:Panel ID="pnlOnlineMarketing" runat="server" CssClass="NodePermissions" EnableViewState="false">
                        <table>
                            <tr>
                                <td style="vertical-align: top;">
                                    <cms:LocalizedLabel ID="lblLogActivity" runat="server" EnableViewState="false" ResourceString="om.logpagevisitactivity"
                                        DisplayColon="true" />
                                </td>
                                <td>
                                    <asp:CheckBox runat="server" ID="chkLogPageVisit" />
                                    <div>
                                        <cms:LocalizedCheckBox runat="server" ID="chkPageVisitInherit" ResourceString="om.propertiesinherit"
                                            AutoPostBack="true" OnCheckedChanged="chkPageVisitInherit_CheckedChanged" />
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <br />
                </cms:UIPlaceHolder>
                <cms:UIPlaceHolder ID="pnlUIAdvanced" runat="server" ModuleName="CMS.Content" ElementName="General.Advanced">
                    <asp:Panel ID="pnlAdvanced" runat="server" GroupingText="Advanced" CssClass="NodePermissions"
                        EnableViewState="false">
                        <table cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:LinkButton ID="lnkEditableContent" runat="server" EnableViewState="false" CssClass="MenuItemEdit">
                                        <asp:Image ID="imgEditableContent" runat="server" />
                                        <%=mEditableContent%>
                                    </asp:LinkButton>
                                </td>
                            </tr>
                            <asp:PlaceHolder ID="plcAdHocBoards" runat="server" Visible="false">
                                <tr>
                                    <td>
                                        <asp:LinkButton ID="lnkMessageBoards" runat="server" EnableViewState="false" CssClass="MenuItemEdit">
                                            <asp:Image ID="imgMessageBoards" runat="server" />
                                            <%=mMessageBoards%>
                                        </asp:LinkButton>
                                    </td>
                                </tr>
                            </asp:PlaceHolder>
                            <asp:PlaceHolder ID="plcAdHocForums" runat="server" Visible="false">
                                <tr>
                                    <td>
                                        <asp:LinkButton ID="lnkForums" runat="server" EnableViewState="false" CssClass="MenuItemEdit">
                                            <asp:Image ID="imgForums" runat="server" />
                                            <%=mForums%>
                                        </asp:LinkButton>
                                    </td>
                                </tr>
                            </asp:PlaceHolder>
                        </table>
                    </asp:Panel>
                </cms:UIPlaceHolder>
            </asp:Panel>
        </asp:Panel>
    </asp:Panel>
    <asp:Literal runat="server" ID="ltlScript" EnableViewState="false" />
    </form>
</body>
</html>
