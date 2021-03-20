--Table of accounts, keeping track of usernames, passwords (Which can be null if someone wants their
--account to be unprotected), and how much they contributed in terms of calculated moves.
CREATE TABLE [dbo].[Accounts]
(
	[Id] INT IDENTITY (1,1) NOT NULL PRIMARY KEY,
	[Password] CHAR(64), --Can be null, as a password won't be required.
	[Name] VARCHAR(32) NOT NULL,
	[Progress] BIGINT NOT NULL
)