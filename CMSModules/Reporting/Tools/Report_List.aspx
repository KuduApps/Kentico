<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Reporting_Tools_Report_List"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" CodeFile="Report_List.aspx.cs" %>
<%@ Register src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" tagname="UniGrid" tagprefix="cms" %>

<asp:Content ContentPlaceHolderID="plcContent" ID="content" runat="server">

    <script type="text/javascript">
        //<![CDATA[
        function EditReport(reportId) {
            parent.location.replace("Report_Edit.aspx?reportid=" + reportId + "&categoryid=" + "<%=categoryId%>");
            parent.parent.frames['reportcategorytree'].location.href = "ReportCategory_Tree.aspx?reportid=" + reportId;
        }
        //]]>
    </script>

    <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
        Visible="false" />
        <asp:Literal runat="server" ID="ltlScript" />
    <cms:UniGrid runat="server" ID="UniGrid" GridName="Report_List.xml" OrderBy="DisplayName"
        Columns="ObjectID, DisplayName" IsLiveSite="false" />
</asp:Content>
