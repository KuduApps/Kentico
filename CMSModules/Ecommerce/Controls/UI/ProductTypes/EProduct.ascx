<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EProduct.ascx.cs" Inherits="CMSModules_Ecommerce_Controls_UI_ProductTypes_EProduct" %>
<%@ Register TagPrefix="cms" TagName="SelectValidity" Src="~/CMSAdminControls/UI/Selectors/SelectValidity.ascx" %>
<%@ Register TagPrefix="cms" TagName="File" Src="~/CMSModules/AdminControls/Controls/MetaFiles/File.ascx" %>
<%@ Register TagPrefix="cms" TagName="FileList" Src="~/CMSModules/AdminControls/Controls/MetaFiles/FileList.ascx" %>
<asp:Panel ID="pnlEProduct" runat="server">
    <table>
        <%-- E-product validity --%>
        <tr valign="top">
            <td class="FieldLabel" style="width: 150px; padding-top: 4px;">
                <cms:LocalizedLabel runat="server" ResourceString="com.eproduct.filesvalidity" DisplayColon="true" />
            </td>
            <td>
                <cms:SelectValidity ID="selectValidityElem" runat="server" OnOnValidityChanged="selectValidityElem_OnValidityChanged"
                    AutoPostBack="true" />
            </td>
        </tr>
        <%-- New e-product file uploader --%>
        <asp:PlaceHolder ID="plcNewProductUploader" runat="server" Visible="false">
            <tr>
                <td class="FieldLabel">
                    <cms:LocalizedLabel runat="server" EnableViewState="false" ResourceString="com.eproduct.files"
                        DisplayColon="true" />
                </td>
                <td>
                    <cms:File ID="newProductFileUploader" runat="server" Enabled="false" />
                </td>
            </tr>
        </asp:PlaceHolder>
        <%-- Existing e-product files list --%>
        <asp:PlaceHolder ID="plcFileList" runat="server">
            <tr valign="top">
                <td class="FieldLabel" style="padding-top: 4px;">
                    <cms:LocalizedLabel runat="server" ResourceString="com.eproduct.files" DisplayColon="true" />
                </td>
                <td style="width: 450px">
                    <cms:CMSUpdatePanel runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="fileListElem" EventName="OnBeforeDelete" />
                        </Triggers>
                        <ContentTemplate>
                            <cms:LocalizedLabel runat="server" ID="lblAttachmentError" CssClass="ErrorLabel"
                                Visible="false" EnableViewState="false" />
                        </ContentTemplate>
                    </cms:CMSUpdatePanel>
                    <cms:FileList ID="fileListElem" runat="server" UploadLabelVisible="false" AllowEdit="false" />
                </td>
            </tr>
        </asp:PlaceHolder>
    </table>
</asp:Panel>
