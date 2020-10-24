CREATE TABLE [dbo].[CLIENTS]
(
	[Client_ID] BIGINT NOT NULL PRIMARY KEY, 
    [Booking_ID] INT NULL, 
    [Client_Name] NCHAR(30) NULL, 
    [Client_Surname] NCHAR(30) NULL, 
    [Client_Email] NCHAR(30) NULL
)
