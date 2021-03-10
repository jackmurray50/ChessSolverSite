using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace chess_solver_client
{
    class Program
    {
        static readonly HttpClient client = new HttpClient();

        static private bool IsVerbose = false;
        static private int MemoryAllowance = 512;
        static private string username;
        static private string password;

        static readonly string  ConnectionString = "https://localhost:44394/";
        static readonly string GetLeafString = "api/board/GetLeaf";
        static async Task Main(string[] args)
        {
            //Step 1: See if there's any args we care about
            SetArgs(new List<string>(args));
            //Step 2: If necessary, sign in

            //Step 3: Request a board from the server
            await GetBoard();

            //Step 4: Find the possible moves for the current board state and add them to a stack
            //Stack <move> =  new Stack<move>();

        }

        /// <summary>
        /// Gets a board from the Chess Solver Server.
        /// </summary>
        /// <returns></returns>
        static async Task GetBoard()
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
                    Console.WriteLine(responseBody);
                }


            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
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
