SET IDENTITY_INSERT [CMS_Transformation] ON
INSERT INTO [CMS_Transformation] ([TransformationID], [TransformationName], [TransformationCode], [TransformationType], [TransformationClassID], [TransformationVersionGUID], [TransformationGUID], [TransformationLastModified], [TransformationIsHierarchical], [TransformationHierarchicalXML], [TransformationCSS]) VALUES (1115, N'AtomItem', N'<entry>
  <title><%# EvalCDATA("DocumentName") %></title>
  <link href="<%# GetAbsoluteUrl(GetDocumentUrlForFeed(), Eval("SiteName")) %>" />
  <id>urn:uuid:<%# Eval("NodeGUID") %></id>
  <published><%# GetAtomDateTime(Eval("DocumentCreatedWhen")) %></published>
  <updated><%# GetAtomDateTime(Eval("DocumentModifiedWhen")) %></updated>
  <author>
    <name><%# Eval("NodeOwnerFullName") %></name>
  </author>
  <summary><%# EvalCDATA("NodeAliasPath") %></summary>
</entry>', N'ascx', 1095, N'adb540cb-3c30-494e-99a4-455fc8123177', 'ce31d65b-f97e-4967-824c-c65bb10543ad', '20100802 15:39:59', 0, N'', NULL)
INSERT INTO [CMS_Transformation] ([TransformationID], [TransformationName], [TransformationCode], [TransformationType], [TransformationClassID], [TransformationVersionGUID], [TransformationGUID], [TransformationLastModified], [TransformationIsHierarchical], [TransformationHierarchicalXML], [TransformationCSS]) VALUES (676, N'Attachment', N'<%@ Register TagPrefix="cc1" Namespace="CMS.GlobalHelper" Assembly="CMS.GlobalHelper" %>
<div>
<a target="_blank" href="<%# GetAbsoluteUrl(GetAttachmentUrl(Eval("AttachmentName"), Eval("NodeAliasPath")), Eval<int>("AttachmentSiteID")) %>">
<img style="border: none;" src="<%# IfCompare(ImageHelper.IsImage((string)Eval("AttachmentExtension")), true, GetAttachmentIconUrl(Eval("AttachmentExtension"), null), GetAbsoluteUrl(GetAttachmentUrl(Eval("AttachmentName"), Eval("NodeAliasPath")), Eval<int>("AttachmentSiteID"))) %>?maxsidesize=150" alt="<%# Eval("AttachmentName", true) %>" />
</a>
<%# IfCompare(ImageHelper.IsImage((string)Eval("AttachmentExtension")), true, "<br />" + ResHelper.GetString("attach.openfile"), "") %>
<br />
<%# Eval("AttachmentName",true) %>
<br />
</div>', N'ascx', 1095, N'6c8255a2-005a-4587-9bb8-9cd366fe5d57', '6eba21c8-7c1a-49a6-937e-974627c9224b', '20110816 16:49:34', 0, N'', N'')
INSERT INTO [CMS_Transformation] ([TransformationID], [TransformationName], [TransformationCode], [TransformationType], [TransformationClassID], [TransformationVersionGUID], [TransformationGUID], [TransformationLastModified], [TransformationIsHierarchical], [TransformationHierarchicalXML], [TransformationCSS]) VALUES (1982, N'AttachmentCarousel2D', N'<%@ Register TagPrefix="cc1" Namespace="CMS.GlobalHelper" Assembly="CMS.GlobalHelper" %>
<%# IfCompare(ImageHelper.IsImage((string)Eval("AttachmentExtension")), true,"<li><div style=''text-align:center;''><div style=''font-size: 11px;line-height: 12px;position:relative;z-index:1000;margin:auto;''><a target=''_blank'' href=''" + GetAbsoluteUrl(GetAttachmentUrl(Eval("AttachmentName"), Eval("NodeAliasPath")), Eval<int>("AttachmentSiteID")) + "''><img style=''border: none;'' src=''" + GetAttachmentIconUrl(Eval("AttachmentExtension"), null) + "'' alt=''" + Eval("AttachmentName") + "'' /></a></div><p>" + ResHelper.GetString("attach.openfile") + "</p></div></li>",
"<li><img src=''" + GetAbsoluteUrl(GetAttachmentUrl(Eval("AttachmentName"), Eval("NodeAliasPath")), Eval<int>("AttachmentSiteID")) + "?maxsidesize=150'' class=''cloudcarousel2d'' alt=''" + Eval("AttachmentTitle", true) + "'' title=''" + Eval("AttachmentDescription", true) + "'' /></li>") %>', N'ascx', 1095, N'ea8cacd9-974b-416b-ae82-b2b0fdbf5b2c', '1813bc8e-8791-4a0a-b6b2-acb90f0fa599', '20110919 18:12:12', 0, N'', N'')
INSERT INTO [CMS_Transformation] ([TransformationID], [TransformationName], [TransformationCode], [TransformationType], [TransformationClassID], [TransformationVersionGUID], [TransformationGUID], [TransformationLastModified], [TransformationIsHierarchical], [TransformationHierarchicalXML], [TransformationCSS]) VALUES (1739, N'AttachmentCarousel3D', N'<%@ Register TagPrefix="cc1" Namespace="CMS.GlobalHelper" Assembly="CMS.GlobalHelper" %>
<%# IfCompare(ImageHelper.IsImage((string)Eval("AttachmentExtension")), true,
"<div style=\"text-align:center;width: 350px;\"><div style=\"font-size: 11px;line-height: 12px;position:relative;z-index:1000;margin:auto;width:140px;\"><a target=\"_blank\" href=\"" + GetAbsoluteUrl(GetAttachmentUrl(Eval("AttachmentName"), Eval("NodeAliasPath")), Eval<int>("AttachmentSiteID")) + "\"><img style=\"border: none;\" src=\"" + GetAttachmentIconUrl(Eval("AttachmentExtension"), null) + "\" alt=\"" + Eval("AttachmentName") + "\" /></a><p>" + ResHelper.GetString("attach.openfile") + "</p></div></div>",
"<img src=\"" + GetAbsoluteUrl(GetAttachmentUrl(Eval("AttachmentName"), Eval("NodeAliasPath")), Eval<int>("AttachmentSiteID")) + "?maxsidesize=150\" class=\"cloudcarousel3d\" alt=\"" + Eval("AttachmentTitle", true) + "\" title=\"" + Eval("AttachmentDescription", true) + "\" />") %>', N'ascx', 1095, N'cd223232-c634-4263-8717-9c03b0810912', '49c7568b-113e-41eb-b183-4712344811a3', '20110518 17:07:34', 0, N'', N'')
INSERT INTO [CMS_Transformation] ([TransformationID], [TransformationName], [TransformationCode], [TransformationType], [TransformationClassID], [TransformationVersionGUID], [TransformationGUID], [TransformationLastModified], [TransformationIsHierarchical], [TransformationHierarchicalXML], [TransformationCSS]) VALUES (713, N'AttachmentLightbox', N'<%@ Register TagPrefix="cc1" Namespace="CMS.GlobalHelper" Assembly="CMS.GlobalHelper" %>
<a style="text-decoration: none;" href="<%# GetAbsoluteUrl(GetAttachmentUrl(Eval("AttachmentName"), Eval("NodeAliasPath")), Eval<int>("AttachmentSiteID")) %>" rel="lightbox" rev="<%# Eval("AttachmentID") %>" title="<%# Eval("AttachmentName", true) %>">
<img style="border: none;" src="<%# IfCompare(ImageHelper.IsImage((string)Eval("AttachmentExtension")), true, GetAttachmentIconUrl(Eval("AttachmentExtension"), null), GetAbsoluteUrl(GetAttachmentUrl(Eval("AttachmentName"), Eval("NodeAliasPath")), Eval<int>("AttachmentSiteID"))) %>?maxsidesize=150" alt="<%# Eval("AttachmentTitle", true) %>" />
</a>', N'ascx', 1095, N'13566579-2881-46f4-82b6-24fa3bca952e', 'f0460e74-2509-4b1e-8665-5e68cf947e86', '20111125 17:01:52', 0, N'', N'')
INSERT INTO [CMS_Transformation] ([TransformationID], [TransformationName], [TransformationCode], [TransformationType], [TransformationClassID], [TransformationVersionGUID], [TransformationGUID], [TransformationLastModified], [TransformationIsHierarchical], [TransformationHierarchicalXML], [TransformationCSS]) VALUES (714, N'AttachmentLightboxDetail', N'<%@ Register TagPrefix="cc1" Namespace="CMS.GlobalHelper" Assembly="CMS.GlobalHelper" %>
<%# IfCompare(ImageHelper.IsImage((string)Eval("AttachmentExtension")), true,
"<div style=\"text-align:center;width: 350px;\"><div style=\"font-size: 11px;line-height: 12px;position:relative;z-index:1000;margin:auto;width:140px;\"><a target=\"_blank\" href=\"" + GetAbsoluteUrl(GetAttachmentUrl(Eval("AttachmentName"), Eval("NodeAliasPath")), Eval<int>("AttachmentSiteID")) + "\"><img style=\"border: none;\" src=\"" + GetAttachmentIconUrl(Eval("AttachmentExtension"), null) + "\" alt=\"" + Eval("AttachmentName") + "\" /></a><p>" + ResHelper.GetString("attach.openfile") + "</p></div></div>",
"<img src=\"" + GetAbsoluteUrl(GetAttachmentUrl(Eval("AttachmentName"), Eval("NodeAliasPath")), Eval<int>("AttachmentSiteID")) + "?maxsidesize=1000\" alt=\"" + Eval("AttachmentTitle", true) + "\" title=\"" + Eval("AttachmentDescription", true) + "\" />") %>', N'ascx', 1095, N'612e08bb-e563-42a5-8448-b9c4de3d405d', 'fdad8271-1293-4444-9151-e74af81dcda6', '20101116 09:32:08', 0, N'', NULL)
INSERT INTO [CMS_Transformation] ([TransformationID], [TransformationName], [TransformationCode], [TransformationType], [TransformationClassID], [TransformationVersionGUID], [TransformationGUID], [TransformationLastModified], [TransformationIsHierarchical], [TransformationHierarchicalXML], [TransformationCSS]) VALUES (841, N'AttachmentList', N'<div>
<img src="<%# GetAttachmentIconUrl(Eval("AttachmentExtension"), "List") %>" alt="<%# Eval("AttachmentName",true) %>" />
&nbsp;
<a target="_blank" href="<%# GetAbsoluteUrl(GetAttachmentUrl(Eval("AttachmentName"), Eval("NodeAliasPath")), EvalInteger("AttachmentSiteID")) %>">
<%# Eval("AttachmentName",true) %>
</a>
</div>', N'ascx', 1095, N'278428fd-fb9a-491a-89bf-5c009cd3824e', '4455961a-e81f-419b-8a68-19d1cdcef2a1', '20110518 16:21:59', 0, N'', N'')
INSERT INTO [CMS_Transformation] ([TransformationID], [TransformationName], [TransformationCode], [TransformationType], [TransformationClassID], [TransformationVersionGUID], [TransformationGUID], [TransformationLastModified], [TransformationIsHierarchical], [TransformationHierarchicalXML], [TransformationCSS]) VALUES (415, N'CategoryList', N'<asp:PlaceHolder ID="plcCategoryList" runat="server" EnableViewState="false"></asp:PlaceHolder><br />', N'ascx', 1095, N'8a38d66f-9d8f-40af-83e1-e4c2f9735b9a', '4b6c6f44-ac26-45c1-920f-df48f92e94d0', '20110729 07:32:22', 0, N'', N'')
INSERT INTO [CMS_Transformation] ([TransformationID], [TransformationName], [TransformationCode], [TransformationType], [TransformationClassID], [TransformationVersionGUID], [TransformationGUID], [TransformationLastModified], [TransformationIsHierarchical], [TransformationHierarchicalXML], [TransformationCSS]) VALUES (915, N'CMSDeskSmartSearchResults', N'<div style="margin-bottom: 30px;">
  <%-- Search result image --%>
        <div style="border: solid 1px #eeeeee; width: 90px; height:90px; margin-right: 5px;" class="LeftAlign">
           <img src="<%# GetSearchImageUrl(UIHelper.GetImageUrl(Page, "CMSModules/CMS_SmartSearch/no_image.gif"),90) %>" alt="" />
        </div>
        <div class="LeftAlign">
            <%-- Search result title --%>
            <div style="text-align: left;">
                <a style="font-weight: bold" href=''<%# "javascript:SelectItem(" + CMS.ExtendedControls.ControlsHelper.RemoveDynamicControls(ValidationHelper.GetString(GetSearchValue("nodeId"), "")) + ", ''"+ ValidationHelper.GetString(GetSearchValue("DocumentCulture"), "") + "'')" %>''>
                    <%#SearchHighlight(CMS.GlobalHelper.HTMLHelper.HTMLEncode(DataHelper.GetNotEmpty(Eval("Title"), "/")), "<span style=''font-weight:bold;''>", "</span>")%> (<%#ValidationHelper.GetString(GetSearchValue("DocumentCulture"), "")%>)
                </a>
            </div>
            <%-- Search result content --%>
            <div style="margin-top: 5px; width: 590px;min-height:40px">
                <%#SearchHighlight(CMS.GlobalHelper.HTMLHelper.HTMLEncode(TextHelper.LimitLength(HttpUtility.HtmlDecode(CMS.GlobalHelper.HTMLHelper.StripTags(CMS.ExtendedControls.ControlsHelper.RemoveDynamicControls(GetSearchedContent(DataHelper.GetNotEmpty(Eval("Content"), ""))), false, " ")), 280, "...")), "<span style=''background-color: #FEFF8F''>", "</span>")%><br />
            </div>
            <%-- Relevance, URL, Creattion --%>
            <div style="margin-top: 5px;">
                <%-- Relevance --%>
                <div title="Relevance: <%# Convert.ToInt32(ValidationHelper.GetDouble(Eval("Score"),0.0)*100) %>%"
                    style="width: 50px; border: solid 1px #aaaaaa; margin-top: 7px; margin-right: 6px;
                    float: left; color: #0000ff; font-size: 2pt; line-height: 4px; height: 4px;">
                    <div style="<%# "background-color:#a7d3a7;width:"+ Convert.ToString(Convert.ToInt32((ValidationHelper.GetDouble(Eval("Score"),0.0)/2)*100))  + "px;height:4px;line-height: 4px;"%>">
                    </div>
                </div>
                <%-- URL --%>
                <span style="color: #008000">
                    <%# TextHelper.BreakLine(SearchHighlight(SearchResultUrl(true),"<strong>","</strong>"),75,"<br />") %>
                </span>
                <%-- Creation --%>
                <span style="padding-left:5px;color: #888888; font-size: 9pt">
                    <%# GetDateTimeString(ValidationHelper.GetDateTime(Eval("Created"), DateTimeHelper.ZERO_TIME), true) %>
                </span>
            </div>
        </div>
        <div style="clear: both">
        </div>
    </div>', N'ascx', 1095, N'0ebe6cce-399a-4afe-8a30-2c0d2b74bd18', 'aadecc4c-1909-4804-b0bb-990c093fc4e0', '20110617 16:20:16', 0, N'', N'')
INSERT INTO [CMS_Transformation] ([TransformationID], [TransformationName], [TransformationCode], [TransformationType], [TransformationClassID], [TransformationVersionGUID], [TransformationGUID], [TransformationLastModified], [TransformationIsHierarchical], [TransformationHierarchicalXML], [TransformationCSS]) VALUES (916, N'CMSDeskSQLSearchResults', N'<div style="margin-bottom: 30px;">
  <%-- Search result image --%>
        <div style="margin-right: 5px;" class="LeftAlign">
           <img src="<%# UIHelper.GetDocumentTypeIconUrl(this.Page, ValidationHelper.GetString(DataBinder.Eval(((System.Web.UI.WebControls.RepeaterItem)(Container)).DataItem, "ClassName"), "")) %>" alt="" />
        </div>
        <div class="LeftAlign" style="width:95%;">
            <%-- Search result title --%>
            <div>
        <a style="font-weight: bold" href="<%# "javascript:SelectItem(" + Eval("NodeID") + ", \''" + Eval("DocumentCulture") + "\'')" %>"><%# IfEmpty(Eval("NodeName"), "/", HTMLHelper.HTMLEncode(ValidationHelper.GetString(Eval("NodeName"), null))) %> (<%# Eval("DocumentCulture") %>)</a>
            </div>
</div>
<div class="LeftAlign">
  <div style="margin-top: 5px;">
<%-- URL --%>
                <span style="color: #008000">
                    <%#  GetAbsoluteUrl(GetDocumentUrl()) %>
                </span>
                <%-- Creation --%>
                <span style="padding-left:5px;;color: #888888; font-size: 9pt">
                    <%# GetDateTimeString(ValidationHelper.GetDateTime(Eval("DocumentCreatedWhen"), DateTimeHelper.ZERO_TIME), true) %>
                </span>
        </div>
  </div>
<div style="clear: both"></div>
</div>', N'ascx', 1095, N'74f70660-c946-42a2-b475-320724edf9be', '914377c1-21ad-443e-956f-0b9646e5520b', '20101118 15:31:57', 0, N'', NULL)
INSERT INTO [CMS_Transformation] ([TransformationID], [TransformationName], [TransformationCode], [TransformationType], [TransformationClassID], [TransformationVersionGUID], [TransformationGUID], [TransformationLastModified], [TransformationIsHierarchical], [TransformationHierarchicalXML], [TransformationCSS]) VALUES (2226, N'Empty', N'', N'ascx', 1095, N'679bd9ab-b211-4b75-8a62-053c18c3c2b0', '8fe2f25d-5319-4c07-a9a1-1cc8c70aecdb', '20110919 14:05:08', 0, N'', N'')
INSERT INTO [CMS_Transformation] ([TransformationID], [TransformationName], [TransformationCode], [TransformationType], [TransformationClassID], [TransformationVersionGUID], [TransformationGUID], [TransformationLastModified], [TransformationIsHierarchical], [TransformationHierarchicalXML], [TransformationCSS]) VALUES (668, N'GoogleSiteMap', N'<url>
<loc><%# HTMLHelper.HTMLEncode(URLHelper.GetAbsoluteUrl(GetDocumentUrl())) %></loc>
<lastmod><%# HTMLHelper.HTMLEncode(GetDateTime("DocumentModifiedWhen", "yyyy-MM-dd")) %></lastmod>
</url>', N'ascx', 1095, N'7184c565-65a0-4ed5-bc9f-667a80b68fe2', '8d5a6991-b2bf-436d-ba82-5159eb98fc71', '20120113 14:03:46', 0, N'', N'')
INSERT INTO [CMS_Transformation] ([TransformationID], [TransformationName], [TransformationCode], [TransformationType], [TransformationClassID], [TransformationVersionGUID], [TransformationGUID], [TransformationLastModified], [TransformationIsHierarchical], [TransformationHierarchicalXML], [TransformationCSS]) VALUES (153, N'Newsletter_Archive', N'<%#  FormatDateTime(Eval("IssueMailoutTime"),"d") %> - <a href="~/CMSModules/Newsletters/CMSPages/GetNewsletterIssue.aspx?issueId=<%# Eval("IssueID")%>" target="_blank"><%# Eval("IssueSubject",true) %></a> <br />', N'ascx', 1095, N'2516ee44-b5b6-4ff8-a1a2-a2f7dc50d8c7', '62116172-2118-4676-98eb-373c79fa6cc6', '20110317 15:39:33', 0, N'', NULL)
INSERT INTO [CMS_Transformation] ([TransformationID], [TransformationName], [TransformationCode], [TransformationType], [TransformationClassID], [TransformationVersionGUID], [TransformationGUID], [TransformationLastModified], [TransformationIsHierarchical], [TransformationHierarchicalXML], [TransformationCSS]) VALUES (559, N'OnLineUsers', N'<%# Eval("UserName", true) %>&nbsp;', N'ascx', 1095, N'26da0dfa-0436-470e-a547-bddeb248255d', '3b16b39e-3020-485d-8bae-6e7617e13894', '20090122 21:35:31', 0, N'', NULL)
INSERT INTO [CMS_Transformation] ([TransformationID], [TransformationName], [TransformationCode], [TransformationType], [TransformationClassID], [TransformationVersionGUID], [TransformationGUID], [TransformationLastModified], [TransformationIsHierarchical], [TransformationHierarchicalXML], [TransformationCSS]) VALUES (283, N'Print', N'<h3>Print transformation is missing</h3>
<div>Transformation of current document type is missing. You have to define the tranformation in the CMS Site Manager Development section.</div>', N'ascx', 1095, N'b620017e-eee6-4edc-8903-3efb33453ace', 'dfba986c-38de-431c-89df-8ad3e8c7b451', '20080421 16:10:12', 0, N'', NULL)
INSERT INTO [CMS_Transformation] ([TransformationID], [TransformationName], [TransformationCode], [TransformationType], [TransformationClassID], [TransformationVersionGUID], [TransformationGUID], [TransformationLastModified], [TransformationIsHierarchical], [TransformationHierarchicalXML], [TransformationCSS]) VALUES (15, N'RelatedDocuments', N'<strong><a href="<%# ResolveUrl(GetUrl( Eval("NodeAliasPath"), null)) %>">
<%# Eval("DocumentName",true) %></a></strong>
<br />', N'ascx', 1095, N'6d973ef8-2aec-48c5-a16b-378f2679d295', 'b96f0a40-8fb6-4ed5-8eaa-309867a18283', '20110317 15:39:58', 0, N'', NULL)
INSERT INTO [CMS_Transformation] ([TransformationID], [TransformationName], [TransformationCode], [TransformationType], [TransformationClassID], [TransformationVersionGUID], [TransformationGUID], [TransformationLastModified], [TransformationIsHierarchical], [TransformationHierarchicalXML], [TransformationCSS]) VALUES (1019, N'RSSItem', N'<item>
     <guid isPermaLink="false"><%# Eval("NodeGUID") %></guid>
     <title><%# EvalCDATA("DocumentName") %></title>
     <description><%# EvalCDATA("NodeAliasPath") %></description>
     <pubDate><%# GetRSSDateTime(Eval("DocumentCreatedWhen")) %></pubDate>
     <link><![CDATA[<%# GetAbsoluteUrl(GetDocumentUrlForFeed(), Eval("SiteName")) %>]]></link>     	
</item>', N'ascx', 1095, N'1a15194b-d966-4d49-b366-8b58591518a3', 'e38315ed-fcef-46a9-b82a-b33d6902649b', '20100802 15:40:20', 0, N'', NULL)
INSERT INTO [CMS_Transformation] ([TransformationID], [TransformationName], [TransformationCode], [TransformationType], [TransformationClassID], [TransformationVersionGUID], [TransformationGUID], [TransformationLastModified], [TransformationIsHierarchical], [TransformationHierarchicalXML], [TransformationCSS]) VALUES (20, N'SearchResults', N'<div class="SearchResult">
  <div class="ResultTitle">
    <a href="<%# GetDocumentUrl()%>"><%# IfEmpty(Eval("SearchResultName",true), "/", Eval("SearchResultName",true)) %></a>
  </div>
  <div class="ResultPath">
    Path: <%# Eval("DocumentNamePath",true) %><br />
  </div>
</div>', N'ascx', 1095, N'7d0cbd5c-a793-427e-aa92-907d21a704fc', 'a3c543c8-b855-4acd-9c03-9302a02f8f74', '20081205 13:57:06', 0, N'', NULL)
INSERT INTO [CMS_Transformation] ([TransformationID], [TransformationName], [TransformationCode], [TransformationType], [TransformationClassID], [TransformationVersionGUID], [TransformationGUID], [TransformationLastModified], [TransformationIsHierarchical], [TransformationHierarchicalXML], [TransformationCSS]) VALUES (801, N'SmartSearchResults', N'<div style="margin-bottom: 30px;">
        <%-- Search result title --%>
        <div>
            <a style="font-weight: bold" href=''<%# SearchResultUrl(true) %>''>
                <%#SearchHighlight(CMS.GlobalHelper.HTMLHelper.HTMLEncode(CMS.ExtendedControls.ControlsHelper.RemoveDynamicControls(DataHelper.GetNotEmpty(Eval("Title"), "/"))), "<span style=''font-weight:bold;''>", "</span>")%>
            </a>
        </div>
        <%-- Search result content --%>
        <div style="margin-top: 5px; width: 590px;">
            <%#SearchHighlight(CMS.GlobalHelper.HTMLHelper.HTMLEncode(TextHelper.LimitLength(HttpUtility.HtmlDecode(CMS.GlobalHelper.HTMLHelper.StripTags(CMS.ExtendedControls.ControlsHelper.RemoveDynamicControls(GetSearchedContent(DataHelper.GetNotEmpty(Eval("Content"), ""))), false, " ")), 280, "...")), "<span style=''background-color: #FEFF8F''>", "</span>")%><br />
        </div>
        <%-- Relevance, URL, Creattion --%>
        <div style="margin-top: 5px;">
            <%-- Relevance --%>
            <div title="Relevance: <%# Convert.ToInt32(ValidationHelper.GetDouble(Eval("Score"),0.0)*100)%>%"
                style="width: 50px; border: solid 1px #aaaaaa; margin-top: 7px; margin-right: 6px; float: left; color: #0000ff; font-size: 2pt; line-height: 4px; height: 4px;">
                <div style=''<%# "background-color:#a7d3a7;width:"+ Convert.ToString(Convert.ToInt32((ValidationHelper.GetDouble(Eval("Score"),0.0)/2)*100))  + "px;height:4px;line-height: 4px;"%>''>
                </div>
            </div>
            <%-- URL --%>
            <span style="color: #008000">
                <%# SearchHighlight(SearchResultUrl(true),"<strong>","</strong>")%>
            </span>
            <%-- Creation --%>
            <span style="color: #888888; font-size: 9pt">
                <%# GetDateTimeString(ValidationHelper.GetDateTime(Eval("Created"), DateTimeHelper.ZERO_TIME), true) %>
            </span>
        </div>
    </div>', N'ascx', 1095, N'01cf2dc9-486f-46fc-8fd7-e2612c682090', '63b96721-59ae-4462-a842-10fedfc282e1', '20110610 15:07:17', 0, N'', N'')
INSERT INTO [CMS_Transformation] ([TransformationID], [TransformationName], [TransformationCode], [TransformationType], [TransformationClassID], [TransformationVersionGUID], [TransformationGUID], [TransformationLastModified], [TransformationIsHierarchical], [TransformationHierarchicalXML], [TransformationCSS]) VALUES (846, N'SmartSearchResultsWithImages', N'<div style="margin-bottom: 30px;">
  <%-- Search result image --%>
        <div style="float: left; border: solid 1px #eeeeee; width: 90px; height:90px; margin-right: 5px;">
           <img src="<%# GetSearchImageUrl("~/App_Themes/Default/Images/CMSModules/CMS_SmartSearch/no_image.gif",90) %>" alt="" />
        </div>
        <div style="float: left">
            <%-- Search result title --%>
            <div>
                <a style="font-weight: bold" href=''<%# SearchResultUrl(true) %>''>
                    <%#SearchHighlight(CMS.GlobalHelper.HTMLHelper.HTMLEncode(CMS.ExtendedControls.ControlsHelper.RemoveDynamicControls(DataHelper.GetNotEmpty(Eval("Title"), "/"))), "<span style=''font-weight:bold;''>", "</span>")%>
                </a>
            </div>
            <%-- Search result content --%>
            <div style="margin-top: 5px; width: 590px;min-height:40px">
                <%#SearchHighlight(CMS.GlobalHelper.HTMLHelper.HTMLEncode(TextHelper.LimitLength(HttpUtility.HtmlDecode(CMS.GlobalHelper.HTMLHelper.StripTags(CMS.ExtendedControls.ControlsHelper.RemoveDynamicControls(GetSearchedContent(DataHelper.GetNotEmpty(Eval("Content"), ""))), false, " ")), 280, "...")), "<span style=''background-color: #FEFF8F''>", "</span>")%><br />
            </div>
            <%-- Relevance, URL, Creattion --%>
            <div style="margin-top: 5px;">
                <%-- Relevance --%>
                <div title="Relevance: <%#Convert.ToInt32(ValidationHelper.GetDouble(Eval("Score"),0.0)*100)%>%"
                    style="width: 50px; border: solid 1px #aaaaaa; margin-top: 7px; margin-right: 6px;
                    float: left; color: #0000ff; font-size: 2pt; line-height: 4px; height: 4px;">
                    <div style="<%# "background-color:#a7d3a7;width:"+ Convert.ToString(Convert.ToInt32((ValidationHelper.GetDouble(Eval("Score"),0.0)/2)*100))  + "px;height:4px;line-height: 4px;"%>">
                    </div>
                </div>
                <%-- URL --%>
                <span style="color: #008000">
                    <%# TextHelper.BreakLine(SearchHighlight(SearchResultUrl(true),"<strong>","</strong>"),75,"<br />") %>
                </span>
                <%-- Creation --%>
                <span style="padding-left:5px;color: #888888; font-size: 9pt">
                    <%# GetDateTimeString(ValidationHelper.GetDateTime(Eval("Created"), DateTimeHelper.ZERO_TIME), true) %>
                </span>
            </div>
        </div>
        <div style="clear: both">
        </div>
    </div>', N'ascx', 1095, N'd1acf369-54ee-4884-96a5-b496a7839dec', 'fa253f7c-6705-4995-bb27-96c0042dee5b', '20110617 16:20:39', 0, N'', N'')
INSERT INTO [CMS_Transformation] ([TransformationID], [TransformationName], [TransformationCode], [TransformationType], [TransformationClassID], [TransformationVersionGUID], [TransformationGUID], [TransformationLastModified], [TransformationIsHierarchical], [TransformationHierarchicalXML], [TransformationCSS]) VALUES (1143, N'AtomItem', N'<entry>
  <title><%# EvalCDATA("FileName") %></title>
  <link href="<%# GetAbsoluteUrl(GetDocumentUrlForFeed(), Eval("SiteName")) %>"/>
  <id>urn:uuid:<%# Eval("NodeGUID") %></id>
  <published><%# GetAtomDateTime(Eval("DocumentCreatedWhen")) %></published>
  <updated><%# GetAtomDateTime(Eval("DocumentModifiedWhen")) %></updated>
  <author>
    <name><%# Eval("NodeOwnerFullName") %></name>
  </author>
  <summary><%# EvalCDATA("FileDescription") %></summary>
</entry>', N'ascx', 1685, N'a96475ea-3782-4644-b3d9-ed434d39b651', 'f696f2d5-70d0-4cce-93d0-3108df11abaa', '20100802 15:12:31', 0, N'', NULL)
INSERT INTO [CMS_Transformation] ([TransformationID], [TransformationName], [TransformationCode], [TransformationType], [TransformationClassID], [TransformationVersionGUID], [TransformationGUID], [TransformationLastModified], [TransformationIsHierarchical], [TransformationHierarchicalXML], [TransformationCSS]) VALUES (299, N'AttachmentList', N'<a target="_blank" href="<%# GetFileUrl("FileAttachment") %>">
<%# IfImage("FileAttachment", GetImage("FileAttachment", 400, 400, 400, Eval("FileDescription")), "") %>
<br /><%# Eval("FileName",true) %></a><br />', N'ascx', 1685, N'547c875a-0f55-41e4-93e9-87f68131d5d3', '74f430bc-627c-4867-b21c-dbe6134e54c3', '20110317 14:48:43', 0, N'', NULL)
INSERT INTO [CMS_Transformation] ([TransformationID], [TransformationName], [TransformationCode], [TransformationType], [TransformationClassID], [TransformationVersionGUID], [TransformationGUID], [TransformationLastModified], [TransformationIsHierarchical], [TransformationHierarchicalXML], [TransformationCSS]) VALUES (296, N'ImageGallery_detail400', N'<%#IfEmpty(Eval("FileAttachment"), "no image", "<img alt=''" + Eval("FileName", true) + "'' src=''" + GetFileUrl("FileAttachment") + "?maxsidesize=400'' />")%>', N'ascx', 1685, N'fda53c27-62d8-44a0-b860-443325562e03', '9f9fda7c-4695-48d3-8014-88a3bc670a76', '20110617 08:48:00', 0, N'', N'')
INSERT INTO [CMS_Transformation] ([TransformationID], [TransformationName], [TransformationCode], [TransformationType], [TransformationClassID], [TransformationVersionGUID], [TransformationGUID], [TransformationLastModified], [TransformationIsHierarchical], [TransformationHierarchicalXML], [TransformationCSS]) VALUES (302, N'ImageGallery_detail500', N'<%#IfEmpty(Eval("FileAttachment"), "no image", "<img alt=''" + Eval("FileDescription") + "'' src=''" + GetFileUrl("FileAttachment") + "?maxsidesize=500'' />")%>', N'ascx', 1685, N'a6385255-e371-4118-8a5e-1dc873eda13b', 'b7ce1195-a490-4808-b9c9-802922f35b7b', '20110518 12:42:40', 0, N'', N'')
INSERT INTO [CMS_Transformation] ([TransformationID], [TransformationName], [TransformationCode], [TransformationType], [TransformationClassID], [TransformationVersionGUID], [TransformationGUID], [TransformationLastModified], [TransformationIsHierarchical], [TransformationHierarchicalXML], [TransformationCSS]) VALUES (297, N'ImageGallery_detail600', N'<%#IfEmpty(Eval("FileAttachment"), "no image", "<img alt=''" + Eval("FileDescription") + "'' src=''" + GetFileUrl("FileAttachment") + "?maxsidesize=600'' />")%>', N'ascx', 1685, N'809c1327-db18-46f9-9e33-2a006d74e255', 'f08449f5-74a0-4e73-a777-63e2ec95cb4e', '20110518 12:46:11', 0, N'', N'')
INSERT INTO [CMS_Transformation] ([TransformationID], [TransformationName], [TransformationCode], [TransformationType], [TransformationClassID], [TransformationVersionGUID], [TransformationGUID], [TransformationLastModified], [TransformationIsHierarchical], [TransformationHierarchicalXML], [TransformationCSS]) VALUES (298, N'ImageGallery_detail800', N'<%#IfEmpty(Eval("FileAttachment"), "no image", "<img alt=''" + Eval("FileDescription") + "'' src=''" + GetFileUrl("FileAttachment") + "?maxsidesize=800'' />")%>', N'ascx', 1685, N'fa6f2d68-7200-44c8-8f42-8aa840f20dce', '1876199f-187e-41ea-8145-8a5fa9aaf78d', '20110518 12:47:22', 0, N'', N'')
INSERT INTO [CMS_Transformation] ([TransformationID], [TransformationName], [TransformationCode], [TransformationType], [TransformationClassID], [TransformationVersionGUID], [TransformationGUID], [TransformationLastModified], [TransformationIsHierarchical], [TransformationHierarchicalXML], [TransformationCSS]) VALUES (1738, N'ImageGallery_LightBoxList', N'<%#IfEmpty(Eval("FileAttachment"), "no image", "<a href=''" + GetFileUrl("FileAttachment") + "'' class=''ImgLightBox''><img alt=''" + Eval("FileDescription") + "'' src=''" + GetFileUrl("FileAttachment") + "?maxsidesize=800'' /></a>")%>', N'ascx', 1685, N'ffacffbb-fbdd-4d1a-8244-188663cb9473', 'f297745a-1089-44f1-a9cb-645f899addfe', '20110518 12:57:49', 0, N'', N'')
INSERT INTO [CMS_Transformation] ([TransformationID], [TransformationName], [TransformationCode], [TransformationType], [TransformationClassID], [TransformationVersionGUID], [TransformationGUID], [TransformationLastModified], [TransformationIsHierarchical], [TransformationHierarchicalXML], [TransformationCSS]) VALUES (293, N'ImageGallery_thumbnails100', N'<a href="?imagepath=<%# System.Web.HttpUtility.UrlEncode(DataBinder.Eval(Container, "DataItem.NodeAliasPath").ToString()) %>">
<%#IfEmpty(Eval("FileAttachment"), "no image", "<img alt=''" + Eval("FileName", true) + "'' src=''" + GetFileUrl("FileAttachment") + "?maxsidesize=100'' border=''0'' />")%>
</a>', N'ascx', 1685, N'a2374b34-5679-44b9-b7ee-4175993d3109', 'c8365347-59be-4353-88d0-9ae8fde751db', '20110617 08:48:29', 0, N'', N'')
INSERT INTO [CMS_Transformation] ([TransformationID], [TransformationName], [TransformationCode], [TransformationType], [TransformationClassID], [TransformationVersionGUID], [TransformationGUID], [TransformationLastModified], [TransformationIsHierarchical], [TransformationHierarchicalXML], [TransformationCSS]) VALUES (303, N'ImageGallery_thumbnails180', N'<a href="?imagepath=<%# System.Web.HttpUtility.UrlEncode(DataBinder.Eval(Container, "DataItem.NodeAliasPath").ToString()) %>">
<%#IfEmpty(Eval("FileAttachment"), "no image", "<img alt=''" + Eval("FileDescription") + "'' src=''" + GetFileUrl("FileAttachment") + "?maxsidesize=180'' border=''0'' />")%>
</a>', N'ascx', 1685, N'6e279d2c-2316-4d33-8ee5-e3c6753f6b4b', '376eb9e5-13a8-4baf-a9e9-f20d11dfea2a', '20110518 13:07:18', 0, N'', N'')
INSERT INTO [CMS_Transformation] ([TransformationID], [TransformationName], [TransformationCode], [TransformationType], [TransformationClassID], [TransformationVersionGUID], [TransformationGUID], [TransformationLastModified], [TransformationIsHierarchical], [TransformationHierarchicalXML], [TransformationCSS]) VALUES (294, N'ImageGallery_thumbnails200', N'<a href="?imagepath=<%# System.Web.HttpUtility.UrlEncode(DataBinder.Eval(Container, "DataItem.NodeAliasPath").ToString()) %>">
<%#IfEmpty(Eval("FileAttachment"), "no image", "<img alt=''" + Eval("FileName", true) + "'' src=''" + GetFileUrl("FileAttachment") + "?maxsidesize=200'' border=''0'' />")%>
</a>', N'ascx', 1685, N'900a763c-b398-4cf9-87e7-554c9f41db02', 'bc05b40a-fba1-4f66-9730-6bb35fc85e85', '20110617 08:51:09', 0, N'', N'')
INSERT INTO [CMS_Transformation] ([TransformationID], [TransformationName], [TransformationCode], [TransformationType], [TransformationClassID], [TransformationVersionGUID], [TransformationGUID], [TransformationLastModified], [TransformationIsHierarchical], [TransformationHierarchicalXML], [TransformationCSS]) VALUES (295, N'ImageGallery_thumbnails300', N'<a href="?imagepath=<%# System.Web.HttpUtility.UrlEncode(DataBinder.Eval(Container, "DataItem.NodeAliasPath").ToString()) %>">
<%#IfEmpty(Eval("FileAttachment"), "no image", "<img alt=''" + Eval("FileName") + "'' src=''" + GetFileUrl("FileAttachment") + "?maxsidesize=300'' border=''0'' />")%>
</a>', N'ascx', 1685, N'33594609-5666-4051-abf8-2732566b5ff4', '10a47820-4a54-4fec-8a69-0e04853155c7', '20110518 13:09:06', 0, N'', N'')
INSERT INTO [CMS_Transformation] ([TransformationID], [TransformationName], [TransformationCode], [TransformationType], [TransformationClassID], [TransformationVersionGUID], [TransformationGUID], [TransformationLastModified], [TransformationIsHierarchical], [TransformationHierarchicalXML], [TransformationCSS]) VALUES (300, N'Lightbox', N'<a href="<%# GetDocumentUrl() %>" rel="lightbox[group]" rev="<%# Eval("NodeAliasPath") %>" title="<%# Eval("FileDescription", true) %>"><img src="<%# GetFileUrl("FileAttachment") %>?maxsidesize=150" alt="<%# Eval("FileName", true) %>" /></a>', N'ascx', 1685, N'4685b6fa-ab95-4a89-9d6d-8bfa17649f5a', '2673a82a-c87b-47aa-bc33-544d5c8bbc79', '20110518 16:01:04', 0, N'', N'')
INSERT INTO [CMS_Transformation] ([TransformationID], [TransformationName], [TransformationCode], [TransformationType], [TransformationClassID], [TransformationVersionGUID], [TransformationGUID], [TransformationLastModified], [TransformationIsHierarchical], [TransformationHierarchicalXML], [TransformationCSS]) VALUES (301, N'LightboxSelected', N'<img src="<%# GetFileUrl("FileAttachment") %>" title="<%# Eval("FileName",true) %>" alt="" />', N'ascx', 1685, N'5e608953-2ac1-4dd2-8695-4dc9ac71baa2', '6440767f-166e-4e66-960a-41048df70eb6', '20110317 14:51:11', 0, N'', NULL)
INSERT INTO [CMS_Transformation] ([TransformationID], [TransformationName], [TransformationCode], [TransformationType], [TransformationClassID], [TransformationVersionGUID], [TransformationGUID], [TransformationLastModified], [TransformationIsHierarchical], [TransformationHierarchicalXML], [TransformationCSS]) VALUES (2259, N'LightboxSimple', N'<a href="<%# GetDocumentUrl() %>" rel="lightbox" rev="<%# Eval("NodeAliasPath") %>" title="<%# Eval("FileDescription", true) %>"><img src="<%# GetFileUrl("FileAttachment") %>?maxsidesize=150" alt="<%# Eval("FileName", true) %>" /></a>', N'ascx', 1685, N'f1f9625a-428c-4cd5-9f7c-cbf709ab6cb8', '24ded2bd-cc9a-4ed1-9c75-95520bea1875', '20111125 17:02:26', 0, N'', N'')
INSERT INTO [CMS_Transformation] ([TransformationID], [TransformationName], [TransformationCode], [TransformationType], [TransformationClassID], [TransformationVersionGUID], [TransformationGUID], [TransformationLastModified], [TransformationIsHierarchical], [TransformationHierarchicalXML], [TransformationCSS]) VALUES (1082, N'RSSItem', N'<item>
  <guid isPermaLink="false"><%# Eval("NodeGUID") %></guid>
  <title><%# EvalCDATA("FileName") %></title>
  <description><%# EvalCDATA("FileDescription") %></description>
  <pubDate><%# GetRSSDateTime(Eval("DocumentCreatedWhen")) %></pubDate>
  <link><![CDATA[<%# GetAbsoluteUrl(GetDocumentUrlForFeed(), Eval("SiteName")) %>]]></link>
</item>', N'ascx', 1685, N'78697e5c-f259-4c3c-ba21-07b516f3e1cc', 'b365d59a-46fb-46e8-b27c-7f9a220f930b', '20100802 15:13:00', 0, N'', NULL)
SET IDENTITY_INSERT [CMS_Transformation] OFF
