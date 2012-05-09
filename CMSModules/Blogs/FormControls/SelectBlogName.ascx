<%@ Control Language="C#" AutoEventWireup="true" CodeFile="~/CMSModules/Blogs/FormControls/SelectBlogName.ascx.cs" Inherits="CMSModules_Blogs_FormControls_SelectBlogName" %>
<%@ Register src="~/CMSAdminControls/UI/UniSelector/UniSelector.ascx" tagname="UniSelector" tagprefix="cms" %>

<cms:CMSUpdatePanel ID="pnlUpdate" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <cms:UniSelector runat="server" ID="usBlogs" ObjectType="CMS.Blog" 
                SelectionMode="SingleDropDownList" IsLiveSite="false" AllowEmpty="false" />
    </ContentTemplate>    
</cms:CMSUpdatePanel>        
        
        