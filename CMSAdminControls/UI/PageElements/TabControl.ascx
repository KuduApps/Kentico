<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSAdminControls_UI_PageElements_TabControl" CodeFile="TabControl.ascx.cs" %>

<cms:BasicTabControl ID="BasicTabControlMenu" runat="server" />

<script type="text/javascript">
//<![CDATA[
    function showSelectedTab(tabId)
    {
        document.getElementById(tabId).style.display = 'block';
    }
    function hideAllTabs()
    {
        for (i = 0; i < basicTabControlMenuTabs.length; i++)
        {
            document.getElementById(basicTabControlMenuTabs[i]).style.display = 'none';
        }
    }
//]]>
</script>

