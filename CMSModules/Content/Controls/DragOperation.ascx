<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_Content_Controls_DragOperation"
    CodeFile="DragOperation.ascx.cs" %>
<%@ Register Src="~/CMSAdminControls/UI/PageElements/PageTitle.ascx" TagName="PageTitle"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/AsyncControl.ascx" TagName="AsyncControl" TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/AsyncBackground.ascx" TagName="AsyncBackground"
    TagPrefix="cms" %>
<asp:Panel runat="server" ID="pnlLog" Visible="false">
    <cms:AsyncBackground ID="backgroundElem" runat="server" />
    <div class="AsyncLogArea">
        <div>
            <asp:Panel ID="pnlAsyncBody" runat="server" CssClass="PageBody">
                <asp:Panel ID="pnlTitleAsync" runat="server" CssClass="PageHeader">
                    <cms:PageTitle ID="titleElemAsync" runat="server" />
                </asp:Panel>
                <asp:Panel ID="pnlCancel" runat="server" CssClass="PageHeaderLine">
                    <cms:LocalizedButton runat="server" ID="btnCancel" ResourceString="General.Cancel"
                        CssClass="SubmitButton" />
                </asp:Panel>
                <asp:Panel ID="pnlAsyncContent" runat="server" CssClass="PageContent">
                    <cms:AsyncControl ID="ctlAsync" runat="server" />
                </asp:Panel>
            </asp:Panel>
        </div>
    </div>
</asp:Panel>
<asp:Panel runat="server" ID="pnlContent" CssClass="PageContent">
    <asp:Label ID="lblError" runat="server" CssClass="ErrorLabel" EnableViewState="false"
        Visible="false" />
    <asp:Panel ID="pnlAction" runat="server" EnableViewState="false">
        <cms:LocalizedLabel ID="lblQuestion" runat="server" CssClass="ContentLabel" EnableViewState="false"
            Font-Bold="true" />
        <br />
        <br />
        <cms:LocalizedLabel ID="lblTarget" runat="server" CssClass="ContentLabel" EnableViewState="false" />
        <br />
        <br />
        <asp:PlaceHolder ID="plcCopyCheck" runat="server" EnableViewState="false" Visible="false">
            <div>
                <cms:LocalizedCheckBox ID="chkChild" runat="server" CssClass="ContentCheckbox" EnableViewState="false"
                    Checked="true" />
            </div>
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="plcCopyPerm" runat="server" EnableViewState="false">
            <div>
                <cms:LocalizedCheckBox ID="chkCopyPerm" runat="server" CssClass="ContentCheckbox"
                    EnableViewState="false" Checked="false" />
            </div>
        </asp:PlaceHolder>
        <br />
        <cms:LocalizedButton ID="btnOk" runat="server" CssClass="SubmitButton" OnClick="btnOK_Click"
            ResourceString="general.yes" EnableViewState="false" /><cms:LocalizedButton ID="btnNo"
                runat="server" CssClass="SubmitButton" ResourceString="general.no" EnableViewState="false" />
    </asp:Panel>
</asp:Panel>

<script type="text/javascript">
    //<![CDATA[
    // Refresh action
    function RefreshTree(nodeId, selectNodeId) {
        parent.frames['contenttree'].RefreshTree(nodeId, selectNodeId);
    }

    // Selects the node within the tree
    function SelectNode(nodeId) {
        parent.frames['contenttree'].SelectNode(nodeId, null);
    }

    // Display the document
    function DisplayDocument(nodeId) {
        parent.frames['contentmenu'].SelectNode(nodeId);
    }

    // Cancel the operation
    function CancelDragOperation() {
        DisplayDocument();
    }
    //]]>                        
</script>

<asp:Literal ID="ltlScript" runat="server" EnableViewState="false" />