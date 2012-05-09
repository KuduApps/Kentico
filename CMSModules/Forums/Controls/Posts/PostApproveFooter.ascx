<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_Forums_Controls_Posts_PostApproveFooter" CodeFile="PostApproveFooter.ascx.cs" %>

<asp:literal id="ltrScript" runat="server"></asp:literal>
<div class="AlignRight">
    <cms:CMSButton runat="server" ID="btnApprove" EnableViewState="false" CssClass="SubmitButton"
        OnClick="btnApprove_Click" />
    <cms:CMSButton runat="server" ID="btnDelete" EnableViewState="false" CssClass="SubmitButton"
        OnClick="btnDelete_Click" />
    <cms:CMSButton runat="server" ID="btnCancel" EnableViewState="false" CssClass="SubmitButton" />
</div>
