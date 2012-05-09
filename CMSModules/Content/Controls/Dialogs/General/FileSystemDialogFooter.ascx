<%@ Control Language="C#" AutoEventWireup="true"
            Inherits="CMSModules_Content_Controls_Dialogs_General_FileSystemDialogFooter" CodeFile="FileSystemDialogFooter.ascx.cs" %>

<asp:HiddenField ID="hdnSelected" runat="server" />
<div class="FloatRight">
    <cms:LocalizedButton ID="btnInsert" runat="server" ResourceString="dialogs.actions.insert"
        CssClass="SubmitButton" EnableViewState="false" /><cms:LocalizedButton ID="btnCancel"
            runat="server" ResourceString="dialogs.actions.cancel" CssClass="SubmitButton"
            EnableViewState="false" />
</div>
<asp:Literal ID="ltlScript" runat="server" EnableViewState="false" />
<asp:Button ID="btnHidden" runat="server" EnableViewState="false" Style="display: none;"
    OnClick="btnHidden_Click" />