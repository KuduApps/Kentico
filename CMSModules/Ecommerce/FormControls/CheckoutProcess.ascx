<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_Ecommerce_FormControls_CheckoutProcess"
    CodeFile="CheckoutProcess.ascx.cs" %>
<%@ Register Src="~/CMSFormControls/System/LocalizableTextBox.ascx" TagName="LocalizableTextBox"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/UI/PageElements/Help.ascx" TagName="Help" TagPrefix="cms" %>
<asp:HiddenField ID="hdnCheckoutProcessXml" runat="server" EnableViewState="false" />
<div class="CheckoutProcess">
    <asp:PlaceHolder ID="plcList" runat="server">
        <asp:Panel ID="pnlHeaderLine" runat="server" CssClass="PageHeaderLine">
            <table cellpadding="0" cellspacing="0" border="0" style="width: 100%;">
                <tr>
                    <td style="width: 100%;">
                        <asp:Panel ID="pnlActions" runat="server" Visible="true" EnableViewState="false"
                            CssClass="Actions">
                            <asp:Image ID="imgNewItem" runat="server" CssClass="NewItemImage" />
                            <asp:LinkButton ID="lnkNewStep" runat="server" OnClick="lnkNewStep_Click" CssClass="NewItemLink" />
                        </asp:Panel>
                    </td>
                    <td>
                        <cms:Help ID="helpElem" runat="server" TopicName="new_step2" HelpName="helpTopic" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnlContent" runat="server" CssClass="PageContent">
            <asp:Label ID="lblInfo" runat="server" CssClass="InfoLabel" Visible="false" EnableViewState="false" />
            <asp:Label ID="lblError" runat="server" CssClass="ErrorLabel" Visible="false" EnableViewState="false" />
            <asp:GridView ID="gridSteps" runat="server" AutoGenerateColumns="false" CellPadding="3"
                GridLines="Horizontal" CssClass="UniGridGrid">
                <HeaderStyle CssClass="UniGridHead" />
                <Columns>
                    <asp:TemplateField>
                        <HeaderStyle Wrap="false" />
                        <HeaderTemplate>
                            <%# mActions %></HeaderTemplate>
                        <ItemStyle Width="80" CssClass="NW UniGridActions" />
                        <ItemTemplate>
                            <asp:ImageButton ID="btnEdit" runat="server" CommandArgument='<%# Eval("Name") %>'
                                OnClick="btnEdit_Click" ImageUrl='<%# btnEditImageUrl %>' ToolTip='<%# btnEditToolTip %>'
                                AlternateText='<%# btnEditToolTip %>' CssClass="UnigridActionButton" /><asp:ImageButton
                                    ID="btnDelete" runat="server" CommandArgument='<%# Eval("Name") %>' OnClick="btnDelete_Click"
                                    OnClientClick="return ConfirmDelete();" ImageUrl='<%# btnDeleteImageUrl %>' ToolTip='<%# btnDeleteToolTip %>'
                                    AlternateText='<%# btnDeleteToolTip %>' CssClass="UnigridActionButton" /><asp:ImageButton
                                        ID="btnUp" runat="server" CommandArgument='<%# Eval("Name") %>' OnClick="btnMoveUp_Click"
                                        ImageUrl='<%# btnMoveUpImageUrl %>' ToolTip='<%# btnMoveUpToolTip %>' AlternateText='<%# btnMoveUpToolTip %>'
                                        CssClass="UnigridActionButton" /><asp:ImageButton ID="btnDown" runat="server" CommandArgument='<%# Eval("Name") %>'
                                            OnClick="btnMoveDown_Click" ImageUrl='<%# btnMoveDownImageUrl %>' ToolTip='<%# btnMoveDownToolTip %>'
                                            AlternateText='<%# btnMoveDownToolTip %>' CssClass="UnigridActionButton" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderStyle Wrap="false" />
                        <HeaderTemplate>
                            <%# mOrder %></HeaderTemplate>
                        <ItemStyle />
                        <ItemTemplate>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderStyle Wrap="false" />
                        <HeaderTemplate>
                            <%# mCaption %></HeaderTemplate>
                        <ItemStyle Wrap="false" />
                        <ItemTemplate>
                            <%# HTMLHelper.HTMLEncode(ResHelper.LocalizeString(Convert.ToString(Eval("Caption"))))%></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderStyle Wrap="false" />
                        <HeaderTemplate>
                            <%# mShowOnLiveSite %></HeaderTemplate>
                        <ItemStyle  />
                        <ItemTemplate>
                            <%# this.GetColoredBooleanString(Eval("LiveSite"))%></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderStyle  Wrap="false" />
                        <HeaderTemplate>
                            <%# mShowInCMSDeskCustomer%></HeaderTemplate>
                        <ItemStyle  />
                        <ItemTemplate>
                            <%# this.GetColoredBooleanString(Eval("CMSDeskCustomer"))%></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderStyle  Wrap="false" />
                        <HeaderTemplate>
                            <%# mShowInCMSDeskOrder%></HeaderTemplate>
                        <ItemStyle  />
                        <ItemTemplate>
                            <%# this.GetColoredBooleanString(Eval("CMSDeskOrder"))%></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderStyle  Wrap="false" />
                        <HeaderTemplate>
                            <%# mShowInCMSDeskOrderItems%></HeaderTemplate>
                        <ItemStyle  />
                        <ItemTemplate>
                            <%# this.GetColoredBooleanString(Eval("CMSDeskOrderItems"))%></ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <div style="float: right; padding-top: 5px;">
                <cms:CMSButton ID="btnDefaultProcess" runat="server" OnClick="btnDefaultProcess_Click"
                    OnClientClick="return ConfirmDefaultProcess();" CssClass="XLongButton" Visible="false" /></td>
            </div>
        </asp:Panel>
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="plcEdit" runat="server">
        <div class="SimpleHeader">
            <div class="PageTitleBreadCrumbs">
                <div id="plcEditDiv" runat="server" class="PageTitleBreadCrumbsPadding">
                    <asp:LinkButton ID="lnkList" runat="server" OnClick="lnkList_Click" CssClass="TitleBreadCrumb LeftAlign"
                        EnableViewState="false" />
                    <span class="TitleBreadCrumbSeparator LeftAlign" style="height: 14px;">&nbsp;</span>
                    <asp:Label ID="lblCurrentStep" runat="server" EnableViewState="false" CssClass="TitleBreadCrumbLast LeftAlign" />
                    <div style="clear: both;">
                    </div>
                </div>
            </div>
        </div>
        <asp:Panel ID="pnlEditContent" runat="server" CssClass="PageContent">
            <asp:Label ID="lblErrorEdit" runat="server" CssClass="ErrorLabel" Visible="false"
                EnableViewState="false" />
            <table>
                <tr>
                    <td>
                        <asp:Label ID="lblStepCaption" runat="server" EnableViewState="false" />
                    </td>
                    <td>
                        <cms:LocalizableTextBox ID="txtStepCaption" runat="server" CssClass="TextBoxField" />
                        <cms:CMSRequiredFieldValidator ID="rfvStepCaption" runat="server" ControlToValidate="txtStepCaption:textbox"
                            ValidationGroup="CheckoutProcess" Display="Dynamic" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblStepName" runat="server" EnableViewState="false" />
                    </td>
                    <td>
                        <cms:CMSTextBox ID="txtStepName" runat="server" CssClass="TextBoxField" />
                        <cms:CMSRequiredFieldValidator ID="rfvStepName" runat="server" ControlToValidate="txtStepName"
                            ValidationGroup="CheckoutProcess" Display="Dynamic" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblStepImageUrl" runat="server" EnableViewState="false" />
                    </td>
                    <td>
                        <cms:CMSTextBox ID="txtStepImageUrl" runat="server" CssClass="TextBoxField" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblStepControlPath" runat="server" EnableViewState="false" />
                    </td>
                    <td>
                        <cms:CMSTextBox ID="txtStepControlPath" runat="server" CssClass="TextBoxField" />
                        <cms:CMSRequiredFieldValidator ID="rfvStepControlPath" runat="server" ControlToValidate="txtStepControlPath"
                            ValidationGroup="CheckoutProcess" Display="Dynamic" />
                    </td>
                </tr>
                <asp:PlaceHolder ID="plcDefaultTypes" runat="server">
                    <tr>
                        <td>
                            <asp:Label ID="lblLiveSite" runat="server" EnableViewState="false" />
                        </td>
                        <td>
                            <asp:CheckBox ID="chkLiveSite" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblCMSDeskCustomer" runat="server" EnableViewState="false" />
                        </td>
                        <td>
                            <asp:CheckBox ID="chkCMSDeskCustomer" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblCMSDeskOrder" runat="server" EnableViewState="false" />
                        </td>
                        <td>
                            <asp:CheckBox ID="chkCMSDeskOrder" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblCMSDeskOrderItems" runat="server" EnableViewState="false" />
                        </td>
                        <td>
                            <asp:CheckBox ID="chkCMSDeskOrderItems" runat="server" />
                        </td>
                    </tr>
                </asp:PlaceHolder>
                <tr>
                    <td>
                    </td>
                    <td>
                        <cms:CMSButton ID="btnOk" runat="server" OnClick="btnOk_Click" ValidationGroup="CheckoutProcess"
                            CssClass="SubmitButton" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </asp:PlaceHolder>
</div>
