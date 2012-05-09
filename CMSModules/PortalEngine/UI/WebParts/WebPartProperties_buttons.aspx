<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_PortalEngine_UI_WebParts_WebPartProperties_buttons"
    Theme="default" CodeFile="WebPartProperties_buttons.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>Web part properties</title>
    <style type="text/css">
        body
        {
            margin: 0px;
            padding: 0px;
            height: 100%;
            background-color: #f5f3ec;
        }
    </style>
</head>
<body class="<%=mBodyClass%> Buttons">
    <form id="form1" runat="server">
    <asp:Panel runat="server" ID="pnlScroll" CssClass="ButtonPanel">
        <table style="width: 100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="TextLeft">
                    &nbsp;&nbsp;
                    <asp:CheckBox runat="server" ID="chkRefresh" Checked="true" />
                </td>
                <td class="TextRight">
                    <cms:CMSButton ID="btnOk" runat="server" CssClass="SubmitButton" /><cms:CMSButton
                        ID="btnCancel" runat="server" CssClass="SubmitButton" /><cms:CMSButton ID="btnApply"
                            runat="server" CssClass="SubmitButton" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    </form>
    <asp:Literal runat="server" ID="ltlScript" EnableViewState="false" />

    <script type="text/javascript">
        //<![CDATA[
        function Apply() {
            if (parent.frames['webpartpropertiescontent'].OnApplyButton) {
                parent.frames['webpartpropertiescontent'].OnApplyButton(GetRefreshStatus());
            }
            else if (parent.frames['webpartpropertiescontent'].frames['webpartlayoutcontent'].OnApplyButton) {
                parent.frames['webpartpropertiescontent'].frames['webpartlayoutcontent'].OnApplyButton(GetRefreshStatus());
            }
        }

        function Save() {
            if (parent.frames['webpartpropertiescontent'].OnOKButton) {
                parent.frames['webpartpropertiescontent'].OnOKButton(GetRefreshStatus());
            }
            else if (parent.frames['webpartpropertiescontent'].frames['webpartlayoutcontent'].OnOKButton) {
                parent.frames['webpartpropertiescontent'].frames['webpartlayoutcontent'].OnOKButton(GetRefreshStatus());
            }
        }

        function Close() {
            top.window.close();
        }
        //]]>
    </script>

</body>
</html>
