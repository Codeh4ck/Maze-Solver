using System.Collections.Generic;

namespace MazeSolver.MazeComponents.HelperTypes
{
    public class MazeParts
    {
        public List<string> Lines { get; set; }

        public int Columns { get; set; }

        public int Rows { get; set; }

        public MazeParts()
        {
            Lines = new List<string>();
        }
    }
}
