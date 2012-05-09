<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_PortalEngine_UI_PageTemplates_Scopes_PageTemplateScope_Edit"
    MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Page Template Edit - Scopes list"
    Theme="Default" CodeFile="PageTemplateScope_Edit.aspx.cs" %>

<%@ Register Src="~/CMSModules/Content/FormControls/Documents/selectsinglepath.ascx" TagName="selectpath" TagPrefix="cms" %>
<%@ Register Src="~/CMSFormControls/Classes/selectclass.ascx" TagName="SelectClass" TagPrefix="cms" %>
<%@ Register Src="~/CMSFormControls/Cultures/selectculture.ascx" TagName="SelectCulture" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/PortalEngine/FormControls/PageTemplates/PageTemplateScopeLevels.ascx"
    TagName="SelectLevels" TagPrefix="cms" %>
<asp:Content runat="server" ContentPlaceHolderID="plcContent">
    <cms:LocalizedLabel runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
        Visible="false" />
    <cms:LocalizedLabel runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
        Visible="false" />
    <table>
        <tr>
            <td>
                <cms:LocalizedLabel runat="server" ID="lblPath" ResourceString="template.scopes.path"
                    DisplayColon="true"></cms:LocalizedLabel>
            </td>
            <td>
                <cms:selectpath ID="pathElem" runat="server" IsLiveSite="false" />
            </td>
        </tr>
        <tr>
            <td>
                <cms:LocalizedLabel runat="server" ID="lblDocumentType" ResourceString="general.documenttype"
                    DisplayColon="true"></cms:LocalizedLabel>
            </td>
            <td>
                <cms:SelectClass runat="server" ID="classElem" IsLiveSite="false" />
            </td>
        </tr>
        <tr>
            <td>
                <cms:LocalizedLabel runat="server" ID="lblCulture" ResourceString="general.culture"
                    DisplayColon="true"></cms:LocalizedLabel>
            </td>
            <td>
                <cms:SelectCulture runat="server" ID="cultureElem" DisplayClearButton="false" IsLiveSite="false" />
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top; padding-top: 5px">
                <cms:LocalizedLabel runat="server" ID="lblLevels" ResourceString="template.scopes.levels"
                    DisplayColon="true"></cms:LocalizedLabel>
            </td>
            <td>
                <cms:SelectLevels runat="server" ID="levelElem" Level="10" IsLiveSite="false" />
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <cms:LocalizedButton runat="server" ID="btnOk" ResourceString="general.ok" CssClass="SubmitButton"
                    OnClick="btnOk_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
