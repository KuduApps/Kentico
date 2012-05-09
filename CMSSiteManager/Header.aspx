<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSSiteManager_Header" Theme="Default"
    CodeFile="Header.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/UniSelector/UniSelector.ascx" TagName="UniSelector"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/UI/PageElements/FrameResizer.ascx" TagName="FrameResizer"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/UI/HeaderLinks.ascx" TagName="HeaderLinks" TagPrefix="cms" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>CMSSiteManager - Header</title>
    <style type="text/css">
        body
        {
            margin: 0px;
            padding: 0px;
            height: 100%;
        }
    </style>
</head>
<body class="<%=mBodyClass%>">
    <form id="form1" runat="server">
    <asp:PlaceHolder ID="plcScriptManager" runat="server" />
    <asp:Literal ID="ltlFBConnectScript" runat="server" EnableViewState="false" />
    <cms:FrameResizer ID="frmResizer" runat="server" MinSize="0, 0, 6, *" Vertical="True"
        CssPrefix="Vertical" />
    <asp:Panel runat="server" ID="PanelHeader" CssClass="SM_Header">
        <asp:HyperLink runat="server" ID="lnkCmsDeskLogo" CssClass="SM_HeaderLeft" EnableViewState="false"
            Font-Underline="false">
            &nbsp;
        </asp:HyperLink>
        <asp:Panel runat="server" ID="PanelTabs" CssClass="HeaderTabs">
            <cms:BasicTabControl ID="BasicTabControlHeader" ShortID="t" runat="server" UseClientScript="true"
                RenderLinks="true" />
        </asp:Panel>
        <asp:Panel runat="server" ID="PanelRight" CssClass="HeaderRight" EnableViewState="false">
            <table cellpadding="0" cellspacing="0" class="RightAlign">
                <tr>
                    <td style="padding: 0px 5px">
                        <asp:HyperLink ID="lnkTestingMode" CssClass="HeaderLink" runat="server" Visible="false" />
                    </td>
                    <td style="padding: 0px 5px">
                        <asp:HyperLink ID="lnkCmsDesk" CssClass="HeaderLink" runat="server" />
                    </td>
                    <td style="padding: 0px 5px">
                        <asp:Label ID="lblUser" runat="server" CssClass="HeaderUser" />
                    </td>
                    <td>
                        <asp:Label ID="lblUserInfo" runat="server" CssClass="HeaderUserInfo" />
                    </td>
                    <asp:PlaceHolder ID="pnlUsers" runat="server">
                        <td>
                            <div class="Hidden">
                                <cms:UniSelector ID="ucUsers" ObjectType="CMS.User" ResourcePrefix="selectuser" runat="server"
                                    ReturnColumnName="UserName" SelectionMode="SingleButton" IsLiveSite="false" DisplayNameFormat="##USERDISPLAYFORMAT##" />
                            </div>
                            &nbsp;
                            <cms:ContextMenuContainer runat="server" ID="menuCont">
                                <asp:ImageButton runat="server" ID="imgImpersonate" />
                            </cms:ContextMenuContainer>
                        </td>
                    </asp:PlaceHolder>
                    <td style="padding: 0px 10px;">
                        <asp:Label runat="server" ID="lblVersion" EnableViewState="false" CssClass="HeaderVersion" />
                    </td>
                    <cms:HeaderLinks ID="elemLinks" runat="server" />
                    <td>
                        <asp:Panel runat="server" ID="pnlSignOut" CssClass="HeaderSignOutPnl" EnableViewState="false">
                            <asp:LinkButton runat="server" ID="lnkSignOut" OnClick="btnSignOut_Click" Font-Underline="false"
                                EnableViewState="false">
                                <asp:Label runat="server" ID="lblSignOut" EnableViewState="false" CssClass="HeaderSignOut" />
                            </asp:LinkButton>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
            <asp:Literal ID="ltlScript" runat="server" EnableViewState="false" />
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlExtraIcons" Visible="false" CssClass="ExtraIcons">
            <asp:Image runat="server" ID="imgEnterpriseSolution" Visible="false" />
            <asp:Image runat="server" ID="imgWindowsAzure" Visible="false" />
        </asp:Panel>
        <asp:Panel runat="server" ID="PanelSeparator" CssClass="SM_HeaderContentSeparator"
            EnableViewState="false">
            &nbsp;
        </asp:Panel>
    </asp:Panel>
    </form>
</body>
</html>
