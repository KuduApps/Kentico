<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_Blogs_Controls_NewBlog" CodeFile="NewBlog.ascx.cs" %>
<asp:Label ID="lblInfo" runat="server" CssClass="InfoLabel" EnableViewState="false"
    Visible="false" />
<asp:Label ID="lblError" runat="server" CssClass="ErrorLabel" EnableViewState="false"
    Visible="false" />
<cms:CMSRequiredFieldValidator ID="rfvName" runat="server" CssClass="ErrorLabel" ControlToValidate="txtName"
    Display="Static" ValidationGroup="NewBlog" EnableViewState="false" />
<table>
    <tr>
        <td>
            <asp:Label ID="lblName" runat="server" AssociatedControlID="txtName" EnableViewState="false" />
        </td>
        <td>
            <cms:CMSTextBox ID="txtName" runat="server" MaxLength="100" CssClass="TextBoxField"
                EnableViewState="false" />
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblDescription" runat="server" AssociatedControlID="txtDescription"
                EnableViewState="false" />
        </td>
        <td>
            <cms:CMSTextBox ID="txtDescription" runat="server" CssClass="TextAreaField" TextMode="MultiLine"
                EnableViewState="false" />
        </td>
    </tr>
    <tr>
        <td>
        </td>
        <td>
            <cms:CMSButton ID="btnOk" runat="server" CssClass="SubmitButton" ValidationGroup="NewBlog"
                EnableViewState="false" />
        </td>
    </tr>
</table>
