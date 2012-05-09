<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_PortalEngine_UI_WebParts_Development_WebPart_New"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Web parts - New" CodeFile="WebPart_New.aspx.cs" %>

<%@ Register Src="~/CMSFormControls/System/LocalizableTextBox.ascx" TagName="LocalizableTextBox" TagPrefix="cms" %>

<%@ Register Src="~/CMSModules/PortalEngine/Controls/WebParts/SelectWebpart.ascx"
    TagName="SelectWebpart" TagPrefix="cms" %>
<%@ Register Src="~/CMSFormControls/System/UserControlSelector.ascx" TagPrefix="cms"
    TagName="FileSystemSelector" %>
<asp:Content ContentPlaceHolderID="plcContent" runat="server">
    <asp:Panel ID="PanelUsers" runat="server">
        <cms:CMSUpdatePanel ID="pnlUpdate" runat="server">
            <ContentTemplate>
                <asp:Label ID="lblInfo" runat="server" /><asp:Label runat="server" ID="lblError"
                    CssClass="ErrorLabel" EnableViewState="false" Visible="false" />
                <asp:PlaceHolder ID="plcTable" runat="server">
                    <asp:RadioButton runat="server" ID="radNewWebPart" GroupName="wpSelect" Checked="true"
                        OnCheckedChanged="radNewWebPart_CheckedChanged" AutoPostBack="true" />
                    &nbsp;&nbsp;&nbsp;
                    <asp:RadioButton runat="server" ID="radInherited" GroupName="wpSelect" OnCheckedChanged="radNewWebPart_CheckedChanged"
                        AutoPostBack="true" /><br />
                    <br />
                    <table>
                        <tr>
                            <td class="FieldLabel">
                                <cms:LocalizedLabel ID="lbWebPartDisplaytName" runat="server" EnableViewState="False"
                                    ResourceString="general.displayname" DisplayColon="true" />
                            </td>
                            <td>
                                <cms:LocalizableTextBox ID="txtWebPartDisplayName" runat="server" CssClass="TextBoxField" MaxLength="95" />
                                <cms:CMSRequiredFieldValidator ID="rfvWebPartDisplayName" runat="server" EnableViewState="false"
                                    ControlToValidate="txtWebPartDisplayName:textbox" Display="dynamic"></cms:CMSRequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="FieldLabel" style="vertical-align: top">
                                <cms:LocalizedLabel ID="lbWebPartName" runat="server" EnableViewState="False" ResourceString="general.codename"
                                    DisplayColon="true" />
                            </td>
                            <td>
                                <cms:CMSTextBox ID="txtWebPartName" runat="server" CssClass="TextBoxField" MaxLength="95" />
                                <cms:CMSRequiredFieldValidator ID="rfvWebPartName" runat="server" EnableViewState="false"
                                    ControlToValidate="txtWebPartName" Display="dynamic"></cms:CMSRequiredFieldValidator>
                            </td>
                        </tr>
                        <asp:PlaceHolder ID="plcFileName" runat="server">
                            <tr>
                                <td>
                                    <cms:LocalizedLabel ID="lblWebPartFileName" runat="server" ResourceString="general.filename"
                                        DisplayColon="true" />
                                </td>
                                <td>
                                    <cms:FileSystemSelector ID="FileSystemSelector" runat="server" />
                                </td>
                            </tr>
                        </asp:PlaceHolder>
                        <asp:PlaceHolder ID="plcWebparts" runat="server" Visible="false">
                            <tr>
                                <td>
                                    <asp:Label ID="lblWebpartList" runat="server" />
                                </td>
                                <td>
                                    <cms:SelectWebpart ID="webpartSelector" runat="server" />
                                </td>
                            </tr>
                        </asp:PlaceHolder>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <cms:CMSButton ID="btnOk" runat="server" CssClass="SubmitButton" EnableViewState="false"
                                    OnClick="btnOK_Click" />
                            </td>
                        </tr>
                    </table>
                </asp:PlaceHolder>
            </ContentTemplate>
        </cms:CMSUpdatePanel>
    </asp:Panel>
</asp:Content>
