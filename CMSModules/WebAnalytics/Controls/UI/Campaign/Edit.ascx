<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_WebAnalytics_Controls_UI_Campaign_Edit"
    CodeFile="Edit.ascx.cs" %>
<%@ Register Src="~/CMSFormControls/Basic/TextBoxControl.ascx" TagName="TextBoxControl"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSFormControls/Inputs/LargeTextArea.ascx" TagName="LargeTextArea"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSFormControls/Basic/CalendarControl.ascx" TagName="CalendarControl"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSFormControls/Basic/CheckBoxControl.ascx" TagName="CheckBoxControl"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSFormControls/Inputs/ConditionBuilder.ascx" TagName="ConditionBuilder"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSFormControls/System/LocalizableTextBox.ascx" TagName="LocalizableTextBox"
    TagPrefix="cms" %>
<cms:LocalizedLabel runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
    Visible="false" />
<cms:UIForm runat="server" ID="EditForm" ObjectType="analytics.campaign" DefaultFieldLayout="Inline"
    RedirectUrlAfterCreate="Frameset.aspx?campaignid={%EditedObject.ID%}&saved=1">
    <SecurityCheck Resource="CMS.WebAnalytics" Permission="ManageCampaigns" />
    <LayoutTemplate>
        <asp:Panel runat="server" ID="pnlBasic" CssClass="CampaignPanel">
            <table>
                <cms:FormField runat="server" ID="fDisplay" Field="CampaignDisplayName">
                    <tr>
                        <td>
                            <cms:LocalizedLabel ID="lblDisplayName" runat="server" EnableViewState="false" ResourceString="campaign.displayname"
                                DisplayColon="true" />
                        </td>
                        <td>
                            <cms:LocalizableTextBox ID="txtDisplayName" runat="server" CssClass="TextBoxField"
                                MaxLength="200" Trim="true" />
                        </td>
                    </tr>
                </cms:FormField>
                <cms:FormField runat="server" ID="fCodeName" Field="CampaignName">
                    <tr>
                        <td>
                            <cms:LocalizedLabel ID="lblCodeName" runat="server" EnableViewState="false" ResourceString="campaign.codename"
                                DisplayColon="true" />
                        </td>
                        <td>
                            <cms:TextBoxControl ID="txtCodeName" runat="server" CssClass="TextBoxField" MaxLength="200"
                                Trim="true" />
                        </td>
                    </tr>
                </cms:FormField>
                <cms:FormField runat="server" ID="fDescription" Field="CampaignDescription">
                    <tr>
                        <td>
                            <cms:LocalizedLabel ID="lblDescription" runat="server" EnableViewState="false" ResourceString="campaign.description"
                                DisplayColon="true" />
                        </td>
                        <td>
                            <cms:LocalizableTextBox ID="txtDescription" runat="server" TextMode="MultiLine" CssClass="TextAreaField"
                                EnableViewState="false" IsTextArea="true" />
                        </td>
                    </tr>
                </cms:FormField>
                <cms:FormField runat="server" ID="fOpenFrom" Field="CampaignOpenFrom">
                    <tr>
                        <td>
                            <cms:LocalizedLabel ID="lblOpenFrom" runat="server" EnableViewState="false" ResourceString="general.openfrom"
                                DisplayColon="true" />
                        </td>
                        <td>
                            <cms:CalendarControl ID="ucOpenFrom" IsLiveSite="false" runat="server" />
                        </td>
                    </tr>
                </cms:FormField>
                <cms:FormField runat="server" ID="fOpenTo" Field="CampaignOpenTo">
                    <tr>
                        <td>
                            <cms:LocalizedLabel ID="lblOpenTo" runat="server" EnableViewState="false" ResourceString="general.opento"
                                DisplayColon="true" />
                        </td>
                        <td>
                            <cms:CalendarControl ID="ucOpenTo" IsLiveSite="false" runat="server" />
                        </td>
                    </tr>
                </cms:FormField>
                <cms:FormField runat="server" ID="fEnabled" Field="CampaignEnabled">
                    <tr>
                        <td>
                            <cms:LocalizedLabel ID="lblEnabled" runat="server" EnableViewState="false" ResourceString="general.enabled"
                                DisplayColon="true" />
                        </td>
                        <td>
                            <cms:CheckBoxControl ID="chkEnabled" runat="server" />
                        </td>
                    </tr>
                </cms:FormField>
            </table>
        </asp:Panel>
        <br />
        <br />
        <asp:Panel runat="server" ID="pnlAdvanced" CssClass="CampaignPanel">
            <table>
                <cms:FormField runat="server" ID="fImpressions" Field="CampaignImpressions">
                    <tr>
                        <td>
                            <cms:LocalizedLabel ID="lblImpressions" runat="server" EnableViewState="false" ResourceString="campaign.impressions"
                                DisplayColon="true" />
                        </td>
                        <td>
                            <cms:TextBoxControl ID="txtImpressions" runat="server" />
                        </td>
                    </tr>
                </cms:FormField>
                <cms:FormField runat="server" ID="fTotalCost" Field="CampaignTotalCost">
                    <tr>
                        <td>
                            <cms:LocalizedLabel ID="lblTotalCost" runat="server" EnableViewState="false" ResourceString="campaign.totalcost"
                                DisplayColon="true" />
                        </td>
                        <td>
                            <cms:TextBoxControl ID="txtTotalCost" runat="server" />
                        </td>
                    </tr>
                </cms:FormField>
                <cms:FormField runat="server" ID="fCampaignRules" Field="CampaignRules">
                    <tr>
                        <td>
                            <cms:LocalizedLabel ID="LocalizedLabel3" runat="server" EnableViewState="false" ResourceString="campaign.rules"
                                DisplayColon="true" />
                        </td>
                        <td>
                            <cms:ConditionBuilder ID="ucMacroEditor" runat="server" />
                        </td>
                    </tr>
                </cms:FormField>
            </table>
        </asp:Panel>
        <br />
        <div class="CampaignSubmitButton">
            <cms:FormSubmit runat="server" ID="btnSubmit" />
        </div>
    </LayoutTemplate>
</cms:UIForm>
