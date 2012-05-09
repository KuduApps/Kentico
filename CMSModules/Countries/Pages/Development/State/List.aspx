<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Countries_Pages_Development_State_List"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Country edit - State list"
    CodeFile="List.aspx.cs" %>

<%@ Register Src="~/CMSModules/Countries/Controls/State/StateList.ascx"
    TagName="StateList" TagPrefix="cms" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <cms:StateList ID="stateList" runat="server" />
</asp:Content>
