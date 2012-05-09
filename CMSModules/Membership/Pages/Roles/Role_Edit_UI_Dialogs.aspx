<%@ Page Title="" Language="C#" MasterPageFile="~/CMSMasterPages/UI/EmptyPage.master" AutoEventWireup="true" CodeFile="Role_Edit_UI_Dialogs.aspx.cs" 
Inherits="CMSModules_Membership_Pages_Roles_Role_Edit_UI_Dialogs" Theme="Default" %>

<%@ Register Src="~/CMSModules/UIPersonalization/Controls/UIPersonalization.ascx" TagName="UIPersonalization" TagPrefix="cms" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <div style="width: 100%; height: 100%;">
        <cms:UIPersonalization runat="server" ID="editElem" CssClass="UIPersonalizationTreeSmall" />
    </div>
</asp:Content>

