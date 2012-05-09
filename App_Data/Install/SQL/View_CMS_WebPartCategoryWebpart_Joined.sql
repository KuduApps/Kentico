CREATE VIEW [View_CMS_WebPartCategoryWebpart_Joined]
AS
(SELECT CategoryID AS ObjectID, CategoryName AS CodeName, 
CategoryDisplayName AS DisplayName, CategoryParentID AS ParentID,
CategoryGUID AS GUID, CategoryLastModified AS LastModified      
      ,CategoryImagePath
      ,CategoryPath AS ObjectPath
      ,CategoryOrder
      ,CategoryLevel AS ObjectLevel
      ,CategoryChildCount
      ,CategoryWebPartChildCount
      ,ISNULL(CategoryChildCount,0) + ISNULL(CategoryWebPartChildCount,0) AS CompleteChildCount
      ,NULL AS WebPartParentID
      ,NULL AS WebPartFileName
      ,NULL AS WebPartGUID     
      ,NULL AS WebPartType
      ,NULL AS WebPartLoadGeneration
      ,NULL WebPartLastSelection  
      ,NULL AS WebPartDescription 
      ,'webpartcategory' AS ObjectType     
FROM         CMS_WebPartCategory)
UNION ALL
(
SELECT WebPartID AS ObjectID, WebPartName AS CodeName, 
WebPartDisplayName AS DisplayName, WebPartCategoryID AS ParentID,
WebPartGUID AS GUID, WebPartLastModified AS LastModified
	  ,NULL AS CategoryImagePath
      ,CMS_WebPartCategory.CategoryPath + '/' + WebPartName AS ObjectPath
      ,NULL AS CategoryOrder
      ,CMS_WebPartCategory.CategoryLevel + 1 AS ObjectLevel
      ,0 AS CategoryChildCount
      ,0 AS CategoryWebPartChildCount 
      ,0 AS CompleteChildCount
      ,WebPartParentID 
      ,WebPartFileName
      ,WebPartGUID     
      ,WebPartType
      ,WebPartLoadGeneration
      ,WebPartLastSelection 
      ,CAST(WebPartDescription AS nvarchar(1000))   
      ,'webpart' AS ObjectType  
FROM  CMS_WebPart LEFT JOIN CMS_WebPartCategory ON CMS_WebPart.WebPartCategoryID = CMS_WebPartCategory.CategoryID
)
GO
