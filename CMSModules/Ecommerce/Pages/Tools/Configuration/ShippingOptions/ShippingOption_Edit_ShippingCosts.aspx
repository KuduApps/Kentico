<%@ Page Title="" Language="C#" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" AutoEventWireup="true" CodeFile="ShippingOption_Edit_ShippingCosts.aspx.cs" Inherits="CMSModules_Ecommerce_Pages_Tools_Configuration_ShippingOptions_ShippingOption_Edit_ShippingCosts" Theme="Default" %>
<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<asp:Content ContentPlaceHolderID="plcContent" ID="content" runat="server">
    <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
        Visible="false" />
    <cms:UniGrid runat="server" ID="gridElem" GridName="ShippingOption_Edit_ShippingCosts.xml"
        IsLiveSite="false" OrderBy="ShippingCostMinWeight" />

    <script type="text/javascript">
        //<![CDATA[
        // Refreshes current page when properties are changed in modal dialog window
        function RefreshPage() {
            window.location.replace(window.location.href);
        }
        //]]>
    </script>
</asp:Content>
