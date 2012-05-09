<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_BizForms_Tools_BizForm_Edit_Data" EnableEventValidation="false"
    MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="BizForm Data" Theme="Default" CodeFile="BizForm_Edit_Data.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <cms:UniGrid runat="server" ID="gridData" GridName="~/CMSModules/BizForms/Tools/BizForm_Edit_Data.xml" IsLiveSite="false" />
</asp:Content>
