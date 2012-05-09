-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE FUNCTION [Func_Selection_ParseIDs]
(	
	@SelectionValues nvarchar(max)
)
RETURNS @List TABLE ( ItemID int )
AS
BEGIN
	DECLARE @StartIndex int
	DECLARE @EndIndex int
	DECLARE @Value nvarchar(450)
	DECLARE @Total int
	
	SET @StartIndex = 1
	SET @EndIndex = CHARINDEX(',', @SelectionValues)
	
	WHILE @EndIndex > 0
	BEGIN
		SET @Value = LTRIM(RTRIM(SUBSTRING(@SelectionValues, @StartIndex, @EndIndex - @StartIndex)));
		IF @Value <> ''
		BEGIN
			INSERT INTO @List VALUES (CAST(@Value AS int))
		END
		SET @StartIndex = @EndIndex + 1
		SET @EndIndex = CHARINDEX(',', @SelectionValues, @StartIndex)
	END
	
	IF LEN(@SelectionValues) > @StartIndex
	BEGIN
		SET @Value = LTRIM(RTRIM(SUBSTRING(@SelectionValues, @StartIndex, LEN(@SelectionValues) + 1 - @StartIndex)));
		IF @Value <> ''
		BEGIN
			INSERT INTO @List VALUES (CAST(@Value AS int))
		END
	END
	
	RETURN
END
