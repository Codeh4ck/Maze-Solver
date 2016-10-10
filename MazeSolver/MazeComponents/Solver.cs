using System;
using System.Collections.Generic;
using System.Drawing;
using MazeSolver.MazeComponents.HelperTypes;

namespace MazeSolver.MazeComponents
{
    class Solver
    {
        /// <summary>
        /// The original maze matrix to be solved.
        /// </summary>
        private readonly int[,] MazeMatrix;

        /// <summary>
        /// The number of rows (y-axis) of the maze matrix. 
        /// </summary>                               
        private readonly int MazeRows;

        /// <summary>
        ///  The number of columns (x-axis) of the maze matrix.
        /// </summary>
        private readonly int MazeColumns;

        /// <summary>
        /// Containes the solved maze matrix of <see cref="MazeMatrix"/>.
        /// Null if no solution for the maze exists.
        /// </summary>
        public int[,] SolvedMazeMatrix { get; private set; }

        /// <summary>
        /// A list of points that contains form the solution path.
        /// </summary>
        public List<Point> SolutionPoints = null;

        /// <summary>
        /// Constructor of <see cref="Solver"/>.
        /// Assigns the rows and columns from a given 2D int array maze matrix.
        /// </summary>
        /// <param name="mazeMatrix">The maze matrix to find a solution on.</param>
        public Solver(int[,] mazeMatrix)
        {
            MazeMatrix = mazeMatrix;
            MazeRows = mazeMatrix.GetLength(0);
            MazeColumns = mazeMatrix.GetLength(1);

            SolutionPoints = new List<Point>();
        }

        /// <summary>
        /// Constructor of <see cref="Solver"/>.
        /// Creates a 2D int array maze matrix with x number of <see cref="rows"/> and y number of <see cref="rows"/>
        /// </summary>
        /// <param name="rows">The number of rows the maze matrix will have (y-axis).</param>
        /// <param name="columns">The number of columns the maze matrix will have (x-axis).</param>
        public Solver(int rows, int columns)
        {
            MazeMatrix = new int[rows, columns];
            MazeRows = rows;
            MazeColumns = columns;

            SolutionPoints = new List<Point>();
        }

        /// <summary>
        /// Overloads the <see cref="this"/> operator to provide element access in the 2D integer array maze matrix.
        /// </summary>
        /// <param name="x">The number of the column to check.</param>
        /// <param name="y">The number of the row to check</param>
        /// <returns>The value of coordinate [x, y] in the maze matrix.</returns>
        public int this[int x, int y]
        {
            get { return MazeMatrix[x, y]; }
            set { MazeMatrix[x, y] = value; }
        }

        /// <summary>
        /// Gets the maze matrix the <see cref="Solver"/> is currently working on.
        /// </summary>
        /// <returns>Returns the <see cref="MazeMatrix"/>.</returns>
        public int[,] GetMaze()
        {
            return MazeMatrix;
        }

        /// <summary>
        /// Gets the value of the given-maze node number.
        /// </summary>
        /// <param name="mazeMatrix">The 2D integer array of the maze matrix that contains the node.</param>
        /// <param name="nodeNumber">The number of the node in the <see cref="MazeMatrix"/>.</param>
        /// <returns>The value of <see cref="nodeNumber"/> contained in <see cref="MazeMatrix"/>.</returns>
        private int GetNodeValue(int[,] mazeMatrix, int nodeNumber)
        {
            int MatrixColumns = mazeMatrix.GetLength(1);
            try
            {
                return mazeMatrix[nodeNumber/MatrixColumns, nodeNumber - nodeNumber/MatrixColumns*MatrixColumns];
            }
            catch
            {
                throw new ArgumentOutOfRangeException(nameof(nodeNumber),
                    "GetNodeValue(): The nodeNumber requested points to an out-of-range element on the maze matrix.");
            }
        }

        /// <summary>
        /// Gets the value of the given-maze node number.
        /// </summary>
        /// <param name="mazeMatrix">The 2D integer array of the maze matrix that contains the node.</param>
        /// <param name="nodeNumber">The number of the node in the <see cref="MazeMatrix"/>.</param>
        /// <param name="value">The value to set on <see cref="nodeNumber"/>.</param>
        private void SetNodeValue(int[,] mazeMatrix, int nodeNumber, int value)
        {
            int MatrixColumns = mazeMatrix.GetLength(1);
            mazeMatrix[nodeNumber / MatrixColumns, nodeNumber - nodeNumber / MatrixColumns * MatrixColumns] = value;
        }

        /// <summary>
        /// Solves the <see cref="MazeMatrix"/> and returns a 2D integer array maze matrix with the solution.
        /// </summary>
        /// <param name="startPointX"></param>
        /// <param name="startPointY"></param>
        /// <param name="endPointX"></param>
        /// <param name="endPointY"></param>
        /// <returns></returns>
        public int[,] SolveMazeMatrix(int startPointX, int startPointY, int endPointX, int endPointY)
        {
            int EntranceNode = startPointY * MazeColumns + startPointX;
            int ExitNode = endPointY * MazeColumns + endPointX;

            return FindMazeExit(EntranceNode, ExitNode);
        }

        /// <summary>
        /// Converts the given <see cref="nodeNumber"/> of the given <see cref="MazeMatrix"/> to an X, Y coordinate.
        /// </summary>
        /// <param name="mazeMatrix">The 2D integer array of the maze matrix that contains the node.</param>
        /// <param name="nodeNumber">The number of the node in the <see cref="MazeMatrix"/>.</param>
        /// <returns>A point containing the position of the <see cref="nodeNumber"/>.</returns>
        public Point ConvertNodeToPoint(int[,] mazeMatrix, int nodeNumber)
        {
            try
            {
                int MatrixColumns = mazeMatrix.GetLength(1);
                return new Point(nodeNumber/MatrixColumns, nodeNumber - nodeNumber/MatrixColumns*MatrixColumns);
            }
            catch
            {
                throw new ArgumentOutOfRangeException(nameof(nodeNumber),
                   "ConvertNodeToPoint(): The nodeNumber requested points to an out-of-range element on the maze matrix.");
            }
        }

        /// <summary>
        /// Tests if <see cref="nodeNumber"/>, which is adjacent to <see cref="currentNode"/> is inside the maze matrix bounds at the given <see cref="direction"/>.
        /// </summary>
        /// <param name="nodeNumber">The adjacent node number of the maze matrix to test.</param>
        /// <param name="currentNode">The current node number, which is a reference point of <see cref="nodeNumber"/>.</param>
        /// <param name="direction">The direction at which the <see cref="nodeNumber"/> should be.</param>
        /// <returns>True if <see cref="nodeNumber"/> is in the given <see cref="direction"/> from <see cref="currentNode"/>. Otherwise, false.</returns>
        private bool TestRelatedLegitimacy(int nodeNumber, int currentNode, MazeDirections direction)
        {
            int MazeArea = MazeColumns * MazeRows;
            switch (direction)
            {
                case MazeDirections.NORTH:
                    return nodeNumber >= 0;
                case MazeDirections.SOUTH:                    
                    return nodeNumber < MazeArea;
                case MazeDirections.EAST:
                    return nodeNumber < MazeArea && nodeNumber/MazeColumns == currentNode/MazeColumns;
                case MazeDirections.WEST:
                    return nodeNumber >= 0 && nodeNumber/MazeColumns == currentNode/MazeColumns;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
        }

        /// <summary>
        /// Adds the adjacent node of <param name="currentNode"> to the nodes <param name="queue"/>.</param>
        /// </summary>
        /// <param name="mazeMatrix">The matrix containing the queue, tracking points and given nodes.</param>
        /// <param name="queue">A reference to the integer array which represents the node queue.</param>
        /// <param name="trackingPoints">A reference to the integer array which represents the tracking points.</param>
        /// <param name="currentNode">The node which is the currently processed on in the <param name="queue">.</param>"/></param>
        /// <param name="previousNode">The previous node which was processed on in the <param name="queue">.</param></param>
        /// <param name="direction">The direction on which to check for an adjacent node.</param>
        public void EqueueNextNode(int[,] mazeMatrix, ref int[] queue, ref int[] trackingPoints, int currentNode, ref int previousNode, MazeDirections direction)
        {
            int AdjacentNode;

            switch (direction)
            {
                case MazeDirections.NORTH:
                    AdjacentNode = currentNode - MazeColumns;
                    break;
                case MazeDirections.SOUTH:
                    AdjacentNode = currentNode + MazeColumns;
                    break;
                case MazeDirections.EAST:
                    AdjacentNode = currentNode + 1;
                    break;
                case MazeDirections.WEST:
                    AdjacentNode = currentNode - 1;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, "Enqueue: Adjacent node appears to be either invalid or out of the matrix.");
            }

            if (!TestRelatedLegitimacy(AdjacentNode, currentNode, direction)) return;
            if (GetNodeValue(MazeMatrix, AdjacentNode) != (int) MazeNodeStatus.Ready) return;
            if (GetNodeValue(mazeMatrix, AdjacentNode) != (int)MazeNodeStatus.Ready) return;

            queue[previousNode] = AdjacentNode;
            trackingPoints[previousNode] = currentNode;
            SetNodeValue(mazeMatrix, AdjacentNode, (int) MazeNodeStatus.Busy);
            previousNode++;
        }

        /// <summary>
        /// Finds the exit path of the given <see cref="MazeMatrix"/> from <see cref="entrance"/> to <see cref="exit"/>.
        /// </summary>
        /// <param name="entrance">The entrance node of the <see cref="MazeMatrix"/>.</param>
        /// <param name="exit">The exit node of the <see cref="MazeMatrix"/>.</param>
        /// <returns>A 2D integer array containing the solved maze matrix.</returns>
        private int[,] FindMazeExit(int entrance, int exit)
        {
            int CurrentMazeRows = MazeRows;
            int CurrentMazeColumns = MazeColumns;
            int MazeArea = CurrentMazeRows * CurrentMazeColumns;

            int[] Queue = new int[MazeArea];
            int[] TrackingPoints = new int[MazeArea];

            int NextNode = 0;
            int PreviousNode = 0;

            if (GetNodeValue(MazeMatrix, entrance) != 0 ||
                GetNodeValue(MazeMatrix, exit) != 0)
            {
                return null;
            }

            int[,] WalkableMazeTiles = new int[CurrentMazeRows, CurrentMazeColumns];

            for (int x = 0; x < CurrentMazeRows; x++)
            {
                for (int y = 0; y < CurrentMazeColumns; y++)
                {
                    WalkableMazeTiles[x, y] = (int)MazeNodeStatus.Ready;
                }                    
            }
                
            Queue[PreviousNode] = entrance;
            TrackingPoints[PreviousNode] = -1;

            PreviousNode++;

            int CurrentNode;

            while (NextNode != PreviousNode)	
            {
                if (Queue[NextNode] == exit)
                    break;

                CurrentNode = Queue[NextNode];

                EqueueNextNode(WalkableMazeTiles, ref Queue, ref TrackingPoints, CurrentNode, ref PreviousNode, MazeDirections.WEST);
                EqueueNextNode(WalkableMazeTiles, ref Queue, ref TrackingPoints, CurrentNode, ref PreviousNode, MazeDirections.EAST);
                EqueueNextNode(WalkableMazeTiles, ref Queue, ref TrackingPoints, CurrentNode, ref PreviousNode, MazeDirections.NORTH);
                EqueueNextNode(WalkableMazeTiles, ref Queue, ref TrackingPoints, CurrentNode, ref PreviousNode, MazeDirections.SOUTH);              

                SetNodeValue(WalkableMazeTiles, CurrentNode, (int) MazeNodeStatus.Processed);
                NextNode++;

            }
            
            int[,] SolvedMaze = new int [CurrentMazeRows, CurrentMazeColumns];
            for (int x = 0; x < CurrentMazeRows; x++)
            {
                for (int y = 0; y < CurrentMazeColumns; y++)
                {
                    SolvedMaze[x, y] = MazeMatrix[x, y];
                }
            }
                

            CurrentNode = exit;
            SetNodeValue(SolvedMaze, CurrentNode, (int) MazeNodeStatus.Path);

            for (int x = NextNode; x >= 0; x--)
            {
                if (Queue[x] == CurrentNode)
                {
                    CurrentNode = TrackingPoints[x];

                    if (CurrentNode == -1)
                        return SolvedMaze;

                    SetNodeValue(SolvedMaze, CurrentNode, (int) MazeNodeStatus.Path);
                    SolutionPoints.Add(ConvertNodeToPoint(SolvedMaze, CurrentNode));
                }
            }

            return null;
        }

    }
}
