<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_Widgets_LiveDialogs_WidgetProperties_Buttons" Theme="default" CodeFile="WidgetProperties_Buttons.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>Widget properties - Buttons</title>
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
    <div class="LiveSiteDialog">
        <asp:Panel runat="server" ID="pnlScroll" CssClass="PageFooterLine">
            <div class="FloatRight">
                <cms:CMSButton ID="btnOk" runat="server" CssClass="SubmitButton" /><cms:CMSButton
                    ID="btnCancel" runat="server" CssClass="SubmitButton" /><cms:CMSButton ID="btnApply"
                        runat="server" CssClass="SubmitButton" />
            </div>
        </asp:Panel>
    </div>
    <asp:Literal runat="server" ID="ltlScript" EnableViewState="false" />
    </form>

    <script type="text/javascript">
        //<![CDATA[
        function Apply() {
            parent.frames['widgetpropertiescontent'].OnApplyButton(true);
        }

        function Save() {
            //If redirected to not allowed site, no parent frames OnOKButton exists
            //If OK just close the window
            if (parent.frames['widgetpropertiescontent'].OnOKButton) {
                parent.frames['widgetpropertiescontent'].OnOKButton(true);
            }
            else {
                top.window.close();
            }
        }

        function Close() {
            top.window.close();
        }
        //]]>
    </script>

</body>
</html>
