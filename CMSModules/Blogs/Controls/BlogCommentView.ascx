<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_Blogs_Controls_BlogCommentView" CodeFile="BlogCommentView.ascx.cs" %>
<%@ Register Src="~/CMSFormControls/Inputs/SecurityCode.ascx" TagName="SecurityCode" TagPrefix="cms" %>
<%@ Register Src="BlogCommentEdit.ascx" TagName="BlogCommentEdit" TagPrefix="cms" %>
<%@ Register Src="NewSubscription.ascx" TagName="NewSubscription" TagPrefix="cms" %>
<asp:Panel ID="pnlTrackbackURL" runat="server" Visible="false" EnableViewState="false" CssClass="TrackbackPanel" >
    <cms:LocalizedLabel ID="lblURLTitle" runat="server" EnableViewState="false" ResourceString="blog.commentview.trackbackurlentry"
        DisplayColon="true" CssClass="TrackbackLabel" />
    <asp:Label ID="lblURLValue" runat="server" EnableViewState="false" CssClass="TrackbackURL" /><br />
    <br />
</asp:Panel>
<a id="comments"></a>
<asp:Label ID="lblTitle" runat="server" EnableViewState="false" CssClass="BlogCommentsTitle" />
<div>
    <cms:LocalizedLabel ID="lblInfo" runat="server" EnableViewState="false" CssClass="InfoLabel" ResourceString="blog.commentview.nocomments" />
</div>
<div>
    <asp:Repeater ID="rptComments" runat="server" />
</div>
<table class="BlogPanel">
    <tr>
        <td>
            <asp:Panel ID="pnlComment" runat="server">
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <cms:LocalizedLabel ID="lblLeaveCommentLnk" runat="server" EnableViewState="false"
                                CssClass="BlogLeaveComment" ResourceString="blog.commentview.lnkleavecomment" />
                        </td>
                        <asp:PlaceHolder ID="plcBtnSubscribe" runat="server">
                            <td style="text-align: right;">
                                <cms:LocalizedLinkButton ID="btnSubscribe" runat="server" EnableViewState="false"
                                    CssClass="BlogSubscribe" ResourceString="blog.commentview.lnksubscription" />
                            </td>
                        </asp:PlaceHolder>
                    </tr>
                </table>
                <cms:BlogCommentEdit ID="ctrlCommentEdit" runat="server" />
            </asp:Panel>
            <asp:Panel ID="pnlSubscription" runat="server">
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <cms:LocalizedLabel ID="lblNewSubscription" runat="server" EnableViewState="false"
                                CssClass="BlogLeaveComment" ResourceString="blog.commentview.lnksubscription" />
                        </td>
                        <td style="text-align: right;">
                            <cms:LocalizedLinkButton ID="btnLeaveMessage" runat="server" EnableViewState="false"
                                CssClass="BlogSubscribe" ResourceString="blog.commentview.lnkleavemessage" />
                        </td>
                    </tr>
                </table>
                <cms:NewSubscription ID="elemSubscription" runat="server" />
            </asp:Panel>
        </td>
    </tr>
</table>
<asp:HiddenField ID="hdnSelSubsTab" runat="server" />

<script type="text/javascript"> 
<!--
    // Refreshes current page when comment properties are changed in modal dialog window
    function RefreshPage() 
    {         
    
        var url = window.location.href;
        
        // String "#comments" found in url -> trim it
        var charIndex = window.location.href.indexOf('#');
        if (charIndex != -1)
        {
            url = url.substring(0, charIndex);
        }
        
        // Refresh page content
        window.location.replace(url);       
    }
    
    // Switches between edit control and subscription control
    function ShowSubscription(subs, hdnField, elemEdit, elemSubscr) {
      if (hdnField && elemEdit && elemSubscr) 
      {
          var hdnFieldElem = document.getElementById(hdnField);
          var elemEditElem = document.getElementById(elemEdit);
          var elemSubscrElem = document.getElementById(elemSubscr);
          if((hdnFieldElem!=null)&&(elemEditElem!=null)&&(elemSubscrElem!=null))
          {
              if (subs == 1) { // Show subscriber control
                elemEditElem.style.display = 'none';
                elemSubscrElem.style.display = 'block';
              }
                else
              {                // Show edit control
                elemEditElem.style.display = 'block';
                elemSubscrElem.style.display = 'none';
              }
               hdnFieldElem.value = subs
          }
       }      
    }    
-->
</script>

