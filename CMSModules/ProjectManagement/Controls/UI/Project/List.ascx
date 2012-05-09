<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_ProjectManagement_Controls_UI_Project_List"
    CodeFile="List.ascx.cs" %>
<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
    Visible="false" />
<cms:UniGrid ID="gridElem" runat="server" GridName="~/CMSModules/ProjectManagement/Controls/UI/Project/List.xml"
    OrderBy="ProjectDisplayName" />
