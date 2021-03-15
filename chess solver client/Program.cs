using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Diagnostics;

namespace chess_solver_client
{
    class Program
    {
        static readonly HttpClient client = new HttpClient();

        static private bool IsVerbose = false;
        static private int MemoryAllowance = 536870091;
        static private string username;
        static private string password;

        static readonly string  ConnectionString = "https://localhost:44394/";
        static readonly string GetLeafString = "api/board/GetLeaf";
        static async Task Main(string[] args)
        {
            //Step 1: See if there's any args we care about
            SetArgs(new List<string>(args));
            //Step 2: If necessary, sign in

            //Step 3: Request a board from the server. In a loop so
            //it can repeat later. For now its set to run once
            int i = 0;
            while (i == 0)
            {
                i = 1;
                BoardViewModel temp = await GetBoard();
                ChessBoard root = new ChessBoard(0, temp.BoardState, temp.TurnsSinceCapture, temp.Turn);

                if (IsVerbose)
                {
                    Console.WriteLine("Root Board: \n" + root.UglyToString());
                }
                //Step 4: Find the possible moves for the current board state and add them to a stack
                Stack<Move> Stack = new Stack<Move>();
                List<ChessBoard> Boards = new List<ChessBoard>();
                List<BoardRelationshipViewModel> Relationships = new List<BoardRelationshipViewModel>();
                List<BoardViewModel> FinishedBoards = new List<BoardViewModel>();
                
                Boards.Add(root);
                //Step 5: Initial assignment of board and moves
                foreach (Move m in root.PossibleMoves())
                {
                    Stack.Push(m);
                }
                //Step 6: Initialize Process so we can track memory usage.
                //Tracking memory usage is expensive so we'll only do it before adding to the stack
                Process CurProcess = Process.GetCurrentProcess();
                Console.WriteLine(CurProcess.PrivateMemorySize64);
                //Step 7: work through the Stack
                bool KeepAddingToStack = true;
                while(Stack.Count > 0)
                {
                    Move m = Stack.Pop();
                    ChessBoard b =
                        DeepCopy.DeepCopier.Copy<ChessBoard>(Boards[m.BoardId]);
                    Boards[m.BoardId].Moves -= 1;
                    if(Boards[m.BoardId].Moves == 0)
                    {
                        //No longer need the Board saved, so we remove it and create a BoardViewModel
                        //TODO
                        ChessBoard CurBoard = Boards[m.BoardId];
                        BoardViewModel bvm = new BoardViewModel();
                        bvm.Id = m.BoardId;
                        bvm.IsFinished = true;
                        if(CurBoard.Turn == Colour.BLACK)
                        {
                            bvm.Turn = "BLACK";
                        }
                        else
                        {
                            bvm.Turn = "WHITE";
                        }
                        bvm.TurnsSinceCapture = CurBoard.TurnsSinceCapture;
                        bvm.BoardState = CurBoard.UglyToString();
                        FinishedBoards.Add(bvm);
                        Boards.Remove(Boards[m.BoardId]);
                    }
                    //Make the move and see what its result is
                    int result = b.Move(m);
                    //Set it a new Id
                    b.Id = Boards.Count;
                    //Add a new relationship
                    Relationships.Add(new BoardRelationshipViewModel(Relationships.Count, b.Id, m.BoardId));
                    if (IsVerbose)
                    {
                        DisplayBoard(b, m);
                    }
                    Boards.Add(b);
                    if(result == 0)
                    {
                        //Only do this if its been 50 checks since, to cut down on cpu time
                        if(b.Id % 50 == 0)
                        {
                            CurProcess.Refresh();
                        }
                        //continues if there's RAM available
                        if(CurProcess.PrivateMemorySize64 >= MemoryAllowance)
                        {
                            KeepAddingToStack = false;
                        }
                        if (KeepAddingToStack)
                        {
                            foreach (Move move in b.PossibleMoves())
                            {
                                Stack.Push(move);
                            }
                        }
                    }
                    else //If there's a win
                    {

                    }
                }

                Console.WriteLine($"Finished first Stack. Board count: {FinishedBoards.Count} Relationship count: {Relationships.Count}\nUploading...");
            }
        }

        static void DisplayBoard(ChessBoard board, Move move)
        {
            string output = board.ToString();
            foreach (char c in output)
            {
                //the string is formatted so the place the piece came from is
                //in red, and the place its going is in green
                if (c == '[')
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                }
                else if (c == ']' || c == '}')
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                else if (c == '{')
                {
                    Console.BackgroundColor = ConsoleColor.Green;
                }
                else
                {
                    Console.Write(c);
                }
            }
        }

        /// <summary>
        /// Gets a board from the Chess Solver Server.
        /// </summary>
        /// <returns></returns>
        static async Task<BoardViewModel> GetBoard()
        {
            if (IsVerbose)
            {
                Console.WriteLine("Getting leaf from " + ConnectionString + GetLeafString);
            }
            // Call asynchronous network methods in a try/catch block to handle exceptions.
            try
            {
                HttpResponseMessage response = await client.GetAsync(ConnectionString + GetLeafString);
                if(response.StatusCode == System.Net.HttpStatusCode.OK && IsVerbose)
                {
                    Console.WriteLine("Connection successful");
                }

                string responseBody = await response.Content.ReadAsStringAsync();
                if (IsVerbose)
                {
                    Console.WriteLine("Json response: " + responseBody);
                }
                return JsonConvert.DeserializeObject<BoardViewModel>(responseBody);

            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                return null;
            }
        }

        /// <summary>
        /// Sets the various input flags
        /// </summary>
        /// <param name="args">A list of the input flags</param>
        static void SetArgs(List<string> args)
        {
            //-v: Verbose
            //-m [number]: The amount of megabytes of memory to use (Defaults to 500)
            //-user: The username to use
            //-pass: The password to use
            try
            {
                if (args.Contains("-v"))
                {
                    IsVerbose = true;
                }
                if (args.Contains("-m"))
                {
                    MemoryAllowance = int.Parse(args[args.IndexOf("-m") + 1]);
                }
                if (args.Contains("-user"))
                {
                    username = args[args.IndexOf("-user") + 1];
                    if (args.Contains("-pass"))
                    {
                        password = args[args.IndexOf("-pass") + 1];
                    }
                }
            }catch(IndexOutOfRangeException ex)
            {
                Console.Write(ex);
                Environment.Exit(-1);
            }

        }
    }
}
