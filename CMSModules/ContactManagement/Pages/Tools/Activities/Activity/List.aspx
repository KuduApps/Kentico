<%@ Page Language="C#" AutoEventWireup="true" CodeFile="List.aspx.cs"
    MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Activity list"
    Inherits="CMSModules_ContactManagement_Pages_Tools_Activities_Activity_List" Theme="Default" EnableEventValidation="false" %>
    
<%@ Register Src="~/CMSModules/ContactManagement/Controls/UI/Activity/List.ascx" TagName="ActivityList" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/ContactManagement/Controls/UI/Activity/Filter.ascx" TagName="Filter" TagPrefix="cms" %>
<%@ Register TagPrefix="cms" TagName="HeaderActions" Src="~/CMSAdminControls/UI/PageElements/HeaderActions.ascx" %>

<asp:Content ID="cntBefore" runat="server" ContentPlaceHolderID="plcBeforeContent">
    <asp:Panel runat="server" ID="pnlDis" CssClass="PageHeaderLine" Visible="false">
        <cms:LocalizedLabel runat="server" ID="lblDis" EnableViewState="false" ResourceString="om.activity.disabled" />
    </asp:Panel>
</asp:Content>
<asp:Content ID="cntActions" runat="server" ContentPlaceHolderID="plcActions">
    <cms:CMSUpdatePanel ID="pnlActons" runat="server">
        <ContentTemplate>
            <div class="LeftAlign">
                <cms:HeaderActions ID="hdrActions" runat="server" />
            </div>
            <cms:LocalizedLabel ID="lblWarnNew" runat="server" ResourceString="om.choosesite"
                EnableViewState="false" Visible="false" CssClass="ActionsInfoLabel" />
            <div class="ClearBoth">
            </div>
        </ContentTemplate>
    </cms:CMSUpdatePanel>
</asp:Content>  
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:Label ID="lblInfo" runat="server" EnableViewState="false" Visible="false" />
    <cms:Filter runat="server" ID="fltElem" />
    <cms:ActivityList ID="listElem" runat="server" IsLiveSite="false" ShowContactNameColumn="true" ShowRemoveButton="true" />
</asp:Content>