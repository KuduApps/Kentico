<%@ Page Title="Search" Language="C#" MasterPageFile="~/CMSMasterPages/UI/Dialogs/ModalDialogPage.master" Theme="Default"
    AutoEventWireup="true" CodeFile="EditSource.aspx.cs" Inherits="CMSAdminControls_CodeMirror_dialogs_EditSource" %>        

<asp:Content ID="plcContentContent" ContentPlaceHolderID="plcContent" runat="server">

     <script type="text/javascript">     
         function getSource() {
             if (window.location.search.indexOf("editorName=") == 1) {
                 var editorName = window.location.search.substring(12, window.location.search.length);
                 var editor = window.opener[editorName];
                 document.getElementById('<%= txtSource.ClientID %>').value = editor.getValue();
             }
         }
         function setSource() {
             if (window.location.search.indexOf("editorName=") == 1) {
                 var editorName = window.location.search.substring(12, window.location.search.length);
                 var editor = window.opener[editorName];
                 editor.setValue(document.getElementById('<%= txtSource.ClientID %>').value);
                 var inputElem = editor.getInputField();
                 if (inputElem.onchange) {
                     inputElem.onchange();
                 }
             }
         }
         function focusOnTextBox(textBoxId) {
             var txtBox = document.getElementById(textBoxId);
             if (txtBox != null) {
                 try {
                     txtBox.focus();
                 }
                 catch (e) {
                 }
             }
         }         
         function doResize() {
             var textBox = document.getElementById('<%= txtSource.ClientID %>');
             var container = document.getElementById('divContent');
             textBox.style.height = (container.clientHeight - 6) + 'px';
             textBox.style.width = (container.clientWidth - 6) + 'px';
         }
     </script>
          
     <cms:CMSTextBox runat="server" ID="txtSource" TextMode="MultiLine" Width="800px" Height="550px" CssClass="SHEditorDialogCodeTextBox"/>     
     
    <script type="text/javascript">        
        setInterval('doResize();', 500);
        getSource();
        focusOnTextBox('<%= txtSource.ClientID %>');
    </script>
             
</asp:Content>

<asp:Content ID="plcFooterContent" ContentPlaceHolderID="plcFooter" runat="server">    
    <div class="FloatRight">        
        <cms:LocalizedButton ID="btnOk" runat="server" CssClass="SubmitButton" OnClientClick="setSource(); window.close();" ResourceString="general.ok" />
        <cms:LocalizedButton ID="btnCancel" runat="server" CssClass="SubmitButton" OnClientClick="window.close();" ResourceString="general.cancel" />
    </div>
</asp:Content>
