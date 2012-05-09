CREATE PROCEDURE [Proc_Analytics_GenerateSampleData]
@From DATETIME,
	@To DATETIME,
	@SiteID INT,
	@Type NVARCHAR (50)
AS
BEGIN
DECLARE @Current DATETIME
	DECLARE @CodeName NVARCHAR (150)
	DECLARE @HourHitsMax INT
	-- DECLARE TEMP TABLES
	DECLARE @CodeNames TABLE
	(
	CodeNameID INT IDENTITY PRIMARY KEY,
	CodeName NVARCHAR (100),
	CreateForMultipleNodes INT
	)
 
	DECLARE @Cultures TABLE
	(
	CultureCode NVARCHAR (20)
	) 
 
	-- Temp analytics_statistics "row"
	DECLARE @AnalyticsDetail TABLE 
	(
		CodeNameID INT,	
		ObjectName NVARCHAR (200),
		ObjectID NVARCHAR (100)	
	) 
 
	---- Insert codenames into table
	INSERT INTO @CodeNames VALUES ('aggviews',1) -- 1
	INSERT INTO @CodeNames VALUES ('avgtimeonpage',1) -- 2 
	INSERT INTO @CodeNames VALUES ('browsertype',0) -- 3 
	--INSERT INTO @CodeNames VALUES ('campaign',0)
	--INSERT INTO @CodeNames VALUES ('conversion',0)
	INSERT INTO @CodeNames VALUES ('countries',0) -- 4
	INSERT INTO @CodeNames VALUES ('exitpage',1) -- 5 
	INSERT INTO @CodeNames VALUES ('filedownloads',1) -- 6
	INSERT INTO @CodeNames VALUES ('flash',0) -- 7 
	INSERT INTO @CodeNames VALUES ('java',0) -- 8 
	INSERT INTO @CodeNames VALUES ('landingpage',1) -- 9 
	INSERT INTO @CodeNames VALUES ('onsitesearchkeyword',2) --10 
	INSERT INTO @CodeNames VALUES ('operatingsystem',0) --11
	INSERT INTO @CodeNames VALUES ('pagenotfound',1) -- 12
	INSERT INTO @CodeNames VALUES ('pageviews',1) -- 13
	INSERT INTO @CodeNames VALUES ('referringsite_direct',2) -- 14 
	INSERT INTO @CodeNames VALUES ('referringsite_local',0) -- 15 
	INSERT INTO @CodeNames VALUES ('referringsite_referring',2) -- 16
	INSERT INTO @CodeNames VALUES ('registereduser',0) -- 17 
	INSERT INTO @CodeNames VALUES ('referringsite_search',2) -- 18
	INSERT INTO @CodeNames VALUES ('screencolor',0) -- 19
	INSERT INTO @CodeNames VALUES ('screenresolution',0) -- 20
	INSERT INTO @CodeNames VALUES ('searchkeyword',2) -- 21
	INSERT INTO @CodeNames VALUES ('silverlight',0) --22 
	INSERT INTO @CodeNames VALUES ('urlreferrals',0) -- 23 
	INSERT INTO @CodeNames VALUES ('visitfirst',0) -- 24 
	INSERT INTO @CodeNames VALUES ('visitreturn',0) -- 25
	-- Cultures 
	INSERT INTO @Cultures VALUES ('en-US')
	INSERT INTO @Cultures VALUES ('fr-FR')
	-- Browser types 
	INSERT INTO @AnalyticsDetail VALUES (3,'Opera',null)
	INSERT INTO @AnalyticsDetail VALUES (3,'IE9',null)
	INSERT INTO @AnalyticsDetail VALUES (3,'IE8',null)
	INSERT INTO @AnalyticsDetail VALUES (3,'IE7',null)
	INSERT INTO @AnalyticsDetail VALUES (3,'Firefox',null)
	INSERT INTO @AnalyticsDetail VALUES (3,'Safari',null)
	-- Countries
	INSERT INTO @AnalyticsDetail VALUES (4,'United states',null)
	INSERT INTO @AnalyticsDetail VALUES (4,'GB',null)
	INSERT INTO @AnalyticsDetail VALUES (4,'Germany',null)
	INSERT INTO @AnalyticsDetail VALUES (4,'Italy',null)
	INSERT INTO @AnalyticsDetail VALUES (4,'Czech republic',null)
	INSERT INTO @AnalyticsDetail VALUES (4,'France',null)
	INSERT INTO @AnalyticsDetail VALUES (4,'China',null)
	INSERT INTO @AnalyticsDetail VALUES (4,'Japan',null)
	INSERT INTO @AnalyticsDetail VALUES (4,'Austria',null)
	INSERT INTO @AnalyticsDetail VALUES (4,'Canada',null)
	-- Flash
	INSERT INTO @AnalyticsDetail VALUES (7,'hf',null)
	INSERT INTO @AnalyticsDetail VALUES (7,'nf',null)
	-- Java
	INSERT INTO @AnalyticsDetail VALUES (8,'hj',null)
	INSERT INTO @AnalyticsDetail VALUES (8,'nj',null)
	-- Silverlight
	INSERT INTO @AnalyticsDetail VALUES (22,'hs',null)
	INSERT INTO @AnalyticsDetail VALUES (22,'ns',null)
	-- Referring sites
	INSERT INTO @AnalyticsDetail VALUES (16,'amazon.com',null)
	INSERT INTO @AnalyticsDetail VALUES (16,'facebook.com',null)
	INSERT INTO @AnalyticsDetail VALUES (16,'codeproject.com',null)
	INSERT INTO @AnalyticsDetail VALUES (16,'gmail.com',null)
	-- Search engines
	INSERT INTO @AnalyticsDetail VALUES (18,'google.com',null)
	INSERT INTO @AnalyticsDetail VALUES (18,'bing.com',null)
	-- Screen colors
	INSERT INTO @AnalyticsDetail VALUES (19,'24-bit',null)
	INSERT INTO @AnalyticsDetail VALUES (19,'16-bit',null)
	INSERT INTO @AnalyticsDetail VALUES (19,'32-bit',null)
	-- Screen resolution
	INSERT INTO @AnalyticsDetail VALUES (20,'1920x1080',null)
	INSERT INTO @AnalyticsDetail VALUES (20,'1280x720',null)
	INSERT INTO @AnalyticsDetail VALUES (20,'800x600',null)
	-- Search keywords
	INSERT INTO @AnalyticsDetail VALUES (21,'Kentico',null)
	INSERT INTO @AnalyticsDetail VALUES (21,'CMS',null)
	INSERT INTO @AnalyticsDetail VALUES (21,'Content management system',null)
	-- Url refferals
	INSERT INTO @AnalyticsDetail VALUES (23,'amazon.com/direct',null)
	INSERT INTO @AnalyticsDetail VALUES (23,'amazon.com/books?bookid=5',null)
	-- Campaign 
	--INSERT INTO @AnalyticsDetail VALUES (4,'Amazon banner',null)
	--INSERT INTO @AnalyticsDetail VALUES (4,'Google adwords',null)
	---- Conversion 
	--INSERT INTO @AnalyticsDetail VALUES (5,'Register',null)
	--INSERT INTO @AnalyticsDetail VALUES (5,'Newsletters',null)
	--INSERT INTO @AnalyticsDetail VALUES (5,'Shopping',null)
	-- On Site Search
	INSERT INTO @AnalyticsDetail VALUES (10,'Home',null)
	INSERT INTO @AnalyticsDetail VALUES (10,'Television',null)
	INSERT INTO @AnalyticsDetail VALUES (10,'Text',null)
	-- Operation system
	INSERT INTO @AnalyticsDetail VALUES (11,'Windows',null)
	INSERT INTO @AnalyticsDetail VALUES (11,'Linux',null)
	INSERT INTO @AnalyticsDetail VALUES (11,'Mac OS',null)
	-- Registered user
	INSERT INTO @AnalyticsDetail VALUES (17,'Thomas Jefferson',1)
	INSERT INTO @AnalyticsDetail VALUES (17,'Andy Black',2)
	INSERT INTO @AnalyticsDetail VALUES (17,'Mary Ann Moss',3)
	
	-- Referring local site
	DECLARE @LocalFrom INT;
	DECLARE @LocalTo INT;
	SET @LocalFrom = (SELECT TOP 1 NodeID FROM View_CMS_Tree_Joined WHERE NodeSiteID = @SiteID AND NodeLevel > 0
	   ORDER BY NodeLevel, NodeOrder ASC )
    SET @LocalTo =(SELECT TOP 1 NodeID FROM View_CMS_Tree_Joined WHERE NodeSiteID = @SiteID AND NodeLevel > 0 AND NodeID <> @LocalFrom
	   ORDER BY NodeLevel, NodeOrder ASC )
	INSERT INTO @AnalyticsDetail VALUES (15, @LocalTo,@LocalFrom)
	
	-- Conversions
	DECLARE @ConversionName NVARCHAR(100)
	SET @ConversionName = (SELECT TOP 1 ConversionName FROM Analytics_Conversion WHERE ConversionSiteID = @SiteID)
	IF (@Type = 'campaign' OR @Type = 'abtest' OR @Type ='mvtest' OR @Type ='' OR @Type = 'conversion')
	BEGIN 
		IF (@ConversionName IS NULL)
		BEGIN 		
			INSERT INTO Analytics_Conversion (ConversionName,ConversionDisplayName,ConversionSiteID,ConversionGUID,ConversionLastModified)
			VALUES ('SampleConversion','Sample conversion',@SiteID,newid(),getdate())
			SET @ConversionName = 'SampleConversion'
		END
		
			INSERT INTO @CodeNames (CodeName,CreateForMultipleNodes)			
			VALUES ('conversion',0)
			
			INSERT INTO @AnalyticsDetail 
			SELECT CodeNameID,@ConversionName,NULL FROM @CodeNames  WHERE CodeName LIKE 'conversion'
	END
	
	-- Manage AB Tests
	IF (@Type = 'ABTest' OR @Type = '')
	BEGIN	
		-- Find ID of first ABTest
		DECLARE @ABTestID INT;
		SET  @ABTestID = (SELECT TOP 1 ABTestID FROM OM_ABTest WHERE ABTestSiteID = @SiteID)		
		IF (@ABTestID IS NOT NULL) 
		BEGIN
			-- GET ABTest Name
			DECLARE @ABTestName NVARCHAR(100)
			SELECT @ABTestName = ABTestName FROM OM_ABTest WHERE ABTestID = @ABTestID;
			INSERT INTO @CodeNames (CodeName,CreateForMultipleNodes)			
			SELECT 'abconversion;'+@ABTestName+';'+ABVariantName,0 FROM OM_ABVariant WHERE ABVariantTestID = @ABTestID
			
			INSERT INTO @AnalyticsDetail 
			SELECT CodeNameID,@ConversionName,NULL FROM @CodeNames  WHERE CodeName LIKE 'abconversion;%'
			
			INSERT INTO @CodeNames (CodeName,CreateForMultipleNodes)			
			VALUES ('pageviews', 10)
			
			INSERT INTO @CodeNames (CodeName,CreateForMultipleNodes)			
			VALUES ('avgtimeonpage', 10)			
			
			INSERT INTO @AnalyticsDetail 
			SELECT CodeNameID,ABVariantPath,NodeID FROM @CodeNames,OM_ABVariant  
			JOIN View_CMS_Tree_Joined ON NodeAliasPath = ABVariantPath WHERE  NodeSiteId = @SiteID
			AND	ABVariantTestID = @ABTestID	AND CreateForMultipleNodes = 10 AND (CodeName = 'pageviews' OR CodeName ='avgtimeonpage')
		END	
	END	
	
	IF (@Type = 'MVTest' OR @Type = '')
	BEGIN
		-- Find ID of first MVTest
		DECLARE @MVTestID INT;
		SET @MVTestID = (SELECT TOP 1 MVTestID FROM OM_MVTest WHERE MVTestSiteID = @SiteID)
		IF (@MVTestID IS NOT NULL)
		BEGIN
			DECLARE @AliasPath NVARCHAR(200);
			DECLARE @TestName NVARCHAR(200);
			DECLARE @Culture NVARCHAR(200);
			SET @AliasPath = (SELECT TOP 1 MVTestPage FROM OM_MVTest WHERE MVTestID = @MVTestID)
			SET @TestName = (SELECT TOP 1 MVTestName FROM OM_MVTest WHERE MVTestID = @MVTestID)
			SET @Culture =  (SELECT TOP 1 MVTestCulture FROM OM_MVTest WHERE MVTestID = @MVTestID)
			
			INSERT INTO @CodeNames (CodeName,CreateForMultipleNodes)
			SELECT 'mvtconversion;'+@TestName + ';'+ MVTCombinationName,0 FROM OM_MVTCombination WHERE MVTCombinationPageTemplateID IN
			(
				SELECT DocumentPageTemplateID FROM View_CMS_Tree_Joined WHERE NodeAliasPath = @AliasPath AND 
				(DocumentCulture = @Culture OR @Culture IS NULL) AND NodeSiteID = @SiteID
				
			)	
			INSERT INTO @AnalyticsDetail 
			SELECT CodeNameID,@ConversionName,NULL FROM @CodeNames  WHERE CodeName LIKE 'mvtconversion;%'						
			
			-- page views for MVTest
			INSERT INTO @CodeNames (CodeName,CreateForMultipleNodes)			
			VALUES ('pageviews', 0)
			
			DECLARE @MVPV INT
			SET @MVPV = (select scope_identity())
			
			INSERT INTO @AnalyticsDetail 
			SELECT @MVPV,@AliasPath,(SELECT TOP 1 NodeID FROM View_CMS_Tree_Joined WHERE NodeAliasPath =@AliasPath AND NodeSiteID = @SiteID)
			
			-- AVG time on page for mvt
			INSERT INTO @CodeNames (CodeName,CreateForMultipleNodes)			
			VALUES ('avgtimeonpage', 0)			
			SET @MVPV = (select scope_identity())
			
			INSERT INTO @AnalyticsDetail 
			SELECT @MVPV,@AliasPath,(SELECT TOP 1 NodeID FROM View_CMS_Tree_Joined WHERE NodeAliasPath =@AliasPath AND NodeSiteID = @SiteID)
		END		
	END		
	
	IF (@Type = 'campaign' OR @Type = '') 
	BEGIN
		DECLARE @CampaignName NVARCHAR(100)
		SET @CampaignName = (SELECT TOP 1 CampaignName FROM Analytics_Campaign WHERE CampaignSiteID = @SiteID)
		IF (@CampaignName IS NULL)
		BEGIN 
			INSERT INTO Analytics_Campaign (CampaignName,CampaignDisplayName,CampaignSiteID,CampaignLastModified,CampaignGUID)
			VALUES ('SampleCampaign','Sample campaign',@SiteID,getdate(),newid())
			SET @CampaignName = 'SampleCampaign'
		END
			INSERT INTO @CodeNames (CodeName,CreateForMultipleNodes)
			VALUES ('campconversion;'+@CampaignName,0)
			
			INSERT INTO @AnalyticsDetail 
			SELECT CodeNameID,@ConversionName,NULL FROM @CodeNames  WHERE CodeName LIKE 'campconversion;%'	
			
			INSERT INTO @CodeNames (CodeName,CreateForMultipleNodes)
			VALUES ('campaign',0)
			
			DECLARE @CampaignCodeID INT
			SET @CampaignCodeID = (SELECT SCOPE_IDENTITY());
			
			INSERT INTO @AnalyticsDetail
			VALUES (@CampaignCodeID,@CampaignName,NULL)		
	END	
	
	-- Create node ids and paths
	DECLARE @CodeNameID INT;
	DECLARE @NodeID INT;
	DECLARE @NodeAliasPath NVARCHAR(300);
	DECLARE @CultureCode NVARCHAR(50);
	DECLARE @CreateForMultipleNodes BINARY;
	DECLARE @ObjectName NVARCHAR (150)
	 
	 -- JOIN First X Trees for each codename With CreateForMultipleNodes set
	DECLARE codenameDetails CURSOR FOR
	 SELECT CodeNameID,CreateForMultipleNodes,NodeID,NodeAliasPath FROM @CodeNames 
	   LEFT JOIN (SELECT DISTINCT TOP 1 NodeID,NodeAliasPath,NodeLevel,NodeOrder FROM View_CMS_Tree_Joined WHERE NodeSiteID = @SiteID AND NodeLevel > 0
	   ORDER BY NodeLevel,NodeOrder ASC ) AS X ON 1=1
	   WHERE CreateForMultipleNodes=1  AND (CodeName = @Type OR @Type = ''OR (@Type='abtest' AND (CodeName = 'avgtimeonpage' OR CodeName = 'pageviews' OR CodeName LIKE 'abconversion;'))
	   OR (@Type='mvtest' AND (CodeName = 'avgtimeonpage' OR CodeName = 'pageviews' OR CodeName LIKE 'mvtconversion;%')))    
	   OPEN codenameDetails    
		FETCH NEXT FROM codenameDetails INTO @CodeNameID,@CreateForMultipleNodes,@NodeID,@NodeAliasPath
		WHILE @@FETCH_STATUS = 0
		 BEGIN
			SET @ObjectName = @NodeAliasPath;
			IF (@CodeNameID = 16)
			BEGIN 
				 SELECT @ObjectName = NodeID FROM View_CMS_Tree_Joined WHERE NodeSiteID = @SiteID AND NodeLevel > 0
					AND  NodeID != @NodeID ORDER BY newid ()					
			END 
			
			INSERT INTO @AnalyticsDetail(CodeNameID,ObjectName,ObjectID) VALUES(@CodeNameID,@ObjectName,@NodeID)									
			FETCH NEXT FROM codenameDetails INTO @CodeNameID,@CreateForMultipleNodes,@NodeID,@NodeAliasPath		
		 END
		CLOSE codenameDetails
	    
	    -- Special join with nodes for statistics like (searchkeywords,searchengines) -
		-- meaning they are connected to node but with different style from pageviews
	 DECLARE externalCodenameDetails CURSOR FOR
	 SELECT CodeNames.CodeNameID,NodeID,Ext.ObjectName FROM @CodeNames AS CodeNames 
		LEFT JOIN @AnalyticsDetail AS Ext ON CodeNames.CodeNameID = Ext.CodeNameID
	   LEFT JOIN (SELECT DISTINCT TOP 1 NodeID,NodeLevel,NodeOrder FROM View_CMS_Tree_Joined WHERE NodeSiteID = @SiteID AND NodeLevel > 0
	   ORDER BY NodeLevel,NodeOrder ASC ) AS X ON 1=1
	   WHERE CreateForMultipleNodes=2  AND (CodeName = @Type OR @Type = '')    
	   
	   OPEN externalCodenameDetails    
		FETCH NEXT FROM externalCodenameDetails INTO @CodeNameID,@NodeID,@ObjectName
		WHILE @@FETCH_STATUS = 0
		 BEGIN
			INSERT INTO @AnalyticsDetail(CodeNameID,Ext.ObjectName,ObjectID) VALUES(@CodeNameID,@ObjectName,@NodeID)									
			FETCH NEXT FROM externalCodenameDetails INTO @CodeNameID,@NodeID,@ObjectName
		 END
		CLOSE externalCodenameDetails               
	       	       
		-- Delte empty statistics rows with no ids       	       
	   DELETE FROM @AnalyticsDetail WHERE ObjectID IS NULL AND CodeNameID IN (SELECT CodeNameID FROM @CodeNames
			WHERE CreateForMultipleNodes =2)        	       
	        	       
		-- Max Hits for hour
		SET @HourHitsMax = 200
		-- Select codenames
		DECLARE @Index INT;
		SET @Index = 0;
		DECLARE @CodeCount INT;
		SELECT @CodeCount = COUNT (*) FROM @CodeNames
		DECLARE @AnalyticsCodeName NVARCHAR (150)
		DECLARE @AnalyticsCulture NVARCHAR (150)
		DECLARE @AnalyticsObjectName NVARCHAR (200)
		DECLARE @AnalyticsObjectID NVARCHAR (150)	
		
	-- Insert all relevant codename
	 DECLARE allcodenames CURSOR FOR
	 SELECT CodeName,ObjectName,ObjectID,CultureCode FROM @CodeNames AS CodeNames LEFT JOIN @AnalyticsDetail AS Details ON Details.CodeNameID = CodeNames.CodeNameID
		LEFT JOIN @Cultures AS Cultures ON CodeNames.CodeName <> 'registereduser' AND CodeName NOT LIKE 'abconversion;%' AND CodeName NOT LIKE 'mvtconversion;%'
		WHERE (CodeName = @Type OR @Type = '' OR (@Type='abtest' AND (CodeName = 'avgtimeonpage' OR CodeName ='pageviews' OR CodeName LIKE 'abconversion;%')) OR (@Type='mvtest' AND (CodeName = 'avgtimeonpage' OR CodeName ='pageviews' OR CodeName LIKE 'mvtconversion;%'))
		OR (@Type = 'campaign' AND (CodeName LIKE 'campconversion;%' OR CodeName LIKE 'campaign')))	  	  	 
	 OPEN allcodenames
	  
	 FETCH NEXT FROM allcodenames INTO @AnalyticsCodeName,@AnalyticsObjectName,@AnalyticsObjectID,@AnalyticsCulture 
	 
	 WHILE @@FETCH_STATUS = 0
	 BEGIN		
	 	IF (@AnalyticsCulture IS NULL)
		BEGIN
			SET @AnalyticsCulture = (SELECT TOP 1 CultureCode FROM @Cultures)
		END
	 
		INSERT INTO Analytics_Statistics VALUES (@SiteID,@AnalyticsCodeName,@AnalyticsObjectName,@AnalyticsObjectID,@AnalyticsCulture)
		FETCH NEXT FROM allcodenames INTO @AnalyticsCodeName,@AnalyticsObjectName,@AnalyticsObjectID,@AnalyticsCulture
	 END
	 	 
	 CLOSE allcodenames
 
	   DECLARE @Constant INT
	   DECLARE @StatisticsCode NVARCHAR(50)
	   	   	   
	   DECLARE statisticsCursor SCROLL CURSOR FOR 
	   SELECT StatisticsID, StatisticsCode FROM Analytics_Statistics 
		WHERE [StatisticsSiteID] = @SiteID AND (StatisticsCode = @Type OR @Type = '' OR (@Type='abtest' AND (StatisticsCode = 'avgtimeonpage' OR StatisticsCode = 'pageviews' OR StatisticsCode LIKE 'abconversion;%'))
		OR (@Type='mvtest' AND (StatisticsCode = 'avgtimeonpage' OR StatisticsCode = 'pageviews' OR StatisticsCode LIKE 'mvtconversion;%')) 
		OR (@Type = 'campaign' AND (StatisticsCode LIKE 'campconversion;%' OR StatisticsCode LIKE 'campaign')))
	  
	   OPEN statisticsCursor;
		SET @From = dbo.Func_Analytics_DateTrim(@From,'hour');
		-- Insert Hours      
		FETCH NEXT FROM statisticsCursor INTO @CodeNameID,@StatisticsCode	
		WHILE @@FETCH_STATUS = 0
		BEGIN
			SET @Constant = ROUND((RAND()* 30),0)+1
			SET @Current =  @From;
			WHILE (@Current <= @To)
			BEGIN 		
				IF (@StatisticsCode = 'registereduser')
				BEGIN
					INSERT INTO Analytics_HourHits VALUES (@CodeNameID,@Current,DATEADD(HOUR,1,@Current),1,1);
					BREAK;
				END; 				
				
				INSERT INTO Analytics_HourHits VALUES (@CodeNameID,@Current,DATEADD(HOUR,1,@Current),ROUND((RAND()* @HourHitsMax),0)/@Constant,
					ROUND((RAND()* @HourHitsMax),0)/2);
				SET @Current = DATEADD(HOUR,6,@Current)  
			END		
			FETCH NEXT FROM statisticsCursor INTO @CodeNameID,@StatisticsCode
		END	
		   
	   -- Days --
	   SET @From = dbo.Func_Analytics_DateTrim (@From,'day')
	   DECLARE @Count INT
	   DECLARE @Value INT
	   FETCH FIRST FROM statisticsCursor INTO @CodeNameID,@StatisticsCode	 
	   WHILE @@FETCH_STATUS = 0
		 BEGIN
		   SET @Current =  @From;
		   WHILE (@Current <= @To)
		   BEGIN 	
				SELECT  @Count = SUM(HitsCount) FROM Analytics_HourHits WHERE HitsStatisticsID  = @CodeNameID
						AND HitsStartTime >= @Current AND HitsEndTime <= DATEADD (DAY,1,@Current)
				SELECT @Value = SUM(HitsValue) FROM Analytics_HourHits WHERE HitsStatisticsID  = @CodeNameID
						AND HitsStartTime >= @Current AND HitsEndTime <= DATEADD (DAY,1,@Current)
				IF (@Count > 0)
				BEGIN
					INSERT INTO Analytics_DayHits VALUES (@CodeNameID,@Current,DATEADD(DAY,1,@Current),ISNULL(@Count,0),ISNULL(@Value,0));
				END		
				SET @Current = DATEADD(DAY,1,@Current)  
			END;
		 FETCH NEXT FROM statisticsCursor INTO @CodeNameID,@StatisticsCode	
		END;
		
	   -- Weeks --  
	   FETCH FIRST FROM statisticsCursor INTO @CodeNameID,@StatisticsCode	 
	   WHILE @@FETCH_STATUS = 0
		 BEGIN
		   SET @Current =  dbo.Func_Analytics_DateTrim (@From,'week')
		   WHILE (@Current <= @To)
		   BEGIN 	
				SELECT  @Count = SUM(HitsCount) FROM Analytics_DayHits WHERE HitsStatisticsID  = @CodeNameID
						AND HitsStartTime >= @Current AND HitsEndTime <= DATEADD (DAY,7,@Current)
				SELECT @Value = SUM(HitsValue) FROM Analytics_DayHits WHERE HitsStatisticsID  = @CodeNameID
						AND HitsStartTime >= @Current AND HitsEndTime <= DATEADD (DAY,7,@Current)
			
				IF (@Count > 0)
				BEGIN
					INSERT INTO Analytics_WeekHits VALUES (@CodeNameID,@Current,DATEADD(DAY,7,@Current),ISNULL(@Count,0),ISNULL(@Value,0));
				END
				SET @Current = DATEADD(DAY,7,@Current)  
			END;
		 FETCH NEXT FROM statisticsCursor INTO @CodeNameID,@StatisticsCode	
		END;
		
	
		-- Months	
	   SET @From = dbo.Func_Analytics_DateTrim (@From,'month')
	   FETCH FIRST FROM statisticsCursor INTO @CodeNameID,@StatisticsCode	 
	   WHILE @@FETCH_STATUS = 0
		 BEGIN
		   SET @Current =  @From;
		   WHILE (@Current <= @To)
		   BEGIN 	
				SELECT  @Count = SUM(HitsCount) FROM Analytics_DayHits WHERE HitsStatisticsID  = @CodeNameID
						AND HitsStartTime >= @Current AND HitsEndTime <= DATEADD (Month,1,@Current)
				SELECT @Value = SUM(HitsValue) FROM Analytics_DayHits WHERE HitsStatisticsID  = @CodeNameID
						AND HitsStartTime >= @Current AND HitsEndTime <= DATEADD (Month,1,@Current)
			
				IF (@Count > 0)
				BEGIN
					INSERT INTO Analytics_MonthHits VALUES (@CodeNameID,@Current,DATEADD(Month,1,@Current),ISNULL(@Count,0),ISNULL(@Value,0));
				END
				SET @Current = DATEADD(Month,1,@Current)  
			END;
		 FETCH NEXT FROM statisticsCursor INTO @CodeNameID,@StatisticsCode	
		END;
		
		-- Years	
	   SET @From = dbo.Func_Analytics_DateTrim (@From,'year')
	   FETCH FIRST FROM statisticsCursor INTO @CodeNameID,@StatisticsCode	 
	   WHILE @@FETCH_STATUS = 0
		 BEGIN
		   SET @Current =  @From;
		   WHILE (@Current <= @To)
		   BEGIN 	
				SELECT  @Count = SUM(HitsCount) FROM Analytics_MonthHits WHERE HitsStatisticsID  = @CodeNameID
						AND HitsStartTime >= @Current AND HitsEndTime <= DATEADD (YEAR,1,@Current)
				SELECT @Value = SUM(HitsValue) FROM Analytics_MonthHits WHERE HitsStatisticsID  = @CodeNameID
						AND HitsStartTime >= @Current AND HitsEndTime <= DATEADD (YEAR,1,@Current)		  			
				
				IF (@Count > 0)
				BEGIN					
					INSERT INTO Analytics_YEARHits VALUES (@CodeNameID,@Current,DATEADD(YEAR,1,@Current),ISNULL(@Count,0),ISNULL(@Value,0));
				END
								
				SET @Current = DATEADD(YEAR,1,@Current)  
			END;
		 FETCH NEXT FROM statisticsCursor INTO @CodeNameID,@StatisticsCode	
		END;		   
	   CLOSE statisticsCursor;
	   DEALLOCATE statisticsCursor
END
