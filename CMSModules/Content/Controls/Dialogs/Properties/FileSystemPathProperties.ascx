<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_Content_Controls_Dialogs_Properties_FileSystemPathProperties" CodeFile="FileSystemPathProperties.ascx.cs" %>

<script type="text/javascript" language="javascript">
    //<![CDATA[
    // -- function insertItem() {
    //    RaiseHiddenPostBack();
    //}%>
    //]]>
</script>

<div class="DialogInfoArea">
    <asp:Literal ID="ltlScript" runat="server" EnableViewState="false" />
    <asp:Label ID="lblEmpty" runat="server" />
    <div class="LeftAlign" style="width: 600px;">
        <cms:CMSUpdatePanel ID="plnPathUpdate" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
                    Visible="false" />
                <table style="vertical-align: top; white-space: nowrap;" width="100%">
                    <asp:PlaceHolder runat="server" ID="plcPathText">
                        <tr>
                            <td>
                                <cms:LocalizedLabel ID="lblPathText" runat="server" EnableViewState="false" ResourceString="general.path"
                                    DisplayColon="true" />
                            </td>
                            <td>
                                <cms:CMSTextBox runat="server" ID="txtPathText" CssClass="VeryLongTextBox" ReadOnly="true" />
                            </td>
                        </tr>
                    </asp:PlaceHolder>
                    <asp:PlaceHolder runat="server" ID="plcFileSize">
                        <tr>
                            <td>
                                <cms:LocalizedLabel ID="lblFileSize" runat="server" EnableViewState="false" ResourceString="media.file.filesize"
                                    DisplayColon="true" />
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblFileSizeText" />
                            </td>
                        </tr>
                    </asp:PlaceHolder>
                </table>
            </ContentTemplate>
        </cms:CMSUpdatePanel>
        <div class="Hidden">
            <cms:CMSUpdatePanel ID="plnFileSystemButtonsUpdate" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Button ID="hdnButton" runat="server" OnClick="hdnButton_Click" CssClass="HiddenButton"
                        EnableViewState="false" />
                </ContentTemplate>
            </cms:CMSUpdatePanel>
        </div>
    </div>
</div>
