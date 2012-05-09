<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Reporting_Tools_Ecommerce_Reports_Tree"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/Tree.master" CodeFile="Reports_Tree.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/UIProfiles/UIMenu.ascx" TagName="UIMenu"
    TagPrefix="cms" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcTree">
    <cms:UIMenu runat="server" ID="treeElem" ModuleName="CMS.Ecommerce" ElementName="ECReports" />

    <script type="text/javascript">
        //<![CDATA[
        var currentNode = document.getElementById('treeSelectedNode');

        function ShowDesktopContent(nodeElem, contentUrl) {
            if ((currentNode != null) && (nodeElem != null)) {
                currentNode.className = 'ContentTreeItem';
            }

            parent.frames['ecommreports'].location.href = contentUrl;

            if (nodeElem != null) {
                currentNode = nodeElem;
                currentNode.className = 'ContentTreeSelectedItem';
            }
        }
        //]]>    
    </script>

    <asp:Literal runat="server" ID="ltlNodePreselectScript" />
</asp:Content>
