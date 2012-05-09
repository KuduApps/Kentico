<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master"
    Title="MVT test list" Inherits="CMSModules_OnlineMarketing_Pages_Tools_MVTest_List"
    Theme="Default" CodeFile="List.aspx.cs" %>

<%@ Register Src="~/CMSModules/OnlineMarketing/Controls/UI/MVTest/List.ascx" TagName="MVTestList"
    TagPrefix="cms" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <div>
        <cms:MVTestList ID="listElem" runat="server" IsLiveSite="false" />
    </div>
</asp:Content>
