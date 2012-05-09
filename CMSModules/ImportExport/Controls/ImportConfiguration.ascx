<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_ImportExport_Controls_ImportConfiguration"
    CodeFile="ImportConfiguration.ascx.cs" %>
<%@ Register Src="~/CMSModules/Content/Controls/Attachments/DirectFileUploader/DirectFileUploader.ascx"
    TagName="DirectFileUploader" TagPrefix="cms" %>
<cms:CMSUpdatePanel runat="server" ID="pnlUpdate" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:Literal ID="ltlScripts" runat="server" EnableViewState="false" />
        <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" Visible="false" EnableViewState="false" />
        <table class="ImportStepConfiguration">
            <tr>
                <td>
                    <strong><asp:Label runat="server" ID="lblImports" EnableViewState="false" CssClass="InfoLabel" /></strong>
                    <asp:Panel runat="server" ID="pnlRefresh" CssClass="PageHeaderItemRight" Style="padding: 8px 5px">
                        <asp:Image runat="server" ID="imgRefresh" CssClass="NewItemImage" /><cms:LocalizedLinkButton
                            runat="server" ID="btnRefresh" CssClass="NewItemLink" Style="font-size: 12px"
                            ResourceString="general.refresh" OnClick="btnRefresh_Click" />
                    </asp:Panel>
                    <asp:Panel runat="server" ID="pnlLeftActions" CssClass="PageHeaderItemLeft" Style="padding: 8px 5px">
                        <cms:DirectFileUploader ID="newImportPackage" runat="server" />&nbsp;
                        <asp:Image runat="server" ID="imgDelete" CssClass="DeletePackageImage" /><cms:LocalizedLinkButton
                            runat="server" ID="btnDelete" CssClass="DeletePackageLink" ResourceString="importconfiguration.deletepackage"
                            OnClick="btnDelete_Click" />
                    </asp:Panel>
                    <asp:ListBox runat="server" ID="lstImports" CssClass="ContentListBoxLow" Width="450"
                        Enabled="false" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:RadioButton ID="radAll" runat="server" GroupName="Import" Checked="true" CssClass="RadioImport" />
                    <asp:RadioButton ID="radNew" runat="server" GroupName="Import" CssClass="RadioImport" />
                </td>
            </tr>
        </table>
    </ContentTemplate>
</cms:CMSUpdatePanel>
