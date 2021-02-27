CREATE TABLE [dbo].[Account]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[Password] CHAR(64), --Can be null, as a password won't be required.
	[Name] VARCHAR(32) NOT NULL,
	[Progress] BIGINT NOT NULL
)
