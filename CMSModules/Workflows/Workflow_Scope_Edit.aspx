<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Workflows_Workflow_Scope_Edit"
    MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" EnableEventValidation="false"
    Theme="Default" Title="Workflows - Workflow Scopes" CodeFile="Workflow_Scope_Edit.aspx.cs" %>

<%@ Register Src="~/CMSModules/Content/FormControls/Documents/SelectSinglePath.ascx"
    TagName="SelectSinglePath" TagPrefix="cms" %>
<%@ Register Src="~/CMSFormControls/Cultures/SiteCultureSelector.ascx" TagName="SiteCultureSelector"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSFormControls/Classes/SelectClassNames.ascx" TagName="SelectClassNames"
    TagPrefix="cms" %>
<asp:Content ContentPlaceHolderID="plcContent" ID="content" runat="server">
    <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
        Visible="false" />
    <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
        Visible="false" />
    <asp:Panel ID="pnlForm" runat="server">
        <table>
            <tr>
                <td class="FieldLabel">
                    <cms:LocalizedLabel ID="lblStartingAliasPath" runat="server" EnableViewState="false"
                        ResourceString="Development-Workflow_Scope_Edit.StartingAliasPath" />
                </td>
                <td>
                    <cms:SelectSinglePath runat="server" ID="pathElem" IsLiveSite="false" />
                </td>
            </tr>
            <tr>
                <td class="FieldLabel">
                    <cms:LocalizedLabel ID="lblDocumentTypes" runat="server" EnableViewState="false"
                        ResourceString="Development-Workflow_Scope_Edit.DocumentType" />
                </td>
                <td>
                    <cms:SelectClassNames ID="selectClassNames" runat="server" IsLiveSite="false" DisplayClearButton="false" />
                </td>
            </tr>
            <asp:PlaceHolder ID="plcCulture" runat="server">
                <tr>
                    <td>
                        <cms:LocalizedLabel ID="lblCulture" runat="server" EnableViewState="false" ResourceString="general.culture"
                            DisplayColon="true" />
                    </td>
                    <td>
                        <cms:SiteCultureSelector runat="server" ID="cultureSelector" IsLiveSite="false" AddDefaultRecord="false" />
                    </td>
                </tr>
            </asp:PlaceHolder>
            <tr>
                <td>
                </td>
                <td>
                    <cms:LocalizedButton ID="btnOk" runat="server" OnClick="btnOK_Click" CssClass="SubmitButton"
                        ResourceString="general.ok" EnableViewState="false" />
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
