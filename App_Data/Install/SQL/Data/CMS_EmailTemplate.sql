SET IDENTITY_INSERT [CMS_EmailTemplate] ON
INSERT INTO [CMS_EmailTemplate] ([EmailTemplateID], [EmailTemplateName], [EmailTemplateDisplayName], [EmailTemplateText], [EmailTemplateSiteID], [EmailTemplateGUID], [EmailTemplateLastModified], [EmailTemplatePlainText], [EmailTemplateSubject], [EmailTemplateFrom], [EmailTemplateCc], [EmailTemplateBcc], [EmailTemplateType]) VALUES (744, N'Blog.NotificationToModerators', N'Blogs - Notification to blog moderators', N'<html>
  <head>
    <style>
    body, td
    {
      font-size: 12px; 
      font-family: Arial;
    }
    </style>
  </head>  
  <body>
  <p>
    New blog post comment was added and now is waiting for your approval:
  </p>
  <table>
    <tr valign="top">
    <td>
    <strong>Blog post:&nbsp;</strong>
    </td>
    <td>
    <a href="{%BlogPostLink|(user)administrator|(hash)36b5558e168e922c56383acc40fe079b2efd68f857b64f97a385862abe2844de%}">{%BlogPost.DocumentName|(user)administrator|(hash)424d42b9dc90388d432b8deb80a8ad90d83db9f839f9b724a653601fea4c86e5%}</a>
    </td>
    </tr>
    <tr valign="top">
    <td>
    <strong>Blog:&nbsp;</strong>
    </td>
    <td>
    <a href="{%BlogLink|(user)administrator|(hash)a1ddcef316152dbb4652173cd4ac8626475803af9c5499abb3924c0b4efaeaba%}">{%Blog.DocumentName|(user)administrator|(hash)88bc0ea070a09841abcf6bfbd1263528493bffa42525c540c6988e463c1a0ab9%}</a>
    </td>
    </tr>
    <tr valign="top">
    <td>
    <strong>Added by:&nbsp;</strong>
    </td>
    <td>
    {% TrimSitePrefix(Comment.CommentUserName)|(user)administrator|(hash)4b71a93e79e739fb3ae7b6974ef7976e89cf36312bde20e9d6facfda1ed037f7%}
    </td>
    </tr>
    <tr valign="top">
    <td>
    <strong>Date and time:&nbsp;</strong>
    </td>
    <td>
    {%Comment.CommentDate|(user)administrator|(hash)6b2f1f598c0d8e5749a513a8a3f4fd36ab983e7f73a2a918c3dbb889cbc5e0ba%}
    </td>
    </tr>
    <tr valign="top">
    <td>
    <strong>Text:&nbsp;</strong>
    </td>
    <td>
    {%Comment.CommentText|(user)administrator|(hash)cba96462e792288c8a01d19ae320c62d9e44ad30fcf4edc7f9fc79e3d111e8c7%}   
    </td>
    </tr>
  </table>    
  </body>
</html>', NULL, '744e6923-0224-4a10-b633-7922646db03d', '20110905 17:29:44', N'New blog post comment was added and now is waiting for your approval: 
Blog post:   [url={%BlogPostLink|(user)administrator|(hash)36b5558e168e922c56383acc40fe079b2efd68f857b64f97a385862abe2844de%}]{%BlogPost.DocumentName|(user)administrator|(hash)424d42b9dc90388d432b8deb80a8ad90d83db9f839f9b724a653601fea4c86e5%}[/url]
Blog:   [url={%BlogLink|(user)administrator|(hash)a1ddcef316152dbb4652173cd4ac8626475803af9c5499abb3924c0b4efaeaba%}]{%Blog.DocumentName|(user)administrator|(hash)88bc0ea070a09841abcf6bfbd1263528493bffa42525c540c6988e463c1a0ab9%}[/url]
Added by:   {% TrimSitePrefix(Comment.CommentUserName)|(user)administrator|(hash)4b71a93e79e739fb3ae7b6974ef7976e89cf36312bde20e9d6facfda1ed037f7%}  
Date and time:   {%Comment.CommentDate|(user)administrator|(hash)6b2f1f598c0d8e5749a513a8a3f4fd36ab983e7f73a2a918c3dbb889cbc5e0ba%}  
Text:   {%Comment.CommentText|(user)administrator|(hash)cba96462e792288c8a01d19ae320c62d9e44ad30fcf4edc7f9fc79e3d111e8c7%}', N'Comment needs to be approved on the blog post {%BlogPost.DocumentName|(user)administrator|(hash)424d42b9dc90388d432b8deb80a8ad90d83db9f839f9b724a653601fea4c86e5%}', N'', N'', N'', N'blog')
INSERT INTO [CMS_EmailTemplate] ([EmailTemplateID], [EmailTemplateName], [EmailTemplateDisplayName], [EmailTemplateText], [EmailTemplateSiteID], [EmailTemplateGUID], [EmailTemplateLastModified], [EmailTemplatePlainText], [EmailTemplateSubject], [EmailTemplateFrom], [EmailTemplateCc], [EmailTemplateBcc], [EmailTemplateType]) VALUES (769, N'Blog.NewCommentNotification', N'Blogs - Notification to blog owner', N'<html>
  <head>
    <style>
    body, td
    {
      font-size: 12px; 
      font-family: Arial
    }
    </style>
  </head>  
  <body>
  <p>
    New comment was added to your blog post:
  </p>
  <table>
    <tr valign="top">
    <td>
    <strong>Blog post:&nbsp;</strong>
    </td>
    <td>
    <a href="{%BlogPostLink|(user)administrator|(hash)36b5558e168e922c56383acc40fe079b2efd68f857b64f97a385862abe2844de%}">{%BlogPost.DocumentName|(user)administrator|(hash)424d42b9dc90388d432b8deb80a8ad90d83db9f839f9b724a653601fea4c86e5%}</a>
    </td>
    </tr>
    <tr valign="top">
    <td>
    <strong>Blog:&nbsp;</strong>
    </td>
    <td>
    <a href="{%BlogLink|(user)administrator|(hash)a1ddcef316152dbb4652173cd4ac8626475803af9c5499abb3924c0b4efaeaba%}">{%Blog.DocumentName|(user)administrator|(hash)88bc0ea070a09841abcf6bfbd1263528493bffa42525c540c6988e463c1a0ab9%}</a>
    </td>
    </tr>
    <tr valign="top">
    <td>
    <strong>Added by:&nbsp;</strong>
    </td>
    <td>
    {% TrimSitePrefix(Comment.CommentUserName)|(user)administrator|(hash)4b71a93e79e739fb3ae7b6974ef7976e89cf36312bde20e9d6facfda1ed037f7%}
    </td>
    </tr>
    <tr valign="top">
    <td>
    <strong>Date and time:&nbsp;</strong>
    </td>
    <td>
    {%Comment.CommentDate|(user)administrator|(hash)6b2f1f598c0d8e5749a513a8a3f4fd36ab983e7f73a2a918c3dbb889cbc5e0ba%}
    </td>
    </tr>
    <tr valign="top">
    <td>
    <strong>Text:&nbsp;</strong>
    </td>
    <td>
    {%Comment.CommentText|(user)administrator|(hash)cba96462e792288c8a01d19ae320c62d9e44ad30fcf4edc7f9fc79e3d111e8c7%}
    </td>
    </tr>
  </table>    
  </body>
</html>', NULL, 'f5083a57-1355-430a-a1b9-5679e3d0e5fc', '20110905 17:11:27', N'New comment was added to your blog post:
Blog post: [url={%BlogPostLink|(user)administrator|(hash)36b5558e168e922c56383acc40fe079b2efd68f857b64f97a385862abe2844de%}]{%BlogPost.DocumentName|(user)administrator|(hash)424d42b9dc90388d432b8deb80a8ad90d83db9f839f9b724a653601fea4c86e5%}[/url]
Blog: [url={%BlogLink|(user)administrator|(hash)a1ddcef316152dbb4652173cd4ac8626475803af9c5499abb3924c0b4efaeaba%}]{%Blog.DocumentName|(user)administrator|(hash)88bc0ea070a09841abcf6bfbd1263528493bffa42525c540c6988e463c1a0ab9%}[/url]
Added by: {%TrimSitePrefix(Comment.CommentUserName)|(user)administrator|(hash)82f847d9f01b4508eac406822bdbbe98c1a80733642f2fcd551a71f801403fc2%}
Date and time: {%Comment.CommentDate|(user)administrator|(hash)6b2f1f598c0d8e5749a513a8a3f4fd36ab983e7f73a2a918c3dbb889cbc5e0ba%}
Text: {%Comment.CommentText|(user)administrator|(hash)cba96462e792288c8a01d19ae320c62d9e44ad30fcf4edc7f9fc79e3d111e8c7%}', N'New comment was added to your blog post {%BlogPost.DocumentName|(user)administrator|(hash)424d42b9dc90388d432b8deb80a8ad90d83db9f839f9b724a653601fea4c86e5%}', N'', N'', N'', N'blog')
INSERT INTO [CMS_EmailTemplate] ([EmailTemplateID], [EmailTemplateName], [EmailTemplateDisplayName], [EmailTemplateText], [EmailTemplateSiteID], [EmailTemplateGUID], [EmailTemplateLastModified], [EmailTemplatePlainText], [EmailTemplateSubject], [EmailTemplateFrom], [EmailTemplateCc], [EmailTemplateBcc], [EmailTemplateType]) VALUES (768, N'Blog.NotificationToSubcribers', N'Blogs - Notification to blog post subscribers', N'<html>
  <head>
    <style>
    body, td
    {
      font-size: 12px; 
      font-family: Arial
    }
    </style>
  </head>  
  <body>
  <p>
    New comment was added to the blog post you are subscribed to:
  </p>
  <table>
    <tr valign="top">
    <td>
    <strong>Blog post:&nbsp;</strong>
    </td>
    <td>
    <a href="{%BlogPostLink|(user)administrator|(hash)36b5558e168e922c56383acc40fe079b2efd68f857b64f97a385862abe2844de%}">{%BlogPost.DocumentName|(user)administrator|(hash)424d42b9dc90388d432b8deb80a8ad90d83db9f839f9b724a653601fea4c86e5%}</a>
    </td>
    </tr>
    <tr valign="top">
    <td>
    <strong>Blog:&nbsp;</strong>
    </td>
    <td>
    <a href="{%BlogLink|(user)administrator|(hash)a1ddcef316152dbb4652173cd4ac8626475803af9c5499abb3924c0b4efaeaba%}">{%Blog.DocumentName|(user)administrator|(hash)88bc0ea070a09841abcf6bfbd1263528493bffa42525c540c6988e463c1a0ab9%}</a>
    </td>
    </tr>
    <tr valign="top">
    <td>
    <strong>Added by:&nbsp;</strong>
    </td>
    <td>
    {%TrimSitePrefix(Comment.CommentUserName)|(user)administrator|(hash)82f847d9f01b4508eac406822bdbbe98c1a80733642f2fcd551a71f801403fc2%}
    </td>
    </tr>
    <tr valign="top">
    <td>
    <strong>Date and time:&nbsp;</strong>
    </td>
    <td>
    {%Comment.CommentDate|(user)administrator|(hash)6b2f1f598c0d8e5749a513a8a3f4fd36ab983e7f73a2a918c3dbb889cbc5e0ba%}
    </td>
    </tr>
    <tr valign="top">
    <td>
    <strong>Text:&nbsp;</strong>
    </td>
    <td>
    {%Comment.CommentText|(user)administrator|(hash)cba96462e792288c8a01d19ae320c62d9e44ad30fcf4edc7f9fc79e3d111e8c7%}
    </td>
    </tr>
  </table>    
  <p>
        <a href="{%unsubscriptionlink|(user)administrator|(hash)3230c3e21f6b1607e56b3f829dcd57b84dd3dfd9d5ebcd0ae34ee4d093e70503%}">Click here to unsubscribe</a>
  </p>
  </body>
</html>', NULL, 'c82d3577-1657-43b5-8fd8-b3172a87b9d1', '20110905 17:30:45', N'New comment was added to the blog post you are subscribed to:
Blog post: [url={%BlogPostLink|(user)administrator|(hash)36b5558e168e922c56383acc40fe079b2efd68f857b64f97a385862abe2844de%}]{%BlogPost.DocumentName|(user)administrator|(hash)424d42b9dc90388d432b8deb80a8ad90d83db9f839f9b724a653601fea4c86e5%}[/url]
Blog: [url={%BlogLink|(user)administrator|(hash)a1ddcef316152dbb4652173cd4ac8626475803af9c5499abb3924c0b4efaeaba%}]{%Blog.DocumentName|(user)administrator|(hash)88bc0ea070a09841abcf6bfbd1263528493bffa42525c540c6988e463c1a0ab9%}[/url]
Added by: {%TrimSitePrefix(Comment.CommentUserName)|(user)administrator|(hash)82f847d9f01b4508eac406822bdbbe98c1a80733642f2fcd551a71f801403fc2%}
Date and time: {%Comment.CommentDate|(user)administrator|(hash)6b2f1f598c0d8e5749a513a8a3f4fd36ab983e7f73a2a918c3dbb889cbc5e0ba%}
Text: {%Comment.CommentText|(user)administrator|(hash)cba96462e792288c8a01d19ae320c62d9e44ad30fcf4edc7f9fc79e3d111e8c7%}
[url={%unsubscriptionlink|(user)administrator|(hash)3230c3e21f6b1607e56b3f829dcd57b84dd3dfd9d5ebcd0ae34ee4d093e70503%}]Click here to unsubscribe[/url]', N'New comment was added to the blog post {%BlogPost.DocumentName|(user)administrator|(hash)424d42b9dc90388d432b8deb80a8ad90d83db9f839f9b724a653601fea4c86e5%}', N'', N'', N'', N'blog')
INSERT INTO [CMS_EmailTemplate] ([EmailTemplateID], [EmailTemplateName], [EmailTemplateDisplayName], [EmailTemplateText], [EmailTemplateSiteID], [EmailTemplateGUID], [EmailTemplateLastModified], [EmailTemplatePlainText], [EmailTemplateSubject], [EmailTemplateFrom], [EmailTemplateCc], [EmailTemplateBcc], [EmailTemplateType]) VALUES (760, N'Boards.NotificationToModerators', N'Boards - Notification to board moderators', N'<html>
  <head>
    <style>
    body, td
    {
      font-size: 12px; 
      font-family: Arial;
    }
    </style>
  </head>  
  <body>
  <p>
    New message was added and now is waiting for your approval:
  </p>
  <table>
    <tr valign="top">
    <td>
    <strong>Board:&nbsp;</strong>
    </td>
    <td>
    <a href="{%DocumentLink%}">{%Board.BoardDisplayName|(user)administrator|(hash)bb060a4c64bcfb096333ba7efd49626a1c8fd313bc4e5a00cd874de545443d12%}</a>
    </td>
    </tr>
    <tr valign="top">
    <td>
    <strong>Added by:&nbsp;</strong>
    </td>
    <td>
    {%TrimSitePrefix(Message.MessageUserName)|(user)administrator|(hash)9c50b792057764e71c40b15c714b181ff47d599f61e214868e4c1ef499fe5dca%}
    </td>
    </tr>
    <tr valign="top">
    <td>
    <strong>Date and time:&nbsp;</strong>
    </td>
    <td>
    {%Message.MessageInserted|(user)administrator|(hash)7a0f0ac6cf10a84d82362d66fd55fa12060d13fc6c7530c561083d936a01a3eb%}
    </td>
    </tr>
    <tr valign="top">
    <td>
    <strong>Text:&nbsp;</strong>
    </td>
    <td>
    {%Message.MessageText|(user)administrator|(hash)437c7c76660588790076b39fd34e1daf8bbe1a319c3cf7f41a12b8a8ad59601e%}
    </td>
    </tr>
  </table>    
  </body>
</html>', NULL, '35a95893-ee26-449e-b257-edb134a67c44', '20110922 15:45:42', N'New message was added and now is waiting for your approval: 
Board:   [url={%DocumentLink%}]{%Board.BoardDsiplayName|(user)administrator|(hash)83414e3171ab82d20995f4689e5b7ba9088662c0455783a5602d875a5d90aa0b%}[/url]
Added by:   {%TrimSitePrefix(Message.MessageUserName)|(user)administrator|(hash)9c50b792057764e71c40b15c714b181ff47d599f61e214868e4c1ef499fe5dca%}  
Date and time:   {%Message.MessageInserted|(user)administrator|(hash)7a0f0ac6cf10a84d82362d66fd55fa12060d13fc6c7530c561083d936a01a3eb%}  
Text:   {%Message.MessageText|(user)administrator|(hash)437c7c76660588790076b39fd34e1daf8bbe1a319c3cf7f41a12b8a8ad59601e%}', N'Message needs to be approved in the board {%Board.BoardDisplayName|(user)administrator|(hash)bb060a4c64bcfb096333ba7efd49626a1c8fd313bc4e5a00cd874de545443d12%}', N'', N'', N'', N'boards')
INSERT INTO [CMS_EmailTemplate] ([EmailTemplateID], [EmailTemplateName], [EmailTemplateDisplayName], [EmailTemplateText], [EmailTemplateSiteID], [EmailTemplateGUID], [EmailTemplateLastModified], [EmailTemplatePlainText], [EmailTemplateSubject], [EmailTemplateFrom], [EmailTemplateCc], [EmailTemplateBcc], [EmailTemplateType]) VALUES (751, N'Boards.NotificationToSubscribers', N'Boards - Notification to board subscribers', N'<html>
  <head>
    <style>
    body, td
    {
      font-size: 12px; 
      font-family: Arial;
    }
    </style>
  </head>  
  <body>
  <p>
    New message was added to the board you are subscribed to:
  </p>
  <table>
    <tr valign="top">
    <td>
    <strong>Board:&nbsp;</strong>
    </td>
    <td>
    <a href="{%DocumentLink%}">{%Board.BoardDisplayName|(user)administrator|(hash)bb060a4c64bcfb096333ba7efd49626a1c8fd313bc4e5a00cd874de545443d12%}</a>
    </td>
    </tr>
    <tr valign="top">
    <td>
    <strong>Added by:&nbsp;</strong>
    </td>
    <td>
    {%TrimSitePrefix(Message.MessageUserName)|(user)administrator|(hash)9c50b792057764e71c40b15c714b181ff47d599f61e214868e4c1ef499fe5dca%}
    </td>
    </tr>
    <tr valign="top">
    <td>
    <strong>Date and time:&nbsp;</strong>
    </td>
    <td>
    {%Message.MessageInserted|(user)administrator|(hash)7a0f0ac6cf10a84d82362d66fd55fa12060d13fc6c7530c561083d936a01a3eb%}
    </td>
    </tr>
    <tr valign="top">
    <td>
    <strong>Text:&nbsp;</strong>
    </td>
    <td>
    {%Message.MessageText|(user)administrator|(hash)437c7c76660588790076b39fd34e1daf8bbe1a319c3cf7f41a12b8a8ad59601e%}
    </td>
    </tr>
  </table>    
  <p>
        <a href="{%UnsubscriptionLink%}">Click here to unsubscribe</a>
  </p>
  </body>
</html>', NULL, '856a1cbf-6340-4c20-b7da-ac32810b8546', '20110922 15:46:21', N'New message was added to the board you are subscribed to: 
Board:   [url={%DocumentLink%}"]{%Board.BoardDisplayName|(user)administrator|(hash)bb060a4c64bcfb096333ba7efd49626a1c8fd313bc4e5a00cd874de545443d12%}[/url]
Added by:   {%TrimSitePrefix(Message.MessageUserName)|(user)administrator|(hash)9c50b792057764e71c40b15c714b181ff47d599f61e214868e4c1ef499fe5dca%}  
Date and time:   {%Message.MessageInserted|(user)administrator|(hash)7a0f0ac6cf10a84d82362d66fd55fa12060d13fc6c7530c561083d936a01a3eb%}  
Text:   {%Message.MessageText|(user)administrator|(hash)437c7c76660588790076b39fd34e1daf8bbe1a319c3cf7f41a12b8a8ad59601e%}  
[url={%UnsubscriptionLink%}]Click here to unsubscribe[/url]', N'New message was added to the board {%Board.BoardDisplayName|(user)administrator|(hash)bb060a4c64bcfb096333ba7efd49626a1c8fd313bc4e5a00cd874de545443d12%}', N'', N'', N'', N'boards')
INSERT INTO [CMS_EmailTemplate] ([EmailTemplateID], [EmailTemplateName], [EmailTemplateDisplayName], [EmailTemplateText], [EmailTemplateSiteID], [EmailTemplateGUID], [EmailTemplateLastModified], [EmailTemplatePlainText], [EmailTemplateSubject], [EmailTemplateFrom], [EmailTemplateCc], [EmailTemplateBcc], [EmailTemplateType]) VALUES (705, N'BookingEvent.Invitation', N'Booking system - Event invitation', N'<html><head></head><body>
<p>Hello,</p>
<p>Thank you for your registration for event {%EventName|(encode)false%}. This is an e-mail confirmation that you have been registered. Below, you can find event details:</p>
<p><strong>Event: {%EventName|(encode)false%}</strong></p>
<p><em>{%EventSummary|(encode)false%}</em></p>
<p>{%EventDetails|(encode)false%}</p>
<p><strong>Location:</strong><br />
{%EventLocation|(encode)false%}</p>
<p><strong>Date:</strong><br />
{%EventDateString|(encode)false%}</p>
</body>
</html>', NULL, 'eb37ce02-7853-4f91-bbd1-127597ebce66', '20110717 12:06:11', N'Hello,
Thank you for your registration for event {%EventName|(encode)false%}. This is an e-mail confirmation that you have been registered. Below, you can find event details:
Event: {%EventName|(encode)false%}
{%EventSummary|(encode)false%}
{%EventDetails|(encode)false%}
Location:
{%EventLocation|(encode)false%}
Date:
{%EventDateString|(encode)false%}', N'', N'', N'', N'', N'bookingevent')
INSERT INTO [CMS_EmailTemplate] ([EmailTemplateID], [EmailTemplateName], [EmailTemplateDisplayName], [EmailTemplateText], [EmailTemplateSiteID], [EmailTemplateGUID], [EmailTemplateLastModified], [EmailTemplatePlainText], [EmailTemplateSubject], [EmailTemplateFrom], [EmailTemplateCc], [EmailTemplateBcc], [EmailTemplateType]) VALUES (843, N'Ecommerce.EproductExpirationNotification', N'E-commerce - E-product expiration notification', N'You have bought the folowing e-products, please download them until their download links expire. Once the download link expires, you won''t be able to download the file.
<br />
{%EproductsTable.ApplyTransformation("Ecommerce.Transformations.Order_EproductsTable")|(user)administrator|(hash)fef517a14a52a2216f7d25ef0905f4a692857a108b31d9e064cc8ceb385a240e%}
<br />
You can download your e-products also from My profile -> Orders section until their expiration.
<br />
This is an automatic reminder, please do not respond.
<br />
Thank you.', NULL, '26c89400-44e4-4208-92ba-cdd7d6bd407e', '20110907 17:14:42', N'You have bought the folowing e-products, please download them until their download links expire. Once the download link expires, you won''t be able to download the file.
{%EproductsTable.ApplyTransformation("Ecommerce.Transformations.Order_EproductsTable")|(user)administrator|(hash)fef517a14a52a2216f7d25ef0905f4a692857a108b31d9e064cc8ceb385a240e%}
You can download your e-products also from My profile -> Orders section until their expiration.
This is an automatic reminder, please do not respond.
Thank you.', N'', N'', N'', N'', N'ecommerceeproductexpiration')
INSERT INTO [CMS_EmailTemplate] ([EmailTemplateID], [EmailTemplateName], [EmailTemplateDisplayName], [EmailTemplateText], [EmailTemplateSiteID], [EmailTemplateGUID], [EmailTemplateLastModified], [EmailTemplatePlainText], [EmailTemplateSubject], [EmailTemplateFrom], [EmailTemplateCc], [EmailTemplateBcc], [EmailTemplateType]) VALUES (167, N'Ecommerce.OrderNotificationToAdmin', N'E-commerce - Order notification to administrator', N'<html><head></head><body>
<table cellspacing="0" cellpadding="5" bordercolor="black" border="1" width="600">
    <tbody>
        <tr>
            <td height="50" valign="bottom" colspan="2">
            <table height="100%" width="100%">
                <tbody>
                    <tr>
                        <td style="text-align: left; vertical-align: bottom;"><span style="font-size: 18pt;">New order</span></td>
                        <td style="text-align: center; vertical-align: middle;"><span style="font-family: Garamond,Times,serif; font-size: 24pt; font-style: italic;">Company logo</span></td>
                    </tr>
                </tbody>
            </table>
            </td>
        </tr>
        <tr>
            <td style="text-align: left;"><br />
            <table width="100%">
                <tbody>
                    <tr>
                        <td valign="bottom" style="text-align: left;"><strong>Invoice number:</strong></td>
                        <td style="text-align: right; padding-right: 10px;">{%Order.OrderInvoiceNumber|(user)administrator|(hash)2526fa4a5e2bc2b4a58e8fab08d08b139025706f1f614719df397bc9bd3b90a5%}</td>
                    </tr>
                </tbody>
            </table>
            </td>
            <td style="text-align: left;"><br />
            <table width="100%">
                <tbody>
                    <tr>
                        <td valign="bottom" style="text-align: left;"><strong>Order date:</strong></td>
                        <td style="text-align: right; padding-right: 10px;">{%Format(Order.OrderDate, "{0:d}")|(user)administrator|(hash)1cccd184308256a5f6b82024b0aa03bb4e9c9d4eaa029664e3714197a15300b6%}</td>
                    </tr>
                    <tr>
                        <td valign="bottom" style="text-align: left;"><strong>Order status:</strong></td>
                        <td style="text-align: right; padding-right: 10px;">{%GetResourceString(OrderStatus.StatusDisplayName)|(user)administrator|(hash)09902315904d1db9014aea7bf4d5d4d5680f68f7aed449931a8570c2d9dc27e6%} </td>
                    </tr>
                </tbody>
            </table>
            </td>
        </tr>
        <tr>
            <td width="50%" style="text-align: left; vertical-align: top;"><strong>Supplier</strong>
            <br />
            <br />
            <table>
                <tbody>
                    <tr>
                        <td>Company address</td>
                    </tr>
                </tbody>
            </table>
            </td>
            <td width="50%" style="text-align: left; vertical-align: top;"><span style="font-weight: bold;">Customer</span><br />
            <br />
            {%BillingAddress.ApplyTransformation("Ecommerce.Transformations.Order_Address"|(user)administrator|(hash)7960adbbbb40670d0bf29659fe42435cc7ec2db333a9b4f6d7d4d8133fe2c2e1%}
            <br />
            <strong>Company address:</strong>
            {%CompanyAddress.ApplyTransformation("Ecommerce.Transformations.Order_Address"|(user)administrator|(hash)98f29f2a9bdf80406f46478366c2c8276adaa3775727fa4a66819b5665e2abb9%}
            </td>
        </tr>
        <tr>
            <td colspan="2">
            <table width="100%">
                <tbody>
                    <tr>
                        <td style="text-align: left;"><span style="font-weight: bold;">Payment option</span></td>
                    </tr>
                    <tr>
                        <td style="text-align: center;">{%GetResourceString(PaymentOption.PaymentOptionDisplayName)|(user)administrator|(hash)f4bd53a4cf633921fb1f15e9097ff5312b27b1eb54cf01fb5669c38dbdec1cb1%}</td>
                    </tr>
                </tbody>
            </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
            <table width="100%">
                <tbody>
                    <tr>
                        <td style="text-align: left;"><span style="font-weight: bold;">Shipping option</span></td>
                    </tr>
                    <tr>
                        <td style="text-align: center;">{%GetResourceString(ShippingOption.ShippingOptionDisplayName)|(user)administrator|(hash)748fabbfafb7f9dfdf45675a28eba4bdc4c072592e58a0c358051d0a3153bf8e%}</td>
                    </tr>
                </tbody>
            </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: left;">
            {%ContentTable.ApplyTransformation("Ecommerce.Transformations.Order_ContentTable", "Ecommerce.Transformations.Order_ContentTableHeader", "Ecommerce.Transformations.Order_ContentTableFooter")|(user)administrator|(hash)9e8c2d4859c4e8c80639a9cffb6cc25fc935fb55e7b03c135c935bf947d8bcf0%}
            <hr size="1" />
            <div style="text-align: right;">
            <table cellpadding="5" style="text-align: left;">
                <tbody>
                    <tr>
                        <td><strong>Total shipping:</strong></td>
                        <td style="text-align: right; padding-right: 0px;"><strong>{%TotalShipping.Format(Currency.CurrencyFormatString)|(user)administrator|(hash)ab9d10aa152f2e3a3c22e79c05607ad18691707fce7787aba2d512335482886e%}</strong></td>
                    </tr>
                    <tr>
                        <td><strong>Total price:</strong></td>
                        <td style="text-align: right; padding-right: 0px;"><strong>{%TotalPrice.Format(Currency.CurrencyFormatString)|(user)administrator|(hash)49695c0d3fa3613ef4a7958194fc8cfd077dee4d8198799beaf432a0dbf8c7f0%}</strong></td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top;"><strong>Tax summary:</strong></td>
                        <td style="text-align: right; padding-right: 0px;">{%ContentTaxesTable.ApplyTransformation("Ecommerce.Transformations.Order_TaxesTable", "Ecommerce.Transformations.Order_TaxesTableHeader", "Ecommerce.Transformations.Order_TaxesTableFooter")|(user)administrator|(hash)76921da8b0bdaf955b33c85e57dab8feaccc01f0ae2cedc9def953d4b39660c1%}</td>
                    </tr>
                </tbody>
            </table>
            </div>
            <div style="height: 120px;">&nbsp;</div>
            </td>
        </tr>
        <tr>
            <td colspan="2">
            <table width="100%">
                <tbody>
                    <tr>
                        <td style="text-align: left;"><span style="font-weight: bold;">Order note</span></td>
                    </tr>
                    <tr>
                        <td style="text-align: left;">{%Order.OrderNote|(encode)|(user)administrator|(hash)a5ed7cf37728b6c1ad6cf0cf4c5d39df4ed3d35a95731a7bc7841d04318951bf%}</td>
                    </tr>
                </tbody>
            </table>
            </td>
        </tr>
    </tbody>
</table>
<div style="padding-top-10px;">{%NewOrderLink%}</div>
</body>
</html>', NULL, 'f49163f2-32c3-4c7b-ab1b-c128d621c02f', '20110930 08:13:28', N'New order Company logo 
 
Invoice number: {%Order.OrderInvoiceNumber|(user)administrator|(hash)2526fa4a5e2bc2b4a58e8fab08d08b139025706f1f614719df397bc9bd3b90a5%}
 
Order date: {%Format(Order.OrderDate, "{0:d}")|(user)administrator|(hash)1cccd184308256a5f6b82024b0aa03bb4e9c9d4eaa029664e3714197a15300b6%}
Order status: {%GetResourceString(OrderStatus.StatusDisplayName)|(user)administrator|(hash)09902315904d1db9014aea7bf4d5d4d5680f68f7aed449931a8570c2d9dc27e6%} 
 
Supplier 
Company address 
Customer 
{%BillingAddress.ApplyTransformation("Ecommerce.Transformations.Order_Address"|(user)administrator|(hash)7960adbbbb40670d0bf29659fe42435cc7ec2db333a9b4f6d7d4d8133fe2c2e1%}
Company address: {%CompanyAddress.ApplyTransformation("Ecommerce.Transformations.Order_Address"|(user)administrator|(hash)98f29f2a9bdf80406f46478366c2c8276adaa3775727fa4a66819b5665e2abb9%}
Payment option  
{%GetResourceString(PaymentOption.PaymentOptionDisplayName)|(user)administrator|(hash)f4bd53a4cf633921fb1f15e9097ff5312b27b1eb54cf01fb5669c38dbdec1cb1%}
 
Shipping option  
{%GetResourceString(ShippingOption.ShippingOptionDisplayName)|(user)administrator|(hash)748fabbfafb7f9dfdf45675a28eba4bdc4c072592e58a0c358051d0a3153bf8e%}
 
{%ContentTable.ApplyTransformation("Ecommerce.Transformations.Order_ContentTable", "Ecommerce.Transformations.Order_ContentTableHeader", "Ecommerce.Transformations.Order_ContentTableFooter")|(user)administrator|(hash)9e8c2d4859c4e8c80639a9cffb6cc25fc935fb55e7b03c135c935bf947d8bcf0%}
--------------------------------------------------------------------------------
Total shipping: {%TotalShipping.Format(Currency.CurrencyFormatString)|(user)administrator|(hash)ab9d10aa152f2e3a3c22e79c05607ad18691707fce7787aba2d512335482886e%}
Total price: {%TotalPrice.Format(Currency.CurrencyFormatString)|(user)administrator|(hash)49695c0d3fa3613ef4a7958194fc8cfd077dee4d8198799beaf432a0dbf8c7f0%}
Tax summary: {%ContentTaxesTable.ApplyTransformation("Ecommerce.Transformations.Order_TaxesTable", "Ecommerce.Transformations.Order_TaxesTableHeader", "Ecommerce.Transformations.Order_TaxesTableFooter")|(user)administrator|(hash)76921da8b0bdaf955b33c85e57dab8feaccc01f0ae2cedc9def953d4b39660c1%}
  
Order note  
{%Order.OrderNote|(encode)|(user)administrator|(hash)a5ed7cf37728b6c1ad6cf0cf4c5d39df4ed3d35a95731a7bc7841d04318951bf%}', N'', N'', N'', N'', N'ecommerce')
INSERT INTO [CMS_EmailTemplate] ([EmailTemplateID], [EmailTemplateName], [EmailTemplateDisplayName], [EmailTemplateText], [EmailTemplateSiteID], [EmailTemplateGUID], [EmailTemplateLastModified], [EmailTemplatePlainText], [EmailTemplateSubject], [EmailTemplateFrom], [EmailTemplateCc], [EmailTemplateBcc], [EmailTemplateType]) VALUES (166, N'Ecommerce.OrderNotificationToCustomer', N'E-commerce - Order notification to customer', N'<html><head></head><body>
<p>Thank you for your order. Below you can find the order details.</p>
<table width="600" cellspacing="0" cellpadding="5" bordercolor="black" border="1">
    <tbody>
        <tr>
            <td height="50" valign="bottom" colspan="2">
            <table height="100%" width="100%">
                <tbody>
                    <tr>
                        <td style="text-align: left; vertical-align: bottom;"><span style="font-size: 18pt;">Your order</span></td>
                        <td style="text-align: center; vertical-align: middle;"><span style="font-family: Garamond,Times,serif; font-size: 24pt; font-style: italic;">Company logo</span></td>
                    </tr>
                </tbody>
            </table>
            </td>
        </tr>
        <tr>
            <td style="text-align: left;"><br />
            <table width="100%">
                <tbody>
                    <tr>
                        <td valign="bottom" style="text-align: left;"><strong>Invoice number:</strong></td>
                        <td style="text-align: right; padding-right: 10px;">{%Order.OrderInvoiceNumber|(user)administrator|(hash)2526fa4a5e2bc2b4a58e8fab08d08b139025706f1f614719df397bc9bd3b90a5%}</td>
                    </tr>
                </tbody>
            </table>
            </td>
            <td style="text-align: left;"><br />
            <table width="100%">
                <tbody>
                    <tr>
                        <td valign="bottom" style="text-align: left;"><strong>Order date:</strong></td>
                        <td style="text-align: right; padding-right: 10px;">{%Format(Order.OrderDate, "{0:d}")|(user)administrator|(hash)1cccd184308256a5f6b82024b0aa03bb4e9c9d4eaa029664e3714197a15300b6%}</td>
                    </tr>
                    <tr>
                        <td valign="bottom" style="text-align: left;"><strong>Order status:</strong></td>
                        <td style="text-align: right; padding-right: 10px;">{%GetResourceString(OrderStatus.StatusDisplayName)|(user)administrator|(hash)09902315904d1db9014aea7bf4d5d4d5680f68f7aed449931a8570c2d9dc27e6%}</td>
                    </tr>
                </tbody>
            </table>
            </td>
        </tr>
        <tr>
            <td width="50%" style="text-align: left; vertical-align: top;"><strong>Supplier</strong><br />
            <br />
            <table>
                <tbody>
                    <tr>
                        <td>Company address</td>
                    </tr>
                </tbody>
            </table>
            </td>
            <td width="50%" style="text-align: left; vertical-align: top;"><span style="font-weight: bold;">Customer</span><br />
            <br />
            {%BillingAddress.ApplyTransformation("Ecommerce.Transformations.Order_Address"|(user)administrator|(hash)7960adbbbb40670d0bf29659fe42435cc7ec2db333a9b4f6d7d4d8133fe2c2e1%}
            <br />
            <strong>Company address:</strong>
            {%CompanyAddress.ApplyTransformation("Ecommerce.Transformations.Order_Address"|(user)administrator|(hash)98f29f2a9bdf80406f46478366c2c8276adaa3775727fa4a66819b5665e2abb9%}
            </td>
        </tr>
        <tr>
            <td colspan="2">
            <table width="100%">
                <tbody>
                    <tr>
                        <td style="text-align: left;"><span style="font-weight: bold;">Payment option</span></td>
                    </tr>
                    <tr>
                        <td style="text-align: center;">{%GetResourceString(PaymentOption.PaymentOptionDisplayName)|(user)administrator|(hash)f4bd53a4cf633921fb1f15e9097ff5312b27b1eb54cf01fb5669c38dbdec1cb1%}</td>
                    </tr>
                </tbody>
            </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
            <table width="100%">
                <tbody>
                    <tr>
                        <td style="text-align: left;"><span style="font-weight: bold;">Shipping option</span></td>
                    </tr>
                    <tr>
                        <td style="text-align: center;">{%GetResourceString(ShippingOption.ShippingOptionDisplayName)|(user)administrator|(hash)748fabbfafb7f9dfdf45675a28eba4bdc4c072592e58a0c358051d0a3153bf8e%}</td>
                    </tr>
                </tbody>
            </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: left;">{%ContentTable.ApplyTransformation("Ecommerce.Transformations.Order_ContentTable", "Ecommerce.Transformations.Order_ContentTableHeader", "Ecommerce.Transformations.Order_ContentTableFooter")|(user)administrator|(hash)9e8c2d4859c4e8c80639a9cffb6cc25fc935fb55e7b03c135c935bf947d8bcf0%}<hr size="1" />
            <div style="text-align: right;">
            <table cellpadding="5" style="text-align: left;">
                <tbody>
                    <tr>
                        <td><strong>Total shipping:</strong></td>
                        <td style="text-align: right; padding-right: 0px;"><strong>{%TotalShipping.Format(Currency.CurrencyFormatString)|(user)administrator|(hash)ab9d10aa152f2e3a3c22e79c05607ad18691707fce7787aba2d512335482886e%}</strong></td>
                    </tr>
                    <tr>
                        <td><strong>Total price:</strong></td>
                        <td style="text-align: right; padding-right: 0px;"><strong>{%TotalPrice.Format(Currency.CurrencyFormatString)|(user)administrator|(hash)49695c0d3fa3613ef4a7958194fc8cfd077dee4d8198799beaf432a0dbf8c7f0%}</strong></td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top;"><strong>Tax summary:</strong></td>
                        <td style="text-align: right; padding-right: 0px;">{%ContentTaxesTable.ApplyTransformation("Ecommerce.Transformations.Order_TaxesTable", "Ecommerce.Transformations.Order_TaxesTableHeader", "Ecommerce.Transformations.Order_TaxesTableFooter")|(user)administrator|(hash)76921da8b0bdaf955b33c85e57dab8feaccc01f0ae2cedc9def953d4b39660c1%}</td>
                    </tr>
                </tbody>
            </table>
            </div>
            <div style="height: 120px;">&nbsp;</div>
            </td>
        </tr>
        <tr>
            <td colspan="2">
            <table width="100%">
                <tbody>
                    <tr>
                        <td style="text-align: left;"><span style="font-weight: bold;">Order note</span></td>
                    </tr>
                    <tr>
                        <td style="text-align: left;">{%Order.OrderNote|(encode)|(user)administrator|(hash)a5ed7cf37728b6c1ad6cf0cf4c5d39df4ed3d35a95731a7bc7841d04318951bf%}</td>
                    </tr>
                </tbody>
            </table>
            </td>
        </tr>
    </tbody>
</table>
</body>
</html>', NULL, '674d1b85-ce19-40bd-b2df-166a5891090a', '20110930 08:14:08', N'Thank you for your order!
Your order Company logo
 
Invoice number: {%Order.OrderInvoiceNumber|(user)administrator|(hash)2526fa4a5e2bc2b4a58e8fab08d08b139025706f1f614719df397bc9bd3b90a5%}
 
Order date: {%Format(Order.OrderDate, "{0:d}")|(user)administrator|(hash)1cccd184308256a5f6b82024b0aa03bb4e9c9d4eaa029664e3714197a15300b6%}
Order status: {%GetResourceString(OrderStatus.StatusDisplayName)|(user)administrator|(hash)09902315904d1db9014aea7bf4d5d4d5680f68f7aed449931a8570c2d9dc27e6%}
 
Supplier
Company address
 Customer
{%BillingAddress.ApplyTransformation("Ecommerce.Transformations.Order_Address"|(user)administrator|(hash)7960adbbbb40670d0bf29659fe42435cc7ec2db333a9b4f6d7d4d8133fe2c2e1%}
Company address: {%CompanyAddress.ApplyTransformation("Ecommerce.Transformations.Order_Address"|(user)administrator|(hash)98f29f2a9bdf80406f46478366c2c8276adaa3775727fa4a66819b5665e2abb9%}
Payment option
{%GetResourceString(PaymentOption.PaymentOptionDisplayName)|(user)administrator|(hash)f4bd53a4cf633921fb1f15e9097ff5312b27b1eb54cf01fb5669c38dbdec1cb1%}
Shipping option  
{%GetResourceString(ShippingOption.ShippingOptionDisplayName)|(user)administrator|(hash)748fabbfafb7f9dfdf45675a28eba4bdc4c072592e58a0c358051d0a3153bf8e%}
 
{%ContentTable.ApplyTransformation("Ecommerce.Transformations.Order_ContentTable", "Ecommerce.Transformations.Order_ContentTableHeader", "Ecommerce.Transformations.Order_ContentTableFooter")|(user)administrator|(hash)9e8c2d4859c4e8c80639a9cffb6cc25fc935fb55e7b03c135c935bf947d8bcf0%}
--------------------------------------------------------------------------------
Total shipping: {%TotalShipping.Format(Currency.CurrencyFormatString)|(user)administrator|(hash)ab9d10aa152f2e3a3c22e79c05607ad18691707fce7787aba2d512335482886e%}
Total price: {%TotalPrice.Format(Currency.CurrencyFormatString)|(user)administrator|(hash)49695c0d3fa3613ef4a7958194fc8cfd077dee4d8198799beaf432a0dbf8c7f0%}
Tax summary: {%ContentTaxesTable.ApplyTransformation("Ecommerce.Transformations.Order_TaxesTable", "Ecommerce.Transformations.Order_TaxesTableHeader", "Ecommerce.Transformations.Order_TaxesTableFooter")|(user)administrator|(hash)76921da8b0bdaf955b33c85e57dab8feaccc01f0ae2cedc9def953d4b39660c1%}
  
Order note  
{%Order.OrderNote|(encode)|(user)administrator|(hash)a5ed7cf37728b6c1ad6cf0cf4c5d39df4ed3d35a95731a7bc7841d04318951bf%}', N'', N'', N'', N'', N'ecommerce')
INSERT INTO [CMS_EmailTemplate] ([EmailTemplateID], [EmailTemplateName], [EmailTemplateDisplayName], [EmailTemplateText], [EmailTemplateSiteID], [EmailTemplateGUID], [EmailTemplateLastModified], [EmailTemplatePlainText], [EmailTemplateSubject], [EmailTemplateFrom], [EmailTemplateCc], [EmailTemplateBcc], [EmailTemplateType]) VALUES (706, N'Ecommerce.OrderPaymentNotificationToAdmin', N'E-commerce - Order payment notification to administrator', N'<html>
<head>
</head>
<body>
<p>Payment for the order below received.</p>
<table cellspacing="0" cellpadding="5" border="1" bordercolor="black" width="600px">
  <tr>
    <td colspan="2" valign="bottom" height="50">
      <table width="100%" height="100%">
        <tr>
          <td style="text-align: left; vertical-align: bottom;"
            <span style="font-size: 18pt">New order</span>
          </td>
          <td style="text-align: center; vertical-align: middle;">
            <span style="font-family: Garamond, Times, serif; font-size: 24pt; font-style: italic;">Company logo</span>
          </td>
        </tr>
      </table> 
    </td>
  </tr>
  <tr>
    <td style="text-align: left">    
      <br />
      <table width="100%">
        <tr>
          <td style="text-align: left;" valign="bottom"> 
            <strong>Invoice number:</strong>
          </td>
          <td style="text-align: right; padding-right: 10px">
            {%Order.OrderInvoiceNumber|(user)administrator|(hash)2526fa4a5e2bc2b4a58e8fab08d08b139025706f1f614719df397bc9bd3b90a5%}
          </td>
        </tr>
      </table> 
      <br />
    </td>
    <td style="text-align: left">    
      <br />
      <table width="100%">
        <tr>
          <td style="text-align: left;" valign="bottom"> 
            <strong>Order date:</strong>
          </td>
          <td style="text-align: right; padding-right: 10px">
            {%Format(Order.OrderDate, "{0:d}")|(user)administrator|(hash)1cccd184308256a5f6b82024b0aa03bb4e9c9d4eaa029664e3714197a15300b6%}
          </td>
        </tr>
  <tr>
          <td style="text-align: left;" valign="bottom"> 
            <strong>Order status:</strong>
          </td>
          <td style="text-align: right; padding-right: 10px">
            {%GetResourceString(OrderStatus.StatusDisplayName)|(user)administrator|(hash)09902315904d1db9014aea7bf4d5d4d5680f68f7aed449931a8570c2d9dc27e6%}
          </td>
        </tr>
      </table>  
      <br />
    </td>
  </tr>
  <tr>
    <td style="text-align: left; vertical-align: top" width="50%">
      <strong>Supplier</strong>
      <br/>
      <br/>
      <table>
        <tr>
          <td>
            Company address
          </td>
        </tr>
      </table>
      <br />
    </td>
    <td style="text-align: left; vertical-align: top" width="50%">
      <span style="font-weight: bold"> Customer </span><br />
      <br />
        {%BillingAddress.ApplyTransformation("Ecommerce.Transformations.Order_Address"|(user)administrator|(hash)7960adbbbb40670d0bf29659fe42435cc7ec2db333a9b4f6d7d4d8133fe2c2e1%}
      <br />
    </td>
  </tr>
  <tr>
    <td colspan="2">
      <table width="100%">
        <tr>
          <td style="text-align: left">
            <span style="font-weight: bold"> Payment option </span>
          </td>
        </tr>
        <tr>
          <td style="text-align: center">
            {%GetResourceString(PaymentOption.PaymentOptionDisplayName)|(user)administrator|(hash)f4bd53a4cf633921fb1f15e9097ff5312b27b1eb54cf01fb5669c38dbdec1cb1%}
          </td>
        </tr>
      </table> 
    </td>
  </tr>
  <tr>
    <td colspan="2">
      <table width="100%">
        <tr>
          <td style="text-align: left">
            <span style="font-weight: bold"> Shipping option </span>
          </td>
        </tr>
        <tr>
          <td style="text-align: center">
            {%GetResourceString(ShippingOption.ShippingOptionDisplayName)|(user)administrator|(hash)748fabbfafb7f9dfdf45675a28eba4bdc4c072592e58a0c358051d0a3153bf8e%}
          </td>
        </tr>
      </table>
    </td>
  </tr>
  <tr>
    <td style="text-align: left" colspan="2">
      {%ContentTable.ApplyTransformation("Ecommerce.Transformations.Order_ContentTable", "Ecommerce.Transformations.Order_ContentTableHeader", "Ecommerce.Transformations.Order_ContentTableFooter")|(user)administrator|(hash)9e8c2d4859c4e8c80639a9cffb6cc25fc935fb55e7b03c135c935bf947d8bcf0%}
      <hr size="1" />
      <div style="text-align: right;">
      <table style="text-align: left;" cellpadding="5">
  <tr>
    <td><strong>Total shipping:</strong></td>
          <td style="text-align: right; padding-right: 0px;">
            <strong>{%TotalShipping.Format(Currency.CurrencyFormatString)|(user)administrator|(hash)ab9d10aa152f2e3a3c22e79c05607ad18691707fce7787aba2d512335482886e%}</strong>
          </td>
  </tr>
  <tr>
    <td style="vertical-align:top;"><strong>Shipping tax summary:</strong></td>
          <td style="text-align: right; padding-right: 0px;">
            {%ShippingTaxesTable.ApplyTransformation("Ecommerce.Transformations.Order_TaxesTable", "Ecommerce.Transformations.Order_TaxesTableHeader", "Ecommerce.Transformations.Order_TaxesTableFooter")|(user)administrator|(hash)e44e8ed374402353c054e1d0a129da7dd1cd5abf209cec406830894cd852bc7b%}
          </td>
  </tr>  
  <tr>
    <td><strong>Total price:</strong></td>
          <td style="text-align: right; padding-right: 0px;">
            <strong>{%TotalPrice.Format(Currency.CurrencyFormatString)|(user)administrator|(hash)49695c0d3fa3613ef4a7958194fc8cfd077dee4d8198799beaf432a0dbf8c7f0%}</strong>
          </td>
  </tr>
  <tr>
    <td style="vertical-align:top;"><strong>Tax summary:</strong></td>
          <td style="text-align: right; padding-right: 0px;">
            {%ContentTaxesTable.ApplyTransformation("Ecommerce.Transformations.Order_TaxesTable", "Ecommerce.Transformations.Order_TaxesTableHeader", "Ecommerce.Transformations.Order_TaxesTableFooter")|(user)administrator|(hash)76921da8b0bdaf955b33c85e57dab8feaccc01f0ae2cedc9def953d4b39660c1%}
          </td>
  </tr>
      </table>
      </div>
      <div style="height: 120px;">&nbsp;</div>
    </td>
  </tr>
  <tr>
    <td colspan="2">
      <table width="100%">
        <tr>
          <td style="text-align: left">
            <span style="font-weight: bold"> Order note </span>
          </td>
        </tr>
        <tr>
          <td style="text-align: left">
            {%Order.OrderNote|(encode)|(user)administrator|(hash)a5ed7cf37728b6c1ad6cf0cf4c5d39df4ed3d35a95731a7bc7841d04318951bf%}
          </td>
        </tr>
      </table>
    </td>
  </tr>
</table>
<div style="padding-top-10px;">
   {%NewOrderLink%}
</div>
</body>
</html>', NULL, '7b821e4a-d695-42a7-85f2-1ca14c208953', '20110930 08:10:59', N'Payment for the order below received.
New order  Company logo  
 
Invoice number:  {%Order.OrderInvoiceNumber|(user)administrator|(hash)2526fa4a5e2bc2b4a58e8fab08d08b139025706f1f614719df397bc9bd3b90a5%} 
 
Order date:  {%Format(Order.OrderDate, "{0:d}")|(user)administrator|(hash)1cccd184308256a5f6b82024b0aa03bb4e9c9d4eaa029664e3714197a15300b6%}  
Order status:  {%GetResourceString(OrderStatus.StatusDisplayName)|(user)administrator|(hash)09902315904d1db9014aea7bf4d5d4d5680f68f7aed449931a8570c2d9dc27e6%}  
 
Supplier 
Company address  
Customer 
{%BillingAddress.ApplyTransformation("Ecommerce.Transformations.Order_Address"|(user)administrator|(hash)7960adbbbb40670d0bf29659fe42435cc7ec2db333a9b4f6d7d4d8133fe2c2e1%} 
 
Payment option  
{%GetResourceString(PaymentOption.PaymentOptionDisplayName)|(user)administrator|(hash)f4bd53a4cf633921fb1f15e9097ff5312b27b1eb54cf01fb5669c38dbdec1cb1%}  
 
Shipping option  
{%GetResourceString(ShippingOption.ShippingOptionDisplayName)|(user)administrator|(hash)748fabbfafb7f9dfdf45675a28eba4bdc4c072592e58a0c358051d0a3153bf8e%}
 
{%ContentTable.ApplyTransformation("Ecommerce.Transformations.Order_ContentTable", "Ecommerce.Transformations.Order_ContentTableHeader", "Ecommerce.Transformations.Order_ContentTableFooter")|(user)administrator|(hash)9e8c2d4859c4e8c80639a9cffb6cc25fc935fb55e7b03c135c935bf947d8bcf0%}
--------------------------------------------------------------------------------
Total shipping: {%TotalShipping.Format(Currency.CurrencyFormatString)|(user)administrator|(hash)ab9d10aa152f2e3a3c22e79c05607ad18691707fce7787aba2d512335482886e%}
Shipping tax summary:
{%ShippingTaxesTable.ApplyTransformation("Ecommerce.Transformations.Order_TaxesTable", "Ecommerce.Transformations.Order_TaxesTableHeader", "Ecommerce.Transformations.Order_TaxesTableFooter")|(user)administrator|(hash)e44e8ed374402353c054e1d0a129da7dd1cd5abf209cec406830894cd852bc7b%}
Total price: {%TotalPrice.Format(Currency.CurrencyFormatString)|(user)administrator|(hash)49695c0d3fa3613ef4a7958194fc8cfd077dee4d8198799beaf432a0dbf8c7f0%}
Tax summary: 
{%ContentTaxesTable.ApplyTransformation("Ecommerce.Transformations.Order_TaxesTable", "Ecommerce.Transformations.Order_TaxesTableHeader", "Ecommerce.Transformations.Order_TaxesTableFooter")|(user)administrator|(hash)76921da8b0bdaf955b33c85e57dab8feaccc01f0ae2cedc9def953d4b39660c1%}
  
Order note  
{%Order.OrderNote|(encode)|(user)administrator|(hash)a5ed7cf37728b6c1ad6cf0cf4c5d39df4ed3d35a95731a7bc7841d04318951bf%}', N'', N'', N'', N'', N'ecommerce')
INSERT INTO [CMS_EmailTemplate] ([EmailTemplateID], [EmailTemplateName], [EmailTemplateDisplayName], [EmailTemplateText], [EmailTemplateSiteID], [EmailTemplateGUID], [EmailTemplateLastModified], [EmailTemplatePlainText], [EmailTemplateSubject], [EmailTemplateFrom], [EmailTemplateCc], [EmailTemplateBcc], [EmailTemplateType]) VALUES (707, N'Ecommerce.OrderPaymentNotificationToCustomer', N'E-commerce - Order payment notification to customer', N'<html>
<head>
</head>
<body>
<p>We have received your payment for the order bellow:</p>
<table cellspacing="0" cellpadding="5" border="1" bordercolor="black" width="600px">
  <tr>
    <td colspan="2" valign="bottom" height="50">
      <table width="100%" height="100%">
        <tr>
          <td style="text-align: left; vertical-align: bottom;"
            <span style="font-size: 18pt">Your order</span>
          </td>
          <td style="text-align: center; vertical-align: middle;">
            <span style="font-family: Garamond, Times, serif; font-size: 24pt; font-style: italic;">Company logo</span>
          </td>
        </tr>
      </table> 
    </td>
  </tr>
  <tr>
    <td style="text-align: left">    
      <br />
      <table width="100%">
        <tr>
          <td style="text-align: left;" valign="bottom"> 
            <strong>Invoice number:</strong>
          </td>
          <td style="text-align: right; padding-right: 10px">
            {%Order.OrderInvoiceNumber|(user)administrator|(hash)2526fa4a5e2bc2b4a58e8fab08d08b139025706f1f614719df397bc9bd3b90a5%}
          </td>
        </tr>
      </table> 
      <br />
    </td>
    <td style="text-align: left">    
      <br />
      <table width="100%">
        <tr>
          <td style="text-align: left;" valign="bottom"> 
            <strong>Order date:</strong>
          </td>
          <td style="text-align: right; padding-right: 10px">
            {%Format(Order.OrderDate, "{0:d}"|(user)administrator|(hash)6f79510dc59f2df24b4c5b9d07b36cf7482b447fc933541ec0bbafeb745609f1%}
          </td>
        </tr>
  <tr>
          <td style="text-align: left;" valign="bottom"> 
            <strong>Order status:</strong>
          </td>
          <td style="text-align: right; padding-right: 10px">
            {%GetResourceString(OrderStatus.StatusDisplayName)|(user)administrator|(hash)09902315904d1db9014aea7bf4d5d4d5680f68f7aed449931a8570c2d9dc27e6%} 
          </td>
        </tr>
      </table>  
      <br />
    </td>
  </tr>
  <tr>
    <td style="text-align: left; vertical-align: top" width="50%">
      <strong>Supplier</strong>
      <br/>
      <br/>
      <table>
        <tr>
          <td>
            Company address
          </td>
        </tr>
      </table>
      <br />
    </td>
    <td style="text-align: left; vertical-align: top" width="50%">
      <span style="font-weight: bold"> Customer </span><br />
      <br />
        {%BillingAddress.ApplyTransformation("Ecommerce.Transformations.Order_Address"|(user)administrator|(hash)7960adbbbb40670d0bf29659fe42435cc7ec2db333a9b4f6d7d4d8133fe2c2e1%}
      <br />
    </td>
  </tr>
  <tr>
    <td colspan="2">
      <table width="100%">
          <tbody>
              <tr>
                  <td style="text-align: left;"><span style="font-weight: bold;">Payment option</span></td>
              </tr>
              <tr>
                  <td style="text-align: center;">{%GetResourceString(PaymentOption.PaymentOptionDisplayName)|(user)administrator|(hash)f4bd53a4cf633921fb1f15e9097ff5312b27b1eb54cf01fb5669c38dbdec1cb1%}</td>
              </tr>
          </tbody>
      </table>
    </td>    
  </tr>
  <tr>
    <td colspan="2">
      <table width="100%">
          <tbody>
              <tr>
                  <td style="text-align: left;"><span style="font-weight: bold;">Shipping option</span></td>
              </tr>
              <tr>
                  <td style="text-align: center;">{%GetResourceString(ShippingOption.ShippingOptionDisplayName)|(user)administrator|(hash)748fabbfafb7f9dfdf45675a28eba4bdc4c072592e58a0c358051d0a3153bf8e%}</td>
              </tr>
          </tbody>
      </table>
    </td>  
  </tr>
  <tr>
    <td style="text-align: left" colspan="2">
      {%ContentTable.ApplyTransformation("Ecommerce.Transformations.Order_ContentTable", "Ecommerce.Transformations.Order_ContentTableHeader", "Ecommerce.Transformations.Order_ContentTableFooter")|(user)administrator|(hash)9e8c2d4859c4e8c80639a9cffb6cc25fc935fb55e7b03c135c935bf947d8bcf0%}
      <hr size="1" />
      <div style="text-align: right;">
      <table style="text-align: left;" cellpadding="5">
  <tr>
    <td><strong>Total shipping:</strong></td>
          <td style="text-align: right; padding-right: 0px;">
            <strong>{%TotalShipping.Format(Currency.CurrencyFormatString)|(user)administrator|(hash)ab9d10aa152f2e3a3c22e79c05607ad18691707fce7787aba2d512335482886e%}</strong>
          </td>
  </tr>
  <tr>
    <td style="vertical-align:top;"><strong>Shipping tax summary:</strong></td>
          <td style="text-align: right; padding-right: 0px;">
            {%ShippingTaxesTable.ApplyTransformation("Ecommerce.Transformations.Order_TaxesTable", "Ecommerce.Transformations.Order_TaxesTableHeader", "Ecommerce.Transformations.Order_TaxesTableFooter")|(user)administrator|(hash)e44e8ed374402353c054e1d0a129da7dd1cd5abf209cec406830894cd852bc7b%}
          </td>
  </tr>  
  <tr>
    <td><strong>Total price:</strong></td>
          <td style="text-align: right; padding-right: 0px;">
            <strong>{%TotalPrice.Format(Currency.CurrencyFormatString)|(user)administrator|(hash)49695c0d3fa3613ef4a7958194fc8cfd077dee4d8198799beaf432a0dbf8c7f0%}</strong>
          </td>
  </tr>
  <tr>
    <td style="vertical-align:top;"><strong>Tax summary:</strong></td>
          <td style="text-align: right; padding-right: 0px;">
            {%ContentTaxesTable.ApplyTransformation("Ecommerce.Transformations.Order_TaxesTable", "Ecommerce.Transformations.Order_TaxesTableHeader", "Ecommerce.Transformations.Order_TaxesTableFooter")|(user)administrator|(hash)76921da8b0bdaf955b33c85e57dab8feaccc01f0ae2cedc9def953d4b39660c1%}
          </td>
  </tr>
      </table>
      </div>
      <div style="height: 120px;">&nbsp;</div>
    </td>
  </tr>
  <tr>
    <td colspan="2">
      <table width="100%">
        <tr>
          <td style="text-align: left">
            <span style="font-weight: bold"> Order note </span>
          </td>
        </tr>
        <tr>
          <td style="text-align: left">
            {%Order.OrderNote|(encode)|(user)administrator|(hash)a5ed7cf37728b6c1ad6cf0cf4c5d39df4ed3d35a95731a7bc7841d04318951bf%}
          </td>
        </tr>
      </table>
    </td>
  </tr>
</table>
{%IfDataSourceIsEmpty(EproductsTable, "",
"<p>Your <b>e-products download links were activated</b>, please download the files before their expiration. Once the files expire, you won''t be able to download them.</p>" +
EproductsTable.ApplyTransformation("Ecommerce.Transformations.Order_EproductsTable")|(user)administrator|(hash)13109af79801052a66b6fb87d6102dfbd7df3d1522d522aa5e00c2ab3a22d238%}
</body>
</html>', NULL, '5da46ce2-23bf-4c2e-9dbb-22d67c550399', '20110930 10:02:32', N'We have received your payment for the order below.
Your order  Company logo  
 
Invoice number: {%Order.OrderInvoiceNumber|(user)administrator|(hash)2526fa4a5e2bc2b4a58e8fab08d08b139025706f1f614719df397bc9bd3b90a5%} 
 
Order date: {%Format(Order.OrderDate, "{0:d}")|(user)administrator|(hash)1cccd184308256a5f6b82024b0aa03bb4e9c9d4eaa029664e3714197a15300b6%} 
Order status: {%GetResourceString(OrderStatus.StatusDisplayName)|(user)administrator|(hash)09902315904d1db9014aea7bf4d5d4d5680f68f7aed449931a8570c2d9dc27e6%}
 
Supplier 
Company address  
Customer 
{%BillingAddress.ApplyTransformation("Ecommerce.Transformations.Order_Address"|(user)administrator|(hash)7960adbbbb40670d0bf29659fe42435cc7ec2db333a9b4f6d7d4d8133fe2c2e1%} 
 
Payment option  
{%GetResourceString(PaymentOption.PaymentOptionDisplayName)|(user)administrator|(hash)f4bd53a4cf633921fb1f15e9097ff5312b27b1eb54cf01fb5669c38dbdec1cb1%}
 
Shipping option  
{%GetResourceString(ShippingOption.ShippingOptionDisplayName)|(user)administrator|(hash)748fabbfafb7f9dfdf45675a28eba4bdc4c072592e58a0c358051d0a3153bf8e%}
 
{%ContentTable.ApplyTransformation("Ecommerce.Transformations.Order_ContentTable", "Ecommerce.Transformations.Order_ContentTableHeader", "Ecommerce.Transformations.Order_ContentTableFooter")|(user)administrator|(hash)9e8c2d4859c4e8c80639a9cffb6cc25fc935fb55e7b03c135c935bf947d8bcf0%}
--------------------------------------------------------------------------------
Total shipping: {%TotalShipping.Format(Currency.CurrencyFormatString)|(user)administrator|(hash)ab9d10aa152f2e3a3c22e79c05607ad18691707fce7787aba2d512335482886e%} 
Shipping tax summary: {%ShippingTaxesTable.ApplyTransformation("Ecommerce.Transformations.Order_TaxesTable", "Ecommerce.Transformations.Order_TaxesTableHeader", "Ecommerce.Transformations.Order_TaxesTableFooter")|(user)administrator|(hash)e44e8ed374402353c054e1d0a129da7dd1cd5abf209cec406830894cd852bc7b%}
Total price: {%TotalPrice.Format(Currency.CurrencyFormatString)|(user)administrator|(hash)49695c0d3fa3613ef4a7958194fc8cfd077dee4d8198799beaf432a0dbf8c7f0%}  
Tax summary: {%ContentTaxesTable.ApplyTransformation("Ecommerce.Transformations.Order_TaxesTable", "Ecommerce.Transformations.Order_TaxesTableHeader", "Ecommerce.Transformations.Order_TaxesTableFooter")|(user)administrator|(hash)76921da8b0bdaf955b33c85e57dab8feaccc01f0ae2cedc9def953d4b39660c1%}  
  
Order note  
{%Order.OrderNote|(encode)|(user)administrator|(hash)a5ed7cf37728b6c1ad6cf0cf4c5d39df4ed3d35a95731a7bc7841d04318951bf%}
{%EproductsTable.ApplyTransformation("Ecommerce.Transformations.Order_EproductsTable",
"Your e-products download links were activated, please download the files before their expiration. Once the files expire, you won''t be able to download them.")|(user)administrator|(hash)a24d6ee33533038e21bd562a0cfbb8148332016389fd696277d7ee39cd1c9f8e%}', N'', N'', N'', N'', N'ecommerce')
INSERT INTO [CMS_EmailTemplate] ([EmailTemplateID], [EmailTemplateName], [EmailTemplateDisplayName], [EmailTemplateText], [EmailTemplateSiteID], [EmailTemplateGUID], [EmailTemplateLastModified], [EmailTemplatePlainText], [EmailTemplateSubject], [EmailTemplateFrom], [EmailTemplateCc], [EmailTemplateBcc], [EmailTemplateType]) VALUES (731, N'Ecommerce.OrderStatusNotificationToAdmin', N'E-commerce - Order status notification to administrator', N'<html>
<head>
</head>
<body>
<p>Status of the order bellow has changed</p>
<table cellspacing="0" cellpadding="5" border="1" bordercolor="black" width="600px">
  <tr>
    <td colspan="2" valign="bottom" height="50">
      <table width="100%" height="100%">
        <tr>
          <td style="text-align: left; vertical-align: bottom;"
            <span style="font-size: 18pt">New order</span>
          </td>
          <td style="text-align: center; vertical-align: middle;">
            <span style="font-family: Garamond, Times, serif; font-size: 24pt; font-style: italic;">Company logo</span>
          </td>
        </tr>
      </table> 
    </td>
  </tr>
  <tr>
    <td style="text-align: left">    
      <br />
      <table width="100%">
        <tr>
          <td style="text-align: left;" valign="bottom"> 
            <strong>Invoice number:</strong>
          </td>
          <td style="text-align: right; padding-right: 10px">
            {%Order.OrderInvoiceNumber|(user)administrator|(hash)2526fa4a5e2bc2b4a58e8fab08d08b139025706f1f614719df397bc9bd3b90a5%}
          </td>
        </tr>
      </table> 
      <br />
    </td>
    <td style="text-align: left">    
      <br />
      <table width="100%">
        <tr>
          <td style="text-align: left;" valign="bottom"> 
            <strong>Order date:</strong>
          </td>
          <td style="text-align: right; padding-right: 10px">
            {%Format(Order.OrderDate, "{0:d}")|(user)administrator|(hash)1cccd184308256a5f6b82024b0aa03bb4e9c9d4eaa029664e3714197a15300b6%}
          </td>
        </tr>
  <tr>
          <td style="text-align: left;" valign="bottom"> 
            <strong>Order status:</strong>
          </td>
          <td style="text-align: right; padding-right: 10px">
            {%OrderStatus.StatusDisplayName|(user)administrator|(hash)d6c68de905b2b672c83145fd1c3b42fda0e83621645a6db50d3a8cafb613ac2e%} 
          </td>
        </tr>
      </table>  
      <br />
    </td>
  </tr>
  <tr>
    <td style="text-align: left; vertical-align: top" width="50%">
      <strong>Supplier</strong>
      <br/>
      <br/>
      <table>
        <tr>
          <td>
            Company address
          </td>
        </tr>
      </table>
      <br />
    </td>
    <td style="text-align: left; vertical-align: top" width="50%">
      <span style="font-weight: bold"> Customer </span><br />
      <br />
        {%BillingAddress.ApplyTransformation("Ecommerce.Transformations.Order_Address"|(user)administrator|(hash)7960adbbbb40670d0bf29659fe42435cc7ec2db333a9b4f6d7d4d8133fe2c2e1%}
      <br />
  <strong>Company address:</strong>
  {%CompanyAddress.ApplyTransformation("Ecommerce.Transformations.Order_Address"|(user)administrator|(hash)98f29f2a9bdf80406f46478366c2c8276adaa3775727fa4a66819b5665e2abb9%}
      <br />
    </td>
  </tr>
  <tr>
    <td colspan="2">
      <table width="100%">
        <tr>
          <td style="text-align: left">
            <span style="font-weight: bold"> Payment option </span>
          </td>
        </tr>
        <tr>
          <td style="text-align: center">
            {%GetResourceString(PaymentOption.PaymentOptionDisplayName)|(user)administrator|(hash)f4bd53a4cf633921fb1f15e9097ff5312b27b1eb54cf01fb5669c38dbdec1cb1%}
          </td>
        </tr>
      </table> 
    </td>
  </tr>
  <tr>
    <td colspan="2">
      <table width="100%">
        <tr>
          <td style="text-align: left">
            <span style="font-weight: bold"> Shipping option </span>
          </td>
        </tr>
        <tr>
          <td style="text-align: center">
            {%GetResourceString(ShippingOption.ShippingOptionDisplayName)|(user)administrator|(hash)748fabbfafb7f9dfdf45675a28eba4bdc4c072592e58a0c358051d0a3153bf8e%}
          </td>
        </tr>
      </table>
    </td>
  </tr>
  <tr>
    <td style="text-align: left" colspan="2">
      {%ContentTable.ApplyTransformation("Ecommerce.Transformations.Order_ContentTable", "Ecommerce.Transformations.Order_ContentTableHeader", "Ecommerce.Transformations.Order_ContentTableFooter")|(user)administrator|(hash)9e8c2d4859c4e8c80639a9cffb6cc25fc935fb55e7b03c135c935bf947d8bcf0%} 
      <hr size="1" />
      <div style="text-align: right;">
      <table style="text-align: left;" cellpadding="5">
  <tr>
    <td><strong>Total shipping:</strong></td>
          <td style="text-align: right; padding-right: 0px;">
            <strong>{%TotalShipping.Format(Currency.CurrencyFormatString)|(user)administrator|(hash)ab9d10aa152f2e3a3c22e79c05607ad18691707fce7787aba2d512335482886e%}</strong>
          </td>
  </tr>
  <tr>
    <td><strong>Total price:</strong></td>
          <td style="text-align: right; padding-right: 0px;">
            <strong>{%TotalPrice.Format(Currency.CurrencyFormatString)|(user)administrator|(hash)49695c0d3fa3613ef4a7958194fc8cfd077dee4d8198799beaf432a0dbf8c7f0%}</strong>
          </td>
  </tr>
  <tr>
    <td style="vertical-align:top;"><strong>Tax summary:</strong></td>
          <td style="text-align: right; padding-right: 0px;">
            {%ContentTaxesTable.ApplyTransformation("Ecommerce.Transformations.Order_TaxesTable", "Ecommerce.Transformations.Order_TaxesTableHeader", "Ecommerce.Transformations.Order_TaxesTableFooter")|(user)administrator|(hash)76921da8b0bdaf955b33c85e57dab8feaccc01f0ae2cedc9def953d4b39660c1%}
          </td>
  </tr>
      </table>
      </div>
      <div style="height: 120px;">&nbsp;</div>
    </td>
  </tr>
  <tr>
    <td colspan="2">
      <table width="100%">
        <tr>
          <td style="text-align: left">
            <span style="font-weight: bold"> Order note </span>
          </td>
        </tr>
        <tr>
          <td style="text-align: left">
            {%Order.OrderNote|(encode)|(user)administrator|(hash)a5ed7cf37728b6c1ad6cf0cf4c5d39df4ed3d35a95731a7bc7841d04318951bf%}
          </td>
        </tr>
      </table>
    </td>
  </tr>
</table>
<div style="padding-top-10px;">
   {%NewOrderLink%}
</div>
</body>
</html>', NULL, 'a32836fb-5d7e-4e71-9dcf-33bc920859b6', '20110930 08:15:19', N'Status of the order bellow has changed
New order  Company logo  
 
Invoice number:  {%Order.OrderInvoiceNumber|(user)administrator|(hash)2526fa4a5e2bc2b4a58e8fab08d08b139025706f1f614719df397bc9bd3b90a5%}  
 
Order date:  {%Format(Order.OrderDate, "{0:d}")|(user)administrator|(hash)1cccd184308256a5f6b82024b0aa03bb4e9c9d4eaa029664e3714197a15300b6%} 
Order status:  {%GetResourceString(OrderStatus.StatusDisplayName)|(user)administrator|(hash)09902315904d1db9014aea7bf4d5d4d5680f68f7aed449931a8570c2d9dc27e6%}  
 
Supplier 
Company address  
Customer 
{%BillingAddress.ApplyTransformation("Ecommerce.Transformations.Order_Address"|(user)administrator|(hash)7960adbbbb40670d0bf29659fe42435cc7ec2db333a9b4f6d7d4d8133fe2c2e1%}
Company address: {%CompanyAddress.ApplyTransformation("Ecommerce.Transformations.Order_Address"|(user)administrator|(hash)98f29f2a9bdf80406f46478366c2c8276adaa3775727fa4a66819b5665e2abb9%}
 
Payment option  
{%GetResourceString(PaymentOption.PaymentOptionDisplayName)|(user)administrator|(hash)f4bd53a4cf633921fb1f15e9097ff5312b27b1eb54cf01fb5669c38dbdec1cb1%}  
 
Shipping option  
{%GetResourceString(ShippingOption.ShippingOptionDisplayName)|(user)administrator|(hash)748fabbfafb7f9dfdf45675a28eba4bdc4c072592e58a0c358051d0a3153bf8e%} 
 
{%ContentTable.ApplyTransformation("Ecommerce.Transformations.Order_ContentTable", "Ecommerce.Transformations.Order_ContentTableHeader", "Ecommerce.Transformations.Order_ContentTableFooter")|(user)administrator|(hash)9e8c2d4859c4e8c80639a9cffb6cc25fc935fb55e7b03c135c935bf947d8bcf0%} 
--------------------------------------------------------------------------------
Total shipping: {%TotalShipping.Format(Currency.CurrencyFormatString)|(user)administrator|(hash)ab9d10aa152f2e3a3c22e79c05607ad18691707fce7787aba2d512335482886e%}  
Total price: {%TotalPrice.Format(Currency.CurrencyFormatString)|(user)administrator|(hash)49695c0d3fa3613ef4a7958194fc8cfd077dee4d8198799beaf432a0dbf8c7f0%} 
Tax summary: 
{%ContentTaxesTable.ApplyTransformation("Ecommerce.Transformations.Order_TaxesTable", "Ecommerce.Transformations.Order_TaxesTableHeader", "Ecommerce.Transformations.Order_TaxesTableFooter")|(user)administrator|(hash)76921da8b0bdaf955b33c85e57dab8feaccc01f0ae2cedc9def953d4b39660c1%}  
  
Order note  
{%Order.OrderNote|(encode)|(user)administrator|(hash)a5ed7cf37728b6c1ad6cf0cf4c5d39df4ed3d35a95731a7bc7841d04318951bf%}
 
{%NewOrderLink%}', N'', N'', N'', N'', N'ecommerce')
INSERT INTO [CMS_EmailTemplate] ([EmailTemplateID], [EmailTemplateName], [EmailTemplateDisplayName], [EmailTemplateText], [EmailTemplateSiteID], [EmailTemplateGUID], [EmailTemplateLastModified], [EmailTemplatePlainText], [EmailTemplateSubject], [EmailTemplateFrom], [EmailTemplateCc], [EmailTemplateBcc], [EmailTemplateType]) VALUES (732, N'Ecommerce.OrderStatusNotificationToCustomer', N'E-commerce - Order status notification to customer', N'<html>
<head>
</head>
<body>
<p>Status of your order has changed.</p>
<table cellspacing="0" cellpadding="5" border="1" bordercolor="black" width="600px">
  <tr>
    <td colspan="2" valign="bottom" height="50">
      <table width="100%" height="100%">
        <tr>
          <td style="text-align: left; vertical-align: bottom;"
            <span style="font-size: 18pt">Your order</span>
          </td>
          <td style="text-align: center; vertical-align: middle;">
            <span style="font-family: Garamond, Times, serif; font-size: 24pt; font-style: italic;">Company logo</span>
          </td>
        </tr>
      </table> 
    </td>
  </tr>
  <tr>
    <td style="text-align: left">    
      <br />
      <table width="100%">
        <tr>
          <td style="text-align: left;" valign="bottom"> 
            <strong>Invoice number:</strong>
          </td>
          <td style="text-align: right; padding-right: 10px">
            {%Order.OrderInvoiceNumber|(user)administrator|(hash)2526fa4a5e2bc2b4a58e8fab08d08b139025706f1f614719df397bc9bd3b90a5%}
          </td>
        </tr>
      </table> 
      <br />
    </td>
    <td style="text-align: left">    
      <br />
      <table width="100%">
        <tr>
          <td style="text-align: left;" valign="bottom"> 
            <strong>Order date:</strong>
          </td>
          <td style="text-align: right; padding-right: 10px">
            {%Format(Order.OrderDate, "{0:d}")|(user)administrator|(hash)1cccd184308256a5f6b82024b0aa03bb4e9c9d4eaa029664e3714197a15300b6%}
          </td>
        </tr>
  <tr>
          <td style="text-align: left;" valign="bottom"> 
            <strong>Order status:</strong>
          </td>
          <td style="text-align: right; padding-right: 10px">
            {%GetResourceString(OrderStatus.StatusDisplayName)|(user)administrator|(hash)09902315904d1db9014aea7bf4d5d4d5680f68f7aed449931a8570c2d9dc27e6%}
          </td>
        </tr>
      </table>  
      <br />
    </td>
  </tr>
  <tr>
    <td style="text-align: left; vertical-align: top" width="50%">
      <strong>Supplier</strong>
      <br/>
      <br/>
      <table>
        <tr>
          <td>
            Company address
          </td>
        </tr>
      </table>
      <br />
    </td>
    <td style="text-align: left; vertical-align: top" width="50%">
      <span style="font-weight: bold"> Customer </span><br />
      <br />
        {%BillingAddress.ApplyTransformation("Ecommerce.Transformations.Order_Address"|(user)administrator|(hash)7960adbbbb40670d0bf29659fe42435cc7ec2db333a9b4f6d7d4d8133fe2c2e1%}
      <br />
  <strong>Company address:</strong>
  {%CompanyAddress.ApplyTransformation("Ecommerce.Transformations.Order_Address"|(user)administrator|(hash)98f29f2a9bdf80406f46478366c2c8276adaa3775727fa4a66819b5665e2abb9%}
      <br />
    </td>
  </tr>
  <tr>
    <td colspan="2">
      <table width="100%">
        <tr>
          <td style="text-align: left">
            <span style="font-weight: bold"> Payment option </span>
          </td>
        </tr>
        <tr>
          <td style="text-align: center">
            {%GetResourceString(PaymentOption.PaymentOptionDisplayName)|(user)administrator|(hash)f4bd53a4cf633921fb1f15e9097ff5312b27b1eb54cf01fb5669c38dbdec1cb1%}
          </td>
        </tr>
      </table> 
    </td>
  </tr>
  <tr>
    <td colspan="2">
      <table width="100%">
        <tr>
          <td style="text-align: left">
            <span style="font-weight: bold"> Shipping option </span>
          </td>
        </tr>
        <tr>
          <td style="text-align: center">
            {%GetResourceString(ShippingOption.ShippingOptionDisplayName)|(user)administrator|(hash)748fabbfafb7f9dfdf45675a28eba4bdc4c072592e58a0c358051d0a3153bf8e%}
          </td>
        </tr>
      </table>
    </td>
  </tr>
  <tr>
    <td style="text-align: left" colspan="2">
      {%ContentTable.ApplyTransformation("Ecommerce.Transformations.Order_ContentTable", "Ecommerce.Transformations.Order_ContentTableHeader", "Ecommerce.Transformations.Order_ContentTableFooter")|(user)administrator|(hash)9e8c2d4859c4e8c80639a9cffb6cc25fc935fb55e7b03c135c935bf947d8bcf0%}
      <hr size="1" />
      <div style="text-align: right;">
      <table style="text-align: left;" cellpadding="5">
  <tr>
    <td><strong>Total shipping:</strong></td>
          <td style="text-align: right; padding-right: 0px;">
            <strong>{%TotalShipping.Format(Currency.CurrencyFormatString)|(user)administrator|(hash)ab9d10aa152f2e3a3c22e79c05607ad18691707fce7787aba2d512335482886e%}</strong>
          </td>
  </tr>
  <tr>
    <td><strong>Total price:</strong></td>
          <td style="text-align: right; padding-right: 0px;">
            <strong>{%TotalPrice.Format(Currency.CurrencyFormatString)|(user)administrator|(hash)49695c0d3fa3613ef4a7958194fc8cfd077dee4d8198799beaf432a0dbf8c7f0%}</strong>
          </td>
  </tr>
  <tr>
    <td style="vertical-align:top;"><strong>Tax summary:</strong></td>
          <td style="text-align: right; padding-right: 0px;">
            {%ContentTaxesTable.ApplyTransformation("Ecommerce.Transformations.Order_TaxesTable", "Ecommerce.Transformations.Order_TaxesTableHeader", "Ecommerce.Transformations.Order_TaxesTableFooter")|(user)administrator|(hash)76921da8b0bdaf955b33c85e57dab8feaccc01f0ae2cedc9def953d4b39660c1%}
          </td>
  </tr>
      </table>
      </div>
      <div style="height: 120px;">&nbsp;</div>
    </td>
  </tr>
  <tr>
    <td colspan="2">
      <table width="100%">
        <tr>
          <td style="text-align: left">
            <span style="font-weight: bold"> Order note </span>
          </td>
        </tr>
        <tr>
          <td style="text-align: left">
            {%Order.OrderNote|(encode)|(user)administrator|(hash)a5ed7cf37728b6c1ad6cf0cf4c5d39df4ed3d35a95731a7bc7841d04318951bf%}
          </td>
        </tr>
      </table>
    </td>
  </tr>
</table>
</body>
</html>', NULL, '68ca5608-f80f-4972-88f3-24daaf669c32', '20110930 08:15:55', N'Status of your order has changed.
Your order  Company logo  
 
Invoice number:  {%Order.OrderInvoiceNumber|(user)administrator|(hash)2526fa4a5e2bc2b4a58e8fab08d08b139025706f1f614719df397bc9bd3b90a5%} 
 
Order date:  {%Format(Order.OrderDate, "{0:d}")|(user)administrator|(hash)1cccd184308256a5f6b82024b0aa03bb4e9c9d4eaa029664e3714197a15300b6%}  
Order status:  {%GetResourceString(OrderStatus.StatusDisplayName)|(user)administrator|(hash)09902315904d1db9014aea7bf4d5d4d5680f68f7aed449931a8570c2d9dc27e6%}  
 
Supplier 
Company address  
Customer 
{%BillingAddress.ApplyTransformation("Ecommerce.Transformations.Order_Address"|(user)administrator|(hash)7960adbbbb40670d0bf29659fe42435cc7ec2db333a9b4f6d7d4d8133fe2c2e1%}
Company address: {%CompanyAddress.ApplyTransformation("Ecommerce.Transformations.Order_Address"|(user)administrator|(hash)98f29f2a9bdf80406f46478366c2c8276adaa3775727fa4a66819b5665e2abb9%}
 
Payment option  
{%GetResourceString(PaymentOption.PaymentOptionDisplayName)|(user)administrator|(hash)f4bd53a4cf633921fb1f15e9097ff5312b27b1eb54cf01fb5669c38dbdec1cb1%}  
 
Shipping option  
{%GetResourceString(ShippingOption.ShippingOptionDisplayName)|(user)administrator|(hash)748fabbfafb7f9dfdf45675a28eba4bdc4c072592e58a0c358051d0a3153bf8e%}  
 
{%ContentTable.ApplyTransformation("Ecommerce.Transformations.Order_ContentTable", "Ecommerce.Transformations.Order_ContentTableHeader", "Ecommerce.Transformations.Order_ContentTableFooter")|(user)administrator|(hash)9e8c2d4859c4e8c80639a9cffb6cc25fc935fb55e7b03c135c935bf947d8bcf0%} 
--------------------------------------------------------------------------------
Total shipping: {%TotalShipping.Format(Currency.CurrencyFormatString)|(user)administrator|(hash)ab9d10aa152f2e3a3c22e79c05607ad18691707fce7787aba2d512335482886e%}  
Total price: {%TotalPrice.Format(Currency.CurrencyFormatString)|(user)administrator|(hash)49695c0d3fa3613ef4a7958194fc8cfd077dee4d8198799beaf432a0dbf8c7f0%}  
Tax summary: 
{%ContentTaxesTable.ApplyTransformation("Ecommerce.Transformations.Order_TaxesTable", "Ecommerce.Transformations.Order_TaxesTableHeader", "Ecommerce.Transformations.Order_TaxesTableFooter")|(user)administrator|(hash)76921da8b0bdaf955b33c85e57dab8feaccc01f0ae2cedc9def953d4b39660c1%}  
  
Order note  
{%Order.OrderNote|(encode)|(user)administrator|(hash)a5ed7cf37728b6c1ad6cf0cf4c5d39df4ed3d35a95731a7bc7841d04318951bf%}', N'', N'', N'', N'', N'ecommerce')
INSERT INTO [CMS_EmailTemplate] ([EmailTemplateID], [EmailTemplateName], [EmailTemplateDisplayName], [EmailTemplateText], [EmailTemplateSiteID], [EmailTemplateGUID], [EmailTemplateLastModified], [EmailTemplatePlainText], [EmailTemplateSubject], [EmailTemplateFrom], [EmailTemplateCc], [EmailTemplateBcc], [EmailTemplateType]) VALUES (161, N'Forums.NewPost', N'Forums -  New post', N'<html>
	<head>
	  <style>
		BODY, TD
		{
		  font-size: 12px; 
		  font-family: arial
		}
	  </style>
	</head>	
	<body>
	<p>
	  This is a notification of a new post added to the forum you subscribed to:
	</p>
	<table>
	  <tr valign="top">
		<td>
		<strong>Forum:</strong>
		</td>
		<td>
		{%forumdisplayname|(user)administrator|(hash)dd1dfb82deb7c9b2e3b197a001a5291d802843e3f0ce9c0bfdd95e91df9d7ca3%}
		</td>
	  </tr>
	  <tr valign="top">
		<td>
		<strong>Subject:</strong>
		</td>
		<td>
		{%postsubject|(user)administrator|(hash)e56d5792480ad52910a80e58c2b291c828171c7ac0b44710a5359226c3e4aec2%}
		</td>
	  </tr>
	  <tr valign="top">
		<td>
		<strong>Posted by:</strong>
		</td>
		<td>
		{%TrimSitePrefix(postusername)|(user)administrator|(hash)b0ae51562f7d1a17e88e14c7f874fb3d4c073375d0b0448a6c652333e7b23c96%}
		</td>
	  </tr>
	  <tr valign="top">
		<td>
		<strong>Date and time:</strong>
		</td>
		<td>
		{%posttime|(user)administrator|(hash)f62620daa9b076c5f0285f62df043c342623be119a9a078b1b0f1f229b640610%}
		</td>
	  </tr>
	  <tr valign="top">
		<td>
		<strong>Text:</strong>
		</td>
		<td>
		{%posttext|(user)administrator|(hash)a6b362d6212ce0833b9639c862a1474c2130a4d345367fbf6ccdfb82efac0133%}
		</td>
	  </tr>
	</table>	  
	<p>
	<a href="{%link|(user)administrator|(hash)3a1cd5192d11737a7dd51e04d3f31fd32d1dfa837d7f9baf3bb13e133a9d4cbc%}">Click here to view forum on-line</a> &nbsp;
        <a href="{%unsubscribelink|(user)administrator|(hash)17d4337051c12b345cb95ff96aa289a8b83328b81456e734143603b41afe40c1%}">Click here to unsubscribe</a>
	</p>
	</body>
</html>', NULL, '2fd97ef6-d0c3-4cd0-99fa-2cda6928773d', '20110905 17:35:19', N'This is a notification of a new post added to the forum you subscribed to: 
Forum:  {%forumdisplayname|(user)administrator|(hash)dd1dfb82deb7c9b2e3b197a001a5291d802843e3f0ce9c0bfdd95e91df9d7ca3%}  
Subject:  {%postsubject|(user)administrator|(hash)e56d5792480ad52910a80e58c2b291c828171c7ac0b44710a5359226c3e4aec2%}  
Posted by:  {%TrimSitePrefix(postusername)|(user)administrator|(hash)b0ae51562f7d1a17e88e14c7f874fb3d4c073375d0b0448a6c652333e7b23c96%}  
Date and time:  {%posttime|(user)administrator|(hash)f62620daa9b076c5f0285f62df043c342623be119a9a078b1b0f1f229b640610%}  
Text:  {%posttextplain|(user)administrator|(hash)0de8ef6ca151a2f05cffce3719512013956667dc6e76600b594108e9d0200912%}  
Click here to view forum on-line:
{%link|(user)administrator|(hash)3a1cd5192d11737a7dd51e04d3f31fd32d1dfa837d7f9baf3bb13e133a9d4cbc%} 
Click here to unsubscribe:
{%unsubscribelink|(user)administrator|(hash)17d4337051c12b345cb95ff96aa289a8b83328b81456e734143603b41afe40c1%}', N'', N'', N'', N'', N'forum')
INSERT INTO [CMS_EmailTemplate] ([EmailTemplateID], [EmailTemplateName], [EmailTemplateDisplayName], [EmailTemplateText], [EmailTemplateSiteID], [EmailTemplateGUID], [EmailTemplateLastModified], [EmailTemplatePlainText], [EmailTemplateSubject], [EmailTemplateFrom], [EmailTemplateCc], [EmailTemplateBcc], [EmailTemplateType]) VALUES (165, N'Forums.ModeratorNotice', N'Forums - Moderator notification', N'<html>
	<head>
	  <style>
		BODY, TD
		{
		  font-size: 12px; 
		  font-family: arial
		}
	  </style>
	</head>	
	<body>
	<p>
	  A new forum post is waiting for your approval.
	</p>
	<table>
	  <tr valign="top">
		<td>
		<strong>Forum:</strong>
		</td>
		<td>
		{%forumdisplayname|(user)administrator|(hash)dd1dfb82deb7c9b2e3b197a001a5291d802843e3f0ce9c0bfdd95e91df9d7ca3%}
		</td>
	  </tr>
	  <tr valign="top">
		<td>
		<strong>Subject:</strong>
		</td>
		<td>
		{%postsubject|(user)administrator|(hash)e56d5792480ad52910a80e58c2b291c828171c7ac0b44710a5359226c3e4aec2%}
		</td>
	  </tr>
	  <tr valign="top">
		<td>
		<strong>Posted by:</strong>
		</td>
		<td>
		{%TrimSitePrefix(postusername)|(user)administrator|(hash)b0ae51562f7d1a17e88e14c7f874fb3d4c073375d0b0448a6c652333e7b23c96%}
		</td>
	  </tr>
	  <tr valign="top">
		<td>
		<strong>Date and time:</strong>
		</td>
		<td>
		{%posttime|(user)administrator|(hash)f62620daa9b076c5f0285f62df043c342623be119a9a078b1b0f1f229b640610%}
		</td>
	  </tr>
	  <tr valign="top">
		<td>
		<strong>Text:</strong>
		</td>
		<td>
		{%posttext|(user)administrator|(hash)a6b362d6212ce0833b9639c862a1474c2130a4d345367fbf6ccdfb82efac0133%}
		</td>
	  </tr>
	</table>	  
	<p>
	<a href="{%link|(user)administrator|(hash)3a1cd5192d11737a7dd51e04d3f31fd32d1dfa837d7f9baf3bb13e133a9d4cbc%}">Click here to view the forum on-line</a> &nbsp;
	</p>
	</body>
</html>', NULL, 'f1fc231d-d1c4-425c-83d0-dd5197bc4c7b', '20110905 17:36:01', N'A new forum post is waiting for your approval. 
Forum:  {%forumdisplayname|(user)administrator|(hash)dd1dfb82deb7c9b2e3b197a001a5291d802843e3f0ce9c0bfdd95e91df9d7ca3%}  
Subject:  {%postsubject|(user)administrator|(hash)e56d5792480ad52910a80e58c2b291c828171c7ac0b44710a5359226c3e4aec2%}  
Posted by:  {%TrimSitePrefix(postusername)|(user)administrator|(hash)b0ae51562f7d1a17e88e14c7f874fb3d4c073375d0b0448a6c652333e7b23c96%}  
Date and time:  {%posttime|(user)administrator|(hash)f62620daa9b076c5f0285f62df043c342623be119a9a078b1b0f1f229b640610%}  
Text:  {%posttext|(user)administrator|(hash)a6b362d6212ce0833b9639c862a1474c2130a4d345367fbf6ccdfb82efac0133%}  
Click here to view the forum on-line:
{%link|(user)administrator|(hash)3a1cd5192d11737a7dd51e04d3f31fd32d1dfa837d7f9baf3bb13e133a9d4cbc%}', N'', N'', N'', N'', N'forum')
INSERT INTO [CMS_EmailTemplate] ([EmailTemplateID], [EmailTemplateName], [EmailTemplateDisplayName], [EmailTemplateText], [EmailTemplateSiteID], [EmailTemplateGUID], [EmailTemplateLastModified], [EmailTemplatePlainText], [EmailTemplateSubject], [EmailTemplateFrom], [EmailTemplateCc], [EmailTemplateBcc], [EmailTemplateType]) VALUES (764, N'Forums.SubscribeConfirmation', N'Forums - subscription confirmation', N'<html>
	<head>
	  <style>
		BODY, TD
		{
		  font-size: 12px; 
		  font-family: arial
		}
	  </style>
	</head>	
	<body>
	<p>
	  You have been successfully subscribed to		
	<strong>Forum</strong> {%forumdisplayname%}{%separator%}{%subject%}.		    
	<p/>
	<p>
	<a href="{%link%}">Click here to view forum on-line</a> &nbsp;
        <a href="{%unsubscribelink%}">Click here to unsubscribe</a>
	</p>
	</body>
</html>', NULL, 'dc4a0178-3055-4339-bab5-5dc1011b9a41', '20090112 19:19:03', N'You have been successfully subscribed to Forum {%forumdisplayname%}{%separator%}{%subject%}. 
Click here to view forum on-line:
{%link%}
Click here to unsubscribe:
{%unsubscribelink%}', N'Forum subscription notification', N'', N'', N'', N'forumsubscribtion')
INSERT INTO [CMS_EmailTemplate] ([EmailTemplateID], [EmailTemplateName], [EmailTemplateDisplayName], [EmailTemplateText], [EmailTemplateSiteID], [EmailTemplateGUID], [EmailTemplateLastModified], [EmailTemplatePlainText], [EmailTemplateSubject], [EmailTemplateFrom], [EmailTemplateCc], [EmailTemplateBcc], [EmailTemplateType]) VALUES (765, N'Forums.UnsubscribeConfirmation', N'Forums - unsubscription confirmation', N'<html>
	<head>
	  <style>
		BODY, TD
		{
		  font-size: 12px; 
		  font-family: arial
		}
	  </style>
	</head>	
	<body>
	<p>
	  You have been successfully unsubscribed from		
	<strong>Forum</strong> {%forumdisplayname%}{%separator%}{%subject%}.		    
	<p/>	
	</body>
</html>', NULL, '1d636437-f61c-40dc-8bc1-383c66a1bfc1', '20081221 15:48:49', N'You have been successfully unsubscribed from Forum {%forumdisplayname%}{%separator%}{%subject%}.', N'Forum unsubscription notification', N'', N'', N'', N'forumsubscribtion')
INSERT INTO [CMS_EmailTemplate] ([EmailTemplateID], [EmailTemplateName], [EmailTemplateDisplayName], [EmailTemplateText], [EmailTemplateSiteID], [EmailTemplateGUID], [EmailTemplateLastModified], [EmailTemplatePlainText], [EmailTemplateSubject], [EmailTemplateFrom], [EmailTemplateCc], [EmailTemplateBcc], [EmailTemplateType]) VALUES (745, N'Friends.Approve', N'Friends - Friend approval', N'<html>
  <head>
  </head>
  <body>
    <p>{%FORMATTEDSENDERNAME|(user)administrator|(hash)f47bafdfed3b5e831fc6bb1308aa1753430d41955b3734aa41c3e1b63bae7476%} is now your friend.</p>
    <p>Comment: {%Friendship.FriendComment.HTMLEncode()|(user)administrator|(hash)9fb6e4084b11ff40c04757edbf05f5f12246f720e7c9a4cd9eb12f56235356ac%}</p>
    <p>Sent: {%Friendship.FriendApprovedWhen|(user)administrator|(hash)97f937065abdc565157539a4d8ad30b070d05383f73997164fcd3501b16aa0bb%}</p>
  </body>
</html>', NULL, '14926c79-639a-414b-b22d-2ca7cea58f2b', '20110906 01:37:38', N'{%FORMATTEDSENDERNAME|(user)administrator|(hash)f47bafdfed3b5e831fc6bb1308aa1753430d41955b3734aa41c3e1b63bae7476%} is now your friend.
Comment: {%Friendship.FriendComment|(user)administrator|(hash)35aafd579b3193998ac5a10dbd97138142bbd85ce6b367335f37d5874940e0b3%}
Sent: {%Friendship.FriendApprovedWhen|(user)administrator|(hash)97f937065abdc565157539a4d8ad30b070d05383f73997164fcd3501b16aa0bb%}', N'{%FORMATTEDSENDERNAME|(user)administrator|(hash)f47bafdfed3b5e831fc6bb1308aa1753430d41955b3734aa41c3e1b63bae7476%} is now your friend', N'', N'', N'', N'friends')
INSERT INTO [CMS_EmailTemplate] ([EmailTemplateID], [EmailTemplateName], [EmailTemplateDisplayName], [EmailTemplateText], [EmailTemplateSiteID], [EmailTemplateGUID], [EmailTemplateLastModified], [EmailTemplatePlainText], [EmailTemplateSubject], [EmailTemplateFrom], [EmailTemplateCc], [EmailTemplateBcc], [EmailTemplateType]) VALUES (746, N'Friends.Reject', N'Friends - Friend rejection', N'<html>
  <head>
  </head>
  <body>
    <p>{%FORMATTEDSENDERNAME|(user)administrator|(hash)f47bafdfed3b5e831fc6bb1308aa1753430d41955b3734aa41c3e1b63bae7476%} rejected the friendship.</p>
    <p>Comment: {%Friendship.FriendComment.HTMLEncode()|(user)administrator|(hash)9fb6e4084b11ff40c04757edbf05f5f12246f720e7c9a4cd9eb12f56235356ac%}</p>
    <p>Sent: {%Friendship.FriendRejectedWhen|(user)administrator|(hash)156c79a62de3a69efe5caa50a661963ca9af628cc766fee6049098c8c0504988%}</p>
  </body>
</html>', NULL, 'd5d2b5f4-bcbc-4746-a5af-08833dd4c51a', '20110906 01:37:22', N'{%FORMATTEDSENDERNAME|(user)administrator|(hash)f47bafdfed3b5e831fc6bb1308aa1753430d41955b3734aa41c3e1b63bae7476%} has rejected the friendship.
Comment: {%Friendship.FriendComment|(user)administrator|(hash)35aafd579b3193998ac5a10dbd97138142bbd85ce6b367335f37d5874940e0b3%}
Sent: {%Friendship.FriendRejectedWhen|(user)administrator|(hash)156c79a62de3a69efe5caa50a661963ca9af628cc766fee6049098c8c0504988%}', N'{%FORMATTEDSENDERNAME|(user)administrator|(hash)f47bafdfed3b5e831fc6bb1308aa1753430d41955b3734aa41c3e1b63bae7476%} rejected the friendship', N'', N'', N'', N'friends')
INSERT INTO [CMS_EmailTemplate] ([EmailTemplateID], [EmailTemplateName], [EmailTemplateDisplayName], [EmailTemplateText], [EmailTemplateSiteID], [EmailTemplateGUID], [EmailTemplateLastModified], [EmailTemplatePlainText], [EmailTemplateSubject], [EmailTemplateFrom], [EmailTemplateCc], [EmailTemplateBcc], [EmailTemplateType]) VALUES (747, N'Friends.Request', N'Friends - Friend request', N'<html>
  <head>
  </head>
  <body>
    <p>{%FORMATTEDSENDERNAME|(user)administrator|(hash)f47bafdfed3b5e831fc6bb1308aa1753430d41955b3734aa41c3e1b63bae7476%} wants to be your friend.</p>
    <p>Comment: {%Friendship.FriendComment|(user)administrator|(hash)35aafd579b3193998ac5a10dbd97138142bbd85ce6b367335f37d5874940e0b3%}</p>
    <p>Sent: {%Friendship.FriendRequestedWhen.HTMLEncode()|(user)administrator|(hash)a616eb2d16692f41cf261de19af12cc6a8d06d804cb5135344602815b566aea7%}</p>
    <p>Choose one of the following actions:</p>    
    <p><a href="{%MANAGEMENTURL|(user)administrator|(hash)c64ae4c1e0944b29837b5f5e765bc0b4818e3a165abc39c2fd685d9e8fd400c9%}">Accept or reject</a></p>
    <p><a href="{%PROFILEURL|(user)administrator|(hash)d76fde75600a5758fb51fd90644e5c86640312b83071a8ea41aba17ee66bf4fd%}">Open user profile</a></p>
  </body>
</html>', NULL, '6fe616ca-b9d8-4980-90be-9124c2546cde', '20110906 01:37:08', N'{%FORMATTEDSENDERNAME|(user)administrator|(hash)f47bafdfed3b5e831fc6bb1308aa1753430d41955b3734aa41c3e1b63bae7476%} wants to be your friend.
Comment: {%Friendship.FriendComment|(user)administrator|(hash)35aafd579b3193998ac5a10dbd97138142bbd85ce6b367335f37d5874940e0b3%}
Sent: {%Friendship.FriendRequestedWhen|(user)administrator|(hash)6045b6d11199beb1c7e4c10e0c6b825e20ae8e1d4761821849ab725942e1a79a%}
Choose one of the following actions:
[url={%MANAGEMENTURL|(user)administrator|(hash)c64ae4c1e0944b29837b5f5e765bc0b4818e3a165abc39c2fd685d9e8fd400c9%}]Accept or reject[/url]
[url={%PROFILEURL|(user)administrator|(hash)d76fde75600a5758fb51fd90644e5c86640312b83071a8ea41aba17ee66bf4fd%}]Open user profile[/url]', N'{%FORMATTEDSENDERNAME|(user)administrator|(hash)f47bafdfed3b5e831fc6bb1308aa1753430d41955b3734aa41c3e1b63bae7476%} wants to be your friend', N'', N'', N'', N'friends')
INSERT INTO [CMS_EmailTemplate] ([EmailTemplateID], [EmailTemplateName], [EmailTemplateDisplayName], [EmailTemplateText], [EmailTemplateSiteID], [EmailTemplateGUID], [EmailTemplateLastModified], [EmailTemplatePlainText], [EmailTemplateSubject], [EmailTemplateFrom], [EmailTemplateCc], [EmailTemplateBcc], [EmailTemplateType]) VALUES (763, N'Groups.MemberAcceptedInvitation', N'Groups - Member accepted invitation', N'<html>
	<head>
	</head>
	<body>
		<p>{%Sender.FullName|(user)administrator|(hash)e467db00ce17eb4e06ea06fa5bd9b16ac74f2b2c72bc85edb4b929b95403672f%}({%TrimSitePrefix(Sender.UserName)|(user)administrator|(hash)7bb3a85cf09be462831a747a54a53bdd2a49dd7ad63cd53af2552b67ac99800d%}) has accepted your invitation to group ''{%Group.GroupDisplayName|(user)administrator|(hash)9ee133a4404e1a531b3ec9af7433de0b106a9d36c8ecb799a7cb1610812e3aff%}''.<br />
		When: {%GroupMember.MemberJoined|(user)administrator|(hash)99a5495e8e4973499c3431ebd8db98558f3e3815bfe091284ea6504079898f27%}</p>
		<br />
		<br />
		Best Regards,<br />
		<br />
		The Community Team
	</body>
</html>', NULL, '0f68bc2f-bf08-40cf-b4e4-c2550e94f570', '20110905 17:37:05', N'{%Sender.FullName|(user)administrator|(hash)e467db00ce17eb4e06ea06fa5bd9b16ac74f2b2c72bc85edb4b929b95403672f%}({%TrimSitePrefix(Sender.UserName)|(user)administrator|(hash)7bb3a85cf09be462831a747a54a53bdd2a49dd7ad63cd53af2552b67ac99800d%}) has accepted your invitation to group ''{%Group.GroupDisplayName|(user)administrator|(hash)9ee133a4404e1a531b3ec9af7433de0b106a9d36c8ecb799a7cb1610812e3aff%}''.
When: {%GroupMember.MemberJoined|(user)administrator|(hash)99a5495e8e4973499c3431ebd8db98558f3e3815bfe091284ea6504079898f27%}
Best Regards,
The Community Team', N'Invitation to group ''{%Group.GroupDisplayName|(user)administrator|(hash)9ee133a4404e1a531b3ec9af7433de0b106a9d36c8ecb799a7cb1610812e3aff%}'' was accepted', N'', N'', N'', N'groupmemberinvitation')
INSERT INTO [CMS_EmailTemplate] ([EmailTemplateID], [EmailTemplateName], [EmailTemplateDisplayName], [EmailTemplateText], [EmailTemplateSiteID], [EmailTemplateGUID], [EmailTemplateLastModified], [EmailTemplatePlainText], [EmailTemplateSubject], [EmailTemplateFrom], [EmailTemplateCc], [EmailTemplateBcc], [EmailTemplateType]) VALUES (758, N'Groups.MemberApproved', N'Groups - Member approved', N'<html>
  <head>
  </head>
  <body>
    <p>Dear {%MemberUser.FirstName%},<br />
    <br />
    you have been approved as a full member of group ''{%Group.GroupDisplayName%}''.</p>
    <br />
    <br />
    Best Regards,<br />
    <br />
    The Community Team
  </body>
</html>', NULL, 'd57092d8-28c3-4784-98d2-7d02f4a73a66', '20101123 10:28:50', N'Dear {%MemberUser.FirstName%},
you have been approved as a full member of group ''{%Group.GroupDisplayName%}''.
Best Regards,
The Community Team', N'', N'', N'', N'', N'groupmember')
INSERT INTO [CMS_EmailTemplate] ([EmailTemplateID], [EmailTemplateName], [EmailTemplateDisplayName], [EmailTemplateText], [EmailTemplateSiteID], [EmailTemplateGUID], [EmailTemplateLastModified], [EmailTemplatePlainText], [EmailTemplateSubject], [EmailTemplateFrom], [EmailTemplateCc], [EmailTemplateBcc], [EmailTemplateType]) VALUES (761, N'Groups.Invitation', N'Groups - Member invitation', N'<html>
	<head>
	</head>
	<body>
		<p>User {%InvitedBy%} invites you to group ''{%Group.GroupDisplayName%}''.<br />
		With comment "{%Invitation.InvitationComment%}"<br /><br />
		Follow the link below to join the group:<br /><br />
		<a href="{%ACCEPTIONURL%}">{%ACCEPTIONURL%}</a>
		</p>
		<br />
		<br />
		Best Regards,
		<br />
		<br />
		The Community Team
	</body>
</html>', NULL, '8909e20f-80b3-4be1-9c47-61000e856784', '20090127 20:21:24', N'User {%InvitedBy%} invites you to group ''{%Group.GroupDisplayName%}''.
With comment "{%Invitation.InvitationComment%}"
Follow the link below to join the group:
[url={%ACCEPTIONURL%}]{%ACCEPTIONURL%}[/url]
Best Regards,
The Community Team', N'Invitation to group "{%Group.GroupDisplayName%}"', N'', N'', N'', N'groupinvitation')
INSERT INTO [CMS_EmailTemplate] ([EmailTemplateID], [EmailTemplateName], [EmailTemplateDisplayName], [EmailTemplateText], [EmailTemplateSiteID], [EmailTemplateGUID], [EmailTemplateLastModified], [EmailTemplatePlainText], [EmailTemplateSubject], [EmailTemplateFrom], [EmailTemplateCc], [EmailTemplateBcc], [EmailTemplateType]) VALUES (753, N'Groups.MemberJoin', N'Groups - Member join', N'<html>
	<head>
	</head>
	<body>
		<p>User {%MemberUser.FullName|(user)administrator|(hash)973de5a5a86d5ed9dba04e42e70f66ccccacc919d52742ca07c976022ef6562c%}({%TrimSitePrefix(MemberUser.UserName)|(user)administrator|(hash)23548d716d81c3f723f92c36b7811d988ac42f32d24e0afbb2543a290c82acd5%}) joined group ''{%Group.GroupDisplayName|(user)administrator|(hash)9ee133a4404e1a531b3ec9af7433de0b106a9d36c8ecb799a7cb1610812e3aff%}''.</p>
		<br />
		<br />
		Best Regards,<br />
		<br />
		The Community Team
	</body>
</html>', NULL, '7125b6a1-d72b-4e14-9995-2a4e73b910bc', '20110905 17:37:38', N'User {%MemberUser.FullName|(user)administrator|(hash)973de5a5a86d5ed9dba04e42e70f66ccccacc919d52742ca07c976022ef6562c%}({%TrimSitePrefix(MemberUser.UserName)|(user)administrator|(hash)23548d716d81c3f723f92c36b7811d988ac42f32d24e0afbb2543a290c82acd5%}) joined group ''{%Group.GroupDisplayName|(user)administrator|(hash)9ee133a4404e1a531b3ec9af7433de0b106a9d36c8ecb799a7cb1610812e3aff%}''.
Best Regards,
The Community Team', N'', N'', N'', N'', N'groupmember')
INSERT INTO [CMS_EmailTemplate] ([EmailTemplateID], [EmailTemplateName], [EmailTemplateDisplayName], [EmailTemplateText], [EmailTemplateSiteID], [EmailTemplateGUID], [EmailTemplateLastModified], [EmailTemplatePlainText], [EmailTemplateSubject], [EmailTemplateFrom], [EmailTemplateCc], [EmailTemplateBcc], [EmailTemplateType]) VALUES (757, N'Groups.MemberJoinedConfirmation', N'Groups - Member joined confirmation', N'<html>
	<head>
	</head>
	<body>
		<p>Dear {%MemberUser.FirstName%},<br />
		<br />
		welcome to group ''{%Group.GroupDisplayName%}''.</p>
		<br />
		<br />
		Best Regards,
		<br />
		<br />
		The Community Team
	</body>
</html>', NULL, '03636c82-1f7f-44e0-9b51-b7a6c705114e', '20090127 20:20:25', N'Dear {%MemberUser.FirstName%},
welcome to group ''{%Group.GroupDisplayName%}''.
Best Regards,
The Community Team', N'', N'', N'', N'', N'groupmember')
INSERT INTO [CMS_EmailTemplate] ([EmailTemplateID], [EmailTemplateName], [EmailTemplateDisplayName], [EmailTemplateText], [EmailTemplateSiteID], [EmailTemplateGUID], [EmailTemplateLastModified], [EmailTemplatePlainText], [EmailTemplateSubject], [EmailTemplateFrom], [EmailTemplateCc], [EmailTemplateBcc], [EmailTemplateType]) VALUES (756, N'Groups.MemberJoinedWaitingForApproval', N'Groups - Member joined waiting for approval', N'<html>
	<head>
	</head>
	<body>
		<p>Dear {%MemberUser.FirstName%},<br />
		<br />
		you are now waiting for approval to group ''{%Group.GroupDisplayName%}''.</p>
		<br />
		<br />
		Best Regards,<br />
		<br />
		The Community Team
	</body>
</html>', NULL, '4c80e736-d3b4-4288-9d67-bf764634a32e', '20090127 20:19:55', N'Dear {%MemberUser.FirstName%},
you are now waiting for approval to group ''{%Group.GroupDisplayName%}''.
Best Regards,
The Community Team', N'', N'', N'', N'', N'groupmember')
INSERT INTO [CMS_EmailTemplate] ([EmailTemplateID], [EmailTemplateName], [EmailTemplateDisplayName], [EmailTemplateText], [EmailTemplateSiteID], [EmailTemplateGUID], [EmailTemplateLastModified], [EmailTemplatePlainText], [EmailTemplateSubject], [EmailTemplateFrom], [EmailTemplateCc], [EmailTemplateBcc], [EmailTemplateType]) VALUES (754, N'Groups.MemberLeave', N'Groups - Member leave', N'<html>
	<head>
	</head>
	<body>
		<p>User {%MemberUser.FullName|(user)administrator|(hash)973de5a5a86d5ed9dba04e42e70f66ccccacc919d52742ca07c976022ef6562c%}({%TrimSitePrefix(MemberUser.UserName)|(user)administrator|(hash)23548d716d81c3f723f92c36b7811d988ac42f32d24e0afbb2543a290c82acd5%}) just left group ''{%Group.GroupDisplayName|(user)administrator|(hash)9ee133a4404e1a531b3ec9af7433de0b106a9d36c8ecb799a7cb1610812e3aff%}''.</p>
		<br />
		<br />
		Best Regards,<br />
		<br />
		The Community Team
	</body>
</html>', NULL, '85636277-a9a7-46ba-80f0-50e3fd30bc24', '20110905 17:38:47', N'User {%MemberUser.FullName|(user)administrator|(hash)973de5a5a86d5ed9dba04e42e70f66ccccacc919d52742ca07c976022ef6562c%}({%TrimSitePrefix(MemberUser.UserName)|(user)administrator|(hash)23548d716d81c3f723f92c36b7811d988ac42f32d24e0afbb2543a290c82acd5%}) just left group ''{%Group.GroupDisplayName|(user)administrator|(hash)9ee133a4404e1a531b3ec9af7433de0b106a9d36c8ecb799a7cb1610812e3aff%}''.
Best Regards,
The Community Team', N'', N'', N'', N'', N'groupmember')
INSERT INTO [CMS_EmailTemplate] ([EmailTemplateID], [EmailTemplateName], [EmailTemplateDisplayName], [EmailTemplateText], [EmailTemplateSiteID], [EmailTemplateGUID], [EmailTemplateLastModified], [EmailTemplatePlainText], [EmailTemplateSubject], [EmailTemplateFrom], [EmailTemplateCc], [EmailTemplateBcc], [EmailTemplateType]) VALUES (759, N'Groups.MemberRejected', N'Groups - Member rejected', N'<html>
	<head>
	</head>
	<body>
		<p>Dear {%MemberUser.FirstName%},<br />
		<br />
		you have been rejected from group ''{%Group.GroupDisplayName%}''.</p>
		<br />
		<br />
		Best Regards,<br />
		<br />
		The Community Team
	</body>
</html>', NULL, '0d52d7da-4ab4-4342-b52c-a94f14f23ee9', '20090127 20:21:48', N'Dear {%MemberUser.FirstName%},
you have been rejected from group ''{%Group.GroupDisplayName%}''.
Best Regards,
The Community Team', N'', N'', N'', N'', N'groupmember')
INSERT INTO [CMS_EmailTemplate] ([EmailTemplateID], [EmailTemplateName], [EmailTemplateDisplayName], [EmailTemplateText], [EmailTemplateSiteID], [EmailTemplateGUID], [EmailTemplateLastModified], [EmailTemplatePlainText], [EmailTemplateSubject], [EmailTemplateFrom], [EmailTemplateCc], [EmailTemplateBcc], [EmailTemplateType]) VALUES (755, N'Groups.MemberWaitingForApproval', N'Groups - Member waiting for approval', N'<html>
	<head>
	</head>
	<body>
		<p>User {%MemberUser.FullName|(user)administrator|(hash)973de5a5a86d5ed9dba04e42e70f66ccccacc919d52742ca07c976022ef6562c%}({%TrimSitePrefix(MemberUser.UserName)|(user)administrator|(hash)23548d716d81c3f723f92c36b7811d988ac42f32d24e0afbb2543a290c82acd5%}) is waiting for approval into group ''{%Group.GroupDisplayName|(user)administrator|(hash)9ee133a4404e1a531b3ec9af7433de0b106a9d36c8ecb799a7cb1610812e3aff%}''.</p>
		<br />
		<br />
		Best Regards,<br />
		<br />
		The Community Team
	</body>
</html>', NULL, '719d4419-0b2d-49db-ba8b-a5e29648d98c', '20110905 17:39:48', N'User {%MemberUser.FullName|(user)administrator|(hash)973de5a5a86d5ed9dba04e42e70f66ccccacc919d52742ca07c976022ef6562c%}({%TrimSitePrefix(MemberUser.UserName)|(user)administrator|(hash)23548d716d81c3f723f92c36b7811d988ac42f32d24e0afbb2543a290c82acd5%}) is waiting for approval into group ''{%Group.GroupDisplayName|(user)administrator|(hash)9ee133a4404e1a531b3ec9af7433de0b106a9d36c8ecb799a7cb1610812e3aff%}''.
Best Regards,
The Community Team', N'', N'', N'', N'', N'groupmember')
INSERT INTO [CMS_EmailTemplate] ([EmailTemplateID], [EmailTemplateName], [EmailTemplateDisplayName], [EmailTemplateText], [EmailTemplateSiteID], [EmailTemplateGUID], [EmailTemplateLastModified], [EmailTemplatePlainText], [EmailTemplateSubject], [EmailTemplateFrom], [EmailTemplateCc], [EmailTemplateBcc], [EmailTemplateType]) VALUES (781, N'Groups.WaitingForApproval', N'Groups - Waiting for approval', N'<html>
  <head>
  </head>
  <body>    
    Group ''{%Group.GroupDisplayName%}'' is waiting for your approval.
    <br />
    Best Regards,<br />
    <br />
    The Community Team
  </body>
</html>', NULL, '08d21450-4431-46c9-b654-a35b622fad10', '20101123 14:20:48', N'Group ''{%Group.GroupDisplayName%}'' is waiting for your approval.
Best Regards,
The Community Team', N'Group ''{%Group.GroupDisplayName%}'' is waiting for approval', N'', N'', N'', N'groupmember')
INSERT INTO [CMS_EmailTemplate] ([EmailTemplateID], [EmailTemplateName], [EmailTemplateDisplayName], [EmailTemplateText], [EmailTemplateSiteID], [EmailTemplateGUID], [EmailTemplateLastModified], [EmailTemplatePlainText], [EmailTemplateSubject], [EmailTemplateFrom], [EmailTemplateCc], [EmailTemplateBcc], [EmailTemplateType]) VALUES (842, N'Membership.ChangePasswordRequest', N'Membership - Change password request', N'<html>
  <head>
  </head>
  <body style="font-size:12px; font-family: Arial">
    <p>
       You have submitted a request to change your existing password. Please click <a href="{%ResetPasswordUrl%}">this link</a> to generate a new password.
    </p>
    <p>
       If you want to cancel your request or you did not send request please click <a href="{%CancelUrl%}">this link</a> to invalidate the operation.
    </p>
  </body>
</html>', NULL, 'c97cec10-ecac-4f15-ab20-99f5008d49cf', '20111006 13:20:07', N'You have submitted a request to change your existing password. Please go to this link {%ResetPasswordUrl%} to generate a new password.
If you want to cancel your request or you did not send request please go to this link {%CancelUrl%} to invalidate the operation.', N'', N'', N'', N'', N'membershipchangepassword')
INSERT INTO [CMS_EmailTemplate] ([EmailTemplateID], [EmailTemplateName], [EmailTemplateDisplayName], [EmailTemplateText], [EmailTemplateSiteID], [EmailTemplateGUID], [EmailTemplateLastModified], [EmailTemplatePlainText], [EmailTemplateSubject], [EmailTemplateFrom], [EmailTemplateCc], [EmailTemplateBcc], [EmailTemplateType]) VALUES (738, N'Membership.ChangedPassword', N'Membership - Changed password', N'<html>
<head>
</head>
<body>
Your password has been changed.<br />
<p>Your user name and new password are:</p>
<p><strong>User name:</strong> {%TrimSitePrefix(UserName)|(user)administrator|(hash)ae84cc044b37369545eb088351375699fb72d2742101a18cb3cc50597b49e736%}<br />
<strong>Password:</strong> {%Password|(user)administrator|(hash)3fa3e7dfc7ed2fd25117e5ac94ee3cc91c4597aa0db425e8e9c5e316b9fc019c%}</p>
<br />
<br />
</body>
</html>', NULL, '97b2f851-0693-40e6-8be1-6edd27d138d4', '20110905 17:40:30', N'Your password has been changed.
Your user name and new password are:
User name: {%TrimSitePrefix(UserName)|(user)administrator|(hash)ae84cc044b37369545eb088351375699fb72d2742101a18cb3cc50597b49e736%}
Password: {%Password|(user)administrator|(hash)3fa3e7dfc7ed2fd25117e5ac94ee3cc91c4597aa0db425e8e9c5e316b9fc019c%}', N'', N'', N'', N'', N'password')
INSERT INTO [CMS_EmailTemplate] ([EmailTemplateID], [EmailTemplateName], [EmailTemplateDisplayName], [EmailTemplateText], [EmailTemplateSiteID], [EmailTemplateGUID], [EmailTemplateLastModified], [EmailTemplatePlainText], [EmailTemplateSubject], [EmailTemplateFrom], [EmailTemplateCc], [EmailTemplateBcc], [EmailTemplateType]) VALUES (807, N'Membership.ExpirationNotification', N'Membership - Expiration notification', N'The following memberships will expire soon:
{%MembershipsTable.ApplyTransformation("Ecommerce.Transformations.Order_MembershipsTable")|(user)administrator|(hash)b1f81f748b2d8ba2ccb4736fed24fe73b2b6674f6c04480c1c9893e0c802d112%}
<br />
To renew it, please follow these steps:
<ol>
<li>In My profile section on My memberships tab click the Buy membership button. You will be redirected to the Buy membership page.</li>
<li>Choose the required membership and add it to your shopping cart.</li>
<li>Finish your order.</li>
<li>Once the order is paid, your membership will be renewed.</li>
</ol>
This is an automatic reminder, please do not respond.<br />
Thank you.', NULL, '96db7d3f-e7ca-41eb-926a-9fe92babe634', '20110907 17:13:50', N'The following memberships will expire soon:
{%MembershipsTable.ApplyTransformation("Ecommerce.Transformations.Order_MembershipsTable")|(user)administrator|(hash)b1f81f748b2d8ba2ccb4736fed24fe73b2b6674f6c04480c1c9893e0c802d112%}
To renew it, please follow these steps:
1) In My profile section on My memberships tab click the Buy membership button. You will be redirected to the Buy membership page.
2) Choose the required membership and add it to your shopping cart.
3) Finish your order.
4) Once the order is paid, your membership will be renewed.
This is an automatic reminder, please do not respond.
Thank you.', N'', N'', N'', N'', N'membershipexpiration')
INSERT INTO [CMS_EmailTemplate] ([EmailTemplateID], [EmailTemplateName], [EmailTemplateDisplayName], [EmailTemplateText], [EmailTemplateSiteID], [EmailTemplateGUID], [EmailTemplateLastModified], [EmailTemplatePlainText], [EmailTemplateSubject], [EmailTemplateFrom], [EmailTemplateCc], [EmailTemplateBcc], [EmailTemplateType]) VALUES (162, N'forgottenPassword', N'Membership - Forgotten password', N'<html>
	<head>
	</head>
	<body style="font-size:12px; font-family: Arial">
		<p>
                 You requested a forgotten password at <a href="{%LogonUrl|(user)administrator|(hash)59e2527de35bc66478f351bb0c1064f85ecef99bf976912e293d7e0d06a7038e%}">{%LogonUrl|(user)administrator|(hash)59e2527de35bc66478f351bb0c1064f85ecef99bf976912e293d7e0d06a7038e%}</a>. 
		</p>
		<p>
		Your user name and new password are:
		</p>
		<p>
		<strong>User name:</strong> {%TrimSitePrefix(UserName)|(user)administrator|(hash)ae84cc044b37369545eb088351375699fb72d2742101a18cb3cc50597b49e736%}
                <br/>
                <strong>Password:</strong> {%Password|(user)administrator|(hash)3fa3e7dfc7ed2fd25117e5ac94ee3cc91c4597aa0db425e8e9c5e316b9fc019c%}
		</p>
	</body>
</html>', NULL, 'd46985fb-e598-4b35-88e1-32bc8508aff5', '20110905 17:41:05', N'You requested a forgotten password at [url={%LogonUrl|(user)administrator|(hash)59e2527de35bc66478f351bb0c1064f85ecef99bf976912e293d7e0d06a7038e%}]{%LogonUrl|(user)administrator|(hash)59e2527de35bc66478f351bb0c1064f85ecef99bf976912e293d7e0d06a7038e%}[/url]. 
Your user name and new password are: 
User name: {%TrimSitePrefix(UserName)|(user)administrator|(hash)ae84cc044b37369545eb088351375699fb72d2742101a18cb3cc50597b49e736%} 
Password: {%Password|(user)administrator|(hash)3fa3e7dfc7ed2fd25117e5ac94ee3cc91c4597aa0db425e8e9c5e316b9fc019c%}', N'', N'', N'', N'', N'forgottenpassword')
INSERT INTO [CMS_EmailTemplate] ([EmailTemplateID], [EmailTemplateName], [EmailTemplateDisplayName], [EmailTemplateText], [EmailTemplateSiteID], [EmailTemplateGUID], [EmailTemplateLastModified], [EmailTemplatePlainText], [EmailTemplateSubject], [EmailTemplateFrom], [EmailTemplateCc], [EmailTemplateBcc], [EmailTemplateType]) VALUES (164, N'Registration.New', N'Membership - Notification - New registration', N'<html>
	<head>
	</head>
	<body style="font-size:12px; font-family: Arial">
		<p>
This is a notification that a new user has just registered:<br />
<br />
First name: {%firstname|(user)administrator|(hash)998d81dab6b7c37aa5cb71222ca28b587031d87d21d551ece5124d71cd3f0b3b%} <br />
<br />
Last name: {%lastname|(user)administrator|(hash)3ad2cd3e3f348a3a067a7f0e619e1feaa48311bd86528e7002f1b274a3f7feb2%}<br />
<br />
E-mail: {%email|(user)administrator|(hash)9f9aec987582acb781e33b9dcb2652019c289ff93253dbfff1ba9230f2032ed6%}<br />
<br />
User name: {%TrimSitePrefix(username)|(user)administrator|(hash)ae84cc044b37369545eb088351375699fb72d2742101a18cb3cc50597b49e736%}<br />
<br />
This e-mail is only for your information. No action is required.
<br />
</p>
</body>
</html>', NULL, '89e8fed9-3316-435e-8ea6-630e64de1a12', '20110905 17:42:27', N'This is a notification that a new user has just registered:
First name: {%firstname|(user)administrator|(hash)998d81dab6b7c37aa5cb71222ca28b587031d87d21d551ece5124d71cd3f0b3b%} 
Last name: {%lastname|(user)administrator|(hash)3ad2cd3e3f348a3a067a7f0e619e1feaa48311bd86528e7002f1b274a3f7feb2%}
E-mail: {%email|(user)administrator|(hash)9f9aec987582acb781e33b9dcb2652019c289ff93253dbfff1ba9230f2032ed6%}
User name: {%TrimSitePrefix(username)|(user)administrator|(hash)ae84cc044b37369545eb088351375699fb72d2742101a18cb3cc50597b49e736%}
This e-mail is only for your information. No action is required.', N'', N'', N'', N'', N'registration')
INSERT INTO [CMS_EmailTemplate] ([EmailTemplateID], [EmailTemplateName], [EmailTemplateDisplayName], [EmailTemplateText], [EmailTemplateSiteID], [EmailTemplateGUID], [EmailTemplateLastModified], [EmailTemplatePlainText], [EmailTemplateSubject], [EmailTemplateFrom], [EmailTemplateCc], [EmailTemplateBcc], [EmailTemplateType]) VALUES (163, N'Registration.Approve', N'Membership - Notification - Waiting for approval', N'<html>
	<head>
	</head>
	<body style="font-size:12px; font-family: Arial">
		<p>
This is a notification that a new user has just registered and waiting for your approval:
<br />
<br />
First name: {%firstname|(user)administrator|(hash)998d81dab6b7c37aa5cb71222ca28b587031d87d21d551ece5124d71cd3f0b3b%} <br />
<br />
Last name: {%lastname|(user)administrator|(hash)3ad2cd3e3f348a3a067a7f0e619e1feaa48311bd86528e7002f1b274a3f7feb2%}<br />
<br />
E-mail: {%email|(user)administrator|(hash)9f9aec987582acb781e33b9dcb2652019c289ff93253dbfff1ba9230f2032ed6%}<br />
<br />
User name: {%TrimSitePrefix(username)|(user)administrator|(hash)ae84cc044b37369545eb088351375699fb72d2742101a18cb3cc50597b49e736%}<br />
<br />
Please go to CMS Desk or CMS Site Manager -> Administration -> Users -> Waiting for approval and approve or reject the user.<br />
</p>
</body>
</html>', NULL, '8ee2a9d2-0ddd-4ae9-9066-f9edc6261938', '20110905 17:43:26', N'This is a notification that a new user has just registered and waiting for your approval: 
First name: {%firstname|(user)administrator|(hash)998d81dab6b7c37aa5cb71222ca28b587031d87d21d551ece5124d71cd3f0b3b%} 
Last name: {%lastname|(user)administrator|(hash)3ad2cd3e3f348a3a067a7f0e619e1feaa48311bd86528e7002f1b274a3f7feb2%}
E-mail: {%email|(user)administrator|(hash)9f9aec987582acb781e33b9dcb2652019c289ff93253dbfff1ba9230f2032ed6%}
User name: {%TrimSitePrefix(username)|(user)administrator|(hash)ae84cc044b37369545eb088351375699fb72d2742101a18cb3cc50597b49e736%}
Please go to CMS Desk or CMS Site Manager -> Administration -> Users -> Waiting for approval and approve or reject the user.', N'Registration notification - Waiting for approval', N'', N'', N'', N'registration')
INSERT INTO [CMS_EmailTemplate] ([EmailTemplateID], [EmailTemplateName], [EmailTemplateDisplayName], [EmailTemplateText], [EmailTemplateSiteID], [EmailTemplateGUID], [EmailTemplateLastModified], [EmailTemplatePlainText], [EmailTemplateSubject], [EmailTemplateFrom], [EmailTemplateCc], [EmailTemplateBcc], [EmailTemplateType]) VALUES (766, N'Membership.Registration', N'Membership - Registration', N'<html>
	<head>
	</head>
	<body>
		Thank you for registering at our site. You can find your credentials below:<br />
		<br />
		Username: {%TrimSitePrefix(username)%}<br />
		Password: {%password%}<br />
	</body>
</html>', NULL, 'dcac774e-5ddb-4645-9704-cdadb8eda10a', '20120209 15:57:25', N'Thank you for registering at our site. You can find your credentials below:
Username: {%TrimSitePrefix(username)%}
Password: {%password%}', N'Registration information', N'', N'', N'', N'membershipregistration')
INSERT INTO [CMS_EmailTemplate] ([EmailTemplateID], [EmailTemplateName], [EmailTemplateDisplayName], [EmailTemplateText], [EmailTemplateSiteID], [EmailTemplateGUID], [EmailTemplateLastModified], [EmailTemplatePlainText], [EmailTemplateSubject], [EmailTemplateFrom], [EmailTemplateCc], [EmailTemplateBcc], [EmailTemplateType]) VALUES (750, N'RegistrationUserApproved', N'Membership - Registration approved', N'<html>
	<head>
	</head>
	<body>
		Your registration has been approved by administrator. Now you can sign in using your username and password.  <br />
<br />
<a href="{%homepageurl%}">Click here to navigate to the web site</a>
	</body>
</html>', NULL, '017c6bf7-16bf-4c65-ac16-03b5fa8ed5b7', '20090113 16:55:10', N'Your registration has been approved by administrator. Now you can sign in using your username and password. 
Click to the following link to navigate to the website:
{%homepageurl%}', N'', N'', N'', N'', N'registrationapproval')
INSERT INTO [CMS_EmailTemplate] ([EmailTemplateID], [EmailTemplateName], [EmailTemplateDisplayName], [EmailTemplateText], [EmailTemplateSiteID], [EmailTemplateGUID], [EmailTemplateLastModified], [EmailTemplatePlainText], [EmailTemplateSubject], [EmailTemplateFrom], [EmailTemplateCc], [EmailTemplateBcc], [EmailTemplateType]) VALUES (739, N'RegistrationConfirmation', N'Membership - Registration confirmation', N'<html>
	<head>
	</head>
	<body>
		Thank you for registering at our site. Please click the link below to complete your registration:  <br />
<a href="{%confirmaddress|(user)administrator|(hash)c549f9374a2480bfc11f07d26c3aa47e6cfbb34bd470cf380f0918806960c1cc%}">{%confirmaddress|(user)administrator|(hash)c549f9374a2480bfc11f07d26c3aa47e6cfbb34bd470cf380f0918806960c1cc%}</a>
<br />
<br />
You can find your credentials below:<br />
		Username: {%TrimSitePrefix(username)|(user)administrator|(hash)ae84cc044b37369545eb088351375699fb72d2742101a18cb3cc50597b49e736%}<br />
		Password: {%password|(user)administrator|(hash)3fa3e7dfc7ed2fd25117e5ac94ee3cc91c4597aa0db425e8e9c5e316b9fc019c%}<br />
	</body>
</html>', NULL, '11e9d672-0fbc-46ae-8f0d-6cf8001578ce', '20110905 17:45:48', N'Thank you for registering at our site. Please click the link below to complete your registration:
{%confirmaddress|(user)administrator|(hash)c549f9374a2480bfc11f07d26c3aa47e6cfbb34bd470cf380f0918806960c1cc%}
You can find your credentials below:
Username: {%TrimSitePrefix(username)|(user)administrator|(hash)ae84cc044b37369545eb088351375699fb72d2742101a18cb3cc50597b49e736%}
Password: {%password|(user)administrator|(hash)3fa3e7dfc7ed2fd25117e5ac94ee3cc91c4597aa0db425e8e9c5e316b9fc019c%}', N'Confirm your registration', N'', N'', N'', N'membershipregistration')
INSERT INTO [CMS_EmailTemplate] ([EmailTemplateID], [EmailTemplateName], [EmailTemplateDisplayName], [EmailTemplateText], [EmailTemplateSiteID], [EmailTemplateGUID], [EmailTemplateLastModified], [EmailTemplatePlainText], [EmailTemplateSubject], [EmailTemplateFrom], [EmailTemplateCc], [EmailTemplateBcc], [EmailTemplateType]) VALUES (767, N'Membership.RegistrationWaitingForApproval', N'Membership - Registration waiting for approval', N'<html>
	<head>
	</head>
	<body>
		Thank you for registering at our site&nbsp;{%currentsite.sitename|(user)administrator|(hash)707b696834aa327b3d335291104fec4b95f171ac4ded10f4d2249057bbb7382a%}. Your registration must be approved by administrator.<br />
		<br />
		<br />
		Registration details:<br />
		<br />
		Username: {%TrimSitePrefix(username)|(user)administrator|(hash)ae84cc044b37369545eb088351375699fb72d2742101a18cb3cc50597b49e736%}<br />
		Password: {%password|(user)administrator|(hash)3fa3e7dfc7ed2fd25117e5ac94ee3cc91c4597aa0db425e8e9c5e316b9fc019c%}<br />
	</body>
</html>', NULL, '132be2c1-2db6-4c7d-8165-40041f2e4938', '20110905 17:47:01', N'Thank you for registering at our site {%currentsite.sitename|(user)administrator|(hash)707b696834aa327b3d335291104fec4b95f171ac4ded10f4d2249057bbb7382a%}. Your registration must be approved by administrator.
Registration details:
Username: {%TrimSitePrefix(username)|(user)administrator|(hash)ae84cc044b37369545eb088351375699fb72d2742101a18cb3cc50597b49e736%}
Password: {%password|(user)administrator|(hash)3fa3e7dfc7ed2fd25117e5ac94ee3cc91c4597aa0db425e8e9c5e316b9fc019c%}', N'', N'', N'', N'', N'membershipregistration')
INSERT INTO [CMS_EmailTemplate] ([EmailTemplateID], [EmailTemplateName], [EmailTemplateDisplayName], [EmailTemplateText], [EmailTemplateSiteID], [EmailTemplateGUID], [EmailTemplateLastModified], [EmailTemplatePlainText], [EmailTemplateSubject], [EmailTemplateFrom], [EmailTemplateCc], [EmailTemplateBcc], [EmailTemplateType]) VALUES (737, N'Membership.ResendPassword', N'Membership - Resend password', N'<html>
<head>
</head>
<body>
<p>You have requested the current password information.</p>
<p>Your user name and current password are:</p>
<p><strong>User name:</strong> {%TrimSitePrefix(UserName)|(user)administrator|(hash)ae84cc044b37369545eb088351375699fb72d2742101a18cb3cc50597b49e736%}<br />
<strong>Password:</strong> {%Password|(user)administrator|(hash)3fa3e7dfc7ed2fd25117e5ac94ee3cc91c4597aa0db425e8e9c5e316b9fc019c%}</p>
</body>
</html>', NULL, '30db61b9-2737-4e8e-90b0-0ec304baffda', '20110905 17:48:59', N'You have requested the current password information.
Your user name and current password are:
User name: {%TrimSitePrefix(UserName)|(user)administrator|(hash)ae84cc044b37369545eb088351375699fb72d2742101a18cb3cc50597b49e736%}
Password: {%Password|(user)administrator|(hash)3fa3e7dfc7ed2fd25117e5ac94ee3cc91c4597aa0db425e8e9c5e316b9fc019c%}', N'', N'', N'', N'', N'password')
INSERT INTO [CMS_EmailTemplate] ([EmailTemplateID], [EmailTemplateName], [EmailTemplateDisplayName], [EmailTemplateText], [EmailTemplateSiteID], [EmailTemplateGUID], [EmailTemplateLastModified], [EmailTemplatePlainText], [EmailTemplateSubject], [EmailTemplateFrom], [EmailTemplateCc], [EmailTemplateBcc], [EmailTemplateType]) VALUES (719, N'messaging.messagenotification', N'Messaging  - Notification e-mail', N'<html>
	<head>
	</head>
	<body style="font-size: 12px; font-family: arial">
<p>
Hello {%TrimSitePrefix(Recipient.UserName)|(user)administrator|(hash)518b570d83ce3b3fe52cbc10fe7afdae3db381ff134c438dca6ebd9d955c00a9%},
<br />
you''ve just recieved new message from user ''{%TrimSitePrefix(Sender.UserName)|(user)administrator|(hash)7bb3a85cf09be462831a747a54a53bdd2a49dd7ad63cd53af2552b67ac99800d%}''.
<br />
Original message:
<br />
<hr />
<br />
{%Message.MessageBody|(resolvebbcode)|(user)administrator|(hash)b7f4eb3df983fa34e867c08038952ef9582cf6cebf335deaeeac6b931d3a2a61%}
<br/>
<hr/>
</p>
</body>
</html>', NULL, '3d863d80-a3ab-46d8-99c5-1bcd9c2bd570', '20110905 17:49:41', N'Hello {%TrimSitePrefix(Recipient.UserName)|(user)administrator|(hash)518b570d83ce3b3fe52cbc10fe7afdae3db381ff134c438dca6ebd9d955c00a9%}, 
you''ve just recieved new message from user ''{%TrimSitePrefix(Sender.UserName)|(user)administrator|(hash)7bb3a85cf09be462831a747a54a53bdd2a49dd7ad63cd53af2552b67ac99800d%}''. 
Original message: 
--------------------------------------------------------------------------------
{%Message.MessageBody|(resolvebbcode)|(user)administrator|(hash)b7f4eb3df983fa34e867c08038952ef9582cf6cebf335deaeeac6b931d3a2a61%} 
--------------------------------------------------------------------------------', N'', N'', N'', N'', N'messaging')
INSERT INTO [CMS_EmailTemplate] ([EmailTemplateID], [EmailTemplateName], [EmailTemplateDisplayName], [EmailTemplateText], [EmailTemplateSiteID], [EmailTemplateGUID], [EmailTemplateLastModified], [EmailTemplatePlainText], [EmailTemplateSubject], [EmailTemplateFrom], [EmailTemplateCc], [EmailTemplateBcc], [EmailTemplateType]) VALUES (762, N'newsletter.unsubscriptionrequest', N'Newsletters - Unsubscription request', N'<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN"
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
  <head>
    <title>Newsletter</title>
    <meta http-equiv="content-type"
    content="text/html; charset=UTF-8" />
    <style type="text/css">
		h1,h2,h3,h4,h5 { 
			color: #2a537e;
			font-family: Arial;
		} 
		p {
			font-family: Arial;
		} 
		h1 {
			font-size: 16px;
			color: #2a537e;
		}
		h2 {
			font-size: 14px;
		}
		h3 {
			font-size: 12px;
		}
		a {
			color: #000000;
			text-decoration: underline;
		}
		img {
			border: 0;
			padding:0;
			margin: 0;
		}
		body {
			font-family: Arial;
			font-size: 12px;
		}
		#page {
			margin: auto; width: 700px;
		}
	</style>
  </head>
  <body>
    <div id="page">
      <table border="0" cellpadding="0" cellspacing="0" style="width: 696px;">
        <tbody>
          <tr>
            <td>
              <img alt="Company Logo" src="~/App_Themes/CorporateSite/Images/newsletterLogo.png" />
            </td>
          </tr>
          <tr>
            <td style="padding: 20px 0px; text-align: justify;">
            <h1>You have requested unsubscription from our newsletter - {%NewsletterDisplayName|(user)administrator|(hash)3d4e9e15e4c0ee6eaaf80bfc00ed81ee7594a4739bf5d94bc8251c7c84426d67%}.</h1>
            <br />If you would like to unsubscribe please use the following link {%UnsubscribeLink%}</td>
          </tr>
          <tr>
            <td style="border-top: 1px solid rgb(34, 34, 34); padding: 3px 0px;">
              <font color="#222222" size="1">Company, address, state&#160; All rights reserved.</font>
            </td>
          </tr>
        </tbody>
      </table>
    </div>
  </body>
</html>', NULL, '0e0cbe1c-7349-426a-9937-dd84a20496c2', '20110907 17:20:31', N'You have requested unsubscription from our newsletter - {%NewsletterDisplayName|(user)administrator|(hash)3d4e9e15e4c0ee6eaaf80bfc00ed81ee7594a4739bf5d94bc8251c7c84426d67%}.
If you would like to unsubscribe please use the following link {%UnsubscribeLink%} 
Company, address, state  All rights reserved.', N'Unsubscription request', N'', N'', N'', N'newsletter')
INSERT INTO [CMS_EmailTemplate] ([EmailTemplateID], [EmailTemplateName], [EmailTemplateDisplayName], [EmailTemplateText], [EmailTemplateSiteID], [EmailTemplateGUID], [EmailTemplateLastModified], [EmailTemplatePlainText], [EmailTemplateSubject], [EmailTemplateFrom], [EmailTemplateCc], [EmailTemplateBcc], [EmailTemplateType]) VALUES (787, N'ProjectManagement.ChangedTask', N'Project Management - Changed task', N'<html>
  <head>
  <style type="text/css">
    table{      
      border-collapse:collapse;
      width : 600px;
    }
    td{      
      border: solid 1px black;
    }
    .firstColumn{
      width:20%;
    }
  </style>
  </head>  
       <body>
      <table cellspacing="0" cellpadding= "3"   >
        <tr>
                 <td class="firstColumn">
              {$pm.projecttask.id$}: 
                 </td>
           <td>
      {%ProjectTask.ProjectTaskID|(user)administrator|(hash)1d1e4fdc178119500935821b0542a2372dc8f0e9c4d331675ed57aeefe04541f%}
     </td> 
              </tr>  
        <tr>
                 <td>
              {$pm.projecttask.taskname$}:  
                 </td>
           <td>
      {%ProjectTask.ProjectTaskDisplayName|(user)administrator|(hash)58a0ea820555c81264cc7982f1cbd14a1311e560afa077d4891cbc3ae7a4de0e%}
     </td> 
              </tr>
        <tr>
                 <td>
              {$pm.project.projectname$}:  
                 </td>
           <td>
      {%Project.ProjectDisplayName|(user)administrator|(hash)98aecd455d968e09b8b6edd3de9b4f477b578fa8d0562bb816f4546617f719d2%}
     </td> 
              </tr>
        <tr>
                 <td>
              {$general.status$}:  
                 </td>
           <td>
      {%ProjectTaskStatus.TaskStatusDisplayName|(user)administrator|(hash)5bfdb636dd99f189d1f3beced37ad52da3eca8fbbfe2348e7ec7835b5a0c51ef%}
     </td> 
              </tr>
        <tr>
                 <td>
              {$pm.projecttask.owner$}:  
                 </td>
           <td>
       {%Owner.FullName|(user)administrator|(hash)e23357f15915b833f56c3b69673621b402c2a3887da4661394a2c128d9ffb249%} ({%TrimSitePrefix(Owner.UserName)|(user)administrator|(hash)303982e4d914e49dcd54a030167883e2dea754b91b9fed81d0dce7e1e13bd6f8%})
     </td> 
              </tr>
        <tr>
                 <td>
              {$pm.projecttask.asignee$}:  
                 </td>
           <td>
      {%Assignee.FullName|(user)administrator|(hash)7aa7889ed6069cbdb821ac406ba7a5e536814380ae4585ec79ebf40d62517ee9%} ({%TrimSitePrefix(Assignee.UserName)|(user)administrator|(hash)3816a546893446d9749b295f8639048e6d4a20bb9acf2d6be9578ffcfa74aabc%})
     </td> 
              </tr>
        <tr>
                 <td>
              {$pm.project.deadline$}: 
                 </td>
           <td>
      {%ProjectTask.ProjectTaskDeadline|(user)administrator|(hash)c322d6426b3c13d6fe4786d0009c22e816e4a158f02935010439d3f66eb78acc%}
     </td> 
              </tr>
        <tr>
                 <td>
              {$general.Link$}:  
                 </td>
           <td>
      <a href="{%TaskURL%}">{%ProjectTask.ProjectTaskDisplayName|(user)administrator|(hash)58a0ea820555c81264cc7982f1cbd14a1311e560afa077d4891cbc3ae7a4de0e%}</a>
     </td> 
              </tr>
        <tr>
                 <td>
              {$pm.projecttask.description$}:
                 </td>
           <td>
      {%ProjectTask.ProjectTaskDescription|(encode)false|(user)administrator|(hash)c0f6dc7723fac01dcb5d852edf9ab9db2d38579ef5649812efffb9e34fb67714%} 
     </td> 
              </tr>
          </table>      
  </body>
</html>', NULL, '2bf93fbb-c906-42c2-ab23-534ead0c36ad', '20111005 17:04:58', N'{$pm.projecttask.id$}: {%ProjectTask.ProjectTaskID|(user)administrator|(hash)1d1e4fdc178119500935821b0542a2372dc8f0e9c4d331675ed57aeefe04541f%}
{$pm.projecttask.taskname$}: {%ProjectTask.ProjectTaskDisplayName|(user)administrator|(hash)58a0ea820555c81264cc7982f1cbd14a1311e560afa077d4891cbc3ae7a4de0e%}
{$pm.project.projectname$}: {%Project.ProjectDisplayName|(user)administrator|(hash)98aecd455d968e09b8b6edd3de9b4f477b578fa8d0562bb816f4546617f719d2%}
{$general.status$}: {%ProjectTaskStatus.TaskStatusDisplayName|(user)administrator|(hash)5bfdb636dd99f189d1f3beced37ad52da3eca8fbbfe2348e7ec7835b5a0c51ef%}
{$pm.projecttask.owner$}: {%Owner.FullName|(user)administrator|(hash)e23357f15915b833f56c3b69673621b402c2a3887da4661394a2c128d9ffb249%} ({%TrimSitePrefix(Owner.UserName)|(user)administrator|(hash)303982e4d914e49dcd54a030167883e2dea754b91b9fed81d0dce7e1e13bd6f8%})
{$pm.projecttask.asignee$}: {%Assignee.FullName|(user)administrator|(hash)7aa7889ed6069cbdb821ac406ba7a5e536814380ae4585ec79ebf40d62517ee9%} ({%TrimSitePrefix(Assignee.UserName)|(user)administrator|(hash)3816a546893446d9749b295f8639048e6d4a20bb9acf2d6be9578ffcfa74aabc%})
{$pm.project.deadline$}: {%ProjectTask.ProjectTaskDeadline|(user)administrator|(hash)c322d6426b3c13d6fe4786d0009c22e816e4a158f02935010439d3f66eb78acc%}
{$general.Link$}: {%TaskURL%}
{$pm.projecttask.description$}: {%ProjectTaskDescriptionPlain%}', N'Project manager notification - task changed', N'', N'', N'', N'projectmanagement')
INSERT INTO [CMS_EmailTemplate] ([EmailTemplateID], [EmailTemplateName], [EmailTemplateDisplayName], [EmailTemplateText], [EmailTemplateSiteID], [EmailTemplateGUID], [EmailTemplateLastModified], [EmailTemplatePlainText], [EmailTemplateSubject], [EmailTemplateFrom], [EmailTemplateCc], [EmailTemplateBcc], [EmailTemplateType]) VALUES (788, N'ProjectManagement.NewTask', N'Project Management - New task', N'<html>
  <head>
  <style type="text/css">
    table{      
      border-collapse:collapse;
      width : 600px;
    }
    td{      
      border: solid 1px black;
    }
    .firstColumn{
      width:20%;
    }
  </style>
  </head>  
       <body>
      <table cellspacing="0" cellpadding= "3"   >
        <tr>
                 <td class="firstColumn">
              {$pm.projecttask.id$}: 
                 </td>
           <td>
      {%ProjectTask.ProjectTaskID|(user)administrator|(hash)1d1e4fdc178119500935821b0542a2372dc8f0e9c4d331675ed57aeefe04541f%}
     </td> 
              </tr>  
        <tr>
                 <td>
              {$pm.projecttask.taskname$}:  
                 </td>
           <td>
      {%ProjectTask.ProjectTaskDisplayName|(user)administrator|(hash)58a0ea820555c81264cc7982f1cbd14a1311e560afa077d4891cbc3ae7a4de0e%}
     </td> 
              </tr>
        <tr>
                 <td>
              {$pm.project.projectname$}:  
                 </td>
           <td>
      {%Project.ProjectDisplayName|(user)administrator|(hash)98aecd455d968e09b8b6edd3de9b4f477b578fa8d0562bb816f4546617f719d2%}
     </td> 
              </tr>
        <tr>
                 <td>
              {$general.status$}:  
                 </td>
           <td>
      {%ProjectTaskStatus.TaskStatusDisplayName|(user)administrator|(hash)5bfdb636dd99f189d1f3beced37ad52da3eca8fbbfe2348e7ec7835b5a0c51ef%}
     </td> 
              </tr>
        <tr>
                 <td>
              {$pm.projecttask.owner$}:  
                 </td>
           <td>
       {%Owner.FullName|(user)administrator|(hash)e23357f15915b833f56c3b69673621b402c2a3887da4661394a2c128d9ffb249%} ({%TrimSitePrefix(Owner.UserName)|(user)administrator|(hash)303982e4d914e49dcd54a030167883e2dea754b91b9fed81d0dce7e1e13bd6f8%})
     </td> 
              </tr>
        <tr>
                 <td>
              {$pm.projecttask.asignee$}:  
                 </td>
           <td>
      {%Assignee.FullName|(user)administrator|(hash)7aa7889ed6069cbdb821ac406ba7a5e536814380ae4585ec79ebf40d62517ee9%} ({%TrimSitePrefix(Assignee.UserName)|(user)administrator|(hash)3816a546893446d9749b295f8639048e6d4a20bb9acf2d6be9578ffcfa74aabc%})
     </td> 
              </tr>
        <tr>
                 <td>
              {$pm.project.deadline$}: 
                 </td>
           <td>
      {%ProjectTask.ProjectTaskDeadline|(user)administrator|(hash)c322d6426b3c13d6fe4786d0009c22e816e4a158f02935010439d3f66eb78acc%}
     </td> 
              </tr>
        <tr>
                 <td>
              {$general.Link$}:  
                 </td>
           <td>
      <a href="{%TaskURL|(user)administrator|(hash)60bdefe9971d232f1dd04d8e8b20dbda91ef0e869485b9f84efc82da437a3eac%}">{%ProjectTask.ProjectTaskDisplayName|(user)administrator|(hash)58a0ea820555c81264cc7982f1cbd14a1311e560afa077d4891cbc3ae7a4de0e%}</a>
     </td> 
              </tr>
        <tr>
                 <td>
              {$pm.projecttask.description$}:
                 </td>
           <td>
      {%ProjectTask.ProjectTaskDescription|(encode)false|(user)administrator|(hash)c0f6dc7723fac01dcb5d852edf9ab9db2d38579ef5649812efffb9e34fb67714%} 
     </td> 
              </tr>
          </table>      
  </body>
</html>', NULL, 'aed39df2-ba19-4042-adc7-c9850268f8d7', '20110905 17:52:25', N'{$pm.projecttask.id$}: {%ProjectTask.ProjectTaskID|(user)administrator|(hash)1d1e4fdc178119500935821b0542a2372dc8f0e9c4d331675ed57aeefe04541f%}
{$pm.projecttask.taskname$}: {%ProjectTask.ProjectTaskDisplayName|(user)administrator|(hash)58a0ea820555c81264cc7982f1cbd14a1311e560afa077d4891cbc3ae7a4de0e%}
{$pm.project.projectname$}: {%Project.ProjectDisplayName|(user)administrator|(hash)98aecd455d968e09b8b6edd3de9b4f477b578fa8d0562bb816f4546617f719d2%}
{$general.status$}: {%ProjectStatus.TaskStatusDisplayName|(user)administrator|(hash)bfefc375bf6cab9d34542e6d3a857b11f01e415ed855e115e94840449538db1c%}
{$pm.projecttask.owner$}: {%Owner.FullName|(user)administrator|(hash)e23357f15915b833f56c3b69673621b402c2a3887da4661394a2c128d9ffb249%} ({%TrimSitePrefix(Owner.UserName)|(user)administrator|(hash)303982e4d914e49dcd54a030167883e2dea754b91b9fed81d0dce7e1e13bd6f8%})
{$pm.projecttask.asignee$}: {%Assignee.FullName|(user)administrator|(hash)7aa7889ed6069cbdb821ac406ba7a5e536814380ae4585ec79ebf40d62517ee9%} ({%TrimSitePrefix(Assignee.UserName)|(user)administrator|(hash)3816a546893446d9749b295f8639048e6d4a20bb9acf2d6be9578ffcfa74aabc%})
{$pm.project.deadline$}: {%ProjectTask.ProjectTaskDeadline|(user)administrator|(hash)c322d6426b3c13d6fe4786d0009c22e816e4a158f02935010439d3f66eb78acc%}
{$general.Link$}: {%TaskURL|(user)administrator|(hash)60bdefe9971d232f1dd04d8e8b20dbda91ef0e869485b9f84efc82da437a3eac%}
{$pm.projecttask.description$}: {%ProjectTaskDescriptionPlain|(user)administrator|(hash)7df124fe6b8a3004c2f7cae401d86640a8745988b6a73fc06ff503cd50e9ab03%}', N'Project manager notification - task created', N'', N'', N'', N'projectmanagement')
INSERT INTO [CMS_EmailTemplate] ([EmailTemplateID], [EmailTemplateName], [EmailTemplateDisplayName], [EmailTemplateText], [EmailTemplateSiteID], [EmailTemplateGUID], [EmailTemplateLastModified], [EmailTemplatePlainText], [EmailTemplateSubject], [EmailTemplateFrom], [EmailTemplateCc], [EmailTemplateBcc], [EmailTemplateType]) VALUES (789, N'ProjectManagement.OverdueTask', N'Project Management - Overdue task', N'<html>
  <head>
  <style type="text/css">
    table{      
      border-collapse:collapse;
      width : 600px;
    }
    td{      
      border: solid 1px black;
    }
    .firstColumn{
      width:20%;
    }
  </style>
  </head>  
       <body>
  <p>
    This is an automatically generated task overdue notification. See task details below.
  </p>
      <table cellspacing="0" cellpadding= "3"   >
        <tr>
                 <td>
              {$pm.projecttask.taskname$}:  
                 </td>
           <td>
      {%ProjectTask.ProjectTaskDisplayName%}
     </td> 
              </tr>
       
        <tr>
                 <td>
              {$pm.project.deadline$}:  
                 </td>
           <td>
      {%ProjectTask.ProjectTaskDeadline%}
     </td> 
              </tr>
        <tr>
                 <td>
              {$general.Link$}:  
                 </td>
           <td>
      <a href="{%TaskURL%}">{%ProjectTask.ProjectTaskDisplayName%}</a>
     </td> 
              </tr>
          </table>      
  </body>
</html>', NULL, '71e2cb67-4866-4f61-a2b1-daeaf4cb3b6a', '20110818 14:25:39', N'This is an automatically generated task overdue notification. See task details below.
{$pm.projecttask.taskname$}: {%ProjectTask.ProjectTaskDisplayName%}
{$pm.project.deadline$}: {%ProjectTask.ProjectTaskDeadline%}
{$general.Link$}: {%TaskURL%}', N'Project manager notification - overdue task', N'', N'', N'', N'projectmanagement')
INSERT INTO [CMS_EmailTemplate] ([EmailTemplateID], [EmailTemplateName], [EmailTemplateDisplayName], [EmailTemplateText], [EmailTemplateSiteID], [EmailTemplateGUID], [EmailTemplateLastModified], [EmailTemplatePlainText], [EmailTemplateSubject], [EmailTemplateFrom], [EmailTemplateCc], [EmailTemplateBcc], [EmailTemplateType]) VALUES (790, N'ProjectManagement.TaskReminder', N'Project Management - Task reminder', N'<html>
  <head>
  </head>  
       <body>
    <p>
      This is a reminder message related to task <strong>{%ProjectTask.ProjectTaskDisplayName|(user)administrator|(hash)58a0ea820555c81264cc7982f1cbd14a1311e560afa077d4891cbc3ae7a4de0e%}</strong>, sent by {%currentuser.fullname|(user)administrator|(hash)4534e9e346638dd9be14873ba6889899b89e30784965421c82b857da4505e142%} ({%TrimSitePrefix(currentuser.username)|(user)administrator|(hash)e4dd8de0cb817f62c7a2179b15c497e5bfa02387bb9c3745bcad6ac137c79206%}).
    </p>
    <p>
      {%ReminderMessage|(encode)false|(user)administrator|(hash)b3a985ed76577724b34e74acd2b8c6eb4a03611e65729697730469aa4dba4c8a%}
    </p>
    <hr />
    <p>
      Click here <a href="{%TaskURL|(user)administrator|(hash)60bdefe9971d232f1dd04d8e8b20dbda91ef0e869485b9f84efc82da437a3eac%}">{%ProjectTask.ProjectTaskDisplayName|(user)administrator|(hash)58a0ea820555c81264cc7982f1cbd14a1311e560afa077d4891cbc3ae7a4de0e%}</a> to open the task.
    </p>
  </body>
</html>', NULL, '23eedb5a-1ae6-4a4a-8a34-139eb2b9c54d', '20110905 17:53:17', N'This is a reminder message related to task {%ProjectTask.ProjectTaskDisplayName|(user)administrator|(hash)58a0ea820555c81264cc7982f1cbd14a1311e560afa077d4891cbc3ae7a4de0e%}({%TaskURL|(user)administrator|(hash)60bdefe9971d232f1dd04d8e8b20dbda91ef0e869485b9f84efc82da437a3eac%}), sent by {%currentuser.fullname|(user)administrator|(hash)4534e9e346638dd9be14873ba6889899b89e30784965421c82b857da4505e142%} ({%TrimSitePrefix(currentuser.username)|(user)administrator|(hash)e4dd8de0cb817f62c7a2179b15c497e5bfa02387bb9c3745bcad6ac137c79206%}).
{%ReminderMessagePlain|(user)administrator|(hash)8c36103e505838efd34692108f2b144b56d9ee736e10975b7a63fef1305d7ec2%}', N'Project manager - Reminder', N'', N'', N'', N'projectmanagement')
INSERT INTO [CMS_EmailTemplate] ([EmailTemplateID], [EmailTemplateName], [EmailTemplateDisplayName], [EmailTemplateText], [EmailTemplateSiteID], [EmailTemplateGUID], [EmailTemplateLastModified], [EmailTemplatePlainText], [EmailTemplateSubject], [EmailTemplateFrom], [EmailTemplateCc], [EmailTemplateBcc], [EmailTemplateType]) VALUES (852, N'scoring.notification', N'Scoring - Notification e-mail', N'<html>
  <head>
  </head>
  <body style="font-size: 12px; font-family: arial">
    <p>
    This is an automatic notification sent by Kentico CMS.
    </p>
    <p>
    Contact {%Contact.ContactFirstName|(user)administrator|(hash)3f518c63e916841549b6e4d2b97ee2fd7c40e0384fc1abf502441356d880d5d6%} {%Contact.ContactLastName|(user)administrator|(hash)087a76c72de870c0b15c9f2ec2772d58b6ad3b6598cb8477cec3cdc3aa36259d%} has reached limit in score ''{%Score.ScoreDisplayName|(user)administrator|(hash)09482339456ce3b7320d6ab443a4f23eed958245bba16625e643930d0530404d%}''.
    </p>
    <p>
    Contact''s current score is {%ScoreValue|(user)administrator|(hash)a5d80558851b95f4dfce35c43ea1cfc2887b4b1dd4a3ac314cd3671c562a54d1%}.
    </p>
  </body>
</html>', NULL, '5aea940d-ade5-4419-9f21-4744db45e917', '20110902 15:30:53', N'This is an automatic notification sent by Kentico CMS.
Contact {%Contact.ContactFirstName|(user)administrator|(hash)3f518c63e916841549b6e4d2b97ee2fd7c40e0384fc1abf502441356d880d5d6%} {%Contact.ContactLastName|(user)administrator|(hash)087a76c72de870c0b15c9f2ec2772d58b6ad3b6598cb8477cec3cdc3aa36259d%} has reached limit in score ''{%Score.ScoreDisplayName|(user)administrator|(hash)09482339456ce3b7320d6ab443a4f23eed958245bba16625e643930d0530404d%}''.
Contact''s current score is {%ScoreValue|(user)administrator|(hash)a5d80558851b95f4dfce35c43ea1cfc2887b4b1dd4a3ac314cd3671c562a54d1%}.', N'', N'', N'', N'', N'scoring')
INSERT INTO [CMS_EmailTemplate] ([EmailTemplateID], [EmailTemplateName], [EmailTemplateDisplayName], [EmailTemplateText], [EmailTemplateSiteID], [EmailTemplateGUID], [EmailTemplateLastModified], [EmailTemplatePlainText], [EmailTemplateSubject], [EmailTemplateFrom], [EmailTemplateCc], [EmailTemplateBcc], [EmailTemplateType]) VALUES (160, N'Workflow.Approved', N'Workflow - Document approved', N'<html>
  <head>
  </head>
  <body style="font-size: 12px; font-family: arial">
    <p>
    This is an automatic notification sent by Kentico CMS. The following document was approved.
    </p>
    <p>
    <strong>Document:</strong> <a href="{%DocumentEditUrl%}">{%documentname%}</a> {% ifEmpty(DocumentPreviewUrl, "", "(<a href=\"" + DocumentPreviewUrl + "\">preview</a>)")|(encode)false%}
    <br />
    <br />
    <strong>Approved by:</strong> {%ApprovedBy%}
    <br />
    <strong>Approved when:</strong> {%ApprovedWhen%}
    <br />
    <strong>Original step:</strong> {%originalstepname%}
    <br />
    <strong>Current step:</strong> {%currentstepname%}
    <br />
    <strong>Comment:</strong>
    <br />
    {%Comment%}
    </p>
  </body>
</html>', NULL, '5d4c7b49-0a86-457e-b39c-79be2cc48173', '20110922 10:44:54', N'This is an automatic notification sent by Kentico CMS. The following document was approved. 
Document: [url={%DocumentEditUrl%}]{%documentname%}[/url] {% ifEmpty(DocumentPreviewUrl, "", "([url=" + DocumentPreviewUrl + "]preview[/url])")|(user)administrator|(hash)67accc8d292516c257ff64b3ff2904914102e302c8b08b8e2c6be15daad7421f%}
Approved by: {%approvedby%} 
Approved when: {%approvedwhen%} 
Original step: {%originalstepname%} 
Current step: {%currentstepname%} 
Comment: 
{%comment%}', N'', N'', N'', N'', N'workflow')
INSERT INTO [CMS_EmailTemplate] ([EmailTemplateID], [EmailTemplateName], [EmailTemplateDisplayName], [EmailTemplateText], [EmailTemplateSiteID], [EmailTemplateGUID], [EmailTemplateLastModified], [EmailTemplatePlainText], [EmailTemplateSubject], [EmailTemplateFrom], [EmailTemplateCc], [EmailTemplateBcc], [EmailTemplateType]) VALUES (158, N'Workflow.Archived', N'Workflow - Document archived', N'<html>
  <head>
  </head>
  <body style="font-size: 12px; font-family: arial">
    <p>
    This is an automatic notification sent by Kentico CMS. The following document has been archived.</p>
    <p>
    <strong>Document:</strong> <a href="{%DocumentEditUrl%}">{%documentname%}</a> {% ifEmpty(DocumentPreviewUrl, "", "(<a href=\"" + DocumentPreviewUrl + "\">preview</a>)")|(encode)false%}
    <br />
    <br />
    <strong>Archived by:</strong> {%approvedby%}
    <br />
    <strong>Archived when:</strong> {%approvedwhen%}
    <br />
    <strong>Comment:</strong>
    <br />
    {%comment%}
    </p>
  </body>
</html>', NULL, '53d086cb-dc0c-4e5a-b48d-77a6b58fd549', '20110922 10:45:11', N'This is an automatic notification sent by Kentico CMS. The following document has been archived.
Document: [url={%DocumentEditUrl%}]{%documentname%}[/url] {% ifEmpty(DocumentPreviewUrl, "", "([url=" + DocumentPreviewUrl + "]preview[/url])")|(user)administrator|(hash)67accc8d292516c257ff64b3ff2904914102e302c8b08b8e2c6be15daad7421f%}
Archived by: {%approvedby%}
Archived when: {%approvedwhen%}
Comment: 
{%comment%}', N'', N'', N'', N'', N'workflow')
INSERT INTO [CMS_EmailTemplate] ([EmailTemplateID], [EmailTemplateName], [EmailTemplateDisplayName], [EmailTemplateText], [EmailTemplateSiteID], [EmailTemplateGUID], [EmailTemplateLastModified], [EmailTemplatePlainText], [EmailTemplateSubject], [EmailTemplateFrom], [EmailTemplateCc], [EmailTemplateBcc], [EmailTemplateType]) VALUES (159, N'Workflow.Published', N'Workflow - Document published', N'<html>
  <head>
  </head>
  <body style="font-size: 12px; font-family: arial">
    <p>
    This is an automatic notification sent by Kentico CMS. The following document was published.
    </p>
    <p>
    <strong>Document:</strong> <a href="{%DocumentEditUrl%}">{%documentname%}</a> {% ifEmpty(DocumentPreviewUrl, "", "(<a href=\"" + DocumentPreviewUrl + "\">preview</a>)")|(encode)false%}
    <br />
    <br />
    <strong>Last approved by:</strong> {%approvedby%}
    <br />
    <strong>Last approved when:</strong> {%approvedwhen%}
    <br />
    <strong>Original step:</strong> {%originalstepname%}
    <br />
    <strong>Comment:</strong>
    <br />
    {%comment%}
    </p>
  </body>
</html>', NULL, 'd2c5a1b0-c434-4427-ab81-22c0ca8f2313', '20110922 10:46:14', N'This is an automatic notification sent by Kentico CMS. The following document was published. 
Document: [url={%DocumentEditUrl%}]{%documentname%}[/url] {% ifEmpty(DocumentPreviewUrl, "", "([url=" + DocumentPreviewUrl + "]preview[/url])")|(user)administrator|(hash)67accc8d292516c257ff64b3ff2904914102e302c8b08b8e2c6be15daad7421f%}
Last approved by: {%approvedby%} 
Last approved when: {%approvedwhen%} 
Original step: {%originalstepname%} 
Comment: 
{%comment%}', N'', N'', N'', N'', N'workflow')
INSERT INTO [CMS_EmailTemplate] ([EmailTemplateID], [EmailTemplateName], [EmailTemplateDisplayName], [EmailTemplateText], [EmailTemplateSiteID], [EmailTemplateGUID], [EmailTemplateLastModified], [EmailTemplatePlainText], [EmailTemplateSubject], [EmailTemplateFrom], [EmailTemplateCc], [EmailTemplateBcc], [EmailTemplateType]) VALUES (155, N'Workflow.ReadyForApproval', N'Workflow - Document ready for approval', N'<html>
  <head>
  </head>
  <body style="font-size: 12px; font-family: arial">
    <p>
    This is an automatic notification sent by Kentico CMS. The following document is waiting for your approval. Please sign in to Kentico CMS Desk and approve it.
    </p>
    <p>
    <strong>Document:</strong> <a href="{%DocumentEditUrl%}">{%documentname%}</a> {% ifEmpty(DocumentPreviewUrl, "", "(<a href=\"" + DocumentPreviewUrl + "\">preview</a>)")|(encode)false%}
    <br />
    <strong>Last approved by:</strong> {%approvedby%}
    <br />
    <strong>Last approved when:</strong> {%approvedwhen%}
    <br />
    <strong>Original step:</strong> {%originalstepname%}
    <br />
    <strong>Current step:</strong> {%currentstepname%}
    <br />
    <strong>Comment:</strong>
    <br />
    {%comment%}
    </p>
  </body>
</html>', NULL, 'cfa7ee6c-4ee1-4594-9760-d07fe8545336', '20110922 10:44:39', N'This is an automatic notification sent by Kentico CMS. The following document is waiting for your approval. Please sign in to Kentico CMS Desk and approve it. 
Document: [url={%DocumentEditUrl%}]{%documentname%}[/url] {% ifEmpty(DocumentPreviewUrl, "", "([url=" + DocumentPreviewUrl + "]preview[/url])")|(user)administrator|(hash)67accc8d292516c257ff64b3ff2904914102e302c8b08b8e2c6be15daad7421f%}
Last approved by: {%approvedby%} 
Last approved when: {%approvedwhen%} 
Original step: {%originalstepname%} 
Current step: {%currentstepname%} 
Comment: 
{%comment%}', N'', N'', N'', N'', N'workflow')
INSERT INTO [CMS_EmailTemplate] ([EmailTemplateID], [EmailTemplateName], [EmailTemplateDisplayName], [EmailTemplateText], [EmailTemplateSiteID], [EmailTemplateGUID], [EmailTemplateLastModified], [EmailTemplatePlainText], [EmailTemplateSubject], [EmailTemplateFrom], [EmailTemplateCc], [EmailTemplateBcc], [EmailTemplateType]) VALUES (156, N'Workflow.Rejected', N'Workflow - Document rejected', N'<html>
  <head>
  </head>
  <body style="font-size: 12px; font-family: arial">
    <p>
    This is an automatic notification sent by Kentico CMS. The following document was rejected.
    </p>
    <p>
    <strong>Document:</strong> <a href="{%DocumentEditUrl%}">{%documentname%}</a> {% ifEmpty(DocumentPreviewUrl, "", "(<a href=\"" + DocumentPreviewUrl + "\">preview</a>)")|(encode)false%}
    <br />
    <br />
    <strong>Rejected by:</strong> {%approvedby%}
    <br />
    <strong>Rejected when:</strong> {%approvedwhen%}
    <br />
    <strong>Original step:</strong> {%originalstepname%}
    <br />
    <strong>Current step:</strong> {%currentstepname%}
    <br />
    <strong>Comment:</strong>
    <br />
    {%comment%}
    </p>
  </body>
</html>', NULL, '5b98fd54-1db8-4f57-b802-c7639fe08184', '20110922 10:46:43', N'This is an automatic notification sent by Kentico CMS. The following document was rejected. 
Document: [url={%DocumentEditUrl%}]{%documentname%}[/url] {% ifEmpty(DocumentPreviewUrl, "", "([url=" + DocumentPreviewUrl + "]preview[/url])")|(user)administrator|(hash)67accc8d292516c257ff64b3ff2904914102e302c8b08b8e2c6be15daad7421f%}
Rejected by: {%approvedby%} 
Rejected when: {%approvedwhen%} 
Original step: {%originalstepname%} 
Current step: {%currentstepname%} 
Comment: 
{%comment%}', N'', N'', N'', N'', N'workflow')
SET IDENTITY_INSERT [CMS_EmailTemplate] OFF
