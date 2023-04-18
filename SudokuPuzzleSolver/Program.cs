using System;

class SudokuPuzzleSolver
{
    const int size = 9;

    static bool Solve(int[,] board, int row, int col) 
    {
        //If end of the board, then puzzle has been solved
        if (row == size - 1 && col == size) {
            return true;
        }

        //If end of columns, move to the next row and start from column 0 again
        if (col == size) {
            row++;
            col = 0;
        }

        //If the current cell already has a value, move on to the next column.
        if (board[row, col] != 0) {
            return Solve(board, row, col + 1);
        }

        //If a valid value, set value in [row,col]. Move to next col
        for (int num = 1; num <= size; num++) {
            if (IsValid(board, row, col, num)) {
                board[row, col] = num;

                if (Solve(board, row, col + 1)) {
                    return true;
                }
            }
            //If no valid number works for the current cell, reset it to 0 and backtrack.
            board[row, col] = 0;
        }
        return false;
    }

    static void PrintSudokuBoard(int[,] board) 
    {
        for (int row = 0; row < size; row++)
        {
            for (int col = 0; col < size; col++) {
                Console.Write(board[row, col] + " ");
            }
            Console.WriteLine();
        }
    }

    static bool IsValid(int[,] board, int row, int col, int num)
    {
        for (int i = 0; i < 9; i++) {
            int cellValue;

            //Row - if number already in the [row,col] - not valid
            cellValue = board[i, col];
            if (cellValue != '0' && cellValue == num) {
                return false; 
            }

            //Column - if number already in the [row,col] - not valid
            cellValue = board[row, i];
            if (cellValue != '0' && cellValue == num) {
                return false;
            }

            //3x3 block - if number already exists in the block - not valid
            int blockRow = 3 * (row / 3) + i / 3; //Determine the row index of the current cell in the 3x3 block
            int blockCol = 3 * (col / 3) + i % 3; //Determine the column index of the current cell in the 3x3 block
            cellValue = board[blockRow, blockCol];
            if (cellValue != '0' && cellValue == num) {
                return false;
            }
        }
        // If num does not already exist in the same row, column, or 3x3 block as the specified cell,
        // return true indicating that it is a valid
        return true;
    }
    static void Main()
    {
        //Test different boards by copying from the Test boards.txt file and replacing board in Board.txt
        //Read the file with board puzzle so that it can be solved
        string[] lines = File.ReadAllLines(@"Input\Board.txt");

        int[,] board = new int[9, 9];

        for (int row = 0; row < lines.Length; row++) {

            string[] values = lines[row].Split(',');

            for (int col = 0; col < values.Length; col++) {
                board[row, col] = int.Parse(values[col]);
            }
        }

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(" Original Board");
        Console.WriteLine("-----------------");
        PrintSudokuBoard(board);
        Console.WriteLine();
        Console.ResetColor();

        if (Solve(board, 0, 0)) {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  Solved Board");
            Console.WriteLine("-----------------");
            PrintSudokuBoard(board);
            Console.ResetColor();
        }
        else
            Console.WriteLine("No solution for the Puzzle");
    }
}
        