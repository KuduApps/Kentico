<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_ProjectManagement_Controls_UI_Projectstatus_List" CodeFile="List.ascx.cs" %>
<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>

<script type="text/javascript">
    //<![CDATA[
    function EditProjectstatus(id) {
        window.location.replace('Edit.aspx?projectstatusId=' + id);
    }
    //]]>
</script>

<asp:Literal ID="ltlInfo" runat="server" EnableViewState="false"></asp:Literal>
<cms:LocalizedLabel runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
    Visible="false" />
<cms:UniGrid ID="gridElem" runat="server" GridName="~/CMSModules/ProjectManagement/Controls/UI/ProjectStatus/List.xml" OrderBy="StatusOrder"
    Columns="StatusID,StatusDisplayName,StatusColor,StatusEnabled,StatusIsNotStarted,StatusIcon,StatusIsFinished" />
