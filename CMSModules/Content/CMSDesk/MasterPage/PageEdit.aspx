<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Content_CMSDesk_MasterPage_PageEdit"
    Theme="Default" ValidateRequest="false" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master"
    Title="Master page - page edit" CodeFile="PageEdit.aspx.cs" %>

<%@ Register Src="~/CMSFormControls/Inputs/LargeTextArea.ascx" TagName="LargeTextArea"
    TagPrefix="cms" %>
<asp:Content ContentPlaceHolderID="plcContent" runat="server">
    <script type="text/javascript">
        //<![CDATA[
        parent.SetTabsContext('master');
        //]]>
    </script>
    
    <cms:CMSButton ID="btnSave" runat="server" CssClass="HiddenButton" EnableViewState="false" OnClick="btnSave_Click" />
    <input type="hidden" name="saveChanges" id="saveChanges" value="0" />
    <table cellpadding="0" cellspacing="0" style="width: 100%;">
        <tr>
            <td style="">
                <asp:Label ID="lblInfo" runat="server" CssClass="InfoLabel" Visible="false" EnableViewState="false" />
                <asp:Label ID="lblError" runat="server" CssClass="ErrorLabel" Visible="false" EnableViewState="false" />
                <cms:CMSTextBox runat="server" ID="txtDocType" Width="90%" TextMode="MultiLine" Rows="1" /><br />
                <asp:Label runat="server" ID="lblAfterDocType" EnableViewState="false" CssClass="HTMLCode" />
            </td>
        </tr>
        <tr>
            <td>
                <span class="HTMLCode">
                    <%=mHead%></span>
                <cms:LargeTextArea ID="txtHeadTags" runat="server" Width="90%" Rows="2" AllowMacros="false" />
                <br />
                <asp:Label runat="server" ID="lblAfterHeadTags" EnableViewState="false" CssClass="HTMLCode" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label runat="server" ID="lblBodyStart" EnableViewState="false" CssClass="HTMLCode" />
                <cms:CMSTextBox runat="server" ID="txtBodyCss" Width="60%" /><asp:Label runat="server" ID="lblBodyEnd" EnableViewState="false" CssClass="HTMLCode" />
            </td>
        </tr>
        <tr>
            <td>
                <span class="HTMLCode">
                    <%=mBeforeLayout%></span>
                <asp:Label runat="server" ID="lblChecked" Visible="false" CssClass="ErrorLabel" EnableViewState="false" />
                <div class="MasterPageLayout">
                    <div class="PlaceholderHeaderLine" style="border: none;">
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td class="FieldLabel" style="vertical-align: middle">
                                    <asp:Label ID="lblType" runat="server" EnableViewState="false" />&nbsp;
                                </td>
                                <td>
                                    <asp:DropDownList runat="server" ID="drpType" AutoPostBack="true" />
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="PlaceholderInfoLine">
                        <asp:PlaceHolder runat="server" ID="plcHint">
                            <asp:Label runat="server" ID="lblLayoutInfo" CssClass="InfoLabel" EnableViewState="false" />
                            <asp:Label runat="server" ID="lblLayoutError" CssClass="ErrorLabel" EnableViewState="false"
                                Visible="false" />
                        </asp:PlaceHolder>
                        <asp:PlaceHolder runat="server" ID="plcDirectives" Visible="false">
                            <asp:Label runat="server" ID="ltlDirectives" EnableViewState="false" CssClass="LayoutDirectives" /><br />
                        </asp:PlaceHolder>
                        <asp:PlaceHolder runat="server" ID="plcVirtualInfo" Visible="false">
                            <br />
                            <asp:Label runat="server" ID="lblVirtualInfo" CssClass="ErrorLabel" EnableViewState="false" />
                        </asp:PlaceHolder>
                    </div>
                    <cms:ExtendedTextArea runat="server" ID="txtLayout" EnableViewState="false"
                        EditorMode="Advanced" Width="100%" Height="300" />
                </div>
                <asp:Label runat="server" ID="lblAfterLayout" EnableViewState="false" CssClass="HTMLCode" />
                <span class="HTMLCode">
                    <%=mAfterLayout%></span>
                <br />
            </td>
        </tr>
    </table>
</asp:Content>
