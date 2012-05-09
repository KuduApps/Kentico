<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Content_CMSDesk_Edit_Edit"
    ValidateRequest="false" Theme="Default" EnableEventValidation="false" CodeFile="Edit.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>Content - Edit</title>
    <style type="text/css">
        body
        {
            margin: 0;
            padding: 0;
        }
    </style>
    <asp:Literal ID="ltlSpellScript" runat="server" EnableViewState="false" />
</head>
<body class="<%=mBodyClass%>">
    <form id="form1" runat="server">
    <asp:PlaceHolder runat="server" ID="plcManagers">
        <ajaxToolkit:ToolkitScriptManager ID="scriptManager" runat="server" />
    </asp:PlaceHolder>
    <input type="hidden" name="saveChanges" id="saveChanges" value="0" />
    <div id="CMSHeaderDiv">
        <div id="CKToolbar">
        </div>
        <asp:Panel runat="server" ID="pnlWorkflowInfo" CssClass="PageManagerWorkflowInfo"
            EnableViewState="false">
            <asp:Label ID="lblWorkflowInfo" runat="server" CssClass="WorkflowInfo" EnableViewState="false" />
        </asp:Panel>
    </div>
    <input type="hidden" id="hidAnother" name="hidAnother" value="" />
    <asp:Panel runat="server" ID="pnlBody" CssClass="PageBody">
        <asp:Panel runat="server" ID="pnlContent" CssClass="PageContent ContentEditArea">
            <cms:CMSForm runat="server" ID="formElem" Visible="false" HtmlAreaToolbarLocation="Out:CKToolbar"
                ShowOkButton="false" IsLiveSite="false" ShortID="f" />
            <br class="ClearBoth" />
            <br />
            <asp:PlaceHolder ID="plcNewProduct" runat="server">
                <asp:CheckBox ID="chkCreateProduct" runat="server" CssClass="EditingFormLabel" />
                <asp:Panel ID="pnlNewProduct" runat="server" />
            </asp:PlaceHolder>
        </asp:Panel>
    </asp:Panel>
    <cms:CMSButton ID="btnSave" runat="server" CssClass="HiddenButton" OnClick="btnSave_Click"
        EnableViewState="false" />
    <cms:CMSButton ID="btnApprove" runat="server" CssClass="HiddenButton" EnableViewState="false"
        OnClick="btnApprove_Click" />
    <cms:CMSButton ID="btnReject" runat="server" CssClass="HiddenButton" EnableViewState="false"
        OnClick="btnReject_Click" />
    <cms:CMSButton ID="btnCheckIn" runat="server" CssClass="HiddenButton" EnableViewState="false"
        OnClick="btnCheckIn_Click" />
    <cms:CMSButton ID="btnRefresh" runat="server" CssClass="HiddenButton" EnableViewState="false"
        OnClick="btnRefresh_Click" />
    <div id="CMSFooterDiv">
        <div id="CKFooter">
        </div>
    </div>
    </form>
</body>
</html>
