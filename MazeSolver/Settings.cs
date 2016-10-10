using System.Drawing;

namespace MazeSolver
{
    class Settings
    {
        public static int MAZE_ROWS = 16;
        public static int MAZE_COLUMNS = 20;
        public static int MAZE_BLOCK_SIZE = 20;

        public const int MAZE_ENTRANCE_CODE = 1;
        public const int MAZE_EXIT_CODE = 2;
    }

    class ColorSettings
    {
        /// <summary>
        /// The color of the maze PictureBox'es grid.
        /// </summary>
        public static Color MazeGridColor = Color.DimGray;

        /// <summary>
        /// The pen for the color <see cref="MazeGridColor"/>
        /// </summary>
        public static Pen MazeGridColorPen = new Pen(MazeGridColor);

        /// <summary>
        /// The color of the maze's walkable tiles.
        /// </summary>
        public static Color WalkableTileColor = Color.White;

        /// <summary>
        /// The brush for the color <see cref="WalkableTileColor"/>
        /// </summary>
        public static SolidBrush WalkableTileColorBrush = new SolidBrush(WalkableTileColor);

        /// <summary>
        /// The color of the maze's walkable tiles.
        /// </summary>
        public static Color WallTileColor = Color.SlateGray;

        /// <summary>
        /// The brush for the color <see cref="WallTileColor"/>
        /// </summary>
        public static SolidBrush WallTileColorBrush = new SolidBrush(WallTileColor);

        /// <summary>
        /// The color of the maze's entrance tile.
        /// </summary>
        public static Color EntranceTileColor = Color.LimeGreen;

        /// <summary>
        /// The brush for the color <see cref="EntranceTileColor"/>
        /// </summary>
        public static SolidBrush EntranceTileColorBrush = new SolidBrush(EntranceTileColor);

        /// <summary>
        /// The color of the maze's exit tile.
        /// </summary>
        public static Color ExitTileColor = Color.OrangeRed;

        /// <summary>
        /// The brush for the color <see cref="ExitTileColor"/>
        /// </summary>
        public static SolidBrush ExitTileColorBrush = new SolidBrush(ExitTileColor);

        /// <summary>
        /// The color of the maze's walked tiles.
        /// </summary>
        public static Color WalkedTileColor = Color.DeepSkyBlue;

        /// <summary>
        /// The brush for the color <see cref="WalkedTileColor"/>
        /// </summary>
        public static SolidBrush WalkedTileColorBrush = new SolidBrush(WalkedTileColor);
    }
}
