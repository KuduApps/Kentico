<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_OnlineMarketing_Controls_UI_ABVariant_List"
    CodeFile="List.ascx.cs" %>
<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>

<script type="text/javascript">
    //<![CDATA[
    function EditVariant(id) {
        var red = 'Edit.aspx?abTestID=<%=TestID%>&variantId=' + id;
        var node = '<%=nodeID%>';
        if (node != '0') {
            red += '&nodeid=' + node;
        }
        window.location.replace(red);
    }
    //]]>
</script>

<cms:UniGrid ID="gridElem" runat="server" GridName="~/CMSModules/OnlineMarketing/Controls/UI/ABVariant/List.xml"
    OrderBy="ABVariantDisplayName" Columns="ABVariantID,ABVariantDisplayName,ABVariantPath,ABVariantConversions" />
