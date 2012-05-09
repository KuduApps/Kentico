<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_Content_Controls_Dialogs_General_DialogFooter" CodeFile="DialogFooter.ascx.cs" %>

<asp:HiddenField ID="hdnSelected" runat="server" />
<asp:HiddenField ID="hdnAnchors" runat="server" />
<asp:HiddenField ID="hdnIds" runat="server" />
<div class="FloatRight">
    <cms:LocalizedButton ID="btnInsert" runat="server" ResourceString="dialogs.actions.insert"
        CssClass="SubmitButton" EnableViewState="false" OnClientClick="if ((window.parent.frames['insertContent'])&&(window.parent.frames['insertContent'].insertItem)) {window.parent.frames['insertContent'].insertItem();} return false;" /><cms:LocalizedButton ID="btnCancel"
            runat="server" ResourceString="dialogs.actions.cancel" CssClass="SubmitButton"
            EnableViewState="false" OnClientClick="window.parent.close(); return false;" />
</div>
<asp:Literal ID="ltlScript" runat="server" EnableViewState="false" />
<cms:CMSButton ID="btnHidden" runat="server" EnableViewState="false" Style="display: none;"
    OnClick="btnHidden_Click" />