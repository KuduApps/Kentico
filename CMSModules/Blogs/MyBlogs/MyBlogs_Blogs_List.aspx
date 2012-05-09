<%@ Page Language="C#" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" AutoEventWireup="true"
    Inherits="CMSModules_Blogs_MyBlogs_MyBlogs_Blogs_List" Title="Blogs - List" Theme="Default"
    CodeFile="MyBlogs_Blogs_List.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<asp:Content ID="contentElem" ContentPlaceHolderID="plcContent" runat="Server">
    <asp:Label ID="lblError" runat="server" CssClass="ErrorLabel" Visible="false" EnableViewState="false" />
    <cms:UniGrid ID="gridBlogs" runat="server" GridName="~/CMSModules/Blogs/Tools/Blog_List.xml"
        IsLiveSite="false" ExportFileName="cms_blog" />

    <script type="text/javascript">
        //<![CDATA[
        // Open blog edit page in CMSDesk
        function EditBlog(nodeId, culture) {
            if (nodeId != 0) {
                parent.parent.parent.location.href = "../../../CMSDesk/default.aspx?section=content&action=edit&nodeid=" + nodeId + "&culture=" + culture;
            }
        }
        //]]>
    </script>

    <asp:Literal ID="ltlScript" runat="server" EnableViewState="false" />
</asp:Content>
