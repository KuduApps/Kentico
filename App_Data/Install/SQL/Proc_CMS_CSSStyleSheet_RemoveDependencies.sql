CREATE PROCEDURE [Proc_CMS_CSSStyleSheet_RemoveDependencies]
@ID int
AS
BEGIN
-- CMS_CssStyleSheetSite
    DELETE FROM CMS_CssStylesheetSite WHERE StylesheetID = @ID;
  -- CMS_Document
  UPDATE CMS_Document SET DocumentStylesheetID = NULL WHERE DocumentStylesheetID = @ID;
  -- CMS_Site
  UPDATE CMS_Site SET SiteDefaultStylesheetID = NULL WHERE SiteDefaultStylesheetID = @ID;
  UPDATE CMS_Site SET SiteDefaultEditorStylesheet = NULL WHERE SiteDefaultEditorStylesheet = @ID;
END
