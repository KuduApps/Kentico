<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_Blogs_Controls_CommentFilter" CodeFile="CommentFilter.ascx.cs" %>
<%@ Register src="~/CMSAdminControls/UI/UniSelector/UniSelector.ascx" tagname="UniSelector" tagprefix="cms" %>
    
<table>
    <tr>
        <td>
            <table>
                <tr>
                    <td class="FieldLabel">
                        <cms:LocalizedLabel ID="lblBlog" runat="server" EnableViewState="false" DisplayColon="true"
                            ResourceString="blog.comments.blog" />
                    </td>
                    <td>
                        <cms:UniSelector ID="uniSelector" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="FieldLabel">
                        <cms:LocalizedLabel ID="lblUserName" runat="server" EnableViewState="false" DisplayColon="true"
                            ResourceString="general.username" />
                    </td>
                    <td>
                        <cms:CMSTextBox ID="txtUserName" runat="server" CssClass="TextBoxField" EnableViewState="false" />
                    </td>
                </tr>
                <tr>
                    <td class="FieldLabel">
                        <cms:LocalizedLabel ID="lblComment" runat="server" EnableViewState="false" DisplayColon="true"
                            ResourceString="blog.comments.comment" />
                    </td>
                    <td>
                        <cms:CMSTextBox ID="txtComment" runat="server" CssClass="TextBoxField" EnableViewState="false" />
                    </td>
                </tr>
                <tr>
                    <td class="FieldLabel">
                        <cms:LocalizedLabel ID="lblApproved" runat="server" EnableViewState="false" DisplayColon="true"
                            ResourceString="blog.comments.approved" />
                    </td>
                    <td>
                        <asp:DropDownList ID="drpApproved" runat="server" CssClass="DropDownField" />
                    </td>
                </tr>
                <tr>
                    <td class="FieldLabel">
                        <cms:LocalizedLabel ID="lblSpam" runat="server" EnableViewState="false" DisplayColon="true"
                            ResourceString="blog.comments.spam" />
                    </td>
                    <td>
                        <asp:DropDownList ID="drpSpam" runat="server" CssClass="DropDownField" />
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <cms:CMSButton ID="btnFilter" runat="server" OnClick="btnFilter_Click" CssClass="ContentButton"
                            EnableViewState="false" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
    </tr>
</table>
