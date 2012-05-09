<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Newsletter_Issue_Send.aspx.cs"
    Inherits="CMSModules_Newsletters_Tools_Newsletters_Newsletter_Issue_Send" Theme="Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>Tools - Newsletter issue send</title>
    <style type="text/css">
        body { margin: 0px; padding: 0px; height: 100%; }
        table.SendTable tr td { padding-bottom: 20px; }
    </style>
    <base target="_self" />

</head>
<body class="<%=mBodyClass%>">
    <form id="form1" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="manScript" runat="server" />
    <asp:Panel runat="server" ID="pnlBody" CssClass="TabsPageBody">
        <asp:Panel runat="server" ID="pnlContainer" CssClass="TabsPageContainer">
            <asp:Panel runat="server" ID="pnlScroll" CssClass="TabsPageScrollArea2">
                <asp:Panel runat="server" ID="pnlTab" CssClass="TabsPageContent">
                    <asp:Panel runat="server" ID="pnlContent" CssClass="PageContent" DefaultButton="btnSend">
                        <asp:Label ID="lblError" runat="server" EnableViewState="false" Visible="false" CssClass="ErrorLabel" />
                        <asp:Label ID="lblSent" runat="server" EnableViewState="false" /><br />
                        <br />
                        <table class="SendTable">
                            <tr>
                                <td>
                                    <cms:LocalizedRadioButton ID="radSendNow" runat="server" GroupName="Send" 
                                        ResourceString="Newsletter_Issue_Send.SendNow"
                                        AutoPostBack="true" OnCheckedChanged="radGroupSend_CheckedChanged"
                                        Checked="true"  />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <cms:LocalizedRadioButton ID="radSchedule" runat="server" GroupName="Send"
                                         ResourceString="Newsletter_Issue_Send.Schedule"
                                         AutoPostBack="true" OnCheckedChanged="radGroupSend_CheckedChanged" />
                                    <div class="UnderRadioContent">
                                        <cms:LocalizedLabel ID="lblDateTime" runat="server" 
                                            ResourceString="Newsletter_Issue_Send.DateTime" DisplayColon="true"
                                            AssociatedControlID="calendarControl" EnableViewState="false" />
                                        <cms:DateTimePicker ID="calendarControl" runat="server" Enabled="false"
                                            SupportFolder="~/CMSAdminControls/Calendar" />                                            
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <cms:LocalizedRadioButton ID="radSendDraft" runat="server" GroupName="Send"
                                        ResourceString="Newsletter_Issue_Send.SendDraft" AutoPostBack="true"  
                                        OnCheckedChanged="radGroupSend_CheckedChanged" />
                                    <div class="UnderRadioContent">
                                        <cms:LocalizedLabel ID="lblDraftEmail" runat="server"
                                            ResourceString="Newsletter_Issue_Send.Email" DisplayColon="true"
                                            AssociatedControlID="txtSendDraft" EnableViewState="false" />
                                        <cms:CMSTextBox ID="txtSendDraft" runat="server"
                                            MaxLength="450" Enabled="false" CssClass="TextBoxField" />
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="UnderRadioContent">
                                        <cms:LocalizedButton ID="btnSend" runat="server" CssClass="SubmitButton"
                                            ResourceString="Newsletter_Issue_Send.Send" OnClick="btnSend_Click" Enabled="false"
                                            EnableViewState="false" />
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </asp:Panel>
            </asp:Panel>
        </asp:Panel>
    </asp:Panel>
    </form>
</body>
</html>
