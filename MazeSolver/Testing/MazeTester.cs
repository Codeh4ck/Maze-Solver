using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using MazeSolver.MazeComponents;

// Unit testing framework is for some reason unavailable in my VS edition.
// I wrote my own unit testing functions with a dummy maze.

namespace MazeSolver.Testing
{
    class MazeTester
    {
        /// <summary>
        /// The string form of the sample maze.
        /// </summary>
        private string SampleMaze =
            "0,0,0,3,1,0,0,0\n" +
            "0,3,0,3,3,0,0,0\n" +
            "0,3,0,0,3,0,0,0\n" +
            "0,3,0,0,0,3,3,0\n" +
            "0,3,3,3,0,0,0,0\n" +
            "3,0,0,3,0,0,0,0\n" +
            "0,0,3,0,3,0,3,2\n";


        private Solver MazeSolver;
        private MazeLoader MazeLoader;
        private int[,] MazeMatrix;

        private Point? EntrancePoint = null;
        private Point? ExitPoint = null;


        /// <summary>
        /// Load a maze from a dummy file and perform a solution.
        /// </summary>
        /// <returns>True if the maze has been solved, otherwise false.</returns>
        public bool LoadAndSolveTestMaze()
        {
            LoadSampleMaze();
            if (SolveMazeMatrix())
            {
                Debug.WriteLine("LoadAndSolveTestMaze(): Test maze has been solved.");
                return true;
            }

            Debug.WriteLine("LoadAndSolveTestMaze(): Unable to solve test matrix.");
            return false;
        }

        /// <summary>
        /// Writes the maze in a dummy file and attempts to load it.
        /// If the file is loaded successfully, a Solver is instantiated.
        /// The entrance and exit points are also set.
        /// </summary>
        private void LoadSampleMaze()
        {
            string TempFile = Path.Combine(Path.GetTempPath(), Path.GetTempFileName());

            try
            {
                File.WriteAllText(TempFile, SampleMaze);
                Debug.WriteLine("LoadSampleMaze(): Temporary file containing sample maze has been written.");
            }
            catch (IOException ioEx)
            {
                Debug.WriteLine("LoadSampleMaze(): An IOException has been thrown: {0}.", ioEx.Message);
                AttemptFileDelete(TempFile);
            }


            Debug.WriteLine("LoadSampleMaze(): Loading maze matrix from the temporary file...");
            try
            {
                MazeLoader = new MazeLoader();
                MazeMatrix = MazeLoader.LoadCoordinatesFromFile(TempFile);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("LoadSampleMaze(): An exception has been thrown while loading the matrix: {0}.", ex.Message);
                AttemptFileDelete(TempFile);
            }

            MazeSolver = new Solver(MazeMatrix);
            MazeMatrix = MazeSolver.GetMaze();

            EntrancePoint = new Point(MazeLoader.EntranceCoordinates.X, MazeLoader.EntranceCoordinates.Y);
            ExitPoint = new Point(MazeLoader.ExitCoordinates.X, MazeLoader.ExitCoordinates.Y);

            if (EntrancePoint == null || ExitPoint == null)            
                Debug.WriteLine("LoadSampleMaze(): Entrance point or exit point is either invalid or non-existent.");                        

            AttemptFileDelete(TempFile);
        }

        /// <summary>
        /// Checks if all necessary elements are instantiated and attempts to solve the maze.        
        /// </summary>
        /// <returns>True of the maze is solved, otherwise false.</returns>
        private bool SolveMazeMatrix()
        {
            Debug.Assert(MazeSolver != null);
            Debug.Assert(MazeLoader != null);
            Debug.Assert(MazeMatrix != null);

            Debug.Assert(EntrancePoint != null);
            Debug.Assert(ExitPoint != null);

            Debug.WriteLine("SolveMazeMatrix(): All tests have passed. Starting solving maze...");
            try
            {
                MazeMatrix = MazeSolver.SolveMazeMatrix(EntrancePoint.Value.X, EntrancePoint.Value.Y, ExitPoint.Value.X,
                    ExitPoint.Value.Y);

                if (MazeMatrix != null)
                {
                    Debug.WriteLine("SolveMazeMatrix(): Maze has been solved.");
                    return true;
                }
                else
                {
                    Debug.WriteLine("SolveMazeMatrix(): There is no solution to the given maze.");
                    return false;
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine("SolveMazeMatrix(): An exception has been thrown while solving the maze: {0}.", ex.Message);
                return false;
            }
        }


        /// <summary>
        /// Safe file delete.
        /// </summary>
        /// <param name="filePath">The file to delete.</param>
        private void AttemptFileDelete(string filePath)
        {
            try
            {
                File.Delete(filePath);
            }
            catch (Exception ioEx)
            {
                Debug.WriteLine("AttemptFileDelete(): Could not delete file.\nAn exception has been thrown: {0}.", ioEx.Message);
            }
        }
    }
}
