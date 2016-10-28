using System.Drawing;
using MazeSolver.MazeComponents.Interfaces;

namespace MazeSolver.MazeComponents
{
    class MazeLoader
    {
        /// <summary>
        /// The coordinates (x, y) of the entrance.
        /// </summary>         
        public Point EntranceCoordinates { get; set; }

        /// <summary>
        /// The coordinates (x, y) of the exit.
        /// </summary>
        public Point ExitCoordinates { get; set; }

        /// <summary>
        /// Used to validate the maze file and its contents.
        /// </summary>
        internal IMazeValidator MazeValidator { get; set; }

        /// <summary>
        /// Used to read the maze file and 
        /// </summary>
        internal IMazeReader MazeReader { get; set; }

        /// <summary>
        /// Initializes the loader - must be called after object creation.
        /// </summary>
        public void InitializeLoader()
        {
            /* Creating the validator and reader instances here, which is shockingly bad.
             * This is done to prevent automatic instantiation of them in the constructor
             * in order to unit-test the loader. This gruesome hack is not necessary
             * if a DI framework is used. */
            MazeValidator = new MazeValidator();
            MazeReader = new MazeReader();
        }

        /// <summary>
        /// Loads the maze matrix from the maze blueprint contained in the text file.
        /// </summary>
        /// <param name="fileName">The text file containing the maze blueprint.</param>
        /// <returns>A 2D integer array containing the matrix of the maze.</returns>
        public int[,] LoadCoordinatesFromFile(string fileName)
        {
            MazeValidator.ValidateMazeFile(fileName);

            var parts = MazeReader.ReadMaze(fileName);

            int[,] Maze = new int[parts.Rows, parts.Columns];

            for (int x = 0; x < parts.Rows; x++)
            {
                string[] LineParts = parts.Lines[x].Split(',');
                for (int y = 0; y < LineParts.Length; y++)
                {
                    int NodeStatus;

                    if (int.TryParse(LineParts[y], out NodeStatus))                    
                        Maze[x, y] = NodeStatus;

                    if (NodeStatus == Settings.MAZE_ENTRANCE_CODE)
                    {
                        Maze[x, y] = 0;
                        EntranceCoordinates = new Point(x, y);
                    }
                
                    if (NodeStatus == Settings.MAZE_EXIT_CODE)
                    {
                        Maze[x, y] = 0;
                        ExitCoordinates = new Point(x, y);
                    }

                }
            }

            return Maze;
        }
    }
}
