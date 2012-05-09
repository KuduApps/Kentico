<%@ Page Title="" Language="C#" MasterPageFile="~/CMSMasterPages/UI/EmptyPage.master"
    AutoEventWireup="true" CodeFile="Role_Edit_UI_Editor.aspx.cs" Inherits="CMSModules_Membership_Pages_Roles_Role_Edit_UI_Editor"
    Theme="Default" %>
<%@ Register Src="~/CMSModules/UIPersonalization/Controls/UIPersonalization.ascx"
    TagName="UIPersonalization" TagPrefix="cms" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <div style="position: absolute; width: 100%; height: 100%; top: 0px; bottom: 0px;">
        <cms:uipersonalization runat="server" id="editElem" CssClass="UIPersonalizationTreeClear" />
    </div>
</asp:Content>
