namespace Milestone.Models
{
    public class BoardModel
    {

        public CellModel[,] Grid;
        public double difficulty;
        private int size;


        public int getSize()
        {
            return size;
        }

        public BoardModel(int size)
        {
            Grid = new CellModel[size, size];
            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    Grid[row, col] = new CellModel();
                }
            }
            this.size = size;
        }

        public void setupLiveNeighbors(double difficulty)
        {
            Random rand = new Random();

            int liveGrids = Convert.ToInt32(difficulty * Grid.Length);
            int liveCounter = 0;
            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    int random = rand.Next(0, 2);

                    if (liveCounter < liveGrids)
                    {
                        if (random == 0)
                        {
                            Grid[row, col].Live = false;
                        }
                        else
                        {
                            Grid[row, col].Live = true;
                            liveCounter++;
                        }
                    }
                }
            }
        }

        public void calculateLiveNeighbors()
        {
            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    if (Grid[row, col].Live)
                    {
                       // Grid[row, col].ButtonState = 9;

                    }
                    else
                    {

                        // Top
                        if (inArray(row - 1, col))
                        {
                            if (Grid[row - 1, col].Live)
                            {
                                Grid[row, col].Neighbors++;
                                
                            }
                        }

                        //Top Right
                        if (inArray(row - 1, col - 1))
                        {
                            if (Grid[row - 1, col - 1].Live)
                            {
                                Grid[row, col].Neighbors++;
                                
                            }
                        }

                        // Top Left
                        if (inArray(row - 1, col + 1))
                        {
                            if (Grid[row - 1, col + 1].Live)
                            {
                                Grid[row, col].Neighbors++;
                                
                            }
                        }

                        // Right
                        if (inArray(row, col - 1))
                        {
                            if (Grid[row, col - 1].Live)
                            {
                                Grid[row, col].Neighbors++;
                                
                            }
                        }

                        // Left
                        if (inArray(row, col + 1))
                        {
                            if (Grid[row, col + 1].Live)
                            {
                                Grid[row, col].Neighbors++;
                                
                            }
                        }

                        // Bottom
                        if (inArray(row + 1, col))
                        {
                            if (Grid[row + 1, col].Live)
                            {
                                Grid[row, col].Neighbors++;
                                
                            }
                        }

                        // Bottom left
                        if (inArray(row + 1, col - 1))
                        {
                            if (Grid[row + 1, col - 1].Live)
                            {
                                Grid[row, col].Neighbors++;
                                
                            }
                        }

                        // Bottom right
                        if (inArray(row + 1, col + 1))
                        {
                            if (Grid[row + 1, col + 1].Live)
                            {
                                Grid[row, col].Neighbors++;
                                
                            }
                        }
                    }
                }
            }
        }

        public bool inArray(int row, int col)
        {
            return (row >= 0 && row < size && col >= 0 && col < size);
        }


        public bool winner()
        {
            int counter = 0;
            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    if (Grid[row, col].Live == true || Grid[row, col].Visited == true)
                    {
                        counter++;
                    }
                }
            }
            if (counter == (size * size))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void FloodFill(int Row, int Col)
        {
            if (Grid[Row,Col].Live == true)
            {
                Grid[Row, Col].ButtonState = 9;
            }
            else if (Grid[Row, Col].Neighbors != 0)
            {
                Grid[Row, Col].ButtonState = Grid[Row, Col].Neighbors;
            }
            else if (Grid[Row, Col].Neighbors == 0)
            {
                Grid[Row, Col].ButtonState = Grid[Row, Col].Neighbors;
                // Top
                if (inArray(Row - 1, Col))
                {
                    if (Grid[Row - 1, Col].Live == false && !Grid[Row - 1, Col].Visited)
                    {
                        Grid[Row - 1, Col].Visited = true;
                        FloodFill(Row - 1, Col);
                    }
                }

                // Top Right
                if (inArray(Row - 1, Col - 1))
                {
                    if (Grid[Row - 1, Col - 1].Live == false && !Grid[Row - 1, Col - 1].Visited)
                    {
                        Grid[Row - 1, Col - 1].Visited = true;
                        FloodFill(Row - 1, Col - 1);
                    }
                }

                // Top Left
                if (inArray(Row - 1, Col + 1))
                {
                    if (Grid[Row - 1, Col + 1].Live == false && !Grid[Row - 1, Col + 1].Visited)
                    {
                        Grid[Row - 1, Col + 1].Visited = true;
                        FloodFill(Row - 1, Col + 1);
                    }
                }

                // Right
                if (inArray(Row, Col - 1))
                {
                    if (Grid[Row, Col - 1].Live == false && !Grid[Row, Col - 1].Visited)
                    {
                        Grid[Row, Col - 1].Visited = true;
                        FloodFill(Row, Col - 1);
                    }
                }

                // Left
                if (inArray(Row, Col + 1))
                {
                    if (Grid[Row, Col + 1].Live == false && !Grid[Row, Col + 1].Visited)
                    {
                        Grid[Row, Col + 1].Visited = true;
                        FloodFill(Row, Col + 1);
                    }
                }

                // Bottom
                if (inArray(Row + 1, Col))
                {
                    if (Grid[Row + 1, Col].Live == false && !Grid[Row + 1, Col].Visited)
                    {
                        Grid[Row + 1, Col].Visited = true;
                        FloodFill(Row + 1, Col);
                    }
                }

                // Bottom Right
                if (inArray(Row + 1, Col - 1))
                {
                    if (Grid[Row + 1, Col - 1].Live == false && !Grid[Row + 1, Col - 1].Visited)
                    {
                        Grid[Row + 1, Col - 1].Visited = true;
                        FloodFill(Row + 1, Col - 1);
                    }
                }

                // Bottom Left
                if (inArray(Row + 1, Col + 1))
                {
                    if (Grid[Row + 1, Col + 1].Live == false && !Grid[Row + 1, Col + 1].Visited)
                    {
                        Grid[Row + 1, Col + 1].Visited = true;
                        FloodFill(Row + 1, Col + 1);
                    }
                }
            }
        }

        private void resetVisited()
        {
            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    Grid[row, col].Visited = false;
                }
            }
        }

        private void resetNeighbors()
        {
            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    Grid[row, col].Neighbors = 0;
                }
            }
        }

        private void resetLive()
        {
            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    Grid[row, col].Live = false;
                }
            }
        }

        public void resetBoard()
        {
            resetLive();
            resetNeighbors();
            resetVisited();
        }
    }
}
