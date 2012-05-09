<%@ Control Language="C#" AutoEventWireup="true" CodeFile="~/CMSModules/EventManager/Controls/EventAttendees.ascx.cs"
    Inherits="CMSModules_EventManager_Controls_EventAttendees" %>
<%@ Register Src="~/CMSModules/EventManager/Controls/EventAttendees_Edit.ascx" TagName="AttendeesEdit"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/EventManager/Controls/EventAttendees_List.ascx" TagName="AttendeesList"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/UI/PageElements/HeaderActions.ascx" TagName="HeaderActions"
    TagPrefix="cms" %>

<asp:Literal ID="ltlScript" runat="server" />
<asp:Panel runat="server" ID="pnlList">
    <div class="PageHeaderLine">
        <cms:HeaderActions ID="actionsElem" runat="server" />
    </div>
    <br />
    <cms:AttendeesList ID="attendeesList" runat="server" />
</asp:Panel>
<asp:Panel ID="pnlEdit" runat="server">
    <div>
        <asp:Panel ID="pnlHeader" runat="server" CssClass="PageTitleBreadCrumbsPadding">
            <div class="HeaderTitleBreadcrumbs">
                <asp:Panel ID="pnlNewHeader" runat="server">
                    <asp:LinkButton ID="lnkEditBack" runat="server" CausesValidation="false" />
                    <asp:Label ID="lblEditBack" runat="server" />
                    <asp:Label ID="lblEditNew" runat="server" /><br />
                </asp:Panel>
            </div>
        </asp:Panel>
        <asp:HiddenField ID="hdnState" runat="server" />
        <cms:AttendeesEdit runat="server" ID="attendeeEdit" />
    </div>
</asp:Panel>
