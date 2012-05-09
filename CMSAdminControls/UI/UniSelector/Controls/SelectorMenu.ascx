<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSAdminControls_UI_UniSelector_Controls_SelectorMenu"
    CodeFile="SelectorMenu.ascx.cs" %>
    
<asp:Panel runat="server" ID="pnlSelectorMenu">
    <cms:ContextMenuItem runat="server" ID="iRemoveAll" />
</asp:Panel>
<script type="text/javascript">
    //<![CDATA[
    function US_ContextRemoveAll(clientId) {
        setTimeout('US_RemoveAll_' + clientId + '();');
    }
    //]]>
</script>
