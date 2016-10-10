namespace MazeSolver.MazeComponents.HelperTypes
{
    /// <summary>
    /// All possible maze node (block) statuses.
    /// </summary>
    enum MazeNodeStatus
    {
        Ready = 0,
        Busy = 1,
        Processed = 2,
        Wall = 3,
        Path = 4,
        WalkableTile = Ready
    }
}
