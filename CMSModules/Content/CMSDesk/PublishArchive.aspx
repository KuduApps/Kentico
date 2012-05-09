<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master"
    Inherits="CMSModules_Content_CMSDesk_PublishArchive" Title="Publishes or archives multiple documents"
    ValidateRequest="false" Theme="Default" CodeFile="PublishArchive.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/PageElements/PageTitle.ascx" TagName="PageTitle"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/AsyncControl.ascx" TagName="AsyncControl" TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/AsyncBackground.ascx" TagName="AsyncBackground"
    TagPrefix="cms" %>
<asp:Content ContentPlaceHolderID="plcBeforeBody" runat="server" ID="cntBeforeBody">
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
</asp:Content>
<asp:Content ID="cntContent" ContentPlaceHolderID="plcContent" runat="server" EnableViewState="false">
    <asp:Panel runat="server" ID="pnlContent" CssClass="PageContent" EnableViewState="false">
        <asp:Label ID="lblError" runat="server" CssClass="ErrorLabel" EnableViewState="false"
            Visible="false" />
        <asp:Panel ID="pnlPublish" runat="server" EnableViewState="false">
            <cms:LocalizedLabel ID="lblQuestion" runat="server" CssClass="ContentLabel" EnableViewState="false"
                Font-Bold="true" />
            <br />
            <br id="brSeparator" runat="server" />
            <asp:Panel ID="pnlDocList" runat="server" Visible="false" CssClass="ScrollableList"
                EnableViewState="false">
                <asp:Label ID="lblDocuments" runat="server" CssClass="ContentLabel" EnableViewState="false" />
            </asp:Panel>
            <br />
            <asp:PlaceHolder ID="plcCheck" runat="server" EnableViewState="false">
                <asp:PlaceHolder ID="plcAllCultures" runat="server">
                    <cms:LocalizedCheckBox ID="chkAllCultures" runat="server" CssClass="ContentCheckbox"
                        EnableViewState="false" Checked="true" />
                </asp:PlaceHolder>
                <cms:LocalizedCheckBox ID="chkUnderlying" runat="server" CssClass="ContentCheckbox"
                    EnableViewState="false" Checked="true" />
                <asp:PlaceHolder ID="plcUndoCheckOut" runat="server">
                    <cms:LocalizedCheckBox ID="chkUndoCheckOut" runat="server" CssClass="ContentCheckbox"
                        EnableViewState="false" ResourceString="content.undocheckedoutdocs" />
                </asp:PlaceHolder>
            </asp:PlaceHolder>
            <br />
            <br />
            <cms:LocalizedButton ID="btnOk" runat="server" CssClass="SubmitButton" OnClick="btnOK_Click"
                ResourceString="general.yes" EnableViewState="false" /><cms:LocalizedButton ID="btnNo"
                    runat="server" CssClass="SubmitButton" OnClick="btnNo_Click" ResourceString="general.no"
                    EnableViewState="false" />
        </asp:Panel>
    </asp:Panel>

    <script type="text/javascript">
        //<![CDATA[

        // Display the document
        function DisplayDocument(nodeId) {
            if (parent != null) {
                if (parent.parent != null) {
                    if (parent.parent.frames['contentmenu'] != null) {
                        if (parent.parent.frames['contentmenu'].SelectNode != null) {
                            parent.parent.frames['contentmenu'].SelectNode(nodeId);
                        }
                    }
                    RefreshTree(nodeId);
                }
            }
        }

        function RefreshTree(nodeId) {
            if (parent != null) {
                if (parent.parent != null) {
                    if (parent.parent.frames['contenttree'] != null) {
                        if (parent.parent.frames['contenttree'].RefreshNode != null) {
                            parent.parent.frames['contenttree'].RefreshNode(nodeId, nodeId);
                        }
                    }
                }
            }

        }
        //]]>                        
    </script>

    <asp:Literal runat="server" ID="ltlScript" EnableViewState="false" />
</asp:Content>
