<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSFormControls_Documents_DocumentURLPath"
    CodeFile="DocumentURLPath.ascx.cs" %>
<cms:CMSUpdatePanel runat="server" ID="pnlUpdate">
    <ContentTemplate>
        <table>
            <asp:PlaceHolder runat="server" ID="plcCustom">
                <tr>
                    <td class="FieldLabel">
                        <cms:LocalizedLabel ID="lblCustom" runat="server" EnableViewState="false" ResourceString="GeneralProperties.UseCustomUrlPath"
                            DisplayColon="true" />
                    </td>
                    <td>
                        <asp:CheckBox ID="chkCustomUrl" runat="server" OnCheckedChanged="chkCustomUrl_CheckedChanged"
                            AutoPostBack="true" />
                    </td>
                </tr>
            </asp:PlaceHolder>
            <tr>
                <td class="FieldLabel" style="width: 125px;">
                    <cms:LocalizedLabel ID="lblType" runat="server" EnableViewState="false" ResourceString="URLPath.Type"
                        DisplayColon="true" />
                </td>
                <td>
                    <asp:Panel runat="server" ID="pnlType">
                        <cms:LocalizedRadioButton runat="server" ID="radPage" ResourceString="URLPath.Standard" Checked="true"
                            GroupName="URL" AutoPostBack="true" />
                        <cms:LocalizedRadioButton runat="server" ID="radRoute" ResourceString="URLPath.Route" GroupName="URL" AutoPostBack="true" />
                        <cms:LocalizedRadioButton runat="server" ID="radMVC" ResourceString="URLPath.MVC" GroupName="URL" AutoPostBack="true" />
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td class="FieldLabel">
                    <cms:LocalizedLabel ID="lblPath" runat="server" EnableViewState="false" ResourceString="URLPath.Path"
                        DisplayColon="true" />
                </td>
                <td>
                    <cms:CMSTextBox ID="txtUrlPath" runat="server" CssClass="TextBoxField" MaxLength="450" />
                </td>
            </tr>
            <asp:PlaceHolder runat="server" ID="plcMVC">
                <tr>
                    <td class="FieldLabel">
                        <cms:LocalizedLabel runat="server" ID="lblController" ResourceString="MVC.DefaultController"
                            DisplayColon="true" />
                    </td>
                    <td>
                        <cms:CMSTextBox runat="server" ID="txtController" CssClass="TextBoxField" />
                    </td>
                </tr>
                <tr>
                    <td class="FieldLabel">
                        <cms:LocalizedLabel runat="server" ID="LocalizedLabel1" ResourceString="MVC.DefaultAction"
                            DisplayColon="true" />
                    </td>
                    <td>
                        <cms:CMSTextBox runat="server" ID="txtAction" CssClass="TextBoxField" />
                    </td>
                </tr>
            </asp:PlaceHolder>
        </table>
    </ContentTemplate>
</cms:CMSUpdatePanel>
