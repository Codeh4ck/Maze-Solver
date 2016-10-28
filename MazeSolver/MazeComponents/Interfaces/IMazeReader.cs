using MazeSolver.MazeComponents.HelperTypes;

namespace MazeSolver.MazeComponents.Interfaces
{
    public interface IMazeReader
    {
        MazeParts ReadMaze(string fileName);
    }
}
