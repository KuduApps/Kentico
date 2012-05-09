<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_PortalEngine_UI_Layout_SaveNewPageTemplate" Theme="Default"
    MasterPageFile="~/CMSMasterPages/UI/Dialogs/ModalDialogPage.master" Title="Page template - Save as new" CodeFile="SaveNewPageTemplate.aspx.cs" %>

<%@ Register Src="~/CMSFormControls/System/LocalizableTextBox.ascx" TagName="LocalizableTextBox" TagPrefix="cms" %>

<%@ Register Src="~/CMSModules/PortalEngine/Controls/PageTemplates/SelectPageTemplate.ascx"
    TagName="SelectPageTemplate" TagPrefix="cms" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">

    <script type="text/javascript">
        //<![CDATA[          
        function SelectActualData(pageTemplateId, isPortal, isReusable) {
            var TemplateDisplayName = document.getElementById('<%=txtTemplateDisplayName.TextBox.ClientID %>').value;
            TemplateDesctiption = document.getElementById('<%=txtTemplateDescription.ClientID %>').value;
            TemplateCategory = document.getElementById('<%=categorySelector.DropDownList.ClientID %>').value;

            wopener.ReceiveNewTemplateData(TemplateDisplayName, TemplateCategory, TemplateDesctiption, pageTemplateId, isPortal, isReusable);
            window.close();
        }

        function Cancel() {
            wopener.SelectTemplate("", 0, "");
            window.close();
        }
        //]]>
    </script>

    <div class="PageContent">
        <table style="width: 100%">
            <tr>
                <td colspan="2">
                    <asp:Label ID="lblError" runat="server" ForeColor="Red" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblTemplateDisplayName" runat="server" CssClass="ContentLabel" />
                </td>
                <td>
                    <cms:LocalizableTextBox ID="txtTemplateDisplayName" MaxLength="200" runat="server" CssClass="TextBoxField" /><br />
                    <cms:CMSRequiredFieldValidator ID="rfvTemplateDisplayName" runat="server" ControlToValidate="txtTemplateDisplayName:textbox"
                        Display="Dynamic"></cms:CMSRequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblTemplateCodeName" runat="server" CssClass="ContentLabel" />
                </td>
                <td>
                    <cms:CMSTextBox ID="txtTemplateCodeName" MaxLength="100" runat="server" CssClass="TextBoxField" /><br />
                    <cms:CMSRequiredFieldValidator ID="rfvTemplateCodeName" runat="server" ControlToValidate="txtTemplateCodeName"
                        Display="Dynamic"></cms:CMSRequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblTemplateCategory" runat="server" CssClass="ContentLabel" />
                </td>
                <td>
                    <cms:SelectPageTemplate ID="categorySelector" runat="server" ShowTemplates="false"
                        EnableCategorySelection="true" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblTemplateDescription" runat="server" CssClass="ContentLabel" />
                </td>
                <td>
                    <cms:CMSTextBox ID="txtTemplateDescription" runat="server" TextMode="MultiLine" CssClass="TextAreaField" />
                </td>
            </tr>
        </table>
    </div>
    <asp:Literal ID="ltlScript" runat="server" />
</asp:Content>
<asp:Content ID="cntFooter" ContentPlaceHolderID="plcFooter" runat="server">
    <div class="FloatRight">
        <cms:LocalizedButton ID="btnOk" runat="server" CssClass="SubmitButton" EnableViewState="False"
            OnClick="btnOK_Click" /><cms:LocalizedButton ID="btnCancel" runat="server" CssClass="SubmitButton"
                EnableViewState="False" OnClientClick="Cancel()" />
    </div>
</asp:Content>
