<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSFormControls_LiveSelectors_SpellCheck"
    Theme="Default" MasterPageFile="~/CMSMasterPages/LiveSite/Dialogs/ModalDialogPage.master"
    ValidateRequest="false" ClassName="PopUpSpell" Title="Spell Checker" CodeFile="SpellCheck.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/SpellChecker/SpellCheck.ascx" TagName="SpellCheck"
    TagPrefix="cms" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <div class="PageContent">
        <cms:SpellCheck ID="spellCheck" runat="server" IsLiveSite="false" EnableViewState="true" />
    </div>
</asp:Content>
