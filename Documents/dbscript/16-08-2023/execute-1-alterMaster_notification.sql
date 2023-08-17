alter table Master_Notification
add IsEmailSent bit default(0) not null
go
alter table Master_Notification
add SentDatetime datetime
go