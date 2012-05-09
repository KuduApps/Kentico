<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_OnlineMarketing_Controls_UI_MVTest_List" CodeFile="List.ascx.cs" %>
<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/OnlineMarketing/Controls/UI/MVTest/ListFilter.ascx" TagName="ListFilter"
    TagPrefix="cms" %>
    
<script type="text/javascript">
    //<![CDATA[
    function EditMvtest(id) {
        var redir = '<%=mEditPage%>?mvtestId=' + id;
        var node = '<%=NodeID%>';
        if (node != '0') {
            redir += '&nodeid=' + node;
        }
        
        if (parent.updateTabHeader) {
            parent.updateTabHeader();
        }


        window.location.replace(redir);
    }
    //]]>
</script>

<asp:PlaceHolder runat="server" ID="pnlFiler">
    <cms:ListFilter ID="ucFilter" runat="server" />
    <br />
</asp:PlaceHolder>

<cms:UniGrid ID="gridElem" runat="server" OrderBy="MVTestName" GridName="~/CMSModules/OnlineMarketing/Controls/UI/MVTest/List.xml"
    Columns="MVTestID,MVTestName,MVTestDisplayName,MVTestPage,MVTestConversions,MVTestCulture,MVTestOpenTo,MVTestOpenFrom,MVTestEnabled" />
