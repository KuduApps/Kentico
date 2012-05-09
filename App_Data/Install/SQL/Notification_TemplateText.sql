CREATE TABLE [Notification_TemplateText] (
		[TemplateTextID]               [int] IDENTITY(1, 1) NOT NULL,
		[TemplateID]                   [int] NOT NULL,
		[GatewayID]                    [int] NOT NULL,
		[TemplateSubject]              [nvarchar](250) NOT NULL,
		[TemplateHTMLText]             [nvarchar](max) NOT NULL,
		[TemplatePlainText]            [nvarchar](max) NOT NULL,
		[TemplateTextGUID]             [uniqueidentifier] NOT NULL,
		[TemplateTextLastModified]     [datetime] NOT NULL
)  
ALTER TABLE [Notification_TemplateText]
	ADD
	CONSTRAINT [PK_Notification_TemplateText]
	PRIMARY KEY
	CLUSTERED
	([TemplateTextID])
	
	
CREATE NONCLUSTERED INDEX [IX_Notification_TemplateText_GatewayID]
	ON [Notification_TemplateText] ([GatewayID])
	
CREATE NONCLUSTERED INDEX [IX_Notification_TemplateText_TemplateID]
	ON [Notification_TemplateText] ([TemplateID])
	
ALTER TABLE [Notification_TemplateText]
	WITH CHECK
	ADD CONSTRAINT [FK_Notification_TemplateText_GatewayID_Notification_Gateway]
	FOREIGN KEY ([GatewayID]) REFERENCES [Notification_Gateway] ([GatewayID])
ALTER TABLE [Notification_TemplateText]
	CHECK CONSTRAINT [FK_Notification_TemplateText_GatewayID_Notification_Gateway]
ALTER TABLE [Notification_TemplateText]
	WITH CHECK
	ADD CONSTRAINT [FK_Notification_TemplateText_TemplateID_Notification_Template]
	FOREIGN KEY ([TemplateID]) REFERENCES [Notification_Template] ([TemplateID])
ALTER TABLE [Notification_TemplateText]
	CHECK CONSTRAINT [FK_Notification_TemplateText_TemplateID_Notification_Template]
