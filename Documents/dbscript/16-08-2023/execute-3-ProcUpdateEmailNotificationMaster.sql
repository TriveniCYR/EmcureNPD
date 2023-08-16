USE [EmcureNPDDev]
GO

/****** Object:  StoredProcedure [dbo].[ProcUpdateEmailNotificationMaster]    Script Date: 16-08-2023 15:38:56 ******/
DROP PROCEDURE [dbo].[ProcUpdateEmailNotificationMaster]
GO

/****** Object:  StoredProcedure [dbo].[ProcUpdateEmailNotificationMaster]    Script Date: 16-08-2023 15:38:56 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE proc [dbo].[ProcUpdateEmailNotificationMaster]
@NotificationId bigint,
@Success varchar(12) out
AS
BEGIN
  UPDATE Master_Notification
  set IsEmailSent=1,
      SentDatetime=GETDATE()
  WHERE NotificationId=@NotificationId
  set @Success='success'
  select @Success
END


GO


