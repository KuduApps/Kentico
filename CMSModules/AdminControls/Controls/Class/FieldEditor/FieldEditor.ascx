<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_AdminControls_Controls_Class_FieldEditor_FieldEditor"
    CodeFile="FieldEditor.ascx.cs" %>
<%@ Register Src="~/CMSModules/AdminControls/Controls/Class/FieldEditor/ControlSettings.ascx"
    TagName="ControlSettings" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/AdminControls/Controls/Class/FieldEditor/CSSsettings.ascx"
    TagName="CSSsettings" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/AdminControls/Controls/Class/FieldEditor/DatabaseConfiguration.ascx"
    TagName="DatabaseConfiguration" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/AdminControls/Controls/Class/FieldEditor/DocumentSource.ascx"
    TagName="DocumentSource" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/AdminControls/Controls/Class/FieldEditor/FieldAppearance.ascx"
    TagName="FieldAppearance" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/AdminControls/Controls/Class/FieldEditor/ValidationSettings.ascx"
    TagName="ValidationSettings" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/AdminControls/Controls/Class/FieldEditor/SimpleMode.ascx"
    TagName="SimpleMode" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/AdminControls/Controls/Class/FieldEditor/CategoryEdit.ascx"
    TagName="CategoryEdit" TagPrefix="cms" %>
<asp:Panel ID="pnlFieldEditor" runat="server" CssClass="FieldEditor">
    <asp:Panel ID="pnlFieldEditorWrapper" runat="server">
        <asp:Panel ID="pnlTopMenu" runat="server" CssClass="FieldTopMenu">
            <div class="FieldTopMenuPadding">
                <div class="FloatLeft">
                    <asp:LinkButton ID="lnkSave" runat="server" OnClick="lnkSave_Click" CssClass="ContentSaveLinkButton">
                        <asp:Image ID="imgSave" runat="server" EnableViewState="false" CssClass="NewItemImage" />
                        <cms:LocalizedLabel ID="lblSave" runat="server" EnableViewState="false" ResourceString="fieldeditor.savefield" />
                    </asp:LinkButton>
                </div>
                <asp:Panel ID="pnlModeButtons" runat="server" class="FloatRight">
                    <cms:LocalizedLinkButton ID="btnSimplified" runat="server" ResourceString="fieldeditor.tabs.simplifiedmode"
                        Visible="false" OnCommand="btnSimplified_Command" />
                    <cms:LocalizedLinkButton ID="btnAdvanced" runat="server" ResourceString="fieldeditor.tabs.advancedmode"
                        Visible="false" OnCommand="btnAdvanced_Command" />
                </asp:Panel>
                <div class="ClearBoth">
                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlLeft" runat="server" CssClass="FieldPanelLeft">
            <table border="0" cellpadding="0" cellspacing="0" class="FieldTableMenu">
                <tr>
                    <td>
                        <asp:ListBox ID="lstAttributes" runat="server" AutoPostBack="True" OnSelectedIndexChanged="lstAttributes_SelectedIndexChanged"
                            EnableViewState="true" CssClass="AttributesList" />
                    </td>
                    <td class="FieldMenuButtons">
                        <asp:PlaceHolder ID="plcActions" runat="server">
                            <div class="FieldGrouppedButtons">
                                <asp:ImageButton ID="btnUpAttribute" runat="server" OnClick="btnUpAttribute_Click" />
                                <asp:ImageButton ID="btnDownAttribute" runat="server" OnClick="btnDownAttribute_Click" />
                            </div>
                            <div class="FieldGrouppedButtons">
                                <asp:ImageButton ID="btnNewCategory" runat="server" OnClick="btnNewCategory_Click" />
                                <asp:ImageButton ID="btnNewSysAttribute" runat="server" OnClick="btnNewSysAttribute_Click" />
                                <asp:ImageButton ID="btnNewPrimaryAttribute" runat="server" OnClick="btnNewPrimaryAttribute_Click" />
                            </div>
                            <asp:ImageButton ID="btnNewAttribute" runat="server" OnClick="btnNewAttribute_Click" />
                            <asp:ImageButton ID="btnDeleteItem" runat="server" OnClick="btnDeleteItem_Click" />
                        </asp:PlaceHolder>
                    </td>
                </tr>
                <tr>
                    <td>
                        <cms:DocumentSource ID="documentSource" runat="server" ShortID="ds" />
                    </td>
                </tr>
            </table>
            <asp:PlaceHolder ID="plcQuickSelect" runat="server" EnableViewState="false"><strong>
                <cms:LocalizedLabel ID="lblQuickLinks" runat="server" EnableViewState="false" ResourceString="fieldeditor.quicklinks"
                    DisplayColon="true" />
            </strong>
                <ul>
                    <li><a href="#Database">
                        <cms:LocalizedLabel ID="lblDatabase" runat="server" EnableViewState="false" ResourceString="templatedesigner.section.database" /></a></li>
                    <asp:PlaceHolder ID="plcQuickAppearance" runat="server">
                        <li><a href="#FieldAppearance">
                            <cms:LocalizedLabel ID="lblField" runat="server" EnableViewState="false" ResourceString="templatedesigner.section.fieldappearance" /></a></li>
                    </asp:PlaceHolder>
                    <asp:PlaceHolder ID="plcQuickSettings" runat="server">
                        <li><a href="#ControlSettings">
                            <cms:LocalizedLabel ID="lblSettings" runat="server" EnableViewState="false" ResourceString="templatedesigner.section.settings" /></a></li>
                    </asp:PlaceHolder>
                    <asp:PlaceHolder ID="plcQuickValidation" runat="server">
                        <li><a href="#ValidationSettings">
                            <cms:LocalizedLabel ID="lblValidation" runat="server" EnableViewState="false" ResourceString="templatedesigner.section.validation" /></a></li>
                    </asp:PlaceHolder>
                    <asp:PlaceHolder ID="plcQuickStyles" runat="server">
                        <li><a href="#CSSStyles">
                            <cms:LocalizedLabel ID="lblStyles" runat="server" EnableViewState="false" ResourceString="templatedesigner.section.styles" /></a></li>
                    </asp:PlaceHolder>
                </ul>
            </asp:PlaceHolder>
        </asp:Panel>
        <asp:Panel ID="pnlContent" runat="server" CssClass="FieldPanelRightContent">
            <div class="FieldPanelRightContentPadding">
                <div class="FieldRightScrollPanel">
                    <div class="FieldPanelRightWizard">
                        <div class="FieldPanelRightWrapper">
                            <asp:PlaceHolder ID="plcLabels" runat="server">
                                <cms:LocalizedLabel ID="lblError" runat="server" CssClass="ErrorLabel" EnableViewState="false"
                                    Visible="false" /><cms:LocalizedLabel ID="lblInfo" runat="server" CssClass="InfoLabel"
                                        EnableViewState="false" Visible="false" />
                            </asp:PlaceHolder>
                            <asp:Panel runat="server" ID="pnlField" class="FieldContentTable">
                                <asp:PlaceHolder ID="plcCategory" runat="server" Visible="false">
                                    <cms:CategoryEdit ID="categoryEdit" runat="server" ShortID="ce" />
                                </asp:PlaceHolder>
                                <asp:PlaceHolder ID="plcSimple" runat="server" Visible="false">
                                    <cms:CMSUpdatePanel ID="pnlUpdateSimpleMode" runat="server">
                                        <ContentTemplate>
                                            <cms:SimpleMode ID="simpleMode" runat="server" ShortID="sm" />
                                        </ContentTemplate>
                                    </cms:CMSUpdatePanel>
                                </asp:PlaceHolder>
                                <asp:PlaceHolder ID="plcAdvanced" runat="server" Visible="false">
                                    <asp:PlaceHolder ID="plcDatabase" runat="server">
                                        <div class="FieldAnchor" id="Database">
                                        </div>
                                        <cms:CMSUpdatePanel ID="pnlUpdateDatabase" runat="server">
                                            <ContentTemplate>
                                                <cms:DatabaseConfiguration ID="databaseConfiguration" runat="server" ShortID="dc" />
                                            </ContentTemplate>
                                        </cms:CMSUpdatePanel>
                                    </asp:PlaceHolder>
                                    <cms:CMSUpdatePanel ID="pnlUpdateDisplay" runat="server">
                                        <ContentTemplate>
                                            <cms:LocalizedCheckBox ID="chkDisplayInForm" runat="server" AutoPostBack="True" OnCheckedChanged="chkDisplayInForm_CheckedChanged"
                                                CssClass="CheckBoxMovedLeft" ResourceString="templatedesigner.displayinform" />
                                            <asp:Panel runat="server" ID="pnlDisplayInDashBoard" Visible="false">
                                                <cms:LocalizedCheckBox ID="chkDisplayInDashBoard" runat="server" CssClass="CheckBoxMovedLeft"
                                                    ResourceString="templatedesigner.displayindashboard" />
                                            </asp:Panel>
                                        </ContentTemplate>
                                    </cms:CMSUpdatePanel>
                                    <asp:PlaceHolder ID="plcField" runat="server">
                                        <div class="FieldAnchor" id="FieldAppearance">
                                        </div>
                                        <cms:CMSUpdatePanel ID="pnlUpdateAppearance" runat="server">
                                            <ContentTemplate>
                                                <cms:FieldAppearance ID="fieldAppearance" runat="server" ShortID="fa" />
                                            </ContentTemplate>
                                        </cms:CMSUpdatePanel>
                                    </asp:PlaceHolder>
                                    <asp:PlaceHolder ID="plcSettings" runat="server">
                                        <div class="FieldAnchor" id="ControlSettings">
                                        </div>
                                        <cms:CMSUpdatePanel ID="pnlUpdateSettings" runat="server">
                                            <ContentTemplate>
                                                <cms:ControlSettings ID="controlSettings" runat="server" ShortID="cs" />
                                            </ContentTemplate>
                                        </cms:CMSUpdatePanel>
                                    </asp:PlaceHolder>
                                    <asp:PlaceHolder ID="plcValidation" runat="server">
                                        <div class="FieldAnchor" id="ValidationSettings">
                                        </div>
                                        <cms:CMSUpdatePanel ID="pnlUpdateValidation" runat="server">
                                            <ContentTemplate>
                                                <cms:ValidationSettings ID="validationSettings" runat="server" ShortID="vs" />
                                            </ContentTemplate>
                                        </cms:CMSUpdatePanel>
                                    </asp:PlaceHolder>
                                    <asp:PlaceHolder ID="plcCSS" runat="server">
                                        <div class="FieldAnchor" id="CSSStyles">
                                        </div>
                                        <cms:CMSUpdatePanel ID="pnlUpdateCSS" runat="server">
                                            <ContentTemplate>
                                                <cms:CSSsettings ID="cssSettings" runat="server" ShortID="ss" />
                                            </ContentTemplate>
                                        </cms:CMSUpdatePanel>
                                    </asp:PlaceHolder>
                                </asp:PlaceHolder>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>
        <div class="ClearBoth">
        </div>
    </asp:Panel>
</asp:Panel>
<asp:Literal ID="ltlConfirmText" runat="server" EnableViewState="false" />
<asp:Literal ID="ltlScript" runat="server" EnableViewState="false" />

<script type="text/javascript">
    //<![CDATA[
    function confirmDelete() {
        return confirm(document.getElementById('confirmdelete').value);
    }
    //]]>
</script>

