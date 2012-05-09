<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_EmailTemplates_Pages_Edit"
    Theme="Default" ValidateRequest="false" EnableEventValidation="false" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master"
    Title="Email template - Edit" CodeFile="Edit.aspx.cs" %>

<%@ Register Src="~/CMSModules/EmailTemplates/Controls/EmailTemplateEdit.ascx" TagName="EmailTemplateEdit"
    TagPrefix="cms" %>
<asp:Content ContentPlaceHolderID="plcContent" ID="content" runat="server">
    <cms:EmailTemplateEdit ID="emailTemplateEditElem" runat="server" />
</asp:Content>
