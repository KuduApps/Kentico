<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_AdminControls_Pages_ObjectRelationships" Theme="Default" MasterPageFile="~/CMSMasterPages/UI/Dialogs/ModalDialogPage.master" CodeFile="ObjectRelationships.aspx.cs" %>
<%@ Register Src="~/CMSModules/AdminControls/Controls/ObjectRelationships/ObjectRelationships.ascx" TagName="ObjectRelationships"
    TagPrefix="cms" %>
<asp:Content runat="server" ContentPlaceHolderID="plcContent" ID="cntContent">
    <cms:ObjectRelationships runat="server" ID="relElem" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="plcFooter" ID="cntFooter">
    <div class="FloatRight">
        <cms:LocalizedButton runat="server" ID="btnClose" OnClientClick="window.close(); return false;" ResourceString="General.Close" EnableViewState="false" CssClass="SubmitButton" />
    </div>
</asp:Content>
