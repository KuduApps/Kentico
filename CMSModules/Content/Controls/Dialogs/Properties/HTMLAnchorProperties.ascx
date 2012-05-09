<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_Content_Controls_Dialogs_Properties_HTMLAnchorProperties" CodeFile="HTMLAnchorProperties.ascx.cs" %>
<%@ Register src="~/CMSAdminControls/UI/PageElements/Help.ascx" tagname="Help" tagprefix="cms" %>
<%@ Register Src="~/CMSModules/Content/Controls/Dialogs/General/WidthHeightSelector.ascx" TagPrefix="cms"
    TagName="WidthHeightSelector" %>
<asp:Literal ID="ltlScript" runat="server" />

<script type="text/javascript" language="javascript">
    function insertItem() {
        RaiseHiddenPostBack();
    }      
</script>

<div class="HTMLAnchorProperties">
    <cms:CMSUpdatePanel ID="plnAnchorUpdate" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="LeftAlign" style="width: 500px;">
                <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
                    Visible="false" />
                <table width="100%" style="white-space: nowrap;">
                    <tr>
                        <td>
                            <cms:LocalizedLabel ID="lblLinkText" runat="server" EnableViewState="false" ResourceString="dialogs.anchor.linktext"
                                DisplayColon="true" CssClass="DialogLabel" />
                        </td>
                        <td>
                            <cms:CMSTextBox ID="txtLinkText" runat="server" CssClass="VeryLongTextBox" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <asp:RadioButton ID="rbAnchorName" runat="server" AutoPostBack="true" OnCheckedChanged="rbAnchorName_CheckedChanged"
                                CssClass="AnchorRadioButton" />
                            <div class="AnchorDropDownList">
                                <asp:DropDownList ID="drpAnchorName" runat="server" CssClass="SmallDropDown" />
                            </div>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <asp:RadioButton ID="rbAnchorId" runat="server" AutoPostBack="true" OnCheckedChanged="rbAnchorId_CheckedChanged" />
                            <div class="AnchorDropDownList">
                                <asp:DropDownList ID="drpAnchorId" runat="server" CssClass="SmallDropDown" />
                            </div>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <asp:RadioButton ID="rbAnchorText" runat="server" AutoPostBack="true" OnCheckedChanged="rbAnchorText_CheckedChanged" />
                            <div class="AnchorDropDownList">
                                <cms:CMSTextBox ID="txtAnchorText" runat="server" CssClass="SmallTextBox"></cms:CMSTextBox >
                            </div>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </cms:CMSUpdatePanel>
    <div class="Hidden">
        <cms:CMSUpdatePanel ID="plnAnchorButtonsUpdate" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <cms:CMSButton ID="hdnButton" runat="server" OnClick="hdnButton_Click" CssClass="HiddenButton" EnableViewState="false" />
                <cms:CMSButton ID="btnHiddenUpdate" runat="server" OnClick="btnHiddenUpdate_Click" CssClass="HiddenButton" EnableViewState="false" />
            </ContentTemplate>
        </cms:CMSUpdatePanel>
    </div>
    <div class="RightAlign">
        <cms:Help ID="helpElem" runat="server" TopicName="dialogs_link_anchor" />
    </div>
</div>
