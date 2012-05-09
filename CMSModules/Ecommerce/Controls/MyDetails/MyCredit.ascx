<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_Ecommerce_Controls_MyDetails_MyCredit" CodeFile="MyCredit.ascx.cs" %>
<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<div class="MyCredit">
    <div style="padding-bottom: 5px;" class="TotalCredit">
        <asp:Label ID="lblCredit" runat="server" EnableViewState="false" />
        <asp:Label ID="lblCreditValue" runat="server" EnableViewState="false" />
    </div>
    <cms:UniGrid runat="server" ID="gridCreditEvents" GridName="~/CMSModules/Ecommerce/Controls/MyDetails/MyCredit.xml"
        Columns="EventID,EventDate,EventName,EventCreditChange,EventDescription" OrderBy="EventDate DESC" />
</div>
