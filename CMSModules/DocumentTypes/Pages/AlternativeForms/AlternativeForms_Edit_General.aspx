<%@ Page Language="C#" AutoEventWireup="true"
    MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Alternative forms - General properties"
    Inherits="CMSModules_DocumentTypes_Pages_AlternativeForms_AlternativeForms_Edit_General"
    Theme="Default" CodeFile="AlternativeForms_Edit_General.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/NameForm.ascx" TagName="NameForm" TagPrefix="uc1" %>

<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
<asp:Label ID="lblInfo" runat="server" CssClass="InfoLabel" Visible="false" EnableViewState="false" />
<asp:Label ID="lblError" runat="server" CssClass="ErrorLabel" Visible="false" EnableViewState="false" />
<uc1:NameForm ID="nameElem" runat="server" />
</asp:Content>
