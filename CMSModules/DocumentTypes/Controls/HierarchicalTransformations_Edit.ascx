<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_DocumentTypes_Controls_HierarchicalTransformations_Edit" CodeFile="HierarchicalTransformations_Edit.ascx.cs" %>
<%@ Register Src="~/CMSFormControls/Classes/SelectTransformation.ascx" TagName="SelectTransformation"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSFormControls/Classes/SelectClassNames.ascx" TagName="ClassSelector"
    TagPrefix="cms" %>
<cms:LocalizedLabel runat="server" id="lblError" cssclass="ErrorLabel" visible="false" />
<cms:localizedlabel runat="server" id="lblInfo" visible="false" />
<table>
    <tr>
        <td >
            <cms:localizedlabel runat="server" resourcestring="documenttype_edit_transformation_edit.transformtype" />
        </td>
        <td>
            <asp:DropDownList runat="server" ID="drpTemplateType" CssClass="DropDownField" />
        </td>
    </tr>
    <tr>
        <td>
            <cms:localizedlabel id="lblDocTypes" runat="server" resourcestring="development.documenttypes" />
        </td>
        <td>
            <cms:classselector runat="server" id="ucClassSelector" islivesite="false" SiteID="0" />
        </td>
    </tr>
    <tr>
        <td>
            <cms:localizedlabel id="lblLevel" runat="server" resourcestring="development.level" />
        </td>
        <td>
            <cms:CMSTextBox ID="txtLevel" runat="server" CssClass="TextBoxField" />
        </td>
    </tr>
    <tr>
        <td>
            <cms:localizedlabel id="lblName" runat="server" resourcestring="documenttype_edit_transformation_edit.transformname" />
        </td>
        <td>
            <cms:selecttransformation id="ucTransformations" runat="server" islivesite="false" EditWindowName="EditLevel2" />
        </td>
    </tr>    
    <tr>
        <td>
            &nbsp;
        </td>
        <td>
            <cms:localizedbutton id="btnOK" runat="server" onclick="btnOK_Click" cssclass="SubmitButton"
                resourcestring="general.ok" />
        </td>
    </tr>
</table>
