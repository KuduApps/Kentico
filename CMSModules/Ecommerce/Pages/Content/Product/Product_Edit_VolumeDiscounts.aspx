<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Ecommerce_Pages_Content_Product_Product_Edit_VolumeDiscounts" Theme="Default" CodeFile="Product_Edit_VolumeDiscounts.aspx.cs" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" %>

<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/UI/PageElements/PageTitle.ascx" TagName="PageTitle"
    TagPrefix="cms" %>
<asp:Content ContentPlaceHolderID="plcContent" ID="content" runat="server">
    <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
        Visible="false" />
    <cms:UniGrid runat="server" ID="gridElem" GridName="Product_Edit_VolumeDiscount_List.xml"
        IsLiveSite="false" OrderBy="VolumeDiscountID" />

    <script type="text/javascript">
        //<![CDATA[
        // Refreshes current page when volume discount level properties are changed in modal dialog window
        function RefreshPage() {
            window.location.replace(window.location.href);
        }
        //]]>
    </script>

</asp:Content>
