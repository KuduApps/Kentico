<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSFormControls_Selectors_FieldSelection"
    Title="Field selection" ValidateRequest="false" Theme="default" MasterPageFile="~/CMSMasterPages/UI/Dialogs/ModalDialogPage.master"
    CodeFile="FieldSelection.aspx.cs" %>

<%@ Register Src="~/CMSFormControls/Classes/SelectClassNames.ascx" TagPrefix="cms"
    TagName="SelectClassNames" %>
<asp:Content ID="cntBody" ContentPlaceHolderID="plcContent" runat="server">
    <cms:CMSUpdatePanel ID="pnlUpdate" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="pnlContent" CssClass="PageContent" runat="server">
                <table>
                    <tr>
                        <td>
                            <cms:LocalizedLabel ID="lblClassNames" runat="server" CssClass="ContentLabel" ResourceString="objecttype.cms_documenttype"
                                DisplayColon="true" />
                        </td>
                        <td>
                            <cms:SelectClassNames ID="selectionElem" runat="server" ShowOnlyCoupled="true" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <cms:LocalizedLabel ID="lblField" runat="server" CssClass="ContentLabel" ResourceString="attach.field"
                                DisplayColon="true" />
                        </td>
                        <td>
                            <asp:DropDownList ID="drpField" runat="server" CssClass="DropDownField" AutoPostBack="false" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
    </cms:CMSUpdatePanel>
</asp:Content>
<asp:Content ID="plcFooter" ContentPlaceHolderID="plcFooter" runat="server">
    <cms:CMSUpdatePanel ID="pnlUpdateButtons" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="FloatRight">
                <cms:LocalizedButton ID="btnOk" runat="server" CssClass="SubmitButton" ResourceString="general.ok" /><cms:LocalizedButton
                    ID="btnCancel" runat="server" CssClass="SubmitButton" ResourceString="general.cancel" />
            </div>
        </ContentTemplate>
    </cms:CMSUpdatePanel>
</asp:Content>
