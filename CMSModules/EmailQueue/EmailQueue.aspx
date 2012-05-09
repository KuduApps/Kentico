<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_EmailQueue_EmailQueue"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" CodeFile="EmailQueue.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/UniGrid/Filters/TextSimpleFilter.ascx" TagName="TextSimpleFilter"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/EmailQueue/Controls/EmailQueue.ascx" TagName="EmailQueueGrid"
    TagPrefix="cms" %>    

<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false" Visible="false" />
    <span class="InfoLabel">
        <cms:LocalizedLabel ID="lblText" runat="server" EnableViewState="false" ResourceString="EmailQueue.Queue.Text" /><br />
        <cms:LocalizedLabel runat="server" ID="lblDisabled" EnableViewState="false" Visible="false"
            ResourceString="EmailQueue.EmailsDisabled" CssClass="FieldLabel" />
    </span>
    <span class="InfoLabel">
        <asp:Image runat="server" ID="imgShowFilter" CssClass="NewItemImage" />
        <cms:LocalizedLinkButton ID="btnShowFilter" runat="server" OnClick="btnShowFilter_Click" />
    </span>
    <asp:PlaceHolder ID="plcFilter" runat="server" Visible="false">
        <table>
            <tr runat="server">
                <td class="FieldLabel">
                    <cms:LocalizedLabel ID="lblFrom" runat="server" EnableViewState="false" DisplayColon="true"
                        ResourceString="general.from" />
                </td>
                <td>
                    <cms:TextSimpleFilter ID="fltFrom" runat="server" Column="EmailFrom" Size="100" />
                </td>
            </tr>
            <tr runat="server">
                <td class="FieldLabel">
                    <cms:LocalizedLabel ID="lblTo" runat="server" EnableViewState="false" DisplayColon="true"
                        ResourceString="general.toemail" />
                </td>
                <td>
                    <cms:TextSimpleFilter ID="fltTo" runat="server" Column="EmailTo" Size="100" />
                </td>
            </tr>
            <tr runat="server">
                <td class="FieldLabel">
                    <cms:LocalizedLabel ID="lblSubject" runat="server" EnableViewState="false" DisplayColon="true"
                        ResourceString="general.subject" />
                </td>
                <td>
                    <cms:TextSimpleFilter ID="fltSubject" runat="server" Column="EmailSubject" Size="450" />
                </td>
            </tr>
            <tr runat="server">
                <td class="FieldLabel">
                    <cms:LocalizedLabel ID="lblBody" runat="server" EnableViewState="false" DisplayColon="true"
                        ResourceString="general.body" />
                </td>
                <td>
                    <cms:TextSimpleFilter ID="fltBody" runat="server" Column="EmailBody" />
                </td>
            </tr>
            <tr>
                <td class="FieldLabel">
                    <cms:LocalizedLabel ID="lblPriority" runat="server" EnableViewState="false" DisplayColon="true"
                        ResourceString="emailqueue.priority" />
                </td>
                <td>
                    <asp:DropDownList ID="drpPriority" runat="server" CssClass="DropDownFieldFilter" />
                </td>
            </tr>
            <tr>
                <td class="FieldLabel">
                    <cms:LocalizedLabel ID="lblStatus" runat="server" EnableViewState="false" DisplayColon="true"
                        ResourceString="emailqueue.status" />
                </td>
                <td>
                    <asp:DropDownList ID="drpStatus" runat="server" CssClass="DropDownFieldFilter" />
                </td>
            </tr>
            <tr runat="server">
                <td class="FieldLabel">
                    <cms:LocalizedLabel ID="lblErrorMessage" runat="server" EnableViewState="false" DisplayColon="true"
                        ResourceString="emailqueue.lastsendresult" />
                </td>
                <td>
                    <cms:TextSimpleFilter ID="fltLastResult" runat="server" Column="EmailLastSendResult" />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    <cms:LocalizedButton ID="btnFilter" runat="server" OnClick="btnFilter_Clicked" CssClass="ContentButton"
                        EnableViewState="false" ResourceString="General.Show" />
                </td>
            </tr>
        </table>
    </asp:PlaceHolder>
    <br />
    <cms:EmailQueueGrid ID="gridEmailQueue" runat="server" />
</asp:Content>
