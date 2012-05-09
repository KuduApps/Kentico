<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_Newsletters_Tools_Newsletters_Newsletter_Preview" CodeFile="Newsletter_Preview.ascx.cs" %>

<script type="text/javascript">
//<![CDATA[
    var currentSubscriberIndex = 0;
    var newsletterIssueId = 0;
//]]>
</script>

<asp:Literal ID="ltlScript" runat="server" EnableViewState="false" />
<asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
    Visible="false" />
<asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
    Visible="false" />

<asp:Panel ID="pnlLinkButtons" runat="server" Width="100%" EnableViewState="false">
    <table cellpadding="0" cellspacing="0" style="margin-bottom: 15px">
        <tr>
            <td>
                <asp:Label ID="lblSubscriber" runat="server" EnableViewState="false" />
            </td>
            <td align="right">
                <asp:LinkButton ID="lnkPrevious" runat="server" OnClientClick="getPreviousSubscriber(); return false;" />
                <asp:Label ID="lblPrevious" runat="server" EnableViewState="false" />
            </td>
            <td style="width: 300px;" align="center">
                <asp:Label ID="lblEmail" runat="server" EnableViewState="false" />
            </td>
            <td align="left">
                <asp:LinkButton ID="lnkNext" runat="server" OnClientClick="getNextSubscriber(); return false;" />
                <asp:Label ID="lblNext" runat="server" EnableViewState="false" />
            </td>
            <td>
            </td>
        </tr>
    </table>
</asp:Panel>
<asp:Label ID="lblSubject" runat="server" EnableViewState="false" /><br />
<br />
<div id="prev" style="background-color:#fff">
    <iframe id="preview" style="width:100%; height:455px; border: solid 1px #cccccc; background-color:#fff;" frameborder="0"></iframe>
</div>

<script type="text/javascript">
<!--

    function pageLoad()
    {
        document.getElementById('preview').src = "Newsletter_Issue_ShowPreview.aspx?subscriberguid=" + guid[currentSubscriberIndex] + "&issueid=" + newsletterIssueId;

        if (document.getElementById(lblEmail) == null) return;
        
        if ((guid != null) && (guid.length > 0) && (email != null) && (email.length > 0) && (subject != null) && (subject.length > 0))
        {
            if (guid.length > 1)
            {
                document.getElementById(lblNext).style.display = "none";
            }
            else
            {
                document.getElementById(lnkNext).style.display = "none";
            }
            document.getElementById(lnkPrev).style.display = "none";

            document.getElementById(lblEmail).innerHTML = email[currentSubscriberIndex];
            document.getElementById(lblSubj).innerHTML = subject[currentSubscriberIndex];
        }
        else
        {
            document.getElementById('prev').style.display = 'none';
        }
    }
    
    function getPreviousSubscriber()
    {
        if ((currentSubscriberIndex > 0)&&(guid.length > 0)&&(email.length > 0)&&(subject.length > 0))
        {
            if (document.getElementById(lblEmail) == null) return;

            currentSubscriberIndex--;
            document.getElementById(lblEmail).innerHTML = email[currentSubscriberIndex];
            document.getElementById(lblSubj).innerHTML = subject[currentSubscriberIndex];
            document.getElementById('preview').src = "Newsletter_Issue_ShowPreview.aspx?subscriberguid=" + guid[currentSubscriberIndex] + "&issueid=" + newsletterIssueId;
            
            if (currentSubscriberIndex == 0)
            {
                document.getElementById(lnkPrev).style.display = "none";
                document.getElementById(lblPrev).style.display = "inline";               
            }
            document.getElementById(lnkNext).style.display = "inline";
            document.getElementById(lblNext).style.display = "none";
        }
    }
    
    function getNextSubscriber()
    {
        if ((currentSubscriberIndex < email.length - 1)&&(currentSubscriberIndex < subject.length - 1)&&(currentSubscriberIndex < guid.length - 1)&&(guid.length > 0)&&(email.length > 0)&&(subject.length > 0))
        {
            if (document.getElementById(lblEmail) == null) return;
        
            currentSubscriberIndex++;
            document.getElementById(lblEmail).innerHTML = email[currentSubscriberIndex];
            document.getElementById(lblSubj).innerHTML = subject[currentSubscriberIndex];
            document.getElementById('preview').src = "Newsletter_Issue_ShowPreview.aspx?subscriberguid=" + guid[currentSubscriberIndex] + "&issueid=" + newsletterIssueId;
        
            if (currentSubscriberIndex == email.length - 1)
            {
                document.getElementById(lnkNext).style.display = "none";
                document.getElementById(lblNext).style.display = "inline";             
            }
            document.getElementById(lnkPrev).style.display = "inline";
            document.getElementById(lblPrev).style.display = "none";
        }
    }

-->
</script>
