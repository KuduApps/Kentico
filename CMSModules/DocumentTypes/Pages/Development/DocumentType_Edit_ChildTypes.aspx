<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_DocumentTypes_Pages_Development_DocumentType_Edit_ChildTypes"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Document Type Edit - Child Types" CodeFile="DocumentType_Edit_ChildTypes.aspx.cs" %>
<%@ Register Src="~/CMSAdminControls/UI/UniSelector/UniSelector.ascx" TagName="UniSelector"
    TagPrefix="cms" %>

<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
        Visible="false" />
    <table width="70%">
        <tr>
            <td style="vertical-align: top; width: 30%; white-space: nowrap">
                <strong>
                    <cms:LocalizedLabel runat="server" ID="lblChildren" CssClass="InfoLabel" EnableViewState="false"
                        ResourceString="DocumentType.AllowedChildren" /></strong>
                <cms:UniSelector ID="uniSelector" runat="server" IsLiveSite="false" ObjectType="cms.documenttype"
                    SelectionMode="Multiple" />
            </td>
            <td style="width: 10%;">
                &nbsp;
            </td>
            <td style="vertical-align: top; width: 30%; white-space: nowrap">
                <strong>
                    <cms:LocalizedLabel runat="server" ID="lblParent" CssClass="InfoLabel" EnableViewState="false"
                        ResourceString="DocumentType.AllowedParents" /></strong>
                <cms:UniSelector ID="selParent" runat="server" IsLiveSite="false" ObjectType="cms.documenttype"
                    SelectionMode="Multiple" />
            </td>
        </tr>
    </table>
</asp:Content>
