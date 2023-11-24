using System.Numerics;

namespace TicTacToe
{
    internal class Program
    {
        const char PLAYER_X = 'X';
        const char PLAYER_O = 'O';

        const int ROW_VALUE_NUMBER = 3;
        const int COL_VALUE_NUMBER = 4;

        const int MIN_MOVES_PLAYED = 1;

        const int MIN_ROW_VALUE_NUMBER = 0;
        const int MAX_ROW_VALUE_NUMBER = ROW_VALUE_NUMBER;

        const int MIN_COL_VALUE_NUMBER = 0;
        const int MAX_COL_VALUE_NUMBER = COL_VALUE_NUMBER;

        const int FIRST_NUMBER = 1;
        const int SECOND_NUMBER = 2;

        const int A_VALUE = 65;

        static char colLetter;
    
        static void InitializingBoard(char[,] board)
        {
            for (int row = 0; row < ROW_VALUE_NUMBER; row++)
            {
                for (int col = 0; col < COL_VALUE_NUMBER; col++)
                {
                    board[row, col] = ' ';
                }
            }
        }

        static void PrintBoard(char[,] board)
        {
            Console.Write("    ");
            for (int i = 0; i < COL_VALUE_NUMBER; i++)
            {
                colLetter = (char)(A_VALUE + i);
                Console.Write(colLetter + "   " );                
            }

            Console.WriteLine();

            for (int row = FIRST_NUMBER; row <= ROW_VALUE_NUMBER; row++)
            {
                Console.Write(row + " | ");
                for (int col = 0; col < COL_VALUE_NUMBER; col++)
                {
                    Console.Write(board[row - FIRST_NUMBER, col]);
                    Console.Write(" | ");
                }

                Console.WriteLine();
            }
        }

        static char SwitchPlayer(char currentPlayer)
        {
            if (currentPlayer == PLAYER_X)
            {
                return PLAYER_O;
            }
            else
            {
                return PLAYER_X;
            }
        }

        static char GettingValuesFromPlayer(char[,] board, char player)
        {
            bool correctNumberAdded = false;
            bool correctNumberAdded1 = false;

            int row;
            int col;

            do
            {
                Console.Write("Please enter row: ");

                bool successParse = int.TryParse(Console.ReadLine(), out row);

                if (successParse == false ||
                    row < MIN_ROW_VALUE_NUMBER || row > MAX_ROW_VALUE_NUMBER  )
                {
                    Console.WriteLine("Not in range.");
                }
                else
                {
                    correctNumberAdded = true;
                }

                //връщане на стойността на индексите
                row = row - FIRST_NUMBER;

            }
            while (correctNumberAdded == false);

            do
            {
                Console.Write("Please enter col: ");

                bool successParse = int.TryParse(Console.ReadLine(), out col);

                if (successParse == false ||
                    col < MIN_COL_VALUE_NUMBER || col > MAX_COL_VALUE_NUMBER )
                {
                    Console.WriteLine("Not in range.");
                }
                else
                {
                    correctNumberAdded1 = true;
                }

                //връщане на стойността на индексите
                col = col - FIRST_NUMBER ;

            }
            while (correctNumberAdded1 == false);

            while (board[row, col] == PLAYER_X || board[row, col] == PLAYER_O)
            {
                Console.WriteLine("Not a valid turn:");

                GettingValuesFromPlayer(board, player);

                if (player == PLAYER_X)
                {
                    return PLAYER_O;
                }
                else
                {
                    return PLAYER_X;
                }
                    
            }
            
            board[row, col] = player;
            return board[row, col];
        }

        static bool CheckForWin(bool endGame,char [,] board, char player)
        {
            for (int row = 0; row < ROW_VALUE_NUMBER; row++)
            {
                bool rowWin = true;

                for (int col = 0; col < COL_VALUE_NUMBER; col++)
                {
                    if (board[row, col] != player)
                    {
                        rowWin = false;
                        break;
                    }
                }
                if (rowWin)
                {
                    Console.WriteLine("Player " + player + " wins");
                    endGame = true;
                    return endGame;
                }
  
            }

            for (int col = 0; col < COL_VALUE_NUMBER; col++)
            {
                bool colWin = true;

                for (int row = 0; row < ROW_VALUE_NUMBER; row++)
                {
                    if (board[row, col] != player)
                    {
                        colWin = false;
                        break;
                    }
                }
                if (colWin)
                {
                    Console.WriteLine("Player " + player + " wins");
                    endGame = true;
                    return endGame;
                }
            }

            bool diagonalWin1 = true;
            bool diagonalWin2 = true;
            if (ROW_VALUE_NUMBER != COL_VALUE_NUMBER)
            {
                for (int i = 0; i < ROW_VALUE_NUMBER; i++)
                {
                    if (board[i, i] != player)
                    {
                        diagonalWin1 = false;
                    }

                    if (board[i, ROW_VALUE_NUMBER - i ] != player)
                    {
                        diagonalWin2 = false;
                    }
                }
                if (diagonalWin1 || diagonalWin2)
                {
                    Console.WriteLine("Player " + player + " wins");
                    endGame = true;
                    return endGame;
                }
            }
            if(ROW_VALUE_NUMBER == COL_VALUE_NUMBER)
            {
                for (int i = 0; i < ROW_VALUE_NUMBER; i++)
                {
                    if (board[i, i] != player)
                    {
                        diagonalWin1 = false;
                    }

                    if (board[i, ROW_VALUE_NUMBER - i - FIRST_NUMBER ] != player)
                    {
                        diagonalWin2 = false;
                    }
                }
                if (diagonalWin1 || diagonalWin2)
                {
                    Console.WriteLine("Player " + player + " wins");
                    endGame = true;
                    return endGame;
                }
            }

            return endGame;
        }

        

        static void Main(string[] args)
        {
            bool endGame = false;
            int movesPlayed = 0;
            char player = PLAYER_X;
            char[,] board = new char[ROW_VALUE_NUMBER, COL_VALUE_NUMBER];

            InitializingBoard(board);

            do
            {
                Console.Clear();
                PrintBoard(board);

                Console.WriteLine("It's player " + player + "'s turn");

                GettingValuesFromPlayer(board, player);

                PrintBoard(board);

                endGame = CheckForWin(endGame, board, player);

                movesPlayed += MIN_MOVES_PLAYED;

                player = SwitchPlayer(player);
            }
            while (endGame == false || movesPlayed == ROW_VALUE_NUMBER * COL_VALUE_NUMBER);

            if (movesPlayed == ROW_VALUE_NUMBER * COL_VALUE_NUMBER)
            {
                Console.WriteLine("DRAW");
            }
        }


    }
}