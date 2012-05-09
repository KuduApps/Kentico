<%@ Page Language="C#" AutoEventWireup="true" Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master"
    CodeFile="HierarchicalTransformations_Transformations.aspx.cs" Inherits="CMSModules_DocumentTypes_Pages_Development_HierarchicalTransformations_Transformations" %>

<%@ Register Src="~/CMSModules/DocumentTypes/Controls/HierarchicalTransformations_List.ascx"
    TagName="HierarchicalTransfList" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/DocumentTypes/Controls/HierarchicalTransformations_Edit.ascx"
    TagName="HierarchicalTransfNew" TagPrefix="cms" %>
<asp:Content ID="cntControls" runat="server" ContentPlaceHolderID="plcControls">
    <table style="padding-left:5px" >
        <tr>
            <td class="HierarchicalTransformationFilterLabelWidth">
                <cms:LocalizedLabel runat="server"
                    ID="lblTemplateType" ResourceString="documenttype_edit_transformation_edit.transformtype" />
                    </td><td>
                <asp:DropDownList runat="server" ID="drpTransformations" CssClass="DropDownField"  AutoPostBack="true"/>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:Panel ID="pnlContainer" runat="server">
        <cms:HierarchicalTransfList runat="server" ID="ucTransf" />
        <cms:HierarchicalTransfNew runat="server" ID="ucNewTransf" Visible="false" />
    </asp:Panel>
</asp:Content>
