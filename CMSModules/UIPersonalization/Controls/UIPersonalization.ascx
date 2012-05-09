<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_UIPersonalization_Controls_UIPersonalization" CodeFile="UIPersonalization.ascx.cs" %>
<%@ Register src="~/CMSAdminControls/UI/PageElements/HeaderActions.ascx" tagname="HeaderActions" tagprefix="cms" %>
<%@ Register Src="~/CMSFormControls/Sites/SiteSelector.ascx" TagName="SiteSelector" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/Membership/FormControls/Roles/selectrole.ascx" TagName="RoleSelector" TagPrefix="cms" %>
<%@ Register Src="~/CMSFormControls/SelectModule.ascx" TagName="ModuleSelector" TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/UI/UIProfiles/UIElementCheckBoxTree.ascx" TagName="UITree" TagPrefix="cms" %>

<cms:CMSUpdatePanel runat="server" ID="pnlUpdate" UpdateMode="Always">
    <ContentTemplate>
        <asp:PlaceHolder runat="server" ID="plcContent">
            <asp:Panel ID="pnlActions" runat="server" CssClass="PageHeaderLine SiteHeaderLine" Style="height: auto;">
                <table>
                    <asp:PlaceHolder runat="server" ID="plcSite">
                        <tr>
                            <td>
                                <cms:LocalizedLabel runat="server" ID="lblSite" EnableViewState="false" ResourceString="general.site"
                                    DisplayColon="true" />
                            </td>
                            <td>
                                <cms:SiteSelector runat="server" ID="selectSite" AllowAll="false" AllowEmpty="false"
                                    OnlyRunningSites="false" />
                            </td>
                        </tr>
                    </asp:PlaceHolder>
                    <asp:PlaceHolder runat="server" ID="plcRole">
                    <tr>
                        <td>
                            <cms:LocalizedLabel runat="server" ID="lblRole" EnableViewState="false" ResourceString="objecttype.cms_role"
                                DisplayColon="true" />
                        </td>
                        <td>
                            <cms:RoleSelector runat="server" ID="selectRole" ShowSiteFilter="false" AddGlobalObjectSuffix="false" UseCodeNameForSelection="false"
                                AllowEmpty="false" />
                        </td>
                    </tr>
                    </asp:PlaceHolder>
                    <asp:PlaceHolder runat="server" ID="plcModule">
                        <tr>
                            <td>
                                <cms:LocalizedLabel runat="server" ID="lblModule" EnableViewState="false" ResourceString="objecttype.cms_resource"
                                    DisplayColon="true" />
                            </td>
                            <td>
                                <cms:ModuleSelector runat="server" ID="selectModule" DisplayOnlyForGivenSite="true" DisplayOnlyWithPermission="false" />
                            </td>
                        </tr>
                    </asp:PlaceHolder>
                </table>
            </asp:Panel>
            <cms:LocalizedLabel runat="server" ID="lblNoRoleInfo" EnableViewState="false" Visible="false"
                ResourceString="uiprofile.norole" CssClass="InfoLabel PageContent" />
            <cms:LocalizedLabel runat="server" ID="lblNoModuleInfo" EnableViewState="false" Visible="false"
                ResourceString="uiprofile.nomodule" CssClass="InfoLabel PageContent" />
            <asp:Panel ID="pnlAdditionalControls" runat="server" CssClass="PageHeaderLine" Style="height: auto;"
                EnableViewState="false">
                <cms:HeaderActions ID="actionsElem" runat="server" />
            </asp:Panel>
            <asp:Panel ID="pnlTree" runat="server" CssClass="UIPersonalizationTreeMedium">
                <cms:LocalizedLabel ID="lblDisabled" runat="server" EnableViewState="false" CssClass="PageHeaderLine"
                    ResourceString="uiprofile.disabled" />
                <cms:LocalizedLabel ID="lblInfo" runat="server" EnableViewState="false" CssClass="PageHeaderLine" Visible="false" />
                <div class="ContentTree">
                    <div class="UIPersonalizationTreeArea">
                        <cms:UITree runat="server" ID="treeElem" />
                    </div>
                </div>
            </asp:Panel>
        </asp:PlaceHolder>
    </ContentTemplate>
</cms:CMSUpdatePanel>
