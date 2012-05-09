<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_Widgets_FormControls_SelectZoneType" CodeFile="SelectZoneType.ascx.cs" %>
<div id="warningDiv" class="ErrorLabel" style="display: none">
    <%= ResHelper.GetString("widgets.zonetypechangewarning") %></div>

<script language="javascript" type="text/javascript">
    //<![CDATA[
    function ShowZoneTypeWarning() {
        var warningDiv = document.getElementById('warningDiv');
        if (warningDiv != null) {
            warningDiv.style.display = '';
        }
    }
    //]]>
</script>

<asp:RadioButtonList ID="rblOptions" runat="server" RepeatDirection="Vertical">
</asp:RadioButtonList>
