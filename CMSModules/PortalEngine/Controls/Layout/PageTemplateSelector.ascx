<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_PortalEngine_Controls_Layout_PageTemplateSelector" CodeFile="PageTemplateSelector.ascx.cs" %>
<%@ Register Src="~/CMSModules/PortalEngine/Controls/Layout/PageTemplateFlatSelector.ascx"
    TagName="PageTemplateFlatSelector" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/PortalEngine/Controls/Layout/PageTemplateTree.ascx"
    TagName="PageTemplateTree" TagPrefix="cms" %>
<table class="SelectorTable" cellpadding="0" cellspacing="0">
    <tr>
        <td>
            <div class="SelectorTree">
                <cms:PageTemplateTree EnableViewState="false" UsePostBack="false" ID="treeElem" runat="server"
                    SelectPageTemplates="false" />
            </div>
        </td>
        <td class="SelectorBorder">
            <div class="SelectorBorderGlue">
            </div>
        </td>
        <td class="ItemSelectorArea">
            <div class="ItemSelector">
                <cms:PageTemplateFlatSelector ID="flatElem" runat="server" />
            </div>
        </td>
    </tr>
</table>
