<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSFormControls_System_DialogStartConfiguration"
    CodeFile="DialogStartConfiguration.ascx.cs" %>
<%@ Register Src="~/CMSModules/Content/FormControls/Documents/SelectSinglePath.ascx"
    TagName="PathSelector" TagPrefix="cms" %>
<%@ Register Src="~/CMSFormControls/System/AutoResizeConfiguration.ascx" TagName="AutoResize"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSFormControls/Sites/SiteSelector.ascx" TagName="SiteSelector"
    TagPrefix="cms" %>
<cms:CMSUpdatePanel runat="server" ID="updatePanel" UpdateMode="Always">
    <ContentTemplate>
        <table>
            <tr>
                <td class="TextColumn" colspan="2">
                    <cms:LocalizedLinkButton ID="lnkAdvacedFieldSettings" runat="server" ResourceString="TemplateDesigner.ConfigureSettings" />
                </td>
            </tr>
            <asp:PlaceHolder ID="plcAdvancedFieldSettings" runat="server" Visible="false">
                <tr>
                    <td colspan="2">
                        <cms:LocalizedLabel ID="lblError" runat="server" EnableViewState="false" CssClass="ErrorLabel"
                            Visible="false" ResourceString="dialogs.config.wrongformat" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <strong>
                            <cms:LocalizedLabel ID="lblContentTab" runat="server" EnableViewState="false" ResourceString="dialogs.config.contenttab" /></strong>
                    </td>
                </tr>
                <tr>
                    <td class="FieldLabel">
                        <cms:LocalizedLabel ID="lblDisplayContentTab" runat="server" EnableViewState="false"
                            DisplayColon="true" ResourceString="dialogs.config.displaytab" />
                    </td>
                    <td>
                        <asp:CheckBox ID="chkDisplayContentTab" runat="server" Checked="true" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <cms:LocalizedLabel ID="lblContentSite" runat="server" EnableViewState="false" DisplayColon="true"
                            ResourceString="dialogs.config.availablesites" />
                    </td>
                    <td>
                        <cms:SiteSelector ID="siteSelectorContent" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <cms:LocalizedLabel ID="lblContentStartPath" runat="server" EnableViewState="false"
                            DisplayColon="true" ResourceString="dialogs.config.startingpath" />
                    </td>
                    <td>
                        <cms:PathSelector ID="selectPathElem" runat="server" IsLiveSite="false" />
                    </td>
                </tr>
                <asp:PlaceHolder ID="plcMedia" runat="server">
                    <tr>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <strong>
                                <cms:LocalizedLabel ID="lblMediaTab" runat="server" EnableViewState="false" ResourceString="dialogs.config.mediatab" /></strong>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <cms:LocalizedLabel ID="lblDisplayMediaTab" runat="server" EnableViewState="false"
                                DisplayColon="true" ResourceString="dialogs.config.displaytab" />
                        </td>
                        <td>
                            <asp:CheckBox ID="chkDisplayMediaTab" runat="server" Checked="true" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <cms:LocalizedLabel ID="lblMediaSite" runat="server" EnableViewState="false" DisplayColon="true"
                                ResourceString="dialogs.config.availablesites" />
                        </td>
                        <td>
                            <cms:SiteSelector ID="siteSelectorMedia" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <cms:LocalizedLabel ID="lblMediaSiteLibraries" runat="server" EnableViewState="false"
                                DisplayColon="true" ResourceString="dialogs.config.sitelibraries" />
                        </td>
                        <td>
                            <asp:DropDownList ID="drpSiteLibraries" runat="server" CssClass="DropDownField" />
                        </td>
                    </tr>
                    <asp:PlaceHolder ID="plcGroups" runat="server">
                        <tr>
                            <td>
                                <cms:LocalizedLabel ID="lblMediaSiteGroups" runat="server" EnableViewState="false"
                                    DisplayColon="true" ResourceString="dialogs.config.sitegroups" />
                            </td>
                            <td>
                                <asp:DropDownList ID="drpGroups" runat="server" AutoPostBack="true" CssClass="DropDownField" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <cms:LocalizedLabel ID="lblMediaGroupLibraries" runat="server" EnableViewState="false"
                                    DisplayColon="true" ResourceString="dialogs.config.grouplibraries" />
                            </td>
                            <td>
                                <asp:DropDownList ID="drpGroupLibraries" runat="server" CssClass="DropDownField" />
                            </td>
                        </tr>
                    </asp:PlaceHolder>
                    <tr>
                        <td>
                            <cms:LocalizedLabel ID="lblMediaStartPath" runat="server" EnableViewState="false"
                                DisplayColon="true" ResourceString="dialogs.config.startingpath" />
                        </td>
                        <td>
                            <cms:CMSTextBox ID="txtMediaStartPath" runat="server" CssClass="TextBoxField" /><br />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <i>
                                <cms:LocalizedLabel ID="lblMediaStartPathExample" runat="server" EnableViewState="false"
                                    ResourceString="dialogs.config.mediaexample" /></i>
                        </td>
                    </tr>
                </asp:PlaceHolder>
                <tr>
                    <td colspan="2">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <strong>
                            <cms:LocalizedLabel ID="lblOtherTabs" runat="server" EnableViewState="false" ResourceString="dialogs.config.othertabs" /></strong>
                    </td>
                </tr>
                <tr>
                    <td>
                        <cms:LocalizedLabel ID="lblDisplayAttachments" runat="server" EnableViewState="false"
                            DisplayColon="true" ResourceString="dialogs.config.displayattachments" />
                    </td>
                    <td>
                        <asp:CheckBox ID="chkDisplayAttachments" runat="server" Checked="true" />
                    </td>
                </tr>
                <asp:PlaceHolder runat="server" ID="plcDisplayEmail">
                    <tr>
                        <td>
                            <cms:LocalizedLabel ID="lblDisplayEmail" runat="server" EnableViewState="false" DisplayColon="true"
                                ResourceString="dialogs.config.displayemail" />
                        </td>
                        <td>
                            <asp:CheckBox ID="chkDisplayEmail" runat="server" Checked="true" />
                        </td>
                    </tr>
                </asp:PlaceHolder>
                <asp:PlaceHolder runat="server" ID="plcDisplayAnchor">
                    <tr>
                        <td>
                            <cms:LocalizedLabel ID="lblDisplayAnchor" runat="server" EnableViewState="false"
                                DisplayColon="true" ResourceString="dialogs.config.displayanchor" />
                        </td>
                        <td>
                            <asp:CheckBox ID="chkDisplayAnchor" runat="server" Checked="true" />
                        </td>
                    </tr>
                </asp:PlaceHolder>
                <asp:PlaceHolder runat="server" ID="plcDisplayWeb">
                    <tr>
                        <td>
                            <cms:LocalizedLabel ID="lblDisplayWeb" runat="server" EnableViewState="false" DisplayColon="true"
                                ResourceString="dialogs.config.displayweb" />
                        </td>
                        <td>
                            <asp:CheckBox ID="chkDisplayWeb" runat="server" Checked="true" />
                        </td>
                    </tr>
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="plcAutoResize" runat="server">
                    <tr>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <strong>
                                <cms:LocalizedLabel ID="lblAutoresize" runat="server" EnableViewState="false" ResourceString="dialogs.config.autoresize" /></strong>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <cms:AutoResize ID="elemAutoResize" runat="server" />
                        </td>
                    </tr>
                </asp:PlaceHolder>
            </asp:PlaceHolder>
        </table>
    </ContentTemplate>
</cms:CMSUpdatePanel>
