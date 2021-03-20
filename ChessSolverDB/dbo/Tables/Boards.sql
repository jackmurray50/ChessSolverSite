--All the registered board-states so far.
CREATE TABLE [dbo].[Boards]
(
	[Id] INT IDENTITY (1,1) NOT NULL PRIMARY KEY,
	[BoardState] NVARCHAR(72) NOT NULL,
	--Will always be replaced with the lowest value
	[TurnsSinceCapture] INT NOT NULL,
	[Turn] VARCHAR(5) NOT NULL CHECK ([Turn] in('WHITE', 'BLACK')),
	--caching win-state so lookup is faster
	[WinState] VARCHAR(5) NOT NULL CHECK ([WinState] in ('WHITE', 'BLACK', 'DRAW', 'NA')) DEFAULT ('NA'),
	[IsFinished] BIT NOT NULL,
	[VerificationAmount] INT
)