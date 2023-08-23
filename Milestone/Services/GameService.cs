using Milestone.Models;
using System.Diagnostics;

namespace Milestone.Services
{
    public class GameService
    {
        public static void printBoards(BoardModel obj)
        {
            int boardSize = obj.getSize();

            Debug.Write("  ");
            for (int i = 0; i < boardSize; i++)
            {
                Debug.Write((i) + "   ");
            }
            Debug.WriteLine("");
            for (int row = 0; row < boardSize; row++)
            {
                for (int col = 0; col < boardSize; col++)
                {
                    if (col == 0)
                    {
                        line(boardSize);
                        Debug.WriteLine("+");
                    }
                    if (obj.Grid[row, col].Live == true)
                    {
                        Debug.Write("| * ");
                    }
                    else
                    {
                        Debug.Write("| " + obj.Grid[row, col].Neighbors + " ");
                    }
                }
                Debug.Write("|");
                Debug.Write(" " + (row));
                Debug.WriteLine("");
            }
            line(boardSize);
            Debug.WriteLine("+");
        }

        private static void line(int size)
        {
            for (int i = 0; i < size; i++)
            {
                Debug.Write("+---");
            }
        }
    }
}
