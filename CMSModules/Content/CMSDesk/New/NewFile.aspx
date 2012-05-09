<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Content_CMSDesk_New_NewFile"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Content - New file" CodeFile="NewFile.aspx.cs" %>

<%@ Register Src="~/CMSModules/Content/Controls/Attachments/DocumentAttachments/DirectUploader.ascx"
    TagName="DirectFileUploader" TagPrefix="cms" %>
    
<asp:Content ContentPlaceHolderID="plcContent" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="ucManager" runat="server" />    
    <asp:Literal ID="ltlSpellScript" runat="server" EnableViewState="false" />

    <script type="text/javascript">
        //<![CDATA[
        // Passive refresh
        function PassiveRefresh(nodeId, parentNodeId) {
            parent.PassiveRefresh(nodeId, parentNodeId);
        }

        // Refresh action
        function RefreshTree(nodeId, selectNodeId) {
            return parent.RefreshTree(nodeId, selectNodeId);
        }

        function SelectNode(nodeId) {
            return parent.SelectNode(nodeId);
        }

        // Create another
        function CreateAnother() {
            parent.CreateAnother();
        }

        // File created
        function FileCreated(nodeId, parentNodeId, closeWindow) {
            parent.FileCreated(nodeId, parentNodeId, closeWindow);
            if (closeWindow) {
                top.window.close();
            }
        }

        //]]>
    </script>

    <div class="PageContent">
        <asp:Label ID="lblInfo" runat="server" CssClass="ContentLabel" EnableViewState="false" /><asp:Label
            ID="lblError" runat="server" EnableViewState="false" ForeColor="Red"></asp:Label>
        <asp:Panel ID="pnlForm" runat="server">
            <table>
                <tr>
                    <td>
                        <asp:Label ID="lblUploadFile" runat="server" EnableViewState="false" />
                    </td>
                    <td>
                        <asp:PlaceHolder ID="plcDirect" runat="server">
                            <cms:DirectFileUploader ID="ucDirectUploader" runat="server" CheckPermissions="false" />
                        </asp:PlaceHolder>
                        <asp:PlaceHolder ID="plcUploader" runat="server" Visible="false">
                            <cms:CMSFileUpload ID="FileUpload" runat="server" Width="456px" />
                        </asp:PlaceHolder>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblFileDescription" runat="server" EnableViewState="false" />
                    </td>
                    <td>
                        <cms:CMSTextBox ID="txtFileDescription" runat="server" MaxLength="500" TextMode="MultiLine"
                            Rows="6" Width="450px"></cms:CMSTextBox >
                    </td>
                </tr>
            </table>
            <cms:CMSButton ID="btnOk" runat="server" CssClass="HiddenButton" OnClick="btnOk_Click" />
        </asp:Panel>
        <input type="hidden" id="hidAnother" name="hidAnother" value="" />
        <asp:Literal ID="ltlScript" runat="server" EnableViewState="False" />
    </div>
</asp:Content>
