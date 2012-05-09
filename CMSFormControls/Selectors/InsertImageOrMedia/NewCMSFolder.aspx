<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSFormControls_Selectors_InsertImageOrMedia_NewCMSFolder"
    Theme="Default" EnableEventValidation="false" MasterPageFile="~/CMSMasterPages/UI/Dialogs/ModalSimplePage.master" CodeFile="NewCMSFolder.aspx.cs" %>

<%@ Register Src="~/CMSModules/Content/Controls/Dialogs/LinkMediaSelector/NewFolder.ascx"
    TagName="NewFolder" TagPrefix="cms" %>
<asp:Content ID="folderEditContent" runat="server" ContentPlaceHolderID="plcContent">
    <div class="PageContent">
        <cms:NewFolder ID="createFolder" runat="server" />
    </div>
    <asp:Literal ID="ltlScript" runat="server"></asp:Literal>

    <script type="text/javascript" language="javascript">
        //<![CDATA[
        if (typeof (FocusFolderName) != 'undefined') {
            FocusFolderName();
        }
        //]]>
    </script>

</asp:Content>
