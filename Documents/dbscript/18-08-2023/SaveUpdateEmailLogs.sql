
GO

/****** Object:  StoredProcedure [emcurenpddev_dbUser].[SaveUpdateEmailLogs]    Script Date: 18-08-2023 14:10:48 ******/
DROP PROCEDURE dbo.[SaveUpdateEmailLogs]
GO

/****** Object:  StoredProcedure [emcurenpddev_dbUser].[SaveUpdateEmailLogs]    Script Date: 18-08-2023 14:10:48 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--select * from Master_EmailLog
Create PROC [dbo].[SaveUpdateEmailLogs]
@EmailLogId bigint=0,
@ToEmailAddress nvarchar(2000),
@Subject nvarchar(2000),
@SentSuccessfully bit=0
AS
BEGIN
if(not exists(select * from Master_EmailLog where EmailLogId=@EmailLogId))
BEGIN
  INSERT INTO Master_EmailLog
              (ToEmailAddress,Subject,SentSuccessfully)
         VALUES(@ToEmailAddress,@Subject,@SentSuccessfully)
END
else if(exists(select * from Master_EmailLog where EmailLogId=@EmailLogId))
BEGIN
UPDATE Master_EmailLog
SET ToEmailAddress=@ToEmailAddress,
    Subject=@Subject,
	SentSuccessfully=@SentSuccessfully,
	CreatedDate=GETDATE()
WHERE EmailLogId=@EmailLogId
END
END
GO


