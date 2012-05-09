<%@ Page Language="C#" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" AutoEventWireup="true"
    Inherits="CMSModules_Blogs_Tools_Blogs_Blogs_List" Title="Blogs - Blogs list"
    Theme="Default" CodeFile="Blogs_Blogs_List.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<asp:Content ContentPlaceHolderID="plcSiteSelector" runat="Server">
    <table>
        <tr>
            <td>
                <cms:LocalizedLabel ID="lblBlogs" runat="server" EnableViewState="false" DisplayColon="true"
                    ResourceString="blog.blogs" />
            </td>
            <td>
                <cms:LocalizedDropDownList ID="drpBlogs" runat="server" AutoPostBack="true" CssClass="DropDownField" />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ContentPlaceHolderID="plcContent" runat="Server">
     <cms:UniGrid ID="gridBlogs" runat="server" GridName="Blog_List.xml" IsLiveSite="false" ExportFileName="cms_blog"/>

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
