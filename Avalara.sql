USE master;
GO
IF DB_ID (N'Avalara') IS NOT NULL
DROP DATABASE Avalara;
GO

CREATE DATABASE Avalara;
GO

USE Avalara;
GO

IF OBJECT_ID (N'dbo.todo_list', N'U') IS NOT NULL  
DROP TABLE dbo.todo_list;  
GO  


CREATE TABLE dbo.todo_list
(
 [todo_list_id] uniqueidentifier CONSTRAINT PK_todo_list PRIMARY KEY CLUSTERED CONSTRAINT DF_todo_list_id DEFAULT NEWSEQUENTIALID()
 ,[name] varchar(30) NOT NULL
 ,[create_date] datetimeoffset CONSTRAINT DF_todo_list_create_date DEFAULT SYSDATETIMEOFFSET() NOT NULL
);

IF OBJECT_ID (N'dbo.task', N'U') IS NOT NULL  
DROP TABLE dbo.task;  
GO  


CREATE TABLE dbo.task
(
 [task_id] INT CONSTRAINT PK_task PRIMARY KEY CLUSTERED IDENTITY 
 ,[todo_list_Id] uniqueidentifier NOT NULL CONSTRAINT FK_todo_list_task FOREIGN KEY REFERENCES [dbo].[todo_list] ([todo_list_id])
 ,[subject] varchar(30) NOT NULL
 ,[description] varchar(30) NOT NULL
 ,[create_date] datetimeoffset CONSTRAINT DF_task_create_date DEFAULT SYSDATETIMEOFFSET() NOT NULL
 ,[status] varchar(16) NOT NULL CHECK ([status] IN ('NotStarted', 'InProgress', 'Completed'))
)