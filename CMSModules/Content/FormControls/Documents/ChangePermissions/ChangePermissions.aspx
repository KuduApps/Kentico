<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChangePermissions.aspx.cs"
    Inherits="CMSModules_Content_FormControls_Documents_ChangePermissions_ChangePermissions"
    MasterPageFile="~/CMSMasterPages/UI/Dialogs/ModalDialogPage.master" Theme="Default" %>

<%@ Register Src="~/CMSModules/Content/Controls/Security.ascx" TagName="Security"
    TagPrefix="cms" %>
<asp:Content ID="cntContent" ContentPlaceHolderID="plcContent" runat="Server">

    <script type="text/javascript">
        //<![CDATA[   
        function Close() {
            window.close();
        }
        //]]>
    </script>

    <asp:Panel ID="pnlContent" CssClass="PageContent" runat="server">
        <cms:Security ID="securityElem" runat="server" IsLiveSite="false" />
        <cms:LocalizedLabel ID="lblInfo" runat="server" EnableViewState="false" Visible="false" />
    </asp:Panel>
</asp:Content>
<asp:Content ID="plcFooter" ContentPlaceHolderID="plcFooter" runat="server">
    <cms:CMSUpdatePanel ID="pnlUpdateButtons" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="FloatRight">
                <cms:LocalizedButton ID="btnOk" runat="server" CssClass="SubmitButton" ResourceString="general.apply" /><cms:LocalizedButton
                    ID="btnCancel" runat="server" CssClass="SubmitButton" ResourceString="general.close"
                    OnClientClick="Close();return false;" />
            </div>
        </ContentTemplate>
    </cms:CMSUpdatePanel>
</asp:Content>
