<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSWebParts_Events_EventManager" CodeFile="~/CMSWebParts/Events/EventManager.ascx.cs" %>
<asp:Label runat="server" ID="lblInfo" CssClass="EventManagerInfo" Visible="false"
    EnableViewState="false" />
<asp:Panel runat="server" ID="pnlControl">
    <div class="EventManagerRegistration">
        <asp:Label runat="server" ID="lblRegTitle" CssClass="EventManagerRegTitle" EnableViewState="false" Visible="false" />
        <asp:Label runat="server" ID="lblRegInfo" CssClass="EventManagerRegInfo" EnableViewState="false"
            Visible="false" />
        <asp:Label runat="server" ID="lblError" CssClass="EventManagerRegError" EnableViewState="false"
            Visible="false" />
        <asp:Panel runat="server" ID="pnlReg">
            <table>
                <asp:PlaceHolder runat="server" ID="plcName">
                    <tr>
                        <td>
                            <cms:LocalizedLabel runat="server" ID="lblFirstName" CssClass="EventManagerRegLabel"
                                EnableViewState="false" ResourceString="eventmanager.firstname" AssociatedControlID="txtFirstName" />
                        </td>
                        <td>
                            <cms:CMSTextBox runat="server" ID="txtFirstName" CssClass="EventManagerRegText" EnableViewState="false" MaxLength="100" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <cms:LocalizedLabel runat="server" ID="lblLastName" CssClass="EventManagerRegLabel"
                                EnableViewState="false" ResourceString="eventmanager.lastname" AssociatedControlID="txtLastName" />
                        </td>
                        <td>
                            <cms:CMSTextBox runat="server" ID="txtLastName" EnableViewState="false" CssClass="EventManagerRegText" MaxLength="100" />
                        </td>
                    </tr>
                </asp:PlaceHolder>
                <tr>
                    <td>
                        <cms:LocalizedLabel runat="server" ID="lblEmail" CssClass="EventManagerRegLabel"
                            EnableViewState="false" ResourceString="general.email" AssociatedControlID="txtEmail" DisplayColon="true" />
                    </td>
                    <td>
                        <cms:CMSTextBox runat="server" ID="txtEmail" EnableViewState="false" CssClass="EventManagerRegText" MaxLength="250" />
                    </td>
                </tr>
                <asp:PlaceHolder runat="server" ID="plcPhone">
                    <tr>
                        <td>
                            <cms:LocalizedLabel runat="server" ID="lblPhone" CssClass="EventManagerRegLabel"
                                EnableViewState="false" AssociatedControlID="txtPhone" ResourceString="eventmanager.phone" />
                        </td>
                        <td>
                            <cms:CMSTextBox runat="server" ID="txtPhone" EnableViewState="false" CssClass="EventManagerRegText" MaxLength="50" />
                        </td>
                    </tr>
                </asp:PlaceHolder>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <cms:LocalizedButton runat="server" ID="btnRegister" CssClass="EventManagerRegButton"
                            OnClick="btnRegister_Click" EnableViewState="false" ResourceString="eventmanager.buttonregister" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:HyperLink runat="server" ID="lnkOutlook" CssClass="EventManagerOutlookLink"
            EnableViewState="false" Visible="true" />
    </div>
</asp:Panel>
