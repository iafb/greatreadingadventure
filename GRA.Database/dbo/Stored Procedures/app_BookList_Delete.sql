﻿
CREATE PROCEDURE [dbo].[app_BookList_Delete] @BLID INT,
	@TenID INT = NULL
AS
DELETE
FROM BookListBooks
WHERE BLID = @BLID
	AND TenID = @TenID

DELETE
FROM BookList
WHERE BLID = @BLID
	AND TenID = @TenID
