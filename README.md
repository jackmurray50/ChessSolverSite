# ChessSolverSite
ChessSolver is a website and client with a single (doomed) purpose: To brute-force chess, and solve it. There's an estimated 10<sup>120</sup> possible games, and 10<sup>50</sup> possible positions. While I started to create an application that calculates them, running it on a single computer simply isn't going to get it done with any reasonable amount of speed. So I started working on ChessSolverSite to make it technically possible, though it's still practically impossible.

ChessSolverSite contains 2 main parts: The server and the client. The server's purpose is to store board-states, represented as such:

♜♞♝♛♚♝♞♜<br>
♟︎♟︎♟︎♟︎♟︎♟︎♟︎♟︎<br>
X#X#X#X#<br>
X#X#X#X#<br>
X#X#X#X#<br>
X#X#X#X#<br>
♙♙♙♙♙♙♙♙<br>
♖♘♗♕♔♗♘♖

The database will also hold board relationships, represented as a many-to-many relationship between parents and children. This will help ensure that processing time and drive space isn't wasted with 2 different sets of moves that diverge then merge further down the line.

The other purpose of the server is to supply clients with board-states they can work on solving. Because the client only solves possible legal moves, it's state-agnostic and so will solve any board given to it.


The client is a command line application that accepts a few values at start-up: -v, -m, -user, and -pass. -v will enter verbose mode, making the client display some information about what's going on. -m limits the maximum amount of memory the client can use. Once the memory is reached, the client stops solving and starts packaging the information to be returned to the server. -user and -pass tell the server the username and password the client wants to use, only really used for tracking who did how much work.
It should be noted that the server is NOT secure. Passwords are optional, they're not salted (They are encrypted though), there's no way to retrieve a forgotten password or to change it. That may be changed in the future, but for now its considered an unimportant feature.

In short: The server passes a board-state to the client. The client plays games, then returns them back to the server, which then adds those to the database.

Useful reading: 
[Shannon's Number](https://en.wikipedia.org/wiki/Shannon_number), an estimation of how many possible games there are.

[Solving Chess](https://en.wikipedia.org/wiki/Solving_chess) is finding ways to optimize a strategy for chess in a way that one player can always force a victory.

[Solved Games](https://en.wikipedia.org/wiki/Solved_game) are games whose outcome can be predicted from any position.

