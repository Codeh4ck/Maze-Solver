using System;
using System.IO;
using MazeSolver.MazeComponents.HelperTypes;
using MazeSolver.MazeComponents.Interfaces;

namespace MazeSolver.MazeComponents
{
    public class MazeReader : IMazeReader
    {
        public MazeParts ReadMaze(string fileName)
        {
            var parts = new MazeParts();
            
            using (StreamReader Reader = new StreamReader(fileName))
            {
                string Line = string.Empty;
                while ((Line = Reader.ReadLine()) != null)
                {
                    string[] LineParts = Line.Split(',');

                    if (parts.Columns == 0)
                        parts.Columns = LineParts.Length;
                    else
                    {
                        if (parts.Columns != LineParts.Length)
                            throw new Exception("The maze file appears to be corrupt. One of the lines has more/less elements than the previous one.");
                    }
                    parts.Lines.Add(Line);
                    parts.Rows++;
                }
            }

            return parts;
        }
    }
}
