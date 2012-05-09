<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DeleteDocument.ascx.cs"
    Inherits="CMSModules_DocumentLibrary_Controls_DeleteDocument" %>
<div class="DialogPageContent DialogScrollableContent">
    <div class="PageBody">
        <asp:PlaceHolder ID="plcConfirmation" runat="server">
            <cms:LocalizedLabel ID="lblConfirmation" runat="server" EnableViewState="false" CssClass="ContentLabel" /><br />
            <cms:LocalizedCheckBox ID="chkAllCultures" runat="server" ResourceString="contentdelete.allcultures" />
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="plcMessage" runat="server" Visible="false">
            <cms:LocalizedLabel ID="lblInfo" runat="server" EnableViewState="false" CssClass="InfoLabel" />
            <cms:LocalizedLabel ID="lblError" runat="server" EnableViewState="false" CssClass="ErrorLabel" />
        </asp:PlaceHolder>
    </div>
</div>
<div class="PageFooterLine">
    <div class="Buttons">
        <cms:LocalizedButton ID="btnDelete" runat="server" EnableViewState="false" ResourceString="general.delete"
            OnClick="btnDelete_Click" CssClass="SubmitButton" />
        <cms:LocalizedButton ID="btnCancel" runat="server" CssClass="SubmitButton" CausesValidation="false" /></div>
    <div class="ClearBoth">
        &nbsp;</div>
</div>
