<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_ContactManagement_Controls_UI_Activity_Edit"
    CodeFile="Edit.ascx.cs" %>
<%@ Register Src="~/CMSFormControls/Basic/TextBoxControl.ascx" TagName="TextBoxControl"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/ContactManagement/FormControls/ActivityTypeSelector.ascx"
    TagName="TypeSelector" TagPrefix="cms" %>
<%@ Register Src="~/CMSFormControls/Sites/SiteSelector.ascx" TagName="SiteSelector"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/UI/UniSelector/UniSelector.ascx" TagName="UniSelector"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/ContactManagement/FormControls/ContactSelector.ascx"
    TagName="ContactSelector" TagPrefix="cms" %>
<asp:Label ID="lblError" runat="server" CssClass="ErrorLabel" EnableViewState="false"
    Visible="false" />
<%@ Register Src="~/CMSFormControls/Basic/HtmlAreaControl.ascx" TagName="HtmlAreaControl"
    TagPrefix="cms" %>

<script type="text/javascript">
    function InsertHTML(htmlString, ckClientID) {
        // Get the editor instance that we want to interact with.
        var oEditor = oEditor = window.CKEDITOR.instances[ckClientID];
        // Check the active editing mode.
        if (oEditor != null) {
            // Check the active editing mode.
            if (oEditor.mode == 'wysiwyg') {
                // Insert the desired HTML.
                oEditor.focus();
                oEditor.insertHtml(htmlString);
            }
        }
        return false;
    }

    function AddStamp() {
        ckClientID = "<%=txtComment.CurrentEditor.ClientID%>";
        InsertHTML('<div><%=AddStampValue%></div>', ckClientID);
    }
</script>

<cms:UIForm runat="server" ID="EditForm" ObjectType="om.activity" IsLiveSite="false"
    DefaultFieldLayout="Inline" RedirectUrlAfterCreate="~/CMSModules/ContactManagement/Pages/Tools/Activities/Activity/List.aspx?saved=1&siteid={?siteid?}&issitemanager={?issitemanager?}">
    <LayoutTemplate>
        <table>
            <tr>
                <cms:FormField runat="server" ID="fContact">
                    <td class="ActivityDetailsLabel">
                        <cms:LocalizedLabel ID="lblContact" runat="server" EnableViewState="false" ResourceString="om.activity.contact"
                            DisplayColon="true" />
                    </td>
                    <td>
                        <cms:ContactSelector ID="ucContact" runat="server" />
                    </td>
                </cms:FormField>
            </tr>
            <tr>
                <cms:FormField runat="server" ID="fType" Field="ActivityType">
                    <td class="ActivityDetailsLabel">
                        <cms:LocalizedLabel ID="lblType" runat="server" EnableViewState="false" ResourceString="om.activity.type"
                            DisplayColon="true" />
                    </td>
                    <td>
                        <cms:TypeSelector ID="tsType" runat="server" ShowCustomActivitiesOnly="true" ShowAll="false" />
                    </td>
                </cms:FormField>
            </tr>
            <tr>
                <cms:FormField runat="server" ID="fTitle" Field="ActivityTitle">
                    <td class="ActivityDetailsLabel">
                        <cms:LocalizedLabel ID="lblTitle" runat="server" EnableViewState="false" ResourceString="om.activity.title"
                            DisplayColon="true" />
                    </td>
                    <td>
                        <cms:TextBoxControl ID="txtTitle" runat="server" MaxLength="250" />
                    </td>
                </cms:FormField>
            </tr>
            <tr>
                <cms:FormField runat="server" ID="fValue" Field="ActivityValue">
                    <td class="ActivityDetailsLabel">
                        <cms:LocalizedLabel ID="lblValue" runat="server" EnableViewState="false" ResourceString="om.activity.value"
                            DisplayColon="true" />
                    </td>
                    <td>
                        <cms:TextBoxControl ID="txtValue" runat="server" MaxLength="250" />
                    </td>
                </cms:FormField>
            </tr>
            <tr>
                <cms:FormField runat="server" ID="fURL" Field="ActivityURL">
                    <td class="ActivityDetailsLabel">
                        <cms:LocalizedLabel ID="lblURL" runat="server" EnableViewState="false" ResourceString="om.activity.url"
                            DisplayColon="true" />
                    </td>
                    <td>
                        <cms:TextBoxControl ID="txtURL" runat="server" MaxLength="2048" />
                    </td>
                </cms:FormField>
            </tr>
            <asp:PlaceHolder runat="server" ID="plcSite" Visible="false">
                <tr>
                    <cms:FormField runat="server" ID="fSite">
                        <td class="ActivityDetailsLabel">
                            <cms:LocalizedLabel ID="lblSite" runat="server" EnableViewState="false" ResourceString="om.activity.site"
                                DisplayColon="true" />
                        </td>
                        <td>
                            <cms:SiteSelector ID="ucSite" runat="server" AllowAll="false" AllowEmpty="false"
                                PostbackOnDropDownChange="true" />
                        </td>
                    </cms:FormField>
                </tr>
            </asp:PlaceHolder>
            <tr>
                <cms:FormField runat="server" ID="fCampaign">
                    <td class="ActivityDetailsLabel">
                        <cms:LocalizedLabel ID="lblCampaign" runat="server" EnableViewState="false" ResourceString="om.activity.campaign"
                            DisplayColon="true" />
                    </td>
                    <td>
                        <cms:UniSelector ID="ucCampaign" ShortID="us" ObjectType="analytics.campaign" ResourcePrefix="selectcamp"
                            runat="server" ReturnColumnName="CampaignName" SelectionMode="SingleDropDownList"
                            IsLiveSite="false" />
                    </td>
                </cms:FormField>
            </tr>
            <tr>
                <cms:FormField runat="server" ID="fCreated" Field="ActivityCreated">
                    <td class="ActivityDetailsLabel">
                        <cms:LocalizedLabel ID="lblCreated" runat="server" EnableViewState="false" ResourceString="om.activity.created"
                            DisplayColon="true" />
                    </td>
                    <td>
                        <cms:DateTimePicker ID="dtCreated" runat="server" />
                    </td>
                </cms:FormField>
            </tr>
            <tr>
                <cms:FormField runat="server" ID="fComment">
                    <td class="ActivityDetailsLabel" style="vertical-align: top;">
                        <cms:LocalizedLabel ID="lblComment" runat="server" EnableViewState="false" ResourceString="om.activity.comment"
                            DisplayColon="true" />
                    </td>
                    <td style="width: 580px;">
                        <cms:HtmlAreaControl runat="server" ID="txtComment" ToolbarSet="Basic" />
                        <div class="MiddleButton">
                            <cms:LocalizedButton ID="btnStamp" runat="server" ResourceString="om.account.stamp"
                                CssClass="ContentButton" EnableViewState="false" OnClientClick="AddStamp(); return false;" /></div>
                    </td>
                </cms:FormField>
            </tr>
        </table>
    </LayoutTemplate>
    <SecurityCheck Resource="CMS.ContactManagement" Permission="ManageActivities" />
</cms:UIForm>
