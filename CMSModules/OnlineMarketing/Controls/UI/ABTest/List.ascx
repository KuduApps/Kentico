<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_OnlineMarketing_Controls_UI_AbTest_List"
    CodeFile="List.ascx.cs" %>
<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/OnlineMarketing/Controls/UI/ABTest/ListFilter.ascx"
    TagName="ListFilter" TagPrefix="cms" %>

<script type="text/javascript">
    //<![CDATA[
    function EditAbTest(id) {
        var red = 'Frameset.aspx?abTestId=' + id;
        var node = '<%=NodeID%>';
        if (node != '0') {
            red += '&nodeid=' + node;
        }

        if (parent.updateTabHeader) {
            parent.updateTabHeader();
        }

        window.location.replace(red);
    }
    //]]>
</script>

<asp:PlaceHolder runat="server" ID="pnlFiler">
    <cms:ListFilter ID="ucFilter" runat="server" />
    <br />
</asp:PlaceHolder>
<cms:UniGrid ID="gridElem" runat="server" GridName="~/CMSModules/OnlineMarketing/Controls/UI/AbTest/List.xml"
    OrderBy="ABTestName" Columns="ABTestID, ABTestDisplayName,ABTestConversions,ABTestOpenFrom,ABTestOpenTo,ABTestCulture,ABTestOriginalPage" />
