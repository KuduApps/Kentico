CREATE PROCEDURE [Proc_CMS_Site_RemoveDependencies]
@ID int
AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON;
    -- removes VersionHistory dependences from VersionAttachment
    DELETE FROM [CMS_VersionAttachment] WHERE VersionHistoryID IN (
        SELECT VersionHistoryID FROM [CMS_VersionHistory] WHERE NodeSiteID = @ID
    );
    -- removes VersionHistory dependences from WorkflowHistory
    DELETE FROM [CMS_WorkflowHistory] WHERE VersionHistoryID IN (
        SELECT VersionHistoryID FROM [CMS_VersionHistory] WHERE NodeSiteID = @ID
    );
    -- deletes user cultures
    DELETE FROM [CMS_UserCulture] WHERE SiteID=@ID;
    -- deletes site's VersionHistory
    DELETE FROM [CMS_VersionHistory] WHERE NodeSiteID=@ID;
    -- deletes site's SettingKeys
    DELETE FROM [CMS_SettingsKey] WHERE SiteID=@ID;
    -- deletes site's EmailTemplates
    DELETE FROM [CMS_EmailTemplate] WHERE EmailTemplateSiteID=@ID;
    -- deletes site's DomainAliases
    DELETE FROM [CMS_SiteDomainAlias] WHERE SiteID=@ID;
    -- deletes site's cultures
    DELETE FROM [CMS_SiteCulture] WHERE SiteID=@ID;
    -- deletes site's categories
	DELETE FROM [CMS_Category] WHERE CategorySiteID=@ID;
	
    -- deletes site's PageTemplates
    -- declare temporary templates table
	DECLARE @templateTable TABLE (
		PageTemplateID int NOT NULL
	);	
	-- get the all templates to be deleted 
	INSERT INTO @templateTable SELECT PageTemplateID FROM CMS_PageTemplate WHERE (PageTemplateID IN (SELECT PageTemplateID FROM CMS_PageTemplateSite WHERE SiteID = @ID ) OR PageTemplateSiteID = @ID) GROUP BY PageTemplateID HAVING COUNT(PageTemplateID) = 1
	
    -- Online Marketing
    DELETE FROM OM_MVTCombinationVariation WHERE MVTCombinationID IN ( SELECT MVTCombinationID FROM OM_MVTCombination WHERE MVTCombinationPageTemplateID IN (SELECT * FROM @templateTable));
    DELETE FROM OM_MVTCombinationVariation WHERE MVTVariantID IN ( SELECT MVTVariantID FROM OM_MVTVariant WHERE MVTVariantPageTemplateID IN (SELECT * FROM @templateTable));
	DELETE FROM OM_MVTCombination WHERE MVTCombinationPageTemplateID IN (SELECT * FROM @templateTable);
	DELETE FROM OM_MVTVariant WHERE MVTVariantPageTemplateID IN (SELECT * FROM @templateTable);    	
	
	-- delete pagetemplate <-> site bindings
    DELETE FROM [CMS_PageTemplateSite] WHERE SiteID=@ID;
    -- delete page templates that are not used anywhere else
    DELETE FROM [CMS_PageTemplate] WHERE PageTemplateSiteID=@ID AND PageTemplateID IN (SELECT * FROM @templateTable);
    -- deletes site's Users
    DELETE FROM [CMS_UserSite] WHERE SiteID=@ID;
    -- removes Role's dependences when site's Roles are deleted
    DELETE FROM [Forums_ForumRoles ] WHERE RoleID IN (
        SELECT RoleID FROM [CMS_Role] WHERE SiteID = @ID
    );
    DELETE FROM [CMS_UserRole] WHERE RoleID IN (
        SELECT RoleID FROM [CMS_Role] WHERE SiteID=@ID
    );
    DELETE FROM [CMS_RolePermission] WHERE RoleID IN (
        SELECT RoleID FROM [CMS_Role] WHERE SiteID=@ID
    );
    DELETE FROM [CMS_RoleUIElement] WHERE RoleID IN (
        SELECT RoleID FROM [CMS_Role] WHERE SiteID=@ID
    );
    DELETE FROM [CMS_WorkflowStepRoles] WHERE RoleID IN (
        SELECT RoleID FROM [CMS_Role] WHERE SiteID=@ID
    );
    DELETE FROM [Polls_PollRoles] WHERE RoleID IN (
        SELECT RoleID FROM [CMS_Role] WHERE SiteID = @ID
    );
    DELETE FROM [CMS_FormRole] WHERE RoleID IN (
        SELECT RoleID FROM [CMS_Role] WHERE SiteID=@ID
    );
    DELETE FROM [Community_GroupRolePermission] WHERE RoleID IN (
        SELECT RoleID FROM [CMS_Role] WHERE SiteID = @ID
    );
	DELETE FROM [Media_LibraryRolePermission] WHERE RoleID IN (
        SELECT RoleID FROM [CMS_Role] WHERE SiteID = @ID
    );
    DELETE FROM [CMS_WidgetRole] WHERE RoleID IN (
        SELECT RoleID FROM [CMS_Role] WHERE SiteID = @ID
    );
    DELETE FROM [PM_ProjectRolePermission] WHERE ProjectID IN (
		SELECT ProjectID FROM PM_Project WHERE ProjectSiteID = @ID
	);
	
    DELETE FROM [CMS_MembershipRole] WHERE MembershipID IN (
     SELECT MembershipID FROM CMS_Membership WHERE CMS_Membership.MembershipSiteID = @ID)
     
    DELETE FROM [CMS_MembershipUser] WHERE MembershipID IN (
     SELECT MembershipID FROM CMS_Membership WHERE CMS_Membership.MembershipSiteID = @ID);
       
    -- delete site's membership
    DELETE FROM [CMS_Membership] WHERE MembershipSiteID=@ID;
    
    -- delete site's campaigns
    DELETE FROM [Analytics_Campaign] WHERE CampaignID IN
     (SELECT CampaignID FROM Analytics_Campaign WHERE CampaignSiteID = @ID);     
     
    DELETE FROM [Analytics_Conversion] WHERE ConversionID IN
     (SELECT ConversionID FROM Analytics_Conversion WHERE ConversionSiteID = @ID);     
    
    -- deletes site's Roles
    DELETE FROM [CMS_Role] WHERE SiteID=@ID;
    -- deletes site's Resources
    DELETE FROM [CMS_ResourceSite] WHERE SiteID=@ID;
    -- deletes site's Classes
    DELETE FROM [CMS_ClassSite] WHERE SiteID=@ID;
    -- deletes Events of the site
    DELETE FROM [CMS_EventLog] WHERE SiteID=@ID;
    -- deletes site's RelationshipNames
    DELETE FROM [CMS_RelationshipNameSite] WHERE SiteID=@ID;
    -- deletes site's WorkflowScopes
    DELETE FROM [CMS_WorkflowScope] WHERE ScopeSiteID=@ID;
    -- deletes site's CSSStylesheets
    DELETE FROM CMS_CssStylesheetSite WHERE SiteID=@ID;
    -- deletes site's InlineControls
    DELETE FROM [CMS_InlineControlSite] WHERE SiteID=@ID;
    -- deletes site's ScheduledTasks
    DELETE FROM [CMS_ScheduledTask] WHERE TaskSiteID=@ID;
    -- deletes site's WebPart containers
    DELETE FROM CMS_WebPartContainerSite WHERE SiteID=@ID;
    -- deletes site Polls and site's assignment to Polls
    DELETE FROM [Polls_PollRoles] WHERE PollID IN (
            SELECT PollID FROM [Polls_Poll] WHERE PollSiteID = @ID
        );
    DELETE FROM [Polls_PollSite] WHERE SiteID = @ID;
    DELETE FROM [Polls_PollAnswer] WHERE AnswerPollID IN (
            SELECT PollID FROM [Polls_Poll] WHERE PollSiteID = @ID
        );
    DELETE FROM [Polls_Poll] WHERE PollSiteID = @ID;
    
    -- NEWSLETTER
    -- deletes [Newsletter_OpenedEmail]s when site's [Newsletter_Subscriber]s are deleted    
    DELETE FROM [Newsletter_OpenedEmail] WHERE SubscriberID IN (
		SELECT SubscriberID FROM [Newsletter_Subscriber] WHERE SubscriberSiteID=@ID
	);
	-- deletes [Newsletter_SubscriberLink]s when [Newsletter_Subscriber]s are deleted
	DELETE FROM Newsletter_SubscriberLink WHERE SubscriberID IN (
		SELECT SubscriberID FROM [Newsletter_Subscriber] WHERE SubscriberSiteID=@ID
	);
    -- deletes [Newsletter_Emails] when site's [Newsletter_Subscriber]s are deleted    
    DELETE FROM [Newsletter_Emails] WHERE EmailSubscriberID IN (
        SELECT SubscriberID FROM [Newsletter_Subscriber] WHERE SubscriberSiteID=@ID
    );
    -- deletes [Newsletter_SubscriberNewsletter] when site's [Newsletter_Subscriber]s are deleted
    DELETE FROM [Newsletter_SubscriberNewsletter] WHERE SubscriberID IN (
        SELECT SubscriberID FROM [Newsletter_Subscriber] WHERE SubscriberSiteID=@ID
    );
     -- Online marketing membership relation (MemberType = 2 = newsletter subscriber  )
    DELETE FROM OM_Membership WHERE MemberType = 2 AND RelatedID IN (
        SELECT SubscriberID FROM [Newsletter_Subscriber] WHERE SubscriberSiteID=@ID
    );
    -- deletes site's [Newsletter_Subscriber]s
    DELETE FROM [Newsletter_Subscriber] WHERE SubscriberSiteID=@ID;
    
    -- deletes [Newsletter_OpenedEmail]s when [Newsletter_NewsletterIssue]s are deleted    
    DELETE FROM [Newsletter_OpenedEmail] WHERE IssueID IN (
		SELECT IssueID FROM [Newsletter_NewsletterIssue] WHERE IssueSiteID=@ID
	);		
	-- deletes [Newsletter_SubscriberLink]s when [Newsletter_NewsletterIssue]s are deleted
	DELETE FROM Newsletter_SubscriberLink WHERE LinkID IN (
		SELECT LinkID FROM Newsletter_Link WHERE LinkIssueID IN (
			SELECT IssueID FROM Newsletter_NewsletterIssue WHERE IssueSiteID = @ID
		)
	);
    -- deletes [Newsletter_Link] when [Newsletter_NewsletterIssue]s are deleted
    DELETE FROM Newsletter_Link WHERE LinkIssueID IN
		(SELECT IssueID FROM Newsletter_NewsletterIssue WHERE IssueSiteID = @ID);
		
    -- deletes [Newsletter_Emails] when [Newsletter_Issues]s are deleted
    DELETE FROM [Newsletter_Emails] WHERE EmailNewsletterIssueID IN (
        SELECT IssueID FROM [Newsletter_NewsletterIssue] WHERE IssueNewsletterID IN (
            SELECT NewsletterID FROM [Newsletter_Newsletter] WHERE NewsletterSiteID=@ID        
		)
	);
    -- deletes site's [Newsletter_Issues]s
    DELETE FROM [Newsletter_NewsletterIssue] WHERE IssueNewsletterID IN (
        SELECT NewsletterID FROM [Newsletter_Newsletter] WHERE NewsletterSiteID=@ID
    );
    -- deletes [Newsletter_SubscriberNewsletter] when [Newsletter_Newsletter]s are deleted
    DELETE FROM [Newsletter_SubscriberNewsletter] WHERE NewsletterID IN (
        SELECT NewsletterID FROM [Newsletter_Newsletter] WHERE NewsletterSiteID=@ID
    );
    -- deletes site's [Newsletter_Newsletter]s
    DELETE FROM [Newsletter_Newsletter] WHERE NewsletterSiteID=@ID;
    -- deletes [Newsletter_Emails] when [Newsletter_EmailTemplates] are deleted
    DELETE FROM [Newsletter_Emails] WHERE EmailNewsletterIssueID IN (
        SELECT IssueID FROM [Newsletter_NewsletterIssue] WHERE IssueTemplateID IN (
            SELECT TemplateID FROM [Newsletter_EmailTemplate] WHERE TemplateSiteID=@ID
        )
    );
    -- deletes [Newsletter_NewsletterIssue] when [Newsletter_EmailTemplates] are deleted
    DELETE FROM [Newsletter_NewsletterIssue] WHERE IssueTemplateID IN (
            SELECT TemplateID FROM [Newsletter_EmailTemplate] WHERE TemplateSiteID=@ID
    );
    -- deletes site's [Newsletter_EmailTemplate]s
    DELETE FROM [Newsletter_EmailTemplate] WHERE TemplateSiteID=@ID;
    
    -- removes CMS_Class's dependences when BizForms are deleted
    DELETE FROM [CMS_Query] WHERE ClassID IN (
        SELECT FormClassID FROM [CMS_Form] WHERE FormSiteID=@ID
    );
    -- insert ClassIDs of BizForms into the temporaty table
    DECLARE @classIdTable TABLE (
        FormClassID int
    );
    -- get the classes
    INSERT INTO @classIdTable SELECT FormClassID FROM [CMS_Form] WHERE FormSiteID=@ID;
    -- deletes site's BizForms' roles from CMS_Form
    DELETE FROM [CMS_FormRole] WHERE FormID IN (SELECT FormID FROM [CMS_Form] WHERE FormSiteID=@ID);
    -- deletes site's BizForms' alt.forms from CMS_AlternativeForm
    DELETE FROM [CMS_AlternativeForm] WHERE FormClassID IN (SELECT FormClassID FROM @classIdTable)
        OR FormCoupledClassID IN (SELECT FormClassID FROM @classIdTable);
    -- deletes site's BizForms from CMS_Form
    DELETE FROM [CMS_Form] WHERE FormSiteID=@ID;
    -- deletes BizForms' records in CMS_Class
    DELETE FROM [CMS_Class] WHERE ClassID IN (SELECT FormClassID FROM @classIdTable);
    
    -- FORUM
    -- Forum user favorites
    DELETE FROM [Forums_UserFavorites] WHERE SiteID = @ID;
    -- Forum attachment
    DELETE FROM [Forums_Attachment] WHERE AttachmentSiteID = @ID;
    
    UPDATE [Forums_ForumPost]  SET PostParentID=NULL WHERE PostForumID IN (
        SELECT ForumID FROM [Forums_Forum] WHERE ForumGroupID IN (
        SELECT GroupID FROM [Forums_ForumGroup] WHERE GroupSiteID = @ID
        ));
    DELETE FROM [Forums_ForumSubscription] WHERE SubscriptionForumID IN (
        SELECT ForumID FROM [Forums_Forum] WHERE ForumGroupID  IN (
        SELECT GroupID FROM [Forums_ForumGroup] WHERE GroupSiteID = @ID)
    );
    DELETE FROM [Forums_ForumPost] WHERE PostForumID IN (
        SELECT ForumID FROM [Forums_Forum] WHERE ForumGroupID IN (
        SELECT GroupID FROM [Forums_ForumGroup] WHERE GroupSiteID = @ID)
    );
    DELETE FROM [Forums_ForumRoles ] WHERE ForumID IN (
        SELECT ForumID FROM [Forums_Forum] WHERE ForumGroupID IN (
        SELECT GroupID FROM [Forums_ForumGroup] WHERE GroupSiteID = @ID)
    );
    DELETE FROM [Forums_ForumModerators] WHERE ForumID IN (
        SELECT ForumID FROM [Forums_Forum] WHERE ForumGroupID  IN (
        SELECT GroupID FROM [Forums_ForumGroup] WHERE GroupSiteID = @ID)
    );
    
    DELETE FROM [Forums_Forum] WHERE ForumGroupID  IN (
        SELECT GroupID FROM [Forums_ForumGroup] WHERE GroupSiteID = @ID);    
    DELETE FROM [Forums_ForumGroup] WHERE GroupSiteID = @ID;
    -- delete Staging dependences
    DELETE FROM [Staging_Synchronization] WHERE 
        SynchronizationTaskID IN (SELECT TaskID FROM [Staging_Task] WHERE TaskSiteID = @ID)
        OR
        SynchronizationServerID IN (SELECT ServerID FROM [Staging_Server] WHERE ServerSiteID = @ID)
    ;
    DELETE FROM [Staging_SyncLog] WHERE 
        SyncLogTaskID IN (SELECT TaskID FROM [Staging_Task] WHERE TaskSiteID = @ID)
        OR 
        SyncLogServerID IN (SELECT ServerID FROM [Staging_Server] WHERE ServerSiteID = @ID)
    ;
    DELETE FROM [Staging_Server] WHERE ServerSiteID = @ID;
    -- delete StagingTasks - delete also tasks which don't have any synchronization binding
    DELETE FROM [Staging_Task] WHERE TaskSiteID = @ID OR TaskID NOT IN (SELECT DISTINCT SynchronizationTaskID FROM [Staging_Synchronization]);
    -- ECOMMERCE
    -- delete COM_Order dependencies
	DELETE FROM COM_OrderItem WHERE OrderItemOrderID IN (SELECT OrderID FROM [COM_Order] WHERE OrderSiteID = @ID);
	DELETE FROM COM_OrderStatusUser WHERE OrderID IN (SELECT OrderID FROM [COM_Order] WHERE OrderSiteID = @ID);
    -- delete COM_Order
	DELETE FROM [COM_Order] WHERE OrderSiteID = @ID;
	-- delete COM_ShoppingCart dependencies
	DELETE FROM [COM_ShoppingCartSKU] WHERE SKUID IN (SELECT SKUID FROM COM_SKU WHERE SKUSiteID = @ID);
	DELETE FROM [COM_ShoppingCartSKU] WHERE ShoppingCartID IN (SELECT ShoppingCartID FROM COM_ShoppingCart WHERE ShoppingCartSiteID = @ID);
	-- delete shopping cart
	DELETE FROM [COM_ShoppingCart] WHERE ShoppingCartSiteID = @ID;
	-- delete COM_SKU dependencies
	DELETE FROM [COM_SKUTaxClasses] WHERE SKUID IN (SELECT SKUID FROM COM_SKU WHERE SKUSiteID = @ID);
	DELETE FROM [COM_SKUDiscountCoupon] WHERE SKUID IN (SELECT SKUID FROM COM_SKU WHERE SKUSiteID = @ID);
	DELETE FROM [COM_Wishlist] WHERE SKUID IN (SELECT SKUID FROM COM_SKU WHERE SKUSiteID = @ID);
	DELETE FROM [COM_VolumeDiscount] WHERE VolumeDiscountSKUID IN (SELECT SKUID FROM COM_SKU WHERE SKUSiteID = @ID);
	DELETE FROM [COM_OrderItem] WHERE OrderItemSKUID IN (SELECT SKUID FROM COM_SKU WHERE SKUSiteID = @ID);
	DELETE FROM [COM_SKUOptionCategory] WHERE SKUID IN (SELECT SKUID FROM COM_SKU WHERE SKUSiteID = @ID);
	DELETE FROM [CMS_MetaFile] WHERE MetaFileObjectID IN (SELECT SKUID FROM COM_SKU WHERE SKUSiteID = @ID) AND MetaFileObjectType = 'ecommerce.sku';
	UPDATE [CMS_Tree] SET NodeSKUID = NULL WHERE NodeSKUID IN (SELECT SKUID FROM COM_SKU WHERE SKUSiteID = @ID);
	-- delete product type dependencies
	DELETE FROM [COM_Bundle] WHERE BundleID IN (SELECT SKUID FROM COM_SKU WHERE SKUSiteID = @ID) OR SKUID IN (SELECT SKUID FROM COM_SKU WHERE SKUSiteID = @ID);
	DELETE FROM [COM_SKUFile] WHERE FileSKUID IN (SELECT SKUID FROM COM_SKU WHERE SKUSiteID = @ID);
	-- delete COM_SKU
	DELETE FROM [COM_SKU] WHERE SKUSiteID = @ID;
	DELETE FROM [COM_Address] WHERE AddressCustomerID IN (SELECT CustomerID FROM COM_Customer WHERE CustomerSiteID = @ID);
	DELETE FROM [COM_CustomerCreditHistory] WHERE EventCustomerID IN (SELECT CustomerID FROM COM_Customer WHERE CustomerSiteID = @ID);
	DELETE FROM [COM_CustomerCreditHistory] WHERE EventSiteID = @ID;
	DELETE FROM [COM_ShoppingCartSKU] WHERE ShoppingCartID IN (SELECT ShoppingCartID FROM COM_ShoppingCart WHERE ShoppingCartCustomerID IN (SELECT CustomerID FROM COM_Customer WHERE CustomerSiteID = @ID));
	DELETE FROM [COM_ShoppingCart] WHERE ShoppingCartCustomerID IN (SELECT CustomerID FROM COM_Customer WHERE CustomerSiteID = @ID);
     -- Online marketing membership relation (MemberType = 1 = customer  )
    DELETE FROM OM_Membership WHERE MemberType = 1 AND RelatedID IN (SELECT CustomerID FROM COM_Customer WHERE CustomerSiteID = @ID);	
	-- delete COM_Customer
	DELETE FROM [COM_Customer] WHERE CustomerSiteID = @ID;
	 -- delete COM_TaxClass dependencies
	DELETE FROM [COM_TaxClassCountry] WHERE TaxClassID IN (SELECT TaxClassID FROM [COM_TaxClass] WHERE TaxClassSiteID = @ID);
	DELETE FROM [COM_TaxClassState] WHERE TaxClassID IN (SELECT TaxClassID FROM [COM_TaxClass] WHERE TaxClassSiteID = @ID);
	DELETE FROM [COM_DepartmentTaxClass] WHERE TaxClassID IN (SELECT TaxClassID FROM [COM_TaxClass] WHERE TaxClassSiteID = @ID);
	DELETE FROM [COM_ShippingOptionTaxClass] WHERE TaxClassID IN (SELECT TaxClassID FROM [COM_TaxClass] WHERE TaxClassSiteID = @ID);
	DELETE FROM [COM_SKUTaxClasses] WHERE TaxClassID IN (SELECT TaxClassID FROM [COM_TaxClass] WHERE TaxClassSiteID = @ID);
	-- delete COM_TaxClass
	DELETE FROM [COM_TaxClass] WHERE TaxClassSiteID = @ID;
	-- delete COM_Department dependencies
	DELETE FROM [COM_DepartmentTaxClass] WHERE DepartmentID IN (SELECT DepartmentID FROM [COM_Department] WHERE DepartmentSiteID = @ID);
	DELETE FROM [COM_UserDepartment] WHERE DepartmentID IN (SELECT DepartmentID FROM [COM_Department] WHERE DepartmentSiteID = @ID);
	DELETE FROM [COM_DiscountLevelDepartment] WHERE DepartmentID IN (SELECT DepartmentID FROM [COM_Department] WHERE DepartmentSiteID = @ID);
	UPDATE CMS_Class SET ClassSKUDefaultDepartmentID = NULL WHERE ClassSKUDefaultDepartmentID IN (SELECT DepartmentID FROM [COM_Department] WHERE DepartmentSiteID = @ID);
	-- delete COM_Department
	DELETE FROM [COM_Department] WHERE DepartmentSiteID = @ID;
	-- delete COM_DiscountCoupon dependencies
	DELETE FROM COM_SKUDiscountCoupon WHERE DiscountCouponID IN (SELECT DiscountCouponID FROM COM_DiscountCoupon WHERE DiscountCouponSiteID = @ID);
	-- delete COM_DiscountCoupon
	DELETE FROM COM_DiscountCoupon WHERE DiscountCouponSiteID = @ID
	-- delete COM_DiscountLevel dependencies
	DELETE FROM [COM_DiscountLevelDepartment] WHERE DiscountLevelID IN (SELECT DiscountLevelID FROM [COM_DiscountLevel] WHERE DiscountLevelSiteID = @ID);
	UPDATE COM_Customer SET CustomerDiscountLevelID = NULL WHERE CustomerDiscountLevelID IN (SELECT DiscountLevelID FROM [COM_DiscountLevel] WHERE DiscountLevelSiteID = @ID);
	-- delete COM_DiscountLevel
	DELETE FROM [COM_DiscountLevel] WHERE DiscountLevelSiteID = @ID
	-- delete COM_ShippingOption dependencies
	DELETE FROM COM_PaymentShipping WHERE ShippingOptionID IN (SELECT ShippingOptionID FROM COM_ShippingOption WHERE ShippingOptionSiteID = @ID);
	DELETE FROM COM_ShippingCost WHERE ShippingCostShippingOptionID IN (SELECT ShippingOptionID FROM COM_ShippingOption WHERE ShippingOptionSiteID = @ID);
	DELETE FROM COM_ShippingOptionTaxClass WHERE ShippingOptionID IN (SELECT ShippingOptionID FROM COM_ShippingOption WHERE ShippingOptionSiteID = @ID);
	-- delete COM_ShippingOption
	DELETE FROM COM_ShippingOption WHERE ShippingOptionSiteID = @ID;
	-- delete COM_ExchangeTable dependencies
	DELETE FROM [COM_CurrencyExchangeRate] WHERE ExchangeTableID IN (SELECT ExchangeTableID FROM [COM_ExchangeTable] WHERE ExchangeTableSiteID = @ID);
	-- delete COM_ExchangeTable
	DELETE FROM [COM_ExchangeTable] WHERE ExchangeTableSiteID = @ID;
	-- delete COM_Currency dependencies
	DELETE FROM [COM_CurrencyExchangeRate] WHERE ExchangeRateToCurrencyID IN (SELECT CurrencySiteID FROM [COM_Currency] WHERE CurrencySiteID = @ID);
	-- delete COM_Currency
	DELETE FROM [COM_Currency] WHERE CurrencySiteID = @ID;
	-- delete COM_PaymentOption dependencies
	DELETE FROM [COM_ShoppingCart] WHERE ShoppingCartPaymentOptionID IN (SELECT PaymentOptionID FROM [COM_PaymentOption] WHERE PaymentOptionSiteID = @ID);
	UPDATE [COM_Customer] SET CustomerPrefferedPaymentOptionID = NULL WHERE CustomerPrefferedPaymentOptionID IN (SELECT PaymentOptionID FROM [COM_PaymentOption] WHERE PaymentOptionSiteID = @ID);
	DELETE FROM [COM_PaymentShipping] WHERE PaymentOptionID IN (SELECT PaymentOptionID FROM [COM_PaymentOption] WHERE PaymentOptionSiteID = @ID);
	-- delete COM_PaymentOption
	DELETE FROM [COM_PaymentOption] WHERE PaymentOptionSiteID = @ID;
	-- delete COM_InternalStatus
	DELETE FROM [COM_InternalStatus] WHERE InternalStatusSiteID = @ID;
	-- delete COM_OrderStatus
	DELETE FROM [COM_OrderStatus] WHERE StatusSiteID = @ID;
	-- delete COM_PublicStatus
	DELETE FROM [COM_PublicStatus] WHERE PublicStatusSiteID = @ID;
	-- delete COM_OptionCategory
	DELETE FROM [COM_OptionCategory] WHERE CategorySiteID = @ID;
	-- delete COM_Supplier
	DELETE FROM [COM_Supplier] WHERE SupplierSiteID = @ID;
	-- delete COM_Manufacturer
	DELETE FROM [COM_Manufacturer] WHERE ManufacturerSiteID = @ID;
	    
    -- WEB ANALYTICS
    -- delete Analytics_Statistics dependences
    DELETE FROM Analytics_DayHits WHERE HitsStatisticsID IN (
        SELECT StatisticsID FROM Analytics_Statistics WHERE StatisticsSiteID=@ID);
    DELETE FROM Analytics_HourHits WHERE HitsStatisticsID IN (
        SELECT StatisticsID FROM Analytics_Statistics WHERE StatisticsSiteID=@ID);
    DELETE FROM Analytics_MonthHits WHERE HitsStatisticsID IN (
        SELECT StatisticsID FROM Analytics_Statistics WHERE StatisticsSiteID=@ID);
    DELETE FROM Analytics_WeekHits WHERE HitsStatisticsID IN (
        SELECT StatisticsID FROM Analytics_Statistics WHERE StatisticsSiteID=@ID);
    DELETE FROM Analytics_YearHits WHERE HitsStatisticsID IN (
        SELECT StatisticsID FROM Analytics_Statistics WHERE StatisticsSiteID=@ID);
    -- delete Analytics_Statistics
    DELETE FROM Analytics_Statistics WHERE StatisticsSiteID = @ID;
    -- POLLS
    DELETE FROM Polls_PollSite WHERE SiteID = @ID;
    -- Metafiles
    DELETE FROM CMS_MetaFile WHERE MetaFileSiteID = @ID;
    -- Export history
    DELETE FROM Export_History WHERE ExportSiteID = @ID;
    DELETE FROM Export_Task WHERE TaskSiteID = @ID;
    -- Notifications
    DELETE FROM Notification_Subscription WHERE SubscriptionTemplateID IN (SELECT TemplateID FROM Notification_Template WHERE TemplateSiteID = @ID);
    DELETE FROM Notification_TemplateText WHERE Notification_TemplateText.TemplateID IN (SELECT TemplateID FROM Notification_Template WHERE TemplateSiteID = @ID);
    DELETE FROM Notification_Template WHERE TemplateSiteID = @ID;
    DELETE FROM Notification_Subscription WHERE SubscriptionSiteID = @ID;
    -- Media libraries
    DELETE FROM Media_LibraryRolePermission WHERE LibraryID IN (SELECT Media_Library.LibraryID FROM Media_Library WHERE LibrarySiteID = @ID);
    DELETE FROM Media_File WHERE FileLibraryID IN (SELECT Media_Library.LibraryID FROM Media_Library WHERE LibrarySiteID = @ID);
    DELETE FROM Media_Library WHERE LibrarySiteID = @ID;
    -- Message boards
    DELETE FROM [Board_Message] WHERE MessageBoardID IN (SELECT BoardID FROM [Board_Board] WHERE BoardSiteID = @ID);
	DELETE FROM [Board_Moderator] WHERE BoardID IN (SELECT BoardID FROM [Board_Board] WHERE BoardSiteID = @ID);
    DELETE FROM [Board_Role] WHERE BoardID IN (SELECT BoardID FROM [Board_Board] WHERE BoardSiteID = @ID);
    DELETE FROM [Board_Subscription] WHERE SubscriptionBoardID IN (SELECT BoardID FROM [Board_Board] WHERE BoardSiteID = @ID);
    DELETE FROM [Board_Board] WHERE BoardSiteID = @ID;
    -- Delete Tags
    DELETE FROM [CMS_Tag] WHERE TagGroupID  IN (
        SELECT TagGroupID FROM [CMS_TagGroup] WHERE TagGroupSiteID = @ID);    
    DELETE FROM [CMS_TagGroup] WHERE TagGroupSiteID = @ID;
    -- Project Management
    UPDATE [PM_Project] SET ProjectGroupID = NULL WHERE ProjectGroupID IN (
        SELECT GroupID FROM [Community_Group] WHERE GroupSiteID = @ID
    );    
    -- Groups
    DELETE FROM [Community_GroupMember] WHERE MemberGroupID IN (
        SELECT GroupID FROM [Community_Group] WHERE GroupSiteID = @ID
    );
	DELETE FROM [Community_Invitation] WHERE InvitationGroupID IN (
        SELECT GroupID FROM [Community_Group] WHERE GroupSiteID = @ID
    );
    DELETE FROM [Community_Group] WHERE GroupSiteID = @ID;
    -- Report abuse
    DELETE FROM [CMS_AbuseReport] WHERE ReportSiteID = @ID;
    -- Banned IPs
    DELETE FROM [CMS_BannedIP] WHERE IPAddressSiteID = @ID;
    -- User sessions
    DELETE FROM [CMS_Session] WHERE SessionSiteID = @ID;
    -- search index
    DELETE FROM [CMS_SearchIndexSite] WHERE IndexSiteID = @ID;
    
    -- CMS Page template scopes
    DELETE FROM [CMS_PageTemplateScope] WHERE PageTemplateScopeSiteID = @ID;
    
    -- Project Management
    DELETE FROM [PM_ProjectTask] WHERE ProjectTaskProjectID IN (
		SELECT ProjectID FROM PM_Project WHERE ProjectSiteID = @ID
	);
    DELETE FROM [PM_Project] WHERE ProjectSiteID = @ID;    
    
    -- Remove dashboards related to the current site
    DELETE FROM [CMS_Personalization] WHERE PersonalizationSiteID = @ID;
    
    -- A/B Testing
    DELETE FROM  [OM_ABVariant] WHERE ABVariantTestID IN (
		SELECT ABTestID FROM [OM_ABTest] WHERE ABTestSiteID = @ID
		);
    DELETE FROM [OM_ABTest] WHERE ABTestSiteID = @ID;
    -- MVT
    DELETE FROM [OM_MVTest] WHERE MVTestSiteID = @ID;
        -- Delete site objects object versions
    DELETE FROM [CMS_ObjectVersionHistory] WHERE VersionObjectSiteID = @ID;
    
    -- SMTP Servers
    DELETE FROM [CMS_SMTPServerSite] WHERE SiteID = @ID;
    
    -- SCORING
    -- delete [OM_ScoreContactRule] dependencies
    DELETE FROM [OM_ScoreContactRule] WHERE ScoreID IN (SELECT ScoreID FROM [OM_Score] WHERE ScoreSiteID = @ID);
    -- delete [OM_Rule]
    DELETE FROM [OM_Rule] WHERE RuleSiteID = @ID;
    -- delete [OM_Score]
    DELETE FROM [OM_Score] WHERE ScoreSiteID = @ID;
    
    -- CONTACT MANAGEMENT
    DELETE FROM [OM_PageVisit] WHERE [PageVisitActivityID] IN (SELECT [ActivityID] FROM [OM_Activity] WHERE ActivitySiteID = @ID)
    DELETE FROM [OM_Search] WHERE [SearchActivityID] IN (SELECT [ActivityID] FROM [OM_Activity] WHERE [ActivitySiteID] = @ID);
    DELETE FROM [OM_Activity] WHERE [ActivitySiteID] = @ID
    DELETE FROM [OM_ContactGroupMember] WHERE [ContactGroupMemberContactGroupID] IN (SELECT [ContactGroupID] FROM [OM_ContactGroup] WHERE [ContactGroupSiteID] = @ID);
    DELETE FROM [OM_ContactGroup] WHERE [ContactGroupSiteID] = @ID;
    -- delete all acount-contact relations for account from this site
    DELETE FROM [OM_AccountContact] WHERE [AccountID] IN (SELECT [AccountID] FROM [OM_Account] WHERE [AccountSiteID]=@ID);
    DELETE FROM [OM_AccountContact] WHERE [ContactID] IN (SELECT [ContactID] FROM [OM_Contact] WHERE [ContactSiteID]=@ID);
    DELETE FROM [OM_Account] WHERE [AccountSiteID] = @ID;   
    -- delete all membership relations for contact from this site
    DELETE FROM [OM_Membership] WHERE [ActiveContactID] IN (SELECT [ContactID] FROM [OM_Contact] WHERE [ContactSiteID]=@ID);
    DELETE FROM [OM_Membership] WHERE [OriginalContactID] IN (SELECT [ContactID] FROM [OM_Contact] WHERE [ContactSiteID]=@ID);  
    DELETE FROM [OM_IP] WHERE [IPActiveContactID] IN (SELECT [ContactID] FROM [OM_Contact] WHERE [ContactSiteID]=@ID); 
    DELETE FROM [OM_IP] WHERE [IPOriginalContactID] IN (SELECT [ContactID] FROM [OM_Contact] WHERE [ContactSiteID]=@ID); 
    DELETE FROM [OM_UserAgent] WHERE [UserAgentActiveContactID] IN (SELECT [ContactID] FROM [OM_Contact] WHERE [ContactSiteID]=@ID); 
    DELETE FROM [OM_UserAgent] WHERE [UserAgentOriginalContactID] IN (SELECT [ContactID] FROM [OM_Contact] WHERE [ContactSiteID]=@ID);   
    DELETE FROM [OM_Contact] WHERE [ContactSiteID] = @ID;
    
    -- delete [OM_AccountStatus] dependencies
    DELETE FROM [OM_AccountStatus] WHERE [AccountStatusSiteID] = @ID    
    -- delete [OM_ContactStatus] dependencies
    DELETE FROM OM_ContactStatus WHERE [ContactStatusSiteID] = @ID    
    -- delete [OM_ContactRole] dependencies
    DELETE FROM OM_ContactRole WHERE [ContactRoleSiteID] = @ID
    
    -- delete Integration dependecies
    DELETE FROM [Integration_SyncLog] WHERE SyncLogSynchronizationID IN (SELECT SynchronizationID FROM [View_Integration_Task_Joined] WHERE TaskSiteID = @ID);
    DELETE FROM [Integration_Synchronization] WHERE SynchronizationID IN (SELECT SynchronizationID FROM [View_Integration_Task_Joined] WHERE TaskSiteID = @ID);
    DELETE FROM [Integration_Task] WHERE TaskSiteID = @ID;
END
