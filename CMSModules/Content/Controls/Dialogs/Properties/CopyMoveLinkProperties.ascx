<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CopyMoveLinkProperties.ascx.cs"
    Inherits="CMSModules_Content_Controls_Dialogs_Properties_CopyMoveLinkProperties" %>
<%@ Register Src="~/CMSAdminControls/UI/PageElements/PageTitle.ascx" TagName="PageTitle"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/AsyncControl.ascx" TagName="AsyncControl" TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/AsyncBackground.ascx" TagName="AsyncBackground"
    TagPrefix="cms" %>
<div>
    <asp:Panel runat="server" ID="pnlLog" Visible="false">
        <cms:AsyncBackground ID="backgroundElem" runat="server" />
        <div class="AsyncLogArea">
            <div>
                <asp:Panel ID="pnlAsyncBody" runat="server" CssClass="PageBody">
                    <asp:Panel ID="pnlTitleAsync" runat="server" CssClass="PageHeader">
                        <cms:PageTitle ID="titleElemAsync" runat="server" />
                    </asp:Panel>
                    <asp:Panel ID="pnlCancel" runat="server" CssClass="PageHeaderLine">
                        <cms:CMSButton runat="server" ID="btnCancel" CssClass="SubmitButton" />
                    </asp:Panel>
                    <asp:Panel ID="pnlAsyncContent" runat="server" CssClass="PageContent">
                        <cms:AsyncControl ID="ctlAsync" runat="server" />
                    </asp:Panel>
                </asp:Panel>
            </div>
        </div>
    </asp:Panel>
    <div class="DialogInfoArea" id="ContentDiv">
        <asp:Panel runat="server" ID="pnlEmpty" Visible="true" EnableViewState="false">
            <asp:Label runat="server" ID="lblEmpty" EnableViewState="false" />
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlGeneralTab" Visible="false">
            <div class="">
                <table>
                    <tr>
                        <td colspan="2" style="white-space: nowrap;" class="FolderEditLabelArea">
                            <strong>
                                <cms:LocalizedLabel runat="server" ID="lblCopyMoveInfo" ResourceString="dialogs.copymove.target"
                                    CssClass="FieldLabel" EnableViewState="false" DisplayColon="true" />
                            </strong>
                            <asp:Label runat="server" ID="lblAliasPath" CssClass="FieldLabel" EnableViewState="false" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    <asp:PlaceHolder ID="plcUnderlying" runat="server">
                        <tr>
                            <td colspan="2">
                                <cms:LocalizedCheckBox ID="chkUnderlying" runat="server" AutoPostBack="true" OnCheckedChanged="chkUnderlying_OnCheckedChanged"
                                    Checked="true" />
                            </td>
                        </tr>
                    </asp:PlaceHolder>
                    <asp:PlaceHolder ID="plcCopyPermissions" runat="server">
                        <tr>
                            <td colspan="2" style="white-space: nowrap;">
                                <cms:LocalizedCheckBox ID="chkCopyPermissions" runat="server" AutoPostBack="true"
                                    OnCheckedChanged="chkCopyPermissions_OnCheckedChanged" Checked="false" />
                            </td>
                        </tr>
                    </asp:PlaceHolder>
                    <asp:PlaceHolder ID="plcPreservePermissions" runat="server">
                        <tr>
                            <td colspan="2" style="white-space: nowrap;">
                                <cms:LocalizedCheckBox ID="chkPreservePermissions" runat="server" AutoPostBack="true"
                                    OnCheckedChanged="chkPreservePermissions_OnCheckedChanged" Checked="false" />
                            </td>
                        </tr>
                    </asp:PlaceHolder>
                    <tr>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="white-space: nowrap;" class="FolderEditLabelArea">
                            <strong>
                                <asp:Label ID="lblDocToCopy" runat="server" CssClass="FieldLabel" DisplayColon="true"
                                    EnableViewState="false" />
                            </strong>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="white-space: nowrap;" class="FolderEditLabelArea">
                            <div style="height: 120px; padding: 5px; width: 850px; border: 1px solid #B5C3D6;
                                overflow-y: scroll;">
                                <asp:Label ID="lblDocToCopyList" runat="server" CssClass="FieldLabel" EnableViewState="false" />
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblError" runat="server" CssClass="ErrorLabel" EnableViewState="false" />
                            <asp:Label ID="lblInfo" runat="server" CssClass="InfoLabel" EnableViewState="false" />
                        </td>
                    </tr>
                </table>
            </div>
        </asp:Panel>
    </div>
</div>
