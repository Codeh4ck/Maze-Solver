using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
        private IMazeValidator MazeValidator { get; set; }

        public MazeLoader()
        {
            MazeValidator = new MazeValidator();
        }

        /// <summary>
        /// Loads the maze matrix from the maze blueprint contained in the text file.
        /// </summary>
        /// <param name="fileName">The text file containing the maze blueprint.</param>
        /// <returns>A 2D integer array containing the matrix of the maze.</returns>
        public int[,] LoadCoordinatesFromFile(string fileName)
        {
            if (!File.Exists(fileName))
                throw new IOException("The maze file you are trying to load does not exist.");

            if (!MazeValidator.ValidateMazeFile(fileName))
                throw new IOException("The maze file contains no entrance or exit point. Both points must be set.");

            int Rows = 0;
            int Columns = 0;

            string Line = string.Empty;

            List<string> Lines = new List<string>();

            using (StreamReader Reader = new StreamReader(fileName))
            {
                while ((Line = Reader.ReadLine()) != null)
                {
                    string[] LineParts = Line.Split(',');

                    if (Columns == 0)
                        Columns = LineParts.Length;
                    else
                    {
                        if (Columns != LineParts.Length)
                            throw new Exception("The maze file appears to be corrupt. One of the lines has more/less elements than the previous one.");
                    }
                    Lines.Add(Line);
                    Rows++;
                }                
            }

            int[,] Maze = new int[Rows, Columns];

            for (int x = 0; x < Rows; x++)
            {
                string[] LineParts = Lines[x].Split(',');
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
