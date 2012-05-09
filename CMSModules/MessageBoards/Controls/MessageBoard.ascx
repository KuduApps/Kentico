<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_MessageBoards_Controls_MessageBoard"
    CodeFile="MessageBoard.ascx.cs" %>
<%@ Register Src="~/CMSModules/MessageBoards/Controls/Messages/MessageEdit.ascx"
    TagName="BoardMsgEdit" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/MessageBoards/Controls/NewSubscription.ascx" TagName="NewSubscription"
    TagPrefix="cms" %>
<asp:Label ID="lblNoMessages" runat="server" Visible="false" CssClass="InfoLabel"
    EnableViewState="false" />
<cms:QueryRepeater ID="rptBoardMessages" runat="server" />
<br />
<table class="BoardPanel">
    <tr>
        <td>
            <asp:Panel ID="pnlMsgEdit" runat="server">
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <cms:LocalizedLabel ID="lblLeaveMessage" runat="server" EnableViewState="false" CssClass="BoardLeaveMessage" />
                        </td>
                        <asp:PlaceHolder ID="plcBtnSubscribe" runat="server">
                            <td style="text-align: right;">
                                <asp:LinkButton ID="btnSubscribe" runat="server" EnableViewState="false" CssClass="BoardSubscribe" />
                            </td>
                        </asp:PlaceHolder>
                    </tr>
                </table>
                <cms:BoardMsgEdit ID="msgEdit" runat="server" AdvancedMode="false" Visible="true" />
            </asp:Panel>
            <asp:Panel ID="pnlMsgSubscription" runat="server" EnableViewState="false">
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <cms:LocalizedLabel ID="lblNewSubscription" runat="server" EnableViewState="false"
                                CssClass="BoardLeaveMessage" />
                        </td>
                        <td style="text-align: right;">
                            <asp:LinkButton ID="btnLeaveMessage" runat="server" EnableViewState="false" CssClass="BoardSubscribe" />
                        </td>
                    </tr>
                </table>
                <cms:NewSubscription ID="msgSubscription" runat="server" EnableViewState="true" />
            </asp:Panel>
        </td>
    </tr>
</table>
<asp:HiddenField ID="hdnSelSubsTab" runat="server" />
<asp:Button ID="btnRefresh" runat="server" EnableViewState="false" Style="display: none;"
    OnClick="btnRefresh_Click" />
<asp:Literal ID="ltlScript" runat="server" EnableViewState="false" />

<script type="text/javascript">
    //<![CDATA[
    // Switches between edit control and subscription control
    function ShowSubscription(subs, hdnField, elemEdit, elemSubscr) {
        if (hdnField && elemEdit && elemSubscr) {
            var hdnFieldElem = document.getElementById(hdnField);
            var elemEditElem = document.getElementById(elemEdit);
            var elemSubscrElem = document.getElementById(elemSubscr);
            if ((hdnFieldElem != null) && (elemEditElem != null) && (elemSubscrElem != null)) {
                if (subs == 1) { // Show subscriber control
                    elemEditElem.style.display = 'none';
                    elemSubscrElem.style.display = 'block';
                }
                else {                // Show edit control
                    elemEditElem.style.display = 'block';
                    elemSubscrElem.style.display = 'none';
                }
                hdnFieldElem.value = subs
            }
        }
    }

    // Opens modal dialog with comment edit page
    function EditBoardMessage(editPageUrl) {
        modalDialog(editPageUrl, "BoardMessageEdit", 500, 450);
    }
    //]]> 
</script>

