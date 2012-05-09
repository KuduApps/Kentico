<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CMSMasterPages/UI/Dialogs/ModalDialogPage.master"
    EnableEventValidation="false" Theme="Default" CodeFile="User_Edit_Add_Item_Dialog.aspx.cs"
    Inherits="CMSModules_Membership_Pages_Users_User_Edit_Add_Item_Dialog" %>

<%@ Register Src="~/CMSAdminControls/UI/UniSelector/Controls/SelectionDialog.ascx"
    TagName="SelectionDialog" TagPrefix="cms" %>
<asp:Content ID="cntContent" ContentPlaceHolderID="plcContent" runat="Server">
    <cms:SelectionDialog runat="server" ID="selectionDialog" IsLiveSite="false" />
    <div class="UserEditAddItemDialogDateTimePickerDiv">
        <asp:Label ID="lblError" runat="server" CssClass="ErrorLabel Hidden" EnableViewState="false" />
        <asp:Panel runat="server" ID="pnlDateTime">
            <div class="CalendarPanelUniselectorDialog">
                <cms:LocalizedLabel runat="server" ID="lblValidTo" ResourceString="membership.validto"
                    DisplayColon="true" Width="100" />
                <cms:DateTimePicker ID="ucDateTime" runat="server" EditTime="true" />
            </div>
            <asp:Panel ID="pnlSendNotification" runat="server" class="CalendarPanelUniselectorDialog"
                Visible="false">
                <cms:LocalizedLabel runat="server" ResourceString="membership.sendnotification" DisplayColon="true"
                    Width="100" />
                <asp:CheckBox ID="chkSendNotification" runat="server" Checked="false" />
            </asp:Panel>
        </asp:Panel>
    </div>
</asp:Content>
<asp:Content ID="cntFooter" ContentPlaceHolderID="plcFooter" runat="server">
    <div class="FloatRight">
        <cms:LocalizedButton ID="btnOk" runat="server" CssClass="SubmitButton" EnableViewState="False" /><cms:LocalizedButton
            ID="btnCancel" runat="server" CssClass="SubmitButton" EnableViewState="False" />
    </div>
</asp:Content>
