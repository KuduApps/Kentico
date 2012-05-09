<%@ Page Language="C#" Inherits="CMSAdminControls_Calendar_Calendar"
    Theme="Default" CodeFile="Calendar.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>
        <%=pagetitle%>
    </title>
    <style type="text/css">
        body
        {
            margin: 0px;
            padding: 0px;
            height: 100%;
        }
        .ContentButton
        {
            margin: 0px 5px !important;
        }
    </style>
</head>
<body class="<%=mBodyClass%>">
    <form id="Form1" method="post" runat="server">

    <script type="text/javascript">
        //<![CDATA[
        function CloseWindow(destinationid, value) {
            var elem = wopener.document.getElementById(destinationid);
            elem.value = value;
            if(typeof elem.onchange == "function"){ elem.onchange(); }
            window.close();
        }
        //]]>
    </script>

    <asp:Literal ID="ltlScript" runat="server" EnableViewState="false" />
    <asp:Panel ID="pnlBody" runat="server">
        <asp:Calendar ID="calDate" runat="server" OnSelectionChanged="calDate_SelectionChanged"
            Width="100%" CssClass="CalendarTable" TitleStyle-BackColor="#B5C3D6" BorderColor="#B5C3D6"
            SelectedDayStyle-CssClass="CalendarDaySelected" SelectedDayStyle-ForeColor="Black"
            NextPrevStyle-CssClass="CalendarNextPrev" DayStyle-CssClass="CalendarDay" CellPadding="1">
            <TitleStyle CssClass="UniGridHead" BorderColor="#B5C3D6" Font-Bold="true" />
        </asp:Calendar>
        <div class="CalendarBottom">
            <table style="width: 100%" id="tblLayout" runat="server">
                <tr>
                    <td style="white-space: nowrap; vertical-align: bottom;">
                        <asp:Panel runat="server" ID="pnlMonth">
                            <asp:DropDownList ID="drpMonth" runat="server" Width="40px" AutoPostBack="True" OnSelectedIndexChanged="drpMonth_SelectedIndexChanged" />
                            /
                            <asp:DropDownList ID="drpYear" runat="server" Width="60px" AutoPostBack="True" OnSelectedIndexChanged="drpYear_SelectedIndexChanged" />
                        </asp:Panel>
                    </td>
                    <td rowspan="1" style="text-align: right; vertical-align: bottom">
                        &nbsp;<cms:CMSButton ID="btnNow" runat="server" CssClass="ContentButton" OnClick="btnNow_Click" />
                    </td>
                </tr>
                <tr>
                    <td style="white-space: nowrap; vertical-align: bottom;">
                        <asp:Panel runat="server" ID="pnlTime">
                            <asp:DropDownList ID="drpHours" runat="server" Width="40px" />
                            :
                            <asp:DropDownList ID="drpMinutes" runat="server" Width="40px" />
                            :
                            <asp:DropDownList ID="drpSeconds" runat="server" Width="40px" />
                        </asp:Panel>
                    </td>
                    <td class="CalendarBottomInfo">
                        <asp:Label ID="lblGMTShift" runat="server" EnableViewState="false" Visible="false" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="PageFooterLine">
            <div class="FloatRight">
                <cms:CMSButton ID="btnNA" runat="server" CssClass="SubmitButton" /><cms:CMSButton
                    ID="btnOk" runat="server" CssClass="SubmitButton" OnClick="btnOK_Click" /><cms:CMSButton
                        ID="btnCancel" runat="server" CssClass="SubmitButton" />
            </div>
        </div>
    </asp:Panel>
    </form>
</body>
</html>
