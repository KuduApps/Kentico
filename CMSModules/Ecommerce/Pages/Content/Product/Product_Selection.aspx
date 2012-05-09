<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Ecommerce_Pages_Content_Product_Product_Selection"
    Theme="Default" ValidateRequest="false" CodeFile="Product_Selection.aspx.cs"
    MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Content - Product" %>

<%@ Register TagPrefix="cms" Namespace="CMS.UIControls" Assembly="CMS.UIControls" %>
<%@ Register TagPrefix="cms" TagName="ProductEdit" Src="~/CMSModules/Ecommerce/Controls/UI/ProductEdit.ascx" %>
<%@ Register TagPrefix="cms" TagName="SKUSelector" Src="~/CMSModules/Ecommerce/FormControls/SKUSelector.ascx" %>
<asp:Content ContentPlaceHolderID="plcContent" runat="server">

    <script type="text/javascript">
        //<![CDATA[
        parent.frames['contenteditheader'].SetTabsContext('product');
        //]]>
    </script>

    <ajaxToolkit:ToolkitScriptManager runat="server" ID="scriptManager" />
    <asp:Label runat="server" ID="lblGlobalError" CssClass="ErrorLabel" EnableViewState="false"
        Visible="false" />
    <asp:CheckBox ID="chkMarkDocAsProd" runat="server" OnCheckedChanged="chkMarkDocAsProd_CheckedChanged"
        AutoPostBack="true" />
    <asp:Panel ID="pnlProduct" runat="server" CssClass="PageContent">
        <asp:RadioButton ID="radSelect" runat="server" GroupName="product" OnCheckedChanged="rad_CheckedChanged"
            AutoPostBack="true" />
        <br />
        <asp:Panel ID="pnlSelect" runat="server" Style="margin-left: 20px;">
            <br />
            <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
                Visible="false" />
            <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
                Visible="false" />
            <cms:SKUSelector runat="server" ID="skuElem" IsLiveSite="false" />
        </asp:Panel>
        <br />
        <div>
            <asp:RadioButton ID="radCreate" runat="server" GroupName="product" OnCheckedChanged="rad_CheckedChanged"
                AutoPostBack="true" />
        </div>
        <br />
        <div>
            <asp:RadioButton ID="radCreateGlobal" runat="server" GroupName="product" OnCheckedChanged="rad_CheckedChanged"
                AutoPostBack="true" />
        </div>
        <asp:Panel ID="pnlNew" runat="server" Style="margin-left: 20px; padding-bottom: 10px;">
            <br />
            <cms:ProductEdit ID="ctrlProduct" runat="server" Visible="false" OnProductSaved="ctrlProduct_ProductSaved" />
        </asp:Panel>
    </asp:Panel>
</asp:Content>
