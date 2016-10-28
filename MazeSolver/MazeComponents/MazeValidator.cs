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
        public void ValidateMazeFile(string filePath)
        {
            {
                if (!File.Exists(filePath))
                    throw new IOException("The maze file you are trying to load does not exist.");

                var txt = File.ReadAllText(filePath);

                if (!txt.Contains(Settings.MAZE_ENTRANCE_CODE.ToString()) ||
                    !txt.Contains(Settings.MAZE_EXIT_CODE.ToString()))
                    throw new IOException("The maze file contains no entrance or exit point. Both points must be set.");
            }
        }
    }
}