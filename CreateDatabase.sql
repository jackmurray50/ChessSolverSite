--Ensure there's no current table
USE ChessSolver;

DROP TABLE IF EXISTS [dbo].[Accounts];
DROP TABLE IF EXISTS [dbo].[BoardsRelationships];
DROP TABLE IF EXISTS [dbo].[Boards];


--Table of accounts, keeping track of usernames, passwords (Which can be null if someone wants their
--account to be unprotected), and how much they contributed in terms of calculated moves.
CREATE TABLE [dbo].[Accounts]
(
	[Id] INT IDENTITY (1,1) NOT NULL PRIMARY KEY,
	[Password] CHAR(64), --Can be null, as a password won't be required.
	[Name] VARCHAR(32) NOT NULL,
	[Progress] BIGINT NOT NULL
)

--All the registered board-states so far.
CREATE TABLE [dbo].[Boards]
(
	[Id] INT IDENTITY (1,1) NOT NULL PRIMARY KEY,
	[BoardState] NVARCHAR(72) NOT NULL,
	--Will always be replaced with the lowest value
	[TurnsSinceCapture] INT NOT NULL,
	[Turn] VARCHAR(5) NOT NULL CHECK ([Turn] in('WHITE', 'BLACK')),
	--caching win-state so lookup is faster
	[WinState] VARCHAR(5) NOT NULL CHECK ([WinState] in ('WHITE', 'BLACK', 'DRAW', 'TBD')) DEFAULT ('TBD'),
	[VerificationAmount] INT
)

--Relationships between boards; its a many-to-many relationship (A parent can have many children, a child can have many children)
--This defines how a board state can evolve as the turns progress
CREATE TABLE [dbo].[BoardsRelationships]
(
	[Id] INT IDENTITY (1,1) NOT NULL PRIMARY KEY,
	[ChildId] INT NOT NULL FOREIGN KEY REFERENCES [Boards]([Id]),
	[ParentId] INT NOT NULL FOREIGN KEY REFERENCES [Boards]([Id])

);