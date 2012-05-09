<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_PortalEngine_UI_WebParts_webpartproperties_layout"
    Theme="Default" EnableEventValidation="false" CodeFile="webpartproperties_layout.aspx.cs" %>

<%@ Register Src="~/CMSFormControls/System/LocalizableTextBox.ascx" TagName="LocalizableTextBox"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/PortalEngine/FormControls/WebPartLayouts/WebPartLayoutSelector.ascx"
    TagPrefix="cms" TagName="LayoutSelector" %>
<%@ Register Src="~/CMSAdminControls/UI/PageElements/Help.ascx" TagName="Help" TagPrefix="cms" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>Web part properties - layout</title>
    <style type="text/css">
        body
        {
            margin: 0px;
            height: 100%;
            width: 100%;
            overflow: hidden;
        }
    </style>

    <script type="text/javascript">
        //<![CDATA[

        function RefreshPage() {
            var wopener = parent.wopener;
            if (wopener != null) {
                wopener.RefreshPage();
            }
        }
        //]]>
    </script>

</head>
<body class="TabsBody <%=mBodyClass%>">
    <form id="form1" runat="server">
    <asp:Panel runat="server" ID="pnlBody" CssClass="TabsPageBody">
        <asp:Panel ID="pnlScroll" runat="server" CssClass="TabsPageScrollArea">
            <asp:Panel ID="pnlTab" runat="server" CssClass="TabsPageContent">
                <asp:Panel ID="pnlMenu" runat="server" CssClass="ContentEditMenu">
                    <table>
                        <tr>
                            <td>
                                <asp:LinkButton ID="lnkSave" OnClick="btnSave_Click" runat="server" CssClass="MenuItemEdit">
                                    <asp:Image ID="imgSave" runat="server" EnableViewState="false" />
                                    <%=mSave%>
                                </asp:LinkButton>
                            </td>
                            <asp:PlaceHolder ID="plcCheckOut" runat="server" Visible="false">
                                <td>
                                    <asp:LinkButton ID="btnCheckOut" OnClick="btnCheckOut_Click" runat="server" CssClass="MenuItemEdit">
                                        <asp:Image ID="imgCheckOut" runat="server" EnableViewState="false" />
                                        <%=mCheckOut%>
                                    </asp:LinkButton>
                                </td>
                            </asp:PlaceHolder>
                            <asp:PlaceHolder ID="plcCheckIn" runat="server" Visible="false">
                                <td>
                                    <asp:LinkButton ID="btnCheckIn" OnClick="btnCheckIn_Click" runat="server" CssClass="MenuItemEdit">
                                        <asp:Image ID="imgCheckIn" runat="server" EnableViewState="false" />
                                        <%=mCheckIn%>
                                    </asp:LinkButton>
                                </td>
                            </asp:PlaceHolder>
                            <asp:PlaceHolder ID="plcUndoCheckOut" runat="server" Visible="false" EnableViewState="false">
                                <td>
                                    <asp:LinkButton ID="btnUndoCheckOut" OnClick="btnUndoCheckOut_Click" runat="server"
                                        CssClass="MenuItemEdit">
                                        <asp:Image ID="imgUndoCheckOut" runat="server" EnableViewState="false" />
                                        <%=mUndoCheckOut%>
                                    </asp:LinkButton>
                                </td>
                            </asp:PlaceHolder>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnlFormArea" runat="server" CssClass="PageContent">
                    <asp:Panel ID="pnlCheckOutInfo" runat="server" CssClass="InfoLabel" Visible="false">
                        <asp:Label runat="server" ID="lblCheckOutInfo" />
                        <br />
                    </asp:Panel>
                    <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
                        Visible="false">
                    </asp:Label>
                    <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
                        Visible="false" />
                    <asp:PlaceHolder runat="server" Visible="true" ID="plcContent">
                        <table style="width: 100%">
                            <asp:PlaceHolder runat="server" ID="plcValues" Visible="false">
                                <tr>
                                    <td style="white-space: nowrap;">
                                        <cms:LocalizedLabel ID="lblLayoutDisplayName" runat="server" ResourceString="general.displayname"
                                            DisplayColon="true" />
                                    </td>
                                    <td style="width: 100%">
                                        <cms:LocalizableTextBox ID="txtLayoutDisplayName" runat="server" CssClass="TextBoxField" />
                                    </td>
                                    <asp:PlaceHolder runat="server" ID="plcDescription" Visible="false">
                                        <td rowspan="2">
                                            <cms:LocalizedLabel ID="lblDescription" runat="server" ResourceString="general.description"
                                                DisplayColon="true" />
                                        </td>
                                        <td rowspan="2" style="text-align: right">
                                            <cms:CMSTextBox ID="txtDescription" runat="server" CssClass="TextAreaField" TextMode="MultiLine" />
                                        </td>
                                    </asp:PlaceHolder>
                                </tr>
                                <tr>
                                    <td style="white-space: nowrap;">
                                        <cms:LocalizedLabel ID="lblLayoutName" runat="server" ResourceString="general.codename"
                                            DisplayColon="true" />
                                    </td>
                                    <td>
                                        <cms:CMSTextBox ID="txtLayoutName" runat="server" CssClass="TextBoxField" />
                                    </td>
                                </tr>
                            </asp:PlaceHolder>
                            <asp:PlaceHolder runat="server" ID="plcVirtualInfo" Visible="false">
                                <tr>
                                    <td colspan="4">
                                        <br />
                                        <asp:Label runat="server" ID="lblVirtualInfo" CssClass="ErrorLabel" EnableViewState="false" />
                                    </td>
                                </tr>
                            </asp:PlaceHolder>
                            <tr>
                                <td style="white-space: nowrap;">
                                    <cms:LocalizedLabel runat="server" ID="lblCode" ResourceString="webparteditlayoutedit.lblcode"
                                        DisplayColon="true" EnableViewState="false" />
                                </td>
                                <td colspan="3" style="width: 100%">
                                    <asp:PlaceHolder runat="server" ID="plcHint" Visible="false">
                                        <div class="PlaceholderInfoLine">
                                            <asp:Literal ID="ltlHint" runat="server" EnableViewState="false" /><br />
                                            <br />
                                        </div>
                                    </asp:PlaceHolder>
                                    <cms:ExtendedTextArea ID="etaCode" runat="server" EnableViewState="true" ReadOnly="true"
                                        EditorMode="Advanced" Height="300px" />
                                </td>
                            </tr>
                            <asp:PlaceHolder runat="server" ID="plcCssLink">
                                <tr id="cssLink">
                                    <td class="FieldLabel">
                                    </td>
                                    <td>
                                        <cms:LocalizedLinkButton runat="server" ID="lnkStyles" EnableViewState="false" ResourceString="general.addcss"
                                            OnClientClick="document.getElementById('editCss').style.display = 'table-row'; document.getElementById('cssLink').style.display = 'none'; return false;" />
                                    </td>
                                </tr>
                            </asp:PlaceHolder>
                            <tr id="editCss" style="<%= (plcCssLink.Visible ? "display: none": "") %>">
                                <td class="FieldLabel">
                                    <cms:LocalizedLabel runat="server" ID="lblCSS" ResourceString="Container_Edit.ContainerCSS"
                                        DisplayColon="true" EnableViewState="false" />
                                </td>
                                <td colspan="3">
                                    <cms:ExtendedTextArea ID="etaCSS" runat="server" EnableViewState="true" ReadOnly="true"
                                        EditorMode="Advanced" Language="CSS" Height="200px" />
                                </td>
                            </tr>
                        </table>
                    </asp:PlaceHolder>
                    <asp:Literal ID="ltlScript" runat="server" EnableViewState="false" />
                    <cms:CMSButton ID="btnOnOK" runat="server" Visible="false" />
                    <asp:HiddenField runat="server" ID="hidRefresh" Value="0" />
                </asp:Panel>
            </asp:Panel>
        </asp:Panel>
    </asp:Panel>
    </form>
</body>
</html>
