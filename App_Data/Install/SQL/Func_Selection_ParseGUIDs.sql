-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE FUNCTION [Func_Selection_ParseGUIDs]
(	
	@SelectionValues nvarchar(max)
)
RETURNS @List TABLE ( ItemGUID uniqueidentifier )
AS
BEGIN
	DECLARE @StartIndex int
	DECLARE @EndIndex int
	DECLARE @Value nvarchar(450)
	
	SET @StartIndex = 1
	SET @EndIndex = CHARINDEX(',', @SelectionValues)
	WHILE @EndIndex > 0
	BEGIN
		SET @Value = LTRIM(RTRIM(SUBSTRING(@SelectionValues, @StartIndex, @EndIndex - @StartIndex)));
		IF @Value <> ''
		BEGIN
			INSERT INTO @List VALUES (@Value)
		END
		SET @StartIndex = @EndIndex + 1
		SET @EndIndex = CHARINDEX(',', @SelectionValues, @StartIndex)
	END
	
	IF LEN(@SelectionValues) > @StartIndex
	BEGIN
		SET @Value = LTRIM(RTRIM(SUBSTRING(@SelectionValues, @StartIndex, LEN(@SelectionValues) + 1 - @StartIndex)));
		IF @Value <> ''
		BEGIN
			INSERT INTO @List VALUES (@Value)
		END
	END
	
	RETURN
END
