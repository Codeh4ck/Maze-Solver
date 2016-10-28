using System.IO;
using MazeSolver.MazeComponents.Interfaces;

namespace MazeSolver.MazeComponents
{
    public class MazeValidator : IMazeValidator
    {
        /// <summary>
        /// Checks whetherh the maze blueprint in the <see cref="filePath"/>
        /// contains an entrance and an exit.
        /// </summary>
        /// <param name="filePath">The text file containing the maze blueprint.</param>
        /// <returns>True if the maze blueprint contains an ex</returns>
        public bool ValidateMazeFile(string filePath)
        {
            {
                if (!File.Exists(filePath))
                    throw new IOException("The maze file you are trying to load does not exist.");

                string MazeFileText = File.ReadAllText(filePath);


                return MazeFileText.Contains(Settings.MAZE_ENTRANCE_CODE.ToString()) &&
                    MazeFileText.Contains(Settings.MAZE_EXIT_CODE.ToString());
            }
        }
    }
}