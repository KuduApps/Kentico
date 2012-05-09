<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Content_CMSDesk_View_ViewFile"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Content - View file"
    CodeFile="ViewFile.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plcContent" runat="server">
    <asp:Panel ID="pnlContent" runat="server" CssClass="PageContent">
        <table>
            <tr>
                <td>
                    <cms:LocalizedLabel ID="lblFileName" runat="server" EnableViewState="false" ResourceString="general.filename"
                        DisplayColon="true" />
                </td>
                <td>
                    <asp:Label ID="lblFileNameText" runat="server" EnableViewState="false" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblFileSize" runat="server" EnableViewState="false" />
                </td>
                <td>
                    <asp:Label ID="lblFileSizeText" runat="server" EnableViewState="false" />
                </td>
            </tr>
            <asp:PlaceHolder ID="plcSize" runat="server" Visible="false" EnableViewState="false">
                <tr>
                    <td>
                        <asp:Label ID="lblSize" runat="server" EnableViewState="false" />
                    </td>
                    <td>
                        <asp:Label ID="lblSizeText" runat="server" EnableViewState="false" />
                    </td>
                </tr>
            </asp:PlaceHolder>
            <asp:PlaceHolder ID="plcImage" runat="server" Visible="false" EnableViewState="false">
                <tr>
                    <td>
                    </td>
                    <td class="PreviewImage">
                        <asp:Image ID="imgPreview" runat="server" EnableViewState="false" />
                    </td>
                </tr>
            </asp:PlaceHolder>
            <tr>
                <td>
                </td>
                <td>
                    <asp:HyperLink ID="lnkView" runat="server" Target="_blank" EnableViewState="false" />
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
