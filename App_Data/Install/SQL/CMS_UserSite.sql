CREATE TABLE [CMS_UserSite] (
		[UserSiteID]                        [int] IDENTITY(1, 1) NOT NULL,
		[UserID]                            [int] NOT NULL,
		[SiteID]                            [int] NOT NULL,
		[UserPreferredCurrencyID]           [int] NULL,
		[UserPreferredShippingOptionID]     [int] NULL,
		[UserPreferredPaymentOptionID]      [int] NULL,
		[UserDiscountLevelID]               [int] NULL
) 
ALTER TABLE [CMS_UserSite]
	ADD
	CONSTRAINT [PK_CMS_UserSite_1]
	PRIMARY KEY
	CLUSTERED
	([UserSiteID])
	
ALTER TABLE [CMS_UserSite]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_UserSite_COM_Currency]
	FOREIGN KEY ([UserPreferredCurrencyID]) REFERENCES [COM_Currency] ([CurrencyID])
ALTER TABLE [CMS_UserSite]
	CHECK CONSTRAINT [FK_CMS_UserSite_COM_Currency]
ALTER TABLE [CMS_UserSite]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_UserSite_COM_DiscountLevel]
	FOREIGN KEY ([UserDiscountLevelID]) REFERENCES [COM_DiscountLevel] ([DiscountLevelID])
ALTER TABLE [CMS_UserSite]
	CHECK CONSTRAINT [FK_CMS_UserSite_COM_DiscountLevel]
ALTER TABLE [CMS_UserSite]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_UserSite_COM_PaymentOption]
	FOREIGN KEY ([UserPreferredPaymentOptionID]) REFERENCES [COM_PaymentOption] ([PaymentOptionID])
ALTER TABLE [CMS_UserSite]
	CHECK CONSTRAINT [FK_CMS_UserSite_COM_PaymentOption]
ALTER TABLE [CMS_UserSite]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_UserSite_COM_ShippingOption]
	FOREIGN KEY ([UserPreferredShippingOptionID]) REFERENCES [COM_ShippingOption] ([ShippingOptionID])
ALTER TABLE [CMS_UserSite]
	CHECK CONSTRAINT [FK_CMS_UserSite_COM_ShippingOption]
ALTER TABLE [CMS_UserSite]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_UserSite_SiteID_CMS_Site]
	FOREIGN KEY ([SiteID]) REFERENCES [CMS_Site] ([SiteID])
ALTER TABLE [CMS_UserSite]
	CHECK CONSTRAINT [FK_CMS_UserSite_SiteID_CMS_Site]
ALTER TABLE [CMS_UserSite]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_UserSite_UserID_CMS_User]
	FOREIGN KEY ([UserID]) REFERENCES [CMS_User] ([UserID])
ALTER TABLE [CMS_UserSite]
	CHECK CONSTRAINT [FK_CMS_UserSite_UserID_CMS_User]
