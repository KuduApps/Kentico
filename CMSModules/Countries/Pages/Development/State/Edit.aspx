<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Countries_Pages_Development_State_Edit"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Country edit - State edit"
    CodeFile="Edit.aspx.cs" %>

<%@ Register Src="~/CMSModules/Countries/Controls/State/StateEdit.ascx"
    TagName="StateEdit" TagPrefix="cms" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <cms:StateEdit ID="stateEdit" runat="server" />
</asp:Content>
