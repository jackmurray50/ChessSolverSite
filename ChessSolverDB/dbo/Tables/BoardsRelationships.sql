--Relationships between boards; its a many-to-many relationship (A parent can have many children, a child can have many children)
--This defines how a board state can evolve as the turns progress
CREATE TABLE [dbo].[BoardsRelationships]
(
	[Id] INT IDENTITY (1,1) NOT NULL PRIMARY KEY,
	[ChildId] INT NOT NULL FOREIGN KEY REFERENCES [Boards]([Id]),
	[ParentId] INT NOT NULL FOREIGN KEY REFERENCES [Boards]([Id])

);